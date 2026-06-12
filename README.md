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
### 0.1.82-dev — Add black and desert SG-team uniform variants

Two visual alternatives now complement the standard olive-drab SG-team field
uniform:

```text
SG1_BlackSGTeamUniform
SG1_DesertSGTeamUniform
```

The three uniforms intentionally share the same gameplay statistics, crafting
cost, `OnSkin` layer and body-part coverage:

```text
olive-drab -> standard field uniform
black      -> tactical and low-visibility missions
desert     -> arid missions
```

The choice is aesthetic and narrative rather than mechanical. The existing
tactical boots, gloves and vest remain shared modular equipment for all three
uniforms.

A forest-camouflage variant is intentionally omitted for now because its
small RimWorld sprite would be too close to the existing olive-drab baseline.
A sand-colored tactical-vest variant remains optional and should be considered
only after the desert uniform has been reviewed in game with the existing
black vest.

Natural Goa'uld raids, settlements and traders remain disabled.

## Next development focus

- validate black and desert rendering across body types and facings;
- review the desert uniform together with the existing black tactical vest;
- add a sand-colored vest only if the contrast is visually distracting;
- keep final art and semi-realistic wiki concept art as a later dedicated
  visual-production pass.

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
