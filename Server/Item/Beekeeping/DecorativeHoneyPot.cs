// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Decorative Honey Pot - A beautiful pot filled with golden honey

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Civics.Objects;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;

    /// <summary>
    /// <para>Server side recipe definition for "Decorative Honey Pot".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    [RequiresSkill(typeof(BeekeepingSkill), 3)]
    [Ecopedia("Housing Objects", "Kitchen", subPageName: "Decorative Honey Pot Item")]
    public partial class DecorativeHoneyPotRecipe : RecipeFamily
    {
        public DecorativeHoneyPotRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "DecorativeHoneyPot",  //noloc
                displayName: Localizer.DoStr("Decorative Honey Pot"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(EmptyPotItem), 1, true),
                    new IngredientElement(typeof(HoneyItem), 4, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<DecorativeHoneyPotItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(BeekeepingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(DecorativeHoneyPotRecipe), start: 0.5f, skillType: typeof(BeekeepingSkill), typeof(BeekeepingFocusedSpeedTalent), typeof(BeekeepingParallelSpeedTalent));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Decorative Honey Pot"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Decorative Honey Pot"), recipeType: typeof(DecorativeHoneyPotRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(HoneyExtractMechanicalObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Kitchen", subPageName: "Decorative Honey Pot Item")]
    public partial class DecorativeHoneyPotObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(DecorativeHoneyPotItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Honey Pot");

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
    [LocDisplayName("Decorative Honey Pot")]
    [LocDescription("A beautiful glass pot filled with golden honey. The sweet aroma and warm glow make it perfect for kitchen decoration.")]
    [Ecopedia("Housing Objects", "Kitchen", createAsSubPage: true)]
    [Weight(150)] // Defines how heavy Decorative Honey Pot is - heavier than empty pot due to honey
	[Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class DecorativeHoneyPotItem : WorldObjectItem<DecorativeHoneyPotObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

        public override HomeFurnishingValue HomeValue => homeValue;

        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(DecorativeHoneyPotObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Kitchen"),
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
			BaseValue = 1,
            DiminishingReturnMultiplier = 0.6f,
        };
    }
}