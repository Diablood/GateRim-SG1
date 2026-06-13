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
### 0.2.8-dev — Add Stargate crafting-research baseline

GateRim SG-1 now has a dedicated research tab with four production projects.

```text
Jaffa weaponry
-> prerequisite: Gunsmithing
-> unlocks local Ma'Tok and Zat'nik'tel crafting

Jaffa armor
-> prerequisite: Flak armor
-> unlocks local Jaffa armor-component crafting

SGC field equipment
-> prerequisite: Complex clothing
-> unlocks local SG-team clothing and mission-equipment crafting

Goa'uld biotechnology
-> prerequisite: Drug production
-> unlocks tretonin preparation and specialized Prim'ta basins
```

The research gates local reproduction only.

```text
captured weapons remain usable
captured armor remains wearable
existing tretonin doses remain administrable
existing Prim'ta larvae remain implantable
scenario-supplied SG equipment remains usable
```

The Goa'uld-biotechnology project also gates:

```text
Prim'ta incubation basin
Prim'ta preservation basin
Goa'uld ritual basin
Prim'ta assisted-maturation bill
tretonin-preparation bill
```

No C# rebuild is required for this XML-only milestone.

## Next development focus

- validate the GateRim SG-1 research tab;
- validate every vanilla prerequisite and unlock;
- confirm captured or supplied equipment remains usable before research;
- add normal acquisition sources for rare Stargate resources separately.

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
- [x] Contextual social baseline
- [x] Free Jaffa peaceful visitors baseline
- [x] Hidden Tok'ra world-presence baseline
- [x] Stargate crafting-research baseline

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
