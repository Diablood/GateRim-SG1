# Stranded SG-team starter scenario

Version: `0.2.8-dev-r1`

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
4 player starters aged 20+ with adulthood backstories
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

The same scenario part rejects candidates below `20` biological years or
incapable of violence through `AllowPlayerStartingPawn(...)`. The age threshold
ensures that every accepted candidate has an adulthood backstory. The check is
applied to every newly generated candidate, so all four slots remain
regenerable while the initial team stays adult and combat-capable. Specialist
backgrounds remain available when they do not disable combat.

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

Since `0.2.3-dev-r1`, the custom faction uses one broad vanilla-compatible
backstory filter. Tau'ri starters keep access to coherent Earth-origin vanilla
histories until dedicated SGC additions are introduced. This removes the
temporary `No shuffled Childhood` and `No shuffled Adulthood` fallback logs.

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

## Optional field helmets

```text
4 SG1_SGTeamFieldHelmet
```

The helmets arrive as supplies and are deliberately not auto-equipped.

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

Enabled since later `0.2.x` milestones:

- Goa'uld world settlements;
- rare natural Goa'uld direct-assault raids;
- Free Jaffa world presence.

Still deferred:

- Tok'ra world-presence balancing;
- natural acquisition of Ma'Tok and Zat'nik'tel weapons;
- a functional Stargate.

## Manual test checklist

Validation result for `0.2.8-dev-r1`: passed in game, including repeated
regeneration of all four candidate slots.

1. Rebuild the assembly with `-t:Rebuild`.
2. Launch RimWorld without enabling developer mode.
3. Select `Équipe SG isolée` from the new-game scenario list.
4. Confirm that the scenario summary displays the dedicated `expédition du SGC` faction and the expected supplies.
5. Confirm that the narrative text field is visibly populated in the scenario editor.
6. Generate the world and inspect the pawn-selection page.
7. Confirm that exactly four candidates are available, each is at least `20`
   biological years old, has an adulthood backstory and is capable of violence.
8. Regenerate each candidate slot repeatedly and confirm the same age and
   violence-capability requirements are preserved.
9. Confirm that every generated candidate wears the complete olive-drab SG-team field set.
10. Start the map and confirm that four player pawns arrive standing on the map.
11. Confirm that the translated narrative introduction opens automatically.
12. Confirm the presence of three assault rifles, one pump shotgun, four cloth
   bedrolls, four unequipped SG-team field helmets and all emergency supplies.
13. Confirm that no tailoring bench is supplied.
14. Save and reload the new colony.
15. Confirm that the existing Jaffa developer raid tests still work when
    developer mode is enabled for regression testing.
