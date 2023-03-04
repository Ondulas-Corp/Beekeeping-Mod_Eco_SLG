// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMarshmallowItem
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
    [LocDisplayName("Marshmallow")]
    [Weight(100)]
    public class OnduMarshmallowItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Marshmallow");
        public override LocString DisplayDescription => Localizer.DoStr("The best food to share around a wood fire.");
        public override float Calories => 850;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 18, Fat = 7, Protein = 4, Vitamins = 15 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
