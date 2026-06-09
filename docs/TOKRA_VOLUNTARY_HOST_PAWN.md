# Tok'ra voluntary-host pawn prototype

## Scope of 0.1.40-dev

This milestone adds a developer-spawnable player-controlled human host already
carrying an active Tok'ra symbiote.

## PawnKindDef

```text
SG1_TokraVoluntaryHost
```

French label:

```text
hôte Tok'ra volontaire
```

## Purpose

The prototype prepares future Tok'ra visitors, colonies and events without
activating world-faction generation yet.

```text
developer-spawn host prototype
    ↓
player-controlled human pawn
    ↓
active Tok'ra symbiote with persistent identity
```

## Initialization component

```text
GameComponent_TokraHostPrototypeInitializer
```

The component scans spawned pawns every `60` ticks.

When it finds:

```text
pawn.kindDef == SG1_TokraVoluntaryHost
```

it adds:

```text
SG1_GoauldHostSymbiote
```

with:

```text
GoauldSymbioteOrigin.Tokra
```

## One-time initialization

The component persists initialized pawn `ThingID` values.

```text
first spawn
    ↓
one Tok'ra symbiote created

later extraction or manual removal
    ↓
no artificial replacement symbiote
```

This prevents the developer prototype from becoming an infinite symbiote source.

## Shared active-host state

The prototype reuses the generic adult-host Hediff already shared by Goa'uld
and Tok'ra origins:

```text
SG1_GoauldHostSymbiote
```

Its persistent description should display:

```text
origin: Tok'ra
```

## Scope boundaries

This milestone does not yet add:

```text
generated Tok'ra world faction
Tok'ra settlement pawn groups
Tok'ra visitors
Tok'ra traders
Tok'ra quests
Tok'ra-specific human apparel or visuals
named Tok'ra characters
active-host extraction redesign
```

## Test checklist

1. Build with `build.cmd`.
2. Spawn `Tok'ra voluntary host` through developer tools.
3. Confirm the pawn belongs to the player colony.
4. Wait up to `60` ticks.
5. Confirm the message announcing initialization.
6. Open the Health tab.
7. Confirm `adult Goa'uld-family symbiote` is present.
8. Inspect the Hediff details.
9. Confirm origin `Tok'ra`.
10. Save and reload.
11. Confirm the same persistent symbiote identity remains.
12. Spawn a second prototype host.
13. Confirm it receives a distinct Tok'ra identity.
14. Confirm the existing free Tok'ra voluntary-implantation path still works.
15. Confirm normal Goa'uld workflows remain available.


## 0.1.40-dev-r1 resistance-range loading fix

The developer-spawnable humanlike prototype explicitly declares:

```xml
<initialResistanceRange>0~0</initialResistanceRange>
```

RimWorld validates an initial recruitment-resistance range for humanlike
`PawnKindDef` entries. This prototype is generated directly under
`PlayerColony`, so a neutral range is sufficient and avoids introducing a
recruitment behavior that is not part of the milestone.
