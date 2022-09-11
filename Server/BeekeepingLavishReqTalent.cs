// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.BeekeepingLavishReqTalent
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
    [Serialized]
    public class BeekeepingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base
        {
            get
            {
                return false;
            }
        }

        public override Type TalentGroupType
        {
            get
            {
                return typeof(BeekeepingLavishWorkspaceTalentGroup);
            }
        }

        public BeekeepingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
}
