// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduWaxRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Benefit;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    public class OnduWaxRecipe : RecipeFamily
    {
        public OnduWaxRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Wax",
                displayName: Localizer.DoStr("Wax"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(OnduWaxFrameItem), 1)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OnduWaxItem>(2)
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.5f;

            LaborInCalories = CreateLaborInCaloriesValue(30, typeof(BeekeepingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(OnduWaxRecipe),
                start: 0.5f,
                skillType: typeof(BeekeepingSkill),
                typeof(BeekeepingFocusedSpeedTalent),
                typeof(BeekeepingParallelSpeedTalent));

            Initialize(displayText: Localizer.DoStr("Wax"), recipeType: typeof(OnduWaxRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(OnduHoneyExtractObject), recipe: this);
            CraftingComponent.AddRecipe(tableType: typeof(OnduHoneyExtractMecanicalObject), recipe: this);
        }
    }
}
