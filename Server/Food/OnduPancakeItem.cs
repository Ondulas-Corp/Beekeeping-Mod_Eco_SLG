// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPancakeItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server.Food
{
    [Serialized]
    [LocDisplayName("Pancake")]
    [Weight(200)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    public class OnduPancakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Pancake");
        public override LocString DisplayDescription => Localizer.DoStr("Breakfast only.");
        public override float Calories => 900;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 20, Fat = 8, Protein = 15, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BakingSkill), 5)]
    [Ecopedia("Food", "Baking", subPageName: "Pancake Item")]
    public class OnduPancakeRecipe : RecipeFamily
    {
        public OnduPancakeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pancake",
                displayName: Localizer.DoStr("Pancake"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(SugarItem), 2, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 4, typeof(BakingSkill)),
                    new IngredientElement("Fat", 4, typeof(BakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduPancakeItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BakingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduPancakeRecipe),
                start: 4f,
                skillType: typeof(BakingSkill),
                typeof(BakingFocusedSpeedTalent),
                typeof(BakingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Pancake"), recipeType: typeof(OnduPancakeRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
