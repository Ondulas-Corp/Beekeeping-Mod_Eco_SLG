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
    /// <para>Server side item definition for the "Queen Bee Larva" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Queen Bee Larva")]
    [LocDescription("Basic Upgrades. Found in wild beehives.")]
    [Weight(1)]
    [Tag("QueenBeeLarva")]
    public class QueenBeeLarvaItem : EfficiencyModule
    {
        public QueenBeeLarvaItem() : base(
                ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                0.8f,
                typeof(Eco.Mods.TechTree.BeekeepingSkill),
                0.8f
                )
        { }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Queen Bee Larva".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 1)]
    public class QueenBeeLarvaRecipe : RecipeFamily
    {
        public QueenBeeLarvaRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("QueenBeeLarva", Localizer.DoStr("Queen Bee Larva"), new IngredientElement[1]
                {
                    new IngredientElement(typeof (RoyalJellyItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<QueenBeeLarvaItem>(1f)
                })
            };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(1500f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(QueenBeeLarvaRecipe), 1200f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Queen Bee Larva"), typeof(QueenBeeLarvaRecipe));
            CraftingComponent.AddRecipe(typeof(BreedingBeehiveObject), this);
        }
    }
}