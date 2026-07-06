# Shared Goa'uld alliance reprisals

Version: `0.3.82-dev`

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

## Alliance rupture after a major failure

`0.3.82-dev` adds one diplomatic consequence after the **natural** shared
reprisal itself is decisively defeated. Developer-created or developer-triggered
shared reprisals are excluded from this automatic chain.

The two spawned detachments are observed only when they contain at least `6`
combined Jaffa. The observation resolves once when `20%` or fewer remain active.
That major failure deterministically schedules one exact-pair rupture after
`60000–120000` ticks (`1–2` days); no additional random roll is used.

At the deadline, the relation changes from `Alliance` to `Rivalry`. The published
relation tracker performs the transition, updates its previous relation and
normal transition clocks, but suppresses the generic relation letter so the
player receives only the dedicated failure report.

One rupture may be pending globally. It is cancelled without a player letter if
the exact pair ceases to be allied or either domain becomes inactive before the
deadline. The result is stored as `Completed`,
`CancelledNoLongerAllied`, `CancelledInactiveDomain` or
`CancelledTransitionFailed` for diagnostics.

The deadline shares the existing SG-1 Command suspension boundary. Switching to
another storyteller pauses it and shifts the due tick forward on return.

The resolution letter has three localized RP variants with local
anti-repetition. Every variant names both domains and states clearly that the
alliance is dissolved and replaced by rivalry. The letter has no `LookTargets`,
because the consequence is diplomatic and no new force or site is created.

The rupture changes no vanilla goodwill toward the player, no settlement, no
territory, no raid budget, no doctrine profile and no storyteller frequency.

Developer validation uses the exact actions:

- `Create major alliance failure`;
- `Trigger pending alliance rupture now`;
- `Reset alliance rupture state`;
- `Show domain reaction state`.

Final revision `r1` is validated and published. The focused procedure confirms
assembly `0.3.82.0`, one persistent exact-pair rupture, the expected `1/6` debug
survivor state, save/reload continuity, one targetless letter naming both
domains, the exact `Alliance -> Rivalry` transition, final outcome `Completed`,
no gameplay side effect outside the relation change and no new relevant error in
the accepted `Player.log`.

