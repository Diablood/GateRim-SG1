# Jaffa xenotype foundation

## Definitions

```text
SG1_Jaffa
SG1_JaffaPhysiology
SG1_JaffaLongevity
```

The current Jaffa implementation is a Biotech xenotype assembled from vanilla genes plus GateRim-specific genes.

## Appearance policy

Jaffa must not all look like visibly muscular RimWorld hulks.

The vanilla `Body_Hulk` gene remains excluded because it restricts the visible body shape. No `Body_*` gene is imposed by the Jaffa xenotype.

## Custom genes

| Gene | Effect | Purpose |
|---|---:|---|
| `SG1_JaffaPhysiology` | `CarryingCapacity +15` | Physical baseline without forced Hulk appearance |
| `SG1_JaffaLongevity` | `LifespanFactor ×1.5` | Slower aging and later onset of age-related conditions |

## Current xenotype gene set

| Gene | Purpose |
|---|---|
| `SG1_JaffaPhysiology` | Strength baseline without forced Hulk appearance |
| `SG1_JaffaLongevity` | 150% lifespan expectancy |
| `AptitudeStrong_Melee` | Military melee training approximation |
| `Immunity_Strong` | Improved immune response |
| `MeleeDamage_Strong` | Increased close-combat lethality |
| `Pain_Reduced` | Better combat endurance |
| `Robust` | Reduced incoming damage |
| `WoundHealing_Fast` | Accelerated recovery |
| `Superclotting` | Reduced bleeding risk |
| `StrongStomach` | Increased digestive resilience |

## Intentional limitations

The current prototype does not yet model:

- the abdominal symbiote pouch;
- the immature Goa'uld symbiote as a removable entity;
- dependence on a symbiote or tretonin;
- Goa'uld detection at close range;
- weighted Jaffa body-type generation;
- dedicated Jaffa xenotype and gene artwork.

The longevity gene is a temporary XML representation. It may later be attached to the symbiote system.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Start a temporary development game.
3. Open the xenotype editor during pawn creation.
4. Confirm that `Jaffa` appears in the xenotype list.
5. Confirm that `Jaffa physiology` and `Jaffa longevity` appear among its genes.
6. Confirm that lifespan expectancy is displayed as `150%`.
7. Generate several Jaffa pawns and verify that they are not all visually Hulk-bodied.
8. Check the `Player.log` for XML errors mentioning `SG1_Jaffa`, `SG1_JaffaPhysiology` or `SG1_JaffaLongevity`.
