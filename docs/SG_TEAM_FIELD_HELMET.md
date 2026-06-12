# Optional SG-team field helmet prototype

Version: `0.2.0-dev-r2`

## Purpose

This milestone adds an optional open-face field helmet for SG-team missions:

```text
SG1_SGTeamFieldHelmet
```

The stranded SG-team starter scenario supplies four helmets in its recovered
equipment crates. They are not auto-equipped.

## Apparel model

```text
ParentName: ArmorHelmetMakeableBase
Layer: Overhead
Coverage: UpperHead
Category: SG1_SGTeamApparel
```

The open-face design leaves facial visibility intact while protecting the
upper head.

## Crafting

```text
25 steel
15 cloth
Gunsmithing
Crafting 4
```

## Protection

```text
ArmorRating_Sharp  0.32
ArmorRating_Blunt  0.18
ArmorRating_Heat   0.10
Mass               0.9
```

The prototype remains deliberately lighter and less protective than Jaffa
helmet equipment.

## Scenario integration

```text
4 SG1_SGTeamFieldHelmet
```

The helmets are starting supplies only. The player decides whether to equip
them.

## Manual test checklist

1. Load RimWorld and check for XML errors.
2. Select `Équipe SG isolée`.
3. Confirm that the scenario supplies list includes four SG-team field helmets.
4. Start a new colony and confirm that four helmet items arrive on the map.
5. Confirm that no starter pawn wears a helmet automatically.
6. Equip one helmet manually and inspect all four facings.
7. Confirm compatibility with the treillis, boots, gloves and tactical vest.
8. Confirm that Jaffa armor and controlled raids remain unaffected.
