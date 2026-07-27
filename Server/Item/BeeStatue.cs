// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// Bee Statue recipe for beekeeping mod

namespace Beekeeping.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
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
    /// <para>Server side recipe definition for "Bee Statue".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(BeekeepingSkill), 4)]
    [Ecopedia("Items", "Decorative", subPageName: "Bee Statue")]
    public partial class BeeStatueRecipe : RecipeFamily
    {
        public BeeStatueRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BeeStatue",  //noloc
                displayName: Localizer.DoStr("Bee Statue"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PureWaxItem), 4, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(RoyalJellyItem), 1, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(PropolisItem), 2, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent)),
                    new IngredientElement(typeof(BeewaxItem), 2, typeof(BeekeepingSkill), typeof(BeekeepingLavishResourcesTalent))
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BeeStatueItem>(1) // Creates 1 bee statue
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BeekeepingSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BeeStatueRecipe), start: 15f, skillType: typeof(BeekeepingSkill), typeof(BeekeepingFocusedSpeedTalent), typeof(BeekeepingParallelSpeedTalent));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Bee Statue"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Bee Statue"), recipeType: typeof(BeeStatueRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Items", "Decorative", subPageName: "Bee Statue Object")]
    public partial class BeeStatueObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(BeeStatueItem);
        public override LocString DisplayName => Localizer.DoStr("Bee Statue");

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.ModsPostInitialize();
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    /// <summary>
    /// <para>Server side item definition for the "Bee Statue" item.</para>
    /// <para>More information about Item objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.Item.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Bee Statue")] // Defines the localized name of the item.
    [Weight(800)] // Defines how heavy the Bee Statue is.
    [Ecopedia("Items", "Decorative", createAsSubPage: true)]
    [LocDescription("A beautifully crafted statue of a bee, made with pure wax and natural bee products. A testament to the art of beekeeping.")] //The tooltip description for the item.
    public partial class BeeStatueItem : WorldObjectItem<BeeStatueObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Down , WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }
}