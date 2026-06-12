# SG tactical boots prototype

Version: `0.1.79-dev`

## Purpose

This milestone adds the first separate footwear component for SG-team field
equipment:

```text
SG1_SGTacticalBoots
```

The boots complement the olive-drab field uniform without being merged into
it.

## Apparel model

```text
ParentName: ApparelMakeableBase
Layer: Middle
Coverage: Feet
Category: SG1_SGTeamApparel
```

The prototype covers only the feet. It intentionally avoids the broader
`Legs` coverage used by reinforced Jaffa boots, because SG tactical boots are
ordinary durable field footwear rather than segmented armor.

## Crafting

```text
35 plain leather
10 cloth
Crafting 3
```

Available at:

```text
HandTailoringBench
ElectricTailoringBench
```

## Protection

```text
ArmorRating_Sharp  0.12
ArmorRating_Blunt  0.08
ArmorRating_Heat   0.08
Insulation_Cold    2
```

These values remain modest and are not intended to replace armor.

## Graphics

Temporary dedicated graphics are included for:

- inventory display;
- east, north, south and west facings;
- default, male, female, thin, fat, hulk and child body variants.

## Deferred equipment

Not included yet:

- SG tactical gloves;
- SG tactical vest;
- environment-specific uniform variants;
- automatic SG-team pawn loadouts;
- final art and semi-realistic wiki concept art.

## Manual test checklist

1. Load RimWorld and check for XML errors.
2. Spawn or craft `SG1_SGTacticalBoots`.
3. Confirm the recipe appears at both vanilla tailoring benches.
4. Equip the boots alone and inspect all four facings.
5. Equip the boots together with `SG1_GenericSGTeamUniform`.
6. Test representative body types, including a child.
7. Confirm that the boots protect feet only and do not block the field uniform.
8. Confirm that Jaffa equipment and controlled raids remain unaffected.
