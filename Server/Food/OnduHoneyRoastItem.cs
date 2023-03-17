// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyRoastItem
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
    [LocDisplayName("Honey Roast")]
    [Weight(400)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    public class OnduHoneyRoastItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Honey Roast");
        public override LocString DisplayDescription => Localizer.DoStr("A melting meat thanks to its honey...");
        public override float Calories => 1150;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 12, Fat = 20, Protein = 22, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    [Ecopedia("Food", "Cooking", subPageName: "HoneyRoast Item")]
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
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(BakedRoastItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(MeatStockItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SimpleSyrupItem), 1, typeof(AdvancedCookingSkill)),
                    new IngredientElement("Fat", 1, typeof(AdvancedCookingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduHoneyRoastItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(AdvancedCookingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduHoneyRoastRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill),
                typeof(AdvancedCookingFocusedSpeedTalent),
                typeof(AdvancedCookingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Honey Roast"), recipeType: typeof(OnduHoneyRoastRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipe: this);
        }
    }
}
