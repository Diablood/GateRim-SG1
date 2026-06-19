# Tok'ra organic operation opportunities

## Purpose

Organic Tok'ra opportunities provide recurring, hidden-schedule activities that can build or damage trust without exposing a predictable event cycle. They remain separate from manual communicator support requests and unique mission chains.

## Shared framework

The `0.3.x` framework stores one active `TokraOrganicOperationInstance`, selects an eligible worker, persists its operation-specific payload and centralizes offer, acceptance, deadline, success, failure, cleanup and anti-repetition behavior.

The four current workers are:

- Goa'uld observation;
- intelligence-module recovery and analysis;
- wounded-agent care;
- medical-supply handoff.

Only the current operation is shown in normal play. Full history, hidden scheduling and technical state are debug-only.

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
