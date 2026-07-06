# Goa'uld threat progression audit

Version: `0.3.53-dev`; open-conflict natural-raid factor added in `0.3.68-dev`;
bounded alliance factor added in `0.3.73-dev`; delayed allied reinforcement
budget splitting added in `0.3.78-dev`; standard and joint alliance outcomes
added in `0.3.79-dev`; bounded relation-doctrine weighting added in
`0.3.80-dev`.

## Goal

Existing Goa'uld combat content must follow RimWorld's normal threat progression
before any new doctrine becomes natural. Colony wealth, pawn strength and the
active storyteller difficulty remain owned by RimWorld. GateRim SG-1 consumes
the resulting threat points and applies only documented encounter factors.

Ordinary systems remain compatible with vanilla and modded storytellers. The
relation-derived natural-raid factors are explicit SG-1 Command strategic
consequences and are inactive under every other storyteller.

## Audit result

| System | Current behavior |
| --- | --- |
| Natural Jaffa raid | Preserves supplied vanilla points and resolves missing points from the current storyteller. Under SG-1 Command, one non-stacking XML modifier can multiply an already eligible doctrine weight by `1.25` with priority open conflict, alliance, rivalry. Doctrine is selected from the original point context; open conflict then applies `0.75`, otherwise alliance may apply `1.10`. At `800+` final alliance points, half the outcomes remain standard; cooperative direct outcomes become delayed `75/25` reinforcement or simultaneous `60/40` joint assault. |
| Controlled raid doctrines | Use current or explicit test points and never receive the relation factor. |
| Intercepted Tok'ra warning | Stores the vanilla storyteller snapshot and uses the same value when the raid arrives. |
| Extraction reprisal | Uses its stored point snapshot through a forced incident path and never receives the relation factor. |
| Free-symbiote incursion | Maps vanilla points to one through four symbiotes with its intentional biological cap. |
| Combat mission sites | Use vanilla point snapshots with their documented encounter factors and minimums. |
| Relay sabotage site | Stores points before travel, keeps scaling defenders and reinforcements, and selects its layout by threat tier. |
| Goa'uld settlements | Continue to use vanilla settlement generation budgets. |

Neither relation layer changes incident frequency or doctrine eligibility.
Direct, abduction and destruction eligibility is calculated from original
vanilla points. One doctrine-weight multiplier is applied before selection; the
separate point-pressure factor is applied afterward.

All Goa'uld/Jaffa raids routed through the shared worker explicitly use vanilla
`EdgeWalkIn`. High point budgets must never unlock drop-pod arrival modes.

## Relation-derived doctrine influence

Only ordinary natural raids under `Commandement SG-1` use this layer. The
persistent domain profile first provides `4/1/1`, `2/3/1`, `2/1/3` or fallback
`2/1/1`. Existing thresholds then set ineligible abduction or destruction weights
to zero. Finally, one XML Def may multiply one surviving weight by `1.25`:

1. open conflict favors destruction;
2. otherwise alliance favors direct assault;
3. otherwise rivalry favors abduction;
4. neutrality and truce do nothing.

The effects never stack. The modifier cannot turn zero into a positive weight,
does not change points and does not guarantee an outcome. Other storytellers and
all externally forced historical doctrine commands bypass it.

## Relation-derived factor

For the ordinary natural incident:

```text
effective points = max(1, vanilla points × factor)
```

The factor is resolved only when SG-1 Command is active and the selected
attacking faction is a Goa'uld System Lord domain:

1. any active open conflict returns `0.75`;
2. otherwise, any active alliance returns `1.10`;
3. otherwise, return `1.00`.

Several simultaneous conflicts or alliances do not stack. Open conflict has
priority over alliance, so a mixed domain never multiplies `0.75 × 1.10` and
never averages both values.

For an eligible alliance raid at `800+` final points, half the outcomes keep the
entire modified total on a standard single-domain raid. The cooperative half is
resolved after doctrine selection:

- direct raids split evenly between delayed `75/25` reinforcement and
  simultaneous `60/40` joint assault;
- abduction and destruction use delayed reinforcement only;
- every split consumes the same final `1.10` budget rather than adding another
  multiplier.

The delayed form keeps its `1800` to `3600` tick hidden arrival. The joint form
uses opposite edges in one execution and one shared letter. Pending or active
cooperation is serialized; the underlying factor and random outcome remain
derived. Relation-change letters never schedule or guarantee any raid.

The shared raid worker internally marks every generated raid as forced. The
natural worker therefore captures whether the caller was already forced before
entering that shared path. This preserves the intended boundary:

- ordinary storyteller execution receives the relation factor;
- extraction reprisals, controlled raids, deterministic doctrine regressions
  and mission attacks remain outside it;
- the dedicated relation-pressure test explicitly opts one forced execution
  into the real modifier.

## Relay tiers

The relay uses the mission defender budget after the `0.80` encounter factor:

| Defender points | Layout |
| ---: | --- |
| below `600` | command bunker |
| `600` to `1399` | split relay station |
| `1400` and above | walled relay courtyard |

Reinforcements use `25%` of that defender budget with a one-pawn minimum. The
site serializes the original vanilla point snapshot so travel time, temporary
map wealth and save/reload do not silently change its intended difficulty.

## Developer access

Open the progression audit at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

`Show current progression` reports vanilla points, the diagnostic domain, its
persistent profile, the relation-derived point factor, effective natural-raid
points, final doctrine percentages, symbiote count, relay budgets and expected
relay layout.

`Domain doctrines... > Show domain doctrine report` is the authoritative
`0.3.80-dev` diagnostic: it shows profile weights, eligible weights before
relation, selected modifier Def and priority, multipliers and final percentages.

Open the per-domain factor report and dedicated forced modifier test at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...
```

`Show natural raid relation-pressure report` lists every active domain with its
open-conflict state, alliance state and current factor.

`Force current natural raid (relation pressure applied)` executes the real
worker while temporarily enabling the factor for that forced validation only.
`Set all pairs: Alliance` makes multiple-alliance and mixed-precedence tests
repeatable without save editing.

`Force allied natural raid (1200 points, short delay)` provides the focused
allied-wave test with a `600`-tick hidden delay. `Show allied reinforcement
report` was renamed `Show allied raid cooperation report`; it remains the only
place where pending or active internal state is shown.

`Force standard alliance raid (1200 points)` and `Force joint raid (1200
points, simultaneous)` provide deterministic coverage for the new branches.
The cooperation report and primary-withdrawal action expose only debug state.

The exact forced natural direct `300`, abduction `800` and destruction `1800`
actions remain unchanged regression tools.

## Published validation

Final local revision `r1` is validated and published in `0.3.73-dev`. Focused
coverage confirms alliance-only domains at `110%`, open-conflict and mixed
domains at `75%`, non-stacking, other-storyteller `100%`, doctrine eligibility
from original points, ordinary natural execution, exact forced-doctrine points,
save/reload and a clean accepted `Player.log`.

The older threat-scaling, edge-arrival, relay-layout and settlement regressions
remain valid and unchanged.

Revision `r1` for `0.3.78-dev` is validated and published for the focused
allied-wave path and a clean accepted `Player.log`.

Revision `r1` for `0.3.79-dev` is validated and published for standard and simultaneous joint
outcomes, opposite-edge colors, one shared letter, mutual cooperation, bilateral
withdrawal and a clean accepted `Player.log`. The forced `1200`-point joint test
showed no officer as expected: the forced path does not enable the officer
fallback and the `792`-point primary split normally generates no `145`-point
guard to replace.

Revision `r1` for `0.3.80-dev` is validated and published. The build and
focused procedure confirm the `x1.25` XML modifiers, non-stacking priority,
threshold preservation, other-storyteller exclusion, unchanged `75% / 110%`
pressure factors, unchanged alliance budget splits, deterministic forced-command
regressions and a clean accepted `Player.log`.

## Shared alliance reprisal budget (`0.3.81-dev`)

The shared alliance reprisal snapshots `0.80 ×` the vanilla threat points
current when the response is scheduled. That value is the complete combined
budget and is split `60/40`. The ordinary alliance `1.10` natural-raid factor is
not applied a second time because the response is an externally forced,
cause-driven path.
