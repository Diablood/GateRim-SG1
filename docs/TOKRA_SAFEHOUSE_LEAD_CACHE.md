# Tok'ra safehouse lead cache baseline

Version: `0.2.15-dev`

## Purpose

This milestone makes stored Tok'ra safehouse leads useful before the first real
hidden world-site prototype.

A follow-up incident consumes one stored lead and resolves it as a modest
medical cache on the active colony map.

## Incident

```text
SG1_TokraSafehouseLeadCache
```

Worker:

```text
GateRimSG1.Goauld.IncidentWorker_TokraSafehouseLeadCache
```

Parameters:

```text
baseChance: 0.006
earliestDay: 60
minRefireDays: 45
target: Map_PlayerHome
letterDef: PositiveEvent
```

## Requirements

The incident requires:

```text
at least 1 stored Tok'ra safehouse lead
Tok'ra trust tier not wary
persistent hidden SG1_Tokra faction available
reachable unfogged map-edge cell
```

## Result

On success:

```text
-1 Tok'ra safehouse lead
2 tretonin doses
3 industrial medicine
```

## Non-goals

The incident still does not create:

```text
world site
generated map
settlement
pawn
caravan
visitor group
trader
recruitment
military aid
raid
```

This keeps the system safe while proving that leads can be stored, persisted
and consumed before the later site milestone.

## Manual test checklist

1. Store one lead through `SG1_TokraSafehouseSignal`.
2. Force `SG1_TokraSafehouseLeadCache`.
3. Confirm one lead is consumed.
4. Confirm `2` tretonin doses and `3` industrial medicine appear.
5. Confirm no world site, pawn, caravan or raid appears.
6. Save and reload.
7. Confirm the consumed lead remains consumed.
8. Try forcing the cache at `0` leads and confirm it refuses.
