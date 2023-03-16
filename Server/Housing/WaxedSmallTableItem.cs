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
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Small Table Item")]
    public class WaxedSmallTableObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Small Table");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedSmallTableItem);

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedSmallTableItem.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Small Table")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    public class WaxedSmallTableItem : WorldObjectItem<WaxedSmallTableObject>
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

        static WaxedSmallTableItem()
        {
            WorldObject.AddOccupancy<WaxedHewnChairObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Small Table Item")]
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
           new CraftingElement<WaxedSmallTableItem>(1f)
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
