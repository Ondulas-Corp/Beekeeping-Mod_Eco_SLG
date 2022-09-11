// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxSheetRecipe
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
  public class OnduWaxSheetRecipe : RecipeFamily
  {
    public OnduWaxSheetRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("WaxSheet", Localizer.DoStr("Wax Sheet"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduInnocupiedSauvageBeehiveItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduWaxSheetItem>(5f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(60f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduWaxSheetRecipe), 1f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Wax Sheet"), typeof (OnduWaxSheetRecipe));
      CraftingComponent.AddRecipe(typeof (WorkbenchObject), (RecipeFamily) this);
    }
  }
}
