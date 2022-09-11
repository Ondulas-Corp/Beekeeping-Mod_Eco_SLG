// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduGingerbreadItem
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
  [LocDisplayName("Gingerbread")]
  [Weight(400)]
  public class OnduGingerbreadItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 23f,
      Fat = 16f,
      Protein = 8f,
      Vitamins = 15f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Gingerbread");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Tight and melting.");
      }
    }

    public override float Calories
    {
      get
      {
        return 1200f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduGingerbreadItem.nutrition;
      }
    }
  }
}
