# Project state

Current milestone: `0.3.24-dev - Complete observation mission Def migration` — validated locally on revision `r1` and published under the final tag `v0.3.24-dev`.

## Last completed milestone

- Development base tag: `v0.3.23-dev`.
- Dedicated branch: `feature/observation-mission-def-cleanup`.
- Validated local archive revision: `0.3.24-dev-r1`.
- Published final tag: `v0.3.24-dev`.
- Validated assembly version: `0.3.24.0`.
- Cultural backstory count remains `83`.

## Milestone objective

Finish the observation pilot's data migration before using the framework as a reference for a second mission.

The player flow remains specialized C# because its hauling, reservations, pathfinding, Toils, save migration and physical field interactions are specific RimWorld mechanics. The values and content that define the observation mission itself are now owned by its MissionDef instead of being duplicated in that adapter.

## Completed migration

`SG1_TokraOrganic_GoauldObservation` now controls:

- offer duration, deadline, recurrence delay range and trust-tier weights;
- repeat penalty and difficulty snapshot profile;
- observation device, field marker, deployment job and transmission job Def names;
- deployment, active observation, recovery and transmission work durations;
- operator skill and XP gained per active work tick;
- generic final skill XP rewards, currently `Intellectual +250`;
- accept, deploy, continue, recover, transmit and resume action keys;
- status, disabled-reason, phase, target, success and failure text keys;
- weighted success-letter variants with local immediate anti-repetition;
- trust and experience rewards.

The complete legacy C# observation definition has been removed. A missing or incomplete required MissionDef now disables the observation archetype and writes one explicit configuration error instead of silently restoring legacy values. Required `ThingDef`, `JobDef` and `SkillDef` references are checked before the archetype becomes eligible.

The recurrence scheduler consumes the completed observation definition's configured hidden delay range. The other three organic operations retain their previous trust-tier delay logic and C# definitions.

## Deliberately retained in C#

The following are adapter mechanics, not mission balance data:

- RimWorld job target indexes, reservations, carrying and Toil sequencing;
- map-cell search and placement validation;
- reachability and powered-communicator checks;
- persistence fields and migration of old active observations;
- generic active-work polling interval and defensive state checks;
- explicit adapter branches that connect the `GoauldObservation` archetype to its specialized field flow.

These mechanics should only be generalized later when another real mission demonstrates a shared implementation need.

## Validation completed

The complete protocol in `docs/TESTING_CURRENT.md` was validated on local revision `r1`:

- project consistency and forced rebuild passed with assembly `0.3.24.0`;
- the observation MissionDef and all required `ThingDef`, `JobDef` and `SkillDef` references loaded without error;
- the developer report exposed the configured `500 / 10000 / 500 / 1000` work ticks, `Intellectual` active XP and `Intellectual +250` final reward;
- the complete observation flow succeeded from offer through transmission without using the removed C# fallback;
- save/reload preserved offered, active and interrupted-transmission states, including compatibility with an occurrence started under `v0.3.23-dev`;
- success variants, immediate anti-repetition, timeout failure, physical-objective loss and recurrence from the XML delay range were validated;
- intelligence recovery, wounded-agent care and medical handoff retained their legacy C# flows;
- no technical data leaked into player-facing texts or tools outside the intended debug boundary;
- `Player.log` remained clean for the tested scope.

## Next development step

Start the next milestone explicitly from `v0.3.24-dev` on a new dedicated branch.

Select a second real mission or operation to migrate. Prefer a candidate that naturally consumes the captured RimWorld threat points or demonstrates another shared capability not exercised by observation. Do not add theoretical abstractions without at least two concrete consumers.

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
- adapted Tok'ra observation device, point, utility, framework and manager files;
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
