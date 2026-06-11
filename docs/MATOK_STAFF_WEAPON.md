# Ma'Tok staff weapon prototype

Version: `0.1.64-dev`

## Scope

This milestone introduces the first playable Jaffa equipment item:

- `SG1_MatokStaff`
- `SG1_MatokStaffProjectile`

The staff can be spawned with developer tools, equipped by a pawn and
crafted at a machining table after `Gunsmithing` research.

## Prototype balance

- Spacer-level weapon.
- `24` burn damage per shot.
- `0.28` base armor penetration.
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
- `Gunsmithing` research.

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
- Natural Goa'uld raids, settlements and traders.
- Jaffa armor and facial markings.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML
   loading error appears.
2. Spawn `SG1_MatokStaff` with developer tools.
3. Verify that the temporary weapon texture is visible.
4. Equip the staff on a pawn and fire at a target.
5. Confirm that the projectile is visible, deals burn damage and produces no
   exception.
6. Verify that the staff remains usable as a melee weapon.
7. With `Gunsmithing` researched, confirm that a machining table offers a bill
   to craft the staff.
8. Spawn `SG1_GoauldJaffaWarrior` and `SG1_GoauldJaffaGuard` and confirm that
   their existing automatic Prim'ta initialization still works.
9. Confirm that no natural Goa'uld raid, settlement or trader has been enabled.

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
