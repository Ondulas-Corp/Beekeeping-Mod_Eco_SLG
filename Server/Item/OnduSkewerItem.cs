// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSkewerItem
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

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Skewer meat and honey")]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [Weight(200)]
    public class OnduSkewerItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Skewer meat and honey");
        public override LocString DisplayDescription => Localizer.DoStr("Skewer of meat with honey.");
        public override float Calories => 950;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 15, Fat = 10, Protein = 17, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(CookingSkill), 3)]
    public class OnduSkewerRecipe : RecipeFamily
    {
        public OnduSkewerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SkewerMeatandHoney",
                displayName: Localizer.DoStr("Skewer Meat and Honey"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(CookingSkill)),
                    new IngredientElement(typeof(MeatStockItem), 1, typeof(CookingSkill)),
                    new IngredientElement(typeof(ScrapMeatItem), 10, typeof(CookingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduSkewerItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(20, typeof(CookingSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduSkewerRecipe),
                start: 4f,
                skillType: typeof(CookingSkill),
                typeof(BakingFocusedSpeedTalent),
                typeof(BakingParallelSpeedTalent));

            this.Initialize(displayText: Localizer.DoStr("Skewer Meat and Honey"), recipeType: typeof(OnduSkewerRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(CastIronStoveObject), recipe: this);
        }
    }
}
