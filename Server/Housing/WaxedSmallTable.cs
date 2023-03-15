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

namespace Beekeeping.Server.Housing
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedSmallTableObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Small Table");
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
                return typeof(WaxedSmallTable);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedSmallTable.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Small Table")]
    public class WaxedSmallTable : WorldObjectItem<WaxedSmallTableObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("More of a nightstand than a table, really.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 1f,
            TypeForRoomLimit = Localizer.DoStr("Table"),
            DiminishingReturnPercent = 0.6f
        };

        static WaxedSmallTable()
        {
            // TODO : Gérer occupancy
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedSmallTableRecipe : RecipeFamily
    {
        public WaxedSmallTableRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedSmallTable", Localizer.DoStr("Waxed Small Table"), new IngredientElement[3]
        {
          new IngredientElement(typeof (LumberItem), 2f, false),
          new IngredientElement(typeof (NailItem), 3f, false),
          new IngredientElement(typeof (OnduWaxItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedSmallTable>(1f)
        })
      };
            ExperienceOnCraft = 3.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedSmallTableRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Small Table"), typeof(WaxedSmallTableRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
