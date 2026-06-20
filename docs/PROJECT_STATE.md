# Project state

Current milestone: `0.3.18-dev - Add a Tau'ri origin for generated Tok'ra hosts` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.17-dev`.
- Dedicated branch: `feature/tokra-generated-host-tauri-origin`.
- Validated local archive revision: `0.3.18-dev-r1`.
- Final published tag: `v0.3.18-dev`.
- Final commit: `0.3.18-dev - add a Tau'ri origin for generated Tok'ra hosts`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The configurable generated-host system now supports two historical origins for Tok'ra created already joined with a host:

- `SG1_GeneratedHost_OffworldHuman`, with relative weight `1`;
- `SG1_GeneratedHost_TauriSGCVolunteer`, with relative weight `0.2`.

The Tau'ri origin remains deliberately uncommon. It uses the existing Tau'ri name generator, two dedicated modern-Earth childhoods and the eight validated SGC adult careers. The off-world-human origin remains the clear majority without relying on a hard-coded percentage.

The implementation remains fully data-driven:

- the existing generic C# resolver is unchanged;
- both origins are exposed through XML;
- saved generated identities retain their stored origin and are never rerolled merely because the available-origin list grows;
- real implantations continue to capture the actual pawn and clear any generated historical origin;
- the two new childhoods remain isolated from ordinary human and stranded-SG-team starter childhood pools.

The cultural-backstory catalogue now contains `72` entries.

## Functional validation

The complete focused matrix was validated on local revision `r1`:

- forced rebuild and DLL version `0.3.18.0`;
- clean loading of the origin Defs, backstories, XML patch and French translations;
- generation of both historical origins through `Spawn pawn` → `SG1_TokraVoluntaryHost`;
- off-world-human origins remaining visibly predominant under relative weights `1` and `0.2`;
- correct Tau'ri names, dedicated childhoods and SGC adult careers;
- unchanged off-world-human names and backstory pools;
- stable host/symbiote switching and shared progression without skill drift;
- save/load stability with either personality active;
- preservation of an existing `0.3.17-dev` generated identity without reroll;
- extraction followed by real reimplantation switching to `ImplantedExistingHost` and clearing the generated origin;
- isolation of the two new childhoods from ordinary and stranded-SG-team starter pools;
- clean `Player.log` for the tested scope.

No corrective C# or Def revision was required after `r1`.

## Deferred work recorded

A future dedicated audit must verify skill coverage by culture or race rather than expanding catalogues blindly. It must identify absent or under-represented RimWorld skills for Tau'ri / SGC, Goa'uld-aligned Jaffa, Free Jaffa, Goa'uld hosts, Tok'ra and future cultures such as Asgard, Nox and Unas, then add only culturally coherent backstories where a real gap exists.

This audit is recorded in `docs/ROADMAP.md` and is not part of the functional scope of `0.3.18-dev`.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `1.6/Defs/GeneratedHostOriginDefs/SG1_GeneratedHostOrigins.xml`;
- `1.6/Patches/SG1_TokraGeneratedHostOrigins.xml`;
- `1.6/Defs/BackstoryDefs/SG1_TauriChildhoodBackstories.xml`;
- `Languages/French/DefInjected/BackstoryDef/SG1_TauriChildhoodBackstories.xml`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/wiki/Cultural-Backstories.md`.

Because `docs/wiki/Cultural-Backstories.md` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.18-dev` on a new dedicated `feature/...` branch.

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
