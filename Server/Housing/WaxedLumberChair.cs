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
    [RequireComponent(typeof(MountComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedLumberChairObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Lumber Chair");
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
                return typeof(WaxedLumberChair);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedLumberChair.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Lumber Chair")]
    [Ecopedia("Housing Objects", "Seating", subPageName: "LumberChair Item")]
    [Tag("Housing", 1)]
    public class WaxedLumberChair : WorldObjectItem<WaxedLumberChairObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A nice, sturdy waxed lumber chair.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 1.25f,
            TypeForRoomLimit = Localizer.DoStr("Chair"),
            DiminishingReturnPercent = 0.4f
        };

        static WaxedLumberChair()
        {
            WorldObject.AddOccupancy<WaxedLumberChairObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedLumberChairRecipe : RecipeFamily
    {
        public WaxedLumberChairRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedLumberChair", Localizer.DoStr("Waxed Lumber Chair"), new IngredientElement[2]
        {
          new IngredientElement(typeof (LumberChairItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedLumberChair>()
        })
      };
            ExperienceOnCraft = 2.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedLumberChairRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Lumber Chair"), typeof(WaxedLumberChairRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedHardwoodLumberChairRecipe : Recipe
    {
        public WaxedHardwoodLumberChairRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberChairRecipe), new Recipe("Waxed Lumber Chair", Localizer.DoStr("Waxed Lumber Chair"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HardwoodLumberChairItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 1f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberChair>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedSoftwoodLumberChairRecipe : Recipe
    {
        public WaxedSoftwoodLumberChairRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberChairRecipe), new Recipe("Waxed Lumber Chair", Localizer.DoStr("Waxed Lumber Chair"), new IngredientElement[2]
            {
                new IngredientElement(typeof (SoftwoodLumberChairItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 1f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberChair>()
            }));
        }
    }
}
