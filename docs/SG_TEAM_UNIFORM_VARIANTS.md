# Black and desert SG-team uniform variants

Version: `0.1.82-dev`

## Purpose

This milestone adds two visually distinct alternatives to the existing
olive-drab SG-team field uniform:

```text
SG1_BlackSGTeamUniform
SG1_DesertSGTeamUniform
```

The variants remain purely visual. They intentionally share the standard
uniform's statistics, crafting cost, layer and body-part coverage.

## Uniform list

| DefName | Player-facing role |
|---|---|
| `SG1_GenericSGTeamUniform` | Standard olive-drab field uniform |
| `SG1_BlackSGTeamUniform` | Tactical and low-visibility missions |
| `SG1_DesertSGTeamUniform` | Arid missions |

## Shared apparel model

```text
ParentName: ApparelMakeableBase
Layer: OnSkin
Coverage: Torso, Shoulders, Arms, Legs
Category: SG1_SGTeamApparel
Cost: 80 Cloth
Crafting: 3
```

Available at:

```text
HandTailoringBench
ElectricTailoringBench
```

## Graphics

The temporary black and desert textures are recolored from the validated
olive-drab shapes. This keeps body proportions, facings and equipment-layer
compatibility identical across variants.

Each new variant includes:

- inventory display;
- east, north, south and west facings;
- default, male, female, thin, fat, hulk and child body variants.

## Intentionally omitted variants

Forest camouflage is not included in this pass. At RimWorld scale, it would
remain too close to the olive-drab baseline to justify a full duplicate texture
set and separate recipe.

A sand-colored tactical vest remains optional. It should be added only if the
existing black vest looks distracting with the desert uniform during in-game
tests.

## Manual test checklist

1. Load RimWorld and check for XML errors.
2. Spawn or craft all three SG-team field uniforms.
3. Confirm that black and desert recipes appear at both vanilla tailoring benches.
4. Confirm the same `80 Cloth` cost and `Crafting 3` requirement.
5. Equip the black uniform and inspect all four facings.
6. Equip the desert uniform and inspect all four facings.
7. Test representative body types, including a child.
8. Equip each variant with SG tactical boots, gloves and vest.
9. Review the desert uniform with the existing black vest and decide whether a
   sand-colored vest would add enough visual value.
10. Confirm that Jaffa equipment and controlled raids remain unaffected.
