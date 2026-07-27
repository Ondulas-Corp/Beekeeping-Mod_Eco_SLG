using Eco.Shared.Math;
using Eco.World.Blocks;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Simulation;
using Eco.Simulation.Time;
using Eco.Gameplay.Players;
using Eco.Shared.IoC;
using Eco.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Eco.Core.Controller;

namespace Beekeeping.Server
{
    public static class ClusterManager
    {
        private static Random random = new Random();

        // Save file methods
        private static string GetSourceFileDirectory([System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
        }

		private static string ClusterSaveFile => Directory.GetCurrentDirectory() + "/Storage/cluster_centers.txt";

        public static List<Vector3i> LoadClusterCenters()
        {
            var centers = new List<Vector3i>();
            try
            {
                if (File.Exists(ClusterSaveFile))
                {
                    var lines = File.ReadAllLines(ClusterSaveFile);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            centers.Add(new Vector3i(
                                int.Parse(parts[0]), 
                                int.Parse(parts[1]), 
                                int.Parse(parts[2])
                            ));
                        }
                    }
                }
            }
            catch (Exception)
            {
                // If loading fails, return empty list
            }
            return centers;
        }

        public static void SaveClusterCenters(List<HiveCluster> clusters)
        {
            try
            {
                var directory = Path.GetDirectoryName(ClusterSaveFile);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                var lines = clusters.Select(c => $"{c.CenterPosition.X},{c.CenterPosition.Y},{c.CenterPosition.Z}");
                File.WriteAllLines(ClusterSaveFile, lines);
            }
            catch (Exception)
            {
                // Silent failure
            }
        }

        public static List<HiveCluster> RebuildClustersFromCenters(List<Vector3i> centers)
        {
            var newClusters = new List<HiveCluster>();
            var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
            
            // Get ALL wild hives in the world first
            var allWildHives = worldObjectManager.All
                .Where(obj => obj.GetType().Name == "OccupiedSauvageBeehiveObject" || 
                             obj.GetType().Name == "VacantSauvageBeehiveObject")
                .ToList();
            
            // Create all clusters first
            foreach (var center in centers)
            {
                var plantCount = CountPlantsInRadius(new Eco.Shared.Math.WorldPosition3i(center.X, center.Y, center.Z), BeeHiveGeneration.CLUSTER_RADIUS);
                
                var cluster = new HiveCluster
                {
                    CenterPosition = center,
                    MaxPlantCount = Math.Max(plantCount, 50),
                    CurrentPlantCount = plantCount,
                    IsAlive = plantCount >= 10,
                    LastHealthCheck = WorldTime.Seconds,
                    LastRegenerationCheck = WorldTime.Seconds,
                    LastHarvestTime = 0
                };
                
                newClusters.Add(cluster);
            }
            
            // Now assign each hive to its CLOSEST cluster center only
            foreach (var hive in allWildHives)
            {
                var hivePos = hive.Position3i;
                
                // Find the closest cluster center
                HiveCluster closestCluster = null;
                double minDistance = double.MaxValue;
                
                foreach (var cluster in newClusters)
                {
                    var distance = Vector3i.Distance(hivePos, cluster.CenterPosition);
                    if (distance <= BeeHiveGeneration.CLUSTER_RADIUS && distance < minDistance)
                    {
                        minDistance = distance;
                        closestCluster = cluster;
                    }
                }
                
                // Assign hive to closest cluster only
                if (closestCluster != null)
                {
                    closestCluster.ExistingHivePositions.Add(hivePos);
                }
            }
            
            return newClusters;
        }

        public static List<HiveCluster> CreateInitialClusters()
        {
            var clusters = new List<HiveCluster>();
            
            int targetClusters = CalculateTargetClusterCount();
            int maxAttempts = targetClusters * 10;
            int attempts = 0;

            while (attempts < maxAttempts && clusters.Count < targetClusters)
            {
                attempts++;

                try
                {
                    var landPos = Eco.World.World.GetRandomLandPos();
                    
                    if (IsValidClusterLocation(landPos, clusters))
                    {
                        var cluster = CreateCluster(landPos);
                        if (cluster != null)
                        {
                            clusters.Add(cluster);
                            SpawnInitialHivesInCluster(cluster);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }

            return clusters;
        }

        private static int CalculateTargetClusterCount()
        {
            int maxDistance = 0;
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    var pos = Eco.World.World.GetRandomLandPos();
                    var distance = Math.Max(Math.Abs(pos.X), Math.Abs(pos.Z));
                    if (distance > maxDistance)
                        maxDistance = distance;
                }
                catch { continue; }
            }

            int worldRadius = maxDistance;
            
            if (worldRadius < 200)
                return random.Next(30, 41);
            else if (worldRadius < 500)
                return random.Next(60, 81);
            else if (worldRadius < 1000)
                return random.Next(100, 151);
            else
                return random.Next(150, 201);
        }

        private static bool IsValidClusterLocation(Eco.Shared.Math.WorldPosition3i position, List<HiveCluster> existingClusters)
        {
            var groundBlock = Eco.World.World.GetBlock(new Vector3i(position.X, position.Y, position.Z));
            if (!(groundBlock is DirtBlock) || groundBlock is DesertSandBlock)
                return false;

            if (IsInSnowBiome(position))
                return false;

            var plot = PropertyManager.GetPlotFromWorldPos(new Eco.Shared.Math.Vector2i(position.X, position.Z));
            if (plot?.Owners != null)
                return false;

            foreach (var cluster in existingClusters)
            {
                var distance = Vector3i.Distance(new Vector3i(position.X, position.Y, position.Z), cluster.CenterPosition);
                if (distance < BeeHiveGeneration.MIN_CLUSTER_SPACING)
                    return false;
            }

            return true;
        }

        private static bool IsInSnowBiome(Eco.Shared.Math.WorldPosition3i position)
        {
            for (int x = -3; x <= 3; x++)
            {
                for (int z = -3; z <= 3; z++)
                {
                    var checkPos = new Vector3i(position.X + x, position.Y, position.Z + z);
                    var block = Eco.World.World.GetBlock(checkPos);
                    
                    if (block != null)
                    {
                        var blockType = block.GetType().Name.ToLower();
                        if (blockType.Contains("snow") || blockType.Contains("ice"))
                            return true;
                    }
                }
            }
            return false;
        }

        private static HiveCluster CreateCluster(Eco.Shared.Math.WorldPosition3i centerPos)
        {
            var plantCount = CountPlantsInRadius(centerPos, BeeHiveGeneration.CLUSTER_RADIUS);
            
            if (plantCount < 5)
                return null;

            var cluster = new HiveCluster
            {
                CenterPosition = new Vector3i(centerPos.X, centerPos.Y, centerPos.Z),
                MaxPlantCount = plantCount,
                CurrentPlantCount = plantCount,
                IsAlive = true,
                LastHealthCheck = WorldTime.Seconds
            };

            return cluster;
        }

        public static int CountPlantsInRadius(Eco.Shared.Math.WorldPosition3i center, int radius)
        {
            int plantCount = 0;
            
            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    var distance = Math.Sqrt(x * x + z * z);
                    if (distance > radius) continue;

                    var checkPos = new Vector3i(center.X + x, center.Y, center.Z + z);
                    var plant = EcoSim.PlantSim.GetPlant(checkPos);
                    
                    if (plant != null)
                        plantCount++;
                }
            }
            
            return plantCount;
        }

		private static void SpawnInitialHivesInCluster(HiveCluster cluster)
		{
			int initialHives = random.Next(1, 6);
			
			for (int i = 0; i < initialHives; i++)
			{
				var hivePos = FindSuitableHivePosition(cluster.CenterPosition, cluster.ExistingHivePositions);
				if (hivePos.HasValue)
				{
					try
					{
						// Spawn swarms
						var hiveTypeClass = random.NextDouble() < 0.7 
							? typeof(OccupiedSauvageBeehiveObject) 
							: typeof(VacantSauvageBeehiveObject);
							
						WorldObjectManager.ForceAdd(hiveTypeClass, null, hivePos.Value, Quaternion.Identity);
						cluster.ExistingHivePositions.Add(hivePos.Value);
					}
					catch
					{
						// Skip if spawn fails
					}
				}
			}
		}

        public static void UpdateExistingHives(HiveCluster cluster)
        {
            var existingHives = new List<Vector3i>();
            
            foreach (var hivePos in cluster.ExistingHivePositions)
            {
                var block = Eco.World.World.GetBlock((Eco.Shared.Math.WrappedWorldPosition3i)hivePos);
                if (block is WorldObjectBlock worldObjBlock)
                {
                    var obj = worldObjBlock.WorldObjectHandle.Object;
                    if (obj != null && (obj.GetType().Name == "OccupiedSauvageBeehiveObject" || obj.GetType().Name == "VacantSauvageBeehiveObject"))
                    {
                        existingHives.Add(hivePos);
                    }
                }
            }
            
            cluster.ExistingHivePositions = existingHives;
        }
		
		private static Vector3i? FindSuitableHivePosition(Vector3i clusterCenter, List<Vector3i> existingHives)
		{
			int attempts = 0;
			int maxAttempts = 50;
			var random = new Random();

			while (attempts < maxAttempts)
			{
				attempts++;
				
				var angle = random.NextDouble() * 2 * Math.PI;
				var distance = random.NextDouble() * BeeHiveGeneration.CLUSTER_RADIUS;
				
				var x = (int)(clusterCenter.X + Math.Cos(angle) * distance);
				var z = (int)(clusterCenter.Z + Math.Sin(angle) * distance);

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

        public static void PerformHealthCheck(HiveCluster cluster)
        {
            cluster.CurrentPlantCount = CountPlantsInRadius(new Eco.Shared.Math.WorldPosition3i(cluster.CenterPosition.X, cluster.CenterPosition.Y, cluster.CenterPosition.Z), BeeHiveGeneration.CLUSTER_RADIUS);
            
            var plantPercentage = (float)cluster.CurrentPlantCount / cluster.MaxPlantCount;
            cluster.IsAlive = plantPercentage >= BeeHiveGeneration.CLUSTER_DEATH_THRESHOLD;
        }

        public static HiveCluster FindClusterContaining(Vector3i position, List<HiveCluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                var distance = Vector3i.Distance(position, cluster.CenterPosition);
                if (distance <= BeeHiveGeneration.CLUSTER_RADIUS)
                    return cluster;
            }
            return null;
        }
    }
}