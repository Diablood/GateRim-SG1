# Tok'ra organic operation framework

## Purpose

The organic-operation framework schedules low-sensitivity requests initiated by Tok'ra cells. It remains separate from manual communicator support requests and from sensitive mission chains.

`0.2.50-dev` consolidated observation and intelligence recovery. `0.2.51-dev` extends the same framework with recurring care for a wounded Tok'ra agent without renumbering the established save values.

## Definitions

Each archetype is described by `TokraOrganicOperationDefinition` and registered in `TokraOrganicOperationFramework`.

A definition contains:

- persisted archetype identity;
- trust-tier selection weights;
- offer, preparation and deadline timing;
- success and failure trust changes;
- optional skill XP;
- player-facing action, letter and status keys;
- an optional physical-objective ThingDef;
- a technical debug label.

This keeps scheduler behavior data-oriented while retaining ordinary C# definitions and the existing save identifiers.

## Persistent state

`GameComponent_TokraOrganicOperationTracker` remains the authoritative save-persistent tracker. Existing Scribe keys from `0.2.48-dev` and `0.2.49-dev` are retained.

Framework save version `2` added an explicit ready state. Version `3` adds the wounded-agent pawn lifecycle while retaining all previous numeric values and Scribe keys.

Compatibility fields include:

- `tokraOrganicFrameworkSaveVersion`;
- `tokraOrganicResolutionApplied`;
- `tokraOrganicActiveWoundedAgent`;
- `tokraOrganicWoundedAgentStableSinceTick`;
- `tokraOrganicWoundedAgentDepartureOrdered`;
- `tokraOrganicWoundedAgentDepartureDeadlineTick`.

The existing enum values remain stable:

```text
TokraOrganicOperationArchetype.None = 0
TokraOrganicOperationArchetype.GoauldObservation = 1
TokraOrganicOperationArchetype.DeadDropRecovery = 2
TokraOrganicOperationArchetype.WoundedAgentCare = 3

TokraOrganicOperationState.None = 0
TokraOrganicOperationState.Offered = 1
TokraOrganicOperationState.Accepted = 2
TokraOrganicOperationState.Ready = 3
```

The internal `DeadDrop` name is retained only to avoid breaking saved data and Def references. Player text describes a Tok'ra intelligence module.
For legacy saves, an accepted observation whose preparation tick has already elapsed is promoted to `Ready` during load repair. The same promotion is performed lazily when the communicator menu or interaction is queried, preventing the transmission action from depending on the next periodic tracker tick.

## Resolution guarantee

All accepted-operation outcomes pass through one guarded resolver. A successful or failed outcome can apply only once:

- optional skill XP;
- Tok'ra trust change;
- outcome letter;
- success/failure counters;
- physical-objective cleanup;
- next hidden scheduling delay.

The guard is persisted so a save created around resolution cannot reapply the outcome after loading.

## Physical objectives

The shared framework handles lookup, placement and cleanup of physical objectives.

Placement delegates to `TokraDeliveryDropUtility.TryPlaceThingNearPreferredDeliveryCell`, preserving the established hierarchy:

1. Tok'ra delivery drop zone;
2. powered secure communicator;
3. reachable and unfogged map edge.

On load, an absent saved reference can be recovered by searching the active map for the objective ThingDef. Stale objectives outside the active operation are removed.

## Adding a future archetype

A future operation should:

1. add a stable enum value without renumbering existing values;
2. register one definition with timings, weights, outcomes and translation keys;
3. reuse the shared offer and resolution flow;
4. add custom interaction code only for genuinely distinct player actions;
5. preserve RP player text, concise menu labels and technical-only debug labels;
6. extend the durable checks in `docs/TESTING.md` using the standalone, session-ordered format defined in `docs/TESTING_GUIDELINES.md`.

A new archetype should not duplicate scheduling, trust resolution, persistence, objective cleanup or delivery routing.
## Wounded-agent care archetype

`WoundedAgentCare = 3` extends the framework without renumbering the observation or intelligence-recovery values. It uses a persistent pawn reference instead of a building objective. The tracker owns the care deadline, stability interval, departure order and single resolution, while vanilla rescue and tending systems own the actual medical gameplay.

The communicator displays only the currently active patient state. After departure, death or failure, the shared status returns immediately to the generic channel line and the archetype becomes eligible again after the hidden scheduler delay.

