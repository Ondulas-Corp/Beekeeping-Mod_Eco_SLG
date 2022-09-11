using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Diagnostics;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Focused Workflow: Beekeeping")]
    public class BeekeepingFocusedWorkflowTalentGroup : TalentGroup
    {

        public override LocString DisplayDescription { get; } = Localizer.DoStr("Doubles the speed of related tables when alone.");

        public BeekeepingFocusedWorkflowTalentGroup()
        {
            this.Talents = new Type[1]
            {
        typeof (BeekeepingFocusedSpeedTalent)
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 3;
        }
    }
}
