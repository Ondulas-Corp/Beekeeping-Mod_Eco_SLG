using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
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
            this.Value = 0.8f;
        }
    }
}
