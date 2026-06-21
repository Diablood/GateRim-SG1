# Project state

Current milestone: `0.3.28-dev - Audit Tok'ra operation orchestration and long-term recurrence` — validated locally and published under `v0.3.28-dev`.

## Development base

- Starting tag: `v0.3.27-dev`.
- Dedicated branch: `feature/tokra-operation-orchestration-audit`.
- Final validated local revision: `0.3.28-dev-r1`.
- Published assembly version: `0.3.28.0`.
- Final unique tag: `v0.3.28-dev`.
- Cultural backstory count remains `83`.

## Milestone purpose

All four existing Tok'ra organic operations are now MissionDef-backed. This milestone audits the shared persistent scheduler before adding world-site and caravan missions. It does not add a new player-facing mission and does not change current weights, hidden delay ranges, rewards or trust consequences.

The goal is to confirm that long games can repeatedly receive varied operations without concurrent offers, predictable cycles or a temporarily unavailable archetype suppressing another eligible one.

## Orchestration correction

The natural scheduler now:

1. finds the eligible colony map and current Tok'ra trust tier;
2. builds the set of MissionDef-backed archetypes with a positive configured weight;
3. calls each mission worker's `CanOffer(map)` before the weighted draw;
4. removes temporarily unavailable candidates;
5. applies the configured repeated-archetype penalty to the previous offer;
6. performs the weighted draw only among remaining eligible candidates.

Previously, the scheduler selected an archetype before checking `CanOffer`. A temporarily unavailable future world-site or caravan mission could therefore consume a scheduler attempt while another operation was eligible. The filtered draw prevents that failure mode.

If configured candidates exist but all are temporarily unavailable, the manager keeps its global slot empty and retries on the normal internal state-check interval. It does not consume a full hidden recurrence delay. If no archetype has a positive weight for the current context, the stable no-candidate state schedules the normal recurrence delay.

## Developer diagnostics

Two developer-only actions are added:

- `Tok'ra ops: roll next natural offer` runs the real natural weighted selection without forcing a particular archetype. It requires an empty active slot and a powered Tok'ra communicator.
- `Tok'ra ops: audit long-term orchestration` reports the global active slot, next hidden opportunity, outcome counters, current offerability, trust-tier weights and delays, text-bank counts and a deterministic `5000`-draw simulation for every trust tier.

The simulation checks that every positively weighted current archetype remains reachable and reports the immediate-repeat rate after applying each definition's repeat factor. It is diagnostic only and does not modify the save.

## Validated invariants

- one persistent global active operation slot;
- no new natural offer while another operation is offered, accepted, active or ready;
- every success, failure and ignored/expired offer schedules another hidden delay;
- the last offered archetype remains penalized locally but is never permanently excluded;
- all four existing archetypes remain recurrent after success or failure;
- save/load preserves the active state, last archetypes, counters and next hidden opportunity;
- player-facing communicator output exposes only the current operation or a generic channel state;
- weights, delays, outcomes and text variants remain MissionDef-driven;
- current operations remain compatible with compatible vanilla or modded storytellers.

## Locked mission sequence

The following sequence is now authoritative:

1. `0.3.28-dev - Audit Tok'ra operation orchestration and long-term recurrence`;
2. `0.3.29-dev - Add Tok'ra distress call world-site mission`;
3. `0.3.30-dev - Add Tok'ra temporary-base delivery mission`.

### `0.3.29-dev` direction

The distress call will create a temporary world site with a failure timer and a hidden situation revealed on arrival:

- genuine rescue with Tok'ra survivors needing assistance;
- compromised signal or Goa'uld/Jaffa trap;
- arrival too late, with no allied survivors and remaining enemies guarding, searching or preparing to leave the site.

The mission must be recurrent, adapt threat to RimWorld difficulty and colony strength, vary RP texts, avoid immediate repetition and allow Tok'ra trust to rise or fall according to the result and player decisions.

### `0.3.30-dev` direction

The temporary-base delivery will create a world destination and configurable cargo such as an object, intelligence or medicine. It must account for normal delivery, interception or ambush, loss of cargo, delay, abandonment and a compromised destination.

World-site generation, caravans, interception and combat will use specialized C# adapters. Mission identity, phases, configurable cargo, timing, text, recurrence, difficulty, rewards and consequences should remain Def-driven where practical.

## Long-term mission direction

The mission pool is intended to grow progressively so long games remain varied and enjoyable. Before `1.0.0`, the priority is a functional, coherent and sufficiently rich base rather than permanently final balance. Frequency, rewards, difficulty, text variants and mechanics may be revised after prolonged real-play testing, including after `1.0.0`.

The framework remains technically faction-neutral. The Tok'ra pool is completed and enriched first. Separate Goa'uld mission pools, followed by missions for other races and factions, will retain their own RP identity, appearance conditions, rewards and consequences instead of being treated as Tok'ra variants.

## Validation result

Local revision `r1` passed the consistency check, forced rebuild and complete in-game checklist. The deterministic audit returned `PASS`; natural rolls preserved the single global slot; success, failure and ignored offers all scheduled new hidden delays; recurrence and local anti-repetition remained functional; save/load preserved hidden and active states; all four existing operations passed regression checks; normal player UI boundaries and `Player.log` were clean.

## Deferred Tok'ra introduction arc

A future milestone will gate recurrent Tok'ra operations behind an introductory progression:

1. a unique first-contact or recovery mission with a real combat objective awards a Tok'ra key object or analysis artifact;
2. that object unlocks a dedicated GateRim SG-1 research project with vanilla `Electricity` as prerequisite;
3. completing the research permits construction of the Tok'ra communicator;
4. only a constructed and powered communicator makes the recurrent Tok'ra pool eligible.

The exact artifact, enemy force, site type and failure recovery remain to be designed. The unique introduction must not create a permanent campaign lock if the first attempt fails. This arc is recorded for later and is not part of `0.3.28-dev`, `0.3.29-dev` or `0.3.30-dev` unless explicitly rescheduled.

## Publication state

- Branch: `feature/tokra-operation-orchestration-audit`.
- Final tag: `v0.3.28-dev`.
- Main repository and separate wiki synchronized.
- Next milestone: `0.3.29-dev - Add Tok'ra distress call world-site mission`, starting explicitly from `v0.3.28-dev` on a new dedicated branch.
