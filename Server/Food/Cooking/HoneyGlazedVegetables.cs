// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Honey Glazed Vegetables recipe for beekeeping mod

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
    /// <para>Server side recipe definition for "Honey Glazed Vegetables".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(CookingSkill), 2)]
    [Ecopedia("Food", "Cooking", subPageName: "Honey Glazed Vegetables")]
    public partial class HoneyGlazedVegetablesRecipe : RecipeFamily
    {
        public HoneyGlazedVegetablesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneyGlazedVegetables",  //noloc
                displayName: Localizer.DoStr("Honey Glazed Vegetables"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(CookingSkill)),
                    new IngredientElement(typeof(VegetableMedleyItem), 3, true)
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<HoneyGlazedVegetablesItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1.5f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(35, typeof(CookingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(HoneyGlazedVegetablesRecipe), start: 2.5f, skillType: typeof(CookingSkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Honey Glazed Vegetables"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Honey Glazed Vegetables"), recipeType: typeof(HoneyGlazedVegetablesRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side food item definition for the "Honey Glazed Vegetables" item.</para>
    /// <para>More information about FoodItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.FoodItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Honey Glazed Vegetables")] // Defines the localized name of the item.
    [Weight(300)] // Defines how heavy the Honey Glazed Vegetables is.
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("Fresh vegetables glazed with honey, providing enhanced vitamins and natural sweetness.")] //The tooltip description for the food item.
    public partial class HoneyGlazedVegetablesItem : FoodItem
    {
        /// <summary>The plural localization name for the food item.</summary>
        public override LocString DisplayNamePlural     => Localizer.DoStr("Honey Glazed Vegetables");

        /// <summary>The amount of calories awarded for eating the food item.</summary>
        public override float Calories                  => 840;
        /// <summary>The nutritional value of the food item.</summary>
        public override Nutrients Nutrition             => new Nutrients() { Carbs = 10, Fat = 1, Protein = 4, Vitamins = 18};

        /// <summary>Defines the default time it takes for this item to spoil. This value can be modified by the inventory this item currently resides in.</summary>
        public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(36); // 1.5 days shelf life
    }
}