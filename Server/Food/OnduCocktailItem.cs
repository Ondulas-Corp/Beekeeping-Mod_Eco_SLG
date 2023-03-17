// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCocktailItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
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

namespace Beekeeping.Server.Food
{
    [Serialized]
    [LocDisplayName("Blueberry Cocktail")]
    [Weight(350)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    public class OnduCocktailItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Blueberry Cocktail");
        public override LocString DisplayDescription => Localizer.DoStr("For special occasions...");
        public override float Calories => 800;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 19, Fat = 6, Protein = 8, Vitamins = 29 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    [Ecopedia("Food", "Cooking", subPageName: "BlueberryCocktail Item")]
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
                    new IngredientElement(typeof(SugarItem), 4, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(HuckleberryExtractItem), 3, typeof(AdvancedCookingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduCocktailItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedCookingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduCocktailRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill),
                typeof(AdvancedCookingFocusedSpeedTalent),
                typeof(AdvancedCookingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Blueberry Cocktail"), recipeType: typeof(OnduCocktailRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(KitchenObject), recipe: this);
        }
    }
}
