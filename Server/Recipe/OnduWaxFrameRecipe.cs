// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxFrameRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduWaxFrameRecipe : RecipeFamily
    {
        public OnduWaxFrameRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("WaxFrame", Localizer.DoStr("Wax Frame"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduFrameItem), 1f, false),
          new IngredientElement(typeof (OnduWaxSheetItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduWaxFrameItem>(1f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(40f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduWaxFrameRecipe), 240f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Wax Frame"), typeof(OnduWaxFrameRecipe));
            CraftingComponent.AddRecipe(typeof(OnduBreedingBeehiveObject), this);
        }
    }
}
