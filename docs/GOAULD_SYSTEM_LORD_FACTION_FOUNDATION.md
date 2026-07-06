# Goa'uld System Lord world-faction baseline

Version: `0.3.84-dev`

## Scope

The former hidden technical foundation now becomes the first playable hostile
world presence:

```text
SG1_GoauldSystemLordPrototype
```

Each generated RimWorld faction instance represents one distinct Goa'uld System
Lord domain. New worlds now propose three instances by default to support the
strategic layer without consuming an unbounded share of vanilla's faction limit.

## World-generation behavior

```text
hidden: false
requiredCountAtGameStart: 1
maxConfigurableAtWorldCreation: 9999
startingCountAtWorldCreation: 3
displayInFactionSelection: true
settlementGenerationWeight: 0.35
permanentEnemyToEveryoneExcept: SG1_GoauldSystemLordPrototype
```

New worlds therefore propose three visible hostile Goa'uld faction instances,
each with a limited number of settlements. The ordinary vanilla faction list
remains authoritative: players may reduce the count to the required single
baseline when total faction limits matter, or add more instances manually.

The territorial safeguard layer requires at least two non-defeated Goa'uld
factions that each own a permanent settlement. Reducing the world to one domain
does not create a replacement faction; it only suspends territorial strategy.

From `0.3.84-dev`, an exact pair in open conflict may transfer ownership of one
eligible permanent settlement after a guarded delay. The same settlement object,
name, ID and tile remain. The last settlement of a domain, sparse worlds, loaded
maps, player presence, active quest targets and projected ownership above `75%`
with two domains or `50%` with three or more remain protected, and no faction or
settlement is created or destroyed.

The selective permanent-enemy rule keeps each domain permanently hostile to the
player and every outside faction, but makes another instance of
`SG1_GoauldSystemLordPrototype` the sole exception. Inter-domain `Neutral`,
`Hostile` and `Ally` relations can therefore be driven by vanilla goodwill
without weakening the hostile player-facing contract.

## Provisional xenotype summary

```text
SG1_Jaffa: 100%
```

The current world-faction baseline generates Jaffa servants only. The
faction-level `xenotypeSet` now reflects that reality in the Create World
summary instead of falling back to human baseliners.

True Goa'uld hosts remain deferred. Adding `SG1_GoauldHost` to this summary
before connecting persistent symbiote initialization would generate
incomplete hosts that do not participate correctly in implantation,
extraction or identity-transfer mechanics.

## World-map support

The faction defines:

```text
factionIconPath: World/WorldObjects/Expanding/SG1_GoauldSystemLords
settlementTexturePath: World/WorldObjects/DefaultSettlement
factionNameMaker: SG1_NamerFactionGoauldDomain
settlementNameMaker: SG1_NamerSettlementGoauldDomain
gold-toned colorSpectrum
```

The invalid `FactionDef` fields `expandingIconTexture` and `homeIconPath`
were removed in `0.2.1-dev-r1`. Their valid RimWorld 1.6 replacements,
`factionIconPath` and `settlementTexturePath`, are used since `0.2.1-dev-r2`.

Since `0.3.46-dev-r1`, newly generated faction instances and settlements use
dedicated bilingual Goa'uld RulePackDefs instead of a shared fixed faction
name or vanilla pirate place names. Existing serialized names are not migrated.

Since `0.3.50-dev`, the world-faction row uses a dedicated Goa'uld pyramid and serpent silhouette while retaining RimWorld's gold faction-color tinting. The icon texture is white/alpha rather than precolored so manually added duplicate domains can still receive the vanilla lighter or darker tint variations.

## Pawn-group profiles

```text
Combat
Settlement
SG1_TokraDiversionAssault (mission-only)
```

The `Combat` profile remains Jaffa-only:

```text
SG1_GoauldJaffaWarrior
SG1_GoauldJaffaGuard
```

The `Settlement` profile uses dedicated capped variants:

```text
SG1_GoauldSettlementJaffaWarrior: weight 4, max 7
SG1_GoauldSettlementJaffaGuard:   weight 2, max 2
SG1_GoauldHostCaste:              weight 1, max 1
```

These are per-generated-group caps. A full settlement map may resolve more
than one group, so a larger city can contain two persistent ordinary Goa'uld
hosts while keeping the caste minoritarian.

Direct raids therefore remain Jaffa-only while visited domains reliably
contain persistent Goa'uld hosts.

Since `0.3.34-dev`, the mission-only `SG1_TokraDiversionAssault` profile adds
`SG1_GoauldJaffaBreacher` beside the standard warrior and guard. The diversion
incident selects this group explicitly because RimWorld's immediate-breaching
strategy requires a pawn kind marked `isGoodBreacher`. Normal incidents still
request `Combat`, so this profile does not alter ordinary raid composition.

## Goa'uld System Lord leader

```text
basicMemberKind: SG1_GoauldJaffaWarrior
fixedLeaderKinds: SG1_GoauldSystemLordHost
leaderForceGenerateNewPawn: true
leaderTitle: System Lord
```

Since `0.2.3-dev`, visible domains generate a true persistent Goa'uld host as
their leader. The host body remains biologically human and receives an active
adult-symbiote Hediff with persistent identity.

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

New worlds receive the editable three-instance default during normal generation.
Existing saves are not given two extra factions or retroactive settlements. The
shared utility may still create one visible runtime fallback when a controlled
developer incident is used in an older save without any Goa'uld faction; that
fallback has no retroactively generated settlement and therefore does not count
as an active territorial domain.

`0.3.83-dev` dynamically reconciles active domains from actual permanent
settlements. It never repairs a reduced configuration by creating a hidden
replacement domain. `0.3.84-dev` clears legacy dry-run cooldowns, arms a future
natural attempt on older saves instead of applying a takeover immediately and
cancels any pending dry-run reservation without changing ownership.

## Manual test checklist

1. Open Create World and confirm that three
   `Domaines des Grands Maîtres Goa'uld` entries are proposed by default.
2. Reduce the count with vanilla controls and confirm no hidden replacement is
   added.
3. For the full strategic test, restore three entries and generate the world.
4. Confirm each generated instance receives a distinct domain name, leader,
   color variation and limited permanent settlements.
5. Confirm that the faction summary displays `Jaffa: 100%` provisionally.
6. Confirm permanent hostility to the SGC expedition.
7. Under `Commandement SG-1`, confirm that two or more domains with settlements
   are reported as active territorial domains.
8. Confirm that one active domain suspends only territorial strategy.
9. Confirm that no `Faction leader for Domaines des Grands Maîtres Goa'uld is null`
   log appears.
10. Run the exact diplomatic and bounded-takeover procedure in
    `docs/TESTING_CURRENT.md`, including save/reload before and after the owner
    change.
