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
    /// <para>Server side item definition for the "Forager Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Forager Bee")]
    [LocDescription("An experienced forager bee specialized in gathering resources. Will spoil over time.")]
    [Weight(50)]
    [MaxStackSize(5)]
    [Tag("ForagerBee")]
    [Tag("Spoilable")]
    [Ecopedia("Items", "Products", subPageName: "Forager Bee")]
    public class ForagerBeeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Forager Bees");
        public override float Calories => 0f; // No nutritional value
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0f, Fat = 0f, Protein = 0f, Vitamins = 0f };
        
		public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(96);
    }

    /// <summary>
    /// <para>Server side recipe definition for "Forager Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 4)]
    public class ForagerBeeRecipe : RecipeFamily
    {
        public ForagerBeeRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("ForagerBee", Localizer.DoStr("Forager Bee"), new IngredientElement[1]
                {
                    new IngredientElement(typeof(WorkerBeeItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<ForagerBeeItem>(1f)
                })
            };
            ExperienceOnCraft = 0.6f;
            LaborInCalories = CreateLaborInCaloriesValue(30f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(ForagerBeeRecipe), 6f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Forager Bee"), typeof(ForagerBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BeeHiveObject), this);
        }
    }
}