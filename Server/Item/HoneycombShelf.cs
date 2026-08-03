// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Honeycomb Shelf - A decorative shelf displaying honeycombs

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
    /// <para>Server side recipe definition for "Honeycomb Shelf".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    [RequiresSkill(typeof(CarpentrySkill), 2)]
    [Ecopedia("Housing Objects", "Living Room", subPageName: "Honeycomb Shelf Item")]
    public partial class HoneycombShelfRecipe : RecipeFamily
    {
        public HoneycombShelfRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneycombShelf",  //noloc
                displayName: Localizer.DoStr("Honeycomb Shelf"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 15, typeof(CarpentrySkill)),
                    new IngredientElement(typeof(BeewaxItem), 5, true)
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<HoneycombShelfItem>()
                });
				
			var recipeDark = new Recipe();
			recipeDark.Init(
				name: "HoneycombShelfDark",
				displayName: Localizer.DoStr("Honeycomb Shelf Dark"),
				ingredients: new List<IngredientElement>
				{
					new IngredientElement(typeof(HardwoodBoardItem), 15, typeof(CarpentrySkill)),
					new IngredientElement(typeof(BeewaxItem), 5, true)
				},
				items: new List<CraftingElement>
				{
					new CraftingElement<HoneycombShelfDarkItem>()
				});
				
            this.Recipes = new List<Recipe> { recipe, recipeDark };
            this.ExperienceOnCraft = 1.5f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(CarpenterSkill));

            // Defines our crafting time for the recipe
			this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(HoneycombShelfRecipe), start: 2, skillType: typeof(CarpentrySkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Honeycomb Shelf"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Honeycomb Shelf"), recipeType: typeof(HoneycombShelfRecipe));
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
    [RequireComponent(typeof(HousingComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Living Room", subPageName: "Honeycomb Shelf Item")]
    public partial class HoneycombShelfObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelfItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf");

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = HoneycombShelfItem.homeValue;
            base.Initialize();
            this.ModsPostInitialize();
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf")]
    [LocDescription("A wooden shelf displaying beautiful golden honeycombs. Perfect for showing off your beekeeping achievements.")]
    [Ecopedia("Housing Objects", "Living Room", createAsSubPage: true)]
    [Weight(500)] // Defines how heavy Honeycomb Shelf is - it's a proper shelf with wood and honeycombs
    public partial class HoneycombShelfItem : WorldObjectItem<HoneycombShelfObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));

        public override HomeFurnishingValue HomeValue => homeValue;

        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(HoneycombShelfObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }
}