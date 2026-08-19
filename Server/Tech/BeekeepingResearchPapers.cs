// Adds alternate Beekeeping-themed crafting routes for existing vanilla research paper items.
// These are standalone RecipeFamily registrations that output the same vanilla item type,
// registered at the same table — they appear as alternate recipes alongside the vanilla one.

namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Components;
    using Eco.Shared.Localization;
    using Beekeeping.Server;

    public partial class GatheringResearchPaperBasicFromBeesRecipe : RecipeFamily
    {
        public GatheringResearchPaperBasicFromBeesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GatheringResearchPaperBasicFromBees", //noloc
                displayName: Localizer.DoStr("Papier de recherche : Récolte basique à base de miel"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Petals", 20, typeof(BeekeepingSkill)), //noloc
                    new IngredientElement(typeof(BeePollenItem), 15, typeof(BeekeepingSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GatheringResearchPaperBasicItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Papier de recherche : Récolte basique à base de miel"), recipeType: typeof(GatheringResearchPaperBasicFromBeesRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    public partial class AgricultureResearchPaperAdvancedFromBeesRecipe : RecipeFamily
    {
        public AgricultureResearchPaperAdvancedFromBeesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AgricultureResearchPaperAdvancedFromBees", //noloc
                displayName: Localizer.DoStr("Papier de recherche : Agriculture avancée à base de miel"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 20, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(ForagerBeeItem), 5, typeof(BeekeepingSkill)),
                    new IngredientElement("Petals", 20, typeof(BeekeepingSkill)), //noloc
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<AgricultureResearchPaperAdvancedItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3f;
            this.LaborInCalories = CreateLaborInCaloriesValue(240, typeof(BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Papier de recherche : Agriculture avancée à base de miel"), recipeType: typeof(AgricultureResearchPaperAdvancedFromBeesRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    public partial class AgricultureResearchPaperModernFromBeesRecipe : RecipeFamily
    {
        public AgricultureResearchPaperModernFromBeesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AgricultureResearchPaperModernFromBees", //noloc
                displayName: Localizer.DoStr("Papier de recherche : Agriculture moderne à base de miel"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(RoyalJellyItem), 5, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(PropolisItem), 5, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(WaxFrameItem), 5, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(BeewaxItem), 5, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(QueenBeeItem), 1, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(HoneyItem), 20, typeof(BeekeepingSkill)),
                    new IngredientElement("Raw Food", 200, typeof(BeekeepingSkill)), //noloc
                    new IngredientElement(typeof(InkItem), 4, true),
                    new IngredientElement(typeof(PaperItem), 20, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<AgricultureResearchPaperModernItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 6f;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Papier de recherche : Agriculture moderne à base de miel"), recipeType: typeof(AgricultureResearchPaperModernFromBeesRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
