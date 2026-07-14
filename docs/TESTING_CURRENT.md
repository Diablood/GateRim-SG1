# Current testing — legacy Jaffa forehead-mark gene cleanup

Jalon : `0.3.90-dev`
Révision fonctionnelle finale validée : `r2`
Révision documentaire de publication : `r3`
Version de DLL validée : `0.3.90.0`

## Build and static checks — passed

The forced assembly build completed successfully after the C# cleanup. The
following checks passed from the repository root:

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd -ExpectedVersion 0.3.90-dev

git diff --check
```

Validated visual-audit result:

```text
Final local texture families: 21
Local PNG files: 605
Local texture families: 72
Direct external texture paths: 6
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

## Focused in-game validation — passed

- The main menu reports `0.3.90-dev`.
- The obsolete technical genes are absent from normal and developer gene or
  xenotype inspection:
  - `SG1_JaffaForeheadMark_Generic`;
  - `SG1_JaffaForeheadMark_GenericGold`;
  - `SG1_JaffaForeheadMark_GenericSilver`.
- Goa'uld-domain Jaffa still receive and render intrinsic forehead marks.
- The ordinary black, elite silver and First Prime gold developer actions remain
  functional.
- Intrinsic marks persist through save/reload.
- Manual removal persists and does not cause an already initialized pawn to be
  marked again automatically.
- The six active gameplay-gene icons finalized in `0.3.89-dev` are unchanged.
- No new relevant XML, translation, missing-texture, DefOf or C# error was found
  during the focused test.

## Wiki validation — passed

- `docs/wiki/Home.md`, `Content-Status.md` and `Visual-Assets.md` describe the
  intrinsic pawn-data system rather than obsolete migration genes.
- The three discarded gene-icon copies are absent from `docs/wiki/images`.
- No broken reference to those removed files remains.

## Accepted compatibility limit

Private saves that still contain the early technical mark genes are no longer a
supported migration source. Saves already using intrinsic forehead-mark data
remain supported.

## Publication result

Publication-only revision `r3` changes documentation status only. The validated
state is prepared for the final feature-branch commit, fast-forward integration
into `develop`, annotated tag `v0.3.90-dev` and separate-wiki synchronization.

The next test plan belongs to the separate intrinsic pawn-overlay art milestone:
small Apophis marks, black/silver/gold variants and `South`-only rendering.
