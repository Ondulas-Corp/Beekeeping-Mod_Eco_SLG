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
    public class WaxedHewnTableObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Hewn Table");
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
                return typeof(WaxedHewnTable);
            }
        }

        protected override void Initialize()
        {
            this.GetComponent<HousingComponent>().HomeValue = WaxedHewnTable.homeValue;
        }
    }

    [Serialized]
    [LocDisplayName("Waxed Hewn Table")]
    [Tag("Housing", 1)]
    public class WaxedHewnTable : WorldObjectItem<WaxedHewnTableObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A large waxed table for placing things on.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;

        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Seating"),
            HouseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Table"),
            DiminishingReturnPercent = 0.6f
        };

        static WaxedHewnTable()
        {
            WorldObject.AddOccupancy<WaxedHewnTableObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(-1, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 0))
            });
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedHewnTableRecipe : RecipeFamily
    {
        public WaxedHewnTableRecipe()
        {

            Recipes = new List<Recipe>()
      {
        new Recipe("WaxedHewnTable", Localizer.DoStr("Waxed Hewn Table"), new IngredientElement[2]
        {
          new IngredientElement(typeof (HewnTableItem), 1f, false),
          new IngredientElement(typeof (OnduWaxItem), 2f, false)
        }, new CraftingElement[1]
        {
           new CraftingElement<WaxedHewnTable>(1f)
        })
      };
            ExperienceOnCraft = 2.0f;
            LaborInCalories = CreateLaborInCaloriesValue(100f, typeof(CarpentrySkill));
            CraftMinutes = CreateCraftTimeValue(typeof(WaxedHewnTableRecipe), 4f, typeof(CarpentrySkill), new Type[2]
            {
        typeof (CarpentryFocusedSpeedTalent),
        typeof (CarpentryParallelSpeedTalent)
            });
            Initialize(Localizer.DoStr("Waxed Hewn Table"), typeof(WaxedHewnTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedHardwoodHewnTableRecipe : Recipe
    {
        public WaxedHardwoodHewnTableRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnTableRecipe), new Recipe("Waxed Hewn Table", Localizer.DoStr("Waxed Hewn Table"), new IngredientElement[2]
            {
        new IngredientElement(typeof (HewnHardwoodTableItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnTable>(1f)
            }));
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    public class WaxedSoftwoodHewnTableRecipe : Recipe
    {
        public WaxedSoftwoodHewnTableRecipe()
        {
            CraftingComponent.AddTagProduct(typeof(SawmillObject), typeof(WaxedHewnTableRecipe), new Recipe("Waxed Hewn Table", Localizer.DoStr("Waxed Hewn Table"), new IngredientElement[2]
            {
                new IngredientElement(typeof (HewnSoftwoodTableItem), 1f, false),
                new IngredientElement(typeof (OnduWaxItem), 2f, false)
            }, new CraftingElement[1]
            {
         new CraftingElement<WaxedHewnTable>()
            }));
        }
    }
}
