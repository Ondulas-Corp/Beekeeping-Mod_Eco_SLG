// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSalmonHoneyItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;

namespace Beekeeping.Server
{
    [Serialized]
    [LocDisplayName("Salmon Honey")]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    [Weight(500)]
    public class OnduSalmonHoneyItem : FoodItem
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Salmon Honey");
        public override LocString DisplayDescription => Localizer.DoStr("Salmon with honey");
        public override float Calories => 1400;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 13, Fat = 9, Protein = 22, Vitamins = 20 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(72);
    }
}
