# Controlled Goa'uld Jaffa destruction raid

Version: `0.1.72-dev`

## Scope

This milestone adds a third developer-only incident:

```text
SG1_GoauldJaffaControlledDestructionRaid
```

Its storyteller base chance is exactly `0`. At the original `0.1.72-dev`
milestone, natural Goa'uld raids, settlements and traders were still disabled.
Since `0.3.54-dev`, the separate natural incident may explicitly reuse this
strategy while the controlled incident remains deterministic.

## Doctrine

The raid uses:

```text
SG1_GoauldJaffaDestructionAssault
LordJob_GoauldJaffaDestructionAssault
LordToil_GoauldJaffaRecoveryCover
```

## Phase 1 — Military assault

The group begins in `LordToil_AssaultColony`.

During this phase, opportunistic stealing and kidnapping are disabled. The
raid remains focused on combat until either:

- colony damage exceeds `15 %` of initial colony health, with a minimum
  threshold of `300` damage;
- or `12000` ticks pass.

## Phase 2 — Opportunistic recovery with cover

The group then enters `LordToil_GoauldJaffaRecoveryCover`.

Every `181` ticks, available Jaffa are reassigned:

1. preserve any currently carried victim or item;
2. prioritize a nearby downed player colonist within `8` cells;
3. otherwise take a valuable stealable item within `7` cells;
4. otherwise keep `AssaultColony` duty and cover the recovery.

Individual opportunistic tasks remain gated by
`!GenAI.InDangerousCombat(pawn)`.

## Phase 3 — Extraction

After `2400` ticks of recovery, all surviving Jaffa receive
`ExitMapBest`. Pawns keep any already carried victim or item while leaving.

## Relationship with the other controlled doctrines

| Incident | Main purpose | Post-assault behavior |
|---|---|---|
| Direct assault | sustained combat baseline | none |
| Abduction | early capture after first admissible victim | extraction after capture window |
| Destruction | military damage first | opportunistic victims and valuables after victory or timeout |

## Historical manual test checklist

1. Build the mod and start RimWorld with developer mode enabled.
2. Confirm that no new XML, DefOf, translation or C# loading error appears.
3. Trigger:
   `Do incident (Map) > controlled Goa'uld Jaffa destruction test raid`.
4. Confirm that the Jaffa attack normally and do not immediately steal or
   kidnap.
5. Let the Jaffa damage colony buildings, or allow `12000` ticks to pass.
6. Confirm that a post-assault recovery phase begins.
7. Place a valuable stealable item near an available Jaffa and verify that
   one available pawn may take it.
8. Down a player colonist near an available Jaffa and verify that victims
   have priority over items.
9. Confirm that unassigned Jaffa continue fighting during the recovery
   window.
10. Let `2400` recovery ticks pass and confirm that surviving Jaffa extract
    with or without recovered targets.
11. Repeat with `Do incident (points)` and a larger group.
12. Recheck the original direct-assault and abduction incidents.
13. For the original milestone only, confirm that natural Goa'uld raids were
    not enabled by the controlled IncidentDef.
