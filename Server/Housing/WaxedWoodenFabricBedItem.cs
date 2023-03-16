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
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(MountComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    [Ecopedia("Housing Objects", "Bedroom", subPageName: "Waxed WoodenFabricBed Item")]
    public class WaxedWoodenFabricBedObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Wooden Fabric Bed");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedWoodenFabricBedItem);

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedWoodenFabricBedItem.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed WoodenFabricBed")]
    [Ecopedia("Housing Objects", "Bedroom", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedWoodenFabricBedItem : WorldObjectItem<WaxedWoodenFabricBedObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A much more comfortable bed made with fabric.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Bedroom"),
            HouseValue = 3.5f,
            TypeForRoomLimit = Localizer.DoStr("Bed"),
            DiminishingReturnPercent = 0.2f
        };

        static WaxedWoodenFabricBedItem()
        {
            WorldObject.AddOccupancy<WaxedWoodenFabricBedObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(1, 0, 1)),
                new BlockOccupancy(new Vector3i(0, 0, 1)),
                new BlockOccupancy(new Vector3i(0, 0, 2)),
                new BlockOccupancy(new Vector3i(1, 0, 2)),
                new BlockOccupancy(new Vector3i(1, 1, 2)),
                new BlockOccupancy(new Vector3i(0, 1, 2)),
                new BlockOccupancy(new Vector3i(0, 1, 1)),
                new BlockOccupancy(new Vector3i(1, 1, 1)),
                new BlockOccupancy(new Vector3i(1, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(1, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Bedroom", subPageName: "Waxed WoodenFabricBed Item")]
    public class WaxedWoodenFabricBedRecipe : RecipeFamily
    {
        public WaxedWoodenFabricBedRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenFabricBed", Localizer.DoStr("Waxed Wooden Fabric Bed"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenFabricBedItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 3f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenFabricBedItem>(1f)
        })
      };
            ExperienceOnCraft = 4.0f;
            LaborInCalories = CreateLaborInCaloriesValue(300f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenFabricBedRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Fabric Bed"), typeof(WaxedWoodenFabricBedRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
