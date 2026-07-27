using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System.Collections.Generic;

[Serialized]
public class HiveCluster
{
    [Serialized] public Vector3i CenterPosition { get; set; }
    [Serialized] public int MaxPlantCount { get; set; }
    [Serialized] public int CurrentPlantCount { get; set; }
    [Serialized] public bool IsAlive { get; set; }
    [Serialized, ThreadSafe] public List<double> PendingRegenerationTimes { get; set; }
    [Serialized, ThreadSafe] public List<Vector3i> ExistingHivePositions { get; set; }
    [Serialized] public double LastHealthCheck { get; set; }
    [Serialized] public double LastRegenerationCheck { get; set; }
    [Serialized] public bool RegenerationTriggered { get; set; }
    [Serialized] public double LastHarvestTime { get; set; }
    [Serialized] public bool RegenerationInProgress { get; set; } = false;

    public HiveCluster()
    {
        PendingRegenerationTimes = new List<double>();
        ExistingHivePositions = new List<Vector3i>();
        IsAlive = true;
        LastHealthCheck = 0;
        LastRegenerationCheck = 0;
        RegenerationTriggered = false;
        LastHarvestTime = 0;
    }

    public int CurrentHiveCount => ExistingHivePositions.Count;
    public int PendingHiveCount => PendingRegenerationTimes.Count;
    public int TotalFutureHives => CurrentHiveCount + PendingHiveCount;
    
    public int PlantBasedCapacity
    {
        get
        {
            if (MaxPlantCount == 0) return 0;
            
            var plantPercentage = (float)CurrentPlantCount / MaxPlantCount;
            
            if (plantPercentage >= 0.85f) return 5;
            if (plantPercentage >= 0.68f) return 4;
            if (plantPercentage >= 0.51f) return 3;
            if (plantPercentage >= 0.34f) return 2;
            if (plantPercentage >= 0.17f) return 1;
            
            return 0;
        }
    }
}