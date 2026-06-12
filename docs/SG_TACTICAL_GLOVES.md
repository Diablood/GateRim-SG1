# SG tactical gloves prototype

Version: `0.1.80-dev`

## Purpose

This milestone adds the first separate handwear component for SG-team field
equipment:

```text
SG1_SGTacticalGloves
```

The gloves complement the field uniform and tactical boots without being merged
into either item.

## Apparel model

```text
ParentName: ApparelMakeableBase
Layer: Middle
Coverage: Hands
Category: SG1_SGTeamApparel
```

The prototype protects hands and fingers without becoming a reinforced gauntlet
or blocking the textile field uniform.

## Crafting

```text
20 plain leather
8 cloth
Crafting 3
```

Available at:

```text
HandTailoringBench
ElectricTailoringBench
```

## Protection

```text
ArmorRating_Sharp  0.10
ArmorRating_Blunt  0.07
ArmorRating_Heat   0.09
```

These values remain modest and are not intended to replace armor.

## Graphics

Temporary dedicated graphics are included for:

- inventory display;
- east, north, south and west facings;
- default, male, female, thin, fat, hulk and child body variants.

## Deferred equipment

Not included yet:

- SG tactical vest;
- environment-specific uniform variants;
- automatic SG-team pawn loadouts;
- final art and semi-realistic wiki concept art.

## Manual test checklist

1. Load RimWorld and check for XML errors.
2. Spawn or craft `SG1_SGTacticalGloves`.
3. Confirm the recipe appears at both vanilla tailoring benches.
4. Equip the gloves alone and inspect all four facings.
5. Equip the gloves together with the field uniform and tactical boots.
6. Test representative body types, including a child.
7. Confirm that the gloves protect hands and fingers and do not block the field uniform.
8. Confirm that Jaffa equipment and controlled raids remain unaffected.
