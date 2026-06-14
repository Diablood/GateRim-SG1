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
v0.2.17-dev - Add enterable hidden Tok'ra safehouse site
```

Current working branch:

```text
feature/tokra-safehouse-contact-prototype
```

## Current development milestone

Current local milestone:

```text
0.2.18-dev - Add a non-trading Tok'ra safehouse contact prototype
```

Published tag:

```text
v0.2.17-dev
```

Development status at the time this handoff was written:

```text
0.2.18-dev prepared locally from the published v0.2.17-dev tag.
The C# implementation compiles against RimWorld 1.6.
Manual RimWorld validation is pending.
The branch, commit and tag have not been published.
```

Current mod metadata after applying `0.2.18-dev`:

```text
About/About.xml: modVersion = 0.2.18-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.18
```

Reason:

```text
0.2.18-dev adds a custom C# map-generation step and a contact-verification
developer action.
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

### 0.2.17-dev — Enterable hidden Tok'ra safehouse site

Validated behavior:
- adds `SG1_TokraHiddenSafehouseSiteIncident`;
- adds `IncidentWorker_TokraHiddenSafehouseSite`;
- adds `SG1_TokraHiddenSafehouseSite` based on the vanilla `Site` world object;
- adds `SG1_TokraHiddenSafehouseSitePart` based on the vanilla item-stash site
  worker and generation step;
- requires and consumes `1` stored safehouse lead;
- reuses persistent hidden `SG1_Tokra`;
- excludes wary trust;
- creates one temporary site 5 to 12 world tiles from the colony;
- generates a reduced `120 x 120` non-hostile map when visited;
- places `2` tretonin doses and `4` industrial medicine;
- expires after `600000` ticks, or 10 RimWorld days, if unvisited;
- prevents a safehouse marker and safehouse site from coexisting;
- creates no hostile pawn, trader, recruitment, military aid, raid or
  permanent settlement.

### 0.2.18-dev — Non-trading Tok'ra safehouse contact

Current implementation awaiting manual validation:
- adds `GenStep_TokraHiddenSafehouseContact` after the medical stash;
- generates exactly one `SG1_TokraVoluntaryHost` on the safehouse map;
- reuses the persistent hidden `SG1_Tokra` faction;
- assigns a three-day peaceful visit duty so the contact remains on site;
- raises the voluntary-host minimum generation age from 18 to 20;
- requires an adulthood backstory through the existing PawnKind filter;
- sets the generated contact as non-recruitable;
- gives the contact no trader role or trade inventory;
- adds `Verify Tok'ra safehouse contact test` under the `GateRim SG-1` debug
  actions;
- preserves the existing cache, timeout and clean-departure design.

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
- temporary hidden safehouse world marker;
- temporary enterable hidden safehouse site;
- one peaceful non-trading contact on the generated safehouse map.

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

## Validation checklist for 0.2.18-dev

Result: pending manual RimWorld validation.

Mandatory happy-path test:

1. After rebuilding, fully close and restart RimWorld. Enable developer mode
   under `Options > General > Development mode`, then load a colony.
2. On the colony map, open `Debug actions` and choose
   `GateRim SG-1 > Prepare Tok'ra safehouse site test`.
   Expected: a green message confirms neutral trust, `1` stored lead and no
   inactive safehouse marker or site.
3. Open `Debug actions` and choose
   `GateRim SG-1 > Create Tok'ra safehouse test site`.
   Expected: a positive letter, lead count `0/3`, and exactly one world site.
4. Form a caravan with at least one colonist and send it to the safehouse.
   Expected: the available action is a visit, not an attack.
5. On arrival, open `Debug actions` and choose
   `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
   Expected in French: a green message reports `Contacts : 1`, age at least
   `20`, `histoire adulte : True`, `hostile : False`, `marchand : False` and
   `recrutable : False`.
6. Confirm the cache still contains exactly `2` tretonin doses and `4`
   industrial medicine.
7. Save and reload while the safehouse map is open, then run
   `GateRim SG-1 > Verify Tok'ra safehouse contact test` again.
   Expected: the report remains green and no red error appears.
8. Use `Reform caravan` and leave.
   Expected: the temporary map and world site disappear cleanly.

Optional regression tests:

1. With `0` leads and no active site, run
   `GateRim SG-1 > Create Tok'ra safehouse test site` again.
   Expected: no site appears.
2. On a map other than the generated safehouse map, run
   `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
   Expected: a red message asks to open the generated safehouse map.
3. Prepare, create and visit a second site after leaving the first one.
   Expected: exactly one contact appears and passes the same verification
   action.

## Recommended next milestone after 0.2.18 validation

Preferred:

```text
0.2.19-dev - Add one minimal Tok'ra safehouse contact interaction
```

Keep it small:
- expose at most one explicit player interaction;
- keep no trader inventory or recruitment;
- avoid repeatable reward farming;
- keep no military aid, raid or permanent settlement;
- preserve clean site departure and removal.

Alternative:

```text
0.2.19-dev - Review safehouse reward and travel pacing
```

Use this if the travel cost, ten-day timeout or medical reward feels wrong
after playtesting.

## Publication checklist

Before commit:
- verify the current branch with `git branch --show-current`;
- if needed, create a dedicated branch, for example `git switch -c feature/tokra-enterable-safehouse-site-prototype`;
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
