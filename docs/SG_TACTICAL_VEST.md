# SG tactical vest prototype

Version: `0.1.81-dev`

## Purpose

This milestone adds the fourth modular SG-team field-equipment component:

```text
SG1_SGTacticalVest
```

The vest completes the first generic field set together with the olive-drab
uniform, tactical boots and tactical gloves.

## Apparel model

```text
ParentName: ApparelMakeableBase
Layer: Middle
Coverage: Torso, Shoulders
Category: SG1_SGTeamApparel
```

The vest is worn over the `OnSkin` field uniform. It remains compatible with
the boots and gloves because those Middle-layer items cover only feet or
hands.

## Crafting

```text
30 plain leather
35 cloth
Crafting 4
```

Available at:

```text
HandTailoringBench
ElectricTailoringBench
```

## Protection

```text
ArmorRating_Sharp  0.28
ArmorRating_Blunt  0.14
ArmorRating_Heat   0.10
Mass               2.4
```

The prototype represents a load-bearing tactical vest with modest protection,
not heavy ballistic body armor.

## Graphics

Temporary dedicated graphics are included for:

- inventory display;
- east, north, south and west facings;
- default, male, female, thin, fat, hulk and child body variants.

## Completed generic SG-team baseline

```text
SG1_GenericSGTeamUniform
SG1_SGTacticalBoots
SG1_SGTacticalGloves
SG1_SGTacticalVest
```

## Deferred content

Not included yet:

- environment-specific uniform variants;
- role-specific variants;
- automatic SG-team pawn loadouts;
- heavier ballistic or specialist armor;
- final art and semi-realistic wiki concept art.

## Manual test checklist

1. Load RimWorld and check for XML errors.
2. Spawn or craft `SG1_SGTacticalVest`.
3. Confirm the recipe appears at both vanilla tailoring benches.
4. Equip the vest alone and inspect all four facings.
5. Equip the complete four-piece SG-team field set.
6. Test representative body types, including a child.
7. Confirm that the vest protects torso and shoulders without blocking boots
   or gloves.
8. Confirm that Jaffa equipment and controlled raids remain unaffected.
