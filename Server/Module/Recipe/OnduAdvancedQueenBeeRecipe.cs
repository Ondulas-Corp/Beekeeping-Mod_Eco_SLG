using Beekeeping.Server.Module;
using Beekeeping.Server.Object;
using Beekeeping.Server.Skill.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server.Module.Recipe
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduAdvancedQueenBeeRecipe : RecipeFamily
    {
        public OnduAdvancedQueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("AdvancedQueenBee", Localizer.DoStr("Advanced Queen Bee"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduBasicQueenBeeItem), 1f, false),
          new IngredientElement(typeof (OnduRoyalJellyItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<OnduAdvancedQueenBeeItem>(1f)
        })
      };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(150f, typeof(BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(OnduAdvancedQueenBeeRecipe), 180f, typeof(BeekeepingSkill), new Type[2]
            {
        typeof (BeekeepingFocusedSpeedTalent),
        typeof (BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Advanced Queen Bee"), typeof(OnduAdvancedQueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(OnduSmallHiveObject), this);
        }
    }
}
