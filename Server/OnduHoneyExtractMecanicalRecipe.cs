// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractMecanicalRecipe
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
  [RequiresSkill(typeof (MechanicsSkill), 1)]
  public class OnduHoneyExtractMecanicalRecipe : RecipeFamily
  {
    public OnduHoneyExtractMecanicalRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("MechanicalExtractor", Localizer.DoStr("Mechanical Extractor"), new IngredientElement[3]
        {
          new IngredientElement(typeof (IronBarItem), 5f, typeof (MechanicsSkill), typeof (MechanicsLavishResourcesTalent)),
          new IngredientElement(typeof (OnduFrameItem), 1f, false),
          new IngredientElement(typeof (IronPipeItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduHoneyExtractMecanicalItem>(1f)
        })
      });
      this.ExperienceOnCraft = 2.0f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(1000f, typeof (MechanicsSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduHoneyExtractMecanicalRecipe), 6f, typeof (MechanicsSkill), new Type[2]
      {
        typeof (MechanicsFocusedSpeedTalent),
        typeof (MechanicsParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Mechanical Extractor"), typeof (OnduHoneyExtractMecanicalRecipe));
      CraftingComponent.AddRecipe(typeof (MachinistTableObject), (RecipeFamily) this);
    }
  }
}
