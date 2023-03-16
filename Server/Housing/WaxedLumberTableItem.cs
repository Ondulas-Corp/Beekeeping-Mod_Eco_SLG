using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Mods.TechTree;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server.Housing
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed LumberTable Item")]
    public class WaxedLumberTableObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Lumber Table");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedLumberTableItem);

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedLumberTableItem.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Lumber Table")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedLumberTableItem : WorldObjectItem<WaxedLumberTableObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A nice, sturdy waxed lumber table.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Table"),
            DiminishingReturnPercent = 0.6f
        };

        static WaxedLumberTableItem()
        {
            WorldObject.AddOccupancy<WaxedLumberTableObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(-1, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed LumberTable Item")]
    public class WaxedLumberTableRecipe : RecipeFamily
    {
        public WaxedLumberTableRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedLumberTable", Localizer.DoStr("Waxed Lumber Table"), new IngredientElement[2]
        {
          new IngredientElement(typeof (LumberTableItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedLumberTableItem>()
        })
      };
            ExperienceOnCraft = 3.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedLumberTableRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Lumber Table"), typeof(WaxedLumberTableRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Hardwood LumberTable Item")]
    public class WaxedHardwoodLumberTableRecipe : Recipe
    {
        public WaxedHardwoodLumberTableRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberTableRecipe), new Recipe("Waxed Lumber Bench", Localizer.DoStr("Waxed Lumber Bench"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HardwoodLumberTableItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberTableItem>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Softwood LumberTable Item")]
    public class WaxedSoftwoodLumberTableRecipe : Recipe
    {
        public WaxedSoftwoodLumberTableRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberTableRecipe), new Recipe("Waxed Lumber Bench", Localizer.DoStr("Waxed Lumber Bench"), new IngredientElement[2]
            {
                new IngredientElement(typeof (SoftwoodLumberTableItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberTableItem>()
            }));
        }
    }
}
