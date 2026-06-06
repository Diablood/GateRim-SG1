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

## Initial development branch

```text
feature/jaffa-goauld-foundation
```

## Current milestone

### 0.1.9-dev — Fix free Goa'uld symbiote XML

The free Goa'uld symbiote prototype has received its first XML correction after source review:

- Remove the invalid `<wildness>` field from `RaceProperties`
- Preserve `SG1_GoauldSymbiote` as a developer-mode test pawn
- Preserve the weak bite attack and temporary local sprite
- Preserve French `DefInjected` translations
- Keep natural biome spawning disabled by the absence of biome entries
- No player-wiki publication required for this maintenance-only correction

Forced implantation, ritual implantation and host transfer remain later C# milestones.

## First playable milestone

- [x] Jaffa xenotype foundation
- [x] Goa'uld host xenotype prototype
- [x] Free Goa'uld symbiote pawn prototype
- [ ] Recent-implantation state
- [ ] Forced implantation
- [ ] Ritual implantation
- [ ] Host transfer
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
- Use `./tools/sync-wiki.sh` to synchronize wiki drafts safely.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
