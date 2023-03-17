// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFruitCakeItem
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
    [LocDisplayName("Fruit Cake")]
    [Weight(450)]
    [Ecopedia("Food", "Baking", createAsSubPage: true)]
    public class OnduFruitCakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Fruit Cake");
        public override LocString DisplayDescription => Localizer.DoStr("Happy Birthday!");
        public override float Calories => 1300;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 17, Fat = 5, Protein = 11, Vitamins = 29 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 6)]
    [Ecopedia("Food", "Baking", subPageName: "FruitCake Item")]
    public class OnduFruitCakeRecipe : RecipeFamily
    {
        public OnduFruitCakeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FruitCake",
                displayName: Localizer.DoStr("Fruit Cake"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fat", 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fruit", 12, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(HuckleberryExtractItem), 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(FlourItem), 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedBakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduFruitCakeItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedBakingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduFruitCakeRecipe),
                start: 20f,
                skillType: typeof(AdvancedBakingSkill),
                typeof(AdvancedBakingFocusedSpeedTalent),
                typeof(AdvancedBakingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Fruit Cake"), recipeType: typeof(OnduFruitCakeRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
