// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduMarshmallowItem
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
  [LocDisplayName("Marshmallow")]
  [Weight(100)]
  public class OnduMarshmallowItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 18f,
      Fat = 7f,
      Protein = 4f,
      Vitamins = 15f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Marshmallow");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("The best food to share around a wood fire.");
      }
    }

    public override float Calories
    {
      get
      {
        return 850f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduMarshmallowItem.nutrition;
      }
    }
  }
}
