// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduCocktailItem
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
  [LocDisplayName("Blueberry Cocktail")]
  [Weight(350)]
  public class OnduCocktailItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 20f,
      Fat = 8f,
      Protein = 6f,
      Vitamins = 28f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Blueberry Cocktail");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("For special occasions...");
      }
    }

    public override float Calories
    {
      get
      {
        return 700f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduCocktailItem.nutrition;
      }
    }
  }
}
