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
    /// <para>Server side item definition for the "Adult Queen Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Adult Queen Bee")]
    [LocDescription("Adult Upgrade. Found in wild beehives.")]
    [Weight(1)]
    [Tag("QueenBee")]
    public class AdultQueenBeeItem : EfficiencyModule
    {
        public AdultQueenBeeItem() : base(
                ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                0.6f,
                typeof(Eco.Mods.TechTree.BeekeepingSkill),
                0.6f
                )
        { }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Adult Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 5)]
    public class AdultQueenBeeRecipe : RecipeFamily
    {
        public AdultQueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("AdultQueenBee", Localizer.DoStr("Adult Queen Bee"), new IngredientElement[2]
                {
                    new IngredientElement(typeof (YoungQueenBeeItem), 1f, false),
                    new IngredientElement(typeof (RoyalJellyItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<AdultQueenBeeItem>(1f)
                })
            };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(1500f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(AdultQueenBeeRecipe), 1200f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Adult Queen Bee"), typeof(AdultQueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BreedingBeehiveObject), this);
        }
    }
}