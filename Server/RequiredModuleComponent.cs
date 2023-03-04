// Decompiled with JetBrains decompiler
// Type: Eco.Gameplay.Components.RequiredModuleComponent
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    [RequireComponent(typeof(ChunkSubscriberComponent), null)]
    public class RequiredModuleComponent : WorldObjectComponent
    {
        [Serialized]
        private bool isPresent = false;
        private StatusElement status;

        public override bool Enabled
        {
            get
            {
                return isPresent;
            }
        }

        public override void Initialize()
        {
            Parent.GetComponent<PluginModulesComponent>(null).OnChanged.Add(new Action(IsBeePresent));
            status = Parent.GetComponent<StatusComponent>(null).CreateStatusElement(-40);
            IsBeePresent();
            UpdateStatus();
        }

        public void IsBeePresent()
        {
            bool flag = !Parent.GetComponent<PluginModulesComponent>(null).Inventory.IsEmpty;
            if (isPresent == flag)
                return;
            isPresent = flag;
            UpdateStatus();
            Parent.UpdateEnabledAndOperating();
            Parent.SetDirty();
        }

        private void UpdateStatus()
        {
            status?.SetStatusMessage(isPresent, Localizer.DoStr("No does not work if the hive does not have a module. You can find a queen bee in swarms that spawn in nature "));
        }
    }
}
