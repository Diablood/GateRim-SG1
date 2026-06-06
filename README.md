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

### 0.1.2-dev — Jaffa physiology refinement

The Jaffa prototype no longer forces every pawn to use RimWorld's visible Hulk body type.

- `Body_Hulk` removed from `SG1_Jaffa`
- `SG1_JaffaPhysiology` custom gene added
- Physical enhancement represented without a cosmetic body lock
- Temporary balance effect: `+15` carrying capacity
- Existing melee, resilience and immunity genes preserved
- Symbiote dependency intentionally deferred to a dedicated system

## First playable milestone

The first playable milestone focuses on a small, testable Goa'uld and Jaffa foundation:

- [x] Jaffa xenotype foundation
- [x] Jaffa physiology gene without forced Hulk body shape
- [ ] Goa'uld faction
- [ ] Basic Jaffa warrior and guard pawn kinds
- [ ] Ma'Tok staff weapon
- [ ] Zat'nik'tel
- [ ] Generic Jaffa armor

## Directory layout

```text
GateRim-SG1/
├── About/
├── 1.6/
│   ├── Defs/
│   └── Patches/
├── Languages/
├── Textures/
├── Source/
└── docs/
```

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
