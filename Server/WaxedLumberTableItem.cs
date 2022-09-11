// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedLumberTableItem
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

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Waxed Lumber Table")]
  [Tag("Housing", 1)]
  public class WaxedLumberTableItem : WorldObjectItem<WaxedLumberTableObject>
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("A nice, sturdy waxed lumber table.");
      }
    }

    static WaxedLumberTableItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 0);
      homeFurnishingValue.SkillValue = (2.5f);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr("Table"));
      homeFurnishingValue.DiminishingReturnPercent = (0.6f);
      WaxedLumberTableItem.HomeValue = homeFurnishingValue;
      WorldObject.AddOccupancy<WaxedLumberTableObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(-1, 0, 0))
      });
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return WaxedLumberTableItem.HomeValue;
      }
    }
  }
}
