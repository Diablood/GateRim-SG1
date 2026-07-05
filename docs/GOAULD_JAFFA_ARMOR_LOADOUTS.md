# Automatic modular armor loadouts for Goa'uld-aligned Jaffa

Version: `0.1.68-dev`

## Scope

Generated Goa'uld-aligned Jaffa servants now receive deterministic modular
armor loadouts:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`
- `SG1_GoauldJaffaOfficer` (mission-only command variant)

## Implementation

The standard warrior and guard loadouts use RimWorld's vanilla
`PawnKindDef.apparelRequired` field. Unlike `apparelTags`, this declares exact
required pieces rather than a pool of possible apparel choices.

The officer variants deliberately use `generateCommonality = 0` so they never
enter ordinary random apparel generation. After `r1` showed that the mission
target did not receive those pieces, final revision `0.3.74-dev-r2` keeps the
exact PawnKind declaration and adds a mission-spawner verification
that creates and equips the dedicated torso and deployed helmet if vanilla did
not add them during pawn generation.

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

## Mission officer loadout (`0.3.74-dev`)

```xml
<apparelRequired Inherit="False">
    <li>SG1_JaffaOfficerArmor</li>
    <li>SG1_JaffaGauntlets</li>
    <li>SG1_JaffaReinforcedBoots</li>
    <li>SG1_JaffaOfficerDeployedHelmet</li>
</apparelRequired>
```

The officer torso armor preserves the heavy armor's protection and movement
penalty, adds `SocialImpact +0.10`, and uses a stable dedicated texture path.
The officer helmet switches only with
`SG1_JaffaOfficerRetractedHelmet`. Temporary red textures distinguish the
mission target from its ordinary escort.

The helmet is guaranteed in deployed form immediately after the capture target
is generated, then the existing retractable helmet component applies its
persistent mode. Automatic mode retracts the helmet outside draft and deploys it
while drafted. If either distinctive piece cannot be equipped, encounter
initialization fails cleanly instead of accepting an incorrectly dressed
target. This final `r2` behavior is validated in game.

## Intentionally deferred

- Natural Goa'uld raids, settlement defenses and other missions. A future
  expansion may use zero or one officer only in groups containing at least five
  Jaffa, replacing an ordinary pawn within the existing threat budget.
- Traders.
- Faction-specific facial markings.
- Final armor artwork, including replacement of the temporary red officer set.
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
