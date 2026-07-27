// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Big Moai Statue recipe for beekeeping mod

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Core.Items;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;
	using Eco.Gameplay.Housing;
	using Eco.Gameplay.Housing.PropertyValues;
	using Eco.Shared.Items;
	using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;
	
	using Eco.Shared.Services;
	using Eco.Shared.SharedTypes;
	using Eco.Simulation;
	using Eco.Gameplay.Interactions;
	using Eco.Gameplay.Interactions.Interactors;
	using System.Linq;

    /// <summary>
    /// <para>Server side recipe definition for "Big Moai Statue".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(MasonrySkill), 7)]
    [Ecopedia("Items", "Decorative", subPageName: "Big Moai Statue")]
    public partial class BigMoaiStatueRecipe : RecipeFamily
    {
        public BigMoaiStatueRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BigMoaiStatue",  //noloc
                displayName: Localizer.DoStr("Big Moai Statue"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PureWaxItem), 5, typeof(MasonrySkill)),
                    new IngredientElement(typeof(LimestoneItem), 35, typeof(MasonrySkill)),
                    new IngredientElement(typeof(PropolisItem), 3, typeof(MasonrySkill)),
                    new IngredientElement(typeof(RoyalJellyItem), 2, typeof(MasonrySkill)),
                    new IngredientElement(typeof(BeewaxItem), 5, typeof(MasonrySkill))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BigMoaiStatueItem>(1) // Creates 1 big Moai statue
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(800, typeof(MasonrySkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BigMoaiStatueRecipe), start: 45f, skillType: typeof(MasonrySkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Big Moai Statue"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Big Moai Statue"), recipeType: typeof(BigMoaiStatueRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
	[RequireComponent(typeof(HousingComponent))] 
	[Tag("Usable")]
    [Ecopedia("Items", "Decorative", subPageName: "Big Moai Statue Object")]
    public partial class BigMoaiStatueObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(BigMoaiStatueItem);
        public override LocString DisplayName => Localizer.DoStr("Big Moai Statue");

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = BigMoaiStatueItem.homeValue;
            this.ModsPostInitialize();
        }
		

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side item definition for the "Big Moai Statue" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.Item.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Big Moai Statue")] // Defines the localized name of the item.
    [Weight(2500)] // Defines how heavy the Big Moai Statue is.
    [Ecopedia("Items", "Decorative", createAsSubPage: true)]
    [LocDescription("A massive decorative statue inspired by the mysterious Moai of Easter Island, requiring master beekeeping skills and premium materials.")] //The tooltip description for the item.
	[Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class BigMoaiStatueItem : WorldObjectItem<BigMoaiStatueObject>
    {
		
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Down , WorldObject.GetOccupancyInfo(this.WorldObjectType));
		public override HomeFurnishingValue HomeValue => homeValue;

        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(BigMoaiStatueObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }
}