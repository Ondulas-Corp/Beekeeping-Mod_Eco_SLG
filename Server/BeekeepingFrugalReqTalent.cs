using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
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
            this.Value = -0.2f;
        }
    }
}
