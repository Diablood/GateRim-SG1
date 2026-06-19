# Tok'ra organic operation framework

## Purpose

The organic-operation framework schedules recurring requests initiated by Tok'ra cells. It remains separate from manual communicator support requests and from the decoded-relay mission chain.

`0.3.0-dev` replaces the former monolithic operation tracker with an internal reusable framework. No new player-visible operation is added by this milestone.

## Compatibility boundary

`0.3.0-dev` deliberately starts a new save architecture.

- Saves created with `0.2.x-dev` are not supported.
- A new game is required when moving to `0.3.0-dev`.
- Obsolete migration-only fields, load repair and the former medical-container compatibility Def are removed.
- Saves created from `0.3.0-dev` are the new compatibility baseline for future development.

This breaking change is acceptable before public release and avoids carrying unpublished development migrations into the stable mod.

## Main architecture

### `GameComponent_TokraOrganicOperationManager`

The game component owns only framework-wide responsibilities:

- hidden scheduling and trust-tier delays;
- weighted archetype selection and local anti-repetition;
- the currently offered or active instance;
- one post-resolution consequence record;
- shared communicator status;
- success, failure and ignored-offer counters;
- shared developer controls.

Manual Tok'ra requests remain outside this manager.

### `TokraOrganicOperationInstance`

Only one organic operation can be visible at a time. Its persisted runtime data is stored in one `IExposable` instance rather than as new fields added to the game component for every archetype.

The common record contains:

- archetype and state;
- map identifier;
- offer, acceptance, readiness and deadline ticks;
- duplicate-resolution guard;
- optional physical objective;
- optional wounded-agent pawn and medical-care state;
- optional medical liaison, meeting cell and arrival state.

A future archetype should extend the shared instance only when a field is genuinely reusable. Operation-specific behavior belongs in a worker.

### `TokraOrganicOperationWorker`

Each archetype is selected through a worker registry:

- `TokraOrganicOperationWorker_GoauldObservation`;
- `TokraOrganicOperationWorker_DeadDropRecovery`;
- `TokraOrganicOperationWorker_WoundedAgentCare`;
- `TokraOrganicOperationWorker_MedicalSupplyHandoff`.

A worker owns the routing for:

- acceptance;
- active ticking;
- communicator completion when relevant.

Shared scheduling, persistence, trust resolution and cleanup remain in the manager.

### `TokraOrganicOperationDefinition`

Definitions continue to provide data shared by scheduling and presentation:

- trust-tier weights;
- offer, preparation and deadline timings;
- XP and trust consequences;
- translation keys;
- optional physical-objective Def;
- short technical label.

### Shared services

Existing services remain reusable behind the framework:

- `TokraDeliveryDropUtility` for preferred placement;
- `TokraOrganicWoundedAgentUtility` for patient generation and departure;
- `TokraOrganicMedicalSupplyUtility` for liaison arrival, resource transfer and departure;
- `GameComponent_TokraTrustTracker` for qualitative relationship consequences.

## Persistent post-resolution consequences

A consequence can outlive the primary operation. The first implemented case is the medical liaison dying while leaving after a successful handoff.

`TokraOrganicOperationFollowUp` persists this reference and its pending penalty separately. Clearing the primary operation therefore cannot erase the follow-up check or reapply the primary success.

## Resolution guarantee

All accepted outcomes pass through one guarded resolver. A primary outcome can apply only once:

- skill XP;
- Tok'ra trust change;
- outcome letter;
- success/failure counters;
- objective cleanup;
- next hidden scheduling delay.

The guard is stored inside the active instance.

## Debug and validation surface

The framework exposes one shared test API.

RimWorld developer mode provides compact actions under `GateRim SG-1`:

- force each archetype offer;
- accept the current offer;
- advance the current phase;
- resolve success;
- resolve failure;
- expire the current state;
- display the full framework state;
- apply a pending post-operation consequence;
- reset the framework.

The same controls are available from one `Tok'ra operation debug` menu on the secure communicator whenever RimWorld developer mode or the GateRim SG-1 advanced-debug setting is active. No debug command is visible during normal play.

Player-facing texts remain RP-oriented. The state report and debug labels may expose technical details.

## Adding a future archetype

A new organic operation should:

1. add a stable archetype value without renumbering existing values;
2. register one definition;
3. add one worker;
4. reuse the manager's offer, persistence, resolution and scheduling flow;
5. reuse existing visitor, delivery, resource or trust services when applicable;
6. add only genuinely reusable runtime data to `TokraOrganicOperationInstance`;
7. expose all important phases through the common debug API;
8. extend the durable checks in `docs/TESTING.md`.

A new archetype must not duplicate hidden scheduling, anti-repetition, trust resolution, single-visible-operation handling or duplicate-resolution protection.
