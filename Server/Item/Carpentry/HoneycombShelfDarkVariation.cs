namespace Beekeeping.Server
{
    using System;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

    // Honeycomb Shelf 1 Dark
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    public partial class HoneycombShelf1DarkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelf1DarkItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf 1 Dark");
        protected override void Initialize() { base.Initialize(); }
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf 1 Dark")]
    [LocDescription("A small dark wood shelf with 1 hexagonal compartment, displaying a golden honeycomb.")]
    [Weight(150)]
    public partial class HoneycombShelf1DarkItem : WorldObjectItem<HoneycombShelf1DarkObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 0.8f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }

    // Honeycomb Shelf 2 Left Dark
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    public partial class HoneycombShelf2LeftDarkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelf2LeftDarkItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf 2 Left Dark");
        protected override void Initialize() { base.Initialize(); }
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf 2 Left Dark")]
    [LocDescription("A compact dark wood shelf with 2 hexagonal compartments on the left, displaying golden honeycombs.")]
    [Weight(250)]
    public partial class HoneycombShelf2LeftDarkItem : WorldObjectItem<HoneycombShelf2LeftDarkObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 1.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }

    // Honeycomb Shelf 2 Right Dark
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    public partial class HoneycombShelf2RightDarkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelf2RightDarkItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf 2 Right Dark");
        protected override void Initialize() { base.Initialize(); }
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf 2 Right Dark")]
    [LocDescription("A compact dark wood shelf with 2 hexagonal compartments on the right, displaying golden honeycombs.")]
    [Weight(250)]
    public partial class HoneycombShelf2RightDarkItem : WorldObjectItem<HoneycombShelf2RightDarkObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 1.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }

    // Honeycomb Shelf 3 Left Dark
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    public partial class HoneycombShelf3LeftDarkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelf3LeftDarkItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf 3 Left Dark");
        protected override void Initialize() { base.Initialize(); }
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf 3 Left Dark")]
    [LocDescription("A dark wood shelf with 3 hexagonal compartments on the left, displaying golden honeycombs.")]
    [Weight(350)]
    public partial class HoneycombShelf3LeftDarkItem : WorldObjectItem<HoneycombShelf3LeftDarkObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 1.6f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }	
    // Honeycomb Shelf 5 Dark
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    public partial class HoneycombShelfDarkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(HoneycombShelfDarkItem);
        public override LocString DisplayName => Localizer.DoStr("Honeycomb Shelf Dark");
        protected override void Initialize() { base.Initialize(); }
    }

    [Serialized]
    [LocDisplayName("Honeycomb Shelf Dark")]
    [LocDescription("A dark wood shelf displaying beautiful golden honeycombs. Perfect for showing off your beekeeping achievements.")]
    [Weight(500)]
    public partial class HoneycombShelfDarkItem : WorldObjectItem<HoneycombShelfDarkObject>
    {
		protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Backward , WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            Category = HousingConfig.GetRoomCategory("Living Room"),
            BaseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.5f
        };
    }
}