using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(AdvancedCookingSkill), 7)]
    public class OnduSalmonHoneyRecipe : RecipeFamily
    {
        public OnduSalmonHoneyRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("SalmonHoney", Localizer.DoStr("Salmon Honey"), new IngredientElement[4]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 2, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (InfusedOilItem), 1, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (VegetableMedleyItem), 2, typeof (AdvancedCookingSkill),  null),
          new IngredientElement(typeof (CharredFishItem), 2, typeof (AdvancedCookingSkill),  null),
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduSalmonHoneyItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(20f, typeof(AdvancedCookingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduSalmonHoneyRecipe), 5f, typeof(AdvancedCookingSkill), new Type[2]
            {
        typeof (AdvancedCookingFocusedSpeedTalent),
        typeof (AdvancedCookingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Salmon Honey"), typeof(OnduSalmonHoneyRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}


