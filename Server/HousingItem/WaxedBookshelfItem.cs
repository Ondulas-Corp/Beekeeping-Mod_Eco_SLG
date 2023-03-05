// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedBookshelfItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.HousingObject;
using Eco.Core.Items;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server.HousingItem
{
    [Serialized]
    [LocDisplayName("Waxed Bookshelf")]
    [Tag("Housing", 1)]
    public class WaxedBookshelfItem : WorldObjectItem<WaxedBookshelfObject>
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("A place to store knowledge and information; leads to the town hall.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static WaxedBookshelfItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Living Room");
            homeFurnishingValue.HouseValue = 2.5f;
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("Shelves");
            homeFurnishingValue.DiminishingReturnPercent = 0.5f;
            HomeValue = homeFurnishingValue;
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;
    }
}
