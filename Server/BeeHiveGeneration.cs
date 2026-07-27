using Beekeeping.Server;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.World.Blocks;
using Eco.Simulation;
using Eco.Simulation.Time;
using System;
using System.Linq;
using System.Collections.Generic;
using Eco.Shared.Utils;
using Eco.Shared.Serialization;
using Beekeeping.Server.Module;
using Eco.Core.Plugins;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System.IO;

public class BeeHiveGeneration : IModKitPlugin, IInitializablePlugin
{
    public const int CLUSTER_RADIUS = 60;
    public const int MIN_CLUSTER_SPACING = 50;
    private const int SPAWN_RADIUS = 40;
    public const float CLUSTER_DEATH_THRESHOLD = 0.08f;
    private const float MIN_REGEN_HOURS = 1.0f;
    private const float MAX_REGEN_HOURS = 20.0f;
    private const int MAX_HIVES_PER_CLUSTER = 5;

    // Debug MODE enable debug messages in chat and faster spawn
    public const bool DEBUG_MODE = false;

    private List<HiveCluster> hiveClusters = new List<HiveCluster>();
    private static BeeHiveGeneration instance;
    private static Random random = new Random();

    public string GetCategory() => "Beekeeping Cluster System";
    public string GetStatus() => $"Managing {hiveClusters.Count} hive clusters";

	public void Initialize(TimedTask timer)
	{
		instance = this;

		if (DEBUG_MODE)
		{
			try
			{
				var allUsers = Eco.Gameplay.Players.UserManager.Users;
				foreach (var user in allUsers)
				{
					if (DEBUG_MODE && user.IsAdmin && user.Player != null)
					{
						user.Player.MsgLocStr($"[BEEKEEPING] === BEEHIVE SYSTEM INITIALIZATION ===");
					}
				}
			}
			catch { }
		}

		// Load cluster centers from file
		var savedCenters = ClusterManager.LoadClusterCenters();

		if (DEBUG_MODE)
		{
			try
			{
				var allUsers = Eco.Gameplay.Players.UserManager.Users;
				foreach (var user in allUsers)
				{
					if (user.IsAdmin && user.Player != null)
					{
						user.Player.MsgLocStr($"[BEEKEEPING] Loaded {savedCenters.Count} cluster centers from saved file");
					}
				}
			}
			catch { }
		}

		// Check if wild hives exist in world
		var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
		var existingWildHives = worldObjectManager.All.Count(obj => 
			obj.GetType() == typeof(OccupiedSauvageBeehiveObject) || 
			obj.GetType() == typeof(VacantSauvageBeehiveObject));

		if (DEBUG_MODE)
		{
			try
			{
				var allUsers = Eco.Gameplay.Players.UserManager.Users;
				foreach (var user in allUsers)
				{
					if (user.IsAdmin && user.Player != null)
					{
						user.Player.MsgLocStr($"[BEEKEEPING] Found {existingWildHives} existing wild hives in world");
					}
				}
			}
			catch { }
		}

		if (savedCenters.Count > 0)
		{
			if (DEBUG_MODE)
			{
				try
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] Rebuilding clusters from saved centers...");
						}
					}
				}
				catch { }
			}

			// Rebuild clusters from saved centers
			hiveClusters = ClusterManager.RebuildClustersFromCenters(savedCenters);

			if (DEBUG_MODE)
			{
				try
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] Rebuilt {hiveClusters.Count} clusters");
						}
					}
				}
				catch { }
			}

			// Check: If save file exists but no wild hives found, do a reset
			if (existingWildHives == 0)
			{
				if (DEBUG_MODE)
				{
					try
					{
						var allUsers = Eco.Gameplay.Players.UserManager.Users;
						foreach (var user in allUsers)
						{
							if (user.IsAdmin && user.Player != null)
							{
								user.Player.MsgLocStr($"[BEEKEEPING] Save file exists but no hives found - performing reset...");
							}
						}
					}
					catch { }
				}
				
				// Clear old clusters and create fresh ones
				hiveClusters.Clear();
				hiveClusters = ClusterManager.CreateInitialClusters();
				ClusterManager.SaveClusterCenters(hiveClusters);
				
				if (DEBUG_MODE)
				{
					try
					{
						var allUsers = Eco.Gameplay.Players.UserManager.Users;
						foreach (var user in allUsers)
						{
							if (user.IsAdmin && user.Player != null)
							{
								user.Player.MsgLocStr($"[BEEKEEPING] Created {hiveClusters.Count} new clusters");
							}
						}
					}
					catch { }
				}
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
			if (DEBUG_MODE)
			{
				try
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] No save file found - performing full reset...");
						}
					}
				}
				catch { }
			}
			
			// Remove all existing wild hives
			var allWildHives = worldObjectManager.All
				.Where(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject) || 
							 obj.GetType() == typeof(VacantSauvageBeehiveObject))
				.ToList();
			
			foreach (var hive in allWildHives)
			{
				hive.Destroy();
			}
			
			if (DEBUG_MODE)
			{
				try
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] Removed {allWildHives.Count} wild hives");
						}
					}
				}
				catch { }
			}
			
			// Create fresh clusters
			hiveClusters = ClusterManager.CreateInitialClusters();
			ClusterManager.SaveClusterCenters(hiveClusters);
			
			if (DEBUG_MODE)
			{
				try
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] Created {hiveClusters.Count} new clusters");
						}
					}
				}
				catch { }
			}
		}

		// Simple health check timer (no AddRepeating needed for async spawns)
		if (DEBUG_MODE)
		{
			try
			{
				var allUsers = Eco.Gameplay.Players.UserManager.Users;
				foreach (var user in allUsers)
				{
					if (user.IsAdmin && user.Player != null)
					{
						user.Player.MsgLocStr($"[BEEKEEPING] === INITIALIZATION COMPLETE ===");
						user.Player.MsgLocStr($"[BEEKEEPING] System: Async regeneration with direct spawning");
						user.Player.MsgLocStr($"[BEEKEEPING] Active clusters: {hiveClusters.Count}");
						user.Player.MsgLocStr($"[BEEKEEPING] Debug mode: {DEBUG_MODE}");
					}
				}
			}
			catch { }
		}
	}

    private void CheckAndRegenerateEmptyClusters()
    {
        if (DEBUG_MODE) return; // Skip in debug mode

        foreach (var cluster in hiveClusters)
        {
            // Update hive counts first
            ClusterManager.UpdateExistingHives(cluster);

            // Check if cluster has 0 hives and is alive
            if (cluster.CurrentHiveCount == 0 && cluster.IsAlive)
            {
                // Check plant health for regeneration eligibility
                var plantPercentage = cluster.MaxPlantCount > 0 ? (float)cluster.CurrentPlantCount / cluster.MaxPlantCount : 0f;
                if (plantPercentage < 0.17f)
                    continue; // Skip dead clusters

                // Calculate available slots (should be 5 since cluster is empty)
                int availableSlots = MAX_HIVES_PER_CLUSTER;

                // Randomly choose how many hives to spawn (1-5)
                int toSpawn = random.Next(1, Math.Min(5, availableSlots) + 1);

                // Schedule spawns with production delays (1-20 hours)
                for (int i = 0; i < toSpawn; i++)
                {
                    var regenDelay = MIN_REGEN_HOURS + (random.NextDouble() * (MAX_REGEN_HOURS - MIN_REGEN_HOURS));
                    var delaySeconds = regenDelay * 3600.0; // Convert hours to seconds
                    ScheduleSpawn(delaySeconds, cluster, new List<Vector3i>(cluster.ExistingHivePositions));
                }

                // Update last harvest time to prevent immediate re-triggering
                cluster.LastHarvestTime = WorldTime.Seconds;
            }
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
            {
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] ✗ SPAWN CANCELLED: Target cluster is null or dead");
                        }
                    }
                }
                catch { }
                return;
            }

            var hivePos = instance.FindSuitableHivePosition(targetCluster.CenterPosition, existingHives);
            if (hivePos.HasValue)
            {
                var hiveType = random.NextDouble() < 0.7 ? "OccupiedSauvageBeehiveObject" : "VacantSauvageBeehiveObject";

                // Use proper spawning method
                var hiveTypeClass = hiveType == "OccupiedSauvageBeehiveObject" ? typeof(OccupiedSauvageBeehiveObject) : typeof(VacantSauvageBeehiveObject);
                var spawnedHive = WorldObjectManager.ForceAdd(hiveTypeClass, null, hivePos.Value, Quaternion.Identity);

                if (spawnedHive != null)
                {
                    targetCluster.ExistingHivePositions.Add(hivePos.Value);
                    targetCluster.RegenerationInProgress = false;

                    // Enhanced spawn notification with cluster info
                    try
                    {
                        var allUsers = Eco.Gameplay.Players.UserManager.Users;
                        foreach (var user in allUsers)
                        {
                            if (user.IsAdmin && user.Player != null)
                            {
                                var clusterIndex = instance.hiveClusters.IndexOf(targetCluster);
                                var distanceFromCenter = Vector3i.Distance(hivePos.Value, targetCluster.CenterPosition);
								if (DEBUG_MODE)
								{
									user.Player.MsgLocStr($"[BEEKEEPING] ✓ HIVE SPAWNED: {hiveType}");
									user.Player.MsgLocStr($"[BEEKEEPING] Location: {hivePos.Value}");
									user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} center: {targetCluster.CenterPosition}");
									user.Player.MsgLocStr($"[BEEKEEPING] Distance from center: {distanceFromCenter} blocks");
									user.Player.MsgLocStr($"[BEEKEEPING] Cluster now has {targetCluster.CurrentHiveCount} hives");
								}
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    targetCluster.RegenerationInProgress = false;
                    try
                    {
                        var allUsers = Eco.Gameplay.Players.UserManager.Users;
                        foreach (var user in allUsers)
                        {
                            if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                            {
                                user.Player.MsgLocStr($"[BEEKEEPING] ✗ SPAWN FAILED: WorldObjectManager.ForceAdd returned null");
                            }
                        }
                    }
                    catch { }
                }
            }
            else
            {
                // Clear flag even if spawn failed (CASCADE PREVENTION)
                targetCluster.RegenerationInProgress = false;

                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (user.IsAdmin && user.Player != null)
                        {
                            var clusterIndex = instance.hiveClusters.IndexOf(targetCluster);
                            user.Player.MsgLocStr($"[BEEKEEPING] ✗ SPAWN FAILED: No suitable position found for cluster #{clusterIndex} near {targetCluster.CenterPosition}");
                        }
                    }
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            // Clear flag on any error (CASCADE PREVENTION)
            try
            {
                if (targetCluster != null)
                    targetCluster.RegenerationInProgress = false;
            }
            catch { }

            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] Spawn error for cluster: {ex.Message}");
                    }
                }
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
            var distance = random.NextDouble() * CLUSTER_RADIUS;

            var x = (int)(clusterCenter.X + System.Math.Cos(angle) * distance);
            var z = (int)(clusterCenter.Z + System.Math.Sin(angle) * distance);
            var y = clusterCenter.Y;

            for (int checkY = y + 10; checkY >= y - 10; checkY--)
            {
                var groundBlock = Eco.World.World.GetBlock(new Vector3i(x, checkY, z));
                if (groundBlock is DirtBlock && !(groundBlock is DesertSandBlock))
                {
                    var hivePos = new Vector3i(x, checkY + 1, z);

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
                    break;
                }
            }
        }

        return null;
    }

    // Single method to handle all hive destruction/harvest events
    public static void OnHiveDestroyed(Vector3i hivePosition)
    {
        if (instance == null)
        {
            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] System error: Instance is NULL at {hivePosition}");
                    }
                }
            }
            catch { }
            return;
        }

        try
        {
            // Notify of hive destruction
            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] Hive destroyed at {hivePosition}");
                    }
                }
            }
            catch { }

            var cluster = ClusterManager.FindClusterContaining(hivePosition, instance.hiveClusters);

            if (cluster == null)
            {
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Warning: No cluster found for hive at {hivePosition}");
                        }
                    }
                }
                catch { }
                return;
            }

            if (!cluster.IsAlive)
            {
                cluster.ExistingHivePositions.Remove(hivePosition);
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Cluster at {cluster.CenterPosition} is DEAD - no regeneration possible");
                        }
                    }
                }
                catch { }
                return;
            }

            var currentTime = WorldTime.Seconds;
            var clusterIndex = instance.hiveClusters.IndexOf(cluster);
            var distanceToCenter = Vector3i.Distance(hivePosition, cluster.CenterPosition);

            // Detailed cluster information
            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] === CLUSTER ANALYSIS ===");
                        user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} center: {cluster.CenterPosition}");
                        user.Player.MsgLocStr($"[BEEKEEPING] Distance from center: {distanceToCenter} blocks");
                        user.Player.MsgLocStr($"[BEEKEEPING] Hives before removal: {cluster.CurrentHiveCount}");
                    }
                }
            }
            catch { }

            // Force update existing hives to ensure accurate count before removal
            ClusterManager.UpdateExistingHives(cluster);

            // Count hives before removal
            int hivesBeforeRemoval = cluster.CurrentHiveCount;

            // Try to remove the destroyed hive from tracking
            bool wasRemoved = cluster.ExistingHivePositions.Remove(hivePosition);

            // If removal failed due to coordinate mismatch, just decrement the count by removing any hive
            if (!wasRemoved && cluster.ExistingHivePositions.Count > 0)
            {
                cluster.ExistingHivePositions.RemoveAt(cluster.ExistingHivePositions.Count - 1);
                wasRemoved = true;
            }

            // Count hives after removal
            int hivesAfterRemoval = cluster.CurrentHiveCount;

            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] Hives after removal: {hivesAfterRemoval}");
                        if (!wasRemoved)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Warning: Failed to remove hive from tracking");
                        }
                    }
                }
            }
            catch { }

            // Check if cluster needs regeneration - REGENERATE ONLY if ≤2 hives remaining
            if (hivesAfterRemoval > 2)
            {
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} is healthy ({hivesAfterRemoval} hives) - no regeneration needed");
                        }
                    }
                }
                catch { }
                return;
            }

            // Check plant health for regeneration eligibility
            var plantPercentage = cluster.MaxPlantCount > 0 ? (float)cluster.CurrentPlantCount / cluster.MaxPlantCount : 0f;
            if (plantPercentage < 0.17f)
            {
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} plant health too low ({plantPercentage:P1}) - regeneration blocked");
                        }
                    }
                }
                catch { }
                return;
            }

            // Check if regeneration is already in progress (CASCADE PREVENTION)
            if (cluster.RegenerationInProgress)
            {
                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} regeneration already in progress - skipping");
                        }
                    }
                }
                catch { }
                return;
            }

            // Set regeneration flag BEFORE spawning (CASCADE PREVENTION)
            cluster.RegenerationInProgress = true;

            // REGENERATION APPROVED
            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] === REGENERATION APPROVED ===");
                        user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} at {cluster.CenterPosition}");
                        user.Player.MsgLocStr($"[BEEKEEPING] Condition: {hivesAfterRemoval} hives ≤ 2");
                        user.Player.MsgLocStr($"[BEEKEEPING] Plant health: {plantPercentage:P1} ≥ 17%");
                    }
                }
            }
            catch { }

            // Calculate available slots
            int availableSlots = MAX_HIVES_PER_CLUSTER - cluster.CurrentHiveCount;

            if (availableSlots > 0)
            {
                // Randomly choose how many hives to spawn
                int maxToSpawn = Math.Min(5, availableSlots);
                int toSpawn = random.Next(1, maxToSpawn + 1);

                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Scheduling {toSpawn} hives for cluster #{clusterIndex}");
                            user.Player.MsgLocStr($"[BEEKEEPING] Available slots: {availableSlots}, Max possible: {maxToSpawn}");
                        }
                    }
                }
                catch { }

                // Schedule spawns with async delays
                for (int i = 0; i < toSpawn; i++)
                {
                    var delay = DEBUG_MODE ? 60.0 + (i * 5.0) : (MIN_REGEN_HOURS + (random.NextDouble() * (MAX_REGEN_HOURS - MIN_REGEN_HOURS))) * 3600.0;
                    ScheduleSpawn(delay, cluster, new List<Vector3i>(cluster.ExistingHivePositions));

                    try
                    {
                        var allUsers = Eco.Gameplay.Players.UserManager.Users;
                        foreach (var user in allUsers)
                        {
                            if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                            {
                                var timeText = DEBUG_MODE ? $"{delay:F0} seconds" : $"{delay / 3600.0:F1} hours";
                                user.Player.MsgLocStr($"[BEEKEEPING] Hive #{i + 1} scheduled for cluster #{clusterIndex} in {timeText}");
                            }
                        }
                    }
                    catch { }
                }

                // Update last harvest time
                cluster.LastHarvestTime = currentTime;

                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] === REGENERATION SCHEDULED ===");
                        }
                    }
                }
                catch { }
            }
            else
            {
                // Clear flag if no slots available
                cluster.RegenerationInProgress = false;

                try
                {
                    var allUsers = Eco.Gameplay.Players.UserManager.Users;
                    foreach (var user in allUsers)
                    {
                        if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                        {
                            user.Player.MsgLocStr($"[BEEKEEPING] Cluster #{clusterIndex} at maximum capacity - no regeneration needed");
                        }
                    }
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            try
            {
                var allUsers = Eco.Gameplay.Players.UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (DEBUG_MODE && user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[BEEKEEPING] System error: {ex.Message}");
                    }
                }
            }
            catch { }
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