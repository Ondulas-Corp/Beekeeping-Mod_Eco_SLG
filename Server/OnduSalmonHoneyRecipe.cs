

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(AdvancedCookingSkill), 7)]
    public class OnduSalmonHoneyRecipe : RecipeFamily
    {
        public OnduSalmonHoneyRecipe()
        {
            this.Recipes = (new List<Recipe>()
      {
        new Recipe("SalmonHoney", Localizer.DoStr("Salmon Honey"), new IngredientElement[4]
        {
          new IngredientElement(typeof (OnduPurifiedHoneyItem), 2f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (InfusedOilItem), 1f, typeof (AdvancedCookingSkill), (Type) null),
          new IngredientElement(typeof (VegetableMedleyItem), 2f, typeof (AdvancedCookingSkill), (Type) null),
        }, new CraftingElement[1]
        {
          (CraftingElement) new CraftingElement<OnduSalmonHoneyItem>(1f)
        })
      });
            this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(20f, typeof(AdvancedCookingSkill)));
            this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(typeof(OnduSalmonHoneyRecipe), 5f, typeof(AdvancedCookingSkill), new Type[2]
            {
        typeof (AdvancedCookingFocusedSpeedTalent),
        typeof (AdvancedCookingParallelSpeedTalent)
            }));
            this.Initialize(Localizer.DoStr("Salmon Honey"), typeof(OnduSalmonHoneyRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), (RecipeFamily)this);
        }
    }
}
    

