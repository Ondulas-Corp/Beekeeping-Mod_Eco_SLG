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
    [LocDisplayName("Madeleine")]
    [Weight(150)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    [LocDescription("A French pastry.")]
    public partial class MadeleineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Madeleines");
        public override float Calories => 700;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 18, Fat = 7, Protein = 13, Vitamins = 7 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BakingSkill), 4)]
    [Ecopedia("Food", "Baking", subPageName: "Madeleine Item")]
    public partial class MadeleineRecipe : RecipeFamily
    {
        public MadeleineRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Madeleine",
                displayName: Localizer.DoStr("Madeleine"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(SugarItem), 4, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 2, typeof(BakingSkill)),
                    new IngredientElement("Fat", 2, typeof(BakingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<MadeleineItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BakingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(MadeleineRecipe),
                start: 4f,
                skillType: typeof(BakingSkill));
            this.Initialize(displayText: Localizer.DoStr("Madeleine"), recipeType: typeof(MadeleineRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipeFamily: this);
        }
    }
}