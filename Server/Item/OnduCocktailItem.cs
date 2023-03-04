// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCocktailItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Blueberry Cocktail")]
    [Weight(350)]
    public class OnduCocktailItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Blueberry Cocktail");
        public override LocString DisplayDescription => Localizer.DoStr("For special occasions...");
        public override float Calories => 700;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 20, Fat = 8, Protein = 6, Vitamins = 28 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    public class OnduCocktailRecipe : RecipeFamily
    {
        public OnduCocktailRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BlueberryCocktail",
                displayName: Localizer.DoStr("Blueberry Cocktail"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SugarItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(YeastItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(HuckleberryExtractItem), 2, typeof(AdvancedCookingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduCocktailItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedCookingSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduCocktailRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill),
                typeof(AdvancedCookingFocusedSpeedTalent),
                typeof(AdvancedCookingParallelSpeedTalent));

            this.Initialize(displayText: Localizer.DoStr("Blueberry Cocktail"), recipeType: typeof(OnduCocktailRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(KitchenObject), recipe: this);
        }
    }
}
