// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSmallHiveItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Small Hive")]
  [AllowPluginModules(ItemTypes = new Type[] {typeof (OnduQueenBeeItem), typeof (OnduAdvancedQueenBeeItem), typeof (OnduBasicQueenBeeItem), typeof (OnduModernQueenBeeItem)})]
  public class OnduSmallHiveItem : WorldObjectItem<OnduSmallHiveObject>
  {
    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Small hive. Made for queen bees.");
      }
    }
  }
}
