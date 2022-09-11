// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduPurifiedHoneyItem
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
  [LocDisplayName("Purified honey")]
  [Weight(100)]
  public class OnduPurifiedHoneyItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 9f,
      Fat = 0.0f,
      Protein = 0.0f,
      Vitamins = 3f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Purified honey");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Concentrated honey, ready-to-eat.");
      }
    }

    public override float Calories
    {
      get
      {
        return 350f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduPurifiedHoneyItem.nutrition;
      }
    }
  }
}
