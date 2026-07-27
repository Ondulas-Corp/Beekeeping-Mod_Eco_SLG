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
    /// <para>Server side recipe definition for "Biodiesel".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(OilDrillingSkill), 2)]
    [Ecopedia("Items", "Fuel", subPageName: "Biodiesel Item")]
    public partial class BiodieselFromWaxRecipe : RecipeFamily
    {
        public BiodieselFromWaxRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BiodieselFromWax",  //noloc
                displayName: Localizer.DoStr("Biodiesel from Wax"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BeewaxItem), 1, typeof(OilDrillingSkill), typeof(OilDrillingLavishResourcesTalent)),
                    new IngredientElement(typeof(BarrelItem), 1, true),
                    new IngredientElement(typeof(EthanolItem), 6, typeof(OilDrillingSkill), typeof(OilDrillingLavishResourcesTalent))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BiodieselItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(OilDrillingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BiodieselFromWaxRecipe), start: 0.8f, skillType: typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Biodiesel from Wax"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Biodiesel from Wax"), recipeType: typeof(BiodieselFromWaxRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(OilRefineryObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}