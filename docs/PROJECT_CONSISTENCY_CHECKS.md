# Project consistency checks

Version: `0.3.90-dev`

Status: extended through `0.3.90-dev` with mandatory visual-asset and approved-wiki-copy validation.

## Purpose

`tools/check-project-consistency.ps1` prevents public and technical project metadata from drifting between milestones. The command is read-only: it does not rewrite files, create commits or modify the game installation.

The Windows entry point is:

```powershell
./tools/check-project-consistency.cmd
```

The script is compatible with the Windows PowerShell 5.1 parser used by the `.cmd` wrapper. Error messages use explicit formatting where punctuation would otherwise be parsed as part of a variable reference.

## Markdown command-path checks

The checker scans `README.md` and every Markdown file under `docs/` for literal tab characters. A tab causes a non-zero exit code and reports the affected file and line.

Repository-relative PowerShell command paths in Markdown must use forward slashes, for example `./tools/check-project-consistency.cmd`. This prevents a generated or copied `\t` sequence from becoming a tab and corrupting commands such as `./tools/...`.

## Markdown link checks

Since `0.3.63-dev`, the checker validates local Markdown targets in `README.md`
and every document under `docs/`. Repository paths beginning with `docs/` are
resolved from the repository root; ordinary relative paths are resolved from
their source document. Extensionless links in `docs/wiki/` resolve to the
matching `.md` page. External, mail, Steam and same-page anchor links are left
to their respective renderers.

This check makes removal or renaming of a technical document or wiki draft fail
before publication when a Markdown link still points to it.

## Visual asset checks

Since `0.3.85-dev`, the project consistency command launches
`tools/check-visual-assets.ps1` as a mandatory subprocess. The dedicated checker
can also be run directly through:

```powershell
./tools/check-visual-assets.cmd
```

It verifies:

- every PNG under `Textures/` has a valid PNG signature;
- directional and body-type variants normalize to one canonical family;
- every family and physical-file count matches
  `docs/VISUAL_ASSET_REGISTER.md`;
- every local family has at least one direct XML or C# reference;
- no direct local texture path is missing;
- every direct vanilla texture path is registered and still referenced;
- `About/ModIcon.png` remains present;
- every protected gameplay icon copied into `docs/wiki/images/` remains byte-identical to its gameplay PNG and is referenced by the intended wiki page;
- the exact final-family whitelist matches the explicitly approved storyteller, xenotype, gameplay-gene, world-faction and event-site families.

The checker is read-only. Any added, removed, renamed or newly referenced visual
asset must update the register in the same revision.

## Previously validated behavior

The `0.3.20-dev` local validation confirmed that:

- a correct repository state returns exit code `0` and reports the expected version, assembly and backstory count;
- an intentionally wrong expected version returns a non-zero exit code with an explicit `[FAIL]` message;
- a positive rerun succeeds immediately after the negative test;
- no positive or negative execution modifies repository files;
- the Windows PowerShell 5.1 parser accepts the final `0.3.20-dev-r2` implementation.

## Version checks

The command reads `About/About.xml` as the authoritative development version and verifies that it matches:

- `Version`, `AssemblyVersion` and `FileVersion` in `Source/GateRimSG1/GateRimSG1.csproj`;
- the development version in `README.md`;
- the documented version in `docs/wiki/Home.md`;
- the latest revision in `docs/wiki/Content-Status.md`;
- the current milestone in `docs/PROJECT_STATE.md`;
- the active test milestone and expected DLL in `docs/TESTING_CURRENT.md`;
- the newest heading in `docs/CHANGELOG.md`.

The assembly version is derived from the development version. For example, `0.3.21-dev` requires `0.3.21.0` in the project and test plan.


## Preparatory and final wording

A controlled document can legitimately change wording between the testing state and the final tagged state. The DLL-version check therefore accepts both:

```text
Version de DLL attendue : `x.y.z.0`
Version de DLL validée : `x.y.z.0`
```

Both forms must still contain the exact assembly version derived from `About/About.xml`. Missing values produce one explicit failure rather than a second redundant empty-value mismatch.

## Backstory checks

The command loads every XML file under `1.6/Defs/BackstoryDefs` and:

- counts every `BackstoryDef`;
- reports missing or duplicate `defName` values;
- compares the real Def count with the counts shown in the README, wiki home, content-status page, technical backstory document and wiki catalogue;
- counts player-facing table rows between `BACKSTORY_TABLES_START` and `BACKSTORY_TABLES_END` in `docs/wiki/Cultural-Backstories.md`.

This makes a future backstory addition fail the publication check until all required public and technical summaries are updated in the same milestone.

## Optional explicit expectations

The normal command derives its expectations from repository files. A milestone can additionally require exact values:

```powershell
./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.21-dev `
    -ExpectedBackstoryCount 83
```

A mismatch returns a non-zero exit code. This allows the check to be used from PowerShell, Cursor tasks or a future continuous-integration workflow.

## Publication rule

Run the command after the final documentary pass and before `git add -A`. Run it again after extracting any final archive that changes versions, public pages, backstories or the backstory catalogue.

The check complements `git diff --check`; it does not replace the forced RimWorld rebuild, in-game loading test or manual review of player-facing text.
