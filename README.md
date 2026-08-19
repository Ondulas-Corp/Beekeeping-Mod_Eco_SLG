# Beekeeping Mod — Eco SLG

An open-source beekeeping mod for [Eco](https://play.eco/). It introduces a self-sustaining wild hive ecosystem, a full crafting chain built around honey and wax, and a dedicated skill tree.

---

## Features

### Wild Hive Ecosystem

The core of the mod is a cluster system that autonomously manages the wild hive population across the map.

**Hive types**
- `VacantSwarm` — a vacant swarm that naturally colonizes into an occupied hive after a random delay (configurable, default 30 min – 6 h)
- `OccupiedSwarm` — an occupied hive that players can harvest for honey, wax, and bee products

**Clusters**
- Hives are grouped into clusters (default radius: 60 blocks, minimum spacing between clusters: 50 blocks)
- Each cluster supports up to 5 hives (configurable)
- A cluster's **plant health** (`CurrentPlantCount / MaxPlantCount`) determines its maximum capacity:

| Plant health | Capacity |
|---|---|
| ≥ 85 % | 5 hives |
| ≥ 68 % | 4 hives |
| ≥ 51 % | 3 hives |
| ≥ 34 % | 2 hives |
| ≥ 17 % | 1 hive |
| < 17 % | Dead cluster (no regeneration) |

> The death threshold is configurable (default 8 %).

**Regeneration**
- When a cluster drops to ≤ 2 hives, 1–5 new hives are scheduled to appear within 1–20 hours (configurable)
- Hives spawn on free dirt blocks within the cluster radius — using actual terrain surface height, so hilly terrain is handled correctly
- Cluster state is saved to disk and rebuilt on server restart

**Pollination**
- Player-placed hives boost crop growth within a configurable radius (default: 10 blocks, +15 % speed)

---

### Items & Crafting

**Bee lifecycle chain**

```
Wild Hive harvest
  → BeeEggs (bootstrap: BeeColonyCore + Petals ×4 → ×6 | entretien: Petals ×4 → ×2)
  → WorkerBee (BeeEggs ×1 → WorkerBee)
  → BeePollen (WorkerBee + Petals ×3 → ×1 | ForagerBee + Petals ×3 → ×2 [BeeHive])
  → ForagerBee (WorkerBee + BeePollen → ForagerBee [BeeHive])
  → RoyalJelly (WorkerBee ×3 + Honey → RoyalJelly [BeeHive])
  → QueenBee (BeeEggs ×3 + RoyalJelly ×2 → QueenBee [BeeHive, skill 6])
  → WaxFrame (WorkerBee + Frame → ×1 | WorkerBee ×2 + Frame ×2 → ×3 [BeeHive])
  → Propolis (ForagerBee + NaturalFiber ×2 → Propolis [BeeHive])
```

> QueenBee also drops from `OccupiedSwarm` at a 17 % rate.

**Raw materials**
- `Honey`, `BeeWax`, `BeePollen`, `Propolis`, `RoyalJelly`, `BeeColonyCore`
- `QueenBee` *(also a spare part for hives)*, `WorkerBee`, `ForagerBee`, `BeeEggs`
- `Frame`, `WaxFrame`, `EmptyPot`

**Processed goods**
- `HoneySugar`, `PureWax`, `BiodieselFromWax`, `SimpleSyrupFromHoney`

**Food** (26 recipes)
`BerryHoneyCake`, `BlueberryCocktail`, `CandiedFruit`, `FruitCake`, `Gingerbread`, `HardHoneyCandy`, `HerbalHoneyTea`, `HoneyBeanCake`, `HoneyBeer`, `HoneyBread`, `HoneyGlazedMushrooms`, `HoneyGlazedVegetables`, `HoneyPastries`, `HoneyRoast`, `HoneyVinegar`, `HoneyWheatRolls`, `HoneycombCandy`, `Mead`, `Madeleine`, `Marshmallow`, `Pancake`, `RoyalCandy`, `RoyalHoneyCake`, `RoyalHoneyTart`, `SalmonHoney`, `SkewerMeatAndHoney`, `SweetCharredFish`, `SweetCharredMeat`, `Tajine`, `WaxSealedBread`

**Decoration & construction**
- `WaxCandle`, `WaxLumber`
- `BeeStatue`, `DecorativeHoneyPot`, `StoneVessel`
- `HoneycombShelf` (5 variants, light + dark)

**Player hives**

Both hives require two resources to operate:
- **Queen Bee** (spare part) — ages while the hive produces; the hive stops when she dies. Replace her before she reaches 0 % durability. Requires `Beekeeping 1` or `Self Improvement 5` to swap.
- **Bee Colony Core** (fuel) — day-to-day energy source; consumed over time.

| Hive | Skill | Craft ingredients | Specialty |
|---|---|---|---|
| `SmallBeeHive` | Carpentry 2 | WoodBoard ×10, IronBar ×1, Nail ×8, **QueenBee ×1** | Honey production, basic breeding |
| `BeeHive` | Carpentry 6 | WoodBoard ×14, IronBar ×2, HewnLog ×5, **QueenBee ×1** | Advanced breeding, RoyalJelly, ForagerBee training |

---

### Skill Tree

`Beekeeping` skill with four talents:

| Talent | Effect |
|---|---|
| `Focused Workflow: Beekeeping` | Doubles table speed when working alone |
| `Parallel Processing: Beekeeping` | +20 % speed when multiple same tables share a room |
| `Frugal Workspace: Beekeeping` | Lowers tier requirement by 0.2 |
| `Lavish Workspace: Beekeeping` | +0.2 tier requirement, −5 % resource cost |

---

### Admin Commands

All accessible from in-game chat at Admin authorization level.

| Command | Description |
|---|---|
| `/BeekeepingHelp` | Lists all mod commands |
| `/ClusterStatus` | Real-time ecosystem overview (hive counts, cluster health) |
| `/ResetCluster confirm` | Destroys all wild hives and regenerates the ecosystem from scratch |
| `/TestRegeneration` | Simulates a harvest to verify the regeneration system |
| `/BeekeepingItems` | Gives 1 of every mod item to your inventory |

> `/ResetCluster` requires the `confirm` argument to prevent accidental execution.

---

### Server Configuration

On first startup the mod generates `Configs/Beekeeping.eco`, editable from the web admin UI or directly as JSON.

| Parameter | Default | Description |
|---|---|---|
| `ClusterRadius` | 60 | Radius in blocks of each wild hive cluster |
| `MinClusterSpacing` | 50 | Minimum distance between two cluster centers |
| `MaxHivesPerCluster` | 5 | Maximum wild hives per cluster |
| `ClusterDeathThreshold` | 0.08 | Plant fraction below which a cluster stops regenerating |
| `MinRegenHours` | 1.0 | Minimum hours before a harvested hive slot regenerates |
| `MaxRegenHours` | 20.0 | Maximum hours before a harvested hive slot regenerates |
| `MinColonizationHours` | 0.5 | Minimum hours before a vacant swarm colonizes |
| `MaxColonizationHours` | 6.0 | Maximum hours before a vacant swarm colonizes |
| `PollinationRadius` | 10 | Radius in blocks for crop growth boost |
| `GrowthBoostPercent` | 15 | Growth speed boost applied to crops (%) |

---

### Localization

The mod ships with a translation file at `Mods/Translations/Beekeeping.csv`.

| Language | Status |
|---|---|
| English | ✅ Full |
| French | ✅ Full |
| German | ✅ Full |
| Russian | ✅ Full |
| All others | English fallback |

---

## Installation

### From the release ZIP

Extract the ZIP at the root of your Eco server. It follows the standard mod folder layout:

```
Mods/
├── UserCode/Beekeeping/
│   ├── Beekeeping.dll
│   └── Beekeeping.unity3d
└── Translations/
    └── Beekeeping.csv
```

Restart the server. On first launch the mod auto-generates `Configs/Beekeeping.eco` with default values.

> On an existing map with no cluster save file, use `/ResetCluster confirm` to initialize the ecosystem.

### Building from source

Requirements:
- .NET SDK (matching the Eco server target)
- Eco server DLLs as references (see `Beekeeping.csproj`)

```bash
cd Beekeeping-Mod_Eco_SLG
dotnet build Beekeeping.csproj -c Release
```

Copy `bin/Beekeeping.dll` to `Mods/UserCode/Beekeeping/Beekeeping.dll` on the server.

### Debug mode

In [`Server/BeeHiveGeneration.cs`](Server/BeeHiveGeneration.cs), set `DEBUG_MODE` to `true` to:
- Receive real-time diagnostic messages in chat (admins only)
- Accelerate spawn delays (60 seconds instead of 1–20 hours)

```csharp
public const bool DEBUG_MODE = false; // set to true to debug
```

---

## Project Structure

```
Beekeeping-Mod_Eco_SLG/
├── Beekeeping.sln
├── Beekeeping.csproj
├── Server/
│   ├── BeeHiveGeneration.cs        # Plugin entry point, cluster system & regeneration
│   ├── BeekeepingConfig.cs         # Admin-configurable parameters
│   ├── Core/
│   │   ├── ClusterManager.cs       # Cluster persistence and reconstruction
│   │   ├── HiveCluster.cs          # Cluster data model
│   │   ├── ColonizationComponent.cs # Automatic colonization of vacant swarms
│   │   ├── HoneyComponent.cs       # Honey production
│   │   ├── PollinationComponent.cs # Crop growth boost
│   │   ├── QueenBeeComponent.cs    # Queen bee attraction logic
│   │   ├── NeighborBeeHiveComponent.cs # Hive spacing enforcement
│   │   └── BeekeepingCommands.cs   # Admin commands
│   ├── Food/                       # Food recipes (30 files)
│   ├── Item/                       # Item definitions
│   ├── Benefit/                    # Talents
│   ├── Tech/                       # Skill tree
│   └── PluginModule/               # Plugin upgrade module
├── Client/
│   └── Beekeeping.unity3d          # Unity asset bundle (3D models, textures, icons)
└── Mods/
    └── Translations/
        └── Beekeeping.csv          # Localization (EN/FR/DE/RU + EN fallback)
```
