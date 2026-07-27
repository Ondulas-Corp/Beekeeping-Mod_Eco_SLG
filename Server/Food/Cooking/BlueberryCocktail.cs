using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Time;
using Beekeeping.Server;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Blueberry Cocktail")]
    [Weight(350)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("For special occasions...")]
    public partial class BlueberryCocktailItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Blueberry Cocktails");
        public override float Calories => 800;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 19, Fat = 6, Protein = 8, Vitamins = 29 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    [Ecopedia("Food", "Cooking", subPageName: "Blueberry Cocktail Item")]
    public partial class BlueberryCocktailRecipe : RecipeFamily
    {
        public BlueberryCocktailRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BlueberryCocktail",
                displayName: Localizer.DoStr("Blueberry Cocktail"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SugarItem), 4, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(HuckleberryExtractItem), 3, typeof(AdvancedCookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BlueberryCocktailItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BlueberryCocktailRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Blueberry Cocktail"), recipeType: typeof(BlueberryCocktailRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(KitchenObject), recipeFamily: this);
        }
    }
}