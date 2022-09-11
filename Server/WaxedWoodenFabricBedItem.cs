// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenFabricBedItem
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
  [LocDisplayName("Waxed Wooden Fabric Bed")]
  [Tag("Housing", 1)]
  public class WaxedWoodenFabricBedItem : WorldObjectItem<WaxedWoodenFabricBedObject>
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("A much more comfortable bed made with fabric.");
      }
    }

    static WaxedWoodenFabricBedItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 3);
      homeFurnishingValue.SkillValue = (3.5f);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr("Bed"));
      homeFurnishingValue.DiminishingReturnPercent = (0.2f);
      WaxedWoodenFabricBedItem.HomeValue = homeFurnishingValue;
      WorldObject.AddOccupancy<WaxedWoodenFabricBedObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0)),
        new BlockOccupancy(new Vector3i(1, 0, 1)),
        new BlockOccupancy(new Vector3i(0, 0, 1)),
        new BlockOccupancy(new Vector3i(0, 0, 2)),
        new BlockOccupancy(new Vector3i(1, 0, 2)),
        new BlockOccupancy(new Vector3i(1, 1, 2)),
        new BlockOccupancy(new Vector3i(0, 1, 2)),
        new BlockOccupancy(new Vector3i(0, 1, 1)),
        new BlockOccupancy(new Vector3i(1, 1, 1)),
        new BlockOccupancy(new Vector3i(1, 1, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0)),
        new BlockOccupancy(new Vector3i(1, 0, 0))
      });
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return WaxedWoodenFabricBedItem.HomeValue;
      }
    }
  }
}
