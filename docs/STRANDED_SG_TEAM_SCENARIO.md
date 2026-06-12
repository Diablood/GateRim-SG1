# Stranded SG-team starter scenario

Version: `0.2.0-dev`

## Purpose

This milestone starts the first playable no-gate phase of GateRim SG-1.

The selectable scenario is:

```text
SG1_StrandedSGTeam
```

Player-facing French name:

```text
Équipe SG isolée
```

## Narrative

A reconnaissance mission went wrong. The local Stargate became unusable
before extraction and contact with the SGC is impossible. A four-member SG
team must establish a camp with recovered field equipment and emergency
supplies.

## Starter pawns

```text
4 adult player starters
4 candidate pawns during selection
Violence-capable starters only
Standing map arrival
```

The custom scenario part:

```text
GateRimSG1.Scenarios.ScenPart_SGTeamStartingGear
```

uses `Notify_PawnGenerated(...)` for `PlayerStarter` pawns and equips:

```text
SG1_GenericSGTeamUniform
SG1_SGTacticalBoots
SG1_SGTacticalGloves
SG1_SGTacticalVest
```

Existing randomly generated clothing is removed first so every candidate and
selected starter visually represents an SG-team member.

The same scenario part rejects candidates incapable of violence through
`AllowPlayerStartingPawn(...)`. This keeps the initial four-person team
combat-capable while still allowing specialist backgrounds that do not disable
combat.

## Player faction

```text
SG1_PlayerSGCExpedition
```

Player-facing French name:

```text
expédition du SGC
```

This replaces the vanilla `New Arrivals` identity for the scenario while
retaining an industrial player-faction foundation.

## Launch narrative

```text
GateRimSG1.Scenarios.ScenPart_TranslatedGameStartDialog
```

The dedicated scenario part resolves the keyed narrative both in the scenario
editor and when the map starts. It replaces the visually blank vanilla
`textKey`-only field used in the first prototype.

## Starting weapons

```text
3 Gun_AssaultRifle
1 Gun_PumpShotgun
```

These vanilla weapons are temporary Tau'ri placeholders and remain supplies
for the player to distribute.

## Bivouac equipment

```text
4 Bedroll
stuff: Cloth
```

No preconstructed tailoring bench is provided.

## Emergency-supply crates

```text
30 MealSurvivalPack
20 MedicineIndustrial
300 Steel
150 WoodLog
20 ComponentIndustrial
120 Cloth
80 Leather_Plain
```

## Intentionally deferred

Not enabled yet:

- Goa'uld world settlements;
- natural Goa'uld raids;
- Free Jaffa world presence;
- Tok'ra world-presence balancing;
- natural acquisition of Ma'Tok and Zat'nik'tel weapons;
- a functional Stargate.

## Manual test checklist

1. Rebuild the assembly with `-t:Rebuild`.
2. Launch RimWorld without enabling developer mode.
3. Select `Équipe SG isolée` from the new-game scenario list.
4. Confirm that the scenario summary displays the dedicated `expédition du SGC` faction and the expected supplies.
5. Confirm that the narrative text field is visibly populated in the scenario editor.
6. Generate the world and inspect the pawn-selection page.
7. Confirm that exactly four adult candidates are available and that none is incapable of violence.
8. Confirm that every generated candidate wears the complete olive-drab SG-team field set.
9. Start the map and confirm that four player pawns arrive standing on the map.
10. Confirm that the translated narrative introduction opens automatically.
11. Confirm the presence of three assault rifles, one pump shotgun, four cloth
   bedrolls and all emergency supplies.
12. Confirm that no tailoring bench is supplied.
13. Save and reload the new colony.
14. Confirm that the existing Jaffa developer raid tests still work when
    developer mode is enabled for regression testing.
