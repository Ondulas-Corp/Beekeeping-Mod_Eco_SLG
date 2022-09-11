// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMarshmallowRecipe
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
  [RequiresSkill(typeof (CookingSkill), 3)]
  public class OnduMarshmallowRecipe : RecipeFamily
  {
    public OnduMarshmallowRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("Marshmallow", Localizer.DoStr("Marshmallow"), new IngredientElement[3]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 1f, typeof (CookingSkill), (Type) null),
          new IngredientElement(typeof (SugarItem), 2f, typeof (CookingSkill), (Type) null),
          new IngredientElement(typeof (FlourItem), 1f, typeof (CookingSkill), (Type) null)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduMarshmallowItem>(1f)
        })
      });
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(40f, typeof (CookingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduMarshmallowRecipe), 4f, typeof (CookingSkill), new Type[2]
      {
        typeof (CookingFocusedSpeedTalent),
        typeof (CookingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Marshmallow"), typeof (OnduMarshmallowRecipe));
      CraftingComponent.AddRecipe(typeof (CastIronStoveObject), (RecipeFamily) this);
    }
  }
}
