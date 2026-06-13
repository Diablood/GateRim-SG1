# GateRim SG-1

A Stargate SG-1 mod project for RimWorld 1.6.

## Project identity

- Public name: `GateRim SG-1`
- Author: `Diablood`
- Package ID: `diablood.gaterimsg1`
- C# namespace: `GateRimSG1`
- Repository: `https://github.com/Diablood/GateRim-SG1`
- Player wiki: `https://github.com/Diablood/GateRim-SG1/wiki`
- Required DLC for the current development branch: `Biotech`

## Current milestone
### 0.2.4-dev-r1 — Define adulthood body types

The first native cultural-history layer is now in place without adding
Humanoid Alien Races as a dependency.

The milestone adds:

```text
6 Tau'ri SGC adult careers
8 shared Jaffa childhoods
8 Goa'uld-domain Jaffa adult careers
8 Free Jaffa adult careers
6 off-world human childhoods
6 ordinary Goa'uld-host adult careers
4 System Lord adult careers
6 generated Tok'ra-agent adult careers
```

Generation policy:

```text
Tau'ri
-> broad vanilla Earth-compatible histories
-> smaller optional chance of an SGC-specific adulthood

Goa'uld-domain Jaffa
-> dedicated Jaffa childhoods and domain careers only

Free Jaffa
-> dedicated Jaffa childhoods and liberated-community careers only

Generated Goa'uld hosts
-> off-world human childhoods and Goa'uld caste careers only

Generated Tok'ra prototype agents
-> off-world human childhoods and Tok'ra careers only
```

Existing colonists who voluntarily accept a Tok'ra symbiote keep their
original childhood and adulthood. Dedicated stories use
`requiresSpawnCategory = true` so they do not leak into unrelated vanilla
generation.

The baseline is intentionally narrative-first. More stories and granular
skill effects can be added later through small content patches.

The `r1` rendering fix assigns standard gender-appropriate vanilla body types
to every dedicated adulthood history:

```text
male character   -> Male
female character -> Female
```

RimWorld directly asks the selected adulthood backstory for the pawn body
type. Without these fields, generated off-world pawns could receive an
undefined body type, become invisible and trigger portrait-rendering errors.

## Next development focus

- validate representative generation samples;
- confirm no terrestrial histories appear on generated Jaffa or Goa'uld;
- confirm no `No shuffled Childhood` or `No shuffled Adulthood` fallback logs;
- add contextual social rules separately.

## First playable milestone

- [x] Inheritable Jaffa xenotype foundation
- [x] Separate inherited Jaffa lineage from Prim'ta effects
- [x] Goa'uld host xenotype prototype
- [x] Free Goa'uld symbiote pawn prototype
- [x] Recent Goa'uld implantation Hediff prototype
- [x] Automatic Prim'ta workflow
- [x] Forced Goa'uld implantation
- [x] Ritual Goa'uld implantation
- [x] Host conversion after the critical phase
- [x] Goa'uld System Lord faction foundation
- [x] Goa'uld-aligned Jaffa pawn kinds
- [x] Automatic initial Prim'ta for Goa'uld-aligned Jaffa
- [x] Ma'Tok staff weapon prototype
- [x] Automatic Ma'Tok loadout for Goa'uld Jaffa
- [x] Zat'nik'tel first-shot incapacitation prototype
- [x] Modular Jaffa armor prototypes
- [x] Retractable Jaffa helmet modes
- [x] Automatic Jaffa armor loadouts
- [x] SG-team field uniform prototype
- [x] SG tactical boots prototype
- [x] SG tactical gloves prototype
- [x] SG tactical vest prototype
- [x] Black and desert SG-team uniform variants
- [x] Stranded SG-team starter scenario
- [x] Playable Goa'uld world-faction baseline
- [x] Free Jaffa world-faction baseline
- [x] Persistent Goa'uld host-caste baseline
- [x] Cultural backstory baseline

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Include code, technical documentation and wiki drafts in the first ZIP of each milestone.
- Keep manifests outside ZIP archives.
- Keep English in `Defs`.
- Add French `DefInjected` translations as soon as a content batch is stabilized.
- Use bilingual `Keyed` files for future UI messages and C# strings.
- Keep versioned player-wiki drafts under `docs/wiki/`.
- Publish wiki pages directly at the root of the separate `GateRim-SG1.wiki` repository.
- Use `./tools/sync-wiki.sh` from Bash or `.\tools\sync-wiki.cmd` from Windows PowerShell.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
