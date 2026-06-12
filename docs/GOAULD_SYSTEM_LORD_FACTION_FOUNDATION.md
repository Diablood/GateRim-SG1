# Goa'uld System Lord world-faction baseline

Version: `0.2.1-dev-r3`

## Scope

The former hidden technical foundation now becomes the first playable hostile
world presence:

```text
SG1_GoauldSystemLordPrototype
```

The single RimWorld faction is a practical abstraction representing several
Goa'uld System Lord domains.

## World-generation behavior

```text
hidden: false
requiredCountAtGameStart: 1
maxConfigurableAtWorldCreation: 1
startingCountAtWorldCreation: 1
displayInFactionSelection: true
settlementGenerationWeight: 0.35
permanentEnemy: true
```

New worlds therefore contain one visible hostile Goa'uld faction with a
limited number of settlements.

## World-map support

The faction defines:

```text
factionIconPath: World/WorldObjects/Expanding/PirateOutpost
settlementTexturePath: World/WorldObjects/DefaultSettlement
factionNameMaker: NamerFactionPirate
settlementNameMaker: NamerSettlementPirate
gold-toned colorSpectrum
```

The invalid `FactionDef` fields `expandingIconTexture` and `homeIconPath`
were removed in `0.2.1-dev-r1`. Their valid RimWorld 1.6 replacements,
`factionIconPath` and `settlementTexturePath`, are used since `0.2.1-dev-r2`.

## Pawn-group profiles

```text
Combat
Settlement
```

Both profiles currently use:

```text
SG1_GoauldJaffaWarrior
SG1_GoauldJaffaGuard
```

The `Settlement` profile ensures that visible world bases have a first
defender pool if they are visited or attacked.

## Controlled raid policy

The faction deliberately keeps:

```text
raidsForbidden: true
canSiege: false
canStageAttacks: false
```

This blocks generic vanilla faction selection. Natural Goa'uld raids use the
dedicated low-frequency `SG1_GoauldJaffaNaturalRaid` incident and explicit
`ImmediateAttack` workflow.

Natural abduction and destruction raids remain deferred.

## Save compatibility

New worlds receive the faction during normal generation.

The shared utility still creates a visible runtime fallback when controlled
developer incidents are used in an older save without the faction. That
fallback has no retroactively generated settlements.

## Manual test checklist

1. Open Create World and confirm that `Domaines des Grands Maîtres Goa'uld` appears exactly once by default.
2. Confirm one visible hostile Goa'uld faction in the faction list.
3. Confirm multiple limited Goa'uld settlements on the world map.
4. Inspect the gold-toned faction color and default settlement rendering.
5. Confirm permanent hostility to the SGC expedition.
6. Validate the dedicated natural direct-assault incident.
7. Validate the three controlled developer incidents.
