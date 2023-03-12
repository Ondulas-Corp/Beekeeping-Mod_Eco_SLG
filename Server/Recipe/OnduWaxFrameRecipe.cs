// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxFrameRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduWaxFrameRecipe : RecipeFamily
    {
        public OnduWaxFrameRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrame",
                displayName: Localizer.DoStr("Wax Frame"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduFrameItem), 1),
                    new IngredientElement(typeof(OnduWaxSheetItem), 1)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduWaxFrameItem>()
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.5f;

            LaborInCalories = CreateLaborInCaloriesValue(40, typeof(BeekeepingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduWaxFrameRecipe),
                start: 120f,
                skillType: typeof(BeekeepingSkill),
                typeof(BeekeepingFocusedSpeedTalent),
                typeof(BeekeepingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Wax Frame"), recipeType: typeof(OnduWaxFrameRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(OnduBreedingBeehiveObject), recipe: this);
        }
    }
}
