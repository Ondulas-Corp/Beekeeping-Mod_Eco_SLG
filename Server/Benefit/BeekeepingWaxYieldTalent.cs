using System;
using System.Collections.Generic;
using Eco.Gameplay.Bonuses;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Utils;

namespace Eco.Mods.TechTree
{
    public partial class WaxYieldTalent : Talent
    {
        public WaxYieldTalent()
        {
            this.Bonuses.Add(new Bonus
            {
                Name = Localizer.DoStr("Bountiful Comb: Beewax"),
                EffectDescription = Localizer.Do($"You make the most of every frame: every extraction run yields {Text.Positive("+1")} Beewax."),
                Causes = new List<BonusCause> { new CraftBonusCause { Action = BonusAction.Yield, Recipes = new HashSet<Type> { typeof(Beekeeping.Server.BeewaxRecipe) } } },
                Effects = new List<BonusEffect> { new BonusEffectAdditive { Value = 1f } },
            });
        }

        public override bool Base => true;
    }

    [Serialized]
    [LocDisplayName("Bountiful Comb: Beewax")]
    [LocDescription("You make the most of every frame: every extraction run always yields +1 Beewax.")]
    public partial class BeekeepingWaxYieldTalentGroup : TalentGroup
    {
        public BeekeepingWaxYieldTalentGroup()
        {
            Talents = new Type[] { typeof(BeekeepingWaxYieldTalent) };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 6;
        }
    }

    [Serialized]
    public partial class BeekeepingWaxYieldTalent : WaxYieldTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BeekeepingWaxYieldTalentGroup); } }
    }
}
