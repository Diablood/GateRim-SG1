# Goa'uld kara kesh

## Milestone

`0.3.57-dev - Add System Lord kara kesh shield`

Offensive extensions:

- `0.3.59-dev - Add kara kesh kinetic blast`;
- `0.3.60-dev - Add kara kesh neural attack`;
- `0.3.61-dev - Add kara kesh paralysis hold`.

## Design contract

The first Goa'uld rank equipment is the `kara kesh`, a naquadah-powered hand
device controlled through a neural interface and reserved here for
`SG1_GoauldSystemLordHost`. This milestone implements only its personal-shield
mode through RimWorld's native `CompShield` behavior:

- ranged projectiles and shrapnel consume shield energy;
- melee and heat bypass the field;
- EMP breaks the field immediately;
- the wearer cannot fire ranged weapons through the field;
- collapse creates a `1800`-tick reset window before the field returns at full
  charge with RimWorld's break and reset audiovisual feedback.

The field uses `4.0` maximum energy, `0.2` recharge and `0.01` energy loss per
damage, absorbing roughly `400` ranged damage from full charge. This is a
high-rank defense intended to sustain the Goa'uld's godlike presentation while
keeping EMP, melee and heat as decisive counters. The System Lord profile moves
from `170` to `400` `combatPower` so pawn-group budgets account for the added
protection.

The Goa'uld shield must remain strictly superior to the vanilla shield belt:

| Value | Vanilla shield belt | Kara kesh shield |
|---|---:|---:|
| Maximum energy | `1.1` | `4.0` |
| Recharge rate | `0.13` | `0.2` |
| Energy loss per damage | `0.033` | `0.01` |
| Approximate ranged damage from full charge | `33` | `400` |
| Broken reset delay | `3200` ticks | `1800` ticks |
| Energy on reset | `0.2` | `4.0` |

Selecting a wearer exposes RimWorld's native `Gizmo_EnergyShieldStatus`, shown
as `Shield energy` / `Énergie du bouclier`. It is the authoritative in-game
check for current charge and cooldown.

`Comp_KaraKeshShield` preserves native shield absorption, EMP, reset, rendering
and gizmo behavior but adds one rule: active recharge pauses for `300` ticks
after every absorbed hit. Repeated fire keeps extending this pause. Broken
reset countdowns are not paused.

## Availability and research

Every naturally generated System Lord requires the shield. Ordinary hosts and
Jaffa never receive one from their loadouts, and the item has no trader or
random-reward availability. Defeating or capturing a real System Lord can
therefore yield one rare field item.

Player reproduction is a separate progression step:

- research: `SG1_KaraKeshResearch`;
- prerequisite: `SG1_JaffaArmor`;
- GateRim research position: `(2, 1.7)`, directly right of Jaffa armor;
- research cost: `3000`, Spacer technology;
- bench: machining table;
- skill: Crafting `12`;
- ingredients: `6` advanced components, `100` plasteel and `60` gold.

Captured shields are usable before research. Research gates only local
manufacture, matching the existing GateRim crafting-research convention.

## Implementation

- `SG1_KaraKesh` owns the stable save identity and texture path.
- `GameComponent_GoauldHostCasteInitializer` equips the shield once per System
  Lord and persists that assignment, including migration of existing saves.
- `GR_DefOf.SG1_KaraKesh` supports deterministic debug
  spawning.
- `GoauldSystemLordShieldDebugActions` provides item, pawn and damage tests.
- the current raster is a temporary placeholder for the global visual pass.

`Comp_KaraKeshShield` remains a `CompShield` subclass and delegates native
shield behavior only for a biologically eligible wearer. Offensive mode state
and targeting live on the same apparel component, while neural Hediff handling
is isolated in `KaraKeshNeuralAttackUtility`; no Harmony combat patch is used.

The focused kinetic blast is implemented separately in `0.3.59-dev`, the
temporary neural attack in `0.3.60-dev`, and a maintained single-target
paralysis hold in `0.3.61-dev`. All use the same component and shield-energy
reserve. Other speculative functions remain only non-planned ideas in
`docs/IDEAS_TO_REVISIT.md`.

Since `0.3.58-dev`, activation requires the shared persistent biological
naquadah marker. Active Goa'uld and Tok'ra hosts, Prim'ta-bearing Jaffa and
former hosts qualify through `NaquadahTraceUtility`; faction and PawnKind are
never used as substitutes. An ineligible pawn may carry or wear the device, but
the field, energy gizmo and outgoing-fire block remain inactive.

## Kinetic blast extension

Since `0.3.59-dev`, an eligible wearer can release a focused kinetic pulse at a
hostile pawn within `10.9` cells and line of sight. Each activation consumes
`1.25` points from the same four-point shield reserve, pauses recharge and
starts a `900`-tick cooldown. The target receives `12` blunt damage with `0.25`
armor penetration, a `120`-tick stun and a safe push of up to two cells.

Knockback checks every destination cell for map bounds, walkability and pawn
occupancy. It stops before walls, occupied cells and map edges. The blast is an
internal kara kesh mode rather than a ranged weapon verb, so it can fire from
inside the active field while ordinary guns remain blocked.

Hostile non-player wearers use the same method automatically against the nearest
valid enemy on a `60`-tick check. The added control increases System Lord
`combatPower` from `400` to `450`.

Detailed architecture and tests are recorded in
`docs/GOAULD_KARA_KESH_KINETIC_BLAST.md`.

## Neural attack extension

Since `0.3.60-dev`, an eligible wearer can overload the nervous system of one
conscious hostile humanlike flesh pawn within `8.9` cells and line of sight.
The mode consumes `1.75` energy, pauses recharge and starts a `1200`-tick
cooldown.

The target receives `SG1_KaraKeshNeuralAgony` for `600` ticks, adding `0.45`
pain and multiplying Consciousness by `0.8`. It causes no direct injury,
knockback, stun or forced Moving-capacity lock. An already weakened target can
still become incapacitated through RimWorld's normal pain and consciousness
thresholds.

Hostile Grand Masters prioritize this mode against a valid nearby humanlike
target before falling back to their kinetic blast. The added control raises
their `combatPower` from `450` to `500`.

Detailed architecture and tests are recorded in
`docs/GOAULD_KARA_KESH_NEURAL_ATTACK.md`.

## Paralysis hold extension

In published revision `0.3.61-dev`, an eligible wearer can maintain a neural
lock on one conscious hostile humanlike flesh pawn within `6.9` cells and line
of sight. Activation consumes `2.5` energy and starts a persistent `1800`-tick
cooldown.

`SG1_KaraKeshParalysisHold` lasts at most `600` ticks, caps Moving at `0` and
multiplies Manipulation by `0.1`. It causes no direct damage and adds no pain.
The exact source apparel and target are serialized. A dedicated Hediff comp
revalidates their link every `15` ticks and removes the effect when wearer
control, equipment, biological eligibility, shield state, hostility, shared
map, range or line of sight becomes invalid.

While the hold remains active, shield recharge and the other two active modes
are blocked. The wearer may release the exact linked effect manually without an
energy refund or cooldown reset. Hostile Grand Masters prioritize the hold,
then neural attack, then kinetic blast. Their validated `combatPower` rises
from `500` to `550`.

Detailed architecture and tests are recorded in
`docs/GOAULD_KARA_KESH_PARALYSIS_HOLD.md`. Final revision `r1` passed the
forced build and full in-game validation checklist, then was committed, tagged
as `v0.3.61-dev` and published with the synchronized separate wiki. No
functional `r2` was required.

## Final validation

Use exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Run `Spawn hostile System Lord with kara kesh`, apply ten `Apply ranged test hit`
impacts, then one `Apply melee test hit` and one `Apply EMP test hit`. Verify
sustained ranged absorption and the decreasing shield-energy gizmo, melee
bypass, the audible and visible EMP collapse, full reset after `1800` ticks and
a clean `Player.log`. Rapid impacts must not recharge between hits; recharge
must resume about `300` ticks after the final absorbed impact.

Validation result: final revision `r5` passed the required in-game test. The
research dependency, natural equipment, energy gizmo, ranged absorption and
recharge pause, melee bypass, EMP collapse and reset, and `Player.log` were
validated by the maintainer.
