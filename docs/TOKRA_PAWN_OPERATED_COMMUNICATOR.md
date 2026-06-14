# Tok'ra pawn-operated communicator

Version: `0.2.28-dev`

This milestone moves trusted Tok'ra communicator requests from direct building
gizmos to selected-pawn interactions.

## Player-facing behavior

```text
select a player colonist
right-click the Tok'ra secure communicator
choose the trusted contact channel or defensive diversion request
colon walks to the communicator
short operation delay
request resolves at the end
```

## Preserved restrictions

```text
trusted Tok'ra tier required
powered communicator required
active hostile threat required for diversion
five-day diversion cooldown preserved
no trade, recruitment, quest or direct item reward
```

Direct building gizmos are hidden during normal play. They remain available only
when RimWorld developer mode or the GateRim SG-1 advanced debug information option
is active, as diagnostics/instruction entries. Active player use remains the
selected-colonist right-click workflow.
