// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.WaxedSoftwoodHewnTableRecipe
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
  public class WaxedSoftwoodHewnTableRecipe : Recipe
  {
    public WaxedSoftwoodHewnTableRecipe()
    {
      CraftingComponent.AddTagProduct(typeof (SawmillObject), typeof (WaxedHewnTableRecipe), new Recipe("Waxed Hewn Table", Localizer.DoStr("Waxed Hewn Table"), new IngredientElement[2]
      {
        new IngredientElement(typeof (HewnSoftwoodTableItem), 1f, false),
        new IngredientElement(typeof (OnduWaxItem), 2f, false)
      }, new CraftingElement[1]
      {
        (CraftingElement) new CraftingElement<WaxedHewnTableItem>(1f)
      }));
    }
  }
}
