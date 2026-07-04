# GateRim SG-1 storyteller

## Milestone

- Version: `0.3.66-dev`
- Branch: `feature/goauld-inter-domain-relations`
- Base: `v0.3.65-dev`
- Assembly: `0.3.66.0`
- Local revision: `r1`
- Status: published after final local revision `r1`.

## Purpose

`SG1_GateRimStoryteller` provides an optional, explicit orchestration boundary
for strategic systems that should belong to GateRim SG-1 rather than silently
modifying Cassandra, Phoebe, Randy or a modded storyteller.

Selecting `SG-1 Command` preserves the currently resolved Cassandra Classic
incident cadence while activating the persistent Goa'uld inter-domain relation
simulation added in `0.3.66-dev`.

## Player-facing description

The selection-panel description remains the concise `0.3.65-dev-r2` behavioral
summary. The relation system does not lengthen this text or restore a French
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

The appended component still emits no incidents in `0.3.66-dev`. Strategic
relations are maintained by a dedicated persistent game component rather than
by adding an incident to Cassandra's component list.

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
- no SG-1-only frequency or threat modifier is applied;
- existing published GateRim incidents retain their normal contracts.

This is a true suspension rather than a backlog. Returning to SG-1 Command does
not immediately consume transitions that would have become due under Cassandra,
Phoebe, Randy or a modded storyteller.

## Persistent storyteller lifecycle

`GameComponent_GateRimStorytellerOrchestrator` continues to store:

- schema version;
- whether SG-1 Command was active at the previous observation;
- activation count;
- last activation and deactivation ticks;
- last observation tick;
- last observed storyteller Def name.

It observes changes every `250` ticks and on new game, load and final
initialization.

Its report now also embeds the relation tracker's availability, active pair
count, automatic activation state and next strategic deadline.

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
A missing or invalid faction reference is removed during normalization.

New worlds and older saves are reconciled automatically. Every active pair that
does not already exist begins in neutrality.

## Relation states

The five persistent states are:

- neutral;
- rivalry;
- open conflict;
- truce;
- alliance.

The bounded first transition graph is:

```text
neutral -> rivalry | alliance
rivalry -> open conflict | neutral
open conflict -> truce
truce -> neutral | rivalry | alliance
alliance -> neutral | rivalry
```

The graph prevents nonsensical direct jumps such as open conflict immediately
becoming alliance.

## Cadence and suspension

- first transition for a new pair: `8–16` days;
- pair cooldown after a transition: `12–24` days;
- global spacing between RP reports: `5–10` days;
- runtime observation interval: `250` ticks;
- at most one automatic pair transition per global window.

Initial pair delays are derived from stable faction identifiers. Later delays
and transition choices use RimWorld's ordinary random source and persist in the
save.

## Anti-repetition

Two separate safeguards apply:

1. When more than one eligible pair exists, the pair used by the previous
   transition is excluded from the next selection.
2. Each resulting relation has three English and three French RP text variants.
   The exact key used by the previous report cannot repeat immediately when an
   alternative variant exists.

A world containing only one pair may naturally return to that pair after its
full cooldown.

## RP reports

Every real state change produces one neutral-event letter naming both domains.
The texts describe political or military intelligence without claiming effects
that do not exist yet.

The reports do not reveal transition weights, hidden thresholds or future raid
modifiers.

## Developer diagnostics

Open exactly:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

Available actions:

- `Show relation report`;
- `Create additional test domain`;
- `Reconcile relation pairs`;
- `Force next transition`;
- direct setters for neutral, rivalry, open conflict, truce and alliance;
- `Reset relations`.

The report exposes storyteller activation, schema, active and stored pairs,
suspension, last pair, last text key, current and previous states, transition
counts and deadlines.

The ordinary storyteller report remains available at:

```text
Actions de débogage
> GateRim SG-1
> Storyteller SG-1...
> Show orchestration report
```

## Inactive consequences

`0.3.66-dev` deliberately adds no:

- raid-frequency reduction during war;
- raid-frequency or threat increase during alliance;
- battle between two Goa'uld groups near the colony;
- reinforcements, joint raids or shared reprisals;
- doctrine interaction;
- territorial expansion or settlement destruction;
- change to faction goodwill toward the player.

These later effects require separate balancing and validation milestones. The
five states and their RP reports are the complete player-facing strategic
behavior of this milestone.

## Validation

Final revision `r1` passed the forced `0.3.66.0` rebuild, five-state tests,
French RP reports, save/reload persistence, Cassandra suspension and resumption,
pair anti-repetition, existing Goa'uld regressions and a clean `Player.log`.
The final result is recorded in [`TESTING_CURRENT.md`](TESTING_CURRENT.md), and
the durable coverage is maintained in [`TESTING.md`](TESTING.md).
