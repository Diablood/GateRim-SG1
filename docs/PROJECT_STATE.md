# Project state

Current milestone: `0.3.91-dev - Add final intrinsic Jaffa forehead-mark overlays`

Status: validated after final visual revision `r1`; publication-only revision
`r2` records the completed build, static checks, in-game validation, final tag
and wiki synchronization without changing the approved textures.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.90-dev`.
- Final local Git state: reviewed visually by the maintainer in the IDE before
  and after each commit, push, switch and tag operation; no temporary branch
  name is assumed by the documentation.
- Final visual revision: `r1`.
- Publication-document revision: `r2`.
- Validated assembly version: `0.3.91.0`.
- Final annotated tag: `v0.3.91-dev`.
- Integration target: `develop`.

## Published scope

- Replace the twelve existing files under
  `Textures/Things/Pawn/Humanlike/JaffaForeheadMarks`.
- Keep the three canonical families and paths stable:
  - `GenericJaffaForeheadMark` — ordinary black;
  - `GenericSilverJaffaForeheadMark` — elite silver;
  - `GenericGoldJaffaForeheadMark` — First Prime gold.
- Use the same compact Apophis geometry for all three ranks.
- Keep only the `South` texture visible. The `North`, `East` and `West` files are
  valid `128×128` PNGs with a fully transparent alpha channel.
- Add three byte-identical `South` reference copies to `docs/wiki/images`.
- Reclassify the three intrinsic overlay families as final and protect them in
  the exact visual whitelist and wiki-copy audit.

## Preserved behavior

- The marks remain intrinsic removable pawn data, not genes or apparel.
- Stable `JaffaForeheadMarkDef` identifiers and saved pawn records are unchanged.
- Goa'uld-domain assignment, Free Jaffa exclusion, developer tools, persistence,
  manual removal, render nodes, offsets and helmet coverage are unchanged.
- No C# source or gameplay Def is modified by this art-only milestone.

## Validation completed

- The maintainer copied the twelve files directly into their final paths and
  validated their real pawn rendering.
- Black, silver and gold variants were selected deterministically with the
  existing developer actions.
- The mark remains small and centered on the forehead in `South`.
- `North`, `East` and `West` display no mark.
- All twelve source files are `128×128`; the nine hidden facings are fully
  transparent and the three visible facings use the same alpha footprint.
- Forced build of assembly `0.3.91.0` completed successfully.
- Duration-formatting, visual-asset and project-consistency checks passed.
- The visual checker reports `24` final local families, `605` PNG files, `72`
  canonical families and no missing or unregistered reference.
- `git diff --check` passed and the maintainer reviewed the final scope in the
  IDE.
- The three wiki images remain byte-identical to their gameplay `South` files.

## Publication state

Publication-only revision `r2` changes documentation status only. The validated
state is published through the final commit, integration into `develop`, the
unique annotated tag `v0.3.91-dev` and synchronization of the separate wiki.
Local suffixes `r1` and `r2` do not appear in the final commit or tag.

## Next milestone

No later milestone number is reserved. Select the next Phase 1 visual or
presentation family only after `0.3.91-dev` publication is verified.
