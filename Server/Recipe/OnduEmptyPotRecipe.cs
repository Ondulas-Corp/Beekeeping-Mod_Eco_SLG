// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduEmptyPotRecipe
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
    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public class OnduEmptyPotRecipe : RecipeFamily
    {
        public OnduEmptyPotRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("EmptyPot", Localizer.DoStr("Empty Pot"), new IngredientElement[1]
        {
          new IngredientElement(typeof (GlassItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduEmptyPotItem>(1f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(GlassworkingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduEmptyPotRecipe), 1f, typeof(GlassworkingSkill), new Type[2]
            {
        typeof (GlassworkingFocusedSpeedTalent),
        typeof (GlassworkingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Empty Pot"), typeof(OnduEmptyPotRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}
