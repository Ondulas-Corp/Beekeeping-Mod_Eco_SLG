
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    public class BeekeepingSkillBookRecipe : RecipeFamily
    {
        public BeekeepingSkillBookRecipe()
        {
            this.Recipes = new List<Recipe>()
      {
        new Recipe("Beekeeping Skill Book", Localizer.DoStr("Beekeeping Skill Book"), new IngredientElement[3]
        {
          new IngredientElement(typeof (GatheringResearchPaperBasicItem), 2f, false),
          new IngredientElement(typeof (DendrologyResearchPaperBasicItem), 5f, false),
          new IngredientElement("Basic Research", 5f, false)
        }, new CraftingElement[2]
        {
          (CraftingElement) new CraftingElement<BeekeepingSkillBook>(1f),
        })
      };
            this.LaborInCalories = (RecipeFamily.CreateLaborInCaloriesValue(2000f));
            this.CraftMinutes = (RecipeFamily.CreateCraftTimeValue(15f));
            this.Initialize(Localizer.DoStr("Beekeeping Skill Book"), typeof(BeekeepingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), (RecipeFamily)this);
        }
    }
}
