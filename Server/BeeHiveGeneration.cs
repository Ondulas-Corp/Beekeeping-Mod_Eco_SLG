using Beekeeping.Server;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Services;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Time;
using Eco.World.Blocks;
using Beekeeping.Server.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BeeHiveGeneration : IModKitPlugin, IInitializablePlugin, IConfigurablePlugin
{
    // Backwards-compat statics for ClusterManager (delegates to config)
    public static int   CLUSTER_RADIUS        => Config.ClusterRadius;
    public static int   MIN_CLUSTER_SPACING   => Config.MinClusterSpacing;
    public static float CLUSTER_DEATH_THRESHOLD => Config.ClusterDeathThreshold;

    private PluginConfig<Beekeeping.Server.BeekeepingConfig> config;
    public IPluginConfig PluginConfig => config;
    public static Beekeeping.Server.BeekeepingConfig Config => instance?.config?.Config ?? new Beekeeping.Server.BeekeepingConfig();
    public Eco.Core.Utils.ThreadSafeAction<object, string> ParamChanged { get; set; } = new Eco.Core.Utils.ThreadSafeAction<object, string>();

    private List<HiveCluster> hiveClusters = new List<HiveCluster>();
    private static BeeHiveGeneration instance;
    private static Random random = new Random();

    public BeeHiveGeneration() => config = new PluginConfig<Beekeeping.Server.BeekeepingConfig>("Beekeeping");

    public string GetCategory() => "Beekeeping Cluster System";
    public string GetStatus() => $"Managing {hiveClusters.Count} hive clusters";
    public object GetEditObject() => config.Config;
    public void OnEditObjectChanged(object o, string param) => this.SaveConfig();

    public void Initialize(TimedTask timer)
    {
        instance = this;
        config.SaveAsync().Wait(); // Write Configs/Beekeeping.eco with defaults on first run

        // Load cluster centers from file
        var savedCenters = ClusterManager.LoadClusterCenters();

        // Destroy any leftover vacant swarms (system removed)
        var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
        var vacantHives = worldObjectManager.All
            .Where(obj => obj.GetType() == typeof(VacantSauvageBeehiveObject))
            .ToList();
        foreach (var vacant in vacantHives)
            vacant.Destroy();

        // Check if wild hives exist in world
        var existingWildHives = worldObjectManager.All.Count(obj =>
            obj.GetType() == typeof(OccupiedSauvageBeehiveObject));

        if (savedCenters.Count > 0)
        {
            // Rebuild clusters from saved centers
            hiveClusters = ClusterManager.RebuildClustersFromCenters(savedCenters);

            // Check: If save file exists but no wild hives found, do a reset
            if (existingWildHives == 0)
            {
                hiveClusters.Clear();
                hiveClusters = ClusterManager.CreateInitialClusters();
                ClusterManager.SaveClusterCenters(hiveClusters);
            }
            else
            {
                // Normal case - check for empty clusters and schedule regeneration
                CheckAndRegenerateEmptyClusters();
            }
        }
        else
        {
            // NO SAVE FILE - perform full reset
            var allWildHives = worldObjectManager.All
                .Where(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject))
                .ToList();

            foreach (var hive in allWildHives)
                hive.Destroy();

            // Create fresh clusters
            hiveClusters = ClusterManager.CreateInitialClusters();
            ClusterManager.SaveClusterCenters(hiveClusters);
        }
    }

    private void CheckAndRegenerateEmptyClusters()
    {
        foreach (var cluster in hiveClusters)
        {
            // Update hive counts first
            ClusterManager.UpdateExistingHives(cluster);

            if (!cluster.IsAlive) continue;

            int availableSlots = Config.MaxHivesPerCluster - cluster.CurrentHiveCount;
            if (availableSlots <= 0) continue;

            // Check plant health for regeneration eligibility
            var plantPercentage = cluster.MaxPlantCount > 0 ? (float)cluster.CurrentPlantCount / cluster.MaxPlantCount : 0f;
            if (plantPercentage < 0.17f) continue;

            // Schedule one spawn per missing hive, staggered to avoid burst
            for (int i = 0; i < availableSlots; i++)
            {
                var regenDelay = Config.MinRegenHours + (random.NextDouble() * (Config.MaxRegenHours - Config.MinRegenHours));
                var delaySeconds = regenDelay * 3600.0;
                ScheduleSpawn(delaySeconds, cluster, new List<Vector3i>(cluster.ExistingHivePositions));
            }

            cluster.LastHarvestTime = WorldTime.Seconds;
        }
    }

    private static async void ScheduleSpawn(double delaySeconds, HiveCluster targetCluster, List<Vector3i> existingHives)
    {
        await System.Threading.Tasks.Task.Delay((int)(delaySeconds * 1000));

        try
        {
            if (instance == null) return;

            // Use the actual cluster reference instead of searching for it
            if (targetCluster == null || !targetCluster.IsAlive)
                return;

            // Refresh hive count from world state before spawning (guards against overlapping batches)
            ClusterManager.UpdateExistingHives(targetCluster);
            if (targetCluster.CurrentHiveCount >= Config.MaxHivesPerCluster)
            {
                targetCluster.RegenerationInProgress = false;
                return;
            }

            // Global world cap check
            if (Config.MaxTotalWildHives > 0)
            {
                var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
                var globalCount = worldObjectManager.All.Count(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject));
                if (globalCount >= Config.MaxTotalWildHives)
                {
                    targetCluster.RegenerationInProgress = false;
                    return;
                }
            }

            var hivePos = instance.FindSuitableHivePosition(targetCluster.CenterPosition, existingHives);
            if (hivePos.HasValue)
            {
                var spawnedHive = WorldObjectManager.ForceAdd(typeof(OccupiedSauvageBeehiveObject), null, hivePos.Value, Quaternion.Identity);

                if (spawnedHive != null)
                {
                    targetCluster.ExistingHivePositions.Add(hivePos.Value);
                    targetCluster.RegenerationInProgress = false;
                }
                else
                {
                    targetCluster.RegenerationInProgress = false;
                }
            }
            else
            {
                // Clear flag even if spawn failed (CASCADE PREVENTION)
                targetCluster.RegenerationInProgress = false;
            }
        }
        catch (Exception)
        {
            // Clear flag on any error (CASCADE PREVENTION)
            try
            {
                if (targetCluster != null)
                    targetCluster.RegenerationInProgress = false;
            }
            catch { }
        }
    }

    private Vector3i? FindSuitableHivePosition(Vector3i clusterCenter, List<Vector3i> existingHives)
    {
        int attempts = 0;
        int maxAttempts = 50;

        while (attempts < maxAttempts)
        {
            attempts++;

            var angle = random.NextDouble() * 2 * System.Math.PI;
            var distance = random.NextDouble() * Config.ClusterRadius;

            var x = (int)(clusterCenter.X + System.Math.Cos(angle) * distance);
            var z = (int)(clusterCenter.Z + System.Math.Sin(angle) * distance);

            // Use the actual terrain surface instead of scanning ±10 blocks from center Y
            int surfaceY = Eco.World.World.GetTopSolidBlockY(new Vector2i(x, z));
            var groundBlock = Eco.World.World.GetBlock(new Vector3i(x, surfaceY, z));
            if (groundBlock is DirtBlock && !(groundBlock is DesertSandBlock))
            {
                var hivePos = new Vector3i(x, surfaceY + 1, z);
                var aboveBlock = Eco.World.World.GetBlock(hivePos);
                if (aboveBlock == null || aboveBlock is EmptyBlock)
                {
                    bool tooClose = false;
                    foreach (var existing in existingHives)
                    {
                        if (Vector3i.Distance(hivePos, existing) < 8)
                        {
                            tooClose = true;
                            break;
                        }
                    }

                    if (!tooClose)
                        return hivePos;
                }
            }
        }

        return null;
    }

    // Single method to handle all hive destruction/harvest events
    public static void OnHiveDestroyed(Vector3i hivePosition)
    {
        if (instance == null) return;

        try
        {
            var cluster = ClusterManager.FindClusterContaining(hivePosition, instance.hiveClusters);
            if (cluster == null) return;

            if (!cluster.IsAlive)
            {
                cluster.ExistingHivePositions.Remove(hivePosition);
                return;
            }

            var currentTime = WorldTime.Seconds;

            // Force update existing hives to ensure accurate count before removal
            ClusterManager.UpdateExistingHives(cluster);

            // Try to remove the destroyed hive from tracking
            bool wasRemoved = cluster.ExistingHivePositions.Remove(hivePosition);

            // If removal failed due to coordinate mismatch, just decrement the count by removing any hive
            if (!wasRemoved && cluster.ExistingHivePositions.Count > 0)
                cluster.ExistingHivePositions.RemoveAt(cluster.ExistingHivePositions.Count - 1);

            int hivesAfterRemoval = cluster.CurrentHiveCount;

            // Check if cluster needs regeneration - REGENERATE ONLY if ≤2 hives remaining
            if (hivesAfterRemoval > 2) return;

            // Check plant health for regeneration eligibility
            var plantPercentage = cluster.MaxPlantCount > 0 ? (float)cluster.CurrentPlantCount / cluster.MaxPlantCount : 0f;
            if (plantPercentage < 0.17f) return;

            // Check if regeneration is already in progress (CASCADE PREVENTION)
            if (cluster.RegenerationInProgress) return;

            // Set regeneration flag BEFORE spawning (CASCADE PREVENTION)
            cluster.RegenerationInProgress = true;

            // Calculate available slots
            int availableSlots = Config.MaxHivesPerCluster - cluster.CurrentHiveCount;

            if (availableSlots > 0)
            {
                // Randomly choose how many hives to spawn
                int maxToSpawn = Math.Min(5, availableSlots);
                int toSpawn = random.Next(1, maxToSpawn + 1);

                // Schedule spawns with async delays
                for (int i = 0; i < toSpawn; i++)
                {
                    var delay = (Config.MinRegenHours + (random.NextDouble() * (Config.MaxRegenHours - Config.MinRegenHours))) * 3600.0;
                    ScheduleSpawn(delay, cluster, new List<Vector3i>(cluster.ExistingHivePositions));
                }

                cluster.LastHarvestTime = currentTime;
            }
            else
            {
                cluster.RegenerationInProgress = false;
            }
        }
        catch (Exception)
        {
            // Swallow - regeneration is best-effort
        }
    }

    // Static methods for external access
    public static List<HiveCluster> GetAllClusters() => instance?.hiveClusters ?? new List<HiveCluster>();

    public static void ForceHealthCheckAll()
    {
        if (instance == null) return;

        foreach (var cluster in instance.hiveClusters)
        {
            ClusterManager.PerformHealthCheck(cluster);
        }
    }

    public static (int total, int alive, int dead, int totalHives) GetClusterStatistics()
    {
        if (instance == null) return (0, 0, 0, 0);

        int alive = instance.hiveClusters.Count(c => c.IsAlive);
        int dead = instance.hiveClusters.Count(c => !c.IsAlive);
        int totalHives = instance.hiveClusters.Sum(c => c.CurrentHiveCount);

        return (instance.hiveClusters.Count, alive, dead, totalHives);
    }

    public static void ClearAllClusters()
    {
        if (instance != null)
        {
            instance.hiveClusters.Clear();
        }
    }

    public static void ForceUpdateHiveCounts()
    {
        if (instance == null) return;

        foreach (var cluster in instance.hiveClusters)
        {
            ClusterManager.UpdateExistingHives(cluster);
        }
    }

    public static void ForceRegenerateAllClusters()
    {
        if (instance == null) return;

        instance.hiveClusters.Clear();
        instance.hiveClusters = ClusterManager.CreateInitialClusters();
        ClusterManager.SaveClusterCenters(instance.hiveClusters);
    }
}
