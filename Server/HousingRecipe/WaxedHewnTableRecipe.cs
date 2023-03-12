// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedHewnTableRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.HousingItem;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server.HousingRecipe
{
    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedHewnTableRecipe : RecipeFamily
    {
        public WaxedHewnTableRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedHewnTable", Localizer.DoStr("Waxed Hewn Table"), new IngredientElement[2]
        {
          new IngredientElement(typeof (HewnTableItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedHewnTableItem>(1f)
        })
      };
            ExperienceOnCraft = 2.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedHewnTableRecipe), 4f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Hewn Table"), typeof(WaxedHewnTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
