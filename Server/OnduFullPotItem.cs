// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFullPotItem
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
  [LocDisplayName("Honey Pot")]
  [Weight(100)]
  public class OnduFullPotItem : WorldObjectItem<OnduFullPotObject>
  {
    public static readonly HomeFurnishingValue HomeValue;

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("A pot fulled of honey.");
      }
    }

    static OnduFullPotItem()
    {
      HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
      homeFurnishingValue.Category = ((HomeFurnishingValue.RoomCategory) 0);
      homeFurnishingValue.SkillValue = (0.5f);
      homeFurnishingValue.TypeForRoomLimit = (Localizer.DoStr("HoneyPot"));
      homeFurnishingValue.DiminishingReturnPercent = (0.7f);
      OnduFullPotItem.HomeValue = homeFurnishingValue;
    }

    [TooltipChildren(new Type[] {})]
    public HomeFurnishingValue HousingTooltip
    {
      get
      {
        return OnduFullPotItem.HomeValue;
      }
    }
  }
}
