# Goa'uld open-conflict battlefields

Version: `0.3.69-dev`

Branch: `feature/goauld-open-conflict-battlefield-incident`

Local revision: `r6`

Status: compile correction applied after the bounded-retaliation implementation; full retest required.

## Purpose

A persistent `open conflict` relation can now produce a rare battle on a
player-home map while **SG-1 Command** is active. The event represents two
exact Goa'uld domains diverting forces against one another instead of adding
another disguised raid against the colony.

## Eligibility and cadence

`GameComponent_GoauldOpenConflictBattlefieldTracker` requires:

- SG-1 Command as the active storyteller;
- at least one active relation pair in `open conflict`;
- a player-home map with a free colonist;
- no other active hostile force on the selected map;
- no existing local battlefield.

Hidden timing is persistent:

- first opportunity: `8–16` days;
- retry while no open-conflict pair exists: `3–6` days;
- recurrence after a completed battlefield: `20–40` days;
- temporary map or generation failure retry: `1` day.

Opportunity timing is shifted forward while another storyteller is active,
preventing a backlog when SG-1 Command is selected again. An already active
battlefield continues normally after a storyteller switch.

## Pair selection

The scheduler selects an exact persistent relation pair. When several pairs
are eligible, the pair used by the previous battlefield is excluded from the
next draw. The last pair and last RP letter variant persist through
save/reload.

Only one local battlefield may exist at once. The future `0.3.70-dev`
world-map battlefield site must reuse this orchestration boundary so a local
battlefield and a world site cannot be active simultaneously for the same
strategic slot.

## Threat scaling

Each detachment receives:

```text
clamp(vanilla storyteller points × 0.35, 250, 1800)
```

Pawn counts are derived from the Jaffa warrior combat power and bounded from
`2` to `10` pawns per side. Every fifth pawn uses the existing Jaffa guard
kind when available. Both groups belong to the exact domain factions stored
in the selected relation pair.

## Local battle behavior

`GoauldOpenConflictBattlefieldUtility` now separates entry cells from rally
anchors. Both detachments spawn within four cells of the selected map edge,
then jog toward opposing rally positions roughly `48` cells apart.

`MapComponent_GoauldOpenConflictBattlefield` owns three explicit phases:

1. **rally**: each group moves from its edge entry toward its assigned anchor;
2. **assault**: after at least `70%` of both active groups are within seven cells
   of their anchors and have held for `1800` ticks, one player message announces
   the attack and mobile combat control begins;
3. **withdrawal**: mobile survivors leave after one side is broken or the
   bounded two-day duration expires.

A `12000`-tick rally timeout prevents difficult terrain from blocking the event
forever. If it expires, the assault begins with the pawns that successfully
reached the area.

Ranged combat no longer assigns `AttackStatic` while a target is outside weapon
range. Each Jaffa instead advances toward a reachable firing cell, reevaluates
the moving target every `30` ticks and switches to `AttackStatic` only after
range and line of sight are available. Melee pawns retain normal pursuit through
`AttackMelee`.

The component records exact player colonists whose attack job or warmup stance
targets either detachment. After one side has fallen, a new injury in the
surviving camp also infers the nearest active player pawn as a fallback
provoker. That camp prioritizes persisted provocateurs while the other camp
continues fighting its rival.

Retaliation is deliberately bounded. A provoked camp returns to the
inter-domain fight after `1800` ticks without a renewed player attack. Its
pursuit remains within `35` cells of the position where the provocation began;
ongoing fire from beyond that boundary does not drag the detachment across the
map. During withdrawal, a camp may interrupt its exit to defend itself for at
most `6000` ticks in total, then resumes leaving even if the player remains
present. These local combat reactions do not rewrite faction goodwill,
strategic relations or player diplomacy.

The player may:

- remain outside the battlefield;
- attack one camp before, during or after the inter-domain fight;
- attack both camps;
- capture downed survivors;
- recover ordinary dropped equipment after the fight.

No artificial material or goodwill reward is created.

## Withdrawal and cleanup

Withdrawal begins when:

- either side has no mobile combatant;
- exactly one side falls to `30%` or less of its initial mobile force while the
  opponent remains above its own `30%` threshold; or
- the battle reaches `120000` ticks, equal to two RimWorld days.

If both camps are simultaneously at or below the morale threshold, neither is
selected as the sole breaking side and combat continues until elimination or
the absolute deadline. Small detachments of two or three pawns cannot break
before elimination because their exact `30%` threshold rounds down to zero.

Mobile survivors receive a vanilla `LordJob_ExitMapBest`. Revision `r6`
reissues that withdrawal order every `600` ticks only for mobile survivors
whose exit Lord or path has stalled. A withdrawing camp can interrupt its exit
to retaliate, but only inside the persisted `6000`-tick withdrawal-retaliation
window. The original `30000`-tick forced-exit deadline is no longer extended by
player intervention. Remaining mobile non-prisoners are then forced off the
map. Downed pawns, prisoners, corpses and dropped equipment remain available
to normal RimWorld systems.

The global tracker is notified only after the active battlefield resolves,
then schedules the next `20–40` day recurrence window.

## RP communication

Exactly one threat-small letter names both domains and targets a spawned
Jaffa pawn on the colony map, so its jump action opens the local battlefield
rather than the settlement marker on the world map. It explains that both columns have entered from the map edge and are moving to
opposing rally points. A separate localized message announces when the two
forces finish assembling and launch the assault. Intervention remains optional,
and either camp will retaliate if challenged.

Three English and French variants use immediate anti-repetition.

## Developer access

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

Added actions:

- `Show battlefield report`;
- `Make battlefield opportunity due`;
- `Force battlefield now`;
- `Order battlefield withdrawal`;
- `Reset battlefield scheduler`.

The forced battlefield still requires an actual active open-conflict pair,
but may ignore normal map-threat gating for deterministic validation.

## Explicit exclusions

`0.3.69-dev` adds no:

- world-map site or caravan destination;
- settlement destruction or territorial expansion;
- alliance effect, joint raid or reinforcement;
- relation or goodwill change caused by the battle;
- mission objective, success state or failure penalty;
- artificial loot container or reward.

The world-map equivalent is reserved for `0.3.70-dev`.


## Revision history

- `r1`: first local implementation; validation found that the letter jumped to
  the world map, both groups remained idle and player attacks did not trigger
  retaliation.
- `r2`: target the letter at a spawned map pawn, force real AI attack jobs,
  persist exact player provocateurs per camp and refresh stalled withdrawals.
- `r3`: spawn both forces at the map edge, add rally and announced assault
  phases, and preserve retaliation after a victorious camp has begun withdrawing.
- `r4`: replace distant immobile ranged attack jobs with explicit approach-to-
  range behavior, read drafted warmup targets and infer post-victory provocation
  from new injuries when the player attack job is not exposed reliably.
- `r5`: bound player retaliation to `1800` quiet ticks and a `35`-cell pursuit
  radius, cap withdrawal retaliation at `6000` ticks without extending forced
  exit, and add morale withdrawal when exactly one camp falls to `30%` or less
  of its initial mobile force.
- `r6`: fix the C# build by copying the `out` provocation origin into a local
  value before the `RemoveAll` lambda; gameplay behavior is unchanged from
  `r5`.
