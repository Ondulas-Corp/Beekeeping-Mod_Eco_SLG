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
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.SharedTypes;
    using Eco.Shared.Utils;
    using Beekeeping.Server.Module;


    // Occupied Swarm (OccupiedSauvageBeehive)
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [Tag("Usable")]
    [Ecopedia("Items", "Products", subPageName: "Occupied Swarm Item")]
    public partial class OccupiedSauvageBeehiveObject : WorldObject, IRepresentsItem, IHasInteractions
    {
        private bool switching = false;

        public virtual Type RepresentedItemType => typeof(OccupiedSauvageBeehiveItem);
        public override LocString DisplayName => Localizer.DoStr("Occupied Swarm");

        [Interaction(InteractionTrigger.InteractKey)]
        public void HarvestQueen(Player player, InteractionTriggerInfo trigger, InteractionTarget target)
        {
            if (!switching)
            {
                switching = true;
                
                var position = this.Position;
                var rotation = this.Rotation;
                
                player.User.Inventory.TryModify(changeSet =>
                {
                    var random = new System.Random();
                    double roll = random.NextDouble() * 100; // 0-100%
                    
                    if (roll < 60) // 60% chance for wax
                    {
                        int waxAmount = random.Next(1, 7); // 1-6 wax items
                        for (int i = 0; i < waxAmount; i++)
                        {
                            changeSet.AddItem(Item.Get(typeof(WaxItem)));
                        }
                        player.Msg(Localizer.DoStr($"You received {waxAmount} Wax!"), NotificationStyle.InfoBox);
                    }
                    else if (roll < 60.2) // 0.2% chance (reduced from 0.5%)
                    {
                        changeSet.AddItem(Item.Get(typeof(MatriarchQueenBeeItem)));
                        player.Msg(Localizer.DoStr("You received a rare Queen Bee Matriarch!"), NotificationStyle.InfoBox);
                    }
                    else if (roll < 61.4) // 1.2% chance (reduced from 3%)
                    {
                        changeSet.AddItem(Item.Get(typeof(AdultQueenBeeItem)));
                        player.Msg(Localizer.DoStr("You received a Queen Bee Adult!"), NotificationStyle.InfoBox);
                    }
                    else if (roll < 71.4) // 10% chance (reduced from 25%)
                    {
                        changeSet.AddItem(Item.Get(typeof(YoungQueenBeeItem)));
                        player.Msg(Localizer.DoStr("You received a Queen Bee Young!"), NotificationStyle.InfoBox);
                    }
                    else // 28.6% chance (remaining)
                    {
                        changeSet.AddItem(Item.Get(typeof(QueenBeeLarvaItem)));
                        player.Msg(Localizer.DoStr("You received a Queen Bee Larva!"), NotificationStyle.InfoBox);
                    }

                }, player.User);

                this.Destroy();
                
                var vacantObject = new VacantSauvageBeehiveObject();
                WorldObjectManager.ForceAdd(typeof(VacantSauvageBeehiveObject), player.User, position, rotation);
            }
            else
            {
                player.Msg(Localizer.DoStr("Queen Bee has already been taken."), NotificationStyle.InfoBox);
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
    [LocDescription("Occupied Swarm. Contains a Queen Bee.")]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [Weight(500)] // Defines how heavy OccupiedSauvageBeehive is.
    public partial class OccupiedSauvageBeehiveItem : WorldObjectItem<OccupiedSauvageBeehiveObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }
}