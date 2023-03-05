// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedShelfCabinetRecipe
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
    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedShelfCabinetRecipe : RecipeFamily
    {
        public WaxedShelfCabinetRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedShelfCabinet", Localizer.DoStr("Waxed Shelf Cabinet"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduWaxItem), 2f, false),
          new IngredientElement(typeof (ShelfCabinetItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedShelfCabinetItem>(1f)
        })
      };
            ExperienceOnCraft = 5.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedShelfCabinetRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Shelf Cabinet"), typeof(WaxedShelfCabinetRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
