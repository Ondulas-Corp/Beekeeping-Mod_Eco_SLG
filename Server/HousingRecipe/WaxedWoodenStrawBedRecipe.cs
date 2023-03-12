// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenStrawBedRecipe
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
    public class WaxedWoodenStrawBedRecipe : RecipeFamily
    {
        public WaxedWoodenStrawBedRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenStrawBed", Localizer.DoStr("Waxed Wooden Straw Bed"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenStrawBedItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenStrawBedItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenStrawBedRecipe), 2f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Straw Bed"), typeof(WaxedWoodenStrawBedRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
