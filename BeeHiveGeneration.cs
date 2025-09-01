using Beekeeping.Server;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.World.Blocks;
using Eco.Simulation;
using System;
using System.Linq;

public class BeeHiveGeneration : IModKitPlugin, IInitializablePlugin
{
    private Random random = new Random();

    public string GetCategory() => "Job";

    public string GetStatus() => "Natural Beehive Generation Success";

    public void Initialize(TimedTask timer)
    {
        // Check if any occupied hives already exist
        try
        {
            var existingHives = ((IWorldObjectManager)ServiceHolder<IWorldObjectManager>.Obj)
                .All.Where(obj => obj.GetType() == typeof(OccupiedSauvageBeehiveObject));
                
            if (existingHives.Any())
                return; // Hives already exist, skip generation
        }
        catch
        {
            // Continue with generation if check fails
        }

        GenerateNaturalHives();
    }

    private void GenerateNaturalHives()
    {
        // Dynamic hive count based on world size using a better method
        int worldRadius = GetWorldRadius();
        int targetHiveCount = CalculateHiveCount(worldRadius);
        
        int hivesSpawned = 0;
        int maxAttempts = targetHiveCount * 15;
        int attempts = 0;

        while (hivesSpawned < targetHiveCount && attempts < maxAttempts)
        {
            attempts++;
            
            // Get a random land position
            var landPos = Eco.World.World.GetRandomLandPos();
            
            // Check if this location is suitable
            if (IsValidHiveLocation(landPos))
            {
                // Place hive one block above the ground
                var hivePosition = new Vector3i(landPos.X, landPos.Y + 1, landPos.Z);
                
                try
                {
                    WorldObjectDebugUtil.Spawn("OccupiedSauvageBeehiveObject", null, hivePosition);
                    hivesSpawned++;
                }
                catch
                {
                    // If spawn fails, continue trying other locations
                    continue;
                }
            }
        }
    }

    private int GetWorldRadius()
    {
        // Sample positions to estimate world boundaries
        var maxDistance = 0;
        for (int i = 0; i < 50; i++)
        {
            var pos = Eco.World.World.GetRandomLandPos();
            var distance = Math.Max(Math.Abs(pos.X), Math.Abs(pos.Z));
            if (distance > maxDistance)
                maxDistance = distance;
        }
        return maxDistance;
    }

    private int CalculateHiveCount(int worldRadius)
    {
        // Scale hive count based on estimated world size
        if (worldRadius < 200)
            return random.Next(40, 61);      // Small world: 40-60 hives
        else if (worldRadius < 400)
            return random.Next(80, 121);     // Medium world: 80-120 hives
        else if (worldRadius < 600)
            return random.Next(120, 181);    // Large world: 120-180 hives
        else
            return random.Next(180, 251);    // Very large world: 180-250 hives
    }

    private bool IsValidHiveLocation(WorldPosition3i groundPos)
    {
        // Check the ground block
        var groundBlock = Eco.World.World.GetBlock(new Vector3i(groundPos.X, groundPos.Y, groundPos.Z));
        
        // Must be on dirt-like blocks, not sand or desert
        if (!(groundBlock is DirtBlock) || groundBlock is DesertSandBlock)
            return false;

        // Exclude snow/ice biomes - check for snow blocks nearby
        if (IsInColdBiome(groundPos))
            return false;

        // Check if the space above is clear for hive placement
        var abovePos = new Vector3i(groundPos.X, groundPos.Y + 1, groundPos.Z);
        var aboveBlock = Eco.World.World.GetBlock(abovePos);
        
        if (aboveBlock != null && !(aboveBlock is EmptyBlock))
            return false;

        // Make sure it's not on claimed land
        var plot = PropertyManager.GetPlotFromWorldPos(new Vector2i(groundPos.X, groundPos.Z));
        if (plot?.Owners != null)
            return false;

        // Simple plant check - just ensure there are some plants nearby for realism
        return HasNearbyPlants(groundPos);
    }

    private bool IsInColdBiome(WorldPosition3i position)
    {
        // Check a small area around the position for snow/ice blocks
        for (int x = -2; x <= 2; x++)
        {
            for (int z = -2; z <= 2; z++)
            {
                var checkPos = new Vector3i(position.X + x, position.Y, position.Z + z);
                var block = Eco.World.World.GetBlock(checkPos);
                
                if (block != null)
                {
                    var blockType = block.GetType().Name.ToLower();
                    if (blockType.Contains("snow") || blockType.Contains("ice"))
                        return true;
                }
            }
        }
        return false;
    }

    private bool HasNearbyPlants(WorldPosition3i position)
    {
        int plantCount = 0;
        int radius = 8; // Smaller radius for simpler checking
        
        // Check in a smaller area to keep it simple and fast
        for (int x = -radius; x <= radius; x += 2) // Skip every other block for performance
        {
            for (int z = -radius; z <= radius; z += 2)
            {
                var checkPos = new Vector3i(position.X + x, position.Y, position.Z + z);
                var plant = EcoSim.PlantSim.GetPlant(checkPos);
                
                if (plant != null)
                {
                    plantCount++;
                    if (plantCount >= 10) // Found enough plants
                        return true;
                }
            }
        }
        
        return plantCount >= 5; // Minimum 5 plants needed
    }

    // Public method for manual regeneration (for admin commands)
    public static void RegenerateHives()
    {
        var generator = new BeeHiveGeneration();
        generator.GenerateNaturalHives();
    }
}