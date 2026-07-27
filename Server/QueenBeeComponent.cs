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
    [RequireComponent(typeof(StatusComponent))]
    public class QueenBeeComponent : WorldObjectComponent
    {
        [Serialized]
        public bool isModulePresent = false;

        [Serialized]
        public double initialize = WorldTime.Seconds;

        [Serialized]
        public double transformationTime = 0; // When the transformation will occur

        private StatusElement status;
        private static readonly Random random = new Random();

        public override void Initialize()
        {
            var pluginModules = Parent.GetComponent<PluginModulesComponent>();
            pluginModules.OnChanged.Add(new Action(OnModuleChanged));

            status = Parent.GetComponent<StatusComponent>().CreateStatusElement(-10);
            UpdateStatus();
        }

        private void OnModuleChanged()
        {
            var pluginModules = Parent.GetComponent<PluginModulesComponent>();
            isModulePresent = !pluginModules.Inventory.IsEmpty;

            if (isModulePresent)
            {
                initialize = WorldTime.Seconds;
                
                // Random time between 6-16 in-game hours (21600-57600 seconds)
                double randomHours = 6 + (random.NextDouble() * 10); // 6 to 16 hours
                transformationTime = initialize + (randomHours * 3600); // Convert to seconds
            }
            
            // Always update status when module changes
            UpdateStatus();
        }

        public override void Tick()
        {
            base.Tick();

            if (!isModulePresent || WorldTime.Seconds < transformationTime)
                return;

            // Perform transformation
            Parent.Destroy();

            // Replace hive with the new "occupied" version
            WorldObjectManager.ForceAdd(
                typeof(OccupiedSauvageBeehiveObject),
                null,
                Parent.Position3i,
                Parent.Rotation
            );

            isModulePresent = false;
        }

        private void UpdateStatus()
        {
            if (isModulePresent)
            {
                status?.SetStatusMessage(
                    true,
                    Localizer.DoStr("Royal jelly is attracting a queen bee...")
                );
            }
            else
            {
                status?.SetStatusMessage(
                    false,
                    Localizer.DoStr("Place royal jelly here to attract a wild queen bee (6-16 hours).")
                );
            }
        }
    }
}