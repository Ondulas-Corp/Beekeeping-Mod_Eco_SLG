// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBreedingBeehiveItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Breeding Hive")]
  [AllowPluginModules(ItemTypes = new Type[] {typeof (OnduQueenBeeItem), typeof (OnduAdvancedQueenBeeItem), typeof (OnduBasicQueenBeeItem), typeof (OnduModernQueenBeeItem)})]
  public class OnduBreedingBeehiveItem : WorldObjectItem<OnduBreedingBeehiveObject>
  {
    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Breeding hive. It product some honey.");
      }
    }

    static OnduBreedingBeehiveItem()
    {
      WorldObject.AddOccupancy<OnduBreedingBeehiveObject>(new List<BlockOccupancy>()
      {
        new BlockOccupancy(new Vector3i(0, 0, 0)),
        new BlockOccupancy(new Vector3i(0, 1, 0))
      });
    }
  }
}
