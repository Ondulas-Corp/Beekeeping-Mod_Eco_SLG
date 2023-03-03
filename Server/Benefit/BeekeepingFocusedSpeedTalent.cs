using Eco.Mods.TechTree;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
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
            Value = 0.5f;
        }
    }
}
