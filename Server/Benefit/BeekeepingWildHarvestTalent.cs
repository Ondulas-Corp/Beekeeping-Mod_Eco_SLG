// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    // Base talent type — guaranteed fuel return on wild harvest
    public partial class WildHarvestTalent : Talent
    {
        public override bool Base => true;
    }

    [Serialized]
    [LocDisplayName("Wild Harvest: Beekeeping")]
    [LocDescription("You never come back empty-handed. Harvesting a wild swarm always yields +1 Bee Colony Core.")]
    public partial class BeekeepingWildHarvestTalentGroup : TalentGroup
    {
        public BeekeepingWildHarvestTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(BeekeepingWildHarvestTalent),
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 3;
        }
    }

    [Serialized]
    public partial class BeekeepingWildHarvestTalent : WildHarvestTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BeekeepingWildHarvestTalentGroup); } }
    }
}
