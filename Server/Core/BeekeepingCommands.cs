using Eco.Gameplay;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.World.Blocks;
using Eco.Gameplay.Objects;
using Eco.Shared.IoC;
using System.Linq;
using System;
using Eco.Simulation.Time;

namespace Beekeeping.Server
{
    [ChatCommandHandler]
    public class BeekeepingCommands
    {
        [ChatSubCommand("Util", "Give 1 of each Beekeeping mod item to your inventory.", "BeekeepingItems", ChatAuthorizationLevel.Admin)]
        public static void GiveAllItems(IChatClient chatClient)
        {
            var user = chatClient as User;
            if (user == null) { chatClient.MsgLoc($"Must be used by a player."); return; }

            var items = new Type[]
            {
                // Beekeeping resources
                typeof(HoneyItem), typeof(BeewaxItem), typeof(PureWaxItem),
                typeof(RoyalJellyItem), typeof(PropolisItem),
                typeof(BeeColonyCoreItem), typeof(FrameItem), typeof(WaxFrameItem),
                // Wild hive items
                typeof(OccupiedSauvageBeehiveItem), typeof(VacantSauvageBeehiveItem),
                // Crafting station & tool
                typeof(HoneyExtractMechanicalItem), typeof(EmptyPotItem),
                // Player hives
                typeof(BeeHiveItem), typeof(SmallBeeHiveItem),
                // Decorative
                typeof(BeeStatueItem), typeof(DecorativeHoneyPotItem),
                typeof(StoneVesselItem),
                // Honeycomb shelves (light)
                typeof(HoneycombShelfItem), typeof(HoneycombShelf1Item),
                typeof(HoneycombShelf2LeftItem), typeof(HoneycombShelf2RightItem),
                typeof(HoneycombShelf3LeftItem),
                // Honeycomb shelves (dark)
                typeof(HoneycombShelfDarkItem), typeof(HoneycombShelf1DarkItem),
                typeof(HoneycombShelf2LeftDarkItem), typeof(HoneycombShelf2RightDarkItem),
                typeof(HoneycombShelf3LeftDarkItem),
            };

            var label = Localizer.Do($"Beekeeping mod items");
            foreach (var type in items)
                InventoryUtils.AddItemsWithVoidStorageFallback(label, user, Item.Get(type), 1);

            chatClient.MsgLoc($"Gave {items.Length} Beekeeping items.");
        }

        [ChatSubCommand("Util", "Shows all available beekeeping commands and their descriptions.", "BeekeepingHelp", ChatAuthorizationLevel.Admin)]
        public static void BeekeepingHelp(IChatClient chatClient)
        {
            chatClient.MsgLoc($"=== BEEKEEPING CLUSTER SYSTEM COMMANDS ===");
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"/ClusterStatus - Real-time ecosystem overview with health stats");
            chatClient.MsgLoc($"/ResetCluster confirm - DESTROY all wild hives and regenerate ecosystem");
            chatClient.MsgLoc($"/TestRegeneration - Test the regeneration system by simulating harvest");
            chatClient.MsgLoc($"/BeekeepingItems - Give 1 of each mod item to your inventory");
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"Use /ClusterStatus after harvesting hives to see real-time updates.");
        }

        [ChatSubCommand("Util", "Shows real-time comprehensive information about the hive cluster ecosystem.", "ClusterStatus", ChatAuthorizationLevel.Admin)]
        public static void ClusterStatus(IChatClient chatClient)
        {
			BeeHiveGeneration.ForceHealthCheckAll();
            var clusters = BeeHiveGeneration.GetAllClusters();
            var (actualOccupiedHives, actualVacantHives) = CountWorldHives();
            var totalWildHives = actualOccupiedHives + actualVacantHives;
            var (totalClusters, aliveClusters, deadClusters, clusterTrackedHives) = BeeHiveGeneration.GetClusterStatistics();

            chatClient.MsgLoc($"=== REAL-TIME HIVE CLUSTER ANALYSIS ===");
            chatClient.MsgLoc($"");
            
            DisplayHiveCounts(chatClient, actualOccupiedHives, actualVacantHives, totalWildHives);
            DisplayClusterSummary(chatClient, totalClusters, aliveClusters, deadClusters, clusterTrackedHives);
            DisplayTrackingAccuracy(chatClient, clusterTrackedHives, totalWildHives);
            DisplayHiveDistribution(chatClient, clusters);
            DisplayEcosystemHealth(chatClient, clusters);
        }

        private static (int occupied, int vacant) CountWorldHives()
        {
            var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
            var occupied = worldObjectManager.All.Count(obj => obj.GetType().Name == "OccupiedSauvageBeehiveObject");
            var vacant = worldObjectManager.All.Count(obj => obj.GetType().Name == "VacantSauvageBeehiveObject");
            return (occupied, vacant);
        }

        private static void DisplayHiveCounts(IChatClient chatClient, int occupied, int vacant, int total)
        {
            chatClient.MsgLoc($"WORLD HIVE COUNT:");
            chatClient.MsgLoc($"  Occupied wild hives: {occupied}");
            chatClient.MsgLoc($"  Vacant wild hives: {vacant}");
            chatClient.MsgLoc($"  Total wild hives: {total}");
            chatClient.MsgLoc($"");
        }

        private static void DisplayClusterSummary(IChatClient chatClient, int total, int alive, int dead, int tracked)
        {
            chatClient.MsgLoc($"CLUSTER SYSTEM:");
            chatClient.MsgLoc($"  Total clusters: {total}");
            
            if (total > 0)
            {
                chatClient.MsgLoc($"  - Alive clusters: {alive} ({(alive * 100f / total):F1}%)");
                chatClient.MsgLoc($"  - Dead clusters: {dead} ({(dead * 100f / total):F1}%)");
            }
            
            chatClient.MsgLoc($"  Tracked hives: {tracked}");
        }

        private static void DisplayTrackingAccuracy(IChatClient chatClient, int tracked, int total)
        {
            var accuracy = total > 0 ? (tracked * 100f / total) : 0f;
            chatClient.MsgLoc($"  Tracking accuracy: {accuracy:F1}%");
            
            if (accuracy < 90f)
            {
                chatClient.MsgLoc($"");
                chatClient.MsgLoc($"NOTE: Low tracking accuracy suggests some hives are outside cluster management.");
            }
            
            chatClient.MsgLoc($"");
        }

        private static void DisplayHiveDistribution(IChatClient chatClient, System.Collections.Generic.List<HiveCluster> clusters)
        {
            var distribution = new int[6]; // 0-5 hives per cluster
            
            foreach (var cluster in clusters)
            {
                var count = Math.Min(cluster.CurrentHiveCount, 5);
                distribution[count]++;
            }

            chatClient.MsgLoc($"HIVE DISTRIBUTION:");
            for (int i = 0; i <= 5; i++)
            {
                chatClient.MsgLoc($"  {i} hives: {distribution[i]} clusters");
            }
            chatClient.MsgLoc($"");
        }

        private static void DisplayEcosystemHealth(IChatClient chatClient, System.Collections.Generic.List<HiveCluster> clusters)
        {
            int healthy = 0, degraded = 0, dying = 0;

            foreach (var cluster in clusters)
            {
                var plantPercentage = cluster.MaxPlantCount > 0 ? 
                    (float)cluster.CurrentPlantCount / cluster.MaxPlantCount : 0f;
                    
                if (plantPercentage >= 0.6f) 
                    healthy++;
                else if (plantPercentage >= 0.2f) 
                    degraded++;
                else 
                    dying++;
            }

            chatClient.MsgLoc($"ECOSYSTEM HEALTH:");
            chatClient.MsgLoc($"  Healthy clusters (≥60% plants): {healthy}");
            chatClient.MsgLoc($"  Degraded clusters (20-60% plants): {degraded}");
            chatClient.MsgLoc($"  Dying clusters (<20% plants): {dying}");

            var totalClusters = clusters.Count;
            var overallHealth = totalClusters > 0 ? (healthy * 100f) / totalClusters : 0f;
            var healthStatus = GetHealthStatus(overallHealth);
            
            chatClient.MsgLoc($"  Overall health: {healthStatus} ({overallHealth:F1}% healthy)");
        }

        private static string GetHealthStatus(float healthPercentage)
        {
            if (healthPercentage >= 70) return "Excellent";
            if (healthPercentage >= 50) return "Good";
            if (healthPercentage >= 30) return "Fair";
            return "Poor";
        }

        [ChatSubCommand("Util", "DESTROYS all wild hives and completely regenerates the cluster ecosystem.", "ResetCluster", ChatAuthorizationLevel.Admin)]
        public static void ResetCluster(IChatClient chatClient, string confirm = "")
        {
            if (confirm != "confirm")
            {
                chatClient.MsgLoc($"WARNING: This will destroy ALL wild hives and regenerate the ecosystem from scratch.");
                chatClient.MsgLoc($"Type /ResetCluster confirm to proceed.");
                return;
            }
            chatClient.MsgLoc($"=== RESETTING ENTIRE CLUSTER ECOSYSTEM ===");
            chatClient.MsgLoc($"WARNING: This will destroy ALL wild hives and regenerate everything!");
            
            var (removedCount, protectedCount) = RemoveAllWildHives(chatClient);
            
            chatClient.MsgLoc($"Removed {removedCount} wild hives");
            if (protectedCount > 0)
            {
                chatClient.MsgLoc($"Could not remove {protectedCount} hives (may be in use)");
            }

            RegenerateClusterSystem(chatClient);
            DisplayResetResults(chatClient);
        }

        private static (int removed, int protectedCount) RemoveAllWildHives(IChatClient chatClient)
        {
            chatClient.MsgLoc($"Step 1: Removing all wild hives...");
            
            var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
            var allWildHives = worldObjectManager.All
                .Where(obj => obj.GetType().Name == "OccupiedSauvageBeehiveObject" || 
                             obj.GetType().Name == "VacantSauvageBeehiveObject")
                .ToList();

            int removed = 0, protectedCount = 0;

            foreach (var hive in allWildHives)
            {
                try
                {
                    hive.Destroy();
                    removed++;
                }
                catch
                {
                    protectedCount++;
                }
            }

            return (removed, protectedCount);
        }

        private static void RegenerateClusterSystem(IChatClient chatClient)
        {
            chatClient.MsgLoc($"Step 2: Clearing cluster data...");
            BeeHiveGeneration.ClearAllClusters();

            chatClient.MsgLoc($"Step 3: Regenerating cluster ecosystem...");
            
            try
            {
                BeeHiveGeneration.ForceRegenerateAllClusters();
                chatClient.MsgLoc($"Cluster ecosystem regenerated successfully!");
            }
            catch (Exception ex)
            {
                chatClient.MsgLoc($"Error during regeneration: {ex.Message}");
                chatClient.MsgLoc($"Try restarting the server to complete the reset.");
                throw;
            }
        }

        private static void DisplayResetResults(IChatClient chatClient)
        {
            var (totalClusters, aliveClusters, deadClusters, totalHives) = BeeHiveGeneration.GetClusterStatistics();

            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"=== CLUSTER ECOSYSTEM RESET COMPLETE ===");
            chatClient.MsgLoc($"Current clusters: {totalClusters}");
            chatClient.MsgLoc($"Current hives: {totalHives}");
            chatClient.MsgLoc($"Ecosystem health: {aliveClusters} alive, {deadClusters} dead clusters");
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"Use /ClusterStatus to view the ecosystem!");
        }

        [ChatSubCommand("Util", "Tests the regeneration system by simulating hive harvest.", "TestRegeneration", ChatAuthorizationLevel.Admin)]
        public static void TestRegeneration(IChatClient chatClient)
        {
            var clusters = BeeHiveGeneration.GetAllClusters();
            
            chatClient.MsgLoc($"=== REGENERATION SYSTEM DIAGNOSTICS ===");
            chatClient.MsgLoc($"Total clusters in system: {clusters.Count}");
            
            if (!ValidateTestEnvironment(chatClient, clusters))
                return;

            var testCluster = FindTestCluster(clusters);
            if (testCluster == null)
            {
                ReportTestClusterNotFound(chatClient, clusters);
                return;
            }

            var clusterIndex = clusters.IndexOf(testCluster);
            var hivePos = testCluster.ExistingHivePositions[0];
            
            DisplayPreHarvestStatus(chatClient, testCluster, clusterIndex, hivePos);
            
            chatClient.MsgLoc($"Simulating harvest of hive at {hivePos}...");
            BeeHiveGeneration.OnHiveDestroyed(hivePos);
            
            DisplayPostHarvestResults(chatClient, testCluster);
        }

        private static bool ValidateTestEnvironment(IChatClient chatClient, System.Collections.Generic.List<HiveCluster> clusters)
        {
            if (clusters.Count == 0)
            {
                chatClient.MsgLoc($"ERROR: No clusters found! Use /ResetCluster first.");
                return false;
            }
            return true;
        }

        private static HiveCluster FindTestCluster(System.Collections.Generic.List<HiveCluster> clusters)
        {
            return clusters.FirstOrDefault(c => c.IsAlive && c.CurrentHiveCount > 0);
        }

        private static void ReportTestClusterNotFound(IChatClient chatClient, System.Collections.Generic.List<HiveCluster> clusters)
        {
            chatClient.MsgLoc($"No suitable clusters found for testing.");
            chatClient.MsgLoc($"Looking for clusters with issues:");
            
            var deadClusters = clusters.Count(c => !c.IsAlive);
            var emptyClusters = clusters.Count(c => c.CurrentHiveCount == 0);
            
            chatClient.MsgLoc($"  Dead clusters: {deadClusters}");
            chatClient.MsgLoc($"  Empty clusters: {emptyClusters}");
            chatClient.MsgLoc($"Use /ResetCluster to create test environment.");
        }

        private static void DisplayPreHarvestStatus(IChatClient chatClient, HiveCluster cluster, int clusterIndex, Vector3i hivePos)
        {
            chatClient.MsgLoc($"Testing with cluster {clusterIndex} at {cluster.CenterPosition}");
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"BEFORE HARVEST:");
            chatClient.MsgLoc($"  Current hives: {cluster.CurrentHiveCount}");
            chatClient.MsgLoc($"  Plant health: {cluster.CurrentPlantCount}/{cluster.MaxPlantCount} ({GetPlantHealthPercentage(cluster):F1}%)");
            chatClient.MsgLoc($"  Last harvest: {GetHoursSinceLastHarvest(cluster):F1} hours ago");
            chatClient.MsgLoc($"  Cluster alive: {cluster.IsAlive}");
            
            var cooldownRemaining = GetCooldownRemaining(cluster);
            if (cooldownRemaining > 0)
            {
                chatClient.MsgLoc($"  COOLDOWN: {cooldownRemaining:F1} hours remaining");
            }
            chatClient.MsgLoc($"");
        }

        private static void DisplayPostHarvestResults(IChatClient chatClient, HiveCluster cluster)
        {
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"AFTER HARVEST:");
            chatClient.MsgLoc($"  Current hives: {cluster.CurrentHiveCount}");
            chatClient.MsgLoc($"  Last harvest: {GetHoursSinceLastHarvest(cluster):F1} hours ago");
            
            chatClient.MsgLoc($"");
            if (cluster.CurrentHiveCount <= 2 && cluster.IsAlive)
            {
                chatClient.MsgLoc($"SUCCESS: Regeneration should be triggered for low hive count");
                chatClient.MsgLoc($"Hives will spawn automatically using async delays:");
                chatClient.MsgLoc($"  Debug mode: 60 seconds + 5 second stagger");
                chatClient.MsgLoc($"  Production mode: 1-20 hours");
            }
            else if (!cluster.IsAlive)
            {
                chatClient.MsgLoc($"FAILED: Cluster is DEAD (insufficient plants)");
            }
            else
            {
                chatClient.MsgLoc($"No regeneration needed - cluster has sufficient hives ({cluster.CurrentHiveCount}/5)");
            }
            
            chatClient.MsgLoc($"");
            chatClient.MsgLoc($"Use /ClusterStatus to see overall system status.");
        }

        private static float GetPlantHealthPercentage(HiveCluster cluster)
        {
            return cluster.MaxPlantCount > 0 ? 
                (cluster.CurrentPlantCount * 100f / cluster.MaxPlantCount) : 0f;
        }

        private static double GetHoursSinceLastHarvest(HiveCluster cluster)
        {
            return (WorldTime.Seconds - cluster.LastHarvestTime) / 3600.0;
        }

        private static double GetCooldownRemaining(HiveCluster cluster)
        {
            return Math.Max(0, 1.0 - GetHoursSinceLastHarvest(cluster)); // 1 hour cooldown
        }
    }
}