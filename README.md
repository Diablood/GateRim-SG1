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

### 0.1.8-dev — Free Goa'uld symbiote prototype

The mod now includes an XML-only animal-style pawn prototype representing an adult Goa'uld symbiote outside a host:

- `SG1_GoauldSymbiote`
- Weak bite attack
- Temporary local sprite
- French `DefInjected` translations
- Developer-mode spawning for isolated tests
- No natural biome spawning
- Updated wiki drafts
- Safe wiki synchronization helper: `tools/sync-wiki.sh`

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
