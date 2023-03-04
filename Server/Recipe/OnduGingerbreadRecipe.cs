// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduGingerbreadRecipe
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
    [RequiresSkill(typeof(AdvancedBakingSkill), 5)]
    public class OnduGingerbreadRecipe : RecipeFamily
    {
        public OnduGingerbreadRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("Gingerbread", Localizer.DoStr("Gingerbread"), new IngredientElement[5]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (AdvancedBakingSkill),  null),
          new IngredientElement("Fat", 1f, typeof (AdvancedBakingSkill),  null),
          new IngredientElement(typeof (SimpleSyrupItem), 2f, typeof (AdvancedBakingSkill),  null),
          new IngredientElement(typeof (YeastItem), 1f, typeof (AdvancedBakingSkill),  null),
          new IngredientElement(typeof (FlourItem), 3f, typeof (AdvancedBakingSkill),  null)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduGingerbreadItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(20f, typeof(AdvancedBakingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduGingerbreadRecipe), 3f, typeof(AdvancedBakingSkill), new Type[2]
            {
        typeof (AdvancedBakingFocusedSpeedTalent),
        typeof (AdvancedBakingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Gingerbread"), typeof(OnduGingerbreadRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}
