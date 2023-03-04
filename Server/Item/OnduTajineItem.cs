// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduTajineItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Tajine")]
    [Weight(450)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    public class OnduTajineItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Tajine");
        public override LocString DisplayDescription => Localizer.DoStr("Traditional Tagine.");
        public override float Calories => 1300;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 11, Fat = 19, Protein = 20, Vitamins = 12 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
