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
    /// <summary>
    /// Alternative recipe for Simple Syrup using honey instead of sugar.
    /// </summary>
    [RequiresSkill(typeof(MillingSkill), 2)]
    [Ecopedia("Food", "Ingredients", subPageName: "Simple Syrup from Honey")]
    public partial class SimpleSyrupFromHoneyRecipe : RecipeFamily
    {
        public SimpleSyrupFromHoneyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SimpleSyrupFromHoney",
                displayName: Localizer.DoStr("Simple Syrup from Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HoneyItem), 2, typeof(MillingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SimpleSyrupItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(MillingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SimpleSyrupFromHoneyRecipe),
                start: 3f,
                skillType: typeof(MillingSkill));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Simple Syrup from Honey"), recipeType: typeof(SimpleSyrupFromHoneyRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(MillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}