# Kara kesh neural attack

## Milestone

`0.3.60-dev - Add kara kesh neural attack`

Base: `v0.3.59-dev`

Branch: `feature/kara-kesh-neural-attack`

Validation status: final revision `r1` validated after forced rebuild and
in-game testing, then committed, tagged as `v0.3.60-dev` and published with the
synchronized separate wiki. No functional `r2` was required.

## Design contract

The neural attack is the third implemented kara kesh mode after the personal
shield and kinetic blast. It must remain distinct from prolonged paralysis,
torture and remote-control functions.

An eligible wearer may target one conscious hostile humanlike flesh pawn within
`8.9` cells and line of sight. The action:

- costs `1.75` points from the same four-point shield reserve;
- starts a `1200`-tick persistent cooldown;
- pauses shield recharge through the existing `300`-tick activity delay;
- applies `SG1_KaraKeshNeuralAgony` for `600` ticks;
- adds `0.45` pain and multiplies Consciousness by `0.8`;
- inflicts no direct injury, knockback, stun or Moving-capacity lock.

The result is acute disruption rather than guaranteed incapacitation. A healthy
pawn remains mobile, while an already injured or medically compromised pawn may
be downed by RimWorld's normal pain and consciousness thresholds.

The effect is biological and therefore ignores ordinary armor and projectile
shield absorption. It refuses animals, mechanoids, non-humanlike races, downed
pawns, dead pawns and any target that lacks functional Consciousness.

## Shared energy and counterplay

The shield, kinetic blast and neural attack all use the same energy value owned
by `CompShield`. A full device can use the neural attack twice only if it has
time to recharge between uses; one neural attack plus one kinetic blast consumes
`3.0` of the available `4.0` points.

The attack remains unavailable when:

- persistent biological naquadah traces are absent;
- the wearer is downed or dead;
- the shield is resetting after collapse;
- energy is below `1.75`;
- the `1200`-tick neural cooldown is active;
- the target is not a conscious hostile humanlike flesh pawn;
- range or line of sight fails.

EMP, melee and heat remain the main shield counterplay. The attack itself has no
armor check, but its temporary Hediff expires naturally and does not produce a
permanent injury.

## Architecture

### `Comp_KaraKeshShield`

The existing component owns:

- player gizmo construction and targeting;
- shared-energy consumption;
- serialized `karaKeshLastNeuralAttackTick`;
- ready, low-energy, reset and cooldown inspection text;
- player and hostile-AI execution;
- visual feedback;
- priority over kinetic AI during the same tick.

No Harmony combat patch or DLC ability framework is required.

### `KaraKeshNeuralAttackUtility`

This stateless service owns:

- biological target validation;
- lookup of the existing temporary effect;
- first application;
- duration refresh through `HediffComp_Disappears`;
- deterministic removal for debug tests.

### `SG1_KaraKeshNeuralAgony`

The new stable Hediff Def is visible in the Health tab. It uses the native
disappearing-Hediff component and contains no custom serialized state.

### AI

A hostile non-player wearer checks every `60` ticks. Neural attack is attempted
first against the closest valid target within `8.9` cells. If no neural attack
is used, the previously validated kinetic-blast search still runs.

The added control increases `SG1_GoauldSystemLordHost.combatPower` from `450` to
`500`.

## Save compatibility

The kara kesh keeps its stable `SG1_KaraKesh` identity. Existing saves receive
the new component field with the default sentinel value, making neural attack
immediately ready once other conditions are met.

The temporary target effect serializes through ordinary Hediff and
`HediffComp_Disappears` data. Cooldown serializes as an absolute last-use game
tick, matching the kinetic-blast pattern.

## Deterministic debug actions

Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Relevant actions:

- `Spawn hostile System Lord with rank equipment`;
- `Inspect neural attack state`;
- `Prepare neural attack test state`;
- `Use selected wearer's neural attack`;
- `Reset neural attack cooldown`;
- `Clear neural agony`.

For `Use selected wearer's neural attack`, first select the pawn wearing the
kara kesh, then activate the debug tool and click the hostile target.

## Completed validation

1. Prepare a traced player wearer at full energy and zero neural cooldown.
2. Use the visible neural-attack gizmo against a hostile Grand Master within
   `8.9` cells and line of sight.
3. Confirm the target gains the temporary Health-tab effect, pain and reduced
   Consciousness without injury, knockback or forced immobility.
4. Confirm energy falls from `4.00` to about `2.25` and cooldown begins near
   `1200` ticks.
5. Clear the effect, prepare the hostile Grand Master and confirm autonomous AI
   use against a nearby player pawn.
6. Save during effect and cooldown, reload and confirm both persist.
7. Review `Player.log`.

Result: passed on local revision `r1`, including player and hostile-AI use,
shared energy, temporary effect behavior, save/reload persistence and a clean
`Player.log`.

## Subsequent and deferred work

A maintained single-target paralysis hold is implemented separately in local
milestone `0.3.61-dev`. It does not alter this attack's pain and consciousness
contract.

Other speculative functions remain non-planned ideas in
`docs/IDEAS_TO_REVISIT.md`. Dedicated final graphics and gizmo icons remain part
of the future visual pass.
