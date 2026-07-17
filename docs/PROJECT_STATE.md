# Project state

Current milestone: `0.3.98-dev - Finalize tretonin dose visual`

Status: the final tretonin-dose texture is validated in game and pushed. This
revision completes publication metadata, durable tests, the visual register and
protected wiki documentation.

- Starting point: published `develop` aligned with `v0.3.97-dev`.
- Working branch: direct continuation on `develop`.
- Validated gameplay texture: maintainer-provided `128×128` PNG.
- Final documentary revision: `r2`.
- Assembly version: `0.3.98.0`.
- Final annotated tag: `v0.3.98-dev`.

## Completed scope

- Finalize `SG1_TretoninDose` as a small advanced medical ampoule.
- Use genuine exterior transparency and a stronger dark outline.
- Keep the object tightly framed to use the full `128×128` resolution.
- Preserve the existing `drawSize` as the control for its small in-game scale.
- Preserve stack size, mass, production, administration, substitution duration,
  Def name and save compatibility.
- Add a byte-identical protected wiki copy.
- Register the texture family as final.

## Validation result

The maintainer validated the exact uploaded final image in game:

- map and inventory rendering are readable;
- exterior transparency is correct;
- the stronger outline survives reduction;
- the cyan liquid and medical-container silhouette remain distinct;
- the existing gameplay size is appropriate;
- no functional behavior changed.

Before publication, rerun the forced build, duration-formatting check, visual
asset check, complete project-consistency check and `git diff --check`.
