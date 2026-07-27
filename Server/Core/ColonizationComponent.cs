using Eco.Core.Controller;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Simulation.Time;
using System;
using Eco.Gameplay.Players;
using Beekeeping.Server;

[Serialized]
[NoIcon]
public class ColonizationComponent : WorldObjectComponent
{
    [Serialized] public double ColonizationTime { get; set; }
    [Serialized] public bool IsScheduled { get; set; } = false;

	public override void Initialize()
	{
		base.Initialize();
		
		if (!IsScheduled)
		{
			// First time - schedule new colonization
			ScheduleColonization();
			IsScheduled = true;
		}
		else
		{
			// After restart - check if colonization time has passed
			var currentTime = WorldTime.Seconds;
			if (currentTime >= ColonizationTime)
			{
				// Time already passed during downtime - convert immediately
				_ = ConvertToOccupiedAsync(0.1); // Convert in 100ms
			}
			else
			{
				// Time hasn't passed yet - reschedule remaining time
				var remainingTime = ColonizationTime - currentTime;
				_ = ConvertToOccupiedAsync(remainingTime);
			}
		}
	}

	private void ScheduleColonization()
	{
		var random = new Random();
		var delaySeconds = 1800 + (random.NextDouble() * 19800);
		
		ColonizationTime = WorldTime.Seconds + delaySeconds;
		_ = ConvertToOccupiedAsync(delaySeconds);
		
		// Add DEBUG_MODE check
		if (BeeHiveGeneration.DEBUG_MODE)
		{
			try
			{
				var allUsers = Eco.Gameplay.Players.UserManager.Users;
				foreach (var user in allUsers)
				{
					if (user.IsAdmin && user.Player != null)
					{
						user.Player.MsgLocStr($"[BEEKEEPING] Vacant hive at {Parent.Position3i} will be colonized in {delaySeconds/3600:F1} hours");
					}
				}
			}
			catch { }
		}
	}

    private async System.Threading.Tasks.Task ConvertToOccupiedAsync(double delaySeconds)
    {
        await System.Threading.Tasks.Task.Delay((int)(delaySeconds * 1000));
        
        try
        {
            if (Parent == null) return;
            
            var position = Parent.Position;
			var position3i = Parent.Position3i;
            var rotation = Parent.Rotation;


            
            // Destroy the vacant hive
            Parent.Destroy();
            
            // Spawn occupied hive at same location
            var occupiedHive = WorldObjectManager.ForceAdd(typeof(OccupiedSauvageBeehiveObject), null, position, rotation);

			if (occupiedHive != null)
			{
				// Update cluster tracking
				var cluster = ClusterManager.FindClusterContaining(position3i, BeeHiveGeneration.GetAllClusters());
				if (cluster != null)
				{
					// Force refresh the cluster's hive tracking to handle the conversion
					ClusterManager.UpdateExistingHives(cluster);
				}
				
				
				if (BeeHiveGeneration.DEBUG_MODE)
				{
					try
					{
						var allUsers = Eco.Gameplay.Players.UserManager.Users;
						foreach (var user in allUsers)
						{
							if (user.IsAdmin && user.Player != null)
							{
								user.Player.MsgLocStr($"[BEEKEEPING] ✓ Vacant hive at {position3i} has been naturally colonized!");
								if (cluster != null)
								{
									user.Player.MsgLocStr($"[BEEKEEPING] Cluster tracking updated - now has {cluster.CurrentHiveCount} hives");
								}
							}
						}
					}
					catch { }
				}
				
			}
			
        }
        catch (Exception ex)
        {
            try
            {
				if (BeeHiveGeneration.DEBUG_MODE)
				{
					var allUsers = Eco.Gameplay.Players.UserManager.Users;
					foreach (var user in allUsers)
					{
						if (user.IsAdmin && user.Player != null)
						{
							user.Player.MsgLocStr($"[BEEKEEPING] Colonization failed: {ex.Message}");
						}
					}
				}
            }
            catch { }
        }
    }
}