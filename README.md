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

### 0.1.22-dev — Add ritual Goa'uld implantation prototype

A selected free adult Goa'uld symbiote now exposes a controlled ritual command:

```text
Ritual implantation
```

The command implants the nearest compatible reachable humanoid within a radius
of `12` cells, consumes the free pawn and transfers the same persistent symbiote
identity into recent implantation.

All current implantation paths reuse the same transfer core:

```text
manual adjacent implantation
autonomous contact implantation
controlled ritual implantation
```

Explicit target selection, ceremony duration and faction integration remain
future refinements.

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
