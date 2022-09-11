using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Simulation.Time;
using System;

namespace Eco.Gameplay.Components
{
    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class QueenBeeComponent : WorldObjectComponent
    {
        [Serialized]
        public bool isModulePresent = false;
        [Serialized]
        public double initialize = WorldTime.Seconds;
        private StatusElement status;

        public override void Initialize()
        {
            ((ThreadSafeActionBase<Action>)this.Parent.GetComponent<PluginModulesComponent>((string)null).OnChanged).Add(new Action(this.Test));
            this.status = this.Parent.GetComponent<StatusComponent>((string)null).CreateStatusElement(-10);
            this.UpdateStatus();
        }

        private void Test()
        {
            this.isModulePresent = !this.Parent.GetComponent<PluginModulesComponent>((string)null).Inventory.IsEmpty;
            this.initialize = WorldTime.Seconds;
        }

        public override void Tick()
        {
            base.Tick();
            if (!this.isModulePresent || WorldTime.Seconds < this.initialize + 43200.0)
                return;
            this.UpdateStatus();
            this.Parent.Destroy();
            WorldObjectDebugUtil.Spawn("OnduOccupiedSauvageBeehiveObject", (User)null, this.Parent.Position3i);
            this.isModulePresent = false;
        }

        private void UpdateStatus()
        {
            this.status?.SetStatusMessage(this.isModulePresent, Localizer.DoStr("If you put Royal Jelly in the vacant swarm, a queen bee will appear 12 hours later."));
        }
    }
}
