// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Frame-related items and recipes

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Simulation.WorldLayers;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Core.Items;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;

    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Wax Frame")]
    public partial class WaxFrameRecipe : RecipeFamily
    {
        public WaxFrameRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrame",  //noloc
                displayName: Localizer.DoStr("Wax Frame"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WorkerBeeItem), 1, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(FrameItem), 1, true)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<WaxFrameItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(80, typeof(BeekeepingSkill));
            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(beneficiary: typeof(WaxFrameRecipe), start: 3f, skillType: typeof(BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wax Frame"), recipeType: typeof(WaxFrameRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(SmallBeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BeekeepingSkill), 4)]
    [Ecopedia("Items", "Products", subPageName: "Wax Frame Advanced")]
    public partial class WaxFrameAdvancedRecipe : RecipeFamily
    {
        public WaxFrameAdvancedRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrameAdvanced",  //noloc
                displayName: Localizer.DoStr("Wax Frame (Advanced)"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WorkerBeeItem), 2, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(FrameItem), 2, true)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<WaxFrameItem>(3)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.8f;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(BeekeepingSkill));
            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(beneficiary: typeof(WaxFrameAdvancedRecipe), start: 3f, skillType: typeof(BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wax Frame (Advanced)"), recipeType: typeof(WaxFrameAdvancedRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(BeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Wax Frame")]
    [Weight(400)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A frame coated in beeswax, ready for honey extraction.")]
    public partial class WaxFrameItem : Item
    {
    }
}
