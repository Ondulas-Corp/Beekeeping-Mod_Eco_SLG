// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyRoastRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
  [RequiresSkill(typeof (AdvancedBakingSkill), 4)]
  public class OnduHoneyRoastRecipe : RecipeFamily
  {
    public OnduHoneyRoastRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("HoneyRoast", Localizer.DoStr("Honey Roast"), new IngredientElement[4]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement(typeof (BakedRoastItem), 1f, false),
          new IngredientElement(typeof (MeatStockItem), 1f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement(typeof (SimpleSyrupItem), 1f, typeof (AdvancedBakingSkill), (Type) null)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduHoneyRoastItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(20f, typeof (AdvancedBakingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduHoneyRoastRecipe), 4f, typeof (AdvancedBakingSkill), new Type[2]
      {
        typeof (AdvancedCookingFocusedSpeedTalent),
        typeof (AdvancedCookingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Honey Roast"), typeof (OnduHoneyRoastRecipe));
      CraftingComponent.AddRecipe(typeof (BakeryOvenObject), (RecipeFamily) this);
    }
  }
}
