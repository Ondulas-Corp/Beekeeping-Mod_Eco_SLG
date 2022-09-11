// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBiodieselRecipe
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
  [RequiresSkill(typeof (OilDrillingSkill), 2)]
  public class OnduBiodieselRecipe : RecipeFamily
  {
    public OnduBiodieselRecipe()
    {

      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Biodiesel", Localizer.DoStr("Biodiesel"), new IngredientElement[3]
        {
          new IngredientElement(typeof (OnduWaxItem), 1f, typeof (OilDrillingSkill), typeof (OilDrillingLavishResourcesTalent)),
          new IngredientElement(typeof (BarrelItem), 1f, true),
          new IngredientElement(typeof (EthanolItem), 6f, typeof (OilDrillingSkill), typeof (OilDrillingLavishResourcesTalent))
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<BiodieselItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(100f, typeof (OilDrillingSkill)));
      this.ExperienceOnCraft = 0.5f;
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduBiodieselRecipe), 0.8f, typeof (OilDrillingSkill), new Type[2]
      {
        typeof (OilDrillingFocusedSpeedTalent),
        typeof (OilDrillingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Biodiesel"), typeof (OnduBiodieselRecipe));
      CraftingComponent.AddRecipe(typeof (OilRefineryObject), (RecipeFamily) this);
    }
  }
}
