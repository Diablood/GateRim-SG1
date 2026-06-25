# Free Jaffa peaceful visitors baseline

Version: `0.2.6-dev`

## Purpose

This milestone adds a low-frequency natural peaceful encounter:

```text
SG1_FreeJaffaPeacefulVisitors
```

Player-facing French label:

```text
visiteurs Jaffa libres pacifiques
```

## Vanilla workflow reuse

The incident worker inherits:

```text
IncidentWorker_VisitorGroup
```

RimWorld therefore handles the peaceful arrival, visitor behavior and
departure through its existing visitor-group workflow.

## Existing world-faction selection

```text
GateRimSG1.Jaffa.FreeJaffaFactionUtility
```

The helper selects one existing `SG1_FreeJaffa` faction that is non-hostile
toward the player.

It deliberately does not create a runtime fallback faction. Old saves without
a Free Jaffa world presence quietly reject the natural incident.

If the player configured multiple Free Jaffa communities during world
creation, one eligible non-hostile faction is selected randomly.

## Storyteller parameters

```text
baseChance: 0.14
earliestDay: 10
minRefireDays: 20
requireColonistsPresent: true
target: Map_PlayerHome
```

## Group size

```text
PointsPerVisitor: 110
Rand.RangeInclusive(2, 4)
```

## Pawn-group profile

The faction now declares:

```text
Peaceful
```

with:

```text
SG1_FreeJaffaWarrior: 4
SG1_FreeJaffaGuard:   1
```

Visitors reuse the validated Free Jaffa baseline: Jaffa lineage, initial
Prim'ta, Ma'Tok, modular armor, retractable helmet, Free Jaffa cultural
backstories and no automatic Goa'uld forehead mark.

## Deferred work

The peaceful visitor incident remains deliberately non-commercial. Ordinary
Free Jaffa trade is provided separately since `0.3.43-dev`.

Still deferred here:

- civilian Free Jaffa profiles;
- diplomatic envoys;
- gifts;
- quests;
- military aid;
- recruitment;
- custom goodwill changes.

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Start a new game with at least one Free Jaffa world faction.
3. Trigger `SG1_FreeJaffaPeacefulVisitors` through developer incident tools.
4. Confirm that the group arrives peacefully.
5. Confirm two to four armed visitors in typical tests.
6. Confirm Free Jaffa warrior and possible guard profiles.
7. Confirm Jaffa lineage, Prim'ta, Ma'Tok, armor and retractable helmet.
8. Confirm absence of automatic Goa'uld forehead marks.
9. Confirm Free Jaffa cultural backstories.
10. Allow the visitors to depart through the vanilla workflow.
11. Configure multiple Free Jaffa communities and repeat.
12. Confirm an eligible non-hostile community is used.
13. Make one Free Jaffa faction hostile and confirm it is excluded.
14. Check `Player.log` for incident errors or missing pawn-group profiles.
