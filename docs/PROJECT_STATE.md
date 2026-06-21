# Project state

Current milestone: `0.3.20-dev - Add automated project consistency checks` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.19-dev`.
- Dedicated branch: `feature/project-consistency-checks`.
- Validated local archive revision: `0.3.20-dev-r2`.
- Final published tag: `v0.3.20-dev`.
- Final commit: `0.3.20-dev - add automated project consistency checks`.

The local `r2` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The project now includes a read-only PowerShell consistency checker:

- `tools/check-project-consistency.ps1` contains the audit logic;
- `tools/check-project-consistency.cmd` provides the Windows entry point;
- `About/About.xml` remains the authoritative development version;
- project, README, wiki and tracking versions are compared automatically;
- the assembly version is derived from the `x.y.z-dev` milestone version;
- every cultural `BackstoryDef` is counted and checked for missing or duplicate `defName` values;
- the real backstory count is compared with public summaries, technical documentation and the wiki catalogue;
- optional explicit expectations support safe positive and negative tests without editing repository files.

The public presentation drift detected after `0.3.19-dev` is corrected: README and wiki pages now consistently display `0.3.20-dev` and `83` cultural backstories.

Revision `r1` exposed a Windows PowerShell 5.1 parser error caused by a colon immediately following an interpolated variable. Revision `r2` replaced that interpolation with explicit format-operator syntax and is the validated implementation.

## Functional validation

The complete focused protocol was validated on local revision `r2`:

- positive consistency check with expected version `0.3.20-dev` and `83` backstories;
- summary reporting assembly version `0.3.20.0`;
- zero exit code on the valid repository state;
- intentional wrong-version check returning a non-zero exit code and an explicit failure;
- successful positive rerun after the negative test;
- repository working tree unchanged by every checker execution;
- forced rebuild with DLL version `0.3.20.0`;
- RimWorld loading to the main menu with mod version `0.3.20-dev`;
- no new GateRim SG-1 error in `Player.log` for the tested scope;
- README, wiki home and content-status page aligned to `83` backstories.

No gameplay, Def, translation or texture behavior was changed.

## Durable outcome

`docs/MILESTONE_PUBLICATION.md` now requires the consistency checker before the final commit. Future milestones that change a controlled version or backstory-count format must update both the checker and `docs/PROJECT_CONSISTENCY_CHECKS.md` instead of bypassing the check.

The checker complements rather than replaces:

- `git diff --check`;
- the forced RimWorld rebuild;
- the main-menu loading test;
- manual review of player-facing text and wiki navigation.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `README.md`;
- `tools/check-project-consistency.ps1`;
- `tools/check-project-consistency.cmd`;
- `docs/PROJECT_CONSISTENCY_CHECKS.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- `docs/wiki/Home.md`;
- `docs/wiki/Content-Status.md`.

Because files under `docs/wiki/` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.20-dev` on a new dedicated `feature/...` branch.

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
- Run the project consistency checker before every final commit.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
