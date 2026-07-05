# Stargate crafting-research baseline

Version: `0.2.8-dev`

## Purpose

This milestone adds a dedicated GateRim SG-1 research tab and prevents local
manufacturing of Stargate equipment from being available immediately after
basic vanilla workbench construction.

The research layer gates local reproduction only. Captured, gifted and
scenario-supplied objects remain usable.

## Research tab

```text
SG1_GateRimResearch
```

Player-facing label:

```text
GateRim SG-1
```

## Projects

### Jaffa weaponry

```text
SG1_JaffaWeaponry
baseCost: 900
prerequisite: Gunsmithing
```

Unlocks local crafting of:

```text
SG1_MatokStaff
SG1_ZatnikTel
```

### Jaffa armor

```text
SG1_JaffaArmor
baseCost: 1100
prerequisite: FlakArmor
```

Unlocks local crafting of:

```text
SG1_JaffaLightArmor
SG1_JaffaHeavyArmor
SG1_JaffaGauntlets
SG1_JaffaReinforcedBoots
SG1_JaffaDeployedHelmet
SG1_JaffaOfficerArmor
SG1_JaffaOfficerDeployedHelmet
```

Both retracted helmets remain internal visual states and are not crafted
separately. Captured officer equipment stays wearable before research.

### Kara kesh

```text
SG1_KaraKeshResearch
baseCost: 3000
prerequisite: SG1_JaffaArmor
```

Unlocks local crafting of `SG1_KaraKesh`. Captured devices remain usable by a
wearer carrying persistent biological naquadah traces.

### SGC field equipment

```text
SG1_SGFieldEquipment
baseCost: 600
prerequisite: ComplexClothing
```

Unlocks local crafting of:

```text
SG1_GenericSGTeamUniform
SG1_BlackSGTeamUniform
SG1_DesertSGTeamUniform
SG1_SGTacticalBoots
SG1_SGTacticalGloves
SG1_SGTacticalVest
SG1_SGTeamFieldHelmet
```

### Goa'uld biotechnology

```text
SG1_GoauldBiotechnology
baseCost: 1200
prerequisite: DrugProduction
```

Unlocks local preparation or construction of:

```text
SG1_PrepareTretoninDoses
SG1_IncubatePrimtaLarva
SG1_PrimtaIncubationBasin
SG1_PrimtaPreservationBasin
SG1_GoauldRitualBasin
```

### Goa'uld healing devices

```text
SG1_GoauldHealingBraceletResearch
baseCost: 4000
prerequisites: SG1_GoauldBiotechnology + SG1_KaraKeshResearch
```

Unlocks local crafting of `SG1_GoauldHealingBracelet`. The project combines
biotechnology and reverse-engineered naquadah interfaces without adding any
medical function to the kara kesh itself.

## Explicit non-goals

The project does not block:

```text
using captured Ma'Tok or Zat'nik'tel weapons
wearing captured Jaffa armor
wearing a captured kara kesh or Goa'uld healing bracelet
wearing scenario-supplied SG equipment
administering existing tretonin doses
implanting existing Prim'ta larvae
preserving inventory resources by ordinary refrigeration
```

## Manual test checklist

Static preflight completed on `validation/stargate-crafting-research`:

```text
4 vanilla prerequisites resolved
4 GateRim projects parsed
19 production gates mapped
70 gameplay Def XML files parsed
89 French translation XML files parsed
no GateRim research loading error in Player.log
```

Use this isolated active mod list:

```text
Core
Biotech
GateRim SG-1
```

Validation result: passed in game. The dedicated tab, prerequisites, pre-research
locks, post-research unlocks, continued use of existing equipment and log check
all behaved as documented.

1. Start a fresh `Équipe SG isolée` game.
2. Open the research window and confirm the `GateRim SG-1` tab.
3. Confirm all four projects are visible.
4. Confirm their vanilla prerequisites:
   - Gunsmithing;
   - Flak armor;
   - Complex clothing;
   - Drug production.
5. Before completing custom research:
   - confirm captured Ma'Tok and armor can still be equipped;
   - confirm scenario SG equipment remains usable;
   - confirm tretonin doses can still be administered;
   - confirm Prim'ta larvae can still be implanted;
   - confirm gated local crafting bills and basin designations are unavailable.
6. Complete `SG1_JaffaWeaponry`.
7. Confirm Ma'Tok and Zat'nik'tel bills appear at the machining table.
8. Complete `SG1_JaffaArmor`.
9. Confirm Jaffa armor-component bills appear.
10. Complete `SG1_SGFieldEquipment`.
11. Confirm SG-team clothing and mission-equipment bills appear.
12. Complete `SG1_GoauldBiotechnology`.
13. Confirm tretonin preparation, basin construction and assisted Prim'ta
    maturation are available.
14. Check `Player.log` for unresolved research cross-references.
