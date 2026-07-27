# Beekeeping Mod — Eco SLG

Mod open source d'apiculture pour le jeu [Eco](https://play.eco/). Il introduit un écosystème de ruches sauvages autonome, une filière de crafting autour du miel et de la cire, ainsi qu'une arbre de compétences dédié.

---

## Fonctionnalités

### Écosystème de ruches sauvages

Le cœur du mod est un système de clusters qui gère la population de ruches sauvages sur la carte de façon autonome.

**Ruches**
- `VacantSauvageBeehive` — ruche vacante, colonisable naturellement après 30 min à 6 h (délai aléatoire)
- `OccupiedSauvageBeehive` — ruche occupée, récoltable par les joueurs

**Clusters**
- Les ruches sont regroupées en clusters (rayon : 60 blocs, espacement minimum entre clusters : 50 blocs)
- Chaque cluster peut contenir jusqu'à 5 ruches
- La **santé végétale** du cluster (ratio `CurrentPlantCount / MaxPlantCount`) détermine sa capacité maximale :

| Santé végétale | Capacité |
|---|---|
| ≥ 85 % | 5 ruches |
| ≥ 68 % | 4 ruches |
| ≥ 51 % | 3 ruches |
| ≥ 34 % | 2 ruches |
| ≥ 17 % | 1 ruche |
| < 17 % | Cluster mort (pas de régénération) |

**Régénération**
- Quand un cluster tombe à ≤ 2 ruches, 1 à 5 nouvelles ruches sont programmées pour apparaître dans un délai de 1 à 20 heures (mode production)
- Les ruches apparaissent sur des blocs de terre libres à l'intérieur du rayon du cluster
- L'état des clusters est sauvegardé dans un fichier et reconstruit au redémarrage du serveur

---

### Items & crafting

**Matières premières**
- `Honey`, `BeeWax`, `BeePollen`, `Propolis`, `RoyalJelly`
- `QueenBee`, `WorkerBee`, `ForagerBee`
- `Frame`, `WaxFrame`, `EmptyPot`

**Transformés**
- `HoneySugar`, `Purewax`, `BiodieselFromWax`

**Nourriture** (23 recettes)
`BerryHoneyCake`, `CandiedFruit`, `HardHoneyCandy`, `HerbalHoneyTea`, `HoneyBeanCake`, `HoneyBeer`, `HoneyBread`, `HoneyGlazedMushrooms`, `HoneyGlazedVegetables`, `HoneyPastries`, `HoneyWheatRolls`, `HoneycombCandy`, `Mead`, `RoyalCandy`, `RoyalHoneyCake`, `RoyalHoneyTart`, `SweetCharredFish`, `SweetCharredMeat`, `WaxSealedBread`

**Décoration & construction**
- `WaxCandle`, `WaxLumber`, `WaxSealedBread`
- `BeeStatue`, `SmallMoaiStatue`, `MediumMoaiStatue`, `BigMoaiStatue`

**Ruche craftable**
- `SmallBeeHive` — ruche placée par le joueur, avec extraction mécanique (`HoneyExtractMechanical`)

---

### Arbre de compétences

Compétence `Beekeeping` avec quatre talents :

| Talent | Effet |
|---|---|
| `BeekeepingFocusedSpeedTalent` | Vitesse accrue en solo |
| `BeekeepingParallelSpeedTalent` | Vitesse accrue en parallèle |
| `BeekeepingFrugalReqTalent` | Réduction des ressources requises |
| `BeekeepingLavishResourcesTalent` | Bonus de rendement (mode opulence) |

---

### Commandes admin

Accessibles via le chat en jeu, niveau autorisation Admin.

| Commande | Description |
|---|---|
| `/BeekeepingHelp` | Liste toutes les commandes du mod |
| `/ClusterStatus` | Vue d'ensemble de l'écosystème en temps réel |
| `/ResetCluster` | Supprime toutes les ruches sauvages et régénère l'écosystème |
| `/TestRegeneration` | Simule une récolte pour tester la régénération |

---

## Installation

### Prérequis

- Serveur Eco (version compatible avec le mod — voir `Beekeeping.sln`)
- SDK Eco (disponible dans `D:\github\Eco\`) pour compiler le projet
- Unity (pour modifier les assets client dans `D:\github\Beekeeping-Mod_Eco_SLG\Client\`)

### Côté serveur

1. Ouvrir `Beekeeping.sln` dans Visual Studio ou Rider
2. Référencer les DLL Eco depuis votre installation serveur
3. Compiler le projet — la DLL générée va dans `Mods/` sur le serveur
4. Démarrer ou redémarrer le serveur

> Au premier lancement sur une carte vierge, le système crée automatiquement les clusters initiaux.  
> Sur une carte existante sans fichier de sauvegarde, utiliser `/ResetCluster` pour initialiser les clusters.

### Côté client

Copier le contenu du dossier `Client/` dans le dossier `Mods/` du client Eco :
- `BeekeepingScene1.2.1.unity3d` — bundle Unity contenant les modèles 3D et textures
- Les dossiers `Items/`, `Textures/`, `3D models/` contiennent les sources des assets

### Mode debug

Dans [`Server/BeeHiveGeneration.cs`](Server/BeeHiveGeneration.cs) (ligne 35), passer `DEBUG_MODE` à `true` pour :
- Recevoir des messages de diagnostic en temps réel dans le chat (admins uniquement)
- Accélérer les délais de spawn (60 secondes au lieu de 1–20 heures)

```csharp
public const bool DEBUG_MODE = false; // passer à true pour déboguer
```

---

## Structure du projet

```
Beekeeping-Mod_Eco_SLG/
├── Beekeeping.sln
├── Server/
│   ├── PluginModule/       # Point d'entrée du mod (Beekeeping.cs, BeekeepingUpgrade.cs)
│   ├── BeeHiveGeneration.cs    # Système de clusters et de régénération
│   ├── ClusterManager.cs       # Gestion des clusters (sauvegarde, reconstruction)
│   ├── HiveCluster.cs          # Modèle de données d'un cluster
│   ├── ColonizationComponent.cs # Colonisation automatique des ruches vacantes
│   ├── HoneyComponent.cs       # Production de miel
│   ├── QueenBeeComponent.cs    # Logique reine des abeilles
│   ├── NeighborBeeHiveComponent.cs
│   ├── BeekeepingCommands.cs   # Commandes admin
│   ├── Food/                   # Recettes alimentaires (23 fichiers)
│   ├── Item/                   # Définitions des items
│   ├── Benefit/                # Talents
│   └── Tech/                   # Arbre de compétences
└── Client/
    ├── BeekeepingScene1.2.1.unity3d
    ├── Items/
    ├── Textures/
    └── 3D models/
```
