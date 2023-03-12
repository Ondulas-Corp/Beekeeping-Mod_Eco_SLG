// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenFloorLampItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.HousingObject;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Beekeeping.Server.HousingItem
{
    [Serialized]
    [LocDisplayName("Waxed Wooden Floor Lamp")]
    [Tag("Housing", 1)]
    public class WaxedWoodenFloorLampItem : WorldObjectItem<WaxedWoodenFloorLampObject>
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("A more modern way to light up a room. This time from the floor.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static WaxedWoodenFloorLampItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Lighting");
            homeFurnishingValue.HouseValue = 2.5f;
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("Lights");
            homeFurnishingValue.DiminishingReturnPercent = 0.7f;
            HomeValue = homeFurnishingValue;
            WorldObject.AddOccupancy<WaxedWoodenFloorLampObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;

        [Tooltip(7, new Type[] { })]
        private LocString PowerConsumptionTooltip => Localizer.Do(FormattableStringFactory.Create("Consumes: {0}w of {1} power", Text.Info(60), new ElectricPower().Name));
    }
}
