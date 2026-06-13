# Ma'Tok staff weapon prototype

Version: `0.1.64-dev`

Current acquisition update: `0.2.9-dev`

## Scope

This milestone introduces the first playable Jaffa equipment item:

- `SG1_MatokStaff`
- `SG1_MatokStaffProjectile`

The staff can be spawned with developer tools, equipped by a pawn and
crafted at a machining table after `SG1_JaffaWeaponry` research.

## Prototype balance

- Spacer-level weapon.
- `24` primary burn damage per shot.
- `0.28` base armor penetration.
- `8` additional blunt impact damage against non-organic pawns.
- `12` additional blunt impact damage against buildings and turrets.
- No additional blunt impact against organic pawns.
- No area explosion.
- Slow `2.4` second ranged cooldown.
- `1.5` second warmup.
- `27.9` cell range.
- Heavy `4.2` kg staff body.
- Two blunt/poking melee tools.

## Crafting recipe

- `60` steel.
- `20` plasteel.
- `2` industrial components.
- Crafting skill `6`.
- Machining table.
- `SG1_JaffaWeaponry` research.

## Graphics

Two dedicated temporary textures are included:

- `Textures/Things/Item/Equipment/WeaponRanged/SG1_MatokStaff.png`
- `Textures/Things/Projectile/SG1_MatokBlast.png`

They are placeholders for testing and can be replaced later without changing
the Def names.

## Intentionally deferred

- Automatic assignment to Goa'uld-aligned Jaffa servants.
- Final artwork.
- Dedicated custom sound effects.
- Goa'uld traders and dedicated salvage incidents.
- Jaffa armor and facial markings.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML
   loading error appears.
2. Spawn `SG1_MatokStaff` with developer tools.
3. Verify that the temporary weapon texture is visible.
4. Equip the staff on a pawn and fire at a target.
5. Confirm that the projectile is visible, deals burn damage and produces no
   exception.
6. Fire at a vanilla mechanoid and confirm that the reduced structural impact
   prevents the weapon from becoming ineffective.
7. Fire at a turret and a wall and confirm reduced structural deterioration.
8. Confirm that biological pawns do not receive an extra blunt injury and that
   the shot does not create an area explosion.
9. Verify that the staff remains usable as a melee weapon.
10. With `SG1_JaffaWeaponry` researched, confirm that a machining table offers
    a bill to craft the staff.
11. Spawn `SG1_GoauldJaffaWarrior` and confirm that its automatic Prim'ta and
    Ma'Tok loadout still work.
12. Spawn several `SG1_GoauldJaffaGuard` pawns and confirm that both Ma'Tok and
    Zat'nik'tel weapon outcomes occur while Prim'ta initialization still works.
13. Trigger a natural Goa'uld raid and confirm that guards can provide a rare
    Zat'nik'tel recovery route.

## AI combat primary verb

Since `0.1.70-dev r2`, the Ma'Tok ranged verb is explicitly marked as the
equipment primary verb:

```xml
<isPrimary>true</isPrimary>
```

Manual firing was already available through `hasStandardCommand`, but hostile
AI combat requires the equipment tracker to expose a primary verb. Without this
flag, generated Jaffa could receive an assault lord duty while failing to select
an attack job.
