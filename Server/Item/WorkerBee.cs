using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Core.Controller;
using Eco.Gameplay.Players;
using Eco.Shared.Utils;
using Eco.Shared.Time;
using Eco.Gameplay.DynamicValues;
using Eco.Simulation.WorldLayers;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    /// <summary>
    /// <para>Server side item definition for the "Worker Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Worker Bee")]
    [LocDescription("A dedicated worker bee that will spoil over time if not properly cared for.")]
    [Weight(50)]
    [MaxStackSize(10)]
    [Tag("WorkerBee")]
    [Tag("Spoilable")]
    [Ecopedia("Items", "Products", subPageName: "Worker Bee")]
    public class WorkerBeeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Worker Bees");
        public override float Calories => 0f; // No nutritional value
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0f, Fat = 0f, Protein = 0f, Vitamins = 0f };
        
        // Spoils in 3 days (4320 minutes)
        public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(72);
    }

    /// <summary>
    /// <para>Server side recipe definition for "Worker Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class WorkerBeeRecipe : RecipeFamily
    {
        public WorkerBeeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WorkerBee",
                displayName: Localizer.DoStr("Worker Bee"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof (BeeEggsItem), 1f, false)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<WorkerBeeItem>(1f)
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.4f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(typeof(WorkerBeeRecipe), 5f, typeof(Eco.Mods.TechTree.BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            Initialize(Localizer.DoStr("Worker Bee"), typeof(WorkerBeeRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }
}