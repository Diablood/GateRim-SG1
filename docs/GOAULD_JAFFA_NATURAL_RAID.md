# Natural Goa'uld Jaffa raid doctrines

Version: `0.2.1-dev`; doctrine selection extended in `0.3.54-dev`;
open-conflict pressure reduction added in `0.3.68-dev`; bounded alliance
strength added in `0.3.73-dev`.

## Purpose

`SG1_GoauldJaffaNaturalRaid` remains the only natural Goa'uld raid incident.
It can assign the validated direct, abduction or destruction doctrine without
creating three independent storyteller rolls.

## Storyteller contract

```text
baseChance: 0.08
earliestDay: 12
minRefireDays: 18
category: ThreatBig
target: Map_PlayerHome
```

The storyteller supplies vanilla threat points. Colony wealth, pawn strength
and storyteller settings therefore remain authoritative. The common worker
forces the real Goa'uld faction and `EdgeWalkIn`; no doctrine may introduce
transport pods.

The incident chance, earliest day and shared refire delay are not modified by
inter-domain relations.

## Doctrine selection

| Doctrine | Eligibility | Fallback weight when eligible | Result |
| --- | --- | ---: | --- |
| Direct | always | `2` | `ImmediateAttack`, no stealing or kidnapping |
| Abduction | `800+` points and at least 2 free colonists | `1` | capture window, then extraction with or without victims |
| Destruction | `1800+` points and `10 000+` building wealth | `1` | sustained damage phase, recovery window, then extraction |

Persistent domain profiles may replace the fallback `2/1/1` weights with
conquest `4/1/1`, enslavement `2/3/1` or scorched earth `2/1/3`.

Eligibility and relative doctrine selection always use the original vanilla
storyteller points. The relation-derived pressure factor is applied only after
the doctrine has been selected.

The two custom `RaidStrategyDef` selection curves remain zero. Generic vanilla
raid strategy resolution can never select them; only this dedicated worker and
developer regression incidents assign them explicitly.

## Relation-derived pressure effects

Under `Commandement SG-1`, the ordinary natural raid resolves one final factor
for the attacking Goa'uld domain:

| Active relation affecting the domain | Factor |
| --- | ---: |
| at least one open conflict | `0.75` |
| no open conflict and at least one alliance | `1.10` |
| neutrality, rivalry or truce only | `1.00` |
| any relation under another storyteller | `1.00` |

```text
effective natural raid points = max(1, vanilla points × factor)
```

The effect is deliberately bounded:

- several open conflicts never reduce below `0.75`;
- several alliances never increase above `1.10`;
- open conflict overrides alliance when both affect the same domain;
- both domains in a pair are evaluated independently;
- the factor changes raid points only, never incident frequency, doctrine
  weights or contextual eligibility.

No additional state is serialized. The factor is derived from the persistent
relation tracker whenever the ordinary natural raid executes.

## Natural versus forced execution

The shared controlled-raid worker internally sets `parms.forced = true` before
raid generation. That technical flag cannot by itself distinguish a storyteller
incident from an externally forced test or reprisal.

`0.3.73-dev` records whether the caller was already forced before entering the
shared worker:

- ordinary storyteller execution receives the resolved `0.75`, `1.00` or
  `1.10` factor;
- externally forced execution bypasses the factor by default;
- the dedicated relation-pressure command opts one forced execution back into
  the real modifier for deterministic validation.

## Explicit exclusions

The factor is not applied to:

- extraction ultimatums and their delayed reprisals;
- controlled direct, abduction or destruction incidents;
- the three forced natural-doctrine regression actions;
- intercepted threats;
- Tok'ra missions, hostile sites or settlement defense;
- free-symbiote incursions.

## Debug access

Open the threat report at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

Open the relation-derived report and forced modifier test at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...
```

`Show current progression` reports:

- vanilla storyteller points;
- the diagnostic domain;
- its resolved relation factor;
- effective natural raid points;
- unchanged intercepted-raid points;
- free-colonist count, building wealth and normalized doctrine weights.

`Show natural raid relation-pressure report` lists open-conflict state,
alliance state and the effective factor for every active domain.

`Force current natural raid (relation pressure applied)` uses the same worker
while explicitly enabling the factor for this forced test only.

`Set all pairs: Alliance` supports deterministic non-stacking and precedence
coverage with three or more domains. The three `Force natural ... raid`
commands remain exact regression tools and bypass the relation effect so direct
`300`, abduction `800` and destruction `1800` tests retain their historical
contracts.

## Published validation

Final local revision `r1` is validated and published in `0.3.73-dev`. The
focused procedure confirms alliance `110%`, open-conflict `75%`,
other-storyteller `100%`, non-stacking, open-conflict precedence, natural versus
forced execution, save/reload and a clean accepted `Player.log`.
