// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduQueenBeeRecipe
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

namespace Beekeeping.Server.Module
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduQueenBeeRecipe : RecipeFamily
    {
        public OnduQueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("QueenBee", Localizer.DoStr("Queen Bee"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduRoyalJellyItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduQueenBeeItem>(1f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(150f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduQueenBeeRecipe), 60f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Queen Bee"), typeof(OnduQueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(OnduSmallHiveObject), this);
        }
    }
}
