// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedWoodenFabricBedObject
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(MountComponent), null)]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomMaterialTier(1.8f, new Type[] { })]
    public class WaxedWoodenFabricBedObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName
        {
            get
            {
                return Localizer.DoStr("Waxed Wooden Fabric Bed");
            }
        }

        public virtual TableTextureMode TableTexture
        {
            get
            {
                return (TableTextureMode)1;
            }
        }

        public virtual Type RepresentedItemType
        {
            get
            {
                return typeof(WaxedWoodenFabricBedItem);
            }
        }

        protected override void Initialize()
        {
            GetComponent<HousingComponent>(null).HomeValue = WaxedWoodenFabricBedItem.HomeValue;
            GetComponent<MountComponent>(null).Initialize(1);
        }
    }
}
