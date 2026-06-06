# Jaffa xenotype foundation

## Definitions

```text
SG1_Jaffa
SG1_JaffaPhysiology
```

The initial Jaffa implementation is a Biotech xenotype assembled from vanilla genes plus one GateRim-specific gene.

## Appearance policy

Jaffa must not all look like visibly muscular RimWorld hulks.

The vanilla `Body_Hulk` gene was removed because it restricts the visible body shape. No `Body_*` gene is currently imposed by the Jaffa xenotype, leaving appearance unconstrained during pawn generation.

The custom `SG1_JaffaPhysiology` gene represents an enhanced physical baseline independently from appearance.

## Custom physiology gene

| Property | Current value | Purpose |
|---|---:|---|
| `defName` | `SG1_JaffaPhysiology` | GateRim-specific Jaffa identity |
| `CarryingCapacity` offset | `+15` | Modest strength benefit without a visible body lock |
| Metabolic efficiency | `-1` | Initial balance cost |
| Complexity | `1` | Initial balance cost |
| Visible body type | Not forced | Preserve visual variety |

## Current xenotype gene set

| Gene | Purpose |
|---|---|
| `SG1_JaffaPhysiology` | Strength baseline without forced Hulk appearance |
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
- the immature Goa'uld symbiote;
- dependence on a symbiote or tretonin;
- Goa'uld detection at close range;
- weighted Jaffa body-type generation;
- dedicated Jaffa xenotype and gene artwork.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Start a temporary development game.
3. Open the xenotype editor during pawn creation.
4. Confirm that `Jaffa` appears in the xenotype list.
5. Confirm that `Jaffa physiology` is listed among its genes.
6. Generate several Jaffa pawns and verify that they are not all visually Hulk-bodied.
7. Confirm the `+15` carrying-capacity effect.
8. Check the `Player.log` for XML errors mentioning `SG1_Jaffa` or `SG1_JaffaPhysiology`.
