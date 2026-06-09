# Tok'ra pawn-group foundation prototype

## Scope of 0.1.41-dev

This milestone adds the first Tok'ra pawn-group foundation while keeping world
generation disabled.

## 0.1.41-dev-r1 loading fix

The first local version incorrectly declared:

```text
PawnGroupMakerDef
```

as a standalone XML Def type.

RimWorld expects pawn-group makers to be nested inside:

```text
FactionDef
    ↓
pawnGroupMakers
```

The corrected structure is now stored in:

```text
1.6/Defs/FactionDefs/SG1_Tokra.xml
```

The previous file remains as an intentionally empty XML placeholder so applying
the ZIP patch cleanly overwrites the invalid content without requiring manual
deletion.

## Current nested profiles

```text
Tok'ra Combat profile
Tok'ra Peaceful profile
```

Both currently use:

```text
SG1_TokraVoluntaryHost
```

as their only pawn option.

## Why use Peaceful instead of Trader

The visitor foundation is currently represented by a `Peaceful` profile.

A true `Trader` profile will be introduced only after trader kinds, carriers,
guards and faction supply behavior are designed.

## Balancing curve

Because the faction now contains pawn-group makers, it also declares:

```text
maxPawnCostPerTotalPointsCurve
```

## Generation remains disabled

The Tok'ra faction still remains:

```text
hidden
non-generated
without settlements
without automatic visitors
without automatic traders
without diplomacy events
```

The nested profiles provide a valid technical foundation only.

## Test checklist

1. Apply the `0.1.41-dev-r1` patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm this error no longer appears:
   ```text
   Type PawnGroupMakerDef is not a Def type or could not be found
   ```
5. Confirm no new error references:
   ```text
   SG1_Tokra
   pawnGroupMakers
   maxPawnCostPerTotalPointsCurve
   ```
6. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
7. Repeat the free Tok'ra, Tok'ra host and Goa'uld regression tests.


## 0.1.42-dev first Peaceful-profile use

The nested Tok'ra `Peaceful` profile is now used by the manually triggered:

```text
SG1_TokraPeacefulVisitors
```

incident.

Automatic storyteller selection and world generation remain disabled.
