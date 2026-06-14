# GateRim SG-1 — Current project state

## Purpose

This file is the primary handoff document for Codex or any other coding agent.

Before starting a task:
- read this file;
- read `AGENTS.md`;
- verify the current Git branch and tags;
- do not switch to `main`, because `main` is not a usable working base for this repository;
- create new work from the latest validated branch or explicit `v...-dev` tag.

## Current known baseline

Latest prepared milestone:

```text
0.2.15-dev - Add Tok'ra safehouse lead cache baseline
```

Expected tag after validation and publication:

```text
v0.2.15-dev
```

Publication status at the time this handoff was written:

```text
0.2.15-dev patch prepared in ChatGPT.
Validate locally before commit/tag/push.
```

Current mod metadata after applying `0.2.15-dev`:

```text
About/About.xml: modVersion = 0.2.15-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.15
```

Reason:

```text
0.2.15-dev adds C# incident-worker code and lead-consumption code.
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
- triggers only at neutral/cooperative/trusted trust;
- refuses at wary trust;
- applies `+1` Tok'ra trust;
- creates no world site, settlement, caravan, visitor, trader, recruitment, loot, military aid or raid.

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

Prepared behavior:
- adds `SG1_TokraSafehouseLeadCache`;
- adds `IncidentWorker_TokraSafehouseLeadCache`;
- adds `GameComponent_TokraSafehouseLeadTracker.TryConsumeSafehouseLead(...)`;
- requires at least `1` stored safehouse lead;
- consumes `1` lead on success;
- reuses persistent hidden `SG1_Tokra`;
- excludes wary trust;
- places a modest medical cache:
  - `2` tretonin doses;
  - `3` industrial medicine;
- creates no world site, generated map, pawn, caravan, visitor, trader, recruitment, military aid or raid.

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
- safehouse lead follow-up cache.

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

## Validation checklist for 0.2.15-dev

1. Apply ZIP at repository root.
2. Rebuild C# with `-t:Rebuild`.
3. Start RimWorld and confirm no red errors.
4. Force `SG1_TokraSafehouseLeadCache` at `0` leads and confirm it refuses.
5. Force `SG1_TokraSafehouseSignal` to store at least one lead.
6. Force `SG1_TokraSafehouseLeadCache`.
7. Confirm one lead is consumed.
8. Confirm `2` tretonin doses and `3` industrial medicine appear.
9. Confirm no world site, generated map, pawn, caravan, trader, recruitment, military aid or raid appears.
10. Save and reload.
11. Confirm consumed lead count persists.
12. Confirm no duplicate Tok'ra faction is created.

## Recommended next milestone after 0.2.15 validation

Preferred:

```text
0.2.16-dev - Add hidden Tok'ra world-site prototype
```

The site milestone can consume one stored safehouse lead and create a temporary
non-hostile world marker or small safehouse site.

Keep it small:
- consume 1 lead;
- create one temporary hidden site or marker;
- no trader yet;
- no recruitment yet;
- no permanent settlement;
- modest reward only.

Alternative:

```text
0.2.16-dev - Review normal resource acquisition pacing
```

Use this if tretonin, Prim'ta or Goa'uld equipment availability feels off after
playtesting.

## Publication checklist

Before commit:
- verify the current branch with `git branch --show-current`;
- if needed, create a dedicated branch, for example `git switch -c feature/tokra-safehouse-lead-cache-baseline`;
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
git add .
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
git add .
git commit -m "<version> - Update wiki"
git push origin master

Set-Location ..\GateRim-SG1
```
