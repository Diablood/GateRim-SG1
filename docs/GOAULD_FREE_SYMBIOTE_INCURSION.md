# Goa'uld free-symbiote incursion

## Scope of `0.3.39-dev`

This milestone turns the established free-symbiote autonomous-hunt prototype
into a rare recurrent Goa'uld biological threat.

It deliberately reuses:

- `SG1_GoauldSymbiote`;
- `Comp_GoauldForcedImplantation`;
- `SG1_GoauldAutonomousImplant`;
- the persistent `GoauldSymbioteData` transfer path;
- the existing visible Goa'uld System Lord faction.

It does not add a second implantation system, a Tok'ra operation, a world site
or a dedicated storyteller dependency.

## Natural incident

```text
SG1_GoauldFreeSymbioteIncursion
```

The incident can target a player home map when:

- the visible Goa'uld System Lord faction exists;
- at least one living compatible player colonist aged thirteen or older is on
  the map;
- a reachable, unfogged hostile edge-entry cell exists.

Current storyteller settings:

| Property | Value |
|---|---:|
| Category | `ThreatSmall` |
| Base chance | `0.035` |
| Earliest day | `18` |
| Minimum refire delay | `24` days |

## Adaptive group size

The worker consumes the incident threat points supplied by the active
storyteller. A forced developer incident without explicit points falls back to
`StorytellerUtility.DefaultThreatPointsNow(map)`.

| Threat points | Symbiotes |
|---:|---:|
| below `800` | `1` |
| `800–1599` | `2` |
| `1600–2399` | `3` |
| `2400+` | `4` |

The cap remains intentionally low. A symbiote has little direct combat power,
but reaching a compatible pawn starts the persistent Goa'uld implantation path.

## RP variation

Three English and French warning letters are available. The last selected
variant is stored in
`GameComponent_GoauldFreeSymbioteIncursionTracker`, so the immediately previous
text is excluded after save and reload.

## Developer tools

```text
Debug actions
→ GateRim SG-1
→ Goa'uld...
→ Free-symbiote incursion...
```

Available actions:

- `Show current scaling`;
- `Force current scaling`;
- `Force weak-colony scaling` with `300` points;
- `Force advanced-colony scaling` with `2600` points.

## Known boundary

The incident uses the same implantation outcome as every existing free Goa'uld
symbiote. This milestone does not change host faction, player control, identity
persistence, extraction or active-host behavior. Any future change to hostile
host allegiance must be designed and tested as a separate system-wide
milestone rather than hidden inside this incident.

## Final validation

Local revision `r1` was validated with `GateRimSG1.dll` version `0.3.39.0`.
The weak and advanced developer profiles produced one and four hostile
symbiotes respectively, autonomous pursuit and persistent identity transfer
used the established Goa'uld systems, and the warning-text exclusion survived
save and reload. Regression checks confirmed that Tok'ra symbiotes, existing
Goa'uld implantation and extraction paths, and Tok'ra organic operations remain
unchanged.

The initial load error was caused by RimWorld still loading the previously
built `0.3.38.0` assembly. Rebuilding the project to `0.3.39.0` resolved the
missing IncidentWorker type without a source-code change.

