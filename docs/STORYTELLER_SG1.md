
# GateRim SG-1 storyteller

## Milestone

- Version: `0.3.65-dev`
- Branch: `feature/sg1-storyteller-foundation`
- Base: `v0.3.64-dev`
- Assembly: `0.3.65.0`
- Local revision: `r2`
- Status: final revision `r2` validated and published.

## Purpose

`SG1_GateRimStoryteller` provides an optional, explicit orchestration boundary
for strategic systems that should belong to GateRim SG-1 rather than silently
modifying Cassandra, Phoebe, Randy or a modded storyteller.

The first release is intentionally conservative. Selecting `SG-1 Command`
preserves an ordinary classic RimWorld cadence while enabling a persistent
GateRim-specific channel. No relation simulation or new incident is active yet.

## Player-facing description

The selection-panel description follows the same contract as the vanilla
storytellers: a short behavioral summary addressed to the player, not a
technical explanation of the implementation.

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


## Validation result

The maintainer validates the complete foundation and the focused `r2`
interface correction. The French description fits the storyteller-selection
panel without a scrollbar. Selection, portrait, Cassandra baseline,
activation/deactivation, save/reload persistence, diagnostics, existing
GateRim incident regressions and `Player.log` are also validated.

The final local revision is `r2`.

## Baseline architecture

The storyteller Def inherits the shared Core storyteller fields and carries one
GateRim marker component.

At static startup, `GateRimStorytellerBootstrap` reads the currently resolved
`Cassandra` Def and copies its public difficulty curves, adaptation settings and
component list into `SG1_GateRimStoryteller`. It then appends exactly one
`StorytellerComp_GateRimOrchestrator`.

This approach deliberately avoids embedding a frozen copy of the Core
storyteller XML. RimWorld 1.6, active DLCs and compatible updates remain
authoritative for Cassandra's ordinary incident contracts.

The appended component returns no incidents in `0.3.65-dev`.

## Activation contract

All future automatic SG-1 strategic systems must use:

```csharp
GateRimStorytellerUtility.IsGateRimStorytellerActive
```

The check compares the active `StorytellerDef` with
`SG1_GateRimStoryteller`. It never infers activation from difficulty,
storyteller labels or incident history.

When another storyteller is selected:

- no GateRim strategic transition is rolled;
- no SG-1-only frequency or threat modifier is applied;
- existing published GateRim incidents retain their normal contracts;
- serialized future strategic state may remain stored for a later return to the
  SG-1 storyteller.

## Persistent foundation

`GameComponent_GateRimStorytellerOrchestrator` stores:

- schema version;
- whether SG-1 Command was active at the previous observation;
- activation count;
- last activation and deactivation ticks;
- last observation tick;
- last observed storyteller Def name.

It observes changes every `250` ticks and on new game, load and final
initialization. It performs no strategic action.

## Developer diagnostics

Open exactly:

```text
Actions de débogage
> GateRim SG-1
> Storyteller SG-1...
> Show orchestration report
```

The report exposes:

- active storyteller label and Def name;
- SG-1 activation state;
- Cassandra baseline identity;
- bootstrap success;
- baseline and SG-1 component counts;
- persistent lifecycle state;
- explicit confirmation that no foundation incident or relation system is
  active and that other storytellers are not modified.

## Approved future relation model

The following design is recorded but not implemented by this milestone.

### Persistent pair states

Each unordered pair of Goa'uld domains may eventually store:

- neutral;
- rivalry;
- open conflict;
- truce;
- alliance.

The state belongs to faction instances, not their current System Lords. It can
therefore survive leader replacement and save/reload.

### Open conflict

Possible future effects under SG-1 Command only:

- a limited, globally capped reduction of attacks by the two involved domains
  against the player;
- RP reports identifying the pair and cause;
- a temporary map battle near the player colony;
- two forces arriving from distinct directions and prioritizing each other;
- optional player intervention against one force or both;
- one clear letter;
- no initial order to attack player structures;
- forced withdrawal after at most a few days;
- normal recovery of abandoned equipment rather than an artificial quest
  reward.

### Alliance

Possible future effects under SG-1 Command only:

- a small capped increase in attack cadence;
- or a small capped increase in threat points for attacks by the allied
  domains;
- later second-domain reinforcements;
- later joint raids;
- doctrine interactions;
- shared reprisals;
- alliance rupture after a major failure or loss.

No alliance may merge factions or apply an unbounded multiplicative bonus.

### Global safeguards

Future implementation must include:

- one major strategic relation per domain when necessary for readability;
- slow transitions and persistent cooldowns;
- pair-level anti-repetition;
- global caps for both war reductions and alliance bonuses;
- no automatic colony destruction in the first relation milestones;
- no self-elimination or runaway expansion;
- safe handling of defeated or removed factions;
- clean suspension when SG-1 Command is not active.

## Validation

The mandatory validation is maintained in
[`TESTING_CURRENT.md`](TESTING_CURRENT.md). Durable regressions are maintained
in [`TESTING.md`](TESTING.md).
