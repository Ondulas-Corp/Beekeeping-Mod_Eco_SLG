using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Diagnostics;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Focused Workflow: Beekeeping")]
    public class BeekeepingFocusedWorkflowTalentGroup : TalentGroup
    {

        public override LocString DisplayDescription { get; } = Localizer.DoStr("Doubles the speed of related tables when alone.");

        public BeekeepingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[1]
            {
        typeof (BeekeepingFocusedSpeedTalent)
            };
            OwningSkill = typeof(BeekeepingSkill);
            Level = 3;
        }
    }
}
