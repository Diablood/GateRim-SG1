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
0.2.18-dev - Add non-trading Tok'ra safehouse contact
```

Known issue carried from that milestone:

```text
The generated Tok'ra safehouse contact was functionally correct but could appear naked.
```

## Current development milestone

Current local milestone:

```text
0.2.19-dev-r1 - Add Tok'ra field-garb body-type textures
```

Purpose:

```text
Create the dedicated Tok'ra clothing set first, then apply it directly to the
safehouse contact instead of using temporary vanilla apparel.
```

Current mod metadata after applying `0.2.19-dev`:

```text
About/About.xml: modVersion = 0.2.19-dev
Source/GateRimSG1/GateRimSG1.csproj: Version/AssemblyVersion/FileVersion = 0.2.19
```

Implementation scope:

```text
ThingDef: SG1_TokraFieldGarb
PawnKindDef updated: SG1_TokraVoluntaryHost
French translation: tenue de terrain Tok'ra
Textures: Textures/Things/Pawn/Humanlike/Apparel/TokraFieldGarb
```

Build requirement:

```text
Gameplay change is XML/texture only. A rebuild is not required to test the
apparel, but a forced rebuild may be run to refresh assembly metadata.
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

C# build command, optional for this XML/texture milestone:

```powershell
dotnet build `
    ".\Source\GateRimSG1\GateRimSG1.csproj" `
    --configuration Release `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

## 0.2.19-dev — Tok'ra field clothing set

Adds one dedicated Tok'ra clothing set:

```text
SG1_TokraFieldGarb / Tok'ra field garb
```

Design intent:

```text
one set only
sober sand/beige visual identity
quilted or brocade-inspired tunic silhouette
structured shoulders
visible belt
minimal protection
no combat-armor role
no visual variants
```

Integration:

```text
SG1_TokraVoluntaryHost now uses apparelRequired = SG1_TokraFieldGarb.
```

This fixes the validated `0.2.18-dev` safehouse contact appearing naked without
adding a temporary vanilla-clothing workaround.

## Manual test checklist for 0.2.19-dev

1. Apply the ZIP from the repository root.
2. Start RimWorld and confirm there are no red XML load errors referencing
   `SG1_TokraFieldGarb` or `SG1_TokraVoluntaryHost`.
3. Check a tailoring bench after `SG1_SGFieldEquipment` is available and confirm
   the `Tok'ra field garb` bill exists.
4. Use the existing Tok'ra safehouse debug workflow to create a safehouse site.
5. Enter the site and confirm the Tok'ra contact is no longer naked.
6. Confirm the contact remains non-hostile, non-trading and non-recruitable.


## 0.2.19-dev-r1 — Tok'ra field-garb body-type textures

Test status before correction:

```text
0.2.19-dev behavior validated, but the worn apparel graphic was missing for at least the Female body type on the generated contact.
```

Correction:

```text
Add TokraFieldGarb worn texture variants for Male, Female, Thin, Fat and Hulk body types.
```

Build requirement:

```text
Texture/docs-only correction. No C# rebuild is required.
```

## Recent Tok'ra milestones

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

Known follow-up fixed by `0.2.19-dev`:
- the contact could appear naked.

### 0.2.17-dev — Enterable hidden Tok'ra safehouse site

Adds `SG1_TokraHiddenSafehouseSiteIncident`, a temporary enterable site and a
modest medical stash.

### 0.2.16-dev — Hidden Tok'ra safehouse world marker

Adds `SG1_TokraHiddenSafehouseWorldMarker`, a temporary non-enterable world-map
marker consuming one stored safehouse lead.
