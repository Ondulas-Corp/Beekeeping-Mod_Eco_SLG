// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Honeycomb Shelf 2 Left - A decorative shelf displaying 2 honeycombs on the left

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
    /// <para>Server side recipe definition for "Honeycomb Shelf 2 Left".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    [RequiresSkill(typeof(CarpentrySkill), 2)]
    [Ecopedia("Housing Objects", "Living Room", subPageName: "Honeycomb Shelf 2 Left Item")]
    public partial class HoneycombShelf2LeftRecipe : RecipeFamily
    {
        public HoneycombShelf2LeftRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneycombShelf2Left",  //noloc
                displayName: Localizer.DoStr("Honeycomb Shelf 2 Left"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 6, typeof(CarpentrySkill)),
                    new IngredientElement(typeof(BeewaxItem), 3, true)
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<HoneycombShelf2LeftItem>()
                });
				
			var recipeDark = new Recipe();
			recipeDark.Init(
				name: "HoneycombShelf2LeftDark",
				displayName: Localizer.DoStr("Honeycomb Shelf 2 Left Dark"),
				ingredients: new List<IngredientElement>
				{
					new IngredientElement(typeof(HardwoodBoardItem), 6, typeof(CarpentrySkill)),
					new IngredientElement(typeof(BeewaxItem), 3, true)
				},
				items: new List<CraftingElement>
				{
					new CraftingElement<HoneycombShelf2LeftDarkItem>()
				});
				
            this.Recipes = new List<Recipe> { recipe, recipeDark };
            this.ExperienceOnCraft = 0.8f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(CarpenterSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(HoneycombShelf2LeftRecipe), start: 1, skillType: typeof(CarpentrySkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Honeycomb Shelf 2 Left"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Honeycomb Shelf 2 Left"), recipeType: typeof(HoneycombShelf2LeftRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
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
    [Ecopedia("Housing Objects", "Living Room", subPageName: "Honeycomb Shelf 2 Left Item")]
    public partial class HoneycombShelf2LeftObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelf2LeftItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf 2 Left");

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
    [LocDisplayName("Honeycomb Shelf 2 Left")]
    [LocDescription("A compact wooden shelf with 2 hexagonal compartments on the left, displaying golden honeycombs.")]
    [Ecopedia("Housing Objects", "Living Room", createAsSubPage: true)]
    [Weight(250)] // Defines how heavy Honeycomb Shelf 2 Left is - smaller than the 5-shelf version
    public partial class HoneycombShelf2LeftItem : WorldObjectItem<HoneycombShelf2LeftObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));

        public override HomeFurnishingValue HomeValue => homeValue;

        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(HoneycombShelf2LeftObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 1.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }
}