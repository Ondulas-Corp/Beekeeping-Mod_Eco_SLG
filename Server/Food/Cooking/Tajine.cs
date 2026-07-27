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
    [LocDisplayName("Tajine")]
    [Weight(450)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("Traditional Tagine.")]
    public partial class TajineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Tajine");
        public override float Calories => 1300;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 11, Fat = 19, Protein = 20, Vitamins = 12 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 6)]
    [Ecopedia("Food", "Cooking", subPageName: "Tajine Item")]
    public partial class TajineRecipe : RecipeFamily
    {
        public TajineRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Tajine",
                displayName: Localizer.DoStr("Tajine"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(InfusedOilItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SimmeredMeatItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(CornmealItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(VegetableMedleyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(BoiledRiceItem), 1, typeof(AdvancedCookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<TajineItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(TajineRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Tajine"), recipeType: typeof(TajineRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipeFamily: this);
        }
    }
}