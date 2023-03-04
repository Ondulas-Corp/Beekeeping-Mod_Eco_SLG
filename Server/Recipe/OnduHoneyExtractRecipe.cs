// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public class OnduHoneyExtractRecipe : RecipeFamily
    {
        public OnduHoneyExtractRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("ManualExtractor", Localizer.DoStr("Manual Extractor"), new IngredientElement[2]
        {
          new IngredientElement(typeof (IronBarItem), 5f, typeof (SmeltingSkill), typeof (SmeltingLavishResourcesTalent)),
          new IngredientElement(typeof (IronPipeItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduHoneyExtractItem>(1f)
        })
      };
            ExperienceOnCraft = 2.0f;
            LaborInCalories = CreateLaborInCaloriesValue(800f, typeof(SmeltingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduHoneyExtractRecipe), 5f, typeof(SmeltingSkill), new Type[2]
            {
        typeof (SmeltingFocusedSpeedTalent),
        typeof (SmeltingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Manual Extractor"), typeof(OnduHoneyExtractRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}
