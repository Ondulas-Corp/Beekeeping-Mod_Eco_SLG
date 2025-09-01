using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server.Module
{
    /// <summary>
    /// <para>Server side item definition for the "Matriarch Queen Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Matriarch Queen Bee")]
    [LocDescription("Matriarch Upgrade. Found in wild beehives.")]
    [Weight(1)]
    [Tag("QueenBee")]
    public class MatriarchQueenBeeItem : EfficiencyModule
    {
        public MatriarchQueenBeeItem() : base(
                ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                0.5f,
                typeof(Eco.Mods.TechTree.BeekeepingSkill),
                0.5f
                )
        { }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Matriarch Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 7)]
    public class MatriarchQueenBeeRecipe : RecipeFamily
    {
        public MatriarchQueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("MatriarchQueenBee", Localizer.DoStr("Matriarch Queen Bee"), new IngredientElement[2]
                {
                    new IngredientElement(typeof (AdultQueenBeeItem), 1f, false),
                    new IngredientElement(typeof (RoyalJellyItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<MatriarchQueenBeeItem>(1f)
                })
            };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(1500f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(MatriarchQueenBeeRecipe), 1200f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Matriarch Queen Bee"), typeof(MatriarchQueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BreedingBeehiveObject), this);
        }
    }
}