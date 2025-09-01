using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class RequiredModuleComponent : WorldObjectComponent
    {
        [Serialized]
        private bool isPresent;

        private StatusElement? status;

        public override bool Enabled => isPresent;

        public override void Initialize()
        {
            var pluginModules = Parent.GetComponent<PluginModulesComponent>();
            pluginModules.OnChanged.Add(IsModulePresent);

            var statusComponent = Parent.GetComponent<StatusComponent>();
            status = statusComponent.CreateStatusElement(-40);

            IsModulePresent();
            UpdateStatus();
        }

        private void IsModulePresent()
        {
            var pluginModules = Parent.GetComponent<PluginModulesComponent>();
            bool moduleNowPresent = !pluginModules.Inventory.IsEmpty;

            if (isPresent == moduleNowPresent)
                return;

            isPresent = moduleNowPresent;

            UpdateStatus();
            Parent.UpdateEnabledAndOperating();
            Parent.SetDirty();
        }

        private void UpdateStatus()
        {
            status?.SetStatusMessage(
                isPresent,
                Localizer.DoStr("This hive won't function without a module. Find a Queen Bee in swarms found in the wild.")
            );
        }
    }
}
