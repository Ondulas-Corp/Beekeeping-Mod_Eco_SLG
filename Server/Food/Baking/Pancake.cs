using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Time;
using Beekeeping.Server;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Pancake")]
    [Weight(200)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    [LocDescription("Breakfast only.")]
    public partial class PancakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Pancakes");
        public override float Calories => 900;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 20, Fat = 8, Protein = 15, Vitamins = 4 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BakingSkill), 5)]
    [Ecopedia("Food", "Baking", subPageName: "Pancake Item")]
    public partial class PancakeRecipe : RecipeFamily
    {
        public PancakeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pancake",
                displayName: Localizer.DoStr("Pancake"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(SugarItem), 2, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 4, typeof(BakingSkill)),
                    new IngredientElement("Fat", 4, typeof(BakingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<PancakeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BakingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(PancakeRecipe),
                start: 4f,
                skillType: typeof(BakingSkill));
            this.Initialize(displayText: Localizer.DoStr("Pancake"), recipeType: typeof(PancakeRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipeFamily: this);
        }
    }
}