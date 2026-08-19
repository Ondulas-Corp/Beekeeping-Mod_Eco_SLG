// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    // Base talent type — recognised by the talent system as a unique capability marker
    public partial class NimbleHandsTalent : Talent
    {
        public override bool Base => true;
    }

    [Serialized]
    [LocDisplayName("Nimble Hands: Beekeeping")]
    [LocDescription("Your dexterity with wild hives pays off: harvesting a wild swarm always yields +1 Bee Eggs.")]
    public partial class BeekeepingNimbleHandsTalentGroup : TalentGroup
    {
        public BeekeepingNimbleHandsTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(BeekeepingNimbleHandsTalent),
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 3;
        }
    }

    [Serialized]
    public partial class BeekeepingNimbleHandsTalent : NimbleHandsTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BeekeepingNimbleHandsTalentGroup); } }
    }
}
