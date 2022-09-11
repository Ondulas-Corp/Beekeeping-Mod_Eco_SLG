// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPurifiedHoney2Recipe
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
  [RequiresSkill(typeof (BeekeepingSkill), 1)]
  public class OnduPurifiedHoney2Recipe : RecipeFamily
  {
    public OnduPurifiedHoney2Recipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("PurifiedHoney", Localizer.DoStr("Purified Honey"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduFullPotItem), 1f, false)
        }, new CraftingElement[2]
        {
          (CraftingElement) new CraftingElement<OnduPurifiedHoneyItem>(2f),
          (CraftingElement) new CraftingElement<OnduEmptyPotItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(100f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduPurifiedHoney2Recipe), 3f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Purified Honey"), typeof (OnduPurifiedHoney2Recipe));
      CraftingComponent.AddRecipe(typeof (OnduHoneyExtractMecanicalObject), (RecipeFamily) this);
    }
  }
}
