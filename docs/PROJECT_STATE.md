# Current project state

Current milestone: `0.3.61-dev - Add kara kesh paralysis hold` - validated and
published after local revision `r1`.

## Repository state

- Starting tag: `v0.3.60-dev`.
- Active branch: `feature/kara-kesh-paralysis-hold`.
- Published branch: `feature/kara-kesh-paralysis-hold`.
- Last published version: `0.3.61-dev`.
- Last published tag: `v0.3.61-dev`.
- Technical assembly version: `0.3.61.0`.
- Final local revision: `r1`.
- Publication status: validated, committed, tagged and published; the separate
  wiki is synchronized.
- Next milestone: not selected. It must start from `v0.3.61-dev` on a dedicated
  branch.
- The deferred kara kesh concepts requested by the maintainer are recorded in
  `docs/IDEAS_TO_REVISIT.md` and are not active roadmap commitments.

## Current scope

This milestone adds one maintained paralysis mode to the existing Goa'uld kara
kesh without folding any other speculative function into the active roadmap.

A biologically eligible player wearer receives a targeted `Paralysis hold` /
`Maintien paralysant` gizmo. It:

- targets one conscious hostile humanlike flesh pawn within `6.9` cells and
  direct line of sight;
- consumes `2.5` points from the same four-point shield-energy reserve;
- applies `SG1_KaraKeshParalysisHold` for at most `600` ticks;
- sets Moving to a maximum of `0` and multiplies Manipulation by `0.1`;
- deals no direct damage and adds no pain;
- suspends shield recharge and prevents the other active kara kesh modes while
  the hold is maintained;
- ends early if the wearer is downed or killed, the kara kesh is removed, the
  biological trace is lost, the shield resets, hostility ends, range is broken
  or line of sight is lost;
- can be released manually without refunding energy;
- enters a persistent `1800`-tick cooldown from activation.

Hostile non-player wearers check for a valid paralysis target every `60` ticks
and prioritize this mode before neural attack and kinetic blast. The added
control raises `SG1_GoauldSystemLordHost.combatPower` from `500` to `550`.

## Files and architecture

- `Comp_KaraKeshShield` remains the shared shield and active-mode owner.
- `Comp_KaraKeshShield_ParalysisHold.cs` isolates paralysis state, targeting,
  shared-energy consumption, cooldown, player commands and hostile AI choice.
- `KaraKeshParalysisHoldUtility` owns target eligibility, Hediff application,
  source matching and deterministic removal.
- `HediffComp_KaraKeshParalysisHold` stores the exact source apparel and checks
  the maintained link every `15` ticks, including after save/reload.
- `SG1_KaraKeshParalysisHold` is a visible temporary Hediff with no injury or
  pain component.
- `GoauldSystemLordShieldDebugActions` and `GateRimDebugActionMenu` expose the
  exact deterministic tests.
- deferred speculative kara kesh concepts are kept in
  `docs/IDEAS_TO_REVISIT.md`, not in the active milestone.

## Mandatory r1 validation - passed

Load exactly:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Use open ground and keep the game paused while preparing the player test.

1. On a player colonist, open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

   Run `Equip kara kesh on target`, then `Apply persistent trace` if required.
2. Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

   Run `Prepare paralysis hold test state`, then `Inspect paralysis hold state`
   on the colonist. Confirm `trace=True`, an active shield, `4.00` energy,
   `cooldownTicks=0` and `activeTarget=<none>`.
3. Run `Spawn hostile System Lord with kara kesh`. Keep the two pawns within
   `6.9` cells with clear line of sight.
4. Select the colonist, use the visible `Maintien paralysant` / `Paralysis hold`
   gizmo and target the hostile Grand Master.
5. Confirm the target receives `maintien paralysant du kara kesh` /
   `kara kesh paralysis hold`, falls immobile, has Moving capped at `0` and
   Manipulation reduced to `10 %`, without a new injury or added pain.
6. Run `Inspect paralysis hold state` on the colonist. Confirm energy near
   `1.50`, cooldown near `1800`, and the target named as active. Kinetic blast
   and neural attack must be unavailable while the hold remains active.
7. Move the wearer beyond `6.9` cells or place a solid wall between wearer and
   target. Within about `15` ticks the Hediff must disappear and capacities must
   recover. The cooldown must continue.
8. Run `Prepare paralysis hold test state` on the hostile Grand Master. Keep the
   colonist hostile, conscious and within range, then unpause. Within roughly
   `60` ticks the Grand Master must use the same hold and stop using the other
   modes while maintaining it.
9. Save while the AI hold is active, reload, and confirm the exact wearer-target
   link, remaining Hediff duration, energy and cooldown persist. Then use
   `Release paralysis hold` on the wearer and confirm immediate recovery.
10. Inspect `Player.log` for new XML, Def, Scribe, Hediff, targeting or C# errors.

## Optional regression checks

- friendly pawn, animal, mechanoid, downed pawn, target outside `6.9` cells,
  target behind a wall or target already held: targeting refused;
- energy below `2.5`: paralysis command disabled;
- `Apply EMP test hit` during a hold: shield collapse interrupts the effect;
- remove the kara kesh or down the wearer during a hold: effect removed within
  about `15` ticks;
- `Release paralysis hold` from the visible gizmo and debug menu removes only
  the exact linked effect and does not reset the cooldown;
- kinetic blast, neural attack and shield behavior from `0.3.59-dev` and
  `0.3.60-dev` remain unchanged outside an active hold.

## Validation result

The maintainer confirmed the complete mandatory `r1` checklist after a forced
`0.3.61.0` build. Player targeting, capacity suppression, shared energy,
manual and automatic interruption, hostile AI priority, save/reload
persistence, manual release and `Player.log` all passed. The tested balance
values (`2.5` energy, `600` ticks, `1800`-tick cooldown, `6.9` cells and
`combatPower 550`) are accepted for the final revision.

## Next action

Select the next small milestone from the durable roadmap. It must start from
`v0.3.61-dev` on a dedicated branch; no `0.3.62-dev` scope is imposed by this
closure. The speculative kara kesh functions remain only in
`docs/IDEAS_TO_REVISIT.md` until separately discussed and approved.
