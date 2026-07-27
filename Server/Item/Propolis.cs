using Eco.Core.Items;
using Eco.Gameplay.Components;
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
                new Recipe("Propolis", Localizer.DoStr("Propolis"), new IngredientElement[3]
                {
                    new IngredientElement(typeof(ForagerBeeItem), 1f, false),
					new IngredientElement(typeof(BeewaxItem), 2f, false),
                    new IngredientElement("NaturalFiber", 3f, false) // Fiber tag for plant fibers
                }, new CraftingElement[1]
                {
                    new CraftingElement<PropolisItem>(2f)
                })
            };
            ExperienceOnCraft = 0.6f;
            LaborInCalories = CreateLaborInCaloriesValue(600f, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            CraftMinutes = CreateCraftTimeValue(typeof(PropolisRecipe), 6f, typeof(Eco.Mods.TechTree.BeekeepingSkill), new Type[2]
            {
                typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent),
                typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Propolis"), typeof(PropolisRecipe));
            // Use the correct hive name that you mentioned
            CraftingComponent.AddRecipe(typeof(BeeHiveObject), this);
        }
    }
}