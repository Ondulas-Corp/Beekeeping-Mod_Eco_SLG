// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Small Moai Statue recipe for beekeeping mod

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
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

    /// <summary>
    /// <para>Server side recipe definition for "Small Moai Statue".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(MasonrySkill), 3)]
    [Ecopedia("Items", "Decorative", subPageName: "Small Moai Statue")]
    public partial class SmallMoaiStatueRecipe : RecipeFamily
    {
        public SmallMoaiStatueRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SmallMoaiStatue",  //noloc
                displayName: Localizer.DoStr("Small Moai Statue"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PureWaxItem), 2, typeof(MasonrySkill)),
                    new IngredientElement(typeof(LimestoneItem), 15, typeof(MasonrySkill)),
                    new IngredientElement(typeof(PropolisItem), 1, typeof(MasonrySkill))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<SmallMoaiStatueItem>(3) // Creates 3 small Moai statues as specified
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(150, typeof(MasonrySkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SmallMoaiStatueRecipe), start: 8f, skillType: typeof(MasonrySkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Small Moai Statue"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Small Moai Statue"), recipeType: typeof(SmallMoaiStatueRecipe));
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
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Items", "Decorative", subPageName: "Small Moai Statue Object")]
    public partial class SmallMoaiStatueObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SmallMoaiStatueItem);
        public override LocString DisplayName => Localizer.DoStr("Small Moai Statue");

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

    /// <summary>
    /// <para>Server side item definition for the "Small Moai Statue" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.Item.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Small Moai Statue")] // Defines the localized name of the item.
    [Weight(600)] // Defines how heavy the Small Moai Statue is.
	[Tag("Housing")]
    [Ecopedia("Items", "Decorative", createAsSubPage: true)]
    [LocDescription("A small decorative statue inspired by the mysterious Moai of Easter Island, crafted with bee wax and stone.")] //The tooltip description for the item.
	[Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class SmallMoaiStatueItem : WorldObjectItem<SmallMoaiStatueObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Down , WorldObject.GetOccupancyInfo(this.WorldObjectType));
		public override HomeFurnishingValue HomeValue => homeValue;

        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(SmallMoaiStatueObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 0.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }
	
}