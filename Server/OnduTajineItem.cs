// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduTajineItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Tajine")]
  [Weight(450)]
  public class OnduTajineItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 11f,
      Fat = 19f,
      Protein = 20f,
      Vitamins = 12f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Tajine");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Traditional Tagine");
      }
    }

    public override float Calories
    {
      get
      {
        return 1300f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduTajineItem.nutrition;
      }
    }
  }
}
