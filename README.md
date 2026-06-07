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

### 0.1.18-dev — Convert recent implantation into active Goa'uld host

The first complete adult-possession loop is now implemented:

```text
free Goa'uld symbiote
    ↓ manual forced implantation
recent Goa'uld implantation
    ↓ one-day countdown
adult Goa'uld symbiote host state
```

The same persistent symbiote identity is transferred into the active-host state immediately before the temporary implantation state expires.

The acquired active-host state grants:

```text
immunity gain ×1.75
injury healing ×1.75
incoming damage ×0.8
lifespan ×5
pain ×0.7
```

The host's original germline xenotype remains unchanged.

## First playable milestone

- [x] Inheritable Jaffa xenotype foundation
- [x] Separate inherited Jaffa lineage from Prim'ta effects
- [x] Goa'uld host xenotype prototype
- [x] Free Goa'uld symbiote pawn prototype
- [x] Recent Goa'uld implantation Hediff prototype
- [ ] Automatic Prim'ta workflow
- [ ] Forced Goa'uld implantation
- [ ] Ritual Goa'uld implantation
- [ ] Host conversion after the critical phase
- [ ] Goa'uld faction
- [ ] Jaffa pawn kinds
- [ ] Ma'Tok staff weapon
- [ ] Zat'nik'tel
- [ ] Generic Jaffa armor

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Keep English in `Defs`.
- Add French `DefInjected` translations as soon as a content batch is stabilized.
- Use bilingual `Keyed` files for future UI messages and C# strings.
- Keep versioned player-wiki drafts under `docs/wiki/`.
- Publish wiki pages directly at the root of the separate `GateRim-SG1.wiki` repository.
- Use `./tools/sync-wiki.sh` from Bash or `.\tools\sync-wiki.cmd` from Windows PowerShell.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
