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

### 0.1.7-dev — Goa'uld host foundation

The mod now includes an XML-only prototype for humanoids already possessed by an adult Goa'uld symbiote:

- `SG1_GoauldHost`
- `SG1_NaquadahBlood`
- `SG1_GoauldLongevity`
- French `DefInjected` translations
- Temporary local gene icons
- Updated player-wiki drafts

The dedicated symbiote system remains a later C# milestone.

## First playable milestone

- [x] Jaffa xenotype foundation
- [x] French translations for stabilized Jaffa content
- [x] Player wiki foundation
- [x] Goa'uld host xenotype prototype
- [x] French translations for stabilized Goa'uld host content
- [ ] Free Goa'uld symbiote entity
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
- Publish stabilized wiki pages to the separate `GateRim-SG1.wiki` repository.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
