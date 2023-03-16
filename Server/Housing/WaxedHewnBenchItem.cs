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
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed HewnBench Item")]
    public class WaxedHewnBenchObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Waxed Hewn Bench");

        public virtual TableTextureMode TableTexture => (TableTextureMode)1;

        public virtual Type RepresentedItemType => typeof(WaxedHewnBenchItem);

        protected override void Initialize()
        {
            GetComponent<HousingComponent>().HomeValue = WaxedHewnBenchItem.homeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Hewn Bench")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedHewnBenchItem : WorldObjectItem<WaxedHewnBenchObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A waxed basic wooden bench.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Seating"),
            DiminishingReturnPercent = 0.5f
        };

        static WaxedHewnBenchItem()
        {
            WorldObject.AddOccupancy<WaxedHewnBenchObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(-1, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed HewnBench Item")]
    public class WaxedHewnBenchRecipe : RecipeFamily
    {
        public WaxedHewnBenchRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedHewnBench", Localizer.DoStr("Waxed Hewn Bench"), new IngredientElement[2]
        {
          new IngredientElement(typeof (HewnBenchItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedHewnBenchItem>(1f)
        })
      };
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedHewnBenchRecipe), 4f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Hewn Bench"), typeof(WaxedHewnBenchRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Hardwood HewnBench Item")]
    public class WaxedHardwoodHewnBenchRecipe : Recipe
    {
        public WaxedHardwoodHewnBenchRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnBenchRecipe), new Recipe("Waxed Hewn Bench", Localizer.DoStr("Waxed Hewn Bench"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HewnHardwoodBenchItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnBenchItem>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Housing Objects", "Seating", subPageName: "Waxed Softwood HewnBench Item")]
    public class WaxedSoftwoodHewnBenchRecipe : Recipe
    {
        public WaxedSoftwoodHewnBenchRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnBenchRecipe), new Recipe("Waxed Hewn Bench", Localizer.DoStr("Waxed Hewn Bench"), new IngredientElement[2]
            {
                new IngredientElement(typeof (HewnSoftwoodBenchItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnBenchItem>()
            }));
        }
    }
}
