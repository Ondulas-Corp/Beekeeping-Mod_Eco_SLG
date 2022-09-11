// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFrameRecipe
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
  [RequiresSkill(typeof (CarpentrySkill), 2)]
  public class OnduFrameRecipe : RecipeFamily
  {
    public OnduFrameRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Frame", Localizer.DoStr("Frame"), new IngredientElement[2]
        {
          new IngredientElement("WoodBoard", 2f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent)),
          new IngredientElement(typeof (OnduIronWireItem), 2f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent))
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduFrameItem>(1f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(40f, typeof (CarpentrySkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduFrameRecipe), 0.7f, typeof (CarpentrySkill), new Type[2]
      {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Frame"), typeof (OnduFrameRecipe));
      CraftingComponent.AddRecipe(typeof (CarpentryTableObject), (RecipeFamily) this);
    }
  }
}
