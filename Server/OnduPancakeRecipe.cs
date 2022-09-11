// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPancakeRecipe
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
  [RequiresSkill(typeof (BakingSkill), 5)]
  public class OnduPancakeRecipe : RecipeFamily
  {
    public OnduPancakeRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Pancake", Localizer.DoStr("Pancake"), new IngredientElement[4]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (BakingSkill), (Type) null),
          new IngredientElement(typeof (SugarItem), 1f, typeof (BakingSkill), (Type) null),
          new IngredientElement(typeof (FlourItem), 2f, typeof (BakingSkill), (Type) null),
          new IngredientElement("Fat", 2f, typeof (BakingSkill), (Type) null)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduPancakeItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(20f, typeof (BakingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduPancakeRecipe), 2f, typeof (BakingSkill), new Type[2]
      {
        typeof (BakingFocusedSpeedTalent),
        typeof (BakingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Pancake"), typeof (OnduPancakeRecipe));
      CraftingComponent.AddRecipe(typeof (BakeryOvenObject), (RecipeFamily) this);
    }
  }
}
