// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Bee Pollen fertilizer for beekeeping mod

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Core.Items;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Shared.Time;
    using Eco.Mods.TechTree;

    /// <summary>
    /// <para>Server side fertilizer item definition for the "Bee Pollen" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.FertilizerItem-1.html.</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Bee Pollen")] // Defines the localized name of the item.
    [LocDescription("Natural fertilizer created by bees from plant materials. Rich in nutrients for crops.")] //The tooltip description for the item.
    [Weight(500)] // Defines how heavy Bee Pollen is.
    [Category("Tool")] // Gives this item the category of "Tool" for organization
    [Tag("Fertilizer")] // Gives this item the Fertilizer tag for use in recipes
    [Ecopedia("Items", "Fertilizers", createAsSubPage: true)]
    public partial class BeePollenItem : FertilizerItem
    {
        /// <summary>Defines the amount of nutrients in this fertilizer item.</summary>
        public override FertilizerNutrients Nutrients => new FertilizerNutrients(3, 2, 4.5f);
    }

    /// <summary>
    /// <para>Server side recipe definition for "Bee Pollen from Petals".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(BeekeepingSkill), 1)]
    [Ecopedia("Items", "Fertilizers", subPageName: "Bee Pollen")]
    public partial class BeePollenFromPetalsRecipe : RecipeFamily
    {
        public BeePollenFromPetalsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeePollenFromPetals",  //noloc
                displayName: Localizer.DoStr("Bee Pollen"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WorkerBeeItem), 1, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent)),
                    new IngredientElement("Petals", 3, typeof(BeekeepingSkill)) //noloc
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeePollenItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.4f;
            this.LaborInCalories = CreateLaborInCaloriesValue(80, typeof(BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeePollenFromPetalsRecipe), start: 2f, skillType: typeof(BeekeepingSkill), typeof(BeekeepingFocusedSpeedTalent), typeof(BeekeepingParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Bee Pollen"), recipeType: typeof(BeePollenFromPetalsRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(SmallBeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BeekeepingSkill), 4)]
    [Ecopedia("Items", "Fertilizers", subPageName: "Bee Pollen Advanced")]
    public partial class BeePollenAdvancedRecipe : RecipeFamily
    {
        public BeePollenAdvancedRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeePollenAdvanced",  //noloc
                displayName: Localizer.DoStr("Bee Pollen (Advanced)"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ForagerBeeItem), 1, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent)),
                    new IngredientElement("Petals", 3, typeof(BeekeepingSkill)) //noloc
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeePollenItem>(2)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.6f;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeePollenAdvancedRecipe), start: 2f, skillType: typeof(BeekeepingSkill), typeof(BeekeepingFocusedSpeedTalent), typeof(BeekeepingParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Bee Pollen (Advanced)"), recipeType: typeof(BeePollenAdvancedRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(BeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}