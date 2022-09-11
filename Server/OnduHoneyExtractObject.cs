// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduHoneyExtractObject
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [RequireComponent(typeof (PropertyAuthComponent), null)]
  [RequireComponent(typeof (LinkComponent), null)]
  [RequireComponent(typeof (CraftingComponent), null)]
  [RequireComponent(typeof (HousingComponent), null)]
  [RequireComponent(typeof (SolidGroundComponent), null)]
  [RequireComponent(typeof (PluginModulesComponent), null)]
  [RequireComponent(typeof (RoomRequirementsComponent), null)]
  [RequireRoomContainment]
  [RequireRoomVolume(25)]
  [RequireRoomMaterialTier(0.8f, new Type[] {typeof (BeekeepingLavishReqTalent), typeof (BeekeepingFrugalReqTalent)})]
  public class OnduHoneyExtractObject : WorldObject, IRepresentsItem
  {
    public override LocString DisplayName
    {
      get
      {
        return Localizer.DoStr("Manual Extractor");
      }
    }

    public virtual Type RepresentedItemType
    {
      get
      {
        return typeof (OnduHoneyExtractItem);
      }
    }

    protected override void Initialize()
    {
      this.GetComponent<HousingComponent>((string) null).HomeValue = OnduHoneyExtractItem.HomeValue;
    }

    public override void Destroy()
    {
      base.Destroy();
    }
  }
}
