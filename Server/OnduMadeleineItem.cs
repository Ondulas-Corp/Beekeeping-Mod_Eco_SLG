// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMadeleineItem
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
  [LocDisplayName("Madeleine")]
  [Weight(150)]
  public class OnduMadeleineItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 19f,
      Fat = 15f,
      Protein = 7f,
      Vitamins = 7f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Madeleine");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("A Spanish and French pastry.");
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
        return OnduMadeleineItem.nutrition;
      }
    }
  }
}
