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
    [LocDisplayName("Skewer Meat and Honey")]
    [Weight(200)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("Skewer of meat with honey.")]
    public partial class SkewerMeatAndHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Skewer Meat and Honey");
        public override float Calories => 1050;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 14, Fat = 19, Protein = 9, Vitamins = 4 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(CookingSkill), 3)]
    [Ecopedia("Food", "Cooking", subPageName: "Skewer Meat and Honey Item")]
    public partial class SkewerMeatAndHoneyRecipe : RecipeFamily
    {
        public SkewerMeatAndHoneyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SkewerMeatAndHoney",
                displayName: Localizer.DoStr("Skewer Meat and Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(CookingSkill)),
                    new IngredientElement(typeof(MeatStockItem), 1, false),
                    new IngredientElement(typeof(PreparedMeatItem), 2, typeof(CookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SkewerMeatAndHoneyItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(25, typeof(CookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SkewerMeatAndHoneyRecipe),
                start: 4f,
                skillType: typeof(CookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Skewer Meat and Honey"), recipeType: typeof(SkewerMeatAndHoneyRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(CastIronStoveObject), recipeFamily: this);
        }
    }
}