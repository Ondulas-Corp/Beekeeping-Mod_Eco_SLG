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
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Beekeeping.Server.Housing
{
    [Serialized]
    [RequireComponent(typeof(OnOffComponent), null)]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(PowerGridComponent), null)]
    [RequireComponent(typeof(PowerConsumptionComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedWoodenFloorLampObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Wooden Floor Lamp");
            }
        }

        public virtual TableTextureMode TableTexture
        {
            get
            {
                return (TableTextureMode)1;
            }
        }

        public virtual Type RepresentedItemType
        {
            get
            {
                return typeof(WaxedWoodenFloorLamp);
            }
        }

        protected override void Initialize()
        {
            GetComponent<PowerConsumptionComponent>(null).Initialize(60f);
            GetComponent<PowerGridComponent>(null).Initialize(10f, new ElectricPower());
            GetComponent<HousingComponent>().HomeValue = WaxedWoodenFloorLamp.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Wooden Floor Lamp")]
    [Tag("Housing", 1)]
    public class WaxedWoodenFloorLamp : WorldObjectItem<WaxedWoodenFloorLampObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A more modern way to light up a room. This time from the floor.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Lighting"),
            HouseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Lights"),
            DiminishingReturnPercent = 0.7f
        };

        static WaxedWoodenFloorLamp()
        {
            WorldObject.AddOccupancy<WaxedWoodenFloorLampObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }

        [Tooltip(7, new Type[] { })]
        private LocString PowerConsumptionTooltip => Localizer.Do(FormattableStringFactory.Create("Consumes: {0}w of {1} power", Text.Info(60), new ElectricPower().Name));
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedWoodenFloorLampRecipe : RecipeFamily
    {
        public WaxedWoodenFloorLampRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenFloorLamp", Localizer.DoStr("Waxed Wooden Floor Lamp"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenFloorLampItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenFloorLamp>(1f)
        })
      };
            ExperienceOnCraft = 3.0f;
            LaborInCalories = CreateLaborInCaloriesValue(200f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenFloorLampRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Floor Lamp"), typeof(WaxedWoodenFloorLampRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
