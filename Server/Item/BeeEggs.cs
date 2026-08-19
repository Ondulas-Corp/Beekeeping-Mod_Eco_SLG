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
    [Serialized]
    [LocDisplayName("Bee Eggs")]
    [LocDescription("Fresh bee eggs that will spoil over time. Found in wild beehives.")]
    [Weight(50)]
    [MaxStackSize(20)]
    [Tag("BeeEggs")]
    [Tag("Spoilable")]
    [Ecopedia("Items", "Products", subPageName: "Bee Eggs")]
    public class BeeEggsItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Bee Eggs");
        public override float Calories => 0f;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0f, Fat = 0f, Protein = 0f, Vitamins = 0f };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(48);
    }

    // Bootstrap: needs a BeeColonyCore (from wild hive) to seed the colony â€” one-time dependency on the wild ecosystem
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsBootstrapRecipe : RecipeFamily
    {
        public BeeEggsBootstrapRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeeEggsBootstrap",
                displayName: Localizer.DoStr("Bee Eggs (Bootstrap)"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BeeColonyCoreItem), 1f, false),
                    new IngredientElement("Petals", 90f, typeof(Eco.Mods.TechTree.BeekeepingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeeEggsItem>(6f)
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(60f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(typeof(BeeEggsBootstrapRecipe), 8f, typeof(Eco.Mods.TechTree.BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            Initialize(Localizer.DoStr("Bee Eggs (Bootstrap)"), typeof(BeeEggsBootstrapRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }

    // Entretien: petal-only upkeep once the colony is running
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsRecipe : RecipeFamily
    {
        public BeeEggsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeeEggs",
                displayName: Localizer.DoStr("Bee Eggs"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Petals", 40f, typeof(Eco.Mods.TechTree.BeekeepingSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeeEggsItem>(2f)
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.2f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(typeof(BeeEggsRecipe), 5f, typeof(Eco.Mods.TechTree.BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            Initialize(Localizer.DoStr("Bee Eggs"), typeof(BeeEggsRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }
}
