using Eco.Mods.TechTree;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    public class BeekeepingParallelSpeedTalent : ParallelProcessingTalent
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
                return typeof(BeekeepingParallelProcessingTalentGroup);
            }
        }

        public BeekeepingParallelSpeedTalent()
        {
            Value = 0.8f;
        }
    }
}
