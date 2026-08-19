// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Wax-related items and recipes

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Simulation.WorldLayers;
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
    using Eco.Mods.TechTree;


    /// <summary>
    /// <para>Server side recipe definition for "RoyalJelly".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(BeekeepingSkill), 2)]
    [Ecopedia("Items", "Products", subPageName: "Royal Jelly Item")]
    public partial class RoyalJellyRecipe : RecipeFamily
    {
        public RoyalJellyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "RoyalJelly",  //noloc
                displayName: Localizer.DoStr("Royal Jelly"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ForagerBeeItem), 2, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(BeePollenItem), 2, typeof(BeekeepingSkill)),
                    new IngredientElement(typeof(HoneyItem), 1, typeof(BeekeepingSkill))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<RoyalJellyItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 5; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(BeekeepingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply,
                CreateCraftTimeValue(beneficiary: typeof(RoyalJellyRecipe), start: 0.5f, skillType: typeof(BeekeepingSkill)),
                new MultiDynamicValue(MultiDynamicOps.Maximum,
                    new LayerModifiedValue(LayerNames.OccupiedFertileGround, BeeHiveGeneration.Config.HiveCraftLayerRadius),
                    CreateCraftTimeValue(BeeHiveGeneration.Config.HiveCraftSpeedFloor)));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Royal Jelly"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Royal Jelly"), recipeType: typeof(RoyalJellyRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(BeeHiveObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side item definition for the "RoyalJelly" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.Item.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Royal Jelly")] // Defines the localized name of the item.
    [Weight(1)] // Defines how heavy RoyalJelly is.
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A secretion produced by forager bees to nourish larvae and the queen. Essential for queen bee rearing.")] //The tooltip description for the item.
    public partial class RoyalJellyItem : Item
    {
    }


}