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
### 0.1.74-dev — Replace technical Jaffa mark genes with intrinsic mark data

Jaffa forehead marks are now stored as dedicated save-persistent pawn data.
They are no longer part of the Jaffa xenotype and no longer use genes as their
runtime rendering source.

The new intrinsic Defs are:

```text
SG1_JaffaForeheadMark_GenericIntrinsic
SG1_JaffaForeheadMark_GenericSilverIntrinsic
SG1_JaffaForeheadMark_GenericGoldIntrinsic
```

A custom dynamic render-node setup injects the selected mark into RimWorld's
native pawn render tree. The validated head attachment, facings and temporary
textures remain unchanged.

The existing Goa'uld domain profile now exposes three explicit intrinsic-mark
slots:

```text
ordinary Jaffa  -> generic black intrinsic mark
selected elite  -> temporary generic silver intrinsic mark
First Prime     -> temporary generic gold embossed intrinsic mark
```

Former technical `GeneDef` entries remain declared only as invisible migration
placeholders. Encountered pawns are converted automatically and the obsolete
genes are removed from them. A developer map tool can also assign or remove any
mark manually on any pawn, including a non-Jaffa infiltrator.

Heavy armor does not imply First Prime status. Natural Goa'uld raids,
settlements and traders remain disabled.

## Next maintenance focus

Before the next gameplay expansion, the project will perform a consolidation pass:

- keep the five remaining French translation load errors identified as vanilla RimWorld issues;
- audit player-facing information, debug-only diagnostics and conditional gizmos;
- prepare a mod-specific debug option before the `0.2.x` Stargate chapter;
- preserve the future wiki-image and Workshop-asset plan without claiming final visuals yet.

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
- [ ] Zat'nik'tel
- [x] Modular Jaffa armor prototypes
- [x] Retractable Jaffa helmet modes
- [x] Automatic Jaffa armor loadouts
- [ ] Generic human SG-team uniform
- [ ] SG tactical boots
- [ ] SG tactical gloves

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
