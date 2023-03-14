using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Beekeeping Skill")]
    [RequiresSkill(typeof(FarmerSkill), 0)]
    [Tag("Farmer Specialty", 1)]
    [Eco.Gameplay.Items.Tier(1f, true)]
    [Tag("Specialty", 1)]
    [Tag("Teachable", 1)]
    public class BeekeepingSkill : Skill
    {
        public static MultiplicativeStrategy MultiplicativeStrategy = new MultiplicativeStrategy(new float[8]
        {
            1f,
            0.5f,
            0.45f,
            0.4f,
            0.35f,
            0.3f,
            0.25f,
            0.2f
        });
        public static AdditiveStrategy AdditiveStrategy = new AdditiveStrategy(new float[8]
        {
            0.0f,
            0.2f,
            0.25f,
            0.3f,
            0.35f,
            0.4f,
            0.45f,
            0.5f
        });

        public override LocString DisplayDescription => Localizer.DoStr("A skill for the breeding of bees.");

        public override void OnLevelUp(User user) => user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20f, Localizer.DoStr("for leveling up another specialization."));

        public override MultiplicativeStrategy MultiStrategy => MultiplicativeStrategy;

        public override AdditiveStrategy AddStrategy => AdditiveStrategy;

        public override int MaxLevel => 7;

        public override int Tier => 1;

        [Serialized]
        [LocDisplayName("Beekeeping Skill Book")]
        public class BeekeepingSkillBook : SkillBook<BeekeepingSkill, BeekeepingSkillScroll> { }

        [Serialized]
        [LocDisplayName("Beekeeping Skill Scroll")]
        public class BeekeepingSkillScroll : SkillScroll<BeekeepingSkill, BeekeepingSkillBook> { }

        public class BeekeepingSkillBookRecipe : RecipeFamily
        {
            public BeekeepingSkillBookRecipe()
            {
                var recipe = new Recipe();
                recipe.Init(
                    name: "BeekeepingSkillBook",
                    displayName: Localizer.DoStr("Beekeeping Skill Book"),
                    ingredients: new List<IngredientElement>
                    {
                        new IngredientElement(typeof (GatheringResearchPaperBasicItem), 2f, false),
                        new IngredientElement(typeof (DendrologyResearchPaperBasicItem), 5f, false),
                        new IngredientElement("Basic Research", 5f, false)
                    },

                    items: new List<CraftingElement>
                    {
                    new CraftingElement<BeekeepingSkillBook>()
                    });

                Recipes = new List<Recipe> { recipe };

                LaborInCalories = CreateLaborInCaloriesValue(2000);
                CraftMinutes = CreateCraftTimeValue(15f);

                Initialize(displayText: Localizer.DoStr("Blueberry Cocktail"), recipeType: typeof(BeekeepingSkillBookRecipe));
                CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipe: this);
            }
        }
    }
}
