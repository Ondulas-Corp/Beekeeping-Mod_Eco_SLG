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
using System.ComponentModel;

namespace Beekeeping.Server
{
    /// <summary>
    /// <para>Server side item definition for the "Propolis" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Propolis")]
    [LocDescription("Natural bee glue made from tree resins and wax. Used for construction and medicinal purposes.")]
    [Weight(200)]
    [MaxStackSize(20)]
    [Tag("Glue")]
    [Tag("Adhesive")]
    [Ecopedia("Items", "Products", subPageName: "Propolis")]
    public class PropolisItem : Item
    {
    }

    /// <summary>
    /// <para>Server side recipe definition for "Propolis".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 4)]
    public class PropolisRecipe : RecipeFamily
    {
        public PropolisRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("Propolis", Localizer.DoStr("Propolis"), new IngredientElement[2]
                {
                    new IngredientElement(typeof(ForagerBeeItem), 1f, true),
                    new IngredientElement("NaturalFiber", 2f, typeof(Eco.Mods.TechTree.BeekeepingSkill))
                }, new CraftingElement[1]
                {
                    new CraftingElement<PropolisItem>(1f)
                })
            };
            ExperienceOnCraft = 0.6f;
            LaborInCalories = CreateLaborInCaloriesValue(600f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(typeof(PropolisRecipe), 6f, typeof(Eco.Mods.TechTree.BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));
            Initialize(Localizer.DoStr("Propolis"), typeof(PropolisRecipe));
            // Use the correct hive name that you mentioned
            CraftingComponent.AddRecipe(typeof(BeeHiveObject), this);
        }
    }
}