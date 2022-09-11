using Eco.Gameplay.Interactions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Serialization;

namespace Eco.Gameplay.Components
{
    [Serialized]
    public class BeeComponent : WorldObjectComponent
    {
        public InteractResult Interact(InteractionContext context)
        {
            User user = context.Player.User;
            Player player = context.Player;
            InteractionContext interactionContext = (InteractionContext)null;
            interactionContext = context;
            WorldObjectDebugUtil.Spawn("OnduInnocupiedSauvageBeehiveObject", user, this.Parent.Position3i);
            this.Parent.Destroy();
            return InteractResult.Success;
        }
    }
}
