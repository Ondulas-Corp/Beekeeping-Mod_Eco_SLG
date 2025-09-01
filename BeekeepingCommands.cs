using Eco.Gameplay;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Math;
using Eco.World.Blocks;
using Eco.Gameplay.Objects;
using Eco.Shared.IoC;
using System.Linq;

namespace Beekeeping.Server
{
    [ChatCommandHandler]
    public class BeekeepingCommands
    {
        [ChatSubCommand("Util", "To generate new swarms. After texting command, enter the number of new hives you want to get.", "GenerateSwarm", ChatAuthorizationLevel.Admin)]
        public static void Swarm(IChatClient chatClient, int swarm)
        {
            if (swarm > 150)
            {
                chatClient.MsgLoc($"The maximum number of swarms allowed is 150. Try again with a lower value.");
                return;
            }

            int created = 0;
            while (created < swarm)
            {
                var randomLandPos = Eco.World.World.GetRandomLandPos();

                var block = Eco.World.World.GetBlock(new Vector3i(randomLandPos.X, randomLandPos.Y, randomLandPos.Z));

                if (block is DirtBlock && !(block is DesertSandBlock))
                {
                    var hivePos = new WorldPosition3i(randomLandPos.X, randomLandPos.Y + 1, randomLandPos.Z);

                    var plot = PropertyManager.GetPlotFromWorldPos(new Vector2i(hivePos.X, hivePos.Z));
                    if (plot?.Owners == null)
                    {
                        WorldObjectDebugUtil.Spawn("OccupiedSauvageBeehiveObject", null, new Vector3i(hivePos.X, hivePos.Y, hivePos.Z));
                        created++;
                    }
                }
            }

            chatClient.MsgLoc($"{created} new swarms have appeared on the map.");
        }

        [ChatSubCommand("Util", "Removes all wild beehives not on claimed property and regenerates natural hive distribution.", "ResetWildHives", ChatAuthorizationLevel.Admin)]
        public static void ResetWildHives(IChatClient chatClient)
        {
            var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
            
            // Find all wild beehives (both occupied and vacant)
            var occupiedHives = worldObjectManager.All
                .Where(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject))
                .ToList();
            
            var vacantHives = worldObjectManager.All
                .Where(obj => obj.GetType() == typeof(VacantSauvageBeehiveObject))
                .ToList();

            int removedOccupied = 0;
            int removedVacant = 0;
            int totalHives = occupiedHives.Count + vacantHives.Count;

            // Remove occupied hives not on claimed property
            foreach (var hive in occupiedHives)
            {
                var plot = PropertyManager.GetPlotFromWorldPos(new Vector2i((int)hive.Position.X, (int)hive.Position.Z));
                if (plot?.Owners == null) // Not on claimed property
                {
                    hive.Destroy();
                    removedOccupied++;
                }
            }

            // Remove vacant hives not on claimed property
            foreach (var hive in vacantHives)
            {
                var plot = PropertyManager.GetPlotFromWorldPos(new Vector2i((int)hive.Position.X, (int)hive.Position.Z));
                if (plot?.Owners == null) // Not on claimed property
                {
                    hive.Destroy();
                    removedVacant++;
                }
            }

            chatClient.MsgLoc($"Removed {removedOccupied} occupied and {removedVacant} vacant wild hives (total: {removedOccupied + removedVacant} of {totalHives} hives).");

            // Regenerate natural hives
            BeeHiveGeneration.RegenerateHives();
            
            chatClient.MsgLoc($"Natural beehive distribution has been regenerated across the world.");
        }

        [ChatSubCommand("Util", "Counts all beehives in the world (wild and player-built).", "CountHives", ChatAuthorizationLevel.Admin)]
        public static void CountHives(IChatClient chatClient)
        {
            var worldObjectManager = ServiceHolder<IWorldObjectManager>.Obj;
            
            var occupiedWild = worldObjectManager.All.Count(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject));
            var vacantWild = worldObjectManager.All.Count(obj => obj.GetType() == typeof(VacantSauvageBeehiveObject));
            var breedingHives = worldObjectManager.All.Count(obj => obj.GetType() == typeof(BreedingBeehiveObject));
            var smallHives = worldObjectManager.All.Count(obj => obj.GetType() == typeof(SmallHiveObject));

            int wildOnClaimed = 0;
            int wildInWild = 0;

            // Check wild hive locations
            var allWildHives = worldObjectManager.All
                .Where(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject) || 
                             obj.GetType() == typeof(VacantSauvageBeehiveObject));

            foreach (var hive in allWildHives)
            {
                var plot = PropertyManager.GetPlotFromWorldPos(new Vector2i((int)hive.Position.X, (int)hive.Position.Z));
                if (plot?.Owners != null)
                    wildOnClaimed++;
                else
                    wildInWild++;
            }

            chatClient.MsgLoc($"=== BEEHIVE CENSUS ===");
            chatClient.MsgLoc($"Wild Hives: {occupiedWild + vacantWild} total");
            chatClient.MsgLoc($"  - Occupied: {occupiedWild}");
            chatClient.MsgLoc($"  - Vacant: {vacantWild}");
            chatClient.MsgLoc($"  - On claimed land: {wildOnClaimed}");
            chatClient.MsgLoc($"  - In wilderness: {wildInWild}");
            chatClient.MsgLoc($"Player Hives: {breedingHives + smallHives} total");
            chatClient.MsgLoc($"  - Breeding Hives: {breedingHives}");
            chatClient.MsgLoc($"  - Small Hives: {smallHives}");
            chatClient.MsgLoc($"WORLD TOTAL: {occupiedWild + vacantWild + breedingHives + smallHives} hives");
        }
    }
}