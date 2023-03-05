// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSalmonHoneyItem
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
    [LocDisplayName("Salmon Honey")]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [Weight(500)]
    public class OnduSalmonHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Salmon Honey");
        public override LocString DisplayDescription => Localizer.DoStr("Salmon with honey");
        public override float Calories => 1400;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 9, Fat = 25, Protein = 8, Vitamins = 20 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 7)]
    public class OnduSalmonHoneyRecipe : RecipeFamily
    {
        public OnduSalmonHoneyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SalmonHoney",
                displayName: Localizer.DoStr("Salmon Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(InfusedOilItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(VegetableMedleyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SalmonItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(InfusedOilItem), 1, typeof(AdvancedCookingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduSalmonHoneyItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedCookingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduSalmonHoneyRecipe),
                start: 5f,
                skillType: typeof(AdvancedCookingSkill),
                typeof(AdvancedCookingFocusedSpeedTalent),
                typeof(AdvancedCookingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Salmon Honey"), recipeType: typeof(OnduSalmonHoneyRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipe: this);
        }
    }
}
