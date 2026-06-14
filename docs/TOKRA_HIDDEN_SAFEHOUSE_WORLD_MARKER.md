# Hidden Tok'ra safehouse world marker

Version: `0.2.16-dev`

Validation: manual RimWorld checklist passed on June 14, 2026.

## Purpose

This milestone is the first world-map footprint for stored Tok'ra safehouse
leads.

It deliberately stops before generated maps or enterable sites. A stored lead
can become a temporary non-hostile world marker, proving the flow:

```text
safehouse signal
-> stored lead
-> consumed lead
-> visible world marker
```

## Incident

```text
SG1_TokraHiddenSafehouseWorldMarker
```

Worker:

```text
GateRimSG1.Goauld.IncidentWorker_TokraHiddenSafehouseWorldMarker
```

## World object

```text
SG1_TokraHiddenSafehouseMarker
GateRimSG1.Goauld.WorldObject_TokraHiddenSafehouseMarker
```

Duration:

```text
300000 ticks
5 RimWorld days
```

## Requirements

The incident requires:

```text
at least 1 stored Tok'ra safehouse lead
Tok'ra trust tier not wary
no active Tok'ra safehouse marker already present
valid nearby world tile
persistent hidden SG1_Tokra faction available
```

## Result

On success:

```text
-1 Tok'ra safehouse lead
1 temporary non-hostile world marker
```

## Non-goals

The marker does not create:

```text
generated map
enterable site
loot
trader
recruitment
visitors
military aid
raid
permanent settlement
```

## Manual test checklist

1. Store one lead through `SG1_TokraSafehouseSignal`.
2. Force `SG1_TokraHiddenSafehouseWorldMarker`.
3. Confirm one lead is consumed.
4. Confirm a world marker appears near the colony.
5. Select the marker and confirm the inspect string shows it is incomplete.
6. Save and reload with the marker active.
7. Confirm the marker remains.
8. Let it expire or advance time.
9. Confirm it disappears safely.
10. Try forcing the incident while a marker exists and confirm it refuses.
