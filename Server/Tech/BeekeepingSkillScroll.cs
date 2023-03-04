using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Beekeeping Skill Scroll")]
    public class BeekeepingSkillScroll : SkillScroll<BeekeepingSkill, BeekeepingSkillBook>
    {
    }
}
