# Tok'ra introduction arc

## Purpose

The introduction arc establishes first contact before the Tok'ra communicator exists. It is not one of the recurrent organic operations and does not use their single active slot.

The arc provides a physical Tok'ra cipher module. Later milestones may require that artifact for a dedicated study or research step, then require that research to construct the communicator and unlock recurrent Tok'ra operations.

## Uniqueness rule

The mission is unique only after successful artifact recovery.

- Ignoring, declining or allowing an offer to expire does not close the arc.
- Accepting and failing the combat mission does not close the arc.
- Every unresolved attempt schedules a new hidden opportunity after a fresh variable delay.
- The interval may span several in-game days or months so the mission does not repeat mechanically.
- Recovering the exact mission artifact permanently completes the arc and prevents every future offer.

## Persistent state model

`GameComponent_TokraIntroductionArc` owns the persistent state:

| State | Meaning |
|---|---|
| `Uninitialized` | Save has not initialized the subsystem yet. |
| `WaitingForOpportunity` | The initial hidden delay is running or has become due. |
| `Offered` | The encrypted signal has been presented and remains available until expiry. |
| `Active` | The offer was accepted and its world site exists. |
| `RetryDelay` | A declined, expired or failed attempt is waiting for another opportunity. |
| `Completed` | The tracked cipher module was recovered and the arc is permanently closed. |

Persistent counters record opened, failed, expired and declined attempts. The last selected offer and completion text indexes prevent immediate textual repetition. `GateRimMissionRuntimeData` stores the current phase, the captured threat snapshot, the active world-object ID and the exact mission artifact ID.

## Configured delays

The standalone `SG1_TokraIntroductionArtifactMission` Def owns all delay ranges:

| Context | Range |
|---|---:|
| Initial opportunity | `240000–720000` ticks (`4–12` days) |
| Declined, expired or unanswered offer | `600000–3600000` ticks (`10–60` days) |
| Failed accepted attempt | `420000–2700000` ticks (`7–45` days) |

A new random value is selected after every unresolved attempt and persisted as an absolute game tick. Reloading must not reroll or shorten it.

## Natural offer and player choice

When the hidden opportunity becomes due, the arc selects a valid player home map and opens a player-facing encrypted transmission. The player may accept the recovery attempt or let the signal pass.

Acceptance creates the world site before the state changes to `Active`. If no valid site can be found, the offer remains open instead of silently consuming the attempt. Declining uses the same long retry class as an ignored or expired offer.

The offer is independent from the Tok'ra communicator and can occur before that building or its future research exists.

## World site and combat

Acceptance creates `SG1_TokraIntroductionArtifactWorldSite` between `6` and `18` tiles from the selected home map. The site uses vanilla caravan travel and arrival. Its six-day deadline is configured in the MissionDef, with a separate one-day final warning stored on the world object so save/reload cannot repeat it.

On first entry, the encounter map creates:

- one exact `SG1_TokraIntroductionArtifact` cipher module;
- a deliberately moderate adaptive Goa'uld/Jaffa guard based on the threat snapshot captured when the offer opened;
- between `2` and `6` defenders according to the XML profile;
- a preferred nearby caravan entry edge.

The site uses a specialized adapter for map generation and objective tracking, while movement, caravan entry and caravan reformation remain vanilla RimWorld flows.

## Artifact identity and success

The generated module's exact `ThingID` is stored in the mission runtime. The arc completes only when that tracked object is found in:

- a player caravan;
- a player pawn's carried inventory;
- or a player home map.

A separately spawned copy of the same `ThingDef` must not complete the mission. Destruction of the tracked module fails the active attempt and schedules another opportunity after the failed-attempt delay.

## Reformation and loot

After all active hostiles are defeated, the site exposes the vanilla caravan-reformation flow. The player may select surviving colonists, animals, the mission module, enemy weapons and armor, and any other recoverable map item.

The site remains available until the arc is resolved. Leaving without the module does not grant success; the player may revisit while the deadline remains active. Success is detected only after the tracked module actually leaves with the player or reaches a player home map.

## Failure and migration

An active attempt fails and reschedules when:

- the tracked artifact is destroyed;
- the site deadline expires after its persisted final warning;
- the tracked world site disappears unexpectedly;
- or a developer failure action is used.

Accepted-attempt failures use the hidden `7–45` day retry range. The final warning does not change, extend or reroll the deadline; it only makes the remaining time visible to the player.

A save created with revision `r1` in its synthetic `Active` state has no real world site. Revision `r2` migrates that state into a failed-attempt retry so the arc cannot remain permanently blocked.

## Separation from recurrent operations

The recurrent Tok'ra orchestrator resolves six named MissionDefs through `TokraOrganicOperationFramework`. The introduction MissionDef is not added to that registry or to `TokraOrganicOperationArchetype`.

It therefore does not occupy the recurrent-operation slot, expose their catalogue, modify Tok'ra trust or depend on the communicator.

## Developer actions

All actions are under `Debug actions menu → GateRim SG-1`:

- `Tok'ra intro: make opportunity due`
- `Tok'ra intro: force offer`
- `Tok'ra intro: accept offer`
- `Tok'ra intro: decline offer`
- `Tok'ra intro: fail attempt`
- `Tok'ra intro: expire offer`
- `Tok'ra intro: move site to deadline warning`
- `Tok'ra intro: expire active site`
- `Tok'ra intro: destroy tracked artifact`
- `Tok'ra intro: recover key artifact`
- `Tok'ra intro: show state`
- `Tok'ra intro: reset arc`

The exact focused protocol is maintained in `docs/TESTING_CURRENT.md`.

## Validation and deferred implementation

Final local revision `r3` validates the persistent arc, natural choice letter, world site, moderate adaptive guard, exact artifact identity, vanilla loot and reformation, final warning, real failure paths, retries and permanent completion. The final player-text review required no additional functional rewrite.

The following remain later work:

- final scene dressing and a dedicated artifact texture;
- broader balance passes across more colony wealth and storyteller combinations;
- artifact study and dedicated Tok'ra research;
- communicator construction prerequisites;
- gating recurrent operations behind an available powered communicator.
