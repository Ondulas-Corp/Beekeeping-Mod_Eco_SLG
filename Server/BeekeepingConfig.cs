using Eco.Shared.Localization;

namespace Beekeeping.Server
{
    [Localized]
    public class BeekeepingConfig
    {
        // --- Wild hive clusters ---

        [LocDescription("Radius in blocks of each wild hive cluster. Hives spawn and are tracked within this radius.")]
        public int ClusterRadius { get; set; } = 60;

        [LocDescription("Minimum spacing in blocks between two cluster centers. Prevents overlapping clusters.")]
        public int MinClusterSpacing { get; set; } = 50;

        [LocDescription("Maximum number of wild hives allowed per cluster.")]
        public int MaxHivesPerCluster { get; set; } = 5;

        [LocDescription("Plant coverage fraction below which a cluster is considered dead and stops regenerating (e.g. 0.08 = 8%).")]
        public float ClusterDeathThreshold { get; set; } = 0.08f;

        // --- Wild hive regeneration ---

        [LocDescription("Minimum hours before a destroyed/harvested hive slot regenerates.")]
        public float MinRegenHours { get; set; } = 1.0f;

        [LocDescription("Maximum hours before a destroyed/harvested hive slot regenerates.")]
        public float MaxRegenHours { get; set; } = 20.0f;

        // --- Colonization (vacant swarm → occupied beehive) ---

        [LocDescription("Minimum hours before a vacant wild swarm becomes an occupied beehive.")]
        public float MinColonizationHours { get; set; } = 0.5f;

        [LocDescription("Maximum hours before a vacant wild swarm becomes an occupied beehive.")]
        public float MaxColonizationHours { get; set; } = 6.0f;

        // --- Player hive pollination ---

        [LocDescription("Radius in blocks within which a player beehive boosts crop growth.")]
        public int PollinationRadius { get; set; } = 10;

        [LocDescription("Percentage growth speed boost applied to crops within the pollination radius (e.g. 15 = 15% faster).")]
        public float GrowthBoostPercent { get; set; } = 15f;
    }
}
