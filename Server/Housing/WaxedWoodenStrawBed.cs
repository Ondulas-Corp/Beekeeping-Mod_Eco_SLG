using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
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
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireComponent(typeof(MountComponent))]
    public class WaxedWoodenStrawBedObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Wooden Straw Bed");
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
                return typeof(WaxedWoodenStrawBed);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedWoodenStrawBed.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Wooden Straw Bed")]
    [Tag("Housing", 1)]
    public class WaxedWoodenStrawBed : WorldObjectItem<WaxedWoodenStrawBedObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A nice, scratchy, waxed and horrible uncomfortable bed. But at least it keeps you off the ground.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Bedroom"),
            HouseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Bed"),
            DiminishingReturnPercent = 0.4f
        };

        static WaxedWoodenStrawBed()
        {
            WorldObject.AddOccupancy<WaxedWoodenStrawBedObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 0, -1)),
        new BlockOccupancy(new Vector3i(0, 0, -2))
      });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedWoodenStrawBedRecipe : RecipeFamily
    {
        public WaxedWoodenStrawBedRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedWoodenStrawBed", Localizer.DoStr("Waxed Wooden Straw Bed"), new IngredientElement[2]
        {
          new IngredientElement(typeof (WoodenStrawBedItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedWoodenStrawBed>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedWoodenStrawBedRecipe), 2f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Wooden Straw Bed"), typeof(WaxedWoodenStrawBedRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
