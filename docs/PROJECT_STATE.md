# GateRim SG-1 — Current project state

## Purpose

This file is the primary handoff document for Codex or any other coding agent.

Before starting a task:
- read this file;
- read `AGENTS.md`;
- verify the current Git branch and tags;
- do not switch to `main`, because `main` is not a usable working base for this repository;
- create new work from the latest validated branch or explicit `v...-dev` tag.

## Latest validated baseline

Latest validated published tag:

```text
v0.2.16-dev - Add hidden Tok'ra safehouse world marker
```

Current working branch:

```text
feature/tokra-hidden-safehouse-world-marker
```

## Current validated milestone

Latest validated local milestone:

```text
0.2.16-dev - Add hidden Tok'ra safehouse world marker
```

Published tag:

```text
v0.2.16-dev
```

Publication status at the time this handoff was written:

```text
0.2.16-dev repaired locally after an incomplete browser-generated attempt.
Forced C# rebuild and XML validation passed.
Manual RimWorld tests passed on June 14, 2026.
Published as v0.2.16-dev on June 14, 2026.
```

Current mod metadata after applying `0.2.16-dev`:

```text
About/About.xml: modVersion = 0.2.16-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.16
```

Reason:

```text
0.2.16-dev adds C# incident-worker and WorldObject code.
A forced rebuild is required.
```

## Environment and conventions

Project:
- GateRim SG-1
- RimWorld 1.6
- Author/public pseudo: Diablood

Developer environment:
- Windows 11
- Cursor
- PowerShell
- .NET

Rules:
- work in small, testable milestones on dedicated branches;
- use four spaces for indentation where applicable;
- preserve `About/ModIcon.png`;
- keep English Defs and French translations aligned;
- update README, technical docs, wiki drafts and this file when behavior changes;
- do not add temporary root `README-*.txt` files;
- do not commit ZIP archives from the repository root.

C# build command:

```powershell
dotnet build `
    ".\Source\GateRimSG1\GateRimSG1.csproj" `
    --configuration Release `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

## Recent Tok'ra milestones

### 0.2.12-dev-r1 — Hidden Tok'ra cell cache baseline

Adds `SG1_TokraHiddenCellCache`.

Behavior:
- reuses persistent hidden `SG1_Tokra`;
- no settlement, site, trader, recruitment, military aid or raid;
- neutral: `1` tretonin + `2` industrial medicine;
- cooperative: `2` tretonin + `2` industrial medicine;
- trusted: `2` tretonin + `3` industrial medicine;
- refuses at wary trust;
- French label: `cache d'une cellule Tok'ra`.

### 0.2.13-dev — Tok'ra safehouse signal baseline

Adds `SG1_TokraSafehouseSignal`.

Behavior:
- reuses persistent hidden `SG1_Tokra`;
- triggers only at neutral, cooperative or trusted trust;
- refuses at wary trust;
- applies `+1` Tok'ra trust;
- creates no world site, settlement, caravan, visitor, trader, recruitment,
  loot, military aid or raid.

### 0.2.14-dev-r2 — Tok'ra safehouse lead tracker baseline

Adds `GameComponent_TokraSafehouseLeadTracker`.

Behavior:
- persists `tokraSafehouseLeadCount`;
- safehouse signal stores `+1` lead on success;
- maximum provisional lead count: `3`;
- signal still grants `+1` Tok'ra trust;
- signal letter shows lead progress;
- no world site is created yet;
- r1 fixed missing `using RimWorld`;
- r2 improved the player-facing full-lead message.

### 0.2.15-dev — Tok'ra safehouse lead cache baseline

Adds `SG1_TokraSafehouseLeadCache`.

Behavior:
- adds `IncidentWorker_TokraSafehouseLeadCache`;
- adds `GameComponent_TokraSafehouseLeadTracker.TryConsumeSafehouseLead(...)`;
- requires at least `1` stored lead;
- consumes `1` stored lead;
- reuses persistent hidden `SG1_Tokra`;
- excludes wary trust;
- spawns `2` tretonin and `3` industrial medicine;
- creates no world site, generated map, pawn, caravan, visitor, trader,
  recruitment, military aid or raid.

### 0.2.16-dev — Hidden Tok'ra safehouse world marker

Validated behavior:
- adds `SG1_TokraHiddenSafehouseMarker`;
- adds `WorldObject_TokraHiddenSafehouseMarker`;
- adds `SG1_TokraHiddenSafehouseWorldMarker`;
- adds `IncidentWorker_TokraHiddenSafehouseWorldMarker`;
- requires at least `1` stored safehouse lead;
- consumes `1` lead on success;
- reuses persistent hidden `SG1_Tokra`;
- excludes wary trust;
- creates one temporary non-hostile world marker near the colony;
- marker duration: `300000` ticks, or 5 RimWorld days;
- prevents duplicate active markers;
- creates no generated map, loot, pawn, caravan, visitor, trader, recruitment, military aid or raid.

## Current gameplay state

Tok'ra:
- one hidden persistent faction;
- no normal settlements;
- no natural raids;
- no configurable world-creation entry;
- peaceful visitors;
- therapeutic opportunities;
- medical-support deliveries;
- hidden cell cache;
- safehouse signal;
- safehouse lead tracker;
- safehouse lead follow-up cache;
- temporary hidden safehouse world marker.

Research:
- dedicated `GateRim SG-1` tab;
- `SG1_JaffaWeaponry`;
- `SG1_JaffaArmor`;
- `SG1_SGFieldEquipment`;
- `SG1_GoauldBiotechnology`.

Biological acquisition:
- rare queen arrival;
- one immature Prim'ta symbiote every 3 days from a player queen;
- maturation costs 20 raw meat and requires Goa'uld biotechnology.

## Validation checklist for 0.2.16-dev

Result: passed in RimWorld on June 14, 2026.

1. Rebuild C# with `-t:Rebuild`.
2. Start RimWorld and confirm no red errors.
3. Force `SG1_TokraHiddenSafehouseWorldMarker` at `0` leads and confirm it refuses.
4. Force `SG1_TokraSafehouseSignal` to store at least one lead.
5. Force `SG1_TokraHiddenSafehouseWorldMarker`.
6. Confirm one lead is consumed.
7. Confirm a world marker appears near the colony.
8. Confirm the marker is selectable and says it cannot be entered yet.
9. Confirm no generated map, loot, pawn, caravan, trader, recruitment, military aid or raid appears.
10. Save and reload with the marker active.
11. Confirm the marker remains.
12. Let time pass until expiration and confirm it disappears.
13. Confirm forcing another marker while one exists refuses.
14. Confirm no duplicate Tok'ra faction is created.

## Recommended next milestone after 0.2.16 validation

Preferred:

```text
0.2.17-dev - Add enterable hidden Tok'ra safehouse site prototype
```

Keep it small:
- consume 1 lead;
- create one temporary site;
- generate a small non-hostile map or simple reward flow;
- no trader yet;
- no recruitment yet;
- no permanent settlement;
- no combat by default.

Alternative:

```text
0.2.17-dev - Review normal resource acquisition pacing
```

Use this if tretonin, Prim'ta or Goa'uld equipment availability feels off after
playtesting.

## Publication checklist

Before commit:
- verify the current branch with `git branch --show-current`;
- if needed, create a dedicated branch, for example `git switch -c feature/tokra-hidden-safehouse-world-marker`;
- run RimWorld startup test;
- run targeted developer-tool tests;
- inspect `Player.log`;
- update README;
- update `docs/CHANGELOG.md`;
- update relevant technical docs under `docs/`;
- update `docs/wiki/` drafts;
- update this `docs/PROJECT_STATE.md`.

Commit:

```powershell
git status --short
git add <files reviewed for the milestone>
git commit -m "<version> - <description>"
```

Tag:

```powershell
git tag -a v<version> -m "<version> - <description>"
```

Push:

```powershell
git push -u origin HEAD
git push origin v<version>
```

Wiki:

```powershell
.\tools\sync-wiki.cmd

Set-Location ..\GateRim-SG1.wiki
git status --short
git diff --check
git diff --stat
git diff
git add <wiki files reviewed for publication>
git commit -m "<version> - Update wiki"
git push origin master

Set-Location ..\GateRim-SG1
```
