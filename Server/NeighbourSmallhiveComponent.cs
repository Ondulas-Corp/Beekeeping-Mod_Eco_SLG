

using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
// A component that forces the player to move the small hives away from each other
namespace Eco.Gameplay.Components
{
    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    [RequireComponent(typeof(ChunkSubscriberComponent), null)]
    public class NeighbourSmallhiveComponent : WorldObjectComponent, IChunkSubscriber
    {
        [Serialized]
        private bool noNeighbourBeehive = true;
        private StatusElement status;

        public override bool Enabled
        {
            get
            {
                return this.noNeighbourBeehive;
            }
        }

        public float UpdateFrequencySec
        {
            get
            {
                return 2f;
            }
        }

        public float MaxQueuedChunkUpdateTime
        {
            get
            {
                return 10f;
            }
        }

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public override void Initialize()
        {
            this.status = this.Parent.GetComponent<StatusComponent>((string)null).CreateStatusElement(-50);
            this.HasNeighbour();
            this.UpdateStatus();
        }

        public void ChunksChanged()
        {
            bool flag = !this.HasNeighbour();
            if (flag == this.noNeighbourBeehive)
                return;
            this.noNeighbourBeehive = flag;
            this.UpdateStatus();
            this.Parent.UpdateEnabledAndOperating();
            this.Parent.SetDirty();
        }

        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            List<Vector3i> liste = new List<Vector3i>();
            int size = 4;
            this.SpiralDestruction(size).ToList<WorldPosition3i>().ForEach((Action<WorldPosition3i>)(x =>
            {
                float num = System.Math.Min((float)size * 0.5f, (float)size * 0.6f - WorldPosition3i.Distance(x, (WorldPosition3i)this.Parent.Position3i));
                for (int index = 0; (double)index < (double)num; ++index)
                {
                    liste.Add(Eco.Shared.Voxel.World.ToChunkPosition((WrappedWorldPosition3i)(new Vector3i(x.x, x.y + index, x.z))));
                    if ((uint)index > 0U)
                        liste.Add(Eco.Shared.Voxel.World.ToChunkPosition((WrappedWorldPosition3i)(new Vector3i(x.x, x.y - index, x.z))));
                }
            }));
            return liste.Distinct<Vector3i>();
        }

        private bool HasNeighbour()
        {
            int size = 6;
            bool hasNeighbour = false;
            this.SpiralDestruction(size).ToList<WorldPosition3i>().ForEach((Action<WorldPosition3i>)(x =>
            {
                float num1 = System.Math.Min((float)size * 0.5f, (float)size * 0.6f - WorldPosition3i.Distance(x, (WorldPosition3i)this.Parent.Position3i));
                WorldObjectHandle worldObjectHandle;
                for (int index = 0; (double)index < (double)num1; ++index)
                {
                    int num2;
                    if (Eco.World.World.GetBlock((WrappedWorldPosition3i)((Vector3i)x + Vector3i.Up * index)) is WorldObjectBlock block)
                    {
                        worldObjectHandle = block.WorldObjectHandle;
                        if (worldObjectHandle.Object is OnduSmallHiveObject)
                        {
                            worldObjectHandle = block.WorldObjectHandle;
                            num2 = worldObjectHandle.Object != this.Parent ? 1 : 0;
                            goto label_5;
                        }
                    }
                    num2 = 0;
                label_5:
                    if (num2 != 0)
                        hasNeighbour = true;
                }
                for (int index = 0; (double)index < (double)num1; ++index)
                {
                    int num2;
                    if (Eco.World.World.GetBlock((WrappedWorldPosition3i)((Vector3i)x + Vector3i.Down * index)) is WorldObjectBlock block)
                    {
                        worldObjectHandle = block.WorldObjectHandle;
                        if (worldObjectHandle.Object is OnduSmallHiveObject)
                        {
                            worldObjectHandle = block.WorldObjectHandle;
                            num2 = worldObjectHandle.Object != this.Parent ? 1 : 0;
                            goto label_14;
                        }
                    }
                    num2 = 0;
                label_14:
                    if (num2 != 0)
                        hasNeighbour = true;
                }
            }));
            return hasNeighbour;
        }

        private void UpdateStatus()
        {
            this.status?.SetStatusMessage(this.noNeighbourBeehive, Localizer.DoStr("No works if there is a small hive around (6 blocks diametre)."));
        }

        public IEnumerable<WorldPosition3i> SpiralDestruction(int size)
        {
            Vector3i offset = new Vector3i(0, 0, 0);
            Vector3i delta = new Vector3i(0, 0, -1);
            for (int i = size * size; i > 0; --i)
            {
                yield return (WorldPosition3i)(this.Parent.Position3i + offset);
                if (offset.x == offset.z || offset.x < 0 && offset.x == -offset.z || offset.x > 0 && offset.x == 1 - offset.z)
                    delta = new Vector3i(-delta.z, 0, delta.x);
                offset += delta;
            }
        }
    }
}
