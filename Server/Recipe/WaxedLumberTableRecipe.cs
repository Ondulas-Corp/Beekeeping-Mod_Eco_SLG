// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedLumberTableRecipe
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
    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedLumberTableRecipe : RecipeFamily
    {
        public WaxedLumberTableRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedLumberTable", Localizer.DoStr("Waxed Lumber Table"), new IngredientElement[2]
        {
          new IngredientElement(typeof (LumberTableItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedLumberTableItem>()
        })
      };
            ExperienceOnCraft = 3.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedLumberTableRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Lumber Table"), typeof(WaxedLumberTableRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
