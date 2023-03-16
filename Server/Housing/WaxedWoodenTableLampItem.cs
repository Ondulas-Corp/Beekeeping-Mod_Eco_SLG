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
    [Ecopedia("Housing Objects", "Lighting", subPageName: "Waxed WoodenTableLamp Item")]
    public class WaxedWoodenTableLampObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Wooden Table Lamp");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedWoodenTableLampItem);

        protected override void Initialize()
        {
            GetComponent<PowerConsumptionComponent>(null).Initialize(60f);
            GetComponent<PowerGridComponent>(null).Initialize(10f, new ElectricPower());
            GetComponent<HousingComponent>().HomeValue = WaxedWoodenTableLampItem.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Wooden Table Lamp")]
    [Ecopedia("Housing Objects", "Lighting", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedWoodenTableLampItem : WorldObjectItem<WaxedWoodenTableLampObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("For late night studying. Or working. Or anything, really.");
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

        static WaxedWoodenTableLampItem()
        {
            WorldObject.AddOccupancy<WaxedHewnChairObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 0))
            });
        }

        [Tooltip(7, new Type[] { })]
        private LocString PowerConsumptionTooltip => Localizer.Do(FormattableStringFactory.Create("Consumes: {0}w of {1} power", Text.Info(60), new ElectricPower().Name));
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Lighting", subPageName: "WoodenTableLamp Item")]
    public class WaxedWoodenTableLampRecipe : RecipeFamily
    {
        public WaxedWoodenTableLampRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenTableLamp", Localizer.DoStr("Waxed Wooden Table Lamp"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenTableLampItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenTableLampItem>(1f)
        })
      };
            ExperienceOnCraft = 3.0f;
            LaborInCalories = CreateLaborInCaloriesValue(200f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenTableLampRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Table Lamp"), typeof(WaxedWoodenTableLampRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
