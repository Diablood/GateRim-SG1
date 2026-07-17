# Project state

Current milestone: `0.3.97-dev - Finalize Tok'ra hypodermic rifle visual`

Status: the final weapon texture is validated in game. The texture commit was
created directly on `develop`; this documentary revision completes versioning,
tests, the visual register and protected wiki reference material.

- Starting point: published `develop` aligned with annotated tag `v0.3.96-dev`.
- Gameplay texture commit: `4003e97`.
- Working branch: none; the visual commit was made directly on `develop`.
- Final documentary revision: `r3`.
- Assembly version: `0.3.97.0`.
- Final annotated tag: `v0.3.97-dev`.

## Completed scope

- Finalize `SG1_TokraHypodermicRifle` with a simplified, high-contrast
  `128×128` texture.
- Keep the source artwork horizontally aligned for RimWorld long-gun rendering.
- Preserve the inherited small random ground rotation instead of forcing a
  special-case zero angle.
- Preserve the cyan sealed-charge modules and Tok'ra experimental identity.
- Preserve the existing twelve sealed charges, projectile, non-lethal effect,
  mission delivery, save identifiers and automatic disposal.
- Add a byte-identical protected wiki copy and register the rifle family as
  final.

## Explicitly deferred scope

- `SG1_TokraHypodermicDart` remains a distinct temporary projectile family.
- Other weapons, apparel and multidirectional pawn visuals remain outside this
  milestone.

## Validation result

The maintainer validated the final horizontal version in game:

- equipped rendering is correctly aligned;
- ground rendering follows the same small random variation used by vanilla
  weapons;
- the simplified silhouette remains readable at `128×128`;
- stronger Camera+ zoom no longer exposes excessive blurred micro-detail;
- gameplay behavior remains unchanged.

Before publication, rerun the forced build, duration-formatting check, visual
asset check, complete project-consistency check and `git diff --check`.
