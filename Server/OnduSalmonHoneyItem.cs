// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduSalmonHoneyItem
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [LocDisplayName("Salmon Honey")]
  [Weight(500)]
  public class OnduSalmonHoneyItem : FoodItem
  {
    private static Nutrients nutrition = new Nutrients()
    {
      Carbs = 13f,
      Fat = 9f,
      Protein = 22f,
      Vitamins = 20f
    };

    public override LocString DisplayNamePlural
    {
      get
      {
        return Localizer.DoStr("Salmon Honey");
      }
    }

    public override LocString DisplayDescription
    {
      get
      {
        return Localizer.DoStr("Salmon with honey");
      }
    }

    public override float Calories
    {
      get
      {
        return 1400f;
      }
    }

    public override Nutrients Nutrition
    {
      get
      {
        return OnduSalmonHoneyItem.nutrition;
      }
    }
        
    }
}
