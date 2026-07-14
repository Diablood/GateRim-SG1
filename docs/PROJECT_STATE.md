# Project state

Current milestone: `0.3.90-dev - Remove legacy Jaffa forehead-mark migration genes`

Status: validated after corrective functional revision `r2`; publication-only
revision `r3` records the final state for the annotated tag.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.89-dev`.
- Final branch: `feature/final-jaffa-forehead-mark-gene-icons`.
- Final functional revision: `r2`.
- Publication-document revision: `r3`.
- Assembly version: `0.3.90.0`.
- Final annotated tag: `v0.3.90-dev`.
- Integration target: `develop` by fast-forward.

## Published scope

- Remove the three obsolete technical migration `GeneDef` records:
  - `SG1_JaffaForeheadMark_Generic`;
  - `SG1_JaffaForeheadMark_GenericGold`;
  - `SG1_JaffaForeheadMark_GenericSilver`.
- Remove their French `GeneDef` translations, `GR_DefOf` fields and the C# scan
  that converted those former genes into intrinsic forehead-mark data.
- Remove both obsolete gene-icon locations and the discarded local `r1` wiki
  copies:
  - `Textures/Genes/Icons`;
  - `Textures/UI/Genes`;
  - the corresponding files under `docs/wiki/images`.
- Keep the visual baseline at `605` PNG files, `72` canonical texture families
  and exactly `21` approved final local families.
- Preserve the active intrinsic forehead-mark system and its stable save IDs:
  - `SG1_JaffaForeheadMark_GenericIntrinsic`;
  - `SG1_JaffaForeheadMark_GenericSilverIntrinsic`;
  - `SG1_JaffaForeheadMark_GenericGoldIntrinsic`.

## Compatibility decision

The maintainer is the only user of the early development saves and explicitly
accepts dropping compatibility for saves that still contain the obsolete
technical genes. Current saves that already serialize intrinsic forehead-mark
data remain supported because their `JaffaForeheadMarkDef` identifiers and pawn
records are unchanged.

## Validation completed

- Forced build of assembly `0.3.90.0` completed successfully.
- Duration-formatting, visual-asset and project-consistency checks passed.
- `git diff --check` passed.
- The three obsolete genes are absent from normal and developer gene inspection.
- Goa'uld-domain Jaffa retain their intrinsic black, silver and gold marks.
- Developer assignment, persistence, manual removal and save/reload remain
  functional for intrinsic marks.
- The six active gameplay-gene icons from `0.3.89-dev` remain unchanged.
- No new relevant XML, translation, missing-texture, DefOf or C# error was found
  during the focused test.
- The wiki drafts no longer present migration-only genes or their discarded
  icons as gameplay assets.

## Publication state

The validated feature commit is intended for fast-forward integration into
`develop`, followed by the unique annotated tag `v0.3.90-dev` and synchronization
of the separate wiki. Local suffixes `r1`, `r2` and `r3` do not appear in the
final commit or tag.

## Next decided milestone

The next visual lot is separate from this cleanup: replace the actual intrinsic
pawn overlays under `Textures/Things/Pawn/Humanlike/JaffaForeheadMarks` with a
small Apophis forehead symbol. The ordinary, elite and First Prime variants use
black, silver and gold treatments, remain removable intrinsic pawn data and are
rendered only in the `South` facing. This work must be tested against hairstyles,
helmets, save/reload and manual removal before it is accepted as final art.
