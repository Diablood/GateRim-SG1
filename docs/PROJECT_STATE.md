# Project state

Current milestone: `0.3.16-dev - Expand cultural backstory variety` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.15-dev`.
- Dedicated branch: `feature/cultural-backstory-expansion`.
- Validated local archive revision: `0.3.16-dev-r1`.
- Final published tag: `v0.3.16-dev`.
- Final commit: `0.3.16-dev - expand cultural backstory variety`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

GateRim SG-1 now defines `70` native RimWorld `BackstoryDef` entries, up from `58` before this milestone.

The twelve additions are distributed evenly across cultures already implemented in game:

- two Tau'ri / SGC adult careers;
- two shared Jaffa childhoods;
- two Goa'uld-aligned Jaffa adult careers;
- two Free Jaffa adult careers;
- two ordinary Goa'uld-host adult careers;
- two Tok'ra adult careers.

The new content remains fully data-driven:

- one dedicated BackstoryDef XML file;
- one matching French DefInjected file;
- one XML patch extending the existing starter-profile pools and cultural name rules;
- no change to the shared C# resolver, starter-generation engine, naming engine or reversible Tok'ra skill-offset logic.

No Asgard, Nox or Unas backstory was added before those cultures exist as playable or generated content.

## Functional validation

The complete focused matrix was validated on local revision `r1`:

- forced rebuild and DLL version `0.3.16.0`;
- main-menu loading without new XML, patch, BackstoryDef or translation errors;
- all twelve French titles, descriptions and moderate skill bonuses;
- Jaffa starter childhood and adulthood pools, including matching `GoauldJaffa` and `FreeJaffa` name groups;
- Goa'uld-host starter careers, including matching `Goauld` and `Tokra` name groups;
- the stranded SG-team scenario with eight exclusive SGC adult careers;
- ordinary human starters retaining a clear vanilla majority while allowing occasional SGC careers;
- representative Jaffa, Goa'uld-host, Tok'ra, raid and world-pawn generation;
- save/load persistence, manual-name preservation and Tok'ra personality switching without skill drift;
- the complete `70`-entry wiki catalogue matching the French in-game content;
- clean `Player.log` for the tested scope.

No corrective `r2` code or Def revision was required.

## Architecture decision

This milestone confirms the preferred content-extension path for existing cultures: add passive Defs and extend explicit profile lists through XML while the current schema remains sufficient.

The generic cultural framework must not gain a new matcher, branch or special case merely to expand a catalogue. A C# change remains justified only when a real culture-dependent rule cannot be represented accurately by the existing Def model.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `1.6/Defs/BackstoryDefs/SG1_CulturalBackstoryExpansion.xml`;
- `1.6/Patches/SG1_CulturalBackstoryExpansion.xml`;
- `Languages/French/DefInjected/BackstoryDef/SG1_CulturalBackstoryExpansion.xml`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/wiki/Cultural-Backstories.md`.

Because `docs/wiki/Cultural-Backstories.md` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.16-dev` on a new dedicated `feature/...` branch.

Before selecting it, reread:

- `AGENTS.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- `docs/TESTING_CURRENT.md`.

## Repository rules reminder

- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only one final tag per milestone, without an `-rN` suffix.
- Update procedure files in the same milestone whenever a durable workflow improvement is discovered.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
