// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduInnocupiedSauvageBeehiveObject
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [RequireComponent(typeof (PropertyAuthComponent), null)]
  [RequireComponent(typeof (SolidGroundComponent), null)]
  [RequireComponent(typeof (QueenBeeComponent), null)]
  [RequireComponent(typeof (PluginModulesComponent), null)]
  public class OnduInnocupiedSauvageBeehiveObject : WorldObject, IRepresentsItem
  {
    public override LocString DisplayName
    {
      get
      {
        return Localizer.DoStr("Vacant Swarm");
      }
    }

    public virtual Type RepresentedItemType
    {
      get
      {
        return typeof (OnduInnocupiedSauvageBeehiveItem);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    public override void Destroy()
    {
      base.Destroy();
    }
  }
}
