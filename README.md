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
### 0.1.81-dev — Add SG tactical vest prototype

The fourth modular SG-team field-equipment item is now available:

```text
SG1_SGTacticalVest
```

The prototype represents a black load-bearing tactical vest worn over the
olive-drab SG-team field uniform. It uses the `Middle` apparel layer, covers
the torso and shoulders, and remains compatible with the separate tactical
boots and gloves because they protect different body-part groups.

The vest:

- adds modest protection without becoming heavy body armor;
- keeps enough mobility for exploration and extended field operations;
- is craftable at the vanilla hand and electric tailoring benches;
- costs `30` plain leather and `35` cloth;
- requires `Crafting 4`;
- reuses the dedicated `SG teams` apparel category;
- includes temporary body-type and facing-specific graphics.

The first generic SG-team field-equipment baseline is now complete:

```text
SG-team field uniform
SG tactical boots
SG tactical gloves
SG tactical vest
```

Natural Goa'uld raids, settlements and traders remain disabled.

## Next development focus

- validate the full four-piece SG-team field set in game;
- review the remaining pre-0.2 roadmap before adding environment-specific or
  role-specific uniform variants;
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
