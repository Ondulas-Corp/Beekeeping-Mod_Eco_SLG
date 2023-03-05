// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPurifiedHoneyItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Benefit;
using Beekeeping.Server.Object;
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
    [LocDisplayName("Purified honey")]
    [Weight(100)]
    public class OnduPurifiedHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Purified honey");
        public override LocString DisplayDescription => Localizer.DoStr("Concentrated honey, ready-to-eat.");
        public override float Calories => 350;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 9, Fat = 0, Protein = 0, Vitamins = 3 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduPurifiedHoneyRecipe : RecipeFamily
    {
        public OnduPurifiedHoneyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "PurifiedHoney",
                displayName: Localizer.DoStr("Purified Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduFullPotItem), 1, typeof(BakingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduPurifiedHoneyItem>(),
                    new CraftingElement<OnduEmptyPotItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BeekeepingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduPurifiedHoneyRecipe),
                start: 5f,
                skillType: typeof(BeekeepingSkill),
                typeof(BeekeepingFocusedSpeedTalent),
                typeof(BeekeepingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Purified Honey"), recipeType: typeof(OnduPancakeRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(OnduHoneyExtractObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduPurifiedHoneyRecipe2 : RecipeFamily
    {
        public OnduPurifiedHoneyRecipe2()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "PurifiedHoney",
                displayName: Localizer.DoStr("Purified Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduFullPotItem), 1, typeof(BakingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduPurifiedHoneyItem>(2),
                    new CraftingElement<OnduEmptyPotItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BeekeepingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduPurifiedHoneyRecipe),
                start: 5f,
                skillType: typeof(BeekeepingSkill),
                typeof(BeekeepingFocusedSpeedTalent),
                typeof(BeekeepingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Purified Honey"), recipeType: typeof(OnduPurifiedHoneyRecipe2));

            CraftingComponent.AddRecipe(tableType: typeof(OnduHoneyExtractMecanicalObject), recipe: this);
        }
    }
}
