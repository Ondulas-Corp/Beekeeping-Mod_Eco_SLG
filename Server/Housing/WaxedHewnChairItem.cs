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
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireComponent(typeof(MountComponent), null)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed HewnChair Item")]
    public class WaxedHewnChairObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Hewn Chair");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedHewnChairItem);

        protected override void Initialize()
        {
            this.GetComponent<HousingComponent>().HomeValue = WaxedHewnChairItem.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Hewn Chair")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedHewnChairItem : WorldObjectItem<WaxedHewnChairObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A raised surface supported by legs. Without the back, it might be a stool.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 0.75f,
            TypeForRoomLimit = Localizer.DoStr("Chair"),
            DiminishingReturnPercent = 0.7f
        };
        static WaxedHewnChairItem()
        {
            WorldObject.AddOccupancy<WaxedHewnChairObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed HewnChair Item")]
    public class WaxedHewnChairRecipe : RecipeFamily
    {
        public WaxedHewnChairRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedHewnChair", Localizer.DoStr("Waxed Hewn Chair"), new IngredientElement[2]
        {
          new IngredientElement(typeof (HewnChairItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 1f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedHewnChairItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedHewnChairRecipe), 3f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Hewn Chair"), typeof(WaxedHewnChairRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Hardwood HewnChair Item")]
    public class WaxedHardwoodHewnChairRecipe : Recipe
    {
        public WaxedHardwoodHewnChairRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnChairRecipe), new Recipe("Waxed Hewn Chair", Localizer.DoStr("Waxed Hewn Chair"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HewnHardwoodChairItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 1f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnChairItem>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Softwood HewnChair Item")]
    public class WaxedSoftwoodHewnChairRecipe : Recipe
    {
        public WaxedSoftwoodHewnChairRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnChairRecipe), new Recipe("Waxed Hewn Chair", Localizer.DoStr("Waxed Hewn Chair"), new IngredientElement[2]
            {
                new IngredientElement(typeof (HewnSoftwoodChairItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 1f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnChairItem>()
            }));
        }

    }
}
