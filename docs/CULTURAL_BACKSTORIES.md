# Cultural backstory baseline

Version: `0.2.4-dev-r1`

## Scope

This milestone adds `52` native RimWorld `BackstoryDef` entries.

Dedicated histories use:

```text
spawnCategories
requiresSpawnCategory = true
```

Off-world PawnKindDefs use:

```text
backstoryFiltersOverride
categoriesChildhood
categoriesAdulthood
```

## Tau'ri

The SGC expedition keeps broad vanilla-compatible Earth histories. A smaller
filter adds optional SGC adult careers.

## Jaffa

Goa'uld-domain and Free Jaffa share Jaffa childhoods but use separate adult
categories.

## Goa'uld hosts

Generated ordinary hosts and System Lords use off-world human childhoods and
separate Goa'uld adult categories.

## Tok'ra

Generated prototype agents use off-world childhoods and Tok'ra agent careers.
Existing colonists who voluntarily accept implantation keep their original
histories.

## Standard adulthood body types

Every dedicated adulthood story explicitly defines:

```text
bodyTypeMale: Male
bodyTypeFemale: Female
```

RimWorld returns the selected adulthood backstory's body type directly during
pawn generation. Explicit defaults are therefore required to prevent
undefined body types and rendering failures.

## Manual tests

1. Start several `Équipe SG isolée` games and inspect Tau'ri stories.
2. Visit a Goa'uld city and inspect Jaffa, ordinary Goa'uld and the System Lord.
3. Visit a Free Jaffa settlement.
4. Trigger generated Tok'ra visitors or a generated test host.
5. Implant a pre-existing colonist voluntarily and confirm the original
   childhood and adulthood remain unchanged.
6. Check `Player.log` for shuffled-backstory fallback errors.
