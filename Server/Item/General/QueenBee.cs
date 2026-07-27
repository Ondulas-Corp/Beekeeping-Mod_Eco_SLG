using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
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
            LaborInCalories = CreateLaborInCaloriesValue(30f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
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