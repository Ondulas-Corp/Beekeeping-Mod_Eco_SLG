// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedHardwoodLumberChairRecipe
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
  [RequiresSkill(typeof (CarpentrySkill), 7)]
  public class WaxedHardwoodLumberChairRecipe : Recipe
  {
    public WaxedHardwoodLumberChairRecipe()
    {
      CraftingComponent.AddTagProduct(typeof (SawmillObject), typeof (WaxedLumberChairRecipe), new Recipe("Waxed Lumber Chair", Localizer.DoStr("Waxed Lumber Chair"), new IngredientElement[2]
      {
        new IngredientElement(typeof (HardwoodLumberChairItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 1f, false)
      }, new CraftingElement[1]
      {
        (CraftingElement) new CraftingElement<WaxedLumberChairItem>(1f)
      }));
    }
  }
}
