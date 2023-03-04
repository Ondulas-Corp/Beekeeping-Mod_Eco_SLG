// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedShelfCabinetItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Waxed Shelf Cabinet")]
    [Ecopedia("Housing Objects", "Seating", createAsSubPage: true)]
    [Tag("Housing", 1)]
    public class WaxedShelfCabinetItem : WorldObjectItem<WaxedShelfCabinetObject>
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("When a shelf and a cabinet aren't enough individually.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static WaxedShelfCabinetItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Seating");
            homeFurnishingValue.HouseValue = 2.5f;
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("Shelves");
            homeFurnishingValue.DiminishingReturnPercent = 0.5f;
            HomeValue = homeFurnishingValue;
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;
    }
}
