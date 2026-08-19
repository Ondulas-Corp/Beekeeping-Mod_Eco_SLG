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

        [LocDescription("Maximum total number of occupied wild hives allowed simultaneously across the entire world. Set to 0 for no limit.")]
        public int MaxTotalWildHives { get; set; } = 50;

        [LocDescription("Plant coverage fraction below which a cluster is considered dead and stops regenerating (e.g. 0.08 = 8%).")]
        public float ClusterDeathThreshold { get; set; } = 0.08f;

        // --- Wild hive regeneration ---

        [LocDescription("Minimum hours before a destroyed/harvested hive slot regenerates.")]
        public float MinRegenHours { get; set; } = 1.0f;

        [LocDescription("Maximum hours before a destroyed/harvested hive slot regenerates.")]
        public float MaxRegenHours { get; set; } = 20.0f;

        // --- Player hive crafting speed ---

        [LocDescription("Radius in blocks over which the OccupiedFertileGround layer is averaged to compute the crafting speed bonus for player hives.")]
        public float HiveCraftLayerRadius { get; set; } = 20f;

        [LocDescription("Minimum craft time multiplier for hive recipes as a fraction of the base time (e.g. 0.5 = never faster than 50% of base time). Must be between 0.1 and 1.0.")]
        public float HiveCraftSpeedFloor { get; set; } = 0.5f;

        // --- Player hive pollination ---

        [LocDescription("Radius in blocks within which a player beehive boosts crop growth.")]
        public int PollinationRadius { get; set; } = 10;

        [LocDescription("Percentage growth speed boost applied to crops within the pollination radius (e.g. 15 = 15% faster).")]
        public float GrowthBoostPercent { get; set; } = 15f;
    }
}
