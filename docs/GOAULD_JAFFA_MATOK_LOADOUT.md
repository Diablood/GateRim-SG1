# Automatic Ma'Tok loadout for Goa'uld-aligned Jaffa

Version: `0.1.65-dev`

## Scope

Generated Goa'uld-aligned Jaffa servants now receive the existing
`SG1_MatokStaff` weapon automatically:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`

## Implementation

The loadout uses RimWorld's vanilla weapon-tag system.

The Ma'Tok staff already declares:

```xml
<weaponTags>
    <li>SG1_MatokStaff</li>
</weaponTags>
```

Both Jaffa `PawnKindDef` entries now declare:

```xml
<weaponMoney>2000~2000</weaponMoney>
<weaponTags>
    <li>SG1_MatokStaff</li>
</weaponTags>
```

No new C# component is required. The existing one-time automatic Prim'ta
initializer remains unchanged.

## Intentionally deferred

- Natural Goa'uld raids, settlements and traders.
- Final weapon artwork.
- Jaffa armor and facial markings.
- Loadout diversification between warriors and guards.
- Zat'nik'tel integration.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML loading error appears.
2. Spawn `SG1_GoauldJaffaWarrior`.
3. Verify that the generated warrior has the Jaffa xenotype, an automatic Prim'ta and an equipped Ma'Tok staff.
4. Spawn `SG1_GoauldJaffaGuard` and verify the same three elements.
5. Make each pawn fire the Ma'Tok staff and confirm that the projectile, primary burn damage and reduced structural impact against a mechanoid or building still work.
6. Save and reload, then verify that the equipped weapons remain present.
7. Confirm that no natural Goa'uld raid, settlement or trader has been enabled.
