# Project state

Current milestone: `0.3.33-dev - Gate Tok'ra operations behind the communicator` — validated locally after revision `r2` and published under the unique tag `v0.3.33-dev`.

## Published state

- Starting tag: `v0.3.32-dev`.
- Dedicated branch: `feature/tokra-communicator-operation-gating`.
- Final local revision: `0.3.33-dev-r2`.
- Assembly version: `0.3.33.0`.
- Mod metadata version: `0.3.33-dev`.
- Cultural backstory count remains `83`.

## Delivered behavior

- `SG1_TokraSecureCommunications` replaces vanilla `MicroelectronicsBasics` as the direct construction prerequisite of the Tok'ra secure communicator.
- Existing communicators remain compatible with older saves; research completion gates new construction only.
- `TokraSecureCommunicatorAvailabilityUtility` is the common service for detecting a player-controlled, correctly configured and powered communicator on a player home map.
- Every new recurrent Tok'ra offer requires at least one available communicator.
- The unique introduction-artifact mission remains independent and can still begin before the communicator exists.
- Losing power or destroying the building never clears an operation already offered, accepted, ready or active.
- The blocked planner state persists through save/reload.
- When a valid channel returns, an overdue check is replaced with a fresh hidden recurrence delay based on the current trust tier and previous archetype, preventing an immediate guaranteed offer.
- Natural selection, specific force-offer actions and map selection use the same availability service.
- Developer reports expose communicator availability, gate state and the next hidden opportunity tick without revealing those values in normal play.

## Validation completed

- Revision `r1` validated the research prerequisite, compatibility of existing buildings, powered and unpowered states, the shared availability report and save/reload.
- Revision `r2` validated that no new recurrent offer appears without a powered communicator and that this blocked state survives save/reload.
- Restoring a valid communicator schedules one fresh hidden delay in the future instead of immediately releasing an overdue offer.
- `Tok'ra ops: roll next natural offer` still produces one normal weighted offer after the channel returns.
- An offer already occupying the active slot remains unchanged through power loss or building destruction, including save/reload.
- Specific force-offer actions do not bypass the communicator gate.
- No additional failure, refusal, trust consequence, duplicate letter or active-slot replacement was observed during the focused tests.
- The final RP pass found no new player-facing wording requiring correction; the new diagnostic labels remain restricted to developer tools.
- Project consistency, forced rebuild and focused in-game validation completed without a new GateRim SG-1 error being reported.

## Durable boundaries

- Research completion does not disable an already-built communicator.
- A temporary outage does not pause or rewrite deadlines already owned by an active operation.
- Any valid powered communicator on any player home map can reopen the recurrent channel.
- Manual communicator requests keep their own trust and cooldown rules and remain outside the recurrent planner.
- The storyteller remains interchangeable: the communicator gates access to recurrent Tok'ra operations, not their compatibility with vanilla or modded storytellers.

## Next milestone

Do not continue development on the published `0.3.33-dev` branch. Before opening the next milestone, read `docs/ROADMAP.md`, select one focused and testable backlog item, then create a dedicated branch from `v0.3.33-dev`. No `0.3.34-dev` scope has been selected by this closure document.
