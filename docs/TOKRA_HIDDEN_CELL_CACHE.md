# Hidden Tok'ra cell cache baseline

Version: `0.2.12-dev`

## Purpose

This milestone gives the persistent hidden Tok'ra world presence its first
small map-level footprint without creating a normal faction settlement.

The event represents a clandestine Tok'ra cell briefly making contact, leaving
a small medical cache, and disappearing again.

## Incident

```text
SG1_TokraHiddenCellCache
```

Worker:

```text
GateRimSG1.Goauld.IncidentWorker_TokraHiddenCellCache
```

Parameters:

```text
baseChance: 0.012
earliestDay: 30
minRefireDays: 45
target: Map_PlayerHome
letterDef: PositiveEvent
```

## Faction reuse

The worker calls:

```text
TokraFactionUtility.GetOrCreatePersistentFaction(...)
```

It therefore reuses the same hidden `SG1_Tokra` faction as:

```text
Tok'ra peaceful visitors
Tok'ra therapeutic opportunities
Tok'ra medical-support deliveries
```

No new faction should be created unless an old save still lacks the persistent
Tok'ra anchor.

## Trust gating

The cache can trigger at:

```text
Neutral
Cooperative
Trusted
```

It refuses to trigger at:

```text
Wary
```

The first pass does not modify trust. It is support from an already known
clandestine cell, not a full diplomatic event.

## Supplies

```text
Neutral:      1 tretonin dose, 2 industrial medicine
Cooperative: 2 tretonin doses, 2 industrial medicine
Trusted:     2 tretonin doses, 3 industrial medicine
```

The supplies are intentionally modest. Richer support remains handled by the
trust-gated medical-support delivery.

## Non-goals

The event does not add:

```text
Tok'ra settlements
world sites
traders
recruitment
military aid
quest chains
combat
large weapon rewards
queen rewards
Prim'ta larvae
```

A true hidden world site or safehouse can be added later as a separate
milestone after this cache baseline has proven stable.

## Manual test checklist

1. Start from a save with neutral or better Tok'ra trust.
2. Force `SG1_TokraHiddenCellCache`.
3. Confirm a cache appears near a reachable map edge.
4. Confirm the contents match the current trust tier.
5. Confirm no visitor group, caravan, settlement or raid appears.
6. Save and reload after cache placement.
7. Confirm the cache remains normal spawned items.
8. Lower trust to wary if using dev tools and confirm the incident refuses.
9. Confirm `Player.log` has no red errors or duplicate Tok'ra faction creation.
