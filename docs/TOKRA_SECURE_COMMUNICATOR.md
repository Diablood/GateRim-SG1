# Tok'ra secure communicator

Version: `0.2.27-dev`

The Tok'ra secure communicator is the trusted-channel interaction point for
advanced Tok'ra support. It is still deliberately limited: the Tok'ra remain
clandestine and do not become a normal allied faction.

## Current functions

```text
buildable powered communicator
requires Microelectronics
usable only at trusted Tok'ra confidence
opens a vanilla contact dialog
can request a rare defensive diversion during an active attack
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
no medical request yet
no safehouse-lead request yet
no quest start yet
```

## Implementation notes

The building uses `Comp_TokraSecureCommunicator`. The component checks player
control, power, trusted Tok'ra tier, active hostile pawns and its stored
defensive-diversion cooldown.

This keeps the current Tok'ra design intact: support is useful in a crisis, but
rare, defensive and non-repeatable in the short term.


## Future direction

The current prototype exposes building gizmos directly. A later milestone should
move communicator requests toward a selected-pawn interaction so the device feels
like a console operated by a colonist rather than an instant remote button.
