# Goa'uld kara kesh

## Milestone

`0.3.57-dev - Add System Lord kara kesh shield`

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

No custom combat component or Harmony patch is used. Native RimWorld shield
semantics are sufficient for this milestone.

The kinetic blast, neural attack, paralysis and remote-control functions
associated with the kara kesh remain deliberately deferred. They must be added
as separate gameplay slices rather than bundled into the shield prototype.

Lore also limits activation to a user carrying naquadah traces. The current
implementation does not enforce this yet because GateRim needs one durable marker
that correctly covers active Goa'uld, Tok'ra, Prim'ta-bearing Jaffa and former
hosts. A faction or PawnKind check would be an inaccurate substitute.

## Final validation

Use exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh shield...
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
