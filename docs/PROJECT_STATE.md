# Project state

Current milestone: `0.3.27-dev - Migrate medical handoff to mission framework` — completed and published under the final tag `v0.3.27-dev`.

## Development base

- Starting tag: `v0.3.26-dev`.
- Dedicated branch: `feature/medical-handoff-mission-migration`.
- Validated local revision: `0.3.27-dev-r1`.
- Published assembly version: `0.3.27.0`.
- Final publication tag: `v0.3.27-dev`.
- Cultural backstory count remains `83`.

## Milestone outcome

The Tok'ra medical-supply handoff is the fourth complete MissionDef-backed organic operation and the last legacy organic operation in the current set. The player flow remains recognizable: accept the request through the powered communicator, wait for a liaison, send a socially capable colonist to the meeting, transfer the requested medicine and let the liaison leave the map.

## MissionDef ownership

`SG1_TokraOrganic_MedicalSupplyHandoff` controls:

- offer duration and the accepted handoff deadline;
- trust-tier weights, repeated-archetype penalty and hidden recurrence ranges;
- liaison PawnKind, arrival delay range, post-handoff departure grace and the trust penalty if the departing liaison dies;
- delivered ThingDef, required count, dialogue JobDef and negotiation SkillDef;
- generic `Social +350` XP reward and success/failure trust changes;
- all offer, acceptance, arrival, dialogue, status, failure, success and post-handoff text keys;
- three offer variants and three success variants with local anti-repetition;
- offered, accepted, ready, succeeded and failed phases.

The complete C# fallback definition is removed. Missing or invalid required MissionDef data disables only this archetype and writes one explicit configuration error.

## Shared framework addition

The framework gains one bounded `handoff` profile for visitor identity, arrival timing, departure grace and a post-completion death consequence. The resource, quantity, job and skill remain expressed through the existing `DeliverThing` objective vocabulary.

## Deliberately retained in C#

The specialized adapter remains responsible for:

- finding entry and meeting cells;
- spawning the liaison and creating its Lord behavior;
- pathfinding, reservation and dialogue-job execution;
- consuming accessible map resources through RimWorld Thing stacks;
- tracking the liaison before and after completion;
- detecting death, capture, loss, timeout and departure;
- persistent pawn references and old-save migration.

The meeting radius, arrival-distance check and periodic state-check interval remain technical adapter constants rather than mission balance.

## Validation completed

Local revision `r1` validated:

- project consistency, forced rebuild and assembly `0.3.27.0`;
- four valid MissionDefs and a clean framework report;
- delayed liaison arrival, meeting, dialogue and exact delivery of two accessible industrial medicines;
- disabled interaction reasons for missing resources, incapability, reservation and reachability;
- `Social +350` and `+2` trust on successful delivery;
- `-1` trust for accepted-operation failures and the additional `-2` consequence if the liaison dies after completion but before leaving;
- death, capture, disappearance and timeout before delivery;
- save/load during offer, arrival, meeting and monitored departure;
- three offer and three success variants with immediate anti-repetition;
- recurrence after resolution and the configured last-archetype weight penalty;
- observation, intelligence recovery and wounded-agent care without regression;
- debug visibility boundaries and a clean final `Player.log`.

## Publication state

- Branch `feature/medical-handoff-mission-migration` published.
- Final annotated tag `v0.3.27-dev` published.
- Main GitHub repository updated.
- Separate wiki synchronized and published.

## Next milestone

All four current Tok'ra organic operations are now MissionDef-backed. The next milestone must start explicitly from `v0.3.27-dev` on a new dedicated branch and should be selected after auditing the remaining roadmap rather than extending the framework for a theoretical case.

## Main files changed

- `1.6/Defs/MissionDefs/SG1_MissionFramework.xml`;
- `Languages/English/Keyed/SG1_TokraOrganicMedicalSupplyHandoff.xml`;
- `Languages/French/Keyed/SG1_TokraOrganicMedicalSupplyHandoff.xml`;
- `Source/GateRimSG1/Missions/GateRimMissionDef.cs`;
- `Source/GateRimSG1/Missions/GateRimMissionFramework.cs`;
- `Source/GateRimSG1/Goauld/TokraOrganicOperationFramework.cs`;
- `Source/GateRimSG1/Goauld/GameComponent_TokraOrganicOperationManager.cs`;
- `Source/GateRimSG1/Goauld/GameComponent_TokraTrustTracker.cs`;
- `Source/GateRimSG1/Goauld/TokraOrganicMedicalSupplyUtility.cs`;
- `Source/GateRimSG1/Goauld/Dialog_TokraMedicalSupplyHandoff.cs`;
- `Source/GateRimSG1/Goauld/Comp_TokraMedicalSupplyLiaisonFloatMenu.cs`;
- project, test, roadmap, changelog and wiki tracking files.
