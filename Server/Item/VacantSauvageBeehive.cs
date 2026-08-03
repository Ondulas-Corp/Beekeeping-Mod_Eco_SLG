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
    // Vacant Swarm — kept for save compatibility, no longer spawned
    [Serialized]
    [RequireComponent(typeof(StatusComponent))]
	[RequireComponent(typeof(OccupancyRequirementComponent))]
	[Tag("WorldGenerated")]
    [Tag("Usable")]
    [Ecopedia("Items", "Products", subPageName: "Vacant Swarm Item")]
    public partial class VacantSauvageBeehiveObject : WorldObject, IHasInteractions
    {
        public override LocString DisplayName => Localizer.DoStr("Vacant Swarm");
		
		[Interaction(InteractionTrigger.InteractKey)]
        public void RemoveHive(Player player, InteractionTriggerInfo trigger, InteractionTarget target)
        {
            player.Msg(Localizer.DoStr("You disturb the vacant swarm, causing it to relocate."), NotificationStyle.InfoBox);
            
            // Trigger regeneration when player interacts
            BeeHiveGeneration.OnHiveDestroyed(this.Position3i);
            this.Destroy();
        }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.ModsPostInitialize();
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Vacant Swarm")]
    [LocDescription("Vacant Swarm. E to remove.")]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
	[Weight(500000)]
    [MaxStackSize(1)]
    [Tag("NotAllowedInInventories")]
    [AllowPluginModules(ItemTypes = new Type[] { typeof(RoyalJellyItem) })]
    public partial class VacantSauvageBeehiveItem : WorldObjectItem<VacantSauvageBeehiveObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Up | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }
}