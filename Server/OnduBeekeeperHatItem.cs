// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBeekeeperHatItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Beekeeper Hat")]
  [StartsDiscovered]
  [Weight(100)]
  [Tag("Clothes", 1)]
  public class OnduBeekeeperHatItem : ClothingItem
  {
    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Beekeeper Hat. Protect yourself some bees");
      }
    }

    public override string Slot
    {
      get
      {
        return "Head";
      }
    }

    public override bool Starter
    {
      get
      {
        return false;
      }
    }
  }
}
