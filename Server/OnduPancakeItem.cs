// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPancakeItem
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
  [LocDisplayName("Pancake")]
  [Weight(200)]
  public class OnduPancakeItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 21f,
      Fat = 17f,
      Protein = 8f,
      Vitamins = 4f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Pancake");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Breakfast only.");
      }
    }

    public override float Calories
    {
      get
      {
        return 1150f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduPancakeItem.nutrition;
      }
    }
  }
}
