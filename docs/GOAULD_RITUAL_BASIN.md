# Goa'uld ritual basin

## Scope of 0.1.25-dev

This milestone adds the first Core + Biotech environmental requirement for
controlled Goa'uld implantation ceremonies.

## Building

```text
SG1_GoauldRitualBasin
```

Player-facing label:

```text
Goa'uld ritual basin
```

French label:

```text
bassin rituel Goa'uld
```

## Construction

The first balance values are intentionally simple:

| Property | Value |
|---|---:|
| Category | Furniture |
| Size | `1 × 1` |
| Steel | `60` |
| Gold | `5` |
| Work to build | `1200` |
| Hit points | `180` |

The current graphic is a temporary placeholder.

## Ritual rule

The selected free symbiote and ritual target must both remain within:

```text
6 cells
```

of the same spawned basin.

The original symbiote-to-target ritual range remains:

```text
12 cells
```

## Flow

```text
Goa'uld ritual basin built
    ↓
free symbiote and compatible target remain close to the basin
    ↓ explicit target selection
600-tick ceremony
    ↓ basin remains spawned and in range
recent Goa'uld implantation
```

## Cancellation

The active ceremony is cancelled if:

```text
the basin is destroyed
the basin is removed from the map
the free symbiote moves too far from the basin
the target moves too far from the basin
the previous continuity requirements fail
```

The symbiote identity remains available after cancellation.

## DLC strategy

This structure belongs to the required `Core + Biotech` fallback.

A future optional `Ideology` integration can reuse the basin as a ritual focus,
add ideological roles and evaluate richer ceremony quality without replacing
the existing identity-transfer core.

## Test checklist

1. Build with `build.cmd`.
2. Build or spawn one Goa'uld ritual basin.
3. Spawn a free symbiote and disable autonomous hunt.
4. Keep the symbiote within 6 cells of the basin.
5. Keep one compatible target within 6 cells of the same basin.
6. Start ritual implantation and select that target.
7. Confirm the basin appears in the inspection panel.
8. Let the ritual finish and verify persistent identity transfer.
9. Repeat and destroy the basin during the ritual.
10. Confirm automatic cancellation without consuming the symbiote.
11. Repeat and move the target away from the basin.
12. Confirm automatic cancellation.
13. Try starting a ritual without a basin nearby.
14. Confirm rejection.
