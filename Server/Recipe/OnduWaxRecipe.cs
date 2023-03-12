// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxRecipe
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
    [RequiresSkill(typeof(SmeltingSkill), 0)]
    public class OnduWaxRecipe : RecipeFamily
    {
        public OnduWaxRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("Wax", Localizer.DoStr("Wax"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduWaxFrameItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduWaxItem>(2f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(30f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduWaxRecipe), 0.5f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Wax"), typeof(OnduWaxRecipe));
            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }
}
