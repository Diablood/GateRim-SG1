# Modular Jaffa armor prototypes

Version: `0.1.66-dev`

## Scope

This milestone adds five independently testable apparel items:

- `SG1_JaffaLightArmor`
- `SG1_JaffaHeavyArmor`
- `SG1_JaffaGauntlets`
- `SG1_JaffaReinforcedBoots`
- `SG1_JaffaDeployedHelmet`

No automatic pawn loadout is added yet.

## Storage category

All five apparel items are explicitly assigned to the custom
`SG1_JaffaApparel` category, displayed as `Jaffa` under the vanilla
`Apparel` storage branch. This keeps the complete modular set selectable
through one player-facing stockpile filter.

## Coverage and vanilla layers

| Item | Body-part groups | Vanilla layer |
|---|---|---|
| Light torso armor | `Torso`, `Neck`, `Shoulders` | `Shell` |
| Heavy torso armor | `Torso`, `Neck`, `Shoulders` | `Shell` |
| Armored gauntlets | `Arms`, `Hands` | `Middle` |
| Reinforced boots | `Legs`, `Feet` | `Middle` |
| Deployed helmet | `FullHead` | `Overhead` |

`Hands` supplements `Arms` so the gloves also protect fingers.
`Feet` supplements `Legs` so the boots also protect toes.

## Prototype balance

| Item | Sharp | Blunt | Heat | HP | Mass | Move speed |
|---|---:|---:|---:|---:|---:|---:|
| Light torso armor | 68 % | 28 % | 42 % | 240 | 6.5 kg | -0.06 c/s |
| Heavy torso armor | 88 % | 40 % | 56 % | 340 | 11 kg | -0.22 c/s |
| Armored gauntlets | 48 % | 22 % | 38 % | 170 | 0.8 kg | — |
| Reinforced boots | 52 % | 25 % | 38 % | 190 | 1.2 kg | — |
| Deployed helmet | 92 % | 38 % | 52 % | 220 | 2.8 kg | — |

## Helmet roadmap

The current helmet is static and deployed. A later C# milestone will add
retractable visuals with three persistent modes:

- automatic: retracted when undrafted, deployed when drafted;
- always deployed;
- always retracted.

Raw armor ratings will remain identical. Coverage will differ:

- retracted: `UpperHead`;
- deployed: `FullHead`.

## Manual test checklist

1. Start RimWorld with developer mode enabled and confirm that no new XML or
   translation-loading error appears.
2. Spawn each of the five apparel items with developer tools.
3. Confirm that each temporary item texture is visible.
4. Equip the light armor, gauntlets, boots and helmet on a pawn.
5. Confirm that the four items can be worn simultaneously over ordinary OnSkin
   apparel.
6. Verify that light torso armor covers torso, neck and shoulders.
7. Verify that gauntlets cover arms, hands and fingers.
8. Verify that boots cover legs, feet and toes.
9. Verify that the deployed helmet covers the full head and facial parts.
10. Replace light torso armor with heavy torso armor and confirm that the rest of
    the modular set remains wearable.
11. With `Gunsmithing` researched, confirm that the machining table offers five
    crafting bills.
12. Spawn a Goa'uld-aligned Jaffa warrior and guard and confirm that existing
    Prim'ta and Ma'Tok loadouts still work.
13. Confirm that no natural Goa'uld raid, settlement or trader is enabled.

## Officer command variant (`0.3.74-dev`)

The capture officer and eligible Goa'uld field and settlement officers use a
dedicated heavy torso armor and
retractable helmet pair. Their Def names and texture paths are final; the first
PNG set is a temporary red recolor of the existing brown/gold prototypes.

The torso armor retains the heavy armor values and adds:

```xml
<SocialImpact>0.10</SocialImpact>
```

The helmet adds no social offset. Both visible pieces are craftable only after
`SG1_JaffaArmor`; the retracted helmet remains an internal state.

The red pieces keep zero random-generation commonality. After vanilla mission
generation omitted them in `r1`, the capture-site spawner now verifies the newly
generated officer and explicitly equips the dedicated
torso and deployed helmet when necessary. This targeted guarantee does not alter
ordinary Jaffa generation. Final revision `r2` validates the complete red
loadout, the single social offset and unchanged standard Jaffa equipment.

## Eligible force use (`0.3.75-dev`)

Combat and mission groups may replace one `145`-point guard with the dedicated
`SG1_GoauldJaffaFieldOfficer`; settlement groups use the dedicated `130`-point
settlement officer. The capture target remains the historical `165`-point
`SG1_GoauldJaffaOfficer`. The red
apparel Defs and texture paths remain shared and stable. Final cumulative
revision `r2` validates both budget-neutral officer profiles, the five-Jaffa
threshold, the one-officer cap and persistence of the complete loadout.
