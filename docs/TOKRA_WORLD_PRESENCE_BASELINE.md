# Hidden Tok'ra world-presence baseline

Version: `0.2.7-dev`

## Purpose

This milestone promotes the Tok'ra faction from lazy incident-time creation to
one persistent hidden world-faction anchor:

```text
SG1_Tokra
```

The Tok'ra remain clandestine. The world presence exists for save identity,
incident reuse and later expansion, not for territorial settlement placement.

## New-world generation

```text
hidden: true
requiredCountAtGameStart: 1
maxCountAtGameStart: 1
settlementGenerationWeight: 0
```

New worlds therefore receive one saved Tok'ra faction instance without
creating a settlement.

The faction does not declare:

```text
maxConfigurableAtWorldCreation
startingCountAtWorldCreation
displayInFactionSelection
```

It remains absent from the normal faction-selection screen.

## Disabled territorial systems

```text
raidsForbidden: true
canSiege: false
canStageAttacks: false
canRequestTraders: false
canRequestMilitaryAid: false
canGenerateQuestSites: false
```

Future hidden cells or quest sites must be added through dedicated systems
rather than normal settlement generation.

## Old-save migration

```text
GameComponent_TokraWorldPresenceInitializer
```

The component checks:

```text
StartedNewGame
LoadedGame
every 600 ticks as a retry
```

If the save has no `SG1_Tokra` faction, it creates one hidden persistent
instance through:

```text
TokraFactionUtility.GetOrCreatePersistentFaction(...)
```

Existing saves that already created a Tok'ra faction through an earlier
visitor or medical incident reuse that faction without duplication.

## Incident reuse

The existing incidents now reuse the persistent faction:

```text
SG1_TokraPeacefulVisitors
SG1_TokraTherapeuticOpportunity
SG1_TokraMedicalSupportDelivery
```

## Internal leader consistency

Faction generation may create an internal hidden leader. The historical
initializer:

```text
GameComponent_TokraHostPrototypeInitializer
```

now also scans that faction leader and attaches one active persistent Tok'ra
symbiote identity when appropriate.

## Manual test checklist

1. Rebuild with `-t:Rebuild`.
2. Start a fresh game.
3. Use developer faction logs and confirm exactly one hidden `Tok'ra` faction.
4. Confirm no Tok'ra settlement appears on the world map.
5. Confirm Tok'ra do not appear in configurable world-creation lists.
6. Trigger peaceful Tok'ra visitors.
7. Confirm the existing persistent faction is reused.
8. Trigger a therapeutic opportunity with an eligible sick colon.
9. Confirm the same faction escorts the offer.
10. Raise trust and trigger a medical-support delivery.
11. Confirm the same faction is reused.
12. Save and reload.
13. Repeat incident tests and confirm no duplicate faction.
14. Load a save created before `0.2.7-dev`.
15. Wait up to `600` ticks.
16. Confirm one hidden Tok'ra faction is added automatically.
17. Confirm no new settlement or raid appears.
