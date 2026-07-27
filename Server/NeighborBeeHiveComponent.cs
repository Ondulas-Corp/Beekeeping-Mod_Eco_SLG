using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.World;
using Eco.World.Blocks;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    /// <summary>
    /// Unified component that prevents hives from being placed too close to each other.
    /// </summary>
    [Serialized]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class NeighborBeeHiveComponent : WorldObjectComponent, IChunkSubscriber
    {
        // Configuration constants
        private const int MINIMUM_DISTANCE = 3;
        private const int STATUS_PRIORITY = -50;
        private const float UPDATE_FREQUENCY = 5f;
        private const float MAX_QUEUED_UPDATE_TIME = 300f;

        [Serialized]
        private bool hasValidSpacing = true;

        private StatusElement status;

        // IChunkSubscriber properties
        public float UpdateFrequencySec => UPDATE_FREQUENCY;
        public float MaxQueuedChunkUpdateTime => MAX_QUEUED_UPDATE_TIME;
        public double QueuedChunkUpdateTime { get; set; }
        public double LastChunkUpdateTime { get; set; }
        public bool ResetUpdateTimeOnEveryChange => false;
        public bool IgnorePlantUpdates => true;

        // Component is enabled only when spacing is valid
        public override bool Enabled => hasValidSpacing;

        public override void Initialize()
        {
            status = Parent.GetComponent<StatusComponent>().CreateStatusElement(STATUS_PRIORITY);
            CheckNeighborSpacing();
            UpdateStatus();
        }

        public void ChunksChanged()
        {
            bool validSpacingNow = !HasNeighborBeeHiveNearby();

            // Only update if status changed
            if (hasValidSpacing == validSpacingNow)
                return;

            hasValidSpacing = validSpacingNow;
            UpdateStatus();
            Parent.UpdateEnabledAndOperating();
            Parent.SetDirty();
        }

        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            var center = Parent.Position3i;
            var chunks = new HashSet<Vector3i>();

            // Simple box around the hive for chunk subscription
            for (int x = -MINIMUM_DISTANCE; x <= MINIMUM_DISTANCE; x++)
            {
                for (int y = -MINIMUM_DISTANCE; y <= MINIMUM_DISTANCE; y++)
                {
                    for (int z = -MINIMUM_DISTANCE; z <= MINIMUM_DISTANCE; z++)
                    {
                        var pos = center + new Vector3i(x, y, z);
                        chunks.Add(World.ToChunkPosition(pos));
                    }
                }
            }

            return chunks;
        }

        private void CheckNeighborSpacing()
        {
            hasValidSpacing = !HasNeighborBeeHiveNearby();
        }

        /// <summary>
        /// Optimized neighbor detection using simple distance check
        /// </summary>
        private bool HasNeighborBeeHiveNearby()
        {
            var center = Parent.Position3i;

            // Check all positions within the minimum distance
            for (int x = -MINIMUM_DISTANCE; x <= MINIMUM_DISTANCE; x++)
            {
                for (int y = -MINIMUM_DISTANCE; y <= MINIMUM_DISTANCE; y++)
                {
                    for (int z = -MINIMUM_DISTANCE; z <= MINIMUM_DISTANCE; z++)
                    {
                        // Skip checking our own position
                        if (x == 0 && y == 0 && z == 0) 
                            continue;

                        var checkPos = center + new Vector3i(x, y, z);
                        
                        if (IsHiveAtPosition(checkPos))
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if there's a hive (of any compatible type) at the given position
        /// </summary>
        private bool IsHiveAtPosition(Vector3i position)
        {
            if (World.GetBlock((WrappedWorldPosition3i)position) is WorldObjectBlock block)
            {
                var worldObject = block.WorldObjectHandle.Object;
                
                // Check for both types of hives and ensure it's not ourselves
                return (worldObject is BeeHiveObject || worldObject is SmallBeeHiveObject) 
                       && worldObject != Parent;
            }
            
            return false;
        }

        private void UpdateStatus()
        {
            var hiveType = Parent is SmallBeeHiveObject ? "small hive" : "beehive";
            var message = hasValidSpacing 
                ? $"Proper spacing maintained (no {hiveType}s within {MINIMUM_DISTANCE} blocks)"
                : $"Too close to another {hiveType}! Requires {MINIMUM_DISTANCE} blocks minimum distance.";

            status?.SetStatusMessage(hasValidSpacing, Localizer.DoStr(message));
        }
    }
}