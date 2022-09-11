// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractMecanicalItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Mechanical Extractor")]
  [AllowPluginModules(ItemTypes = new Type[] {typeof (MechanicsAdvancedUpgradeItem)}, Tags = new string[] {"AdvancedUpgrade"})]
  public class OnduHoneyExtractMecanicalItem : WorldObjectItem<OnduHoneyExtractMecanicalObject>, IPersistentData
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Mechanical Extractor. For The Honey");
      }
    }

    static OnduHoneyExtractMecanicalItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 1);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr(""));
      OnduHoneyExtractMecanicalItem.HomeValue = homeFurnishingValue;
      WorldObject.AddOccupancy<OnduHoneyExtractMecanicalObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return OnduHoneyExtractMecanicalItem.HomeValue;
      }
    }

    [Eco.Gameplay.Systems.Tooltip.Tooltip(7, new Type[] {})]
    private LocString PowerConsumptionTooltip
    {
      get
      {
        return Localizer.Do(FormattableStringFactory.Create("Consumes: {0}w of {1} power", (object) Text.Info((object) 200), (object) new MechanicalPower().Name));
      }
    }

    [Serialized]
    [TooltipChildren(new Type[] {})]
    public object PersistentData { get; set; }
  }
}
