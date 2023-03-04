// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedHewnBenchItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Waxed Hewn Bench")]
    [Tag("Housing", 1)]
    public class WaxedHewnBenchItem : WorldObjectItem<WaxedHewnBenchObject>
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("A waxed basic wooden bench.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static WaxedHewnBenchItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Seating");
            homeFurnishingValue.HouseValue = 1.5f;
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("Seating");
            homeFurnishingValue.DiminishingReturnPercent = 0.5f;
            HomeValue = homeFurnishingValue;
            WorldObject.AddOccupancy<WaxedHewnBenchObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(-1, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 0, 0))
      });
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;
    }
}
