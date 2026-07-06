# Natural Goa'uld Jaffa raid doctrines

Version: `0.2.1-dev`; doctrine selection extended in `0.3.54-dev`;
open-conflict pressure reduction added in `0.3.68-dev`; bounded alliance
strength added in `0.3.73-dev`; eligible officer replacement added in
`0.3.75-dev`; delayed allied-domain reinforcement added in `0.3.78-dev`.

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

The factor itself adds no serialized state. It is derived from the persistent
relation tracker whenever the ordinary natural raid executes. A delayed allied
wave does serialize its exact pair, points, arrival tick and participating pawns
until that one cooperative attack has resolved.

## Delayed allied-domain reinforcement

When the resolved factor is exactly `1.10` and the final combined budget is at
least `800` points, the ordinary natural raid divides that existing budget:

```text
primary force = combined points × 0.75
allied wave   = combined points × 0.25
```

One exact allied domain is selected from the attacking domain's active
alliances. Multiple alliances never create multiple waves. The primary raid
keeps its selected doctrine; the later support wave uses the validated direct
assault path.

The allied wave is scheduled silently for `1800` to `3600` ticks after the
primary raid. It uses `EdgeWalkIn`, retains the allied faction's exact name and
color, and produces no warning, countdown or advance letter. A localized RP
letter naming both domains appears only when the support force reaches the map.

RimWorld stores separate instances of the same permanent-enemy faction Def as
mutually hostile. A narrow Harmony postfix therefore reports the exact pair as
non-hostile only while its recorded cooperative attack is active. It does not
rewrite faction goodwill or the persistent vanilla relation. Target caches are
refreshed at activation and cleanup, and the original hostility becomes
authoritative again when either participating force is gone. Allied survivors
receive an exit order when the primary surviving force begins withdrawing.

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

## Eligible Jaffa officer replacement

`0.3.75-dev` adds no extra raid pawn. Revision `r2` wraps the real raid
execution in an incident-scoped combat-generation context. The existing
`PawnGroupMakerUtility.GeneratePawns` Harmony postfix then receives the vanilla
generated list and may replace one `SG1_GoauldJaffaGuard` with
`SG1_GoauldJaffaFieldOfficer` only when:

- the generated force contains at least five eligible Jaffa;
- no officer is already present;
- a combat guard exists to replace.

The field officer and replaced guard both use `145` combat power, so group size
and the generated threat budget remain unchanged. The mission-only capture
target keeps its historical `165` combat power and is not used by this path. If
a qualifying force contains only warriors or other
specialists, no officer is added. The officer receives the published red armor,
red retractable helmet, elite silver mark and Prim'ta.

The context also requires the exact expected pawn-group kind and is restored
immediately after generation, preventing unrelated raids or group makers from
inheriting the replacement. Externally forced doctrine regressions remain
unchanged. The dedicated `Force eligible natural raid with officer (900 points)`
action opts one forced direct raid into the officer layer for deterministic
validation.

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

`Force allied natural raid (1200 points, short delay)` requires an eligible
alliance, uses a deterministic `1200`-point raid and shortens only this test's
hidden delay to `600` ticks. `Show allied reinforcement report` reveals pending
or active state for diagnostics; normal play exposes no such countdown.

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

Final cumulative revision `r2` is validated and published in `0.3.75-dev`. The
officer procedure confirms the five-Jaffa threshold, one-for-one `145`-point
guard replacement, unchanged group size, below-threshold exclusion, capture
isolation, save/reload and a clean accepted `Player.log`.

Revision `r1` for `0.3.78-dev` is validated and published for the mandatory path:
silent delay, arrival-only RP letter, exact faction colors, temporary
cooperation and a clean accepted `Player.log`. Persistence and withdrawal
remain durable optional regressions.
