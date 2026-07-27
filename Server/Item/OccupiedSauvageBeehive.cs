// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Wild bee swarm objects - Vacant and Occupied

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Interactions.Interactors;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.SharedTypes;
    using Eco.Shared.Utils;
    using Beekeeping.Server.Module;

    // Occupied Swarm (OccupiedSauvageBeehive)
    [Serialized]
	[RequireComponent(typeof(OccupancyRequirementComponent))]
	[Tag("WorldGenerated")]
    [Tag("Usable")]
    [Ecopedia("Items", "Products", subPageName: "Occupied Swarm Item")]
    public partial class OccupiedSauvageBeehiveObject : WorldObject, IHasInteractions
    {
        private bool switching = false;

        public override LocString DisplayName => Localizer.DoStr("Occupied Swarm");
		
        [Interaction(InteractionTrigger.InteractKey)]
		public void HarvestBees(Player player, InteractionTriggerInfo trigger, InteractionTarget target)
        {
            if (!switching)
            {
                switching = true;
                
                var position = this.Position;
                var rotation = this.Rotation;
                
                player.User.Inventory.TryModify(changeSet =>
                {
                    // Always get 1-2 Bee Eggs
                    int eggCount = RandomUtil.Range(1, 3);
                    changeSet.AddItem(Item.Get(typeof(BeeEggsItem)), eggCount);
                    
                    // 5% chance to get 1 Worker Bee
                    if (RandomUtil.Range(1, 101) <= 5)
                    {
                        changeSet.AddItem(Item.Get(typeof(WorkerBeeItem)));
                    }
                    
                    // 5% chance to get 1-2 Wax
                    if (RandomUtil.Range(1, 101) <= 5)
                    {
                        int waxCount = RandomUtil.Range(1, 3);
                        changeSet.AddItem(Item.Get(typeof(BeewaxItem)), waxCount);
                    }

                    string message = $"You harvested {eggCount} bee eggs from the swarm!";
                    player.Msg(Localizer.DoStr(message), NotificationStyle.InfoBox);

                }, player.User);
				
				BeeHiveGeneration.OnHiveDestroyed(this.Position3i);
                this.Destroy();
                
                // Random chance to spawn vacant hive (prevents farming)
                var random = new Random();
                if (random.NextDouble() < 0.20) // 20% chance to spawn vacant
                {
                    WorldObjectManager.ForceAdd(typeof(VacantSauvageBeehiveObject), player.User, position, rotation);
                }
            }
            else
            {
                player.Msg(Localizer.DoStr("This bee swarm has already been harvested."), NotificationStyle.InfoBox);
            }
        }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Occupied Swarm")]
    [LocDescription("Occupied Swarm. Contains bee eggs and resources.")]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
	[Weight(500000)]
    [MaxStackSize(1)]
    [Tag("NotAllowedInInventories")]
    public partial class OccupiedSauvageBeehiveItem : WorldObjectItem<OccupiedSauvageBeehiveObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }
}