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
    /// <para>Server side item definition for the "Queen Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Queen Bee")]
    [LocDescription("The supreme ruler of the hive. A rare and valuable bee that will spoil over time without proper care.")]
    [Weight(50)]
    [MaxStackSize(1)]
    [Tag("QueenBee")]
    [Tag("Spoilable")]
    [Ecopedia("Items", "Products", subPageName: "Queen Bee")]
    public class QueenBeeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Queen Bees");
        public override float Calories => 0f; // No nutritional value
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0f, Fat = 0f, Protein = 0f, Vitamins = 0f };
        
        // Spoils in 7 days (10080 minutes)
        protected override float BaseShelfLife => 10080f;
    }

    /// <summary>
    /// <para>Server side recipe definition for "Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 6)]
    public class QueenBeeRecipe : RecipeFamily
    {
        public QueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("QueenBee", Localizer.DoStr("Queen Bee"), new IngredientElement[1]
                {
                    new IngredientElement(typeof(ForagerBeeItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<QueenBeeItem>(1f)
                })
            };
            ExperienceOnCraft = 1.0f;
            LaborInCalories = CreateLaborInCaloriesValue(80f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(QueenBeeRecipe), 7f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Queen Bee"), typeof(QueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BeeHiveObject), this);
        }
    }
}