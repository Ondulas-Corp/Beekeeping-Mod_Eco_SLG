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
    [LocDisplayName("Salmon Honey")]
    [Weight(500)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("Salmon with honey.")]
    public partial class SalmonHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Salmon Honey");
        public override float Calories => 1400;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 9, Fat = 25, Protein = 8, Vitamins = 20 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 7)]
    [Ecopedia("Food", "Cooking", subPageName: "Salmon Honey Item")]
    public partial class SalmonHoneyRecipe : RecipeFamily
    {
        public SalmonHoneyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SalmonHoney",
                displayName: Localizer.DoStr("Salmon Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(InfusedOilItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(VegetableMedleyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SalmonItem), 2, typeof(AdvancedCookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SalmonHoneyItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SalmonHoneyRecipe),
                start: 5f,
                skillType: typeof(AdvancedCookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Salmon Honey"), recipeType: typeof(SalmonHoneyRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipeFamily: this);
        }
    }
}