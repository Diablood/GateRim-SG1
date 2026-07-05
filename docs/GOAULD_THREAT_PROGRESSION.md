# Goa'uld threat progression audit

Version: `0.3.53-dev`; open-conflict natural-raid factor added in `0.3.68-dev`.

## Goal

Existing Goa'uld combat content must follow RimWorld's normal threat progression
before any new doctrine becomes natural. Colony wealth, pawn strength and the
active storyteller difficulty remain owned by RimWorld. GateRim SG-1 consumes
the resulting threat points and applies only documented encounter factors.

Ordinary systems remain compatible with vanilla and modded storytellers. The
`0.3.68-dev` open-conflict factor is an explicit SG-1 Command strategic
consequence and is inactive under every other storyteller.

## Audit result

| System | Current behavior |
| --- | --- |
| Natural Jaffa raid | Preserves supplied vanilla points, resolves missing points from the current storyteller and selects doctrine from the original value. Under SG-1 Command only, a domain in open conflict then transmits `75%` of those points to force generation. |
| Controlled raid doctrines | Use current or explicit test points and never receive the relation factor. |
| Intercepted Tok'ra warning | Stores the vanilla storyteller snapshot and uses the same value when the raid arrives. |
| Extraction reprisal | Uses its stored point snapshot through a forced incident path and never receives the relation factor. |
| Free-symbiote incursion | Maps vanilla points to one through four symbiotes with its intentional biological cap. |
| Combat mission sites | Use vanilla point snapshots with their documented encounter factors and minimums. |
| Relay sabotage site | Stores points before travel, keeps scaling defenders and reinforcements, and selects its layout by threat tier. |
| Goa'uld settlements | Continue to use vanilla settlement generation budgets. |

The natural-raid modifier changes neither incident frequency nor doctrine
eligibility. Direct, abduction and destruction weights are calculated before
any open-conflict reduction is applied.

All Goa'uld/Jaffa raids routed through the shared worker explicitly use vanilla
`EdgeWalkIn`. High point budgets must never unlock drop-pod arrival modes.

## Open-conflict factor

For the ordinary non-forced natural incident:

```text
effective points = max(1, vanilla points × factor)
```

The factor is `0.75` only when:

- SG-1 Command is active;
- the selected attacking faction is a Goa'uld System Lord domain;
- that domain participates in at least one active open-conflict pair.

Every other case uses `1.00`. Several simultaneous conflicts do not stack. The
factor is derived from the persistent relation states and adds no serialized
field.

Forced callers remain outside the modifier by default. This includes extraction
reprisals, controlled raids, deterministic doctrine regressions and mission
attacks. The dedicated pressure-test command temporarily enables the factor for
one forced execution so the real worker can be validated without waiting for a
natural storyteller roll.

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

Open the per-domain factor report and the dedicated reduced-raid test at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...
```

`Show natural raid pressure report` lists every active domain with its open-
conflict status and current factor. `Force current natural raid (pressure
applied)` executes the real worker while temporarily enabling the factor for
that forced validation only.

The exact forced natural direct `300`, abduction `800` and destruction `1800`
actions remain unchanged regression tools.

## Final validation

Final local revision `r3` passed the focused coverage in
[`TESTING_CURRENT.md`](TESTING_CURRENT.md): both open-conflict domains at `75%`,
non-stacking, `100%` under Cassandra and after leaving conflict, doctrine
eligibility from original points, one real reduced natural-worker execution,
exact forced-doctrine points, unreduced extraction reprisal points, save/reload
and a clean `Player.log`.

The older threat-scaling, edge-arrival, relay-layout and settlement regressions
remain valid and unchanged.
