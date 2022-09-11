// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedSmallTableItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Waxed Small Table")]
  public class WaxedSmallTableItem : WorldObjectItem<WaxedSmallTableObject>
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("More of a nightstand than a table, really.");
      }
    }

    static WaxedSmallTableItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 0);
      homeFurnishingValue.SkillValue = (1f);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr("Table"));
      homeFurnishingValue.DiminishingReturnPercent = (0.6f);
      WaxedSmallTableItem.HomeValue = homeFurnishingValue;
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return WaxedSmallTableItem.HomeValue;
      }
    }
  }
}
