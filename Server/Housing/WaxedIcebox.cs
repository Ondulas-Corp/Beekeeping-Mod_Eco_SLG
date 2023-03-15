using Eco.Core.Items;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Shared.Items;
using System;
using System.Collections.Generic;
using Eco.Gameplay.Skills;

namespace Beekeeping.Server.Housing
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(LinkComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireComponent(typeof(PublicStorageComponent), null)]
    [Ecopedia("Housing Objects", "Kitchen", subPageName: "Icebox Item")]
    public class WaxedIceboxObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Icebox");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedIcebox);

        protected override void Initialize()
        {
            //GetComponent<HousingComponent>(null).HomeValue = WaxedIceboxItem.HomeValue;
            PublicStorageComponent component = GetComponent<PublicStorageComponent>(null);
            component.Initialize(16);
            component.ShelfLifeMultiplier = 1.2f;
            component.Storage.AddInvRestriction(new FoodStorageRestriction());
            component.Storage.AddInvRestriction(new StackLimitRestriction(200));
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Icebox")]
    [Ecopedia("Housing Objects", "Kitchen", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedIcebox : WorldObjectItem<WaxedIceboxObject>
    {
        [Tooltip(50)]
        public TooltipSection UpdateTooltip()
            => new TooltipSection(Localizer.Do($"{Localizer.DoStr("Increases")} total shelf life by: {Text.InfoLight(Text.Percent(0.2f))}").Dash());

        public override LocString DisplayDescription => Localizer.DoStr("A waxed box of ice. It's in the name!");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Kitchen"),
            HouseValue = 2f,
            TypeForRoomLimit = Localizer.DoStr("Food Storage"),
            DiminishingReturnPercent = 0.3f
        };

        static WaxedIcebox()
        {
            WorldObject.AddOccupancy<WaxedIceboxObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(-1, 0, 0)),
                new BlockOccupancy(new Vector3i(-1, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedIceboxRecipe : RecipeFamily
    {
        public WaxedIceboxRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("Waxed Icebox", Localizer.DoStr("WaxedIcebox"), new IngredientElement[2]
        {
          new IngredientElement(typeof (IceboxItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedIcebox>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedIceboxRecipe), 2f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Icebox"), typeof(WaxedIceboxRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
