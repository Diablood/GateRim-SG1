# Automatic Ma'Tok loadout for Goa'uld-aligned Jaffa

Version: `0.1.65-dev`

Guard diversification: `0.2.9-dev`

## Scope

Generated Goa'uld-aligned Jaffa servants use the existing weapon-tag loadout
system:

- warrior profiles receive `SG1_MatokStaff`;
- guard profiles may receive `SG1_MatokStaff` or `SG1_ZatnikTel` since
  `0.2.9-dev`.

## Implementation

The loadout uses RimWorld's vanilla weapon-tag system.

The Ma'Tok staff already declares:

```xml
<weaponTags>
    <li>SG1_MatokStaff</li>
</weaponTags>
```

Warrior `PawnKindDef` entries declare:

```xml
<weaponMoney>2000~2000</weaponMoney>
<weaponTags>
    <li>SG1_MatokStaff</li>
</weaponTags>
```

Guard `PawnKindDef` entries declare:

```xml
<weaponMoney>2000~2000</weaponMoney>
<weaponTags>
    <li>SG1_MatokStaff</li>
    <li>SG1_ZatnikTel</li>
</weaponTags>
```

No new C# component is required. The existing one-time automatic Prim'ta
initializer remains unchanged.

## Intentionally deferred

- Goa'uld traders and dedicated salvage incidents.
- Final weapon artwork.
- Jaffa armor and facial markings.
- Additional loadout roles beyond the current warrior/guard distinction.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML loading error appears.
2. Spawn `SG1_GoauldJaffaWarrior`.
3. Verify that the generated warrior has the Jaffa xenotype, an automatic Prim'ta and an equipped Ma'Tok staff.
4. Spawn several `SG1_GoauldJaffaGuard` pawns and verify that Ma'Tok and
   Zat'nik'tel weapon outcomes both occur.
5. Make each pawn fire the Ma'Tok staff and confirm that the projectile, primary burn damage and reduced structural impact against a mechanoid or building still work.
6. Save and reload, then verify that the equipped weapons remain present.
7. Trigger a natural Goa'uld raid and confirm guards can provide a rare
   Zat'nik'tel recovery route.
