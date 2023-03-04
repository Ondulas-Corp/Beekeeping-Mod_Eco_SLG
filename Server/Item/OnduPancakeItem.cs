// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPancakeItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

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

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Pancake")]
    [Weight(200)]
    public class OnduPancakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Pancake");
        public override LocString DisplayDescription => Localizer.DoStr("Breakfast only.");
        public override float Calories => 1150;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 21, Fat = 17, Protein = 8, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BakingSkill), 5)]
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
                    new IngredientElement(typeof(SugarItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 2, typeof(BakingSkill)),
                    new IngredientElement("Fat", 2, typeof(BakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduPancakeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BakingSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduPancakeRecipe),
                start: 4f,
                skillType: typeof(BakingSkill),
                typeof(BakingFocusedSpeedTalent),
                typeof(BakingParallelSpeedTalent));

            this.Initialize(displayText: Localizer.DoStr("Pancake"), recipeType: typeof(OnduPancakeRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
