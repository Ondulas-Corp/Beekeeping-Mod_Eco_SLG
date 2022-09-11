// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSmallHiveRecipe
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
  [RequiresSkill(typeof (CarpentrySkill), 5)]
  public class OnduSmallHiveRecipe : RecipeFamily
  {
    public OnduSmallHiveRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Smallhive", Localizer.DoStr("Small Beehive"), new IngredientElement[3]
        {
          new IngredientElement("WoodBoard", 10f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent)),
          new IngredientElement(typeof (IronBarItem), 1f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent)),
          new IngredientElement(typeof (NailItem), 8f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent))
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduSmallHiveItem>(1f)
        })
      });
      this.ExperienceOnCraft = 1.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(30f, typeof (CarpentrySkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduSmallHiveRecipe), 5f, typeof (CarpentrySkill), new Type[2]
      {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Small hive"), typeof (OnduSmallHiveRecipe));
      CraftingComponent.AddRecipe(typeof (CarpentryTableObject), (RecipeFamily) this);
    }
  }
}
