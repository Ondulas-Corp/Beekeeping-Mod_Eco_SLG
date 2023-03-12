// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMadeleineItem
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

namespace Beekeeping.Server.Food
{
    [Serialized]
    [LocDisplayName("Madeleine")]
    [Weight(150)]
    public class OnduMadeleineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Madeleine");
        public override LocString DisplayDescription => Localizer.DoStr("A Spanish and French pastry.");
        public override float Calories => 700;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 18, Fat = 7, Protein = 13, Vitamins = 7 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(BakingSkill), 4)]
    public class OnduMadeleineRecipe : RecipeFamily
    {
        public OnduMadeleineRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Madeleine",
                displayName: Localizer.DoStr("Madeleine"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(SugarItem), 4, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 2, typeof(BakingSkill)),
                    new IngredientElement("Fat", 2, typeof(BakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduMadeleineItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BakingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduMadeleineRecipe),
                start: 4f,
                skillType: typeof(BakingSkill),
                typeof(BakingFocusedSpeedTalent),
                typeof(BakingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Madeleine"), recipeType: typeof(OnduMadeleineRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
