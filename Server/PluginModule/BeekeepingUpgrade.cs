using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Mods.TechTree;

namespace Beekeeping.Server.Module
{
    /// <summary>
    /// <para>Server side recipe definition for "BeekeepingUpgrade".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 7)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", subPageName: "Beekeeping Upgrade Item")]
    public partial class BeekeepingUpgradeRecipe : RecipeFamily
    {
        public BeekeepingUpgradeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeekeepingUpgrade",  //noloc
                displayName: Localizer.DoStr("Beekeeping Upgrade"),

                // Defines the ingredients needed to craft this recipe
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BasicUpgradeLvl4Item), 1, true),
                },

                // Define our recipe output items
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeekeepingUpgradeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; // Experience gained when crafted
            
            // Labor required and skill
            this.LaborInCalories = CreateLaborInCaloriesValue(3000, typeof(Eco.Mods.TechTree.BeekeepingSkill));

            // Crafting time
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeekeepingUpgradeRecipe), start: 8, skillType: typeof(Eco.Mods.TechTree.BeekeepingSkill));

            // Initialize the recipe
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Beekeeping Upgrade"), recipeType: typeof(BeekeepingUpgradeRecipe));
            this.ModsPostInitialize();

            // Register with the crafting system - craft at breeding beehive
            CraftingComponent.AddRecipe(tableType: typeof(Beekeeping.Server.BeeHiveObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Beekeeping Upgrade")]
    [LocDescription("Specialty upgrade that greatly increases efficiency when crafting Beekeeping recipes.")]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    public partial class BeekeepingUpgradeItem : EfficiencyModule
    {
        public BeekeepingUpgradeItem() : base(
            ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
            0.5f + 0.05f, // 55% efficiency bonus
            typeof(Eco.Mods.TechTree.BeekeepingSkill),
            0.5f // 50% speed bonus
        ) { }
    }
}