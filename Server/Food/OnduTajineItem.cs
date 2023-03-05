// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduTajineItem
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
    [LocDisplayName("Tajine")]
    [Weight(450)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    public class OnduTajineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Tajine");
        public override LocString DisplayDescription => Localizer.DoStr("Traditional Tagine.");
        public override float Calories => 1300;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 11, Fat = 19, Protein = 20, Vitamins = 12 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 6)]
    public class OnduTajineRecipe : RecipeFamily
    {
        public OnduTajineRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Tajine",
                displayName: Localizer.DoStr("Tajine"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(InfusedOilItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(SimmeredMeatItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(CornmealItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(VegetableMedleyItem), 2, typeof(AdvancedCookingSkill)),
                    new IngredientElement(typeof(BoiledRiceItem), 1, typeof(AdvancedCookingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduTajineItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedCookingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduTajineRecipe),
                start: 4f,
                skillType: typeof(AdvancedCookingSkill),
                typeof(AdvancedCookingFocusedSpeedTalent),
                typeof(AdvancedCookingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Tajine"), recipeType: typeof(OnduTajineRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(StoveObject), recipe: this);
        }
    }
}
