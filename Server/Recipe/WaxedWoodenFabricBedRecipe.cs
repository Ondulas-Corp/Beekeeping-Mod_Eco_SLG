// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenFabricBedRecipe
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
    public class WaxedWoodenFabricBedRecipe : RecipeFamily
    {
        public WaxedWoodenFabricBedRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenFabricBed", Localizer.DoStr("Waxed Wooden Fabric Bed"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenFabricBedItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 3f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenFabricBedItem>(1f)
        })
      };
            ExperienceOnCraft = 4.0f;
            LaborInCalories = CreateLaborInCaloriesValue(300f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenFabricBedRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Fabric Bed"), typeof(WaxedWoodenFabricBedRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
