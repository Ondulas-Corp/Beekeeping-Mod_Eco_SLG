// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedIceboxItem
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
    [LocDisplayName("Waxed Icebox")]
    [Ecopedia("Housing Objects", "Kitchen", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedIceboxItem : WorldObjectItem<WaxedIceboxObject>
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("A waxed box of ice. It's in the name!");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static WaxedIceboxItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Kitchen");
            homeFurnishingValue.HouseValue = 2f;
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("Food Storage");
            homeFurnishingValue.DiminishingReturnPercent = 0.3f;
            HomeValue = homeFurnishingValue;
            WorldObject.AddOccupancy<WaxedIceboxObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(-1, 0, 0)),
        new BlockOccupancy(new Vector3i(-1, 1, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;
    }
}
