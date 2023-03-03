// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMadeleineItem
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
    [LocDisplayName("Madeleine")]
    [Weight(150)]
    public class OnduMadeleineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Madeleine");
        public override LocString DisplayDescription => Localizer.DoStr("A Spanish and French pastry.");
        public override float Calories => 700;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 19, Fat = 15, Protein = 7, Vitamins = 7 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
