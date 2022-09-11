// Decompiled with JetBrains decompiler
// Type: Eco.Gameplay.Components.RequiredModuleComponent
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Gameplay.Components
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
                return this.isPresent;
            }
        }

        public override void Initialize()
        {
            ((ThreadSafeActionBase<Action>)this.Parent.GetComponent<PluginModulesComponent>((string)null).OnChanged).Add(new Action(this.IsBeePresent));
            this.status = this.Parent.GetComponent<StatusComponent>((string)null).CreateStatusElement(-40);
            this.IsBeePresent();
            this.UpdateStatus();
        }

        public void IsBeePresent()
        {
            bool flag = !this.Parent.GetComponent<PluginModulesComponent>((string)null).Inventory.IsEmpty;
            if (this.isPresent == flag)
                return;
            this.isPresent = flag;
            this.UpdateStatus();
            this.Parent.UpdateEnabledAndOperating();
            this.Parent.SetDirty();
        }

        private void UpdateStatus()
        {
            this.status?.SetStatusMessage(this.isPresent, Localizer.DoStr("No does not work if the hive does not have a module. You can find a queen bee in swarms that spawn in nature "));
        }
    }
}
