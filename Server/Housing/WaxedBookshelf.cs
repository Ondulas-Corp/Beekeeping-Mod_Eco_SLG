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

namespace Beekeeping.Server.Housing
{

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(LinkComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(PublicStorageComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedBookshelfObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Bookshelf");
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
                return typeof(WaxedBookshelf);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedBookshelf.homeValue;
            PublicStorageComponent component = GetComponent<PublicStorageComponent>(null);
            component.Initialize(9);
            component.Storage.AddInvRestriction(new NotCarriedRestriction());
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Bookshelf")]
    [Tag("Housing", 1)]
    public class WaxedBookshelf : WorldObjectItem<WaxedBookshelfObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A place to store knowledge and information; leads to the town hall.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            HouseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Shelves"),
            DiminishingReturnPercent = 0.5f
        };

        static WaxedBookshelf()
        {
            // TODO : Gérer l'occupancy
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedBookshelfRecipe : RecipeFamily
    {
        public WaxedBookshelfRecipe()
        {
            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedBookshelf", Localizer.DoStr("Waxed Bookshelf"), new IngredientElement[2]
        {
          new IngredientElement(typeof (BookshelfItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedBookshelf>(1f)
        })
      };
            ExperienceOnCraft = 10.0f;
            LaborInCalories = CreateLaborInCaloriesValue(200f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedBookshelfRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Bookshelf"), typeof(WaxedBookshelfRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
