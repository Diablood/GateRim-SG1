# Project consistency checks

Version: `0.3.102-dev`
Status: the main consistency command includes documentation-history,
publication-wording and visual-register safeguards. The finalized Jaffa
helmet-mode command icon is now included in the protected visual whitelist and
wiki-copy checks.

## Purpose

`tools/check-project-consistency.ps1` is the read-only aggregate validation
command for a GateRim SG-1 milestone. It must not rewrite repository files,
create commits or alter the RimWorld installation.

Windows entry point:

```powershell
.\tools\check-project-consistency.cmd
```

Final publication gate:

```powershell
.\tools\check-project-consistency.cmd -RequirePublicationReady
```

## Aggregate checks

The command validates:

- literal tabs and local Markdown links in `README.md` and `docs/**/*.md`;
- the version in `About/About.xml`;
- `Version`, `AssemblyVersion` and `FileVersion` in
  `Source/GateRimSG1/GateRimSG1.csproj`;
- version alignment across the README, current state, current tests, changelog,
  wiki home and content-status page;
- all `BackstoryDef` files, unique `defName` values and the documented count of
  `83` backstories;
- the protected wiki backstory catalogue rows;
- the documentation safeguards through
  `tools/check-documentation-consistency.ps1`;
- the visual inventory and protected wiki images through
  `tools/check-visual-assets.ps1`.

Any failed subprocess makes the aggregate command fail.

## Documentation consistency subprocess

`tools/check-documentation-consistency.ps1` independently checks:

- exactly one changelog heading for every published tag from `v0.3.67-dev`
  onward;
- descending changelog order and a current first heading;
- one durable `TESTING.md` section for the current milestone and every published
  tag from `v0.3.93-dev` onward;
- current-version alignment in the visual register and safeguard documentation;
- arithmetic agreement between the visual-register summaries and marked table;
- absence of visual rows outside the authoritative table;
- absence of volatile branch metadata and explicitly obsolete findings;
- an active `ROADMAP.md` without published-history headings;
- mandatory commands and root-script restrictions in
  `docs/MILESTONE_PUBLICATION.md`.

The negative regression fixtures are run separately through:

```powershell
.\tools\test-documentation-consistency-guards.cmd
```

## Publication-ready mode

`-RequirePublicationReady` forwards the same switch to the documentation
subprocess. It rejects current-state wording that still describes a pending test
or publication and requires:

- `Version de DLL validée` in `docs/TESTING_CURRENT.md`;
- the exact final annotated tag in `docs/PROJECT_STATE.md`;
- a clearly validated or published final state;
- all ordinary consistency checks to remain successful.

This mode must pass before `git add -A` for the final milestone commit.

## Optional explicit expectations

```powershell
.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.101-dev `
  -ExpectedBackstoryCount 83
```

## Complementary checks

The aggregate command complements, but does not replace:

- the forced .NET build;
- the focused RimWorld startup or gameplay validation;
- `git diff --check`;
- manual review of player-facing text and visual presentation.
