# Automatic modular armor loadouts for Goa'uld-aligned Jaffa

Version: `0.1.68-dev`

## Scope

Generated Goa'uld-aligned Jaffa servants now receive deterministic modular
armor loadouts:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`

## Implementation

The loadout uses RimWorld's vanilla `PawnKindDef.apparelRequired` field.
Unlike `apparelTags`, this imposes the exact required pieces rather than a
pool of possible apparel choices.

Both pawn kinds also declare:

```xml
<apparelMoney>0</apparelMoney>
```

This prevents unrelated random apparel from being added by the general
budget-based apparel generator.

## Warrior loadout

```xml
<apparelRequired Inherit="False">
    <li>SG1_JaffaLightArmor</li>
    <li>SG1_JaffaGauntlets</li>
    <li>SG1_JaffaReinforcedBoots</li>
    <li>SG1_JaffaDeployedHelmet</li>
</apparelRequired>
```

## Guard loadout

```xml
<apparelRequired Inherit="False">
    <li>SG1_JaffaHeavyArmor</li>
    <li>SG1_JaffaGauntlets</li>
    <li>SG1_JaffaReinforcedBoots</li>
    <li>SG1_JaffaDeployedHelmet</li>
</apparelRequired>
```

The helmet is generated in deployed form, then the existing retractable
helmet component applies its persistent mode. Automatic mode retracts the
helmet outside draft and deploys it while drafted.

## Intentionally deferred

- Natural Goa'uld raids, settlements and traders.
- Faction-specific facial markings.
- Final armor artwork.
- Loadout diversification by System Lord.
- Zat'nik'tel integration.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML
   loading error appears.
2. Spawn `SG1_GoauldJaffaWarrior`.
3. Verify that the warrior has:
   - Jaffa xenotype;
   - initial Prim'ta;
   - equipped Ma'Tok staff;
   - light Jaffa armor;
   - armored gauntlets;
   - reinforced boots;
   - retractable Jaffa helmet.
4. Spawn `SG1_GoauldJaffaGuard`.
5. Verify the same elements, with heavy torso armor instead of light armor.
6. Confirm that neither pawn receives unrelated random apparel.
7. Draft and undraft both pawns to verify automatic helmet deployment.
8. Cycle the helmet mode on one pawn and verify that manual modes still work.
9. Save and reload, then verify that equipment and helmet mode persist.
10. Confirm that every armor piece remains stockable under `Apparel > Jaffa`.
11. Confirm that no natural Goa'uld raid, settlement or trader is enabled.
