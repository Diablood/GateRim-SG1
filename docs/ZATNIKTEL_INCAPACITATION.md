# Zat'nik'tel incapacitation prototype

Version: `0.1.77-dev`

## Purpose

This milestone adds the first playable Zat'nik'tel sidearm without attempting
to implement the complete multi-shot behavior immediately.

The first iteration focuses on a clear RimWorld role:

```text
temporary non-lethal neutralization
```

## Weapon Def

```text
SG1_ZatnikTel
```

The prototype is craftable at the machining table after `Gunsmithing`.

It intentionally remains outside automatic Jaffa loadouts until its combat
balance is validated manually.

## Projectile class

```text
GateRimSG1.Weapons.Projectile_ZatnikTelDisruption
```

The projectile inherits vanilla `RimWorld.Bullet`.

Its primary Def settings are:

```text
10 Stun
75 projectile speed
```

The projectile then adds a small EMP follow-up only for:

```text
non-organic pawn  -> 10 EMP
building          -> 8 EMP
```

Biological targets receive no physical injury and no EMP follow-up.

The prototype does not deal structural damage and does not create an area
explosion.

## Shared technology classification

Both weapon and projectile use:

```xml
<li Class="GateRimSG1.Weapons.SG1EnergyWeaponExtension">
    <technology>Goauld</technology>
    <affectedByFutureReplicatorResistance>true</affectedByFutureReplicatorResistance>
</li>
```

This remains declarative. Replicator-specific resistance is intentionally
reserved for a future milestone.

## Deferred behavior

Not included yet:

- lethal second shot;
- disintegrating third shot;
- persistent recent-shot state on targets;
- automatic Jaffa loadout integration;
- complex weapon-opening animation;
- future Replicator and Kull-specific resistances.

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Load a test map without new XML or C# errors.
3. Craft or spawn `SG1_ZatnikTel`.
4. Shoot a biological pawn and confirm temporary stun without a physical
   wound.
5. Shoot a vanilla mechanoid and confirm visible disruption.
6. Shoot a turret and confirm temporary disruption without structural damage.
7. Shoot a normal wall and confirm no structural damage.
8. Confirm that the pulse projectile is visible and does not cause an area
   explosion.
9. Confirm that newly generated Jaffa still receive Ma'Tok staffs rather than
   Zat'nik'tel sidearms.
10. Trigger the three controlled Jaffa raids and confirm no regression.
