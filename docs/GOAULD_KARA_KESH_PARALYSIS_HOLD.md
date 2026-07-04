# Kara kesh paralysis hold

## Milestone

`0.3.61-dev - Add kara kesh paralysis hold`

Branch: `feature/kara-kesh-paralysis-hold`

Base: `v0.3.60-dev`

## Design contract

The paralysis hold is a maintained single-target control mode, not a damaging
attack and not a generic mental-control system. It must remain readable and
interruptible:

- one conscious hostile humanlike flesh target;
- `6.9`-cell range and direct line of sight;
- `2.5` shared shield energy paid once on activation;
- maximum duration `600` ticks;
- `1800`-tick cooldown starting at activation;
- Moving capped at `0` and Manipulation multiplied by `0.1`;
- no injury, damage-over-time or added pain;
- no shield recharge or other active kara kesh mode while maintained;
- immediate manual release, with no energy refund or cooldown reset.

The link is interrupted when the wearer dies or is downed, the apparel is no
longer worn, biological naquadah eligibility is lost, the shield is resetting,
the two pawns leave the same map, hostility ends, range is exceeded or line of
sight is blocked. Validation runs every `15` ticks so the effect cannot become a
long-lived orphan after equipment changes or loading.

## Architecture

`Comp_KaraKeshShield` is changed to a partial class. Existing shield, kinetic
and neural logic remains in the original source file, while
`Comp_KaraKeshShield_ParalysisHold.cs` owns:

- the source apparel's exact active-target reference;
- cooldown serialization;
- player target and release commands;
- shared-energy consumption;
- hostile AI priority;
- recharge and other-mode blocking while active;
- inspection and debug state.

`SG1_KaraKeshParalysisHold` is a `HediffWithComps` with:

- `HediffComp_Disappears` for the maximum duration;
- `HediffComp_KaraKeshParalysisHold` for the exact source-apparel reference and
  periodic link validation;
- a stage that sets Moving maximum to `0` and applies a `0.1` Manipulation
  factor.

`KaraKeshParalysisHoldUtility` centralizes biological target eligibility,
application, source matching and removal. The source and target are both
serialized as references, so an active maintained link can survive save/reload
and be revalidated naturally on the next tick.

## AI ordering

A hostile wearer checks every `60` ticks. The priority order becomes:

1. maintained paralysis hold;
2. temporary neural attack;
3. kinetic blast.

Once a hold is active, the wearer does not start another kara kesh mode until
the hold ends. `SG1_GoauldSystemLordHost.combatPower` rises from `500` to `550`.

## Save compatibility

No existing Def is renamed. The kara kesh receives new optional serialized
fields with stable defaults. Old saves load with no active target and no
paralysis cooldown. The new Hediff stores the exact source apparel rather than
inferring a wearer from faction, PawnKind or proximity.

## Deferred ideas

Other speculative kara kesh functions are not part of this milestone. They
remain recorded only as non-planned concepts in `docs/IDEAS_TO_REVISIT.md`.

## Mandatory r1 test

Use exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Run `Prepare paralysis hold test state`, `Inspect paralysis hold state`,
`Spawn hostile System Lord with rank equipment`, then use the visible player gizmo.
Validate movement and manipulation suppression, shared energy, active-target
reporting, interruption after range or line-of-sight loss, hostile AI use,
save/reload persistence, manual release and `Player.log`.

Status: final revision `r1` passed the forced `0.3.61.0` build and the complete
mandatory in-game checklist, then was committed, tagged as `v0.3.61-dev` and
published with the synchronized separate wiki. No functional `r2` was
required.
