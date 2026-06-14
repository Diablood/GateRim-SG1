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

Latest validated published milestone:

```text
0.2.21-dev - Add Tok'ra safehouse medical briefing outcome
```

Validated behavior:

```text
The generated Tok'ra safehouse contact is non-hostile, non-trading, non-recruitable and can be contacted through the vanilla colonist right-click flow. The once-per-contact exchange gives a narrative acknowledgement, +1 Tok'ra trust and 400 Medicine XP to the selected colonist.
```

## Current development milestone

Current local milestone:

```text
0.2.22-dev-r2 - Add trust-scaled Tok'ra safehouse briefing outcome
```

Purpose:

```text
Make the non-trading Tok'ra safehouse medical briefing reflect the current Tok'ra trust tier without adding commerce, recruitment, quests, military aid or repeatable rewards.
```

Current mod metadata after applying `0.2.22-dev`:

```text
About/About.xml: modVersion = 0.2.22-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.22
```

Implementation scope:

```text
HediffDef: SG1_TokraSafehouseContactDialogue
HediffComp: HediffComp_TokraSafehouseContactDialogue
Generation hook: GenStep_TokraHiddenSafehouseContact
Interaction flow: select player colonist -> right-click Tok'ra safehouse contact
Trust effect: +1 Tok'ra trust once per generated contact
Medical effect: selected colonist receives trust-scaled Medicine XP once per generated contact
Trust-scaled XP: wary 250, neutral 400, cooperative 600, trusted 800
Debug support: generic +5 / -5 Tok'ra trust actions for tier testing; safehouse preparation preserves current trust while refreshing the test lead/site environment
```

Build requirement:

```text
C# changed. Use a forced rebuild with -t:Rebuild after extracting the ZIP.
```

0.2.22-dev-r2 note:

```text
Prepare Tok'ra safehouse site test no longer resets trust to neutral. It cleans inactive safehouse markers/sites, stores one lead and reports the preserved trust score/tier. Use the +5/-5 trust debug actions before creating a fresh safehouse contact.
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

## 0.2.20-dev-r1 — Tok'ra safehouse basic dialogue outcome

Adds a first selected-contact command to the peaceful Tok'ra contact generated
inside enterable hidden safehouse sites:

```text
Échanger avec le contact Tok'ra
```

Outcome:

```text
short narrative message
+1 Tok'ra trust
once per generated contact
save-persistent acknowledgement flag
```

Non-goals remain explicit:

```text
no trader
no recruitment
no quest
no military aid
no repeatable reward farm
```

## Manual test checklist for 0.2.20-dev-r1

1. Apply the ZIP from the repository root.
2. Run the forced C# rebuild.
3. Start RimWorld and confirm there are no red XML or C# errors.
4. Use the existing Tok'ra safehouse debug workflow to create a safehouse site.
5. Enter the site.
6. Run `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
7. Confirm the verification message is green and includes `dialogue : True`.
8. Select the Tok'ra contact.
9. Use `Échanger avec le contact Tok'ra`.
10. Confirm a narrative message appears and Tok'ra trust increases by `+1`.
11. Confirm the same command becomes disabled for that contact.
12. Save/reload on the safehouse map and confirm the command remains disabled.

## Recent Tok'ra milestones

### 0.2.19-dev-r1 — Tok'ra field clothing set

Adds one dedicated Tok'ra outfit, applies it directly to safehouse contacts and
adds worn body-type textures for Male, Female, Thin, Fat and Hulk.

### 0.2.18-dev — Non-trading Tok'ra safehouse contact

Adds one peaceful `SG1_TokraVoluntaryHost` to the enterable Tok'ra safehouse
site.

Validated behavior:
- one contact is generated;
- age is at least 20;
- adulthood backstory is present;
- contact is non-hostile;
- no trader role;
- not recruitable;
- the safehouse remains temporary and non-hostile.

### 0.2.17-dev — Enterable hidden Tok'ra safehouse site

Adds `SG1_TokraHiddenSafehouseSiteIncident`, a temporary enterable site and a
modest medical stash.

### 0.2.16-dev — Hidden Tok'ra safehouse world marker

Adds `SG1_TokraHiddenSafehouseWorldMarker`, a temporary non-enterable world-map
marker consuming one stored safehouse lead.

## 0.2.20-dev-r2 - Tok'ra safehouse contact dialogue right-click fix

- Replaced the failed Harmony-based right-click implementation from `r1` with a
  filtered `ThingComp` attached to human pawns by XML patch.
- Removed the `0Harmony` project reference so the mod keeps its existing build
  assumptions.
- The Tok'ra safehouse contact dialogue remains a colonist right-click action:
  select a colonist, right-click the Tok'ra contact, then choose the exchange.
- The contact remains non-trading, non-recruitable and non-hostile.

