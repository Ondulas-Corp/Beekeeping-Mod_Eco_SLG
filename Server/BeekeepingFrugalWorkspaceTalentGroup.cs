// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.BeekeepingFrugalWorkspaceTalentGroup
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Diagnostics;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Frugal Workspace: Beekeeping")]
    public class BeekeepingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayDescription { get; } = Localizer.DoStr("Lowers the tier requirement of related tables by 0.2");

        public BeekeepingFrugalWorkspaceTalentGroup()
        {
            this.Talents = new Type[1]
            {
        typeof (BeekeepingFrugalReqTalent)
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 6;
        }
    }
}
