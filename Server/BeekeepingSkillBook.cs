
using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Beekeeping Skill Book")]
    public class BeekeepingSkillBook : SkillBook<BeekeepingSkill, BeekeepingSkillScroll>
    {
    }
}
