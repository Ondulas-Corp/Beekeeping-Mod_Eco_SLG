// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPurifiedHoneyItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Purified honey")]
    [Weight(100)]
    public class OnduPurifiedHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Purified honey");
        public override LocString DisplayDescription => Localizer.DoStr("Concentrated honey, ready-to-eat.");
        public override float Calories => 350;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 9, Fat = 0, Protein = 0, Vitamins = 3 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
