// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.BeekeeperHatRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Mods.TechTree;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(TailoringSkill), 1)]
    public class BeekeeperHatRecipe : RecipeFamily
    {
        public BeekeeperHatRecipe()
        {
            this.Recipes = new List<Recipe>()
              {
                new Recipe("BeekeeperHat", Localizer.DoStr("Beekeeper Hat"), new IngredientElement[2]
                {
                  new IngredientElement(typeof (LeatherHideItem), 2f, typeof (TailoringSkill), typeof (TailoringLavishResourcesTalent)),
                  new IngredientElement(typeof (ClothItem), 2f, typeof (TailoringSkill), typeof (TailoringLavishResourcesTalent))
                }, new CraftingElement[1]
                {
                  (CraftingElement) new CraftingElement<OnduBeekeeperHatItem>(1f)
                }
                )
            };
            this.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(60f, typeof(TailoringSkill));
            this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof(BeekeeperHatRecipe), 10f, typeof(TailoringSkill), new Type[2]
            {
        typeof (TailoringFocusedSpeedTalent),
        typeof (TailoringParallelSpeedTalent)
            }));
            this.Initialize(Localizer.DoStr("Beekeeper Hat"), typeof(BeekeeperHatRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), (RecipeFamily)this);
        }
    }
}
