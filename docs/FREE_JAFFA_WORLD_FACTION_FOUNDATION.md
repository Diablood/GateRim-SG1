# Free Jaffa world-faction baseline

Version: `0.2.6-dev`

## Scope

This milestone adds the second visible Stargate world faction:

```text
SG1_FreeJaffa
```

Player-facing French name:

```text
Jaffa libres
```

The faction represents independent Jaffa communities freed from Goa'uld
domination.

## World-generation behavior

```text
hidden: false
requiredCountAtGameStart: 1
maxConfigurableAtWorldCreation: 9999
startingCountAtWorldCreation: 1
displayInFactionSelection: true
settlementGenerationWeight: 0.25
permanentEnemy: false
naturalEnemy: false
```

One Free Jaffa faction therefore appears by default in RimWorld 1.6 world
creation and generates a limited number of visible settlements. Players may
add additional Free Jaffa factions manually from Create World.

## Xenotype summary

```text
SG1_Jaffa: 100%
```

The faction-level `xenotypeSet` keeps the Create World summary aligned with
the actual Free Jaffa population and prevents RimWorld from displaying the
misleading vanilla human-baseliner fallback.

## Relations

The faction does not declare natural or permanent hostility toward the player.
Its initial SGC relation therefore uses RimWorld's neutral baseline.

The Goa'uld faction remains a permanent enemy. Its own rule makes relations
between Goa'uld domains and Free Jaffa hostile without adding Free Jaffa raids
against the player.

## World-map presentation

```text
factionIconPath: World/WorldObjects/Expanding/Town
settlementTexturePath: World/WorldObjects/DefaultSettlement
green-toned colorSpectrum
```

## Historical baseline restrictions (`0.2.2-dev`)

```text
raidsForbidden: true
canSiege: false
canStageAttacks: false
canRequestTraders: false
canRequestMilitaryAid: false
canGenerateQuestSites: false
```

Rare peaceful visitors are enabled since `0.2.6-dev`.

Trade was intentionally deferred in this foundation milestone. Ordinary vanilla
trade is added later by `0.3.43-dev`, which changes the current faction state to:

```text
canRequestTraders: true
```

Military aid is added later by `0.3.44-dev`, which changes the current faction state to:

```text
canRequestMilitaryAid: true
```

The request remains restricted to an allied faction and uses RimWorld's ordinary communications-console, goodwill, cooldown and `Combat` pawn-group flow. Quests remain deferred.

## Provisional faction leader

```text
fixedLeaderKinds: SG1_FreeJaffaGuard
leaderForceGenerateNewPawn: true
```

A visible humanlike faction needs a leader. The baseline therefore generates a
Free Jaffa guard as its initial leader.

## Pawn-group profiles

```text
Combat
Settlement
Peaceful
```

All three use:

```text
SG1_FreeJaffaWarrior
SG1_FreeJaffaGuard
```

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Open RimWorld world creation.
3. Confirm that `Jaffa libres` appears once by default.
4. Confirm that the faction summary displays `Jaffa: 100%`.
5. Add at least one extra Free Jaffa faction manually.
6. Generate a new world.
7. Confirm limited green-toned Free Jaffa settlements.
8. Confirm neutral relations with `expédition du SGC`.
9. Confirm hostile relations with `Domaines des Grands Maîtres Goa'uld`.
10. Visit or attack a Free Jaffa settlement on a disposable test save.
11. Confirm Free Jaffa warrior and guard defenders.
12. Confirm Ma'Tok, modular armor, automatic Prim'ta and no forced forehead mark.
13. Confirm that no `Faction leader for Jaffa libres is null` log appears.
14. Trigger `SG1_FreeJaffaPeacefulVisitors` and confirm a peaceful armed visitor group.
15. Save and reload.
