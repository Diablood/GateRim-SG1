# SG-team field uniform prototype

Version: `0.1.78-dev`

## Purpose

This milestone starts the SG-team equipment baseline with one simple
field uniform:

```text
SG1_GenericSGTeamUniform
```

The prototype is deliberately modular. It represents only the olive-drab BDU
base layer. Tactical boots, gloves and a vest remain separate future items.

## Apparel model

```text
ParentName: ApparelMakeableBase
Layer: OnSkin
Coverage: Torso, Shoulders, Arms, Legs
Category: SG1_SGTeamApparel
```

The uniform combines jacket and trousers as a single apparel item. This keeps
the early prototype easy to equip and leaves room for later modular tactical
equipment.

## Crafting

The uniform costs:

```text
80 Cloth
```

It can be crafted at:

```text
HandTailoringBench
ElectricTailoringBench
```

Crafting skill requirement:

```text
Crafting 3
```

## Protection

The BDU is clothing, not armor:

```text
ArmorRating_Sharp  0.08
ArmorRating_Blunt  0.04
ArmorRating_Heat   0.10
```

Future tactical vests and specialized gear can add meaningful protection
without overloading the textile baseline.

## Graphics

Temporary dedicated graphics are included for:

- inventory display;
- east, north, south and west facings;
- default, male, female, thin, fat, hulk and child body variants.

## Deferred equipment

Not included yet:

- SG tactical boots;
- SG tactical gloves;
- SG tactical vest;
- black, woodland and desert uniforms;
- heavy tactical, medical and scientific variants;
- automatic SG-team pawn loadouts.

## Manual test checklist

1. Load RimWorld and check for new XML errors.
2. Spawn or craft `SG1_GenericSGTeamUniform`.
3. Confirm the recipe appears at the vanilla hand and electric tailoring benches.
4. Equip the uniform on a colon and rotate the pawn through all four facings.
5. Test representative male, female, thin, fat, hulk and child pawns.
6. Confirm that the uniform can coexist with unrelated headgear and future
   outer-layer apparel.
7. Confirm that the SG-team apparel category appears correctly.
8. Confirm that the existing Jaffa equipment and controlled raids are
   unaffected.
