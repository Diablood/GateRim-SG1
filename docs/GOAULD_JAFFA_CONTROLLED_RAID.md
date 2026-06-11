# Controlled Goa'uld Jaffa test raid

Version: `0.1.69-dev`

## Scope

This milestone adds a developer-only raid incident:

```text
SG1_GoauldJaffaControlledRaid
```

Its storyteller base chance is exactly `0`. The incident must be triggered
manually through RimWorld developer tools. Natural Goa'uld raids, world
settlements and traders remain disabled.

## Runtime faction

`GoauldSystemLordFactionUtility` lazily creates one hidden runtime instance
of `SG1_GoauldSystemLordPrototype` when the controlled incident is triggered.

The utility reuses the same persistent instance on subsequent test raids.

Routine success logs are intentionally omitted. Unity can attach verbose
stack traces to ordinary log messages depending on the active stack-trace
configuration. Warnings and errors remain logged when the controlled path
cannot complete.

## Vanilla raid workflow

`IncidentWorker_GoauldJaffaControlledRaid` subclasses
`IncidentWorker_RaidEnemy`.

It sets:

```csharp
parms.faction = goauldFaction;
parms.forced = true;
```

and then delegates to the vanilla raid worker. The existing nested `Combat`
pawn-group profile therefore generates the group normally.

When developer tools do not supply points, the worker defaults to `500`.

## Intended validation

- real hostile non-player Goa'uld faction;
- hidden-faction persistence across repeated controlled tests;
- vanilla raid arrival and combat behavior;
- generated Jaffa Prim'ta;
- automatic Ma'Tok loadout;
- automatic modular armor loadout;
- enemy helmet automatic synchronization;
- absence of the helmet-mode gizmo on enemy pawns.

## Manual test checklist

1. Build the mod and start RimWorld with developer mode enabled.
2. Confirm that no new XML, DefOf or C# loading error appears.
3. Open developer actions and trigger:
   `Do incident (points) > controlled Goa'uld Jaffa test raid`.
4. Use a moderate point value such as `500`.
5. Confirm that a hostile Jaffa group arrives using a real non-player
   Goa'uld System Lord faction.
6. Inspect one enemy Jaffa and verify:
   - Jaffa xenotype;
   - initial Prim'ta;
   - equipped Ma'Tok staff;
   - modular Jaffa armor;
   - retractable Jaffa helmet.
7. Confirm that the helmet-mode gizmo is absent on the enemy pawn.
8. Draft a player colonist wearing a Jaffa helmet and confirm that the same
   gizmo remains visible for the controlled player pawn.
9. Trigger the incident a second time and confirm that the existing hidden
   faction instance is reused.
10. Confirm that no natural Goa'uld raid starts without a manual developer
    trigger.
