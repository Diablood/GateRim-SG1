# Project state

Current milestone: `0.3.94-dev - Finalize Goa'uld hand-device visuals`

Status: validated after local visual revision `r1`; documentary finalization revision `r2` closes the milestone for publication.

- Starting point: published `develop` aligned with annotated tag `v0.3.93-dev`.
- Working branch: `feature/final-goauld-hand-device-visuals`.
- Validated local revision: `r1`.
- Finalization revision: `r2`.
- Assembly version: `0.3.94.0`.
- Integration target: `develop`.
- Final annotated tag: `v0.3.94-dev`.

## Completed scope

- Replace the temporary Zat'nik'tel reuse used by `SG1_KaraKesh` with the
  maintainer-approved dedicated kara kesh artwork.
- Replace the temporary Zat'nik'tel reuse used by
  `SG1_GoauldHealingBracelet` with the approved second bracelet proposal.
- Keep both established apparel texture paths and save identifiers unchanged.
- Preserve all ThingDefs, mechanics, balance, research, recipes, System Lord
  assignment and C# behavior.
- Register both transparent `128×128` families as final visual references.
- Add byte-identical wiki copies and display each device on its dedicated page
  and on the progressive visual-reference page.
- Extend the visual checker with both gameplay/wiki pairs and raise the exact
  final-family whitelist from `31` to `33` without changing the `606` PNG / `73`
  family baseline.
- Advance public metadata to `0.3.94-dev` and assembly metadata to `0.3.94.0`.

## Validation result

The maintainer validated both devices in game on `r1`:

- correct ground, inventory, inspection and equipped presentation;
- centered readable silhouettes without clipping, magenta fallback, opaque
  rectangle or baked checkerboard;
- genuine exterior transparency;
- unchanged kara kesh shield, kinetic, neural and paralysis behavior;
- unchanged healing-bracelet treatment, fatigue, cooldown and hostile AI use;
- stable save/reload behavior and natural System Lord equipment.

Revision `r2` changes only documentation, protected wiki copies, the visual
register and its checker. Before publication, rerun the forced build, duration,
visual, complete project-consistency and `git diff --check` controls.

## Publication state

The final feature-branch commit is intended to be integrated into `develop` by
fast-forward, tagged once as `v0.3.94-dev` and followed by synchronization of the
changed wiki pages and images. Local suffixes `r1` and `r2` do not appear in the
commit or tag.
