# Automatic modular armor loadouts for Goa'uld-aligned Jaffa

Version: `0.1.68-dev`

## Scope

Generated Goa'uld-aligned Jaffa servants now receive deterministic modular
armor loadouts:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`
- `SG1_GoauldJaffaOfficer` (published capture target)
- `SG1_GoauldJaffaFieldOfficer` (combat and mission replacement)
- `SG1_GoauldSettlementJaffaOfficer` (settlement command replacement)

## Implementation

The standard warrior and guard loadouts use RimWorld's vanilla
`PawnKindDef.apparelRequired` field. Unlike `apparelTags`, this declares exact
required pieces rather than a pool of possible apparel choices.

The officer variants deliberately use `generateCommonality = 0` so they never
enter ordinary random apparel generation. Final revision `0.3.74-dev-r2` added
the first post-generation guarantee for the capture target. `0.3.75-dev` reuses
the same contract through a shared force utility that creates and equips the
dedicated torso and deployed helmet whenever an eligible field or settlement
officer is generated.

The shared warrior and guard bases also declare:

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

## Officer loadout (`0.3.74-dev`, force expansion `0.3.75-dev`)

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
`SG1_JaffaOfficerRetractedHelmet`. Temporary red textures distinguish every officer from ordinary Jaffa.

The helmet is guaranteed in deployed form immediately after any officer is
generated, then the existing retractable helmet component applies its persistent
mode. Automatic mode retracts the helmet outside draft and deploys it while
drafted. The published capture adapter still aborts encounter initialization if
either distinctive piece cannot be equipped; the new force layer instead keeps
the original guard when officer creation fails.

## Eligible officer forces

Since `0.3.75-dev`, the same officer presentation is available outside the
capture operation in bounded contexts:

- ordinary natural Goa'uld raids;
- Goa'uld Settlement pawn groups;
- introduction and distress-call hostile groups;
- relay defenders and reinforcements;
- delivery interceptions and diversion assaults.

A generated group must contain at least five eligible Jaffa. One ordinary guard
may be replaced by one officer; no pawn is added. The field officer and combat
guard both cost `145` combat power, while settlement officers and settlement
guards both cost `130`. The published capture target remains a separate `165`-
point PawnKind. A group without a budget-equivalent guard remains
unchanged. Capture escorts never receive a second officer.

## Published validation

Final cumulative revision `r2` is validated and published in `0.3.75-dev`.
Eligible combat, settlement and mission groups preserve their generated pawn
count and threat budget, never receive more than one officer, and retain the
complete red command loadout after save/reload. Ordinary Jaffa and the capture
escort remain unchanged.

## Intentionally deferred

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
