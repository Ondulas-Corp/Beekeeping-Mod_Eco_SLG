using Eco.Core.Items;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Property;
using Eco.Shared.Items;
using System;
using System;
using System.Collections.Generic;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;

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
    public class WaxedLumberBenchObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Lumber Bench");
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
                return typeof(WaxedLumberBench);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedLumberBench.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Lumber Bench")]
    [Ecopedia("Housing Objects", "Seating", subPageName: "LumberBench Item")]
    [Tag("Housing", 1)]
    public class WaxedLumberBench : WorldObjectItem<WaxedLumberBenchObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A sturdy waxed lumber bench. It doesn't feel as wobbly as more basic wooden benches.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 2f,
            TypeForRoomLimit = Localizer.DoStr("Seating"),
            DiminishingReturnPercent = 0.5f
        };

        static WaxedLumberBench()
        {
            WorldObject.AddOccupancy<WaxedLumberBenchObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(-1, 0, 0))
      });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedLumberBenchRecipe : RecipeFamily
    {
        public WaxedLumberBenchRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedLumberBench", Localizer.DoStr("Waxed Lumber Bench"), new IngredientElement[2]
        {
          new IngredientElement(typeof (LumberBenchItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedLumberBench>(1f)
        })
      };
            ExperienceOnCraft = 1.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedLumberBenchRecipe), 1f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Lumber Bench"), typeof(WaxedLumberBenchRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedHardwoodLumberBenchRecipe : Recipe
    {
        public WaxedHardwoodLumberBenchRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberBenchRecipe), new Recipe("Waxed Lumber Bench", Localizer.DoStr("Waxed Lumber Bench"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HardwoodLumberBenchItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberBench>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 7)]
    public class WaxedSoftwoodLumberBenchRecipe : Recipe
    {
        public WaxedSoftwoodLumberBenchRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedLumberBenchRecipe), new Recipe("Waxed Lumber Bench", Localizer.DoStr("Waxed Lumber Bench"), new IngredientElement[2]
            {
                new IngredientElement(typeof (SoftwoodLumberBenchItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedLumberBench>()
            }));
        }
    }
}
