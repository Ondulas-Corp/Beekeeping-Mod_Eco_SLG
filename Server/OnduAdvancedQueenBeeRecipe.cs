using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
  [RequiresSkill(typeof (BeekeepingSkill), 1)]
  public class OnduAdvancedQueenBeeRecipe : RecipeFamily
  {
    public OnduAdvancedQueenBeeRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("AdvancedQueenBee", Localizer.DoStr("Advanced Queen Bee"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduBasicQueenBeeItem), 1f, false),
          new IngredientElement(typeof (OnduRoyalJellyItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduAdvancedQueenBeeItem>(1f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(150f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduAdvancedQueenBeeRecipe), 180f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
      this.Initialize(Localizer.DoStr("Advanced Queen Bee"), typeof (OnduAdvancedQueenBeeRecipe));
      CraftingComponent.AddRecipe(typeof (OnduSmallHiveObject), (RecipeFamily) this);
    }
  }
}
