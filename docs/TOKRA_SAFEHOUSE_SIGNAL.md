# Tok'ra safehouse signal baseline

Version: `0.2.13-dev`

## Purpose

This milestone adds a safe intermediate step before true hidden Tok'ra world
sites.

A clandestine Tok'ra cell transmits an encrypted safehouse signal. The
coordinates are intentionally incomplete and cannot be visited yet, but
acknowledging the contact confirms a functioning clandestine channel and stores a persistent safehouse lead for future follow-up.

## Incident

```text
SG1_TokraSafehouseSignal
```

Worker:

```text
GateRimSG1.Goauld.IncidentWorker_TokraSafehouseSignal
```

Parameters:

```text
baseChance: 0.008
earliestDay: 45
minRefireDays: 60
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
Tok'ra hidden-cell caches
```

## Trust gating

The signal can trigger at:

```text
Neutral
Cooperative
Trusted
```

It refuses to trigger at:

```text
Wary
```

## Trust effect

Acknowledging the signal applies a tiny trust gain and stores one safehouse lead:

```text
+1 Tok'ra trust
+1 Tok'ra safehouse lead
```

This is deliberately much smaller than accepting a therapeutic hosting offer.

## Non-goals

The event does not create:

```text
world site
settlement
caravan
visitor group
trader
recruitment
loot
military aid
raid
```

True hidden safehouses remain a later milestone.

## Manual test checklist

1. Start from a save with neutral or better Tok'ra trust.
2. Force `SG1_TokraSafehouseSignal`.
3. Confirm a positive letter appears.
4. Confirm Tok'ra trust increases by `+1`.
5. Confirm no map object, site, caravan, trader, visitor group or raid appears.
6. Save and reload.
7. Confirm the trust score persists.
8. Lower trust to wary if using dev tools and confirm the incident refuses.
9. Confirm `Player.log` has no red errors or duplicate Tok'ra faction creation.
