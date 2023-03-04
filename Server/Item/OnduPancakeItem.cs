// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPancakeItem
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
    [LocDisplayName("Pancake")]
    [Weight(200)]
    public class OnduPancakeItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Pancake");
        public override LocString DisplayDescription => Localizer.DoStr("Breakfast only.");
        public override float Calories => 1150;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 21, Fat = 17, Protein = 8, Vitamins = 4 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
