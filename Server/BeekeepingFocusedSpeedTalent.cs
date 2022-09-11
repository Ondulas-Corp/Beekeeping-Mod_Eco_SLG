using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
    [Serialized]
    public class BeekeepingFocusedSpeedTalent : FocusedWorkflowTalent
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
                return typeof(BeekeepingFocusedWorkflowTalentGroup);
            }
        }

        public BeekeepingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
}
