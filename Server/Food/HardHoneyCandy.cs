// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Hard Honey Candy recipe for beekeeping mod

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
    /// <para>Server side recipe definition for "Hard Honey Candy".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(BakingSkill), 1)]
    [Ecopedia("Food", "Confectionery", subPageName: "Hard Honey Candy")]
    public partial class HardHoneyCandyRecipe : RecipeFamily
    {
        public HardHoneyCandyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HardHoneyCandy",  //noloc
                displayName: Localizer.DoStr("Hard Honey Candy"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(BakingSkill), typeof(BakingLavishResourcesTalent)),
                    new IngredientElement(typeof(SugarItem), 1, true)
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<HardHoneyCandyItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(15, typeof(BakingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(HardHoneyCandyRecipe), start: 3f, skillType: typeof(BakingSkill), typeof(BakingFocusedSpeedTalent), typeof(BakingParallelSpeedTalent));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Hard Honey Candy"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Hard Honey Candy"), recipeType: typeof(HardHoneyCandyRecipe));
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
    /// <para>Server side food item definition for the "Hard Honey Candy" item.</para>
    /// <para>More information about FoodItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.FoodItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Hard Honey Candy")] // Defines the localized name of the item.
    [Weight(30)] // Defines how heavy the Hard Honey Candy is.
    [Ecopedia("Food", "Confectionery", createAsSubPage: true)]
    [LocDescription("Long-lasting hard candy made with honey and sugar. Perfect energy snack for extended work periods.")] //The tooltip description for the food item.
    public partial class HardHoneyCandyItem : FoodItem
    {
        /// <summary>The plural localization name for the food item.</summary>
        public override LocString DisplayNamePlural     => Localizer.DoStr("Hard Honey Candies");

        /// <summary>The amount of calories awarded for eating the food item.</summary>
        public override float Calories                  => 100;
        /// <summary>The nutritional value of the food item.</summary>
        public override Nutrients Nutrition             => new Nutrients() { Carbs = 10, Fat = 0, Protein = 0, Vitamins = 0};

        /// <summary>Defines the default time it takes for this item to spoil. This value can be modified by the inventory this item currently resides in.</summary>
        protected override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(168); // 1 weeks shelf life
    }
}