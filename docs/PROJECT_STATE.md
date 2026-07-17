# Project state

Current milestone: `0.3.99-dev - Finalize Goa'uld open-conflict battlefield icon`

Status: the corrected flat `128×128` world-site icon is validated in game. This
revision completes publication metadata, durable tests, the visual register and
the protected wiki reference.

- Starting point: published `develop` aligned with `v0.3.98-dev`.
- Working branch: `feature/final-goauld-open-conflict-battlefield-icon`.
- Validated gameplay revision: `r2`.
- Final documentary revision: `r3`.
- Assembly version: `0.3.99.0`.
- Final annotated tag: `v0.3.99-dev`.

## Completed scope

- Replace the blurred `64×64` battlefield icon with a crisp `128×128` PNG.
- Preserve the same texture family and WorldObjectDef.
- Use a flat, limited-detail presentation consistent with the other validated
  mission icons.
- Keep broad bronze-and-black Goa'uld weapon silhouettes, a central orange
  impact and a strong dark outline.
- Remove pseudo-3D rendering, dense debris and fine shading that became noisy
  at world-map scale.
- Refresh the byte-identical protected wiki copy.
- Keep the entire remaining `World/WorldObjects/Expanding/Sites` set closed and
  final.

## Validation result

The maintainer validated the final flat icon in game:

- the icon is sharp at world-map scale;
- the two-sided Goa'uld conflict remains readable;
- the style matches the other mission icons;
- exterior transparency is correct;
- no site behavior, duration, faction relation or map-generation logic changed.

Before publication, rerun the forced build, duration-formatting check, visual
asset check, complete project-consistency check and `git diff --check`.
