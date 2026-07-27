using Eco.Core.Controller;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Eco.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Gameplay.Players;
using Eco.Shared.Utils;
using Eco.Shared.IoC;

namespace Beekeeping.Server
{
    [Serialized]
    [NoIcon]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(HoneyComponent))]
    public class PollinationComponent : WorldObjectComponent
    {
        private const bool DEBUG_MODE = false;
        private const float DEBUG_GROWTH_BOOST_PERCENT = 86300f; // Crops mature in ~100 seconds (5% per tick)
        private const float TICK_INTERVAL_SECONDS = 5f;
        
        [Serialized] private double nextTickTime = 0;
        [Serialized] private int lastCropsAffected = 0;
        [Serialized] [ThreadSafe] private HashSet<Vector3i> trackedCropPositions = new HashSet<Vector3i>();
        
        private StatusElement status;
        private HoneyComponent honeyComponent;

		// Check if the hive has enough plants around to be active
        public override bool Enabled => honeyComponent?.Enabled ?? false;

        public override void Initialize()
        {
            base.Initialize();
            
            var statusComp = Parent.GetComponent<StatusComponent>();
            if (statusComp != null)
                status = statusComp.CreateStatusElement(priority: -50);

            honeyComponent = Parent.GetComponent<HoneyComponent>();
            
            nextTickTime = Eco.Simulation.Time.WorldTime.Seconds + TICK_INTERVAL_SECONDS;
            
            LogDebug($"Pollination Component Initialized at position: {Parent.Position3i}");
            
            UpdateStatus();
        }

        public override void Tick()
        {
            base.Tick();
            
            // Only tick if the hive is flowered (has enough plants)
            if (!Enabled)
            {
                if (lastCropsAffected > 0)
                {
                    lastCropsAffected = 0;
                    trackedCropPositions.Clear();
                    UpdateStatus();
                }
                return;
            }

            // Check if it's time to tick to prevent ticking every seconds
            if (Eco.Simulation.Time.WorldTime.Seconds < nextTickTime)
                return;

            nextTickTime = Eco.Simulation.Time.WorldTime.Seconds + TICK_INTERVAL_SECONDS;
            
			// Add bonus to crops
            PollinateCrops();
			// Update Hive status
            UpdateStatus();
        }

        private void PollinateCrops()
		{
			var center = Parent.Position3i;
			var cropsFound = new List<Plant>();
			var newTrackedPositions = new HashSet<Vector3i>();
			
			LogDebug($"=== Starting pollination scan ===");
			LogDebug($"Hive position: {center}");
			LogDebug($"Scanning radius: {BeeHiveGeneration.Config.PollinationRadius} blocks");

			int totalPlantsFound = 0;
			int cropsIdentified = 0;

			// Search for crops in a circular area
			for (int x = -BeeHiveGeneration.Config.PollinationRadius; x <= BeeHiveGeneration.Config.PollinationRadius; x++)
			{
				for (int z = -BeeHiveGeneration.Config.PollinationRadius; z <= BeeHiveGeneration.Config.PollinationRadius; z++)
				{
					// Check if position is within circular radius
					if (x * x + z * z > BeeHiveGeneration.Config.PollinationRadius * BeeHiveGeneration.Config.PollinationRadius)
						continue;

					for (int y = -BeeHiveGeneration.Config.PollinationRadius; y <= BeeHiveGeneration.Config.PollinationRadius; y++)
					{
						var pos = center + new Vector3i(x, y, z);
						var plant = EcoSim.PlantSim.GetPlant(pos);
						
						if (plant != null)
						{
							totalPlantsFound++;
							
							// Check if the plant is a crop
							if (IsCropPlant(plant))
							{
								cropsIdentified++;
								cropsFound.Add(plant);
								newTrackedPositions.Add(pos);
							}
						}
					}
				}
			}

			LogDebug($"Crops identified: {cropsIdentified}");

			// Apply growth bonus to crops
			int cropsAffected = 0;
			foreach (var crop in cropsFound)
			{
				// Apply the Bonus
				if (ApplyGrowthBonus(crop))
				{
					cropsAffected++;
					LogDebug($"Boosting: {crop.Species.DisplayName.NotTranslated} at {crop.Position.XYZi()} - Growth: {crop.GrowthPercent:P2}");
				}
			}

			lastCropsAffected = cropsAffected;
			trackedCropPositions = newTrackedPositions;
			
		}

        private static Tag cropTag;
        private static Tag CropTag => cropTag ??= TagManager.Tag("Crop");

        private bool IsCropPlant(Plant plant)
        {
            if (plant == null || plant.Dead)
                return false;

            var species = plant.Species;
            if (species == null)
                return false;

            if (!CanPlantSurviveHere(plant))
                return false;

            // A plant is a crop if any item it drops carries the Eco "Crop" tag.
            // This covers all current and future crops without hardcoding names.
            var tag = CropTag;
            return tag != null && species.ResourceList.Any(r => r.ResourceType != null && r.ResourceType.HasTag(tag));
        }

        private bool CanPlantSurviveHere(Plant plant)
        {
            // Simple check: if the plant is growing at all, it can survive
            
            // Only exclude plants that are clearly dead or dying
            if (plant.Dead)
                return false;

            // If plant is blocked from growing, don't boost it
            if (plant.GrowthBlocked)
                return false;

            return true;
        }


		private bool ApplyGrowthBonus(Plant crop)
		{
			// Don't affect already mature plants
			if (crop.Dead || crop.GrowthPercent >= 1.0f)
			{
				return false;
			}

			// Check if THIS hive should handle this crop to prevent hive overlap (closest hive gets priority)
			if (!ShouldThisHiveHandleCrop(crop.Position.XYZi()))
			{
				return false;
			}
			
			// Calculate growth boost per tick
			float growthPerTick;
			// Bonus to apply
			float boostedTicksToMature;
			
			// Get the number of tick to fully mature
			float normalTicksToMature = (24f * 3600f) / TICK_INTERVAL_SECONDS;
			
			if (DEBUG_MODE)
			{
				// Debug multiplier
				boostedTicksToMature = normalTicksToMature / (1f + (DEBUG_GROWTH_BOOST_PERCENT / 100f));
			}
			else
			{
				// Normal: 15% faster grow overall
				boostedTicksToMature = normalTicksToMature / (1f + (BeeHiveGeneration.Config.GrowthBoostPercent / 100f));
				
			}
			
			// Number of Maturity to add each tick
			growthPerTick = 1.0f / boostedTicksToMature;
			
			float oldGrowth = crop.GrowthPercent;
			float newGrowth = Math.Min(oldGrowth + growthPerTick, 1.0f);
			
			crop.GrowthPercent = newGrowth;
			
			LogDebug($"BOOSTED! {crop.Species.DisplayName.NotTranslated} from {oldGrowth:P2} to {newGrowth:P2} (+{growthPerTick:P4})");
			
			// Update crops for visual update
			World.ForceUpdate(crop.Position.XYZi());
			crop.MarkDirty();

			return true;
		}

		private bool ShouldThisHiveHandleCrop(Vector3i cropPosition)
		{
			// First check if THIS hive has valid spacing (NeighborBeeHiveComponent check)
			var neighborComponent = this.Parent.GetComponent<NeighborBeeHiveComponent>();
			if (neighborComponent != null && !neighborComponent.Enabled)
			{
				// This hive is too close to another hive, don't pollinate
				return false;
			}
			
			var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
			var allBeehives = worldObjectManager.All.Where(obj => obj.HasComponent<PollinationComponent>()).ToList();
			
			var myPosition = this.Parent.Position3i;
			float myDistance = Vector3i.Distance(myPosition, cropPosition);
			
			foreach (var otherHive in allBeehives)
			{
				// Skip self
				if (otherHive == this.Parent)
					continue;

				var pollinationComp = otherHive.GetComponent<PollinationComponent>();
				
				// Only consider enabled hives with valid spacing
				if (pollinationComp == null || !pollinationComp.Enabled)
					continue;
					
				// Also check if other hive has valid spacing
				var otherNeighborComp = otherHive.GetComponent<NeighborBeeHiveComponent>();
				if (otherNeighborComp != null && !otherNeighborComp.Enabled)
					continue; // Skip hives with invalid spacing
				
				float otherDistance = Vector3i.Distance(otherHive.Position3i, cropPosition);
				
				// If other hive is closer, it should handle this crop
				if (otherDistance < myDistance)
				{
					return false;
				}
				
				// If equal distance, use position as tiebreaker (deterministic)
				if (otherDistance == myDistance)
				{
					int positionComparison = ComparePositions(otherHive.Position3i, myPosition);
					if (positionComparison < 0)
					{
						return false;
					}
				}
			}
			
			return true;
		}

		private int ComparePositions(Vector3i a, Vector3i b)
		{
			if (a.X != b.X) return a.X.CompareTo(b.X);
			if (a.Y != b.Y) return a.Y.CompareTo(b.Y);
			return a.Z.CompareTo(b.Z);
		}

        private void UpdateStatus()
        {
            if (status == null) return;

            string statusMessage;
            float boostPercent = DEBUG_MODE ? DEBUG_GROWTH_BOOST_PERCENT : BeeHiveGeneration.Config.GrowthBoostPercent;
            
            if (!Enabled)
            {
                statusMessage = "Pollination inactive - hive needs 100+ nearby plants";
            }
            else if (lastCropsAffected > 0)
            {
                statusMessage = $"Pollinating {lastCropsAffected} crops within {BeeHiveGeneration.Config.PollinationRadius} blocks (+{boostPercent:F0}% growth)";

                if (DEBUG_MODE)
                    statusMessage += " [DEBUG MODE]";
            }
            else
            {
                statusMessage = $"Active — no growing crops within {BeeHiveGeneration.Config.PollinationRadius} blocks";
            }

            status.SetStatusMessage(Enabled && lastCropsAffected > 0, Localizer.DoStr(statusMessage));
        }

        private void LogDebug(string message)
        {
            if (!DEBUG_MODE) return;
            
            try
            {
                var allUsers = UserManager.Users;
                foreach (var user in allUsers)
                {
                    if (user.IsAdmin && user.Player != null)
                    {
                        user.Player.MsgLocStr($"[POLLINATION] {message}");
                    }
                }
            }
            catch { }
        }

        public override void Destroy()
        {
            trackedCropPositions.Clear();
            base.Destroy();
        }
    }
}