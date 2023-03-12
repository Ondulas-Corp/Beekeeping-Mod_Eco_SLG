using Beekeeping.Server.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduFullPotRecipe : RecipeFamily
    {
        public OnduFullPotRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("HoneyPot", Localizer.DoStr("Honey Pot"), new IngredientElement[1]
        {
          new IngredientElement(typeof (OnduEmptyPotItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduFullPotItem>(1f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(30f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduFullPotRecipe), 120f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Honey Pot"), typeof(OnduFullPotRecipe));
            CraftingComponent.AddRecipe(typeof(OnduBreedingBeehiveObject), this);
        }
    }
}
