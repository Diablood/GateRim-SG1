# Goa'uld open-conflict battlefields

Status: local form published in `0.3.69-dev`; world-site form validated in final revision `0.3.70-dev-r4`.

## Purpose

An `OpenConflict` relation must be visible as more than an RP report or a raid
point modifier. Battlefields show two exact Goa'uld domains spending military
forces against one another while keeping player intervention optional.

The subsystem has two presentation forms:

- a local incident near a player colony;
- a temporary world site visitable by caravan.

Both forms use one orchestration slot and one combat implementation.

## Shared orchestration

`GameComponent_GoauldOpenConflictBattlefieldTracker` owns all scheduling.
Version 2 stores:

- one hidden opportunity clock;
- one active local-map ID;
- one active world-object ID;
- the previous exact domain pair;
- the previous form, local or world;
- RP text anti-repetition;
- storyteller suspension state.

The initial delay remains `480000–960000` ticks and recurrence remains
`1200000–2400000` ticks. A recurrence delay begins only after the active local
battle or world site is completely removed.

When both forms are eligible, the scheduler selects the form not used by the
previous occurrence. The first choice is random. If the preferred form cannot be
created, the scheduler attempts the other form without consuming another full
recurrence period.

## Pair eligibility

Both forms require:

- at least two active Goa'uld domain faction instances;
- one stored unordered pair in `OpenConflict`;
- both factions still active and undefeated;
- Commandement SG-1 for natural creation.

The previous pair is avoided whenever another eligible pair exists.

## Difficulty snapshot

Both detachments use the same offer-time calculation:

```text
clamp(vanilla storyteller points × 0.35, 250, 1800)
```

The value is captured when the local event or world site is created. Entering a
world site later does not reroll its strength from a newer colony state.

Pawn generation remains bounded to the published Jaffa warrior/guard mixture and
uses the exact two stored domain factions.

## Local form

The local form is selected only on a player home map with free colonists and no
active hostile force. Both detachments enter from map-edge cells, move to
opposing rally points, wait for sufficient formation or the bounded fallback,
then begin an announced mutual assault.

Its letter targets a spawned Jaffa on the local map.

## World-site form

`GoauldOpenConflictBattlefieldWorldSiteUtility` places
`SG1_GoauldOpenConflictBattlefieldSite` `6–18` tiles from an eligible player home
map. The world object is factionless because it represents contested ground
rather than ownership by one participant.

The site stores:

- both exact faction references;
- vanilla threat snapshot;
- points per detachment;
- eight-day expiration tick;
- encounter map size;
- launched and resolved state.

The dedicated icon is:

```text
World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield
```

Ignoring the marker destroys it silently at the deadline. This is not a quest
failure and causes no political or material consequence.

## Caravan flow

`CaravanArrivalAction_GoauldOpenConflictBattlefieldSite` uses RimWorld's normal
world-path and arrival-action framework. On arrival:

1. the map is generated lazily at size `140 × 140`;
2. the exact stored factions and threat snapshot initialize the shared map
   component;
3. the hostile-map generation notification is sent;
4. the caravan enters from an edge at least `30` cells from both rally anchors;
5. the local rally and combat lifecycle proceeds unchanged.

A second player caravan may enter an already generated unresolved map through
the same world object.

## Shared map component

`MapComponent_GoauldOpenConflictBattlefield` now accepts an optional parent
world site. The combat state remains identical in both forms:

- edge arrival and rally;
- assault announcement;
- ranged movement to firing range and line of sight;
- melee pursuit;
- player-provocation tracking per camp;
- `1800` quiet-tick retaliation expiry;
- `35`-cell pursuit boundary;
- one-sided `30%` morale break;
- two-day absolute battle limit;
- `6000`-tick maximum withdrawal retaliation;
- fixed `30000`-tick forced-exit grace.

A local battle notifies the global tracker directly when resolved. A world-site
battle notifies its parent object, which retains ownership of the shared slot
until RimWorld can remove the map and world object.

## Resolution and cleanup

A world encounter is considered tactically resolved when the map component
finishes withdrawal. The map is not removed while:

- any player pawn blocks removal;
- a caravan still needs reformation;
- an incoming transporter blocks removal.

When ordinary RimWorld removal becomes valid, the world object notifies the
tracker, the map and marker are removed together, and one recurrence delay is
scheduled.

Downed pawns, prisoners, corpses and equipment are not deleted by the battlefield
logic. The player may capture and recover them through normal map and caravan
flows.

## Neutral consequences

Neither form changes:

- strategic inter-domain relation state;
- goodwill toward the player;
- settlement ownership or existence;
- territory;
- raid doctrines or frequency;
- the `0.75` natural-raid pressure factor;
- Tok'ra trust or mission state.

No framework reward is generated. Battlefield equipment is ordinary pawn gear.

## Save compatibility

The tracker schema increases from 1 to 2. Existing `0.3.69-dev` saves load with:

- no active world-object ID;
- no previous world/local alternation unless a new event occurs;
- the existing active local-map ID preserved;
- the existing opportunity timing and pair history preserved.

The world object and optional parent reference are fully serialized for saves
before arrival, during combat and after tactical resolution.

## Developer diagnostics

Path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

Actions:

- show the shared battlefield report;
- make the shared opportunity due;
- force a local battlefield;
- force a world battlefield site;
- expire an unvisited world site;
- order active-map withdrawal;
- reset scheduler history and timing.

The report must identify the active form, exact pair, local map ID, world-object
ID, site expiration, map-generation state and the single shared recurrence
clock.

## Validation boundary

`0.3.70-dev` validates only the optional world representation and shared
orchestration. Alliance bonuses, joint raids, territorial expansion, settlement
destruction and diplomatic consequences remain outside this milestone.

## Final revision r4

- World-site inspection text is RP-facing and formats long durations in days and hours.
- Player intervention is treated as an additional hostile group during the mutual assault; a proportional nearby subset retaliates while the remaining Jaffa keep fighting the rival domain.
- The world object uses RimWorld's vanilla `WorldObjectCompProperties_FormCaravan`, restoring normal caravan reformation once active hostile threats are gone.
