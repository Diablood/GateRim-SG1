# Tok'ra organic operation opportunities

## Purpose

Organic Tok'ra opportunities provide recurring, hidden-schedule activities that can build or damage trust without exposing a predictable event cycle. They remain separate from manual communicator support requests and unique mission chains.

## Shared framework

The `0.3.x` framework stores one active `TokraOrganicOperationInstance`, selects an eligible worker, persists its operation-specific payload and centralizes offer, acceptance, deadline, success, failure, cleanup and anti-repetition behavior.

The seven current workers are:

- Goa'uld observation;
- intelligence-module recovery and analysis;
- wounded-agent care;
- medical-supply handoff;
- distress-call world site;
- temporary-base delivery contract;
- diversion assault on a player colony.

Only the current operation is shown in normal play. Full history, hidden scheduling and technical state are debug-only.

## Communicator access (`0.3.33-dev`)

A new recurrent opportunity is eligible only while at least one valid powered Tok'ra communicator exists on a player home map. The unique introduction-artifact mission remains outside this system and can still occur before the communicator is researched or built.

Channel loss does not clear the single active slot. Offers and accepted operations already stored by the manager continue to use their existing mission deadlines and resolution rules. While the slot is empty, the planner persists a blocked state. Once power or a replacement communicator returns, it schedules a fresh hidden delay instead of replaying an overdue timer immediately.

## Diversion assault (`0.3.34-dev`)

Acceptance sends a false trace through the powered communicator; it does not place or deliver a mission object. A hidden XML-configured delay queues one dedicated Goa'uld/Jaffa assault using the scaled threat snapshot captured at offer time.

The mission-only raid uses an immediate breaching strategy and explicitly selects a dedicated pawn group containing Ma'Tok-equipped Jaffa breachers. This group is not used by ordinary Goa'uld raids. The assault allows kidnapping and stealing and disables normal timeout withdrawal. The runtime registers the exact spawned attackers. The operation succeeds when they are dead, downed or retreating empty-handed. It fails if one reaches the map edge with a player pawn or carried item, or if the player map is lost.

The adapter persists the response due tick, triggered state, raider ThingIDs and extraction diagnostics without new game-component save fields. It reuses the shared communicator gate, active slot, recurrence, anti-repetition and guarded trust resolution.

The stable internal `DecoyTransmissionDefense` identity is retained to avoid renumbering the persisted enum during development. Accepted r1 transmitter test saves are intentionally unsupported. No transmitter or dedicated texture remains in the operation.

## Goa'uld observation workflow (`0.3.2-dev`)

The former abstract wait-and-report sequence is replaced by a physical field operation:

1. acceptance delivers one `SG1_TokraObservationDevice`;
2. `TokraObservationUtility` selects an exterior reachable cell in a peripheral map band and places one temporary marker;
3. an Intellectual-capable colon uses `SG1_DeployTokraObservationDevice` to carry the device to that cell and deploy it;
4. the manager persists the deployment and recording-ready tick;
5. after recording completes, the player starts `SG1_TransmitTokraObservationData` from a powered communicator;
6. the colon retrieves the device, returns to the communicator and performs persistent transmission work;
7. the manager resolves success only after transmission reaches zero remaining work.

The device and marker are operation-only Defs with no Architect designation. Loss of the device or deadline expiry after acceptance produces one failure.

## Persistence

The observation payload stores:

- device and marker references;
- target cell;
- deployment flag;
- recording-ready tick;
- total and remaining transmission work;
- result-text variant.

A `0.3.1-dev` accepted observation that has no physical device is converted once into the new workflow. Other `0.3.x` operation state remains unchanged.

## Presentation boundary

Normal communicator text reports only the current phase. Debug reporting may include exact ticks, target cells, object references, variant indices and scheduler diagnostics.

## Extension rules

Future recurring operations should:

- reuse shared placement, persistence, cleanup and resolution services;
- add worker-specific state only when the shared instance cannot represent it;
- remain resumable after ordinary job interruption and save/reload;
- provide compact player-facing status text and separate technical debug detail;
- avoid adding standalone communicator gizmos when actions can be grouped in the existing debug menu.
