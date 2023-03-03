using Eco.Mods.TechTree;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    public class BeekeepingFrugalReqTalent : FrugalWorkspaceTalent
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
                return typeof(BeekeepingFrugalWorkspaceTalentGroup);
            }
        }

        public BeekeepingFrugalReqTalent()
        {
            Value = -0.2f;
        }
    }
}
