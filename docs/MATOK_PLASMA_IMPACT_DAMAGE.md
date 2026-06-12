# Ma'Tok plasma-impact damage prototype

Version: `0.1.76-dev`

## Purpose

The Ma'Tok staff previously dealt only vanilla `Burn` damage. That correctly
represents its thermal plasma injury against biological targets, but it can
leave the weapon disproportionately weak against non-biological targets.

This milestone preserves the existing identity of the weapon while adding a
small structural impact.

## Projectile class

```text
GateRimSG1.Weapons.Projectile_MatokPlasmaImpact
```

The projectile inherits vanilla `RimWorld.Bullet`, so the existing ranged
impact path remains intact.

## Damage model

The primary projectile settings remain:

```text
24 Burn
0.28 armor penetration
```

After the primary impact, a secondary structural hit is added only for:

```text
non-organic pawn  -> 8 Blunt
building          -> 12 Blunt
```

Organic pawns receive no secondary structural hit.

The extra building impact naturally includes turrets because they are
buildings. The prototype intentionally does not add an area explosion.

## Technology classification

Both the equipment Def and projectile Def include:

```xml
<li Class="GateRimSG1.Weapons.SG1EnergyWeaponExtension">
    <technology>Goauld</technology>
    <affectedByFutureReplicatorResistance>true</affectedByFutureReplicatorResistance>
</li>
```

This classification is declarative in `0.1.76-dev`. Replicator pawns do not
exist yet. A future milestone can query the shared extension instead of
hard-coding each Goa'uld or Tok'ra weapon separately.

## Manual test checklist

1. Build with `-t:Rebuild`.
2. Load a test map without new XML or C# errors.
3. Equip a player-controlled colon with `SG1_MatokStaff`.
4. Fire at a biological pawn and confirm an ordinary thermal injury without
   an extra blunt injury.
5. Fire at a vanilla mechanoid and confirm that the shot causes damage.
6. Fire repeatedly at a turret and confirm structural deterioration.
7. Fire repeatedly at a steel or stone wall and confirm structural
   deterioration.
8. Confirm that one shot does not produce an area explosion.
9. Trigger the three controlled Goa'uld Jaffa raids and confirm normal combat
   behavior.
