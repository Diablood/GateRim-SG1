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
0.2.12-dev - Add hidden Tok'ra cell cache baseline
```

Expected tag after validation and publication:

```text
v0.2.12-dev
```

Publication status at the time this handoff was written:

```text
0.2.12-dev validated locally.
A label-only r1 patch normalizes the French incident name from `cache d'une cellule Tok'ra cachée` to `cache d'une cellule Tok'ra`.
```

Current mod metadata after applying `0.2.12-dev`:

```text
About/About.xml: modVersion = 0.2.12-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.12
```

Reason:

```text
0.2.12-dev adds C# incident-worker code and requires a forced rebuild.
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

## Stable architectural decisions

Do not refactor the mod around Humanoid Alien Races.

The project uses:
- Biotech xenotypes;
- Hediffs;
- intrinsic data and render nodes;
- `PawnKindDef`;
- `FactionDef`;
- targeted C# components.

HAR-like ideas may be implemented natively:
- cultural backstories;
- contextual social thoughts;
- faction-specific generation rules.

Social systems should remain contextual and moderate:
- no automatic attacks;
- no forced permanent relations;
- no absolute social locks unless explicitly designed later.

## Latest validated milestones

### 0.2.8-dev-r1 — Require adult stranded SG-team candidates

The `Équipe SG isolée` scenario no longer offers underage starting candidates.

Implementation:
- `ScenPart_SGTeamStartingGear.AllowPlayerStartingPawn(...)`;
- minimum biological age: `20`;
- still rejects candidates incapable of violence.

### 0.2.9-dev — Add natural Zat'nik'tel acquisition baseline

The Zat'nik'tel is now recoverable naturally but remains rare.

Behavior:
- ordinary Goa'uld Jaffa warriors remain Ma'Tok-only;
- Goa'uld Jaffa guards can generate with either Ma'Tok or Zat'nik'tel;
- settlement guards can also generate with either weapon;
- natural acquisition comes from existing Goa'uld raids and settlements;
- recovered Zats remain usable before research;
- local crafting remains gated by `SG1_JaffaWeaponry`.

### 0.2.10-dev — Add natural Goa'uld queen acquisition baseline

Adds rare natural incident `SG1_GoauldQueenArrival`.

Behavior:
- escaped player-controlled Goa'uld queen;
- duplicate queen prevention across maps and caravans;
- player command to extract one immature Prim'ta symbiote;
- persistent extraction cooldown;
- assisted maturation remains gated by `SG1_GoauldBiotechnology`.

### 0.2.11-dev — Balance queen-origin Prim'ta acquisition

Balances the queen-origin Prim'ta loop.

Updated values:
- queen-arrival `earliestDay`: `45`;
- queen-arrival `baseChance`: `0.015`;
- queen-arrival `minRefireDays`: `90`;
- queen extraction recovery: `180000` ticks, or 3 RimWorld days;
- each extraction produces `1` immature Prim'ta symbiote;
- assisted maturation costs `20` raw meat;
- assisted maturation `workAmount`: `2400`.

### 0.2.12-dev — Add hidden Tok'ra cell cache baseline

Label-only follow-up:
- French player-facing incident label normalized to `cache d'une cellule Tok'ra` to avoid the awkward `cache/cachée` repetition.

Prepared behavior:
- adds incident `SG1_TokraHiddenCellCache`;
- adds worker `IncidentWorker_TokraHiddenCellCache`;
- reuses persistent hidden `SG1_Tokra` faction;
- creates no Tok'ra settlement, world site, trader, recruitment, military aid or raid;
- places a small medical cache near a reachable unfogged map-edge cell;
- can trigger only at neutral, cooperative or trusted Tok'ra trust;
- refuses at wary trust;
- no trust change yet.

Cache contents:
- neutral: `1` tretonin dose + `2` industrial medicine;
- cooperative: `2` tretonin doses + `2` industrial medicine;
- trusted: `2` tretonin doses + `3` industrial medicine.

This milestone changes C# and requires forced rebuild after application.

## Current validated gameplay state

### World factions

Goa'uld:
- visible world faction;
- limited settlements;
- direct natural Jaffa assault raids enabled;
- natural abduction/destruction doctrines still disabled;
- leaders are true Goa'uld System Lord hosts with persistent symbiote identity.

Free Jaffa:
- visible world faction;
- neutral with the SGC expedition by default;
- settlements generated with limited presence;
- peaceful visitor incident exists;
- no automatic Goa'uld forehead mark;
- Free Jaffa visitors use Jaffa lineage, initial Prim'ta, Ma'Tok, modular armor and retractable helmet.

Tok'ra:
- one persistent hidden faction per game;
- no configurable world-creation entry;
- no settlements;
- no natural raids;
- no traders, military aid or automatic quest sites;
- peaceful visitors, therapeutic opportunities and medical-support deliveries reuse the same hidden faction;
- hidden cell cache incident now gives the faction a small non-territorial footprint;
- hidden internal leader is initialized with a persistent Tok'ra symbiote when applicable.

### Research and production

Dedicated research tab:

```text
GateRim SG-1
```

Projects:
- `SG1_JaffaWeaponry` after vanilla `Gunsmithing`;
- `SG1_JaffaArmor` after vanilla `FlakArmor`;
- `SG1_SGFieldEquipment` after vanilla `ComplexClothing`;
- `SG1_GoauldBiotechnology` after vanilla `DrugProduction`.

The research gates local reproduction and construction only.

Objects already captured, supplied, gifted or otherwise acquired remain usable:
- Ma'Tok;
- Zat'nik'tel;
- Jaffa armor;
- SG-team equipment;
- tretonin doses;
- Prim'ta larvae.

### Biological acquisition

Queen-origin loop:

```text
rare queen arrival
-> player-controlled Goa'uld queen
-> 1 immature Prim'ta symbiote every 3 RimWorld days
-> 20 raw meat + Prim'ta incubation basin + SG1_GoauldBiotechnology
-> 1 mature Prim'ta larva
```

The current loop is intentionally modest. Future upgrades should require visible investment.

## Known TODO and deferred work

### Tok'ra content

Implemented:
- hidden persistent Tok'ra faction;
- peaceful visits;
- therapeutic opportunities;
- medical-support deliveries;
- hidden cell cache baseline.

Deferred:
- true hidden Tok'ra world sites or safehouses;
- Tok'ra quest sites;
- richer diplomacy;
- special rewards;
- limited recruitment or host-volunteer events.

### Normal resource acquisition

Partially done:
- Zat'nik'tel rare natural recovery via Goa'uld guards;
- queen-origin Prim'ta loop;
- Tok'ra medical deliveries;
- hidden Tok'ra cell cache.

Still to review later:
- natural tretonin availability pacing after several seasons;
- larva availability outside queen-origin loop;
- whether Free Jaffa visitors should ever give limited supplies;
- whether Goa'uld settlements should contain biological loot.

### Visual/faction polish

Deferred:
- simplified custom faction icons for SGC, Free Jaffa, Goa'uld domains and future factions;
- final Jaffa forehead-mark artwork and placement pass.

## Recommended validation for 0.2.12-dev

Fresh game:
- no Tok'ra settlements appear;
- exactly one hidden Tok'ra faction persists;
- force `SG1_TokraHiddenCellCache`;
- cache appears only at neutral or better trust;
- cache uses `SG1_Tokra`;
- no Tok'ra visitor group, trader, recruitment, military aid or raid appears.

Old save:
- Tok'ra hidden faction migrates if missing;
- cache reuses the migrated faction;
- no duplicate Tok'ra faction is created.

Gameplay:
- cache resolves cleanly;
- rewards are modest;
- save/reload after cache placement remains stable.

Logs:
- no unresolved cross-reference;
- no red errors;
- no accidental debug popup for expected refusal cases.

## Recommended next milestone after 0.2.12 validation

If the cache baseline is stable:

```text
0.2.13-dev - Add hidden Tok'ra world-site prototype
```

Keep it small:
- one temporary hidden safehouse or signal site;
- modest reward;
- no trader yet;
- no recruitment yet;
- no permanent settlement.

Alternative:

```text
0.2.13-dev - Review normal resource acquisition pacing
```

Use this if tretonin, Prim'ta or Goa'uld equipment availability feels off after
playtesting.

## Publication checklist for any milestone

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
