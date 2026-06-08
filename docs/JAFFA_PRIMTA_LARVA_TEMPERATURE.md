# Prim'ta larva temperature tuning

## Scope of 0.1.32-dev

This milestone adds a lightweight living-larva temperature layer on top of
RimWorld's vanilla rotting system.

## Architecture

The larva keeps vanilla:

```text
CompProperties_Rottable
```

and adds:

```text
GateRimSG1.Jaffa.CompProperties_PrimtaLarvaTemperature
GateRimSG1.Jaffa.Comp_PrimtaLarvaTemperature
```

The custom comp is intentionally listed before vanilla `CompRottable` in XML.
Its additional heat damage is therefore applied before vanilla checks stage
changes and destroys a fully rotted larva during the same rare tick.

## Temperature table

| Temperature | Result | Effective rate |
|---|---|---:|
| Below `0 °C` | Frozen, deterioration stopped | `×0` |
| `0 °C` to `10 °C` | Refrigerated, recommended storage | Vanilla `×0` to `×1` |
| Above `10 °C` and below `25 °C` | Warm storage | `×1` |
| `25 °C` to below `40 °C` | Hot storage | `×2` |
| `40 °C` and above | Critical heat | `×3` |

The effective rate is relative to vanilla room-temperature deterioration.

## Inspection panel

Selecting a larva now displays:

```text
Larva temperature
Thermal condition
Effective deterioration rate
```

## Scope boundary

Deep-freezing penalties are deliberately not implemented yet.

For this first pass, freezing remains a safe simplification. A later milestone
can evaluate whether deep freezing should damage living larvae or require
specialized Goa'uld containers.

## Test checklist

1. Build with `build.cmd`.
2. Spawn or incubate several Prim'ta larvae.
3. Select a larva and confirm the new thermal lines appear.
4. Compare storage below `0 °C`, around `5 °C`, around `20 °C`, above `25 °C`
   and above `40 °C`.
5. Confirm the displayed deterioration rate changes.
6. Confirm hot storage destroys larvae faster than room temperature.
7. Confirm freezing stops deterioration for this prototype.
8. Confirm incubation, hauling, stacking, storage filters and Jaffa implantation
   still work.
