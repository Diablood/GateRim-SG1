# Tok'ra secure communicator

Version: `0.2.31-dev`

The Tok'ra secure communicator is the trusted-channel interaction point for
advanced Tok'ra support. It is still deliberately limited: the Tok'ra remain
clandestine and do not become a normal allied faction.

## Current functions

```text
buildable powered communicator
requires Microelectronics
operated by a selected player colonist
right-click interaction on the communicator
usable only at trusted Tok'ra confidence
opens a vanilla contact dialog
can request a rare defensive diversion during an active attack
can request limited medical guidance when a colonist is wounded or sick
can request an informational tactical threat assessment during an active hostile threat
```

## Medical guidance

The trusted medical request is advisory only. A selected colonist operates the
communicator while at least one wounded or sick human colonist is on the map.
The Tok'ra transmit field guidance, the operator gains Medicine experience and
the medical channel enters a short cooldown.

```text
trusted tier only
patient on current map required
600 Medicine XP to the operator
3-day medical cooldown
no direct treatment or item delivery
```

## Defensive diversion

The first active request is defensive only. It requires hostile pawns on the
current map and a ready communicator cooldown. When accepted, a nearby Tok'ra
cell briefly disrupts up to three hostile pawns and then cuts the channel.

```text
trusted tier only
active hostile threat required
up to 3 enemies briefly disrupted
5-day cooldown
no physical Tok'ra squad
```

## Non-goals

```text
no trade
no recruitment
no permanent military help
no offensive strike
no direct item reward
medical request is guidance-only for now
no safehouse-lead request yet
no quest start yet
```

## Implementation notes

The building uses `Comp_TokraSecureCommunicator`. The component checks player
control, power, trusted Tok'ra tier, active hostile pawns and its stored
defensive-diversion cooldown.

This keeps the current Tok'ra design intact: support is useful in a crisis, but
rare, defensive and non-repeatable in the short term.


## 0.2.28-dev note

Communicator requests are now pawn-operated. The player selects a colonist,
right-clicks the communicator and chooses either the trusted contact channel or
the defensive-diversion request. Building gizmos remain as status/instruction
entries, but active requests are no longer instant building actions.


## 0.2.29-dev note

The communicator now supports a trusted medical-guidance request. It is operated
through the selected-colonist right-click workflow, requires a local wounded or
sick colonist, grants Medicine XP to the operator and does not heal directly.


## 0.2.30-dev emergency medical cache

The trusted communicator can now request a small emergency medical cache when a
wounded or sick human colonist is present. The cache places limited medical
supplies near the communicator and starts its own seven-day cooldown. It does not
heal pawns directly and does not open trade, recruitment, quests or military aid.

## Tactical threat assessment

Since `0.2.31-dev`, the trusted communicator can request a quick Tok'ra tactical assessment during an active hostile threat. The request is informational only: it summarizes hostile count, broad composition and threat severity without damaging enemies, revealing the map or calling reinforcements.

```text
trusted tier only
active hostile threat required
one-day tactical cooldown
no combat effect
no map reveal
```
