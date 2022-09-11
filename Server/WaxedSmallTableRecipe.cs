// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedSmallTableRecipe
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
  [RequiresSkill(typeof (CarpentrySkill), 7)]
  public class WaxedSmallTableRecipe : RecipeFamily
  {
    public WaxedSmallTableRecipe()
    {
      
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("WaxedSmallTable", Localizer.DoStr("Waxed Small Table"), new IngredientElement[3]
        {
          new IngredientElement(typeof (LumberItem), 2f, false),
          new IngredientElement(typeof (NailItem), 3f, false),
          new IngredientElement(typeof (OnduWaxItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<WaxedSmallTableItem>(1f)
        })
      });
      this.ExperienceOnCraft = 3.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(100f, typeof (CarpentrySkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (WaxedSmallTableRecipe), 1f, typeof (CarpentrySkill), new Type[2]
      {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Waxed Small Table"), typeof (WaxedSmallTableRecipe));
      CraftingComponent.AddRecipe(typeof (SawmillObject), (RecipeFamily) this);
    }
  }
}
