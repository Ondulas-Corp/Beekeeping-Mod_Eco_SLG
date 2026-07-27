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
    [RequiresSkill(typeof(FertilizersSkill), 2)]
    [Ecopedia("Items", "Fertilizers", subPageName: "Bee Pollen from Petals")]
    public partial class BeePollenFromPetalsRecipe : RecipeFamily
    {
        public BeePollenFromPetalsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeePollenFromPetals",  //noloc
                displayName: Localizer.DoStr("Bee Pollen"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WorkerBeeItem), 1, typeof(FertilizersSkill)),
                    new IngredientElement("Petals", 4, typeof(FertilizersSkill)) //noloc
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeePollenItem>(3)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.4f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(80, typeof(FertilizersSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeePollenFromPetalsRecipe), start: 2f, skillType: typeof(FertilizersSkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Bee Pollen"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Bee Pollen"), recipeType: typeof(BeePollenFromPetalsRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}