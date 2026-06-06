# Jaffa xenotype foundation

## Definition

```text
SG1_Jaffa
```

The initial Jaffa implementation is intentionally limited to a Biotech xenotype assembled from vanilla genes.

## Current gene set

| Gene | Purpose |
|---|---|
| `Body_Hulk` | Reinforced warrior build |
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
- dedicated Jaffa xenotype artwork.

These elements should be added incrementally after the XML-only foundation loads correctly.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Start a temporary development game.
3. Open the xenotype editor during pawn creation.
4. Confirm that `Jaffa` appears in the xenotype list.
5. Create a Jaffa pawn.
6. Confirm that the health, immunity and melee-related gene effects appear.
7. Check the Player.log for XML errors mentioning `SG1_Jaffa`.
