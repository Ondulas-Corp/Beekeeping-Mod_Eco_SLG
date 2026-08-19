// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    // Base talent type — unique capability marker for queen detection chance
    public partial class QueensLuckTalent : Talent
    {
        public override bool Base => true;
    }

    [Serialized]
    [LocDisplayName("Queen's Luck: Beekeeping")]
    [LocDescription("An experienced eye can spot a queen. Harvesting a wild swarm increases the chance to find a Queen Bee from 17% to 25%.")]
    public partial class BeekeepingQueensLuckTalentGroup : TalentGroup
    {
        public BeekeepingQueensLuckTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(BeekeepingQueensLuckTalent),
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 3;
        }
    }

    [Serialized]
    public partial class BeekeepingQueensLuckTalent : QueensLuckTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BeekeepingQueensLuckTalentGroup); } }
    }
}
