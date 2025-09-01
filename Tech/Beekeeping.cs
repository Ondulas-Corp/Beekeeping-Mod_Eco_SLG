namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;

    /// <summary>Auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization.</summary>
    [Serialized]
    [LocDisplayName("Beekeeping")]
    [LocDescription("A skill for the breeding of bees and honey production. Levels up by crafting related beekeeping recipes.")]
    [Ecopedia("Professions", "Farmer", createAsSubPage: true)]
    [RequiresSkill(typeof(FarmerSkill), 0), Tag("Farmer Specialty")]
    [Tag("Specialty")]
    [Tag("Teachable")]
    public partial class BeekeepingSkill : Skill
    {

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static MultiplicativeStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new float[] { 
                1,
                1 - 0.5f,
                1 - 0.55f,
                1 - 0.6f,
                1 - 0.65f,
                1 - 0.7f,
                1 - 0.75f,
                1 - 0.8f,
            });
        public override MultiplicativeStrategy MultiStrategy => MultiplicativeStrategy;

        public static AdditiveStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 
                0,
                0.2f,
                0.25f,
                0.3f,
                0.35f,
                0.4f,
                0.45f,
                0.5f,
            });
        public override AdditiveStrategy AddStrategy => AdditiveStrategy;
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 1; } }
    }

    [Serialized]
    [Weight(1000)]
    [LocDisplayName("Beekeeping Skill Book")]
    [Ecopedia("Items", "Skill Books", createAsSubPage: true)]
    public partial class BeekeepingSkillBook : SkillBook<BeekeepingSkill, BeekeepingSkillScroll> {}

    [Serialized]
    [Weight(100)]
    [LocDisplayName("Beekeeping Skill Scroll")]
    public partial class BeekeepingSkillScroll : SkillScroll<BeekeepingSkill, BeekeepingSkillBook> {}


    /// <summary>
    /// <para>Server side recipe definition for "Beekeeping".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    [RequiresSkill(typeof(GatheringSkill), 1)]
    [Ecopedia("Professions", "Farmer", subPageName: "Beekeeping Skill Book Item")]
    public partial class BeekeepingSkillBookRecipe : RecipeFamily
    {
        public BeekeepingSkillBookRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Beekeeping",  //noloc
                displayName: Localizer.DoStr("Beekeeping Skill Book"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GatheringResearchPaperBasicItem), 2, typeof(GatheringSkill)),
                    new IngredientElement(typeof(DendrologyResearchPaperBasicItem), 5, typeof(GatheringSkill)),
                    new IngredientElement("Basic Research", 5, typeof(GatheringSkill)), //noloc
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeekeepingSkillBook>()
                });
            this.Recipes = new List<Recipe> { recipe };
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(2000, typeof(GatheringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeekeepingSkillBookRecipe), start: 15, skillType: typeof(GatheringSkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Beekeeping Skill Book"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Beekeeping Skill Book"), recipeType: typeof(BeekeepingSkillBookRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}