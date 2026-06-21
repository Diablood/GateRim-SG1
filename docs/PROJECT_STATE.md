# Project state

Current milestone: `0.3.23-dev - Introduce reusable mission framework foundations` — validated locally on revision `r2` and published under the final tag `v0.3.23-dev`.

## Last completed milestone

- Development base tag: `v0.3.22-dev`.
- Dedicated branch: `feature/mission-framework-foundation`.
- Validated local archive revision: `0.3.23-dev-r2`.
- Published final tag: `v0.3.23-dev`.
- Validated assembly version: `0.3.23.0`.
- Cultural backstory count remains `83`.

The abandoned visual-only observation-device milestone was not committed or tagged. Its branch was cleaned, and this functional milestone started again from `v0.3.22-dev`.

## Milestone objective

Introduce the first reusable mission and questline toolbox without attempting to generalize every existing operation at once.

The foundation provides:

- `GateRimMissionDef` entries driven by XML;
- reusable timing, recurrence, difficulty, text-bank, reward, phase, objective, condition, transition and consequence data;
- persistent generic runtime data stored beside specialized operation state;
- RP offer variants with local anti-repetition across occurrences;
- RimWorld threat-point snapshots captured when a configured mission is offered;
- per-mission repeated-archetype penalties;
- generic debug inspection of all loaded mission definitions;
- an abstract C# worker extension point for eligibility, offer, acceptance, periodic updates and mechanics that do not fit the common vocabulary.

## Pilot migration

The recurring Tok'ra Goa'uld-observation operation is the first XML-backed pilot:

- its existing player flow and specialized worker remain unchanged;
- timing, trust-tier weights, repeat penalty, objective, action keys, rewards and three offer texts come from `SG1_TokraOrganic_GoauldObservation`;
- the most recently used offer variant is persisted and excluded from the next draw when alternatives exist;
- its current RimWorld threat points are captured in the generic occurrence data for future adaptive mechanics;
- an old-save observation occurrence receives generic runtime data during load without rerolling or restarting the operation;
- the observation work duration is read from the `observing` phase's `MaintainOperator` objective and is set to `10000` ticks, or four in-game hours;
- the previous hard-coded observation definition remains as a safety fallback if the Def is unavailable.

The detailed observation sequence is still executed by its proven specialized C# code; the XML phase graph is validated data and is not yet interpreted by a universal transition engine. The intelligence recovery, wounded-agent care and medical handoff operations remain on their existing C# definitions during this milestone. This deliberate mixed mode validates gradual migration and save compatibility without over-generalizing from one operation.

## Validation completed

The complete protocol in `docs/TESTING_CURRENT.md` was validated on local revision `r2`:

- project consistency and forced rebuild passed with assembly `0.3.23.0`;
- mission Defs, translations and the generic debug report loaded without error;
- all three observation offer variants appeared and immediate text repetition was prevented;
- the complete observation flow succeeded with the new four-hour duration remaining visible at maximum speed;
- save/reload preserved offered, accepted and active observation occurrences without rerolling or extending existing progress;
- threat snapshots were positive and increased on the materially stronger test colony;
- intelligence recovery, wounded-agent care and medical handoff retained their legacy C# flows;
- no technical data leaked into player-facing texts or tools outside the intended debug boundary;
- `Player.log` remained clean for the tested scope.

## Next development step

Start the next milestone explicitly from `v0.3.23-dev` on a new dedicated branch.

The next framework evolution should migrate a second real mission or operation rather than adding theoretical abstractions. Prefer a candidate that exercises another shared capability—especially adaptive threat consumption—when that capability belongs naturally to its gameplay. The exact candidate and milestone scope should be selected before creating the branch.

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
- `Source/GateRimSG1/Missions/*.cs`;
- adapted Tok'ra organic-operation framework, manager and instance files;
- `1.6/Defs/MissionDefs/SG1_MissionFramework.xml`;
- English and French keyed mission-framework texts;
- `README.md`;
- `docs/MISSION_FRAMEWORK.md`;
- project state, roadmap, current and durable tests, changelog and publication procedure;
- wiki home and content-status revisions.

## Repository rules reminder

- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only one final tag per milestone, without an `-rN` suffix.
- Update procedure files in the same milestone whenever a durable workflow improvement is discovered.
- Use `/` in repository-relative PowerShell paths written in Markdown.
- Run the project consistency checker before every final commit.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
