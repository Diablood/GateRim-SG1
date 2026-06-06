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

### 0.1.4-dev — French Jaffa translations

The stabilized Jaffa foundation now includes French `DefInjected` translations:

- `SG1_Jaffa`
- `SG1_JaffaPhysiology`
- `SG1_JaffaLongevity`

English remains embedded directly in the functional `Defs`.
French translations are maintained in parallel under:

```text
Languages/French/DefInjected/
```

Future UI messages and C# strings will use bilingual `Keyed` files.

## First playable milestone

- [x] Jaffa xenotype foundation
- [x] Jaffa physiology gene without forced Hulk body shape
- [x] Jaffa longevity at 150% lifespan expectancy
- [x] French translations for stabilized Jaffa content
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
