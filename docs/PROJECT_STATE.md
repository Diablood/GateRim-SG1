# Project state

Current milestone: `0.3.92-dev - Add final Goa'uld and Jaffa command icons`

Status: final visual revision `r1` is validated in game and through all required
technical checks. Publication-only revision `r2` records the completed result
without changing the approved icons or runtime behavior.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.91-dev`, including the later untagged wiki-page display correction.
- Working branch: `feature/final-goauld-jaffa-command-icons`.
- Final visual revision: `r1`.
- Publication-document revision: `r2`.
- Validated assembly version: `0.3.92.0`.
- Final annotated tag: `v0.3.92-dev`.
- Integration target: `develop`.

## Published scope

- Replace four existing `Textures/UI/Commands` files without renaming their
  stable C# paths:
  - `SG1_AutonomousHunt.png`;
  - `SG1_EmergencyExtraction.png`;
  - `SG1_ForcedImplantation.png`;
  - `SG1_RitualImplantation.png`.
- Use dedicated transparent `64×64` concepts for autonomous targeting,
  emergency surgical extraction, direct forced implantation and ceremonial
  implantation.
- Add byte-identical copies under `docs/wiki/images`.
- Display each image on its dedicated functional page and display the shared
  ritual icon on `Primta-Formal-Ceremony.md`.
- Reclassify the four command families as final and extend the exact final
  whitelist from `24` to `28`.

## Preserved behavior

- No C# or XML gameplay file is modified.
- Autonomous targeting, emergency extraction, forced implantation, ritual
  implantation, Tok'ra voluntary/therapeutic implantation and formal Prim'ta
  ceremony behavior remain unchanged.
- Existing visibility, developer restrictions, persistence, targeting,
  cancellation and identity-transfer rules remain unchanged.
- The existing shared use of `SG1_RitualImplantation` is intentionally
  preserved.

## Validation completed

- All four gameplay files are valid transparent RGBA PNGs at exactly `64×64`.
- Every matching wiki image remains byte-identical to its gameplay counterpart.
- The maintainer validated all four commands in the real RimWorld interface.
- The four concepts remain distinct and readable at normal gizmo size without
  an opaque square, checkerboard, clipping, incorrect tint or magenta fallback.
- The shared ritual image remains suitable on the Goa'uld, Tok'ra and formal
  Prim'ta ceremony command surfaces.
- Forced build of assembly `0.3.92.0` completed successfully.
- Duration-formatting, visual-asset and project-consistency checks passed.
- The visual checker reports `28` final local families, `605` PNG files, `72`
  canonical families and no missing or unregistered reference.
- `git diff --check` passed and the maintainer reviewed the final state locally.

## Publication state

Publication-only revision `r2` changes documentation status only. The validated
state is published through the final feature-branch commit, fast-forward
integration into `develop`, the unique annotated tag `v0.3.92-dev` and
synchronization of the separate wiki. Local suffixes `r1` and `r2` do not
appear in the final commit or tag.

## Next milestone

No later milestone number is reserved.
