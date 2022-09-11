// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduEmptyPotObject
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
  public class OnduEmptyPotObject : WorldObject, IRepresentsItem
  {
    public override LocString DisplayName
    {
      get
      {
        return Localizer.DoStr("Empty Pot");
      }
    }

    public virtual Type RepresentedItemType
    {
      get
      {
        return typeof (OnduEmptyPotObject);
      }
    }

    public override void Destroy()
    {
      base.Destroy();
    }
  }
}
