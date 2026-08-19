using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Simulation.WorldLayers;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    /// <summary>
    /// <para>Server side item definition for the "Queen Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Queen Bee")]
    [LocDescription("The supreme ruler of the hive. She gradually ages as she lays eggs — replace her before she dies or the hive will stop working.")]
    [Weight(50)]
    [MaxStackSize(1)]
    [Tag("QueenBee")]
    [Ecopedia("Items", "Products", subPageName: "Queen Bee")]
    public class QueenBeeItem : PartItem
    {
        public override LocString DisplayNamePlural  => Localizer.DoStr("Queen Bees");
        public override LocString BrokenDescription  => Localizer.DoStr("The queen bee has died. The hive is disabled until a new queen is installed.");
        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        static readonly IDynamicValue skilledRepairCost = new ConstantValue(1);
    }

    /// <summary>
    /// <para>Server side recipe definition for "Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 6)]
    public class QueenBeeRecipe : RecipeFamily
    {
        public QueenBeeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "QueenBee",
                displayName: Localizer.DoStr("Queen Bee"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BeeEggsItem), 3f, false),
                    new IngredientElement(typeof(RoyalJellyItem), 2f, false)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<QueenBeeItem>(1f)
                });
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 2.0f;
            LaborInCalories = CreateLaborInCaloriesValue(60f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(typeof(QueenBeeRecipe), 10f, typeof(Eco.Mods.TechTree.BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            Initialize(Localizer.DoStr("Queen Bee"), typeof(QueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BeeHiveObject), this);
        }
    }
}