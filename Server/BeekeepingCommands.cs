using Eco.Gameplay;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Math;
using Eco.Shared.Services;
using Eco.World.Blocks;
using System.Runtime.CompilerServices;

namespace Eco.Mods.TechTree
{
    public class BeekeepingCommands : IChatCommandHandler
    {
        [ChatSubCommand("Util", "To generate new swarms. After texting command, enter the number of new hives you want to get.", "GenerateSwarm", ChatAuthorizationLevel.Admin)]
        public static void Swarm(IChatClient chatClient, int swarm)
        {
            int num = swarm;
            if (swarm < 150)
            {
                while (num < swarm + swarm)
                {
                    WorldPosition3i randomLandPos = Eco.World.World.GetRandomLandPos();
                    Block block = Eco.World.World.GetBlock((WrappedWorldPosition3i)((Vector3i)randomLandPos));
                    if (block is DirtBlock && !(block is DesertSandBlock))
                    {
                        ++randomLandPos.y;
                        if (PropertyManager.GetPlotFromWorldPos((Vector2i)randomLandPos)?.Owners == null)
                        {
                            WorldObjectDebugUtil.Spawn("OnduOccupiedSauvageBeehiveObject", (User)null, (Vector3i)randomLandPos);
                            ++num;
                        }
                    }
                }
                chatClient.MsgLoc(FormattableStringFactory.Create("{0} new swarms have appeared on the map.", (object)swarm));
            }
            else
                chatClient.MsgLoc(FormattableStringFactory.Create("The maximum number of swarms allowed is 150. Try again with a lower value."));
        }
    }
}
