//
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Diagnostics;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Parallel Processing: Beekeeping")]
    public class BeekeepingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayDescription { get; } = Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent.");

        public BeekeepingParallelProcessingTalentGroup()
        {
            this.Talents = new Type[1]
            {
        typeof (BeekeepingParallelSpeedTalent)
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 3;
        }
    }
}
