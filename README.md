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
### 0.2.0-dev-r1 — Refine stranded SG-team starter scenario

GateRim SG-1 now opens its first playable no-gate phase with a selectable
scenario:

```text
Stranded SG team
```

The player starts with four adult SG-team members after a failed off-world
reconnaissance mission. The player faction is the dedicated `SGC expedition`
identity rather than vanilla `New Arrivals`. The local Stargate is unusable and contact with the
SGC is impossible.

Each generated starter pawn automatically wears:

```text
SG-team field uniform
SG tactical boots
SG tactical gloves
SG tactical vest
```

The starting camp receives recovered field supplies rather than a
preconstructed workshop:

```text
3 assault rifles
1 pump shotgun
4 cloth bedrolls
30 packaged survival meals
20 industrial medicine
300 steel
150 wood
20 industrial components
120 cloth
80 plain leather
```

The vanilla firearms are temporary Tau'ri placeholders until dedicated SGC
weapons are introduced. Ma'Tok staffs and Zat'nik'tel sidearms remain absent
from the starter loadout so their future natural acquisition retains gameplay
value.

This milestone does not enable world factions, natural Goa'uld raids or a
functional Stargate yet. Those become the next `0.2.x` playable-slice steps.

## Next development focus

- validate the dedicated SGC expedition identity, four-candidate selection
  page and translated launch narrative without developer tools;
- validate automatic SG-team apparel on all four starter pawns;
- add the first playable Goa'uld world-faction baseline;
- activate natural encounters progressively rather than all at once.

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
