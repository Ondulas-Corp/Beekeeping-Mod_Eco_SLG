// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractMecanicalItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Beekeeping.Server.Module;
using Eco.Gameplay.Components;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Mechanical Extractor")]
    [AllowPluginModules(ItemTypes = new Type[] { typeof(AdvancedUpgradeLvl1Item), typeof(AdvancedUpgradeLvl2Item), typeof(AdvancedUpgradeLvl3Item), typeof(AdvancedUpgradeLvl4Item) })]
    public class OnduHoneyExtractMecanicalItem : WorldObjectItem<OnduHoneyExtractMecanicalObject>, IPersistentData
    {
        public static readonly HomeFurnishingValue HomeValue;

        public override LocString DisplayDescription => Localizer.DoStr("Mechanical Extractor. For The Honey");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
        static OnduHoneyExtractMecanicalItem()
        {
            HomeFurnishingValue homeFurnishingValue = new HomeFurnishingValue();
            homeFurnishingValue.Category = HousingConfig.GetRoomCategory("Industrial");
            homeFurnishingValue.TypeForRoomLimit = Localizer.DoStr("");
            HomeValue = homeFurnishingValue;
            WorldObject.AddOccupancy<OnduHoneyExtractMecanicalObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
        }

        [TooltipChildren(new Type[] { })]
        public HomeFurnishingValue HousingTooltip => HomeValue;

        [Tooltip(7, new Type[] { })]
        private LocString PowerConsumptionTooltip => Localizer.Do(FormattableStringFactory.Create("Consumes: {0}w of {1} power", Text.Info(200), new MechanicalPower().Name));

        [Serialized]
        [TooltipChildren(new Type[] { })]
        public object PersistentData { get; set; }
    }
}
