// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxSheet2Recipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduWaxSheet2Recipe : RecipeFamily
    {
        public OnduWaxSheet2Recipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("WaxSheet", Localizer.DoStr("Wax Sheet"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduWaxItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduWaxSheetItem>(2f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(60f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduWaxSheet2Recipe), 0.5f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Wax Sheet"), typeof(OnduWaxSheet2Recipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}
