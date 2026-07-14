# Current testing — final intrinsic Jaffa forehead-mark overlays

Jalon : `0.3.91-dev`
Révision visuelle finale validée : `r1`
Révision documentaire de publication : `r2`
Version de DLL validée : `0.3.91.0`

## Maintainer in-game visual validation — passed

The maintainer placed the twelve supplied PNG files directly in
`Textures/Things/Pawn/Humanlike/JaffaForeheadMarks` and validated the actual pawn
rendering before packaging.

- `GenericJaffaForeheadMark_south.png` displays the ordinary black mark.
- `GenericSilverJaffaForeheadMark_south.png` displays the elite silver mark.
- `GenericGoldJaffaForeheadMark_south.png` displays the First Prime gold mark.
- The three visible files use the same compact Apophis geometry and the same
  alpha footprint.
- The mark is small, centered on the forehead and does not extend above the head.
- Every `north`, `east` and `west` file is fully transparent, so no lateral or
  rear mark is rendered.
- Existing developer actions allow all three ranks to be compared on one pawn
  without relying on random generation.

## Automated image inspection — passed

- All twelve gameplay files are valid `128×128` PNGs.
- The nine hidden-facing files contain no non-transparent pixel.
- Each visible `South` file uses an identical `15×10` alpha bounding box at
  coordinates `(57,55)–(71,64)`.
- Visible colors are exact opaque black `(0,0,0)`, silver `(155,155,155)` and
  gold `(167,161,92)`.
- The three wiki reference files are byte-identical copies of the corresponding
  gameplay `South` files.

## Build and static checks — passed

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd -ExpectedVersion 0.3.91-dev

git diff --check
```

Validated visual-audit result:

```text
Final local texture families: 24
Local PNG files: 605
Local texture families: 72
Direct external texture paths: 6
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

## Focused regression checks — passed

- Ordinary black, elite silver and First Prime gold remain assignable through
  the existing GateRim developer actions.
- The mark remains absent from genes, apparel and inventory.
- The unchanged intrinsic record keeps the selected family through save/reload.
- Manual removal remains persistent after reload.
- Free Jaffa remain unmarked and ordinary Goa'uld-domain assignment is unchanged.
- No new relevant missing-texture, XML, render-node or C# error was found during
  the focused test.

## Wiki validation — passed

- `docs/wiki/Visual-Assets.md` displays all three `South` references.
- Each wiki PNG remains byte-identical to its gameplay texture.
- `Home.md` and `Content-Status.md` report `0.3.91-dev` and the `24` final-family
  baseline.

## Publication result

Publication-only revision `r2` changes documentation status only. The validated
state is prepared for the final commit, integration into `develop`, annotated
tag `v0.3.91-dev` and separate-wiki synchronization. Local `r1` and `r2`
suffixes are omitted from the final commit and tag.
