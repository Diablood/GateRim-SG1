# Prim'ta deep-freezing penalties

## Status

Implemented in `0.1.60-dev`.

## Design goal

Refrigerators and moderate freezers remain useful fallback storage, but extreme
cold is no longer an unlimited optimal solution for living Prim'ta resources.

```text
powered preservation basin
    -> full biological protection
    -> accumulated deep-freeze exposure decreases

temperature above -15 °C
    -> no deep-freeze accumulation
    -> accumulated exposure decreases

temperature at or below -15 °C
    -> deep-freeze exposure accumulates
    -> first day tolerated
    -> slow biological deterioration after the grace period

temperature at or below -30 °C
    -> faster biological deterioration after the grace period
```

## Tuned values

| Parameter | Value |
|---|---:|
| Deep-freeze threshold | `-15 °C` |
| Critical deep-freeze threshold | `-30 °C` |
| Grace period | `60000` ticks / `1` RimWorld day |
| Prolonged deep-freezing deterioration | `×0.25` |
| Critical deep-freezing deterioration | `×0.50` |
| Recovery rate in safer storage | `×2` |

## Supported resources

- `SG1_PrimtaLarva`
- `SG1_ImmaturePrimtaSymbiote`

## Persistence

Deep-freezing exposure is saved and restored. It is blended when stacks merge and
copied when a stack is split.

## Intentional limits

The system does not yet add a separate visible injury object or resource-quality
tier. It uses the existing biological deterioration model to keep the first
iteration readable and compatible with the current incubation and implantation
loops.
