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
    /// <summary>
    /// <para>Server side item definition for the "Bee Eggs" item.</para>
    /// </summary>
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
        public override float Calories => 0f; // No nutritional value
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0f, Fat = 0f, Protein = 0f, Vitamins = 0f };
        
        public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(48);
    }

    /// <summary>
    /// <para>Server side recipe definition for "Bee Eggs from Compost".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsFromCompostRecipe : RecipeFamily
    {
        public BeeEggsFromCompostRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("BeeEggsFromCompost", Localizer.DoStr("Bee Eggs"), new IngredientElement[1]
                {
                    new IngredientElement(typeof(Eco.Mods.TechTree.CompostItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<BeeEggsItem>(3f)
                })
            };
            ExperienceOnCraft = 0.2f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(BeeEggsFromCompostRecipe), 12f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Bee Eggs"), typeof(BeeEggsFromCompostRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Bee Eggs from Spoiled Food".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsFromSpoiledFoodRecipe : RecipeFamily
    {
        public BeeEggsFromSpoiledFoodRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("BeeEggsFromSpoiledFood", Localizer.DoStr("Bee Eggs"), new IngredientElement[1]
                {
                    new IngredientElement(typeof(Eco.Mods.TechTree.SpoiledFoodItem), 5f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<BeeEggsItem>(1f)
                })
            };
            ExperienceOnCraft = 0.2f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(BeeEggsFromSpoiledFoodRecipe), 12f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Bee Eggs"), typeof(BeeEggsFromSpoiledFoodRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Bee Eggs from Petals".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class BeeEggsFromPetalsRecipe : RecipeFamily
    {
        public BeeEggsFromPetalsRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("BeeEggsFromPetals", Localizer.DoStr("Bee Eggs"), new IngredientElement[1]
                {
                    new IngredientElement("Petals", 4f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<BeeEggsItem>(2f)
                })
            };
            ExperienceOnCraft = 0.2f;
            LaborInCalories = CreateLaborInCaloriesValue(50f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(BeeEggsFromPetalsRecipe), 12f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Bee Eggs"), typeof(BeeEggsFromPetalsRecipe));
            CraftingComponent.AddRecipe(typeof(SmallBeeHiveObject), this);
        }
    }
}