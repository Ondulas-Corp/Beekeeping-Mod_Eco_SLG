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
    /// <para>Server side item definition for the "Young Queen Bee" item.</para>
    /// </summary>
    [Serialized]
    [LocDisplayName("Young Queen Bee")]
    [LocDescription("Young Queen bee. Found in wild beehives.")]
    [Weight(1)]
    [Tag("QueenBee")]
    public class YoungQueenBeeItem : EfficiencyModule
    {
        public YoungQueenBeeItem() : base(
                ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                0.7f,
                typeof(Eco.Mods.TechTree.BeekeepingSkill),
                0.7f
                )
        { }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Young Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 2)]
    public class YoungQueenBeeRecipe : RecipeFamily
    {
        public YoungQueenBeeRecipe()
        {
            Recipes = new List<Recipe>()
            {
                new Recipe("YoungQueenBee", Localizer.DoStr("Young Queen Bee"), new IngredientElement[2]
                {
                    new IngredientElement(typeof (QueenBeeLarvaItem), 1f, false),
                    new IngredientElement(typeof (RoyalJellyItem), 1f, false)
                }, new CraftingElement[1]
                {
                    new CraftingElement<YoungQueenBeeItem>(1f)
                })
            };
            ExperienceOnCraft = 0.5f;
            LaborInCalories = CreateLaborInCaloriesValue(1500f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(YoungQueenBeeRecipe), 1200f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Young Queen Bee"), typeof(YoungQueenBeeRecipe));
            CraftingComponent.AddRecipe(typeof(BreedingBeehiveObject), this);
        }
    }
}