// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCocktailRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    public class OnduCocktailRecipe : RecipeFamily
    {
        public OnduCocktailRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("BlueberryCocktail", Localizer.DoStr("Blueberry Cocktail"), new IngredientElement[4]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (SugarItem), 2f, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (YeastItem), 1f, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (HuckleberryExtractItem), 2f, typeof (AdvancedCookingSkill),  null)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduCocktailItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(20f, typeof(AdvancedCookingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduCocktailRecipe), 4f, typeof(AdvancedCookingSkill), new Type[2]
            {
        typeof (AdvancedCookingFocusedSpeedTalent),
        typeof (AdvancedCookingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Blueberry Cocktail"), typeof(OnduCocktailRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}
