// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduTajineRecipe
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
  [RequiresSkill(typeof (AdvancedCookingSkill), 6)]
  public class OnduTajineRecipe : RecipeFamily
  {
    public OnduTajineRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Tajine", Localizer.DoStr("Tajine"), new IngredientElement[5]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 2f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (InfusedOilItem), 1f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (SimmeredMeatItem), 1f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (CornmealItem), 1f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (VegetableMedleyItem), 1f, typeof (AdvancedCookingSkill), (Type) null)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduTajineItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(20f, typeof (AdvancedCookingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduTajineRecipe), 4f, typeof (AdvancedCookingSkill), new Type[2]
      {
        typeof (AdvancedCookingFocusedSpeedTalent),
        typeof (AdvancedCookingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Tajine"), typeof (OnduTajineRecipe));
      CraftingComponent.AddRecipe(typeof (StoveObject), (RecipeFamily) this);
    }
  }
}
