// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduIronWireRecipe
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
  [RequiresSkill(typeof (SmeltingSkill), 1)]
  public class OnduIronWireRecipe : RecipeFamily
  {
    public OnduIronWireRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Iron Wire", Localizer.DoStr("Iron Wire"), new IngredientElement[1]
        {
          new IngredientElement(typeof (IronBarItem), 1f, typeof (SmeltingSkill), typeof (SmeltingLavishResourcesTalent))
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduIronWireItem>(10f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(60f, typeof (SmeltingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduIronWireRecipe), 0.8f, typeof (SmeltingSkill), new Type[2]
      {
        typeof (SmeltingFocusedSpeedTalent),
        typeof (SmeltingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Iron Wire"), typeof (OnduIronWireRecipe));
      CraftingComponent.AddRecipe(typeof (AnvilObject), (RecipeFamily) this);
    }
  }
}
