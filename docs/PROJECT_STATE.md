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
0.2.13-dev - Add Tok'ra safehouse signal baseline
```

Expected tag after validation and publication:

```text
v0.2.13-dev
```

Publication status at the time this handoff was written:

```text
0.2.13-dev patch prepared in ChatGPT.
Validate locally before commit/tag/push.
```

Current mod metadata after applying `0.2.13-dev`:

```text
About/About.xml: modVersion = 0.2.13-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.13
```

Reason:

```text
0.2.13-dev adds C# incident-worker and trust-tracker code and requires a forced rebuild.
```

## Environment and conventions

Project:

```text
GateRim SG-1
RimWorld 1.6
Author/public pseudo: Diablood
```

Local developer environment used by the maintainer:

```text
Windows 11
Cursor
PowerShell
.NET
```

Repository rules:
- work in small, testable milestones on dedicated branches;
- use four spaces for indentation where applicable;
- preserve `About/ModIcon.png`;
- keep English Defs and French translations aligned;
- keep README, technical documentation and `docs/wiki/` drafts aligned when behavior changes;
- do not add temporary root patch-note files such as `README-*.txt`;
- do not commit ZIP archives from the repository root.

Build rule for C# changes:
- always force rebuild after C# changes or extracted ZIP patches.

Recommended direct command:

```powershell
dotnet build `
    ".\Source\GateRimSG1\GateRimSG1.csproj" `
    --configuration Release `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

## Latest validated and prepared milestones

### 0.2.8-dev-r1 — Require adult stranded SG-team candidates

The `Équipe SG isolée` scenario rejects underage starting candidates and requires
a minimum biological age of `20`.

### 0.2.9-dev — Add natural Zat'nik'tel acquisition baseline

Goa'uld Jaffa guards can rarely carry Zat'nik'tel weapons. Ordinary warriors
remain Ma'Tok-only. Recovered Zats are usable before research; local crafting
remains gated by `SG1_JaffaWeaponry`.

### 0.2.10-dev — Add natural Goa'uld queen acquisition baseline

Adds rare `SG1_GoauldQueenArrival`, duplicate queen prevention and
player-controlled queen extraction of immature Prim'ta symbiotes.

### 0.2.11-dev — Balance queen-origin Prim'ta acquisition

Current queen loop:
- queen-arrival `earliestDay`: `45`;
- queen-arrival `baseChance`: `0.015`;
- queen-arrival `minRefireDays`: `90`;
- extraction recovery: `180000` ticks, or 3 RimWorld days;
- `1` immature Prim'ta symbiote per extraction;
- maturation costs `20` raw meat and `2400` work.

### 0.2.12-dev-r1 — Add hidden Tok'ra cell cache baseline

Adds `SG1_TokraHiddenCellCache`.

Behavior:
- reuses persistent hidden `SG1_Tokra`;
- no Tok'ra settlement, site, trader, recruitment, military aid or raid;
- neutral: `1` tretonin + `2` industrial medicine;
- cooperative: `2` tretonin + `2` industrial medicine;
- trusted: `2` tretonin + `3` industrial medicine;
- refuses at wary trust;
- French label normalized to `cache d'une cellule Tok'ra`.

### 0.2.13-dev — Add Tok'ra safehouse signal baseline

Prepared behavior:
- adds incident `SG1_TokraSafehouseSignal`;
- adds worker `IncidentWorker_TokraSafehouseSignal`;
- reuses persistent hidden `SG1_Tokra`;
- can trigger only at neutral, cooperative or trusted Tok'ra trust;
- refuses at wary trust;
- applies a tiny `+1` Tok'ra trust gain;
- creates no world site, settlement, caravan, visitor group, trader, recruitment, loot, military aid or raid;
- updates `GameComponent_TokraTrustTracker` with `NotifyHiddenSafehouseSignalAcknowledged()`.

This milestone changes C# and requires forced rebuild after application.

## Current gameplay state

World factions:
- Goa'uld: visible, limited settlements, direct natural Jaffa assault raids, true Goa'uld System Lord leaders.
- Free Jaffa: visible, neutral with SGC by default, settlements and peaceful visitors.
- Tok'ra: one persistent hidden faction, no settlements, no raids, no configurable world-creation entry.

Tok'ra systems:
- peaceful visitors;
- therapeutic opportunities;
- medical-support deliveries;
- hidden cell cache;
- safehouse signal.

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

## Validation checklist for 0.2.13-dev

1. Apply ZIP at repository root.
2. Rebuild C# with `-t:Rebuild`.
3. Start RimWorld and confirm no red errors.
4. Force `SG1_TokraSafehouseSignal` at neutral trust.
5. Confirm a positive letter appears.
6. Confirm Tok'ra trust increases by `+1`.
7. Confirm no site, pawn, caravan, loot, trader, recruitment, military aid or raid is created.
8. Save and reload.
9. Confirm the trust score persists.
10. If possible, set trust to wary and confirm the incident refuses.
11. Confirm no duplicate Tok'ra faction is created.

## Recommended next milestone after 0.2.13 validation

Preferred:

```text
0.2.14-dev - Add hidden Tok'ra world-site prototype
```

Scope:
- one temporary hidden safehouse or signal site;
- no trader yet;
- no recruitment yet;
- modest reward only;
- no permanent settlement.

Alternative:

```text
0.2.14-dev - Review normal resource acquisition pacing
```

Use this if tretonin, Prim'ta or Goa'uld equipment availability feels off after playtesting.

## Publication checklist

Before commit:
- run RimWorld startup test;
- run targeted developer-tool tests;
- inspect `Player.log`;
- update README;
- update `docs/CHANGELOG.md`;
- update relevant technical docs under `docs/`;
- update `docs/wiki/` drafts;
- update this `docs/PROJECT_STATE.md` handoff.

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
