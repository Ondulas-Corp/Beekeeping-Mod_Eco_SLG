using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Simulation.Time;
using System;

namespace Beekeeping.Server
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
            Parent.GetComponent<PluginModulesComponent>(null).OnChanged.Add(new Action(Test));
            status = Parent.GetComponent<StatusComponent>(null).CreateStatusElement(-10);
            UpdateStatus();
        }

        private void Test()
        {
            isModulePresent = !Parent.GetComponent<PluginModulesComponent>(null).Inventory.IsEmpty;
            initialize = WorldTime.Seconds;
        }

        public override void Tick()
        {
            base.Tick();
            if (!isModulePresent || WorldTime.Seconds < initialize + 43200.0)
                return;
            UpdateStatus();
            Parent.Destroy();
            WorldObjectDebugUtil.Spawn("OnduOccupiedSauvageBeehiveObject", null, Parent.Position3i);
            isModulePresent = false;
        }

        private void UpdateStatus()
        {
            status?.SetStatusMessage(isModulePresent, Localizer.DoStr("If you put Royal Jelly in the vacant swarm, a queen bee will appear 12 hours later."));
        }
    }
}
