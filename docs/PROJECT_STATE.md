
# Project state

Current milestone: `0.3.65-dev - Add GateRim SG-1 storyteller foundation`
- final local revision `r2` validated after the `r1` foundation test.

## Repository state

- Starting tag: `v0.3.64-dev`.
- Active branch: `feature/sg1-storyteller-foundation`.
- Last published version and tag: `0.3.64-dev` / `v0.3.64-dev`.
- Technical assembly version: `0.3.65.0`.
- Final local revision: `r2`.
- Publication state: branch commit, annotated tag and separate wiki published.
- The separate wiki is synchronized and published from `docs/wiki/`.

## Implemented scope

- Add the optional `SG1_GateRimStoryteller`, displayed as `SG-1 Command` or
  `Commandement SG-1`.
- Preserve Cassandra Classic as the ordinary incident baseline by reading its
  current resolved component list at startup instead of copying a stale Core
  XML snapshot.
- Append one GateRim orchestration component that emits no incidents in this
  milestone.
- Add a persistent game component that observes storyteller activation,
  deactivation and save/reload without changing another storyteller.
- Add one authoritative activation utility for future strategic systems.
- Add the exact developer report:

  ```text
  Actions de débogage
  > GateRim SG-1
  > Storyteller SG-1...
  > Show orchestration report
  ```

- Add concise English/French storyteller text, aligned with the vanilla behavior-summary style, and a temporary original portrait.
- Record the approved future Goa'uld relation states and consequences without
  activating them.

## Validation result

The maintainer validates the final `r2` state:

- `Commandement SG-1` is selectable and uses the Cassandra baseline;
- the temporary portrait loads correctly;
- the orchestration report identifies the active storyteller and one additional
  GateRim component;
- activation, deactivation and save/reload persistence work;
- existing GateRim incidents retain their published behavior;
- `Player.log` remains clean;
- the shortened French description is fully visible without a scrollbar.

The `r2` correction changes only the English/French storyteller descriptions.
No C#, Def structure, component, texture, save data or balance value changed
from the functionally validated `r1` foundation.

## Storyteller boundary

Only the GateRim SG-1 storyteller may automatically advance and apply future
inter-domain strategic relations. Cassandra, Phoebe, Randy and compatible
modded storytellers keep their own pacing and receive no hidden GateRim
frequency or threat-point modifiers.

Existing GateRim incidents remain usable with other storytellers through their
published incident contracts. This milestone does not remove, suppress or
reweight any existing incident.

## Deliberately inactive future systems

`0.3.65-dev` does not create:

- rivalry, open conflict, truce or alliance states;
- relation transition rolls;
- reports about inter-domain diplomacy;
- battles between two Goa'uld forces near the colony;
- reductions or increases in raid frequency;
- raid-point multipliers, joint raids or reinforcements;
- expansion or settlement destruction.

## Next milestone

The next planned milestone is:

```text
0.3.66-dev - Add persistent Goa'uld inter-domain relations
branch: feature/goauld-inter-domain-relations
base: v0.3.65-dev
```

It must begin only after confirming the published branch, tag and wiki are
clean.
