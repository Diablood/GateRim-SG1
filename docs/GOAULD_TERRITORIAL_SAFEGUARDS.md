# Goa'uld territorial strategic safeguards

## Milestone

- Version: `0.3.83-dev`
- Branch: `feature/goauld-territorial-strategic-safeguards`
- Target assembly: `0.3.83.0`
- Status: final revision `r3`, validated and published as `v0.3.83-dev`.

## Purpose

This milestone establishes one reusable safety boundary before any Goa'uld
settlement can change owner, disappear or be added by strategic simulation.
It also reconciles GateRim inter-domain diplomacy with RimWorld's vanilla
faction relation kind.

The milestone is deliberately a dry-run foundation. It can reserve and evaluate
one exact candidate, persist it, suspend its clocks and complete its validation,
but it never mutates a settlement or creates a territorial consequence.

Revision `r2` corrected the C# definite-assignment failure found during the
first `r1` build. The first `r2` launch then exposed RimWorld's rejection of
`SetRelationDirect` for goodwill-based factions. Revision `r3` preserves
permanent hostility toward every outside faction while allowing only two
Goa'uld instances to change vanilla goodwill. No territorial safeguard rule or
persisted state changed.

## World-generation baseline

`SG1_GoauldSystemLordPrototype` now proposes three faction instances in the
vanilla world-faction list:

```text
requiredCountAtGameStart: 1
startingCountAtWorldCreation: 3
maxConfigurableAtWorldCreation: 9999
```

Three domains provide the intended SG-1 strategic baseline without consuming an
unbounded share of the vanilla faction limit. The player may reduce the count in
the ordinary world-creation interface. The mod does not add a hidden replacement
domain for the territorial system.

The territorial layer is available with at least two active territorial
domains. A domain is territorial only when it is:

- a Goa'uld System Lord faction instance;
- not defeated;
- owner of at least one permanent vanilla `Settlement` world object.

With zero or one active territorial domain, only the territorial simulation is
suspended. Existing Goa'uld raids, missions and non-territorial systems retain
their own contracts.

## Permanent-settlement scope

The evaluator counts only vanilla `Settlement` world objects owned by a Goa'uld
System Lord faction. It therefore excludes by construction:

- player settlements;
- non-Goa'uld settlements;
- mission sites;
- temporary world objects;
- battlefields;
- caravans and travelling groups.

No settlement lookup is based on labels or tiles. A pending dry-run reservation
stores the exact world-object ID and exact gaining and losing faction references.

## Sparse-world protection

A hostile transfer can be considered only when the world contains at least:

```text
active territorial domains + 2 permanent Goa'uld settlements
```

This keeps two surplus settlements beyond the protected one-per-domain floor.
Examples:

- two active domains require at least four permanent Goa'uld settlements;
- three active domains require at least five;
- four active domains require at least six.

The losing domain must still retain at least one permanent settlement after the
simulated transfer. Its final settlement is always protected.

## Minor expansion and hegemony limits

The safeguard report exposes decreasing expansion weight and increasing delay
multipliers according to the gaining domain's current size:

| Permanent settlements | Expansion weight | Domain-delay multiplier |
|---:|---:|---:|
| 1 | `1.00` | `1` |
| 2 | `0.50` | `2` |
| 3 | `0.25` | `4` |
| 4+ | `0.10` | `8` |

A simulated automatic acquisition is rejected if the gaining domain would own
more than `50%` of all permanent Goa'uld settlements afterwards. An already
unbalanced world is not rewritten retroactively; the dominant faction is simply
blocked from further automatic gain through this path.

These values are centralized in `GoauldTerritorialSafeguardUtility` so a later
territorial consequence cannot silently bypass or duplicate them.

## One global reservation

`GameComponent_GoauldTerritorialStrategyTracker` persists at most one pending
territorial reservation globally. The state records:

- exact gaining and losing domains;
- exact settlement world-object ID;
- required relation;
- creation and resolution ticks;
- creation-time domain and settlement counts;
- pending, completion or cancellation outcome;
- debug-short-delay status.

No second reservation can be scheduled while this slot is occupied.

The current dry-run candidate represents a possible hostile transfer and
requires `OpenConflict` for the exact pair. This does not decide the final form
of `0.3.84-dev`; it provides a concrete regression case for the shared safety
contract.

## Incompatible strategic transitions

A reservation is rejected or cancelled when the exact pair already has an
alliance rupture pending. Future strategic systems must use the same exact-pair
compatibility boundary rather than blocking unrelated domains globally.

The reservation is also cancelled when:

- either domain is defeated or no longer territorial;
- the settlement disappears;
- the settlement changes owner before resolution;
- the exact GateRim relation no longer matches the stored requirement;
- fewer than two territorial domains remain;
- the world becomes too sparse;
- the losing domain would lose its final settlement;
- the gaining domain would exceed the `50%` ceiling.

A different storyteller suspends the deadline instead of cancelling the state.

## Cadence foundation

The tracker centralizes three cooldown layers for later consequences:

- global spacing: `15` days;
- involved-domain base cooldown: `30` days;
- exact-pair cooldown: `60` days.

The gaining domain's cooldown is multiplied by its size factor. All pending and
cooldown clocks are suspended outside `Commandement SG-1` and shifted forward on
return, preventing backlog accumulation.

No natural territorial reservation is scheduled in `0.3.83-dev`. These clocks
are exercised only by the dry-run developer path and persisted for future reuse.

## Diplomatic coherence

`GameComponent_GoauldInterDomainRelationTracker` remains authoritative for
relations between two Goa'uld domain factions. Every creation, transition, load
and periodic reconciliation maps its state to the corresponding vanilla
`FactionRelationKind`:

| GateRim state | Vanilla relation |
|---|---|
| `Neutral` | `Neutral` |
| `Rivalry` | `Neutral` |
| `OpenConflict` | `Hostile` |
| `Truce` | `Neutral` |
| `Alliance` | `Ally` |

The Goa'uld faction Def no longer uses the global `permanentEnemy` flag. It uses
`permanentEnemyToEveryoneExcept` with its own Def as the sole exception. This
keeps every domain permanently hostile to the player and all outside factions,
while permitting vanilla goodwill only between two Goa'uld domain instances.

Inter-domain synchronization uses `TryAffectGoodwillWith` without messages or
hostility letters and targets goodwill `-100`, `0` or `100` for the expected
`Hostile`, `Neutral` or `Ally` kind. The result is verified before success is
recorded; a failed pair/state emits one bounded warning instead of a repeating
error. The relation and territorial reports show the stored GateRim state,
actual vanilla relation, expected vanilla relation and whether the pair is
coherent.

The existing temporary-cooperation patch remains as a narrow runtime fallback
for a cooperative force already present on a map while reconciliation occurs.

## Developer tools

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
> Territorial strategy...
```

Exact actions:

```text
Show territorial safeguard report
Reconcile territorial and diplomatic state
Create pending territorial reservation
Trigger pending territorial reservation now
Cancel pending territorial reservation
Reset territorial safeguard state
```

The relation menu also adds:

```text
Set all pairs: Open conflict
```

The report includes storyteller state, the domain count recorded when the
tracker first initialized, currently existing and territorial domains,
settlement counts, sparse-world threshold, final-settlement protection,
expansion weight, delay multiplier, territorial share ceiling, all cooldowns,
diplomatic coherence, the exact pending reservation and its outcome.

## Explicit non-effects

`0.3.83-dev` does not:

- transfer settlement ownership;
- create or destroy a settlement;
- destroy a faction;
- change any tile;
- alter player or outside-faction goodwill;
- change permanent doctrines;
- change raid points, frequency or refire delay;
- add a natural incident, raid, reward or letter;
- apply the first real territorial consequence.

## Required focused validation

1. Create a new world and confirm three Goa'uld domain entries are proposed by
   default while the vanilla list permits reducing the count.
2. Under `Commandement SG-1`, use the relation report and exact relation actions
   to verify `Neutral/Rivalry/Truce -> Neutral`, `OpenConflict -> Hostile` and
   `Alliance -> Ally` in vanilla.
3. Open the territorial report and confirm at least two active territorial
   domains, exact permanent-settlement counts, the `domains + 2` threshold,
   final-settlement protection and size multipliers.
4. Set all pairs to open conflict, reset territorial state and create one
   pending reservation. Confirm the exact pair, settlement ID, creation counts
   and short positive deadline.
5. Save and reload. Confirm the same reservation and deadline remain.
6. Trigger it now. Confirm `CompletedDryRun`, all three cooldown layers and no
   settlement owner, settlement count, tile, faction, raid, reward or
   player/outside goodwill change.
7. Confirm the accepted `Player.log` contains no new relevant error, especially
   no `SetRelationDirect` rejection or repeated failure to reconcile two
   Goa'uld faction instances.
