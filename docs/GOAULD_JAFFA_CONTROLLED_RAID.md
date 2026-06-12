# Controlled Goa'uld Jaffa test raid

Version: `0.1.69-dev`

## Scope

This milestone adds a developer-only raid incident:

```text
SG1_GoauldJaffaControlledRaid
```

Its storyteller base chance is exactly `0`. The incident must be triggered
manually through RimWorld developer tools. Since `0.2.1-dev`, visible Goa'uld
world settlements and one separate low-frequency natural direct-assault
incident exist, while this controlled incident remains available for precise
regression tests.

## Runtime faction

`GoauldSystemLordFactionUtility` normally reuses the visible world-generated
instance of `SG1_GoauldSystemLordPrototype`.

For older saves or isolated tests without that faction, the utility can still
create one visible runtime fallback. The fallback is reused on subsequent
controlled raids but does not retroactively generate settlements.

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
9. Trigger the incident a second time and confirm that the existing faction
   instance is reused.
10. Confirm that the separate natural direct-assault incident remains
    available without modifying the controlled test path.

## Late-created faction attack-target cache refresh

Since `0.1.70-dev r4`, the resolved System Lord faction refreshes each
loaded map's attack-target cache after it is created or reused.

The fallback faction can still be created lazily, after the player map
and colonists may already exist. Initial faction relations alone are not enough
to rebuild attack-target cache entries for previously spawned pawns.

The utility therefore calls:

```csharp
map.attackTargetsCache.Notify_FactionHostilityChanged(
    goauldFaction,
    otherFaction);
```

for every loaded map and every other known faction before the raid worker
generates its pawns.

## Temporary diagnostics cleanup

The `r3` diagnostic component is no longer required after this fix. Remove:

```text
Source/GateRimSG1/Goauld/GameComponent_GoauldJaffaRaidDiagnostics.cs
docs/GOAULD_JAFFA_RAID_DIAGNOSTICS.md
```

before the final commit.

## Controlled direct-assault baseline

Since `0.1.70-dev r5`, the developer-only controlled raid disables vanilla
stealing explicitly:

```csharp
parms.canSteal = false;
```

`RaidStrategyWorker_ImmediateAttack` forwards `IncidentParms.canSteal` to
`LordJob_AssaultColony`. When stealing is allowed, the vanilla lord graph can
transition to `LordJob_Steal` as soon as high-value items are detected.

The controlled incident exists to validate sustained Jaffa combat behavior, so
this transition is intentionally disabled for the test raid only.

Timeout and flee behavior remain vanilla. Kidnapping behavior is not changed by
this patch and can be reviewed separately as a lore and balance decision before
natural Goa'uld raids are enabled.

## Direct-assault kidnapping policy

Since `0.1.71-dev`, the direct-assault test incident explicitly disables
kidnapping as well as stealing:

```csharp
parms.canSteal = false;
parms.canKidnap = false;
```

This keeps the existing incident focused on sustained combat. The separate
controlled abduction incident validates kidnapping behavior independently.
