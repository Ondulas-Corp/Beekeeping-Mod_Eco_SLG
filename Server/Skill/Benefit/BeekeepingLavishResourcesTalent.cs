using Eco.Mods.TechTree;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server.Skill.Benefit
{
    [Serialized]
    public class BeekeepingLavishResourcesTalent : LavishWorkspaceTalent
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

        public BeekeepingLavishResourcesTalent()
        {
            Value = 0.95f;
        }
    }
}
