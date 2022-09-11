// Decompiled with JetBrains decompiler
// Type: BeeHiveGeneration
// Assembly: BeekeepingMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0CFADE07-BC7B-4B9C-956D-CB3332005D4A
// Assembly location: C:\Users\khisa\Downloads\beekeepingmod1.2.1\BeekeepingMod1.2.1\BeekeepingMod1.2.1.dll

using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Mods.TechTree;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.World.Blocks;
using System;
using System.Linq;
/* The OnduCheckObject is an object that appears only the first time the beekeeping mod is launched on a server. This avoids that a new hive generation is done at each server launch. 
 If you want to change the number of hives appearing at the time of generation, just change the data in the Randomizer.Next method */
 
public class BeeHiveGeneration : IModKitPlugin, IServerPlugin, IInitializablePlugin
{
    private Random randomizer = new Random();
    private int ruche = 0;

    public string GetStatus()
    {
        return "Generation Beehive Succes";
    }

    public void Initialize(TimedTask timer)
    {
        try
        {
            ((IWorldObjectManager)ServiceHolder<IWorldObjectManager>.Obj).All.First<WorldObject>((Func<WorldObject, bool>)(worldObject => worldObject.GetType() == typeof(OnduCheckObject)));
            //Console.WriteLine("Already Generate");
            return;
        }
        catch (InvalidOperationException ex)
        {
            //Console.WriteLine("Let's go to generate Honey");
        }
        WorldObjectDebugUtil.Spawn("OnduCheckObject", (User)null, new Vector3i(0, 200, 0));
        this.ruche = this.randomizer.Next(50, 61);
        int num = 0;
        while (num < this.ruche)
        {
            WorldPosition3i randomLandPos = Eco.World.World.GetRandomLandPos();
            WrappedWorldPosition3i pos = (Vector3i)randomLandPos;
            Block block = Eco.World.World.GetBlock(pos);
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
    }
}
