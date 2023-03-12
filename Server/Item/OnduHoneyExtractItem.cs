// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Module;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Manual Extractor")]
    [AllowPluginModules(ItemTypes = new Type[] { typeof(BasicUpgradeLvl1Item), typeof(BasicUpgradeLvl2Item), typeof(BasicUpgradeLvl3Item), typeof(BasicUpgradeLvl4Item) })]
    public class OnduHoneyExtractItem : WorldObjectItem<OnduHoneyExtractObject>, IPersistentData
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("Manual Extractor. Filters the honey and makes it ripe");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static OnduHoneyExtractItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Industrial");
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("");
            HomeValue = homeFurnishingValue;
            WorldObject.AddOccupancy<OnduHoneyExtractObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;

        [Serialized]
        [TooltipChildren(new Type[] { })]
        public object PersistentData { get; set; }
    }
}
