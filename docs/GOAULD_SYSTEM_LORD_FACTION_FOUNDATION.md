# Goa'uld System Lord faction foundation

## Status

Implemented as a hidden hostile faction prototype in `0.1.61-dev`.

## Scope

This milestone introduces:

```text
SG1_GoauldSystemLordPrototype
```

The definition establishes a first hostile Goa'uld System Lord domain without
activating world gameplay prematurely.

## Current behavior

The faction is:

- humanlike;
- permanently hostile;
- hidden;
- not generated automatically at game start;
- excluded from settlement generation;
- forbidden from raids;
- unable to request traders or military aid;
- excluded from quest-site generation;
- intentionally missing pawn-group profiles until Goa'uld-aligned Jaffa kinds
  exist.

## Validation-oriented fields

RimWorld `FactionDef.ConfigErrors()` requires every faction definition to include:

- a defined technology level;
- backstory categories for humanlike factions;
- a raid-loot value curve.

The prototype provides:

```text
techLevel: Spacer
backstory filter: Offworld
raidLootValueFromPointsCurve: defined
```

No `pawnGroupMakers` are added yet, so a `maxPawnCostPerTotalPointsCurve` is not
needed until the Jaffa-servant milestone.

## Next step

Add Goa'uld-aligned Jaffa `PawnKindDef` entries and nested `Combat` pawn-group
profiles while preserving a visually distinct Free Jaffa path.
