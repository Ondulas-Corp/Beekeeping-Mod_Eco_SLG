// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMarshmallowItem
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
    [LocDisplayName("Marshmallow")]
    [Weight(100)]
    public class OnduMarshmallowItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Marshmallow");
        public override LocString DisplayDescription => Localizer.DoStr("The best food to share around a wood fire.");
        public override float Calories => 950;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 18, Fat = 7, Protein = 4, Vitamins = 15 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(CookingSkill), 3)]
    public class OnduMarshmallowRecipe : RecipeFamily
    {
        public OnduMarshmallowRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Marshmallow",
                displayName: Localizer.DoStr("Marshmallow"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduPurifiedHoneyItem), 1, typeof(BakingSkill)),
                    new IngredientElement(typeof(SugarItem), 2, typeof(BakingSkill)),
                    new IngredientElement(typeof(FlourItem), 1, typeof(BakingSkill))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduMarshmallowItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;

            LaborInCalories = CreateLaborInCaloriesValue(25, typeof(CookingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduMarshmallowRecipe),
                start: 4f,
                skillType: typeof(CookingSkill),
                typeof(CookingFocusedSpeedTalent),
                typeof(CookingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Marshmallow"), recipeType: typeof(OnduMarshmallowRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(CastIronStoveObject), recipe: this);
        }
    }
}
