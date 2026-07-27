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
    [LocDisplayName("Fruit Cake")]
    [Weight(450)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    [LocDescription("Happy Birthday!")]
    public partial class FruitCakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Fruit Cakes");
        public override float Calories => 1300;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 17, Fat = 5, Protein = 11, Vitamins = 29 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 6)]
    [Ecopedia("Food", "Baking", subPageName: "Fruit Cake Item")]
    public partial class FruitCakeRecipe : RecipeFamily
    {
        public FruitCakeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FruitCake",
                displayName: Localizer.DoStr("Fruit Cake"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fat", 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fruit", 12, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(HuckleberryExtractItem), 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(FlourItem), 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedBakingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<FruitCakeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedBakingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(FruitCakeRecipe),
                start: 20f,
                skillType: typeof(AdvancedBakingSkill));
            this.Initialize(displayText: Localizer.DoStr("Fruit Cake"), recipeType: typeof(FruitCakeRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipeFamily: this);
        }
    }
}