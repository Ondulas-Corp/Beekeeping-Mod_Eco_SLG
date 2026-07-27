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

    // Bootstrap: needs a BeeColonyCore (from wild hive) to seed the colony — one-time dependency on the wild ecosystem
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsBootstrapRecipe : RecipeFamily
    {
        public BeeEggsBootstrapRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("BeeEggsBootstrap", Localizer.DoStr("Bee Eggs (Bootstrap)"), new IngredientElement[2]
                {
                    new IngredientElement(typeof(BeeColonyCoreItem), 1f, false),
                    new IngredientElement("Petals", 4f, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent))
                }, new CraftingElement[1]
                {
                    new CraftingElement<BeeEggsItem>(6f)
                })
            };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(60f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(BeeEggsBootstrapRecipe), 8f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
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
            Recipes = new List<Recipe>()
            {
                new Recipe("BeeEggs", Localizer.DoStr("Bee Eggs"), new IngredientElement[1]
                {
                    new IngredientElement("Petals", 4f, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent))
                }, new CraftingElement[1]
                {
                    new CraftingElement<BeeEggsItem>(2f)
                })
            };
            ExperienceOnCraft = 0.2f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(BeeEggsRecipe), 5f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Bee Eggs"), typeof(BeeEggsRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }
}
