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
    [LocDisplayName("Honey Vinegar")]
    [Weight(300)]
    [Ecopedia("Food", "Ingredients", createAsSubPage: true)]
    [LocDescription("A sweet and tangy vinegar made from fermented honey. Perfect for dressings and marinades.")]
    public partial class HoneyVinegarItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Honey Vinegar");
        public override float Calories => 350;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 8, Fat = 0, Protein = 0, Vitamins = 6 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(168); // 7 days shelf life
    }

    [RequiresSkill(typeof(CookingSkill), 1)]
    [Ecopedia("Food", "Ingredients", subPageName: "Honey Vinegar Item")]
    public partial class HoneyVinegarRecipe : RecipeFamily
    {
        public HoneyVinegarRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneyVinegar",
                displayName: Localizer.DoStr("Honey Vinegar"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(CookingSkill)),
                    new IngredientElement(typeof(YeastItem), 1, typeof(CookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<HoneyVinegarItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(CookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(HoneyVinegarRecipe),
                start: 8f,
                skillType: typeof(CookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Honey Vinegar"), recipeType: typeof(HoneyVinegarRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(CastIronStoveObject), recipeFamily: this);
        }
    }
}