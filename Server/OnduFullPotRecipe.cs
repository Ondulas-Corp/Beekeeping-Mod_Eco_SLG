using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
  [RequiresSkill(typeof (BeekeepingSkill), 1)]
  public class OnduFullPotRecipe : RecipeFamily
  {
    public OnduFullPotRecipe()
    {
      this.Recipes = (new List<Recipe>()
      {
        new Recipe("HoneyPot", Localizer.DoStr("Honey Pot"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduEmptyPotItem), 1f, false)
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduFullPotItem>(1f)
        })
      });
      this.ExperienceOnCraft = 0.5f;
      this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(30f, typeof (BeekeepingSkill)));
      this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof (OnduFullPotRecipe), 240f, typeof (BeekeepingSkill), new Type[2]
      {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
      }));
            this.Initialize(Localizer.DoStr("Honey Pot"), typeof (OnduFullPotRecipe));
      CraftingComponent.AddRecipe(typeof (OnduBreedingBeehiveObject), (RecipeFamily) this);
    }
  }
}
