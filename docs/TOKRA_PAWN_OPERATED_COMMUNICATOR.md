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

Direct building gizmos are hidden during normal play. Read-only diagnostics may
appear through the GateRim SG-1 advanced-information option, but direct commands
that force or bypass gameplay require RimWorld developer mode. Active player use
remains the selected-colonist right-click workflow.

## 0.3.42-dev progressive menu discovery

Before the Tok'ra relationship reaches the trusted tier, pawn right-click menus no longer list future trusted requests as disabled entries. The status consultation remains visible, as does an interaction for an organic operation that is already proposed or active.

After the trusted tier is reached, the requests appear normally. Immediate contextual restrictions such as pawn capability, reachability, reservation, power, cooldown, missing threat or missing patient remain visible as disabled reasons.
