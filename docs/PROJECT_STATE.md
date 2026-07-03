# Current project state

Current milestone: `0.3.60-dev - Add kara kesh neural attack` -
validated and published after local revision `r1`.

## Repository state

- Starting tag: `v0.3.59-dev`.
- Active branch: `feature/kara-kesh-neural-attack`.
- Published branch: `feature/kara-kesh-neural-attack`.
- Last published version: `0.3.60-dev`.
- Last published tag: `v0.3.60-dev`.
- Technical assembly version: `0.3.60.0`.
- Final local revision: `r1`.
- Publication status: validated, committed, tagged and published; the separate
  wiki is synchronized.
- Next milestone: not selected. It must start from `v0.3.60-dev` on a dedicated
  branch.

## Current scope

This milestone adds one additional kara kesh mode without bundling prolonged
paralysis, torture or remote-control mechanics.

A biologically eligible player wearer receives a targeted `Neural attack` /
`Attaque neurale` gizmo. It:

- targets a conscious hostile humanlike flesh pawn within `8.9` cells and line
  of sight;
- consumes `1.75` points from the same four-point shield-energy reserve;
- pauses shield recharge through the existing `300`-tick post-use delay;
- applies `SG1_KaraKeshNeuralAgony` for `600` ticks;
- adds `0.45` pain and multiplies Consciousness by `0.8`;
- deals no direct damage, causes no knockback and does not set Moving to zero;
- enters a persistent `1200`-tick cooldown.

The temporary Hediff can incapacitate an already weakened target through normal
RimWorld pain or consciousness rules, but it is not a guaranteed paralysis
effect. Armor and the kara kesh projectile shield do not absorb it because it
is a direct biological neural overload rather than `DamageInfo`.

Hostile non-player wearers prioritize the neural attack against the nearest
valid target on a `60`-tick check. If no neural target can be used, the existing
kinetic-blast AI remains available. The added control raises
`SG1_GoauldSystemLordHost.combatPower` from `450` to `500`.

## Files and architecture

- `Comp_KaraKeshShield` owns the player command, shared-energy cost, cooldown
  persistence, inspection status and hostile AI choice.
- `KaraKeshNeuralAttackUtility` centralizes biological target eligibility,
  temporary Hediff application, duration refresh and debug removal.
- `SG1_KaraKeshNeuralAgony` is a temporary visible Hediff with no direct damage
  or movement lock.
- `GR_DefOf.SG1_KaraKeshNeuralAgony` provides a stable debug and code reference.
- `GoauldSystemLordShieldDebugActions` and `GateRimDebugActionMenu` expose the
  exact deterministic tests.
- the existing `SG1_KaraKesh` item and research remain save-compatible; no
  second hand device or research project is introduced.

## Mandatory r1 validation — completed

Load exactly:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Use open ground with clear line of sight. Pause while preparing the player test.

1. On a player colonist, open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

   Run `Equip kara kesh on target`, then `Apply persistent trace` if needed.
2. Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

   Run `Prepare neural attack test state`, then `Inspect neural attack state`
   on the colonist. Confirm `trace=True`, an active shield, `4.00` energy and
   `cooldownTicks=0`.
3. Run `Spawn hostile System Lord with kara kesh`. Keep both pawns within `8.9`
   cells with clear line of sight.
4. Select the colonist, use the visible `Attaque neurale` / `Neural attack`
   gizmo and target the hostile Grand Master.
5. Confirm the visual flash and floating neural-agony text. The target must gain
   `douleur neurale du kara kesh` / `kara kesh neural agony` for about `600`
   ticks, with increased pain and reduced Consciousness, but no direct injury,
   knockback or forced Moving capacity of zero.
6. Run `Inspect neural attack state` on the colonist. Energy must be near `2.25`
   and cooldown near `1200` ticks. Immediate reuse must be refused.
7. Run `Clear neural agony` on the target, then `Prepare neural attack test
   state` on the hostile Grand Master. Keep the player colonist hostile and
   within `8.9` cells, then unpause. Within roughly `60` ticks the Grand Master
   must use the same neural attack, consume its own energy and enter cooldown.
8. Save while the target still has neural agony and the attacker is in cooldown.
   Reload and confirm both the temporary Hediff duration and the attacker's
   cooldown/energy persist.
9. Inspect `Player.log` for new XML, Def, Scribe, targeting or C# errors.

Validation result: successful on local revision `r1` after a forced `0.3.60.0`
rebuild. The player gizmo, shared-energy cost, temporary neural-agony effect,
hostile System Lord AI, save/reload persistence and `Player.log` were accepted.
No gameplay correction or local revision `r2` is required.

## Optional regression checks

- try to target a friendly pawn, animal, mechanoid, downed pawn, target behind a
  wall or target outside `8.9` cells;
- reduce energy below `1.75` and confirm the command is disabled;
- break the shield with `Apply EMP test hit` and confirm neural attack remains
  unavailable during the `1800`-tick reset;
- verify a wearer without persistent naquadah traces receives neither the
  shield nor either offensive gizmo;
- recheck kinetic blast, ranged absorption, melee bypass, heat bypass and EMP
  collapse after the new mode is added;
- allow the neural-agony Hediff to expire naturally and confirm health
  capacities return to their previous values.

## Remaining uncertainty

The targeted implementation is validated without a required gameplay fix. The
`1.75` energy cost, `600`-tick effect, `1200`-tick cooldown and
`combatPower 500` remain balance values to watch during long-form play rather
than blockers for this milestone.

The generic attack icon and placeholder kara kesh texture remain deferred to
the global visual pass.

## Next action

Select the next small milestone from the durable roadmap. It must start from
`v0.3.60-dev` on a dedicated branch; no `0.3.61-dev` scope is imposed by this
closure.
