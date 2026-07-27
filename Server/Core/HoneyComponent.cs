using Eco.Core.Controller;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Plants;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Simulation;
using Eco.World;
using System.Collections.Generic;
using System.Linq;

namespace Beekeeping.Server
{
    [Serialized]
    [NoIcon]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class HoneyComponent : WorldObjectComponent, IChunkSubscriber
    {
        [Serialized] private bool IsFlowered = false;
        [Serialized] private int lastPlantCount = 0;
        [Serialized] private bool needsUpdate = true;

        private StatusElement status;

        // How often (in seconds) to check for plants nearby
        public float UpdateFrequencySec => 30f;

        public float MaxQueuedChunkUpdateTime => 300f;

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange => false;

        public bool IgnorePlantUpdates => false;

        // Component is enabled only when there are sufficient plants
        public override bool Enabled => IsFlowered;

        public override void Initialize()
        {
            base.Initialize();
            var statusComp = Parent.GetComponent<StatusComponent>();
            if (statusComp != null)
                status = statusComp.CreateStatusElement(priority: -60);

            CheckFloweredStatus();
            UpdateStatus();
        }

        public void ChunksChanged()
        {
            needsUpdate = true;
            Parent.SetDirty();
        }

        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            var radius = 10;
            var center = Parent.Position3i;
            var positions = new List<Vector3i>();

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    for (int z = -radius; z <= radius; z++)
                    {
                        var pos = center + new Vector3i(x, y, z);
                        positions.Add(World.ToChunkPosition(pos));
                    }
                }
            }
            return positions.Distinct();
        }

        public override void Tick()
        {
            base.Tick();

            if (needsUpdate)
            {
                CheckFloweredStatus();
                UpdateStatus();
                needsUpdate = false;
                Parent.UpdateEnabledAndOperating();
            }

            // Age the queen only while the hive is actively producing (plants present)
            if (IsFlowered)
                Parent.GetComponent<PartsComponent>()?.ConsumeDurabilityAccumulated(null, 0.1);
        }

        private void CheckFloweredStatus()
        {
            int plantCount = 0;
            int radius = 10;
            var center = Parent.Position3i;

            // Count plants in a circular area around the hive
            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    // Skip positions outside circular radius for better performance
                    if (x * x + z * z > radius * radius)
                        continue;

                    for (int y = -radius; y <= radius; y++)
                    {
                        var pos = center + new Vector3i(x, y, z);
                        var plant = EcoSim.PlantSim.GetPlant(pos);
                        
                        if (plant != null)
                        {
                            plantCount++;
                        }
                    }
                }
            }

            lastPlantCount = plantCount;
            
            // Minimum 100 plants needed for pollen
            IsFlowered = plantCount >= 100;
        }

        private void UpdateStatus()
        {
            if (status == null) return;

            string statusMessage;
            
            if (IsFlowered)
            {
                statusMessage = $"Sufficient pollen sources: {lastPlantCount} plants found (100+ required)";
            }
            else if (lastPlantCount == 0)
            {
                statusMessage = "No plants found nearby! Plant flowers or other vegetation within 10 blocks for honey production.";
            }
            else
            {
                int needed = 100 - lastPlantCount;
                statusMessage = $"Insufficient pollen: {lastPlantCount}/100 plants found. Plant {needed} more flowers or plants within 10 blocks.";
            }

            status.SetStatusMessage(IsFlowered, Localizer.DoStr(statusMessage));
        }
    }
}