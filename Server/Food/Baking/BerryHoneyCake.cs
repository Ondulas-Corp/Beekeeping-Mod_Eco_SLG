// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Berry Honey Cake recipe for beekeeping mod

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Core.Items;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Shared.Time;
    using Eco.Mods.TechTree;

    /// <summary>
    /// <para>Server side recipe definition for "Berry Honey Cake".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(BakingSkill), 2)]
    [Ecopedia("Food", "Baking", subPageName: "Berry Honey Cake")]
    public partial class BerryHoneyCakeRecipe : RecipeFamily
    {
        public BerryHoneyCakeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BerryHoneyCake",  //noloc
                displayName: Localizer.DoStr("Berry Honey Cake"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 3, typeof(BakingSkill)),
                    new IngredientElement(typeof(HuckleberriesItem), 3, true)
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BerryHoneyCakeItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(BakingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BerryHoneyCakeRecipe), start: 6f, skillType: typeof(BakingSkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Berry Honey Cake"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Berry Honey Cake"), recipeType: typeof(BerryHoneyCakeRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side food item definition for the "Berry Honey Cake" item.</para>
    /// <para>More information about FoodItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.FoodItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Berry Honey Cake")] // Defines the localized name of the item.
    [Weight(600)] // Defines how heavy the Berry Honey Cake is.
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    [LocDescription("A delicious cake made with honey, flour, and fresh berries. Provides balanced nutrition with natural sweetness.")] //The tooltip description for the food item.
    public partial class BerryHoneyCakeItem : FoodItem
    {
        /// <summary>The plural localization name for the food item.</summary>
        public override LocString DisplayNamePlural     => Localizer.DoStr("Berry Honey Cakes");

        /// <summary>The amount of calories awarded for eating the food item.</summary>
        public override float Calories                  => 780;
        /// <summary>The nutritional value of the food item.</summary>
        public override Nutrients Nutrition             => new Nutrients() { Carbs = 18, Fat = 4, Protein = 6, Vitamins = 13};

        /// <summary>Defines the default time it takes for this item to spoil. This value can be modified by the inventory this item currently resides in.</summary>
        public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(48); // 2 days shelf life
    }
}