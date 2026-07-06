# Shared Goa'uld alliance reprisals

Version: `0.3.81-dev`

## Purpose

This subsystem gives one exact allied Goa'uld pair a bounded, readable response
to a decisive player victory. It reuses the existing relation, natural-raid,
joint-raid and domain-reaction foundations instead of creating a parallel quest
or grievance framework.

## Trigger contract

Only a successful ordinary **standard** natural Goa'uld raid under
`Commandement SG-1` can be observed. Delayed reinforcement and simultaneous
joint outcomes are excluded, as are extraction reprisals, missions, controlled
raids and historical developer-forced paths.

The observation requires at least `5` initial Jaffa. It resolves once when
`25%` or fewer remain active on the target home map. At that moment:

- the exact domain pair must still be allied;
- no shared reprisal may already be pending globally;
- the pair must not be inside its `30`-day cooldown;
- a `25%` chance decides whether the response is scheduled.

The observation is then consumed whether the chance succeeds or fails.

## Scheduled response

A normal response waits `120000–240000` ticks, or `2–4` days. Its complete
combined budget is `80%` of the vanilla threat points current when it is
scheduled. That budget is split `60/40` between the offended domain and the
stored allied domain.

The response is a direct simultaneous joint assault from opposite reachable map
edges. It adds no storyteller incident roll, raid-frequency change, relation
transition, goodwill effect, reward or territorial consequence.

One shared reprisal may be pending globally. After resolution or cancellation,
the exact pair enters a `1800000`-tick (`30`-day) cooldown.

## Storyteller suspension

Observations and pending state persist in saves. While another storyteller is
active, no observation resolves and no pending reprisal arrives. On return to
`Commandement SG-1`, the due tick is shifted forward by the suspended duration,
preventing a backlog attack.

## Letter contract

The scheduling letter is purely informational. It has no `LookTargets`, because
no troop or world site exists yet; RimWorld must therefore not show a misleading
camera-jump action.

The arrival letter is emitted only after both detachments spawn. It:

- names the offended domain and allied domain;
- states explicitly that two Jaffa detachments approach from opposite sides;
- stores one representative pawn from each force in a single `LookTargets` list;
- replaces the generic joint-raid letter for this path, avoiding duplicate
  announcements.

## Persistence

`GameComponent_GoauldDomainReprisalTracker` stores:

- `GoauldSharedAllianceRaidObservation` entries with map, exact pair, pawn
  references and initial count;
- `GoauldSharedAllianceReprisalState` entries with exact pair, map, due tick,
  point snapshot, pending flag and pair cooldown;
- the SG-1 storyteller suspension boundary.

The pre-existing extraction ultimatum and reprisal fields remain unchanged.

## Developer path

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
```

Relevant actions:

- `Show domain reaction state`;
- `Create shared alliance reprisal`;
- `Trigger pending shared reprisal now`;
- `Reset shared alliance reprisals`.


## Final validation

Final revision `r2` is validated. The scheduling letter is purely informative
and has no camera target. The arrival letter explicitly announces two
opposite-side detachments and carries one representative pawn from each force.
All previously accepted timing, chance, budget, pair, persistence and combat
checks remain conforming, and no new relevant error appears in the accepted
`Player.log`.

The exact developer action label is
`Trigger pending shared reprisal now`.
