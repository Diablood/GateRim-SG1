# Natural Goa'uld Jaffa raid doctrines

Version: `0.2.1-dev`; doctrine selection extended in `0.3.54-dev`.

## Purpose

`SG1_GoauldJaffaNaturalRaid` remains the only natural Goa'uld raid incident.
It can now assign the validated direct, abduction or destruction doctrine
without creating three independent storyteller rolls.

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

## Doctrine selection

| Doctrine | Eligibility | Weight when eligible | Result |
| --- | --- | ---: | --- |
| Direct | always | `2` | `ImmediateAttack`, no stealing or kidnapping |
| Abduction | `800+` points and at least 2 free colonists | `1` | capture window, then extraction with or without victims |
| Destruction | `1800+` points and `10 000+` building wealth | `1` | sustained damage phase, recovery window, then extraction |

The weights normalize to:

- direct only: `100%` direct;
- abduction eligible: `67%` direct, `33%` abduction;
- all eligible: `50%` direct, `25%` abduction, `25%` destruction.

The two custom `RaidStrategyDef` selection curves remain zero. Generic vanilla
raid strategy resolution can never select them; only this dedicated worker and
developer regression incidents assign them explicitly.

## Debug access

Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

`Show current progression` reports the points, free-colonist count, building
wealth, exact requirements and normalized doctrine weights. The three
`Force natural ... raid` commands use this real natural worker while bypassing
normal eligibility, so manual validation depends on neither map preparation
nor a random roll.

## Required validation

Use a test colony with at least two free colonists to observe capture behavior.
Save and reload between raids. Building wealth does not gate forced tests.

1. Open `Show current progression` and verify the doctrine context.
2. Run `Force natural direct raid (300 points)` and verify direct edge arrival.
3. Reload and run `Force natural abduction raid (800 points)`; verify its
   localized warning and capture behavior.
4. Reload and run `Force natural destruction raid (1800 points)`; verify its
   localized warning, destruction phase and final recovery/extraction.
5. Confirm no pods and inspect `Player.log` for C#, XML, pawn or Lord errors.

Optional eligibility checks: use fewer than two free colonists for abduction or
less than `10 000` building wealth for destruction. The report must show `0%`
for normal selection while the corresponding forced command remains usable.

Final validation passed on `0.3.54-dev-r2`: all three natural-worker paths,
their distinct behavior, on-foot edge arrival and `Player.log` are accepted.
