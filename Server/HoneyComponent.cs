
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Voxel;
using Eco.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

/* Component used to detect if there are enough plants around a breeding beehive. 
 If you want to increase or decrease the number of plants needed, just change the value of the condition in line 104 */

namespace Eco.Gameplay.Components
{
    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    [RequireComponent(typeof(ChunkSubscriberComponent), null)]
    public class HoneyComponent : WorldObjectComponent, IChunkSubscriber
    {
        [Serialized]
        private bool IsFlowered = false;
        private StatusElement status;

        public override bool Enabled
        {
            get
            {
                return this.IsFlowered;
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
                return 300f;
            }
        }

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public override void Initialize()
        {
            this.status = this.Parent.GetComponent<StatusComponent>((string)null).CreateStatusElement(-60);
            this.Test();
            this.UpdateStatus();
        }

        public void ChunksChanged()
        {
            this.Test();
            this.UpdateStatus();
            this.Parent.UpdateEnabledAndOperating();
        }

        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            List<Vector3i> liste = new List<Vector3i>();
            int size = 10;
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

        private void Test()
        {
            int nbrplant = 0;
            int size = 10;
            this.IsFlowered = false;
            this.SpiralDestruction(size).ToList<WorldPosition3i>().ForEach((Action<WorldPosition3i>)(x =>
            {
                float num = System.Math.Min((float)size * 0.5f, (float)size * 0.6f - WorldPosition3i.Distance(x, (WorldPosition3i)this.Parent.Position3i));
                for (int index = 0; (double)index < (double)num; ++index)
                {
                    if (EcoSim.PlantSim.GetPlant((WorldPosition3i)((Vector3i)x + Vector3i.Up * index)) != null)
                        ++nbrplant;
                    if (EcoSim.PlantSim.GetPlant((WorldPosition3i)((Vector3i)x + Vector3i.Down * index)) != null)
                        ++nbrplant;
                }
            }));
            if (nbrplant <= 40)
                return;
            this.IsFlowered = true;
        }

        private void UpdateStatus()
        {
            this.status?.SetStatusMessage(this.IsFlowered, Localizer.DoStr("Enough plants around for pollen (40 plants minimun)."));
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
