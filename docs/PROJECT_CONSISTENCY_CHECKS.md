# Project consistency checks

Version: `0.3.20-dev`

Status: validated and published in `0.3.20-dev`.

## Purpose

`tools/check-project-consistency.ps1` prevents public and technical project metadata from drifting between milestones. The command is read-only: it does not rewrite files, create commits or modify the game installation.

The Windows entry point is:

```powershell
.\tools\check-project-consistency.cmd
```

The script is compatible with the Windows PowerShell 5.1 parser used by the `.cmd` wrapper. Error messages use explicit formatting where punctuation would otherwise be parsed as part of a variable reference.

## Validated behavior

The complete local validation confirms that:

- a correct repository state returns exit code `0` and reports the expected version, assembly and backstory count;
- an intentionally wrong expected version returns a non-zero exit code with an explicit `[FAIL]` message;
- a positive rerun succeeds immediately after the negative test;
- no positive or negative execution modifies repository files;
- the Windows PowerShell 5.1 parser accepts the final `r2` implementation.

## Version checks

The command reads `About/About.xml` as the authoritative development version and verifies that it matches:

- `Version`, `AssemblyVersion` and `FileVersion` in `Source/GateRimSG1/GateRimSG1.csproj`;
- the development version in `README.md`;
- the documented version in `docs/wiki/Home.md`;
- the latest revision in `docs/wiki/Content-Status.md`;
- the current milestone in `docs/PROJECT_STATE.md`;
- the active test milestone and expected DLL in `docs/TESTING_CURRENT.md`;
- the newest heading in `docs/CHANGELOG.md`.

The assembly version is derived from the development version. For example, `0.3.20-dev` requires `0.3.20.0` in the project and test plan.

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
.\tools\check-project-consistency.cmd `
    -ExpectedVersion 0.3.20-dev `
    -ExpectedBackstoryCount 83
```

A mismatch returns a non-zero exit code. This allows the check to be used from PowerShell, Cursor tasks or a future continuous-integration workflow.

## Publication rule

Run the command after the final documentary pass and before `git add -A`. Run it again after extracting any final archive that changes versions, public pages, backstories or the backstory catalogue.

The check complements `git diff --check`; it does not replace the forced RimWorld rebuild, in-game loading test or manual review of player-facing text.
