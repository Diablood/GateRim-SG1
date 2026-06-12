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
### 0.2.0-dev-r2 — Add optional SG-team field helmet prototype

The stranded SG-team starter scenario now supplies four optional open-face
field helmets:

```text
SG1_SGTeamFieldHelmet
```

The helmets are deliberately added to the recovered equipment crates rather
than forced onto the generated starter pawns. This reflects their situational
use during SG missions and leaves the player free to decide when the extra
upper-head protection is worth wearing.

The helmet:

- covers `UpperHead` on the `Overhead` layer;
- remains compatible with the complete SG-team field set;
- weighs `0.9`;
- provides moderate protection without becoming heavy armor;
- costs `25` steel and `15` cloth when crafted;
- requires `Gunsmithing` and `Crafting 4`;
- includes temporary dedicated graphics.

The scenario still starts with:

```text
4 SG-team members
3 assault rifles
1 pump shotgun
4 cloth bedrolls
4 optional SG-team field helmets
emergency food, medicine and resource crates
```

The next gameplay step remains the first playable Goa'uld world-faction
baseline.

## Next development focus

- validate that four helmets appear in the scenario supplies but are not
  auto-equipped;
- validate the helmet rendering in all facings;
- keep final textures and wiki concept art for the later visual pass;
- add the playable Goa'uld world-faction baseline.

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
