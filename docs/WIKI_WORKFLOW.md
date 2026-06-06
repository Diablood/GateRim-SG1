# Player wiki workflow

## Local workspace layout

Keep the repositories side by side:

```text
GateRim-SG1/
GateRim-SG1.wiki/
```

Do not clone the wiki repository inside the main repository.

## Source of truth

Versioned wiki drafts live in:

```text
GateRim-SG1/docs/wiki/
```

Published player-facing pages live directly at the root of the separate repository:

```text
GateRim-SG1.wiki/
├── Home.md
├── _Sidebar.md
└── ...
```

Do not create a `wiki/` subdirectory inside `GateRim-SG1.wiki/`.

## Status labels

Every player-facing page should contain one of these statuses:

| Status | Meaning |
|---|---|
| `Implémenté` | Available and tested |
| `Prototype` | Available or documented but still evolving |
| `Prévu` | Planned, not yet available in game |

Add the introduction version when relevant.

## Recommended synchronization command on Windows

From the main repository in a PowerShell terminal:

```powershell
.\tools\sync-wiki.cmd
```

The `.cmd` wrapper launches the PowerShell script with a local execution-policy bypass.

You can also run the PowerShell script directly when scripts are allowed:

```powershell
.\tools\sync-wiki.ps1
```

## Recommended synchronization command on Git Bash, Linux or macOS

```bash
./tools/sync-wiki.sh
```

## Manual equivalent on Windows PowerShell

Run from the parent directory containing both repositories:

```powershell
Copy-Item -Path ".\GateRim-SG1\docs\wiki\*" -Destination ".\GateRim-SG1.wiki\" -Recurse -Force
```

## Manual equivalent on Bash

```bash
cp -R GateRim-SG1/docs/wiki/. GateRim-SG1.wiki/
```

## Versioning policy

- Use annotated version tags only in `GateRim-SG1`.
- Use simple documentation commits in `GateRim-SG1.wiki`.
