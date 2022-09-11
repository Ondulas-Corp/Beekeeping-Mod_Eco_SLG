// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBeehiveRecipe
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
  public class OnduBeehiveRecipe : RecipeFamily
  {
    public OnduBeehiveRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("BreedingBeehive", Localizer.DoStr("Breeding Beehive"), new IngredientElement[3]
        {
          new IngredientElement("WoodBoard", 14f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent)),
          new IngredientElement(typeof (IronBarItem), 2f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent)),
          new IngredientElement(typeof (PlantFibersItem), 30f, typeof (CarpentrySkill), typeof (CarpentryLavishResourcesTalent))
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduBreedingBeehiveItem>(1f)
        })
      });
      this.ExperienceOnCraft = 2.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(30f, typeof (CarpentrySkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduBeehiveRecipe), 5f, typeof (CarpentrySkill), new Type[2]
      {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Breeding Beehive"), typeof (OnduBeehiveRecipe));
      CraftingComponent.AddRecipe(typeof (CarpentryTableObject), (RecipeFamily) this);
    }
  }
}
