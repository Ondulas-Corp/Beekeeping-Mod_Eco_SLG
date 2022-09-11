// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduFruitCakeItem
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
  [LocDisplayName("Fruit Cake")]
  [Weight(450)]
  public class OnduFruitCakeItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 21f,
      Fat = 11f,
      Protein = 5f,
      Vitamins = 25f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Fruit Cake");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Happy Birthday !");
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
        return OnduFruitCakeItem.nutrition;
      }
    }
  }
}
