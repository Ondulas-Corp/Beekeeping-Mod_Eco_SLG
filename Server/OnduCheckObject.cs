// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCheckObject
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  public class OnduCheckObject : WorldObject, IRepresentsItem
  {
    public override LocString DisplayName
    {
      get
      {
        return Localizer.DoStr("Check Spawn");
      }
    }

    public virtual Type RepresentedItemType
    {
      get
      {
        return typeof (OnduCheckObject.OnduCheckItem);
      }
    }

    [Serialized]
    [LocDisplayName("Check Spawn")]
    public class OnduCheckItem : WorldObjectItem<OnduCheckObject>
    {
      public override LocString DisplayDescription
      {
        get
        {
          return Localizer.DoStr("you're not supposed to see that.");
        }
      }
    }
  }
}
