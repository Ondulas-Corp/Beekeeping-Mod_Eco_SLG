// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyRoastItem
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
    [LocDisplayName("Honey Roast")]
    [Weight(400)]
    public class OnduHoneyRoastItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Honey Roast");
        public override LocString DisplayDescription => Localizer.DoStr("A melting meat thanks to its honey...");
        public override float Calories => 1150;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 12, Fat = 20, Protein = 22, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 4)]
    public class OnduHoneyRoastRecipe : RecipeFamily
    {
        public OnduHoneyRoastRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HoneyRoast",
                displayName: Localizer.DoStr("Honey Roast"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(BakedRoastItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(MeatStockItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(SimpleSyrupItem), 1, typeof(AdvancedBakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduHoneyRoastItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedBakingSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduHoneyRoastRecipe),
                start: 4f,
                skillType: typeof(AdvancedBakingSkill),
                typeof(AdvancedBakingFocusedSpeedTalent),
                typeof(AdvancedBakingParallelSpeedTalent));

            this.Initialize(displayText: Localizer.DoStr("Honey Roast"), recipeType: typeof(OnduHoneyRoastRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
