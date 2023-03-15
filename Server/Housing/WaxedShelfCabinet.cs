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
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedShelfCabinetObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Shelf Cabinet");
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
                return typeof(WaxedShelfCabinet);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedShelfCabinet.homeValue;
            PublicStorageComponent component = GetComponent<PublicStorageComponent>(null);
            component.Initialize(9);
            component.Storage.AddInvRestriction(new NotCarriedRestriction());
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Shelf Cabinet")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedShelfCabinet : WorldObjectItem<WaxedShelfCabinetObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("When a shelf and a cabinet aren't enough individually.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Shelves"),
            DiminishingReturnPercent = 0.5f
        };

        static WaxedShelfCabinet()
        {
            //TODO : Gérer l'occupancy
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedShelfCabinetRecipe : RecipeFamily
    {
        public WaxedShelfCabinetRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedShelfCabinet", Localizer.DoStr("Waxed Shelf Cabinet"), new IngredientElement[2]
        {
          new IngredientElement(typeof (OnduWaxItem), 2f, false),
          new IngredientElement(typeof (ShelfCabinetItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedShelfCabinet>(1f)
        })
      };
            ExperienceOnCraft = 5.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedShelfCabinetRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Shelf Cabinet"), typeof(WaxedShelfCabinetRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
