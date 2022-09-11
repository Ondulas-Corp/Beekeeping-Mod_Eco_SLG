using Eco.Core.Items;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Beekeeping Skill")]
    [RequiresSkill(typeof(FarmerSkill), 0)]
    [Tag("Farmer Specialty", 1)]
    [Eco.Gameplay.Items.Tier(1f, true)]
    [Tag("Specialty", 1)]
    [Tag("Teachable", 1)]
    public class BeekeepingSkill : Skill
    {
        public static MultiplicativeStrategy MultiplicativeStrategy = new MultiplicativeStrategy(new float[8]
        {
      1f,
      0.5f,
      0.45f,
      0.4f,
      0.35f,
      0.3f,
      0.25f,
      0.2f
        });
        public static AdditiveStrategy AdditiveStrategy = new AdditiveStrategy(new float[8]
        {
      0.0f,
      0.5f,
      0.55f,
      0.6f,
      0.65f,
      0.7f,
      0.75f,
      0.8f
        });

        public override LocString DisplayDescription
        {
            get
            {
                return Localizer.DoStr("A skill for the breeding of bees.");
            }
        }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20f, Localizer.DoStr("for leveling up another specialization."));
        }

        public override MultiplicativeStrategy MultiStrategy
        {
            get
            {
                return BeekeepingSkill.MultiplicativeStrategy;
            }
        }

        public override AdditiveStrategy AddStrategy
        {
            get
            {
                return BeekeepingSkill.AdditiveStrategy;
            }
        }

        public override int MaxLevel
        {
            get
            {
                return 7;
            }
        }

        public override int Tier
        {
            get
            {
                return 1;
            }
        }
    }
}
