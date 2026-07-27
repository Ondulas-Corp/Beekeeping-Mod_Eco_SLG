// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Frame-related items and recipes

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
    using Eco.Mods.TechTree;

    /// <summary>
    /// <para>Server side recipe definition for "WaxFrame".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
	[RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 2)]
    [Ecopedia("Items", "Products", subPageName: "Wax Frame from Worker Bee")]
    public partial class WaxFrameFromWorkerBeeRecipe : RecipeFamily
    {
        public WaxFrameFromWorkerBeeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrameFromWorkerBee",  //noloc
                displayName: Localizer.DoStr("Wax Frame"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WorkerBeeItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(FrameItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<WaxFrameItem>(1) // 2 wax frames
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(WaxFrameFromWorkerBeeRecipe), start: 3f, skillType: typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent), typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wax Frame"), recipeType: typeof(WaxFrameFromWorkerBeeRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SmallBeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side recipe definition for "Wax Frame from Forager Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 2)]
    [Ecopedia("Items", "Products", subPageName: "Wax Frame from Forager Bee")]
    public partial class WaxFrameFromForagerBeeRecipe : RecipeFamily
    {
        public WaxFrameFromForagerBeeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrameFromForagerBee",  //noloc
                displayName: Localizer.DoStr("Wax Frame"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ForagerBeeItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(FrameItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<WaxFrameItem>(3)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.7f;
            
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(WaxFrameFromForagerBeeRecipe), start: 3f, skillType: typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent), typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wax Frame"), recipeType: typeof(WaxFrameFromForagerBeeRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(BeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side recipe definition for "Wax Frame from Queen Bee".</para>
    /// </summary>
    [RequiresSkill(typeof(Eco.Mods.TechTree.BeekeepingSkill), 2)]
    [Ecopedia("Items", "Products", subPageName: "Wax Frame from Queen Bee")]
    public partial class WaxFrameFromQueenBeeRecipe : RecipeFamily
    {
        public WaxFrameFromQueenBeeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WaxFrameFromQueenBee",  //noloc
                displayName: Localizer.DoStr("Wax Frame"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(QueenBeeItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(FrameItem), 1, typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingLavishResourcesTalent))
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<WaxFrameItem>(5)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1.0f;
            
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(Eco.Mods.TechTree.BeekeepingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(WaxFrameFromQueenBeeRecipe), start: 3f, skillType: typeof(Eco.Mods.TechTree.BeekeepingSkill), typeof(Eco.Mods.TechTree.BeekeepingFocusedSpeedTalent), typeof(Eco.Mods.TechTree.BeekeepingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wax Frame"), recipeType: typeof(WaxFrameFromQueenBeeRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(BeeHiveObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side item definition for the "WaxFrame" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.Item.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Wax Frame")] // Defines the localized name of the item.
    [Weight(400)] // Defines how heavy WaxFrame is.
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("Wax Frame. Contains the wax.")] //The tooltip description for the item.
    public partial class WaxFrameItem : Item
    {
    }

}