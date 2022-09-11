
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Diagnostics;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Lavish Workspace: Beekeeping")]
    public class BeekeepingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayDescription { get; } = Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 5 percent.");

        public BeekeepingLavishWorkspaceTalentGroup()
        {
            this.Talents = new Type[2]
            {
        typeof (BeekeepingLavishResourcesTalent),
        typeof (BeekeepingLavishReqTalent)
            };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 6;
        }
    }
}
