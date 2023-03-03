// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCocktailItem
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
    [LocDisplayName("Blueberry Cocktail")]
    [Weight(350)]
    public class OnduCocktailItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Blueberry Cocktail");
        public override LocString DisplayDescription => Localizer.DoStr("For special occasions...");
        public override float Calories => 700;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 20, Fat = 8, Protein = 6, Vitamins = 28 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
