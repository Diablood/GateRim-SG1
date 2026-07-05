# Goa'uld threat progression audit

Version: `0.3.53-dev`; open-conflict natural-raid factor added in `0.3.68-dev`;
bounded alliance factor added in `0.3.73-dev`.

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
| Natural Jaffa raid | Preserves supplied vanilla points, resolves missing points from the current storyteller and selects doctrine from the original value. Under SG-1 Command only, open conflict then applies `0.75`; otherwise alliance may apply `1.10`. |
| Controlled raid doctrines | Use current or explicit test points and never receive the relation factor. |
| Intercepted Tok'ra warning | Stores the vanilla storyteller snapshot and uses the same value when the raid arrives. |
| Extraction reprisal | Uses its stored point snapshot through a forced incident path and never receives the relation factor. |
| Free-symbiote incursion | Maps vanilla points to one through four symbiotes with its intentional biological cap. |
| Combat mission sites | Use vanilla point snapshots with their documented encounter factors and minimums. |
| Relay sabotage site | Stores points before travel, keeps scaling defenders and reinforcements, and selects its layout by threat tier. |
| Goa'uld settlements | Continue to use vanilla settlement generation budgets. |

The natural-raid modifier changes neither incident frequency nor doctrine
eligibility. Direct, abduction and destruction weights are calculated before
any relation factor is applied.

All Goa'uld/Jaffa raids routed through the shared worker explicitly use vanilla
`EdgeWalkIn`. High point budgets must never unlock drop-pod arrival modes.

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
never averages both values. The factor is derived from persistent relation
states and adds no serialized field.

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
persistent profile, the relation-derived factor, effective natural-raid points,
unchanged intercepted points, doctrine weights, symbiote count, relay budgets
and expected relay layout.

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
