// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFruitCakeRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
  [RequiresSkill(typeof (AdvancedBakingSkill), 6)]
  public class OnduFruitCakeRecipe : RecipeFamily
  {
    public OnduFruitCakeRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("FruitCake", Localizer.DoStr("Fruit Cake"), new IngredientElement[6]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement("Fat", 2f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement("Fruit", 4f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement(typeof (HuckleberryExtractItem), 1f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement(typeof (FlourItem), 2f, typeof (AdvancedBakingSkill), (Type) null),
          new IngredientElement(typeof (YeastItem), 1f, typeof (AdvancedBakingSkill), (Type) null)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduFruitCakeItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(20f, typeof (AdvancedBakingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduFruitCakeRecipe), 4f, typeof (AdvancedBakingSkill), new Type[2]
      {
        typeof (AdvancedBakingFocusedSpeedTalent),
        typeof (AdvancedBakingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Fruit Cake"), typeof (OnduFruitCakeRecipe));
      CraftingComponent.AddRecipe(typeof (BakeryOvenObject), (RecipeFamily) this);
    }
  }
}
