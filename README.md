# GateRim SG-1

A Stargate SG-1 mod project for RimWorld 1.6.

## Project identity

- Public name: `GateRim SG-1`
- Author: `Diablood`
- Package ID: `diablood.gaterimsg1`
- C# namespace: `GateRimSG1`
- Repository: `https://github.com/Diablood/GateRim-SG1`
- Required DLC for the current development branch: `Biotech`

## Initial development branch

```text
feature/jaffa-goauld-foundation
```

## Current milestone

### 0.1.5-dev — First in-game log fixes

The first `Player.log` review identified and corrected several GateRim issues:

- Remove leading and trailing whitespace from the Jaffa xenotype description.
- Normalize French `DefInjected` values to avoid leading and trailing whitespace.
- Replace the missing vanilla physiology icon path with a local GateRim placeholder.
- Keep the original `About/ModIcon.png` untouched.
- Document an isolated test procedure for third-party gene-category conflicts.

## First playable milestone

- [x] Jaffa xenotype foundation
- [x] Jaffa physiology gene without forced Hulk body shape
- [x] Jaffa longevity at 150% lifespan expectancy
- [x] French translations for stabilized Jaffa content
- [x] First in-game log correction pass
- [ ] Goa'uld faction
- [ ] Basic Jaffa warrior and guard pawn kinds
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
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
