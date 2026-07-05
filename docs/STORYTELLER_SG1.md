# GateRim SG-1 storyteller

## Current milestone

- Foundation: `0.3.65-dev`
- Persistent inter-domain relations: `0.3.66-dev`
- First strategic consequence: `0.3.68-dev`
- Local battlefield: `0.3.69-dev`
- Current world-site extension: `0.3.70-dev`
- Current branch: `feature/goauld-open-conflict-world-battlefield-site`
- Current assembly: `0.3.70.0`
- Current local revision: `r4`
- Status: final revision `r4` validated locally; publication pending.

## Purpose

`SG1_GateRimStoryteller` provides an optional, explicit orchestration boundary
for strategic systems that should belong to GateRim SG-1 rather than silently
modifying Cassandra, Phoebe, Randy or a modded storyteller.

Selecting `SG-1 Command` preserves the currently resolved Cassandra Classic
incident cadence while activating persistent Goa'uld inter-domain relations and
their separately validated strategic consequences.

## Player-facing description

The selection-panel description remains the concise `0.3.65-dev-r2` behavioral
summary. Later strategic systems do not lengthen this text or restore a French
scrollbar.

French:

```text
Le Commandement SG-1 suit un rythme classique : il augmente progressivement la
pression, puis vous accorde un répit. Il coordonne aussi les futurs événements
propres à GateRim.
```

English:

```text
SG-1 Command follows a classic rhythm: it steadily raises the pressure, then
gives you time to recover. It also coordinates future GateRim events.
```

## Cassandra baseline architecture

At static startup, `GateRimStorytellerBootstrap` reads the currently resolved
`Cassandra` Def and copies its public difficulty curves, adaptation settings and
component list into `SG1_GateRimStoryteller`. It then appends exactly one
`StorytellerComp_GateRimOrchestrator`.

This avoids embedding a frozen copy of Core XML. RimWorld 1.6, active DLCs and
compatible updates remain authoritative for Cassandra's ordinary incident
contracts.

The appended component emits no independent incident. Strategic relations and
their consequences are maintained by dedicated systems that check the active
storyteller explicitly.

## Activation contract

All automatic strategic systems must use:

```csharp
GateRimStorytellerUtility.IsGateRimStorytellerActive
```

The check compares the active `StorytellerDef` with
`SG1_GateRimStoryteller`. It never infers activation from difficulty,
storyteller labels or incident history.

When another storyteller is selected:

- no inter-domain transition is rolled;
- no RP relation report is emitted;
- all pair and global relation deadlines are shifted forward by the suspension
  duration when SG-1 Command becomes active again;
- the open-conflict natural-raid pressure factor resolves to `1.00`;
- existing published GateRim incidents retain their ordinary contracts.

This is a true suspension rather than a backlog. Returning to SG-1 Command does
not immediately consume transitions that would have become due under Cassandra,
Phoebe, Randy or a modded storyteller.

## Persistent storyteller lifecycle

`GameComponent_GateRimStorytellerOrchestrator` stores:

- schema version;
- whether SG-1 Command was active at the previous observation;
- activation count;
- last activation and deactivation ticks;
- last observation tick;
- last observed storyteller Def name.

It observes changes every `250` ticks and on new game, load and final
initialization.

Its report embeds relation-tracker availability, active pair count, automatic
activation state, next strategic deadline and the count of domains whose
natural-raid pressure is currently reduced. The battlefield tracker exposes its
own persistent cadence and active-map report through the relation debug menu.

## Persistent relation model

`GameComponent_GoauldInterDomainRelationTracker` stores one state for every
unordered pair of Goa'uld System Lord faction instances.

Each `GoauldInterDomainRelationState` stores:

- canonical first and second faction references ordered by `loadID`;
- current and previous relation;
- establishment tick;
- last and next transition ticks;
- transition count.

The relation belongs to the factions, not their current leaders. Replacing a
System Lord therefore does not reset diplomacy. A defeated domain remains safe
to deserialize and inspect, but its pairs are inactive and cannot transition.

New worlds and older saves are reconciled automatically. Every active pair that
does not already exist begins in neutrality.

## Relation states and transitions

The five persistent states are neutral, rivalry, open conflict, truce and
alliance.

```text
neutral -> rivalry | alliance
rivalry -> open conflict | neutral
open conflict -> truce
truce -> neutral | rivalry | alliance
alliance -> neutral | rivalry
```

Cadence remains:

- first transition for a new pair: `8–16` days;
- pair cooldown after a transition: `12–24` days;
- global spacing between RP reports: `5–10` days;
- runtime observation interval: `250` ticks;
- at most one automatic pair transition per global window.

Pair and text anti-repetition remain unchanged.

## Open-conflict natural-raid pressure

`0.3.68-dev` adds the first mechanical consequence of a relation state.

While SG-1 Command is active, every active domain participating in at least one
open conflict uses a fixed `0.75` factor for its ordinary natural Goa'uld Jaffa
raid points.

The effect is intentionally narrow:

- the factor does not stack across several rivals;
- doctrine eligibility and weighting use the original vanilla points;
- the factor is applied after doctrine selection;
- incident chance and refire delay remain unchanged;
- every forced incident path remains excluded by default, including extraction
  reprisals and deterministic regression raids;
- the dedicated pressure-test command enables the factor for one forced test;
- no new data is serialized.

Changing storyteller or relation state changes the derived factor immediately.

## Open-conflict battlefields

`0.3.69-dev` publishes the first visible battle caused by a relation state.
`0.3.70-dev` extends the same subsystem with an optional world-map site.

The persistent tracker owns one shared slot and cadence for both forms. It
selects one exact open-conflict pair, stores the previous pair and form, and
alternates local and world occurrences when both are possible. A world marker
and a local battle can never coexist in this slot.

Each camp belongs to its stored domain faction and receives `35%` of the
vanilla threat snapshot, clamped to `250–1800` points. Both forms reuse the same
map component: edge arrival, rally, announced assault, movement into weapon
range, bounded player retaliation, one-sided `30%` morale break, two-day limit
and fixed withdrawal.

The local form appears only on a suitable home map without another active
hostile force. The world form appears `6–18` tiles from a home map, lasts eight
days and generates its `140 × 140` encounter map only when a player caravan
arrives through the normal RimWorld world-path action.

Ignoring a world site has no failure consequence. It disappears without
changing goodwill, relation state, territory or settlements. After entry, the
site remains in the shared slot until the battle resolves and all player pawns
have left through ordinary caravan reformation.

## RP reports

Every real state change produces one neutral-event letter naming both domains.
The reports describe political or military intelligence without exposing hidden
weights. The open-conflict texts remain valid because the new effect is a modest
reduction in pressure rather than a guaranteed absence of attacks.

## Developer diagnostics

Relation path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

`Show natural raid pressure report` exposes the natural-raid factor for every
active domain.

Threat path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Threat progression...
```

`Show current progression` displays vanilla points, effective natural points,
pressure factor, unchanged intercepted points and doctrine context.

`Force current natural raid (pressure applied)` is available in the relation
menu and exercises the ordinary relation-aware worker while temporarily enabling
the factor for that forced validation. The historical forced-doctrine commands
remain exact and bypass the factor.

## Still inactive consequences

`0.3.70-dev` still adds no:

- alliance frequency or threat increase;
- reinforcements, joint raids or shared reprisals;
- doctrine interaction caused by relations;
- territorial expansion or settlement destruction;
- change to faction goodwill toward the player.

These effects require separate balancing and validation milestones.

## Validation

Local revision `r4` of `0.3.70-dev` is ready for build and the world-site
procedure recorded in [`TESTING_CURRENT.md`](TESTING_CURRENT.md). Validation
must cover the dedicated icon, optional expiration, ordinary caravan travel,
lazy map generation, shared combat behavior, loot/reformation, the single slot,
alternation, persistence and all published local-battlefield regressions.
