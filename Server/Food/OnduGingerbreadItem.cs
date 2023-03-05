// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduGingerbreadItem
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
    [LocDisplayName("Gingerbread")]
    [Weight(400)]
    public class OnduGingerbreadItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Gingerbread");
        public override LocString DisplayDescription => Localizer.DoStr("Tight and melting.");
        public override float Calories => 1200;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 23, Fat = 16, Protein = 8, Vitamins = 15 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 5)]
    public class OnduGingerbreadRecipe : RecipeFamily
    {
        public OnduGingerbreadRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Gingerbread",
                displayName: Localizer.DoStr("Gingerbread"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(AdvancedBakingSkill)),
                    new IngredientElement("Fat", 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(SimpleSyrupItem), 4, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(YeastItem), 2, typeof(AdvancedBakingSkill)),
                    new IngredientElement(typeof(FlourItem), 6, typeof(AdvancedBakingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduGingerbreadItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(AdvancedBakingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduGingerbreadRecipe),
                start: 3f,
                skillType: typeof(AdvancedBakingSkill),
                typeof(AdvancedBakingFocusedSpeedTalent),
                typeof(AdvancedBakingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Blueberry Cocktail"), recipeType: typeof(OnduGingerbreadRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(BakeryOvenObject), recipe: this);
        }
    }
}
