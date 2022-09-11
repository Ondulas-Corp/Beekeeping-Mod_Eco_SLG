// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenFloorLampRecipe
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
  public class WaxedWoodenFloorLampRecipe : RecipeFamily
  {
    public WaxedWoodenFloorLampRecipe()
    {
      
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("WaxedWoodenFloorLamp", Localizer.DoStr("Waxed Wooden Floor Lamp"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenFloorLampItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<WaxedWoodenFloorLampItem>(1f)
        })
      });
      this.ExperienceOnCraft = 3.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(200f, typeof (CarpentrySkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (WaxedWoodenFloorLampRecipe), 1f, typeof (CarpentrySkill), new Type[2]
      {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Waxed Wooden Floor Lamp"), typeof (WaxedWoodenFloorLampRecipe));
      CraftingComponent.AddRecipe(typeof (SawmillObject), (RecipeFamily) this);
    }
  }
}
