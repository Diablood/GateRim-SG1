# Project state

Current milestone: `0.3.26-dev - Migrate wounded-agent care to mission framework` — completed and published under the final tag `v0.3.26-dev`.

## Development base

- Starting tag: `v0.3.25-dev`.
- Dedicated branch: `feature/wounded-agent-mission-migration`.
- Validated local revision: `0.3.26-dev-r2`.
- Published assembly version: `0.3.26.0`.
- Final publication tag: `v0.3.26-dev`.
- Cultural backstory count remains `83`.

## Milestone outcome

Tok'ra wounded-agent care is the third complete MissionDef-backed organic operation. The player flow remains recognizable: accept the request through the powered communicator, rescue the collapsed Tok'ra agent, place them in a colony medical bed, tend the acute symbiote shock, continue ordinary treatment until the agent is fit to travel, then let the agent leave the map alive.

`SG1_TokraOrganic_WoundedAgentCare` controls:

- offer duration, accepted-operation deadline, trust-tier weights and repeat penalty;
- generic and trust-tier-specific hidden recurrence ranges;
- the generated `PawnKindDef`, acute shock Hediff, recovery Hediff and optional illness Hediff;
- the stable-health duration and post-departure grace duration;
- minimum Moving, Consciousness and summary-health thresholds, maximum bleed rate and critical-Hediff threshold;
- adaptive optional-illness chance and severity ranges driven by the scaled threat snapshot captured at the offer;
- all offer, acceptance, arrival, progress, status, failure and success text keys;
- three offer variants and three success variants with local anti-repetition;
- success and failure trust changes;
- the declarative offered, accepted, recovering, ready, succeeded and failed phases.

The complete C# wounded-agent definition has been removed. A missing or incomplete MissionDef, an invalid required `PawnKindDef` or `HediffDef`, an invalid health profile, an incomplete recurrence table or a missing required text disables the archetype and writes one explicit configuration error.

## Shared framework addition

The framework gains one bounded reusable block, `pawnCare`, containing only declarative pawn-care data. It does not attempt to execute tending or health AI generically. This block can support future rescue, escort or medical-refuge missions when they share the same real needs.

## Deliberately retained in C#

The specialized adapter remains responsible for:

- generating and spawning the pawn at a reachable map edge;
- creating wounds through RimWorld health utilities;
- creating the vanilla-compatible Lord and departure behavior;
- checking the medical bed and actual tending of the shock Hediff;
- evaluating live RimWorld capacities, bleeding, urgent medical rest and lethal Hediffs;
- managing pawn references, save/load migration and map-loss, capture, death and departure outcomes;
- developer actions for forcing offers, completion and failures.

These are engine-facing mechanics. They should move into shared code only after another concrete mission proves that the implementation itself is reusable.

## Validation completed

Local revision `r1` validated the complete wounded-agent flow. The same startup exposed one unrelated regression in the already migrated intelligence operation: the accelerated objective had lost its XML `<workTicks>5000</workTicks>` entry while the archive was assembled. The MissionDef validator correctly disabled only that archetype instead of applying a hidden fallback.

Local revision `r2` restored that XML field without changing C# or wounded-agent gameplay. Final validation confirmed:

- project consistency, forced rebuild and assembly `0.3.26.0`;
- three valid MissionDefs and a clean framework report;
- normal rescue, emergency tending, recovery, `5000` stable ticks and departure;
- success only after the recovered agent actually leaves the map;
- death, capture, disappearance, timeout and failed-departure outcomes;
- persistence during offer, care, recovery and departure;
- adaptive illness parameters on weak and advanced colonies using the snapshot captured at offer;
- text variants, local anti-repetition and recurrence across repeated occurrences;
- observation, intelligence recovery and medical-handoff regressions;
- accelerated intelligence analysis restored to `5000` ticks;
- a clean final `Player.log`.

## Publication state

- Branch `feature/wounded-agent-mission-migration` published.
- Final annotated tag `v0.3.26-dev` published.
- Main GitHub repository updated.
- Separate wiki synchronized and published.

## Next milestone

The next milestone must start explicitly from `v0.3.26-dev` on a new dedicated branch. Medical handoff is the remaining legacy organic operation and the next logical migration candidate, but its exact scope must be confirmed against the authoritative repository files before implementation.

## Main files changed

- `1.6/Defs/MissionDefs/SG1_MissionFramework.xml`;
- `Languages/English/Keyed/SG1_TokraOrganicWoundedAgentCare.xml`;
- `Languages/French/Keyed/SG1_TokraOrganicWoundedAgentCare.xml`;
- `Source/GateRimSG1/Missions/GateRimMissionDef.cs`;
- `Source/GateRimSG1/Missions/GateRimMissionFramework.cs`;
- `Source/GateRimSG1/Goauld/TokraOrganicOperationFramework.cs`;
- `Source/GateRimSG1/Goauld/TokraOrganicWoundedAgentUtility.cs`;
- `Source/GateRimSG1/Goauld/GameComponent_TokraOrganicOperationManager.cs`;
- project, test, roadmap, changelog and wiki tracking files.
