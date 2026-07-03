# Current project state

Current milestone: `0.3.57-dev - Add System Lord kara kesh shield` - validated
and published after local revision `r5`.

## Repository state

- Starting tag: `v0.3.56-dev`.
- Starting commit: `93b29a7`.
- Active branch: `feature/goauld-system-lord-personal-shield`.
- Published branch: `feature/goauld-system-lord-personal-shield`.
- Last published version: `0.3.57-dev`.
- Last published tag: `v0.3.57-dev`.
- Technical assembly version: `0.3.57.0`.
- Final local revision: `r5`.
- Publication status: validated, committed, tagged and published; the separate
  wiki is synchronized.
- Next milestone: not selected. It must start from `v0.3.57-dev` on a dedicated
  branch.

## Current scope

This milestone adds the first functional high-rank Goa'uld equipment: a kara
kesh required only by generated `SG1_GoauldSystemLordHost` pawns. This slice
implements only the hand device's personal-shield mode.
Ordinary Goa'uld hosts and Jaffa do not receive it. Its added combat value is
represented by increasing the System Lord profile from `170` to `400`
`combatPower`.

The shield follows a readable RimWorld contract. It blocks incoming ranged
projectiles and shrapnel while charged, prevents the wearer from firing out,
and does not stop melee or heat. EMP breaks it immediately with RimWorld's
dedicated break sound, flash and crack effects. Its `4.0` maximum energy,
`0.2` recharge rate, `0.01` energy loss per damage and full-energy reset after
`1800` ticks let it absorb roughly `400` ranged damage from full charge.
Every relevant shield statistic is therefore better than the vanilla shield
belt baseline of `1.1`, `0.13`, `0.033`, `3200` ticks and `0.2` reset energy.
After any absorbed hit, active recharge pauses for `300` ticks. Sustained fire
therefore drains the reserve without simultaneous regeneration, while isolated
shots remain ineffective once the five-second quiet window passes.

Captured shields remain immediately usable. Local manufacture at a machining
table requires Crafting `12`, six advanced components, `100` plasteel and `60`
gold. The dedicated `Kara kesh` research costs `3000`, uses the
GateRim research tab and requires `Jaffa armor` first. The item is absent from
normal trader stock and random generation; natural recovery is limited to an
actual System Lord.

The current item texture is an explicit placeholder stored at the final stable
asset path. It will be replaced during the planned global visual pass without
changing the Def or save identity.

## Final r5 validation

Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1` on a player home map.

1. Open `Recherche > GateRim SG-1`. Confirm `Kara kesh` is
   directly to the right of `Armures Jaffa` and cannot be researched before
   that prerequisite.
2. Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh shield...
```

3. Select `Spawn hostile System Lord with kara kesh`. Pause, select the pawn and
   confirm its equipment includes `kara kesh` and the vanilla
   `Énergie du bouclier` / `Shield energy` gizmo is visible.
4. Select `Apply ranged test hit` ten times, clicking that pawn each time.
   Confirm every impact is absorbed, the field remains active and the pawn
   receives no projectile injury. The gizmo must show a gradual charge loss,
   not an immediate collapse or recharge between rapid impacts.
5. Select `Apply melee test hit`, then click the same pawn. Confirm a blunt
   injury passes through while the charged shield remains active.
6. Select `Apply EMP test hit`, then click the same pawn. Confirm the shield
   breaks immediately with a distinct sound, flash and crack effect. Let
   `1800` ticks pass and confirm it returns at full charge. Inspect `Player.log`
   for new XML, Def, texture or C# errors.

Expected result: the research dependency is visible and enforced; every System
Lord has the shield; ranged, melee and EMP interactions follow the stated
counterplay without errors.

Result: passed in game by the maintainer on final revision `r5`. The research
dependency, natural System Lord equipment, shield-energy gizmo, rapid ranged
absorption without active recharge, melee bypass, EMP collapse and reset, and
`Player.log` were validated.

## r4 result and r5 recharge delay

Revision `r4` passed: the kara kesh was highly resistant to gunfire, melee
still reached the wearer, EMP retained its forced shutdown and the vanilla
energy gizmo appeared on a player soldier. The field could not be exhausted by
the tested gunfire, which was accepted as lore-consistent, but its active
recharge was visibly very fast. Revision `r5` pauses recharge for `300` ticks
after every absorbed impact so sustained fire consumes energy without healing
the field at the same time.

## r3 result and r4 rebalance

Revision `r3` passed generation, equipment, ranged, melee and EMP behavior.
Balance was rejected: one debug ranged hit or the first two machine-pistol
bullets exhausted the field, which did not support the System Lord's intended
godlike presentation. Revision `r4` adopts the `4.0` capacity tier seen on
heavy Biotech shielding, reduces energy loss to `0.01` per damage, restores
full energy after a shorter forced-shutdown window and raises `combatPower` to
`400`. EMP remains the clear immediate shutdown counter.

## r2 finding and r3 correction

The r2 log confirms that the existing System Lord `Yareteris` received the
personal shield through the new initializer. The dedicated debug action then
failed while generating a different pawn because vanilla randomly attempted a
parent relation and cast the System Lord's formal name to an incompatible name
type. Revision `r3` uses an explicit non-player generation request with pawn
relations disabled. This changes only the isolated debug subject and leaves
normal world leaders untouched.

## r1 finding and r2 correction

The shield Def loaded, but two System Lords created by the dedicated debug
action wore no shield. The separate shield seen on the ground came from the
intentional `Spawn kara kesh` action and was not dropped by pawn
generation. Revision `r2` removes the ineffective XML assignment. The existing
generated-host initializer now equips one shield directly, records the pawn
identifier and never replaces a shield later removed by the player. The debug
spawn runs the initializer before reporting success.

Repeat the required test with a newly generated System Lord. Expected r3
correction: the shield is worn immediately and no duplicate shield is left on
the ground when only `Spawn hostile System Lord with kara kesh` is used. `Spawn
kara kesh` intentionally creates a standalone item on the ground and is
reserved for the optional player-equipment test.

## Optional regression checks

- After the EMP break, let the pawn survive until the field resets and confirm
  it begins recharging rather than remaining permanently disabled.
- Use `Spawn kara kesh`, finish both required researches with developer
  tools, and confirm the machining table offers exactly one matching recipe
  with the documented ingredients and Crafting `12` requirement.
- Save and reload with a worn shield partly charged; confirm the item, wearer
  and field state remain valid.
- Down or kill the generated System Lord and confirm the shield can be
  recovered, while ordinary Goa'uld hosts and Jaffa still do not carry one.

## Deferred work and known limitation

- The lore requirement for naquadah traces is not yet enforced. A future slice
  must define one persistent biological eligibility rule covering active
  Goa'uld, Tok'ra, Prim'ta-bearing Jaffa and former hosts before restricting
  activation.
- The placeholder texture is intentionally not representative of final art.

Final commit message:

```text
0.3.57-dev - add System Lord kara kesh shield
```
