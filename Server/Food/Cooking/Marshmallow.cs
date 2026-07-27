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
    [LocDisplayName("Marshmallow")]
    [Weight(100)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [LocDescription("The best food to share around a wood fire.")]
    public partial class MarshmallowItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Marshmallows");
        public override float Calories => 950;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 18, Fat = 7, Protein = 4, Vitamins = 15 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(CookingSkill), 3)]
    [Ecopedia("Food", "Cooking", subPageName: "Marshmallow Item")]
    public partial class MarshmallowRecipe : RecipeFamily
    {
        public MarshmallowRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Marshmallow",
                displayName: Localizer.DoStr("Marshmallow"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(CookingSkill)),
                    new IngredientElement(typeof(SugarItem), 2, typeof(CookingSkill)),
                    new IngredientElement(typeof(FlourItem), 1, typeof(CookingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<MarshmallowItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(25, typeof(CookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(MarshmallowRecipe),
                start: 4f,
                skillType: typeof(CookingSkill));
            this.Initialize(displayText: Localizer.DoStr("Marshmallow"), recipeType: typeof(MarshmallowRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(CastIronStoveObject), recipeFamily: this);
        }
    }
}