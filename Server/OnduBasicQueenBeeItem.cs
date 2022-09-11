// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduBasicQueenBeeItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Basic Queen Bee")]
  [Weight(1)]
  [Tag("QueenBee", 1)]
  public class OnduBasicQueenBeeItem : EfficiencyModule
  {
    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Basic Queen bee. Found in wild beehives.");
      }
    }

    public OnduBasicQueenBeeItem() :
            base(
                  ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                  0.7f,
                  typeof(BeekeepingSkill),
                  0.7f
                  ) { }
        }
}
