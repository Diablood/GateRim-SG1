# Current project state

Current milestone: `0.3.59-dev - Add kara kesh kinetic blast` -
validated and published after local revision `r2`.

## Repository state

- Starting tag: `v0.3.58-dev`.
- Active branch: `feature/kara-kesh-kinetic-blast`.
- Published branch: `feature/kara-kesh-kinetic-blast`.
- Last published version: `0.3.59-dev`.
- Last published tag: `v0.3.59-dev`.
- Technical assembly version: `0.3.59.0`.
- Final local revision: `r2`.
- Publication status: validated, committed, tagged and published; the separate
  wiki is synchronized.
- Next milestone: not selected. It must start from `v0.3.59-dev` on a dedicated
  branch.

## Current scope

This milestone adds one offensive mode to the existing Goa'uld kara kesh without
bundling its remaining lore functions.

A biologically eligible wearer receives a targeted `Kinetic blast` / `Onde
cinétique` gizmo while wearing `SG1_KaraKesh`. The blast:

- targets a hostile pawn within `10.9` cells and line of sight;
- consumes `1.25` points from the same four-point shield-energy reserve;
- deals `12` blunt damage with `0.25` armor penetration;
- stuns the target for `120` ticks;
- pushes it up to two walkable, unoccupied cells directly away from the wearer;
- enters a `900`-tick cooldown;
- pauses shield recharge through the same post-impact delay used by absorbed
  attacks.

The action bypasses the ordinary outgoing-fire restriction because it is an
internal kara kesh mode, not a weapon verb. It remains unavailable while the
shield is resetting, while energy is insufficient, when the wearer is downed
or when persistent biological naquadah traces are absent.

Hostile non-player wearers check for the closest valid hostile pawn every `60`
ticks and may use the same implementation automatically. The added offensive
power raises `SG1_GoauldSystemLordHost.combatPower` from `400` to `450`.

Neural attack, prolonged paralysis, torture and remote-control functions remain
outside this milestone.

## Files and architecture

- `Comp_KaraKeshShield` owns player targeting, cooldown persistence, shared
  energy consumption, damage, stun, safe knockback and hostile AI use.
- `CompProperties_KaraKeshShield` exposes all balance values through the
  existing apparel Def.
- `GoauldSystemLordShieldDebugActions` exposes deterministic inspection,
  full-charge preparation, selected-wearer use and cooldown reset actions.
- `GateRimDebugActionMenu` groups all kara kesh tests under the visible
  `Kara kesh...` submenu.
- the existing research and item remain save-compatible; no new Def identity or
  research project is introduced.

## Final r2 validation

Load exactly:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Use open ground with clear line of sight. Pause the game while preparing the
player test.

1. On a player colonist, open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

   Run `Equip kara kesh on target`, then `Apply persistent trace` if needed.
2. Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

   Run `Prepare kinetic blast test state`, then `Inspect kinetic blast state`
   on the colonist. Confirm `trace=True`, an active shield, `4.00` energy and
   zero cooldown.
3. Run `Spawn hostile System Lord with kara kesh`. Keep the game paused and run
   `Prepare kinetic blast test state` on the hostile Grand Master. Select the
   colonist and use the visible `Onde cinétique` / `Kinetic blast` gizmo on the
   hostile Grand Master within `10.9` cells.
4. Confirm a visible flash and text, `12` blunt damage subject to armor, a short
   stun, and a push of up to two cells when unobstructed. The target must never
   be moved into a wall, occupied cell or outside the map.
5. Inspect the colonist again. Energy must have fallen by about `1.25`, and the
   cooldown must be close to `900` ticks. The gizmo must reject immediate reuse.
6. Run `Reset kinetic blast cooldown` on the colonist, then place an obstacle
   directly behind the target and fire again. Damage and stun must occur while
   knockback stops at the last valid cell.
7. Let the hostile Grand Master stand within range and unpause. Within roughly
   `60` ticks it must use the same kinetic blast naturally against a hostile
   player pawn, consuming its own energy and entering cooldown.
8. Save during an active cooldown, reload and inspect both wearers. Cooldown and
   remaining shield energy must persist.
9. Confirm a pawn without persistent naquadah traces still has no active shield
   or kinetic-blast gizmo, then inspect `Player.log` for new XML, Def, Scribe or
   C# errors.

Validation result: passed. The targeted player action, shared-energy cost,
cooldown refusal, safe knockback with and without an obstacle, hostile AI use,
save/reload persistence and biological refusal all behaved as expected. No new
XML, Def, Scribe or C# error was observed in `Player.log`.

## Optional regression checks

- fire at a target behind a wall and confirm that it cannot be selected;
- reduce shield energy below `1.25` and confirm the blast is disabled;
- break the shield with `Apply EMP test hit` and confirm the blast remains
  disabled throughout the `1800`-tick reset;
- verify ordinary ranged weapons remain blocked while the shield is active;
- verify melee and heat still bypass the shield;
- test a map-edge target and a target directly diagonal from the wearer.

## Remaining uncertainty

The first `r1` rebuild failed only because `MapPawns.AllPawnsSpawned` exposes
`IReadOnlyList<Pawn>` in RimWorld 1.6. Revision `r2` corrected that declaration,
rebuilt successfully and passed the complete targeted test. No gameplay
revision `r3` is required. The generic attack icon and placeholder kara kesh
texture remain intentionally deferred to the global visual pass.

## Next action

Apply the final documentation lock, run the prepublication checks and wait for
explicit publication authorization before committing, tagging, pushing the
branch or synchronizing the wiki.
