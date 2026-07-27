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
    [LocDisplayName("Honey Roast")]
    [Weight(400)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("A melting meat thanks to its honey...")]
    public partial class HoneyRoastItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Honey Roasts");
        public override float Calories => 1150;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 12, Fat = 20, Protein = 22, Vitamins = 4 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    [Ecopedia("Food", "Cooking", subPageName: "Honey Roast Item")]
    public partial class HoneyRoastRecipe : RecipeFamily
    {
        public HoneyRoastRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneyRoast",
                displayName: Localizer.DoStr("Honey Roast"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(BakedRoastItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(MeatStockItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SimpleSyrupItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement("Fat", 1, typeof(AdvancedCookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<HoneyRoastItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(HoneyRoastRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Honey Roast"), recipeType: typeof(HoneyRoastRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipeFamily: this);
        }
    }
}