using Eco.Gameplay;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Serialization;

namespace Beekeeping.Server
{
    [Serialized]
    public class BeeComponent : WorldObjectComponent
    {
        public InteractResult Interact(InteractionContext context)
        {
            User user = context.Player.User;
            Player player = context.Player;
            InteractionContext interactionContext = null;
            interactionContext = context;
            WorldObjectDebugUtil.Spawn("OnduInnocupiedSauvageBeehiveObject", user, Parent.Position3i);
            Parent.Destroy();
            return InteractResult.Success;
        }
    }
}
