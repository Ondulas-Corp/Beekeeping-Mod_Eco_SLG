// Decompiled with JetBrains decompiler
// Type: Eco.Mods.TechTree.OnduOccupiedSauvageBeehiveObject
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Services;
using System;
using System.Linq;
using System.Text;

namespace Eco.Mods.TechTree
{
  [Serialized]
  [RequireComponent(typeof (PropertyAuthComponent), null)]
  [RequireComponent(typeof (SolidGroundComponent), null)]
  [RequireComponent(typeof (BeeComponent), null)]
  [RequireComponent(typeof (QueenComponent), null)]
  public class OnduOccupiedSauvageBeehiveObject : WorldObject, IRepresentsItem
  {
    private bool swithing = false;

    public override LocString DisplayName
    {
      get
      {
        return Localizer.DoStr("Occupied Swarm");
      }
    }

    public virtual Type RepresentedItemType
    {
      get
      {
        return typeof (OnduOccupiedSauvageBeehiveItem);
      }
    }

    public override InteractResult OnActRight(InteractionContext context)
    {
      ItemStack itemStack = context.Player.User.Inventory.Clothing.NonEmptyStacks.Where<ItemStack>((Func<ItemStack, bool>) (stack => (string) ((Stackable) stack.Item).DisplayName == "Beekeeper Hat")).FirstOrDefault<ItemStack>();
      if (!this.swithing)
      {
        if (itemStack != null)
        {
          context.Player.User.Inventory.TryModify((Action<IInventoryChangeSet>) (changeSet =>
          {
            this.swithing = true;
            int num = new Random().Next(1, 16);
            if (num == 15)
              changeSet.AddItem(typeof (OnduModernQueenBeeItem), (Inventory) null);
            else if (num > 0 && num < 10)
              changeSet.AddItem(typeof (OnduQueenBeeItem), (Inventory) null);
            else if (num == 10 || num == 11 || num == 12)
              changeSet.AddItem(typeof (OnduBasicQueenBeeItem), (Inventory) null);
            else if (num == 13 || num == 14)
              changeSet.AddItem(typeof (OnduAdvancedQueenBeeItem), (Inventory) null);
            new StringBuilder().Append((string) Localizer.DoStr("You received a Queen Bee !"));
          }), context.Player.User);
          return this.GetComponent<BeeComponent>((string) null).Interact(context);
        }
        context.Player.Msg(Localizer.Format("You can't get the queen bee back without a Beekeeper Mask."), (NotificationStyle)8);
        return InteractResult.Success;
      }
      context.Player.Msg(Localizer.Format("Queen Bee has already been taken."), (NotificationStyle)8);
      return InteractResult.Success;
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    public override void Destroy()
    {
      base.Destroy();
    }
  }
}
