# Project state

Current milestone: `0.3.93-dev - Add final Goa'uld and Prim'ta basin art and simplify relay structures`

Status: local implementation revision `r1` prepared for build and focused in-game validation.

- Starting point: published `develop` aligned with annotated tag `v0.3.92-dev`.
- Working branch: `feature/final-goauld-primta-basin-icons`.
- Current local revision: `r1`.
- Target assembly version: `0.3.93.0`.
- Integration target after validation: `develop`.

## Implemented scope

- Replace the Goa'uld ritual-basin placeholder with dedicated transparent art.
- Replace the shared Prim'ta incubation/preservation placeholder with dedicated
  transparent biological-basin art.
- Keep both stable gameplay texture paths unchanged.
- Change `SG1_PrimtaIncubationBasin` and
  `SG1_PrimtaPreservationBasin` from `2×1` rotatable buildings to compact
  `1×1` non-rotatable buildings.
- Preserve the incubation interaction cell, recipes, storage filters, power use,
  preservation behavior, costs and research prerequisites.
- Remove the redundant custom relay wall, door and barricade ThingDefs.
- Remove their unused claim-gated building classes and DefOf fields.
- Let the existing relay-layout fallback use vanilla `Wall`, `Door` and
  `Barricade` (`Sandbags` only as the existing final fallback).
- Adopt the simplified literal-command branching and publication procedures
  supplied by the maintainer.

## Required validation

- Forced build of assembly `0.3.93.0`.
- Clean game load with no missing DefOf, XML, texture or C# error.
- Both Prim'ta basins occupy exactly one map cell and cannot be rotated.
- Incubation bills and the southern interaction cell still work.
- Preservation accepts only the two intended biological items, holds up to two
  stacks in its one cell and still requires power for preservation.
- The ritual basin renders correctly and retains all implantation/ceremony flows.
- A relay sabotage map generates with vanilla walls, doors and barricades,
  remains roofed and accessible, and can still be sabotaged and evacuated.
- Save/reload on a map containing each basin and during the relay mission.
- Visual assets have genuine alpha and no checkerboard or opaque background.
- Duration-formatting, visual-asset, project-consistency and `git diff --check`
  checks pass after the documentary register is finalized.

## Publication state

Not published. Do not commit, merge, tag or synchronize the wiki until `r1` is
validated locally and a finalization revision has updated the complete visual
register, changelog, durable tests and player-facing wiki pages.
