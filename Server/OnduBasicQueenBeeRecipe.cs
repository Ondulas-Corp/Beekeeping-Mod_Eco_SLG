// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBasicQueenBeeRecipe
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
  public class OnduBasicQueenBeeRecipe : RecipeFamily
  {
    public OnduBasicQueenBeeRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("BasicQueenBee", Localizer.DoStr("Basic Queen Bee"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduQueenBeeItem), 1f, false),
          new IngredientElement(typeof (OnduRoyalJellyItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduBasicQueenBeeItem>(1f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(150f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduBasicQueenBeeRecipe), 180f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Basic Queen Bee"), typeof (OnduBasicQueenBeeRecipe));
      CraftingComponent.AddRecipe(typeof (OnduSmallHiveObject), (RecipeFamily) this);
    }
  }
}
