using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Bonuses;
using Eco.Gameplay.Components;
using Eco.Gameplay.Garbage;
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
                    new IngredientElement(typeof(Beekeeping.Server.ForagerBeeItem), 20, true),
                    new IngredientElement(typeof(Beekeeping.Server.QueenBeeItem), 1, true),
                },
                garbages: new List<GarbageOutput>
                {
                    new GarbageOutput(typeof(Trash), 0.2f),
                },
                // Define our recipe output items
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeekeepingUpgradeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; // Experience gained when crafted

            // Labor required and skill
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, typeof(Eco.Mods.TechTree.BeekeepingSkill));

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
    [SalvageCost(typeof(Trash), 1.0f)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("SpecialtyModule")]
    public partial class BeekeepingUpgradeItem : EfficiencyModule
    {
        public BeekeepingUpgradeItem() : base(ModuleTypes.None, 1f) { }

        public override float MaterialTierBump => 0f;

        public override IEnumerable<Bonus> Bonuses => new[]
        {
            new Bonus
            {
                Causes  = new List<BonusCause>  { new CraftBonusCause { Action = BonusAction.ResourceCost, SkillTypes = new HashSet<Type> { typeof(Eco.Mods.TechTree.BeekeepingSkill) } } },
                Effects = new List<BonusEffect> { new BonusEffectAdditivePercent { Percent = -0.05f, LowerIsBetter = true } },
            },
            new Bonus
            {
                Causes  = new List<BonusCause>  { new CraftBonusCause { Action = BonusAction.CraftTime, SkillTypes = new HashSet<Type> { typeof(Eco.Mods.TechTree.BeekeepingSkill) } } },
                Effects = new List<BonusEffect> { new BonusEffectMultiplicative { Value = 0.75f, LowerIsBetter = true } },
            },
        };
    }
}