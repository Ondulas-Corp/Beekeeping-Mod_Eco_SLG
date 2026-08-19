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
    public partial class BountifulCombTalent : Talent
    {
        public BountifulCombTalent()
        {
            this.Bonuses.Add(new Bonus
            {
                Name = Localizer.DoStr("Bountiful Comb"),
                EffectDescription = Localizer.Do($"Each extraction run yields {Text.Positive("+1")} Honey."),
                Causes = new List<BonusCause> { new CraftBonusCause { Action = BonusAction.Yield, Recipes = new HashSet<Type> { typeof(Beekeeping.Server.HoneyRecipe) } } },
                Effects = new List<BonusEffect> { new BonusEffectAdditive { Value = 1f } },
            });
        }

        public override bool Base => true;
    }

    [Serialized]
    [LocDisplayName("Bountiful Comb: Honey")]
    [LocDescription("Your extraction technique is flawless: every run always yields +1 Honey.")]
    public partial class BeekeepingBountifulCombTalentGroup : TalentGroup
    {
        public BeekeepingBountifulCombTalentGroup()
        {
            Talents = new Type[] { typeof(BeekeepingBountifulCombTalent) };
            this.OwningSkill = typeof(BeekeepingSkill);
            this.Level = 6;
        }
    }

    [Serialized]
    public partial class BeekeepingBountifulCombTalent : BountifulCombTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BeekeepingBountifulCombTalentGroup); } }
    }
}
