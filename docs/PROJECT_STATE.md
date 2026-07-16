# Project state

Current milestone: `0.3.96-dev - Finalize Prim'ta larval item visuals`

Status: both scoped inert biological item visuals are validated in game; revision
`r2` completes documentation, protected wiki copies and publication metadata.

- Starting point: published `develop` aligned with annotated tag `v0.3.95-dev`.
- Working branch: `feature/final-primta-larval-item-visuals`.
- Validated gameplay revision: `r1`.
- Final documentary revision: `r2`.
- Assembly version: `0.3.96.0`.
- Integration target: `develop`.
- Final annotated tag: `v0.3.96-dev`.

## Completed scope

- Finalize `SG1_PrimtaLarva` as a pale, elongated and implantable larval form.
- Finalize `SG1_ImmaturePrimtaSymbiote` as a smaller, curled pre-larval form.
- Correct the immature symbiote XML texture path to its dedicated family.
- Preserve both ThingDefs, labels, save identifiers, storage categories,
  incubation recipes, deterioration, temperature and implantation behavior.
- Add byte-identical protected wiki copies and register both families as final.

## Explicitly deferred scope

The following remain animal pawns and require a later multidirectional visual
lot rather than this inert-item milestone:

- `SG1_GoauldSymbiote`;
- `SG1_TokraSymbiote`;
- `SG1_GoauldQueen`.

## Validation result

The maintainer validated both items in game after correcting the immature
symbiote texture path:

- the Prim'ta larva uses its dedicated longer mature-larval texture;
- the immature symbiote uses its dedicated smaller curled texture;
- both render with genuine exterior transparency and no fallback texture;
- existing incubation, storage and biological behavior remain functional.

Before publication, rerun the forced build, duration-formatting check, visual
asset check, complete project-consistency check and `git diff --check`.
