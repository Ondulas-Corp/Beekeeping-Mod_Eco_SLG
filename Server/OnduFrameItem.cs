// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFrameItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Frame")]
  [Weight(400)]
  public class OnduFrameItem : Item
  {
    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Frame. To contain the wax. ");
      }
    }
  }
}
