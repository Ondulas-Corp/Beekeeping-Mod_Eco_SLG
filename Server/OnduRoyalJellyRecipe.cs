// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduRoyalJellyRecipe
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
  public class OnduRoyalJellyRecipe : RecipeFamily
  {
    public OnduRoyalJellyRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Royal Jelly", Localizer.DoStr("Royal Jelly"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduWaxItem), 1f, false),
          new IngredientElement(typeof (OnduFullPotItem), 1f, false)
        }, new CraftingElement[2]
        {
          (CraftingElement) new CraftingElement<OnduRoyalJellyItem>(1f),
          (CraftingElement) new CraftingElement<OnduEmptyPotItem>(1f)
        })
      });
      this.ExperienceOnCraft = 5.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(30f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduRoyalJellyRecipe), 0.5f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Royal Jelly"), typeof (OnduRoyalJellyRecipe));
      CraftingComponent.AddRecipe(typeof (OnduSmallHiveObject), (RecipeFamily) this);
    }
  }
}
