// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduEmptyPotRecipe
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
  [RequiresSkill(typeof (GlassworkingSkill), 1)]
  public class OnduEmptyPotRecipe : RecipeFamily
  {
    public OnduEmptyPotRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("EmptyPot", Localizer.DoStr("Empty Pot"), new IngredientElement[1]
        {
          new IngredientElement(typeof (GlassItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduEmptyPotItem>(1f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(100f, typeof (GlassworkingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduEmptyPotRecipe), 1f, typeof (GlassworkingSkill), new Type[2]
      {
        typeof (GlassworkingFocusedSpeedTalent),
        typeof (GlassworkingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Empty Pot"), typeof (OnduEmptyPotRecipe));
      CraftingComponent.AddRecipe(typeof (KilnObject), (RecipeFamily) this);
    }
  }
}
