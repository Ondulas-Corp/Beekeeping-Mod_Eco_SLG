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
    [LocDisplayName("Gingerbread")]
    [Weight(400)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    [LocDescription("Tight and melting.")]
    public partial class GingerbreadItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Gingerbreads");
        public override float Calories => 1200;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 23, Fat = 16, Protein = 8, Vitamins = 15 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 5)]
    [Ecopedia("Food", "Baking", subPageName: "Gingerbread Item")]
    public partial class GingerbreadRecipe : RecipeFamily
    {
        public GingerbreadRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Gingerbread",
                displayName: Localizer.DoStr("Gingerbread"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fat", 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(SimpleSyrupItem), 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(FlourItem), 6, typeof(AdvancedBakingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GingerbreadItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedBakingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GingerbreadRecipe),
                start: 3f,
                skillType: typeof(AdvancedBakingSkill));
            this.Initialize(displayText: Localizer.DoStr("Gingerbread"), recipeType: typeof(GingerbreadRecipe));
            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipeFamily: this);
        }
    }
}