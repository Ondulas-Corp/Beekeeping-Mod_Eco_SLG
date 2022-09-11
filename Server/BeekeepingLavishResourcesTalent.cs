using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
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
            this.Value = 0.95f;
        }
    }
}
