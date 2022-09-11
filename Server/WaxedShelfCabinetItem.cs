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
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Waxed Shelf Cabinet")]
  [Tag("Housing", 1)]
  public class WaxedShelfCabinetItem : WorldObjectItem<WaxedShelfCabinetObject>
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("When a shelf and a cabinet aren't enough individually.");
      }
    }

    static WaxedShelfCabinetItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 0);
      homeFurnishingValue.SkillValue = (2.5f);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr("Shelves"));
      homeFurnishingValue.DiminishingReturnPercent = (0.5f);
      WaxedShelfCabinetItem.HomeValue = homeFurnishingValue;
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return WaxedShelfCabinetItem.HomeValue;
      }
    }
  }
}
