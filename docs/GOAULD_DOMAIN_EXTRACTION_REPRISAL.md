# Goa'uld domain extraction reprisal

Version: `0.3.55-dev`

## Purpose

This milestone creates the first cause-driven Goa'uld domain reaction. It does
not add a mission, offer catalogue, trust meter or generic hidden grievance
score.

The cause is a successful `SG1_ExtractActiveGoauldSymbiote` surgery on a player
home map. The persistent symbiote data identifies the exact System Lord domain
that considers the extraction an affront.

## Reaction contract

- warning: immediate, naming domain, symbiote and former host;
- delay: `60000` to `180000` ticks, or one to three days;
- strength: vanilla threat points captured when extraction succeeds;
- consequence: existing `SG1_GoauldJaffaNaturalRaid`;
- arrival: existing forced `EdgeWalkIn` contract;
- stacking: one pending reprisal per domain;
- cooldown: `900000` ticks, or 15 days, after resolution.

The shared raid worker now honors an explicitly supplied Goa'uld faction. Its
old fallback still resolves or creates a faction for legacy controlled paths.

## Persistence and migration

`GameComponent_GoauldDomainReprisalTracker` stores deep reaction states with a
Faction reference, target map ID, due tick, point snapshot and visible cause.

Generated Goa'uld hosts now record their faction in the symbiote identity when
created. Existing generated hosts are backfilled only while they still belong
to an unambiguous System Lord faction; no guessed domain is assigned after that
information has already been lost.

## Deferred choices

Tribute, surrender demands and ultimatums are not part of this milestone. A
future choice must name a real cause and domain, expose its consequence and use
resources meaningful to RimWorld rather than inventing a Tok'ra-style offer.

## Required validation

Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Reset, then schedule an extraction reprisal.
2. Verify the immediate localized warning and exact domain.
3. Inspect the pending map and point snapshot.
4. Save/reload and verify the same pending state.
5. Trigger it now and verify a domain-aligned edge-arrival raid.
6. Inspect the resulting cooldown and `Player.log`.

Optional integration test: complete the real active-host extraction surgery on
a secured Goa'uld prisoner and verify that it schedules the same reaction
without using the debug scheduler.

Final validation passed on `0.3.55-dev-r1`: warning, persisted state,
domain-aligned raid, edge arrival, cooldown and `Player.log` are accepted.
