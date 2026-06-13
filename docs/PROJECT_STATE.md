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

Latest validated local milestone:

```text
0.2.11-dev - Balance queen-origin Prim'ta acquisition
```

Expected tag after publication:

```text
v0.2.11-dev
```

Publication status at the time this handoff was written:

```text
0.2.11-dev validated locally.
If the tag v0.2.11-dev is not present locally/remotely, publish 0.2.11-dev
before starting the next milestone.
```

Current mod metadata after applying `0.2.11-dev`:

```text
About/About.xml: modVersion = 0.2.11-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion remain 0.2.10
```

Reason:

```text
0.2.11-dev is XML-only and does not require a C# assembly version bump.
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

Preserved behavior:
- four candidate slots;
- each slot remains regenerable;
- starting SG equipment is still applied;
- vanilla Earth-compatible backstories and optional SGC adult careers remain possible.

Validated:
- candidates aged `20+`;
- adulthood backstories present;
- regeneration still works;
- violence-capability filter preserved.

### 0.2.9-dev — Add natural Zat'nik'tel acquisition baseline

The Zat'nik'tel is now recoverable naturally but remains rare.

Behavior:
- ordinary Goa'uld Jaffa warriors remain Ma'Tok-only;
- Goa'uld Jaffa guards can generate with either Ma'Tok or Zat'nik'tel;
- settlement guards can also generate with either weapon;
- natural acquisition comes from existing Goa'uld raids and settlements;
- recovered Zats remain usable before research;
- local crafting remains gated by `SG1_JaffaWeaponry`.

This milestone is XML-only.

### 0.2.10-dev — Add natural Goa'uld queen acquisition baseline

Adds the rare natural incident:

```text
SG1_GoauldQueenArrival
```

Initial behavior:
- rare escaped Goa'uld queen arrival;
- player-controlled queen enters from a reachable, unfogged map-edge cell;
- incident refuses if a living player-controlled queen already exists on a map or in a caravan;
- player-controlled queen exposes `Extract immature Prim'ta symbiote`;
- developer-mode access remains available for tests;
- extraction produces `1` `SG1_ImmaturePrimtaSymbiote`;
- cooldown is persistent across save/load;
- assisted maturation remains gated by `SG1_GoauldBiotechnology`.

This milestone changes C# and requires forced rebuild after application.

### 0.2.11-dev — Balance queen-origin Prim'ta acquisition

Balances the queen-origin Prim'ta loop.

Updated values:
- queen-arrival `earliestDay`: `30` -> `45`;
- queen-arrival `baseChance`: `0.02` -> `0.015`;
- queen-arrival `minRefireDays`: `60` -> `90`;
- queen extraction recovery: `60000` ticks -> `180000` ticks;
- recovery equals `3` RimWorld days;
- each extraction still produces `1` immature Prim'ta symbiote;
- assisted maturation raw-meat cost: `10` -> `20`;
- assisted maturation `workAmount`: `1800` -> `2400`;
- `SG1_GoauldBiotechnology` and the Prim'ta incubation basin remain required.

Preserved:
- duplicate queen prevention;
- persistent extraction cooldown;
- developer-mode test access;
- one mature Prim'ta larva per maturation.

This milestone is XML-only and requires no C# rebuild.

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

### Balance and progression

Potential future upgrades for queen-origin production:
- specialized queen chamber;
- advanced Goa'uld biotechnology research;
- power or maintenance requirement;
- higher nutrient cost;
- risk events or containment incidents.

Do not improve queen output for free.

### Tok'ra content

Tok'ra currently have:
- hidden persistent faction;
- peaceful visits;
- therapeutic opportunities;
- medical-support deliveries.

Deferred:
- hidden Tok'ra cells or temporary safehouses;
- Tok'ra quest sites;
- richer diplomacy;
- special rewards;
- limited recruitment or host-volunteer events.

### Normal resource acquisition

Partially done:
- Zat'nik'tel rare natural recovery via Goa'uld guards;
- queen-origin Prim'ta loop;
- Tok'ra medical deliveries.

Still to review later:
- natural tretonin availability pacing;
- larva availability outside queen-origin loop;
- whether Jaffa Free visitors should ever give limited supplies;
- whether Goa'uld settlements should contain biological loot.

### Visual/faction polish

Deferred:
- simplified custom faction icons for SGC, Free Jaffa, Goa'uld domains and future factions;
- final Jaffa forehead-mark artwork and placement pass.

## Recommended next milestone

Recommended branch:

```text
feature/tokra-hidden-cell-site-baseline
```

Recommended milestone:

```text
0.2.12-dev - Add hidden Tok'ra cell-site baseline
```

Rationale:
- Tok'ra world presence already exists but has no world/map footprint;
- adding hidden cells/safehouses is more lore-appropriate than normal Tok'ra settlements;
- this should enrich diplomacy and resources without turning Tok'ra into a territorial faction.

### Proposed 0.2.12 scope

Keep it small and testable.

Add one rare, non-hostile hidden Tok'ra site or cell encounter that:
- uses the existing persistent hidden `SG1_Tokra` faction;
- does not create normal Tok'ra settlements;
- does not enable natural Tok'ra raids;
- is rare and controlled;
- can be found through a storyteller incident, quest/site part or map event;
- despawns/ends cleanly after resolution.

Possible first implementation options:
1. temporary hidden Tok'ra safehouse site;
2. short-lived map encounter with a Tok'ra cell;
3. rare signal/intel incident that creates a small world site.

Preferred first pass:
- choose the simplest implementation compatible with RimWorld 1.6;
- avoid complex multi-stage quests at first;
- do not add recruitment yet;
- do not add a full trader yet;
- do not add military aid yet.

### Suggested rewards for a first cell-site baseline

Keep rewards modest:
- small tretonin cache;
- medical supplies;
- information/flavor letter;
- optionally a small trust increase if the existing Tok'ra trust system supports it cleanly.

Avoid:
- large weapon rewards;
- queen rewards;
- many Prim'ta resources;
- permanent Tok'ra pawns;
- major diplomacy swings.

### 0.2.12 validation checklist

Fresh game:
- no Tok'ra settlements appear;
- exactly one hidden Tok'ra faction persists;
- new cell/site appears only through intended trigger;
- site/event uses `SG1_Tokra`;
- no natural Tok'ra raids occur.

Old save:
- Tok'ra hidden faction migrates if missing;
- new site/event reuses the migrated faction;
- no duplicate Tok'ra faction is created.

Gameplay:
- site/event resolves cleanly;
- rewards are modest;
- no hostile behavior unless explicitly designed;
- save/reload during site/event remains stable.

Logs:
- no unresolved cross-reference;
- no red errors;
- no accidental debug popup for expected refusal cases.

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
