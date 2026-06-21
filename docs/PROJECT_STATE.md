# Project state

Current milestone: `0.3.25-dev - Migrate intelligence recovery to mission framework` — validated locally on revision `r2` and published under the final tag `v0.3.25-dev`.

## Last completed milestone

- Development base tag: `v0.3.24-dev`.
- Dedicated branch: `feature/intelligence-recovery-mission-migration`.
- Validated local archive revision: `0.3.25-dev-r2`.
- Published final tag: `v0.3.25-dev`.
- Validated assembly version: `0.3.25.0`.
- Cultural backstory count remains `83`.

## Milestone objective

Migrate Tok'ra intelligence recovery as the second real MissionDef-backed organic operation and use it to validate adaptive threat consumption.

The existing player flow remains recognizable: accept the recovered module, choose cautious or accelerated analysis at the powered communicator, complete the intellectual work and receive the appropriate result. The migration moves the mission's declarative data and balance out of its legacy C# definition while preserving specialized RimWorld interactions in the adapter.

## Completed migration

`SG1_TokraOrganic_IntelligenceRecovery` now controls:

- offer duration, accepted-operation deadline, trust-tier weights and repeat penalty;
- generic and trust-tier-specific hidden recurrence delay ranges;
- the intelligence module, analysis job and Intellectual skill Def references;
- cautious and accelerated analysis durations of `10000` and `5000` ticks;
- the generic `Intellectual +350` success reward and the accelerated `+150` XP bonus;
- the `35%` accelerated interference chance;
- the Goa'uld patrol IncidentDef, its `5000–12500` tick delay and `2500` tick retry delay;
- a scaled difficulty profile using `35%` of the captured storyteller threat points, clamped to `180–700` points;
- all offer, acceptance, objective, status, method, failure and result text keys;
- three named three-entry result banks for cautious, accelerated and interference outcomes;
- trust changes and phase consequences.

The complete C# intelligence-recovery definition has been removed. A missing or incomplete MissionDef, an invalid required Def reference, a missing threat-scaling profile or an incomplete trust-tier recurrence table disables the archetype and writes one explicit configuration error.

The scheduler consumes the MissionDef's trust-tier-specific recurrence ranges after intelligence recovery. The accelerated Goa'uld patrol receives the scaled threat snapshot captured when the operation was offered; it does not recalculate points from the colony's later state.

## Shared framework additions

This second consumer adds only capabilities required by the real operation:

- context-specific recurrence delay ranges;
- named weighted text banks selected by semantic ID;
- consequence chance, minimum/maximum delay and retry fields;
- developer-report output for those values.

These additions remain generic and reusable by future missions without attempting to execute every phase or consequence automatically.

## Deliberately retained in C#

The specialized adapter remains responsible for:

- placing and tracking the recovered physical module;
- the powered-communicator interaction and method-selection dialog;
- RimWorld job creation, reservations, reachability and Toils;
- active work progress, interruptions and save/load migration;
- queuing the storyteller incident with the configured IncidentDef and captured points;
- compatibility fields for saves created before the MissionDef migration;
- debug actions that force method selection, completion or interference.

These are engine-facing mechanics rather than duplicated mission content. They should only move into shared code when another real mission needs the same implementation.

## Validation completed

The complete protocol in `docs/TESTING_CURRENT.md` was validated on local revision `r2`:

- project consistency and forced rebuild passed with assembly `0.3.25.0`;
- both MissionDefs and every configured `ThingDef`, `JobDef`, `SkillDef` and `IncidentDef` loaded without error;
- cautious analysis completed at `10000` ticks with `Intellectual +350` and no patrol;
- accelerated analysis completed at `5000` ticks with `Intellectual +500` total and retained a clear speed advantage at maximum game speed;
- interruption, resumption and save/reload preserved the configured total and current progress for both methods;
- the adaptive Goa'uld patrol was validated on a weak colony and an advanced colony, using `clamp(base threat × 0.35, 180, 700)`;
- the incident consumed the scaled snapshot captured at offer time even after the colony state changed before interference;
- the three independent result banks, immediate anti-repetition, failure cases and trust-tier recurrence ranges were validated;
- observation, wounded-agent care and medical handoff retained their expected flows;
- developer tools remained within the intended debug boundary and `Player.log` remained clean for the tested scope.

## Next development step

Start the next milestone explicitly from `v0.3.25-dev` on a new dedicated branch. Audit the remaining inherited organic operations and select the next real migration according to the shared capability it can prove. Do not generalize Toils, placement or medical mechanics without a second concrete consumer.

## Durable direction

The mission framework is a toolbox intended to cover roughly 70 to 90 percent of recurring missions and questlines. Specialized workers remain the normal solution for unique mechanics. Existing missions will be migrated progressively only when the common abstractions are proven by multiple real uses.

Every future recurring mission must consider:

- long-game replayability and re-eligibility after success or failure;
- hidden variable delays and local anti-repetition;
- difficulty derived from RimWorld storyteller threat points, active difficulty and colony wealth rather than fixed enemy counts;
- RP text variants, or deliberately repeatable prose when variants would not improve quality;
- persistent state, debug phase forcing and save/load migration.

## Published files

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- extended mission definition and framework classes;
- adapted Tok'ra organic-operation framework and manager files;
- `1.6/Defs/MissionDefs/SG1_MissionFramework.xml`;
- `README.md`;
- `docs/MISSION_FRAMEWORK.md`;
- project state, roadmap, current and durable tests, and changelog;
- wiki home and content-status revisions.

## Repository rules reminder

- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only one final tag per milestone, without an `-rN` suffix.
- Use `/` in repository-relative PowerShell paths written in Markdown.
- Run the project consistency checker before every final commit.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
- Follow `docs/MILESTONE_PUBLICATION.md` for commit, tag, push and wiki publication.
