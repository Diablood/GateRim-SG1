# Tok'ra secure communicator

Current operation gate: `0.3.33-dev-r2`

The Tok'ra secure communicator is the trusted-channel interaction point for
advanced Tok'ra support. It is still deliberately limited: the Tok'ra remain
clandestine and do not become a normal allied faction.

## Current functions

```text
buildable powered communicator
requires `SG1_TokraSecureCommunications` for new construction
operated by a selected player colonist
right-click interaction on the communicator
usable only at trusted Tok'ra confidence
opens a vanilla contact dialog
can request a rare defensive diversion during an active attack
can request limited medical guidance when a colonist is wounded or sick
can request an informational tactical threat assessment during an active hostile threat
can request preparation of a discreet future Tok'ra mission
can receive a Tok'ra-initiated briefing contact after that preparation
can show a non-effect status report for channel availability and cooldowns
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

Since `0.3.33-dev-r1`, new construction requires the dedicated
`SG1_TokraSecureCommunications` research. Existing buildings remain usable in
older saves. `TokraSecureCommunicatorAvailabilityUtility` provides the shared
physical-channel test used by later orchestration: player home map, player
ownership, valid GateRim communicator component and active power. Trust remains
separate because different operations can use different diplomatic thresholds.

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


## 0.2.32-dev status report

The communicator can now show a non-effect status report through the selected
colonist right-click workflow. It lists power, trust, local context and the
current availability or cooldown state of every implemented Tok'ra request.
The consultation does not consume any cooldown and does not trigger support.


## 0.2.36-dev initiated mission briefing

The operational debrief request is temporarily hidden from player-facing
communicator options until it has a clearer trust or narrative role.

When a trusted colony has prepared a discreet Tok'ra mission, the next step is
no longer another immediate communicator command. A Tok'ra cell can recontact the
colony automatically after a short delay and transmit the initial briefing.

```text
prepared mission state required
Tok'ra-initiated contact after delay
persistent briefing-received state
visible in the channel status report
no site, raid or immediate reward
```


## 0.3.33-dev-r2 recurrent-operation gate

The shared service now owns eligibility for every new recurrent Tok'ra offer. If no powered player communicator exists on any player home map, the organic-operation manager records a persistent blocked state and does not create or consume an offer. The introduction-artifact mission remains separate.

An operation already in the single active slot is never cleared merely because the channel loses power or the building is destroyed. Its own offer or mission deadlines continue to follow the rules that existed before this gate.

When channel availability returns, an overdue planner check is not executed immediately. The manager schedules a fresh hidden recurrence delay using the current trust tier and the last offered archetype. This preserves long-game pacing and prevents predictable instant contact after reconnecting power.

Focused developer checks start from:

```text
Debug actions menu
-> GateRim SG-1
-> Tok'ra...
```

Then use:

- `Show communicator availability`;
- `Organic operations... → Framework... → Make natural offer due`;
- `Organic operations... → Framework... → Show state`;
- `Organic operations... → Framework... → Roll next natural offer`.

Expected sequence:

- no communicator: the natural timer becomes due, no offer appears and the framework report shows the gate blocked;
- save/reload: the blocked state remains;
- powered communicator restored: the gate clears and a new future opportunity tick is scheduled;
- forced natural roll after restoration: exactly one normal weighted offer is created;
- power loss with an existing offer or active operation: the same slot remains intact.
