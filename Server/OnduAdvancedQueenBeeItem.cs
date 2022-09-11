using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Advanced Queen Bee")]
    [Weight(1)]
    [Tag("QueenBee", 1)]
    public class OnduAdvancedQueenBeeItem : EfficiencyModule
    {
        public override LocString DisplayDescription
        {
            get
            {
                return Localizer.DoStr("Advanced Upgrade. Found in wild beehives");
            }
        }

        public OnduAdvancedQueenBeeItem() : base(
            ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
            0.6f,
            typeof(BeekeepingSkill),
            0.6f
            ) { }
    }
    
}
