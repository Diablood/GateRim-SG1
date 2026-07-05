# Natural Goa'uld Jaffa raid doctrines

Version: `0.2.1-dev`; doctrine selection extended in `0.3.54-dev`;
open-conflict pressure reduction added in `0.3.68-dev`.

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
storyteller points. The open-conflict pressure factor is applied only after the
doctrine has been selected.

The two custom `RaidStrategyDef` selection curves remain zero. Generic vanilla
raid strategy resolution can never select them; only this dedicated worker and
developer regression incidents assign them explicitly.

## Open-conflict pressure effect

Under `Commandement SG-1`, an active Goa'uld domain that participates in at
least one `open conflict` relation uses:

```text
effective natural raid points = max(1, vanilla points × 0.75)
```

The reduction is deliberately bounded:

- one domain receives the same `0.75` factor whether it has one or several open
  conflicts;
- both active domains in the pair are evaluated independently;
- truce, rivalry, alliance and neutrality use `1.00`;
- Cassandra, Phoebe, Randy and compatible modded storytellers use `1.00`;
- the factor changes raid points only, never incident frequency or doctrine
  weights.

No additional state is serialized. The factor is derived from the persistent
relation tracker whenever the ordinary natural raid executes.

## Explicit exclusions

The factor is not applied to:

- extraction ultimatums and their delayed reprisals;
- controlled direct, abduction or destruction incidents;
- the three forced natural-doctrine regression actions;
- intercepted threats;
- Tok'ra missions, hostile sites or settlement defense;
- free-symbiote incursions.

The worker applies the factor only to the ordinary non-forced incident path.
Every forced caller therefore remains excluded by default. The dedicated test
command temporarily enables the factor while retaining forced execution.

## Debug access

Open the threat report at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

Open the relation-derived factor report and reduced-raid test at:

```text
Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...
```

`Show current progression` reports:

- vanilla storyteller points;
- the diagnostic domain;
- its pressure factor;
- effective natural raid points;
- unchanged intercepted-raid points;
- free-colonist count, building wealth and normalized doctrine weights.

`Show natural raid pressure report` lists the conflict state and effective factor
for every active domain. `Force current natural raid (pressure applied)` uses the
same worker while explicitly enabling the factor for this forced test only.

The three `Force natural ... raid` commands remain exact regression tools. They
bypass the pressure effect so direct `300`, abduction `800` and destruction
`1800` tests retain their historical contracts.

## Final validation

Final local revision `r3` passed the focused procedure in
[`TESTING_CURRENT.md`](TESTING_CURRENT.md): `75%` for both domains under
Commandement SG-1, `100%` under Cassandra and after leaving open conflict,
non-stacking, doctrine choice from original points, a real pressure-enabled
natural raid, exact forced-regression points, unreduced extraction reprisals,
save/reload and a clean `Player.log`.
