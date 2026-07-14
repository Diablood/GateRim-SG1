# Current testing — final Goa'uld and Jaffa command icons

Jalon : `0.3.92-dev`
Révision visuelle finale validée : `r1`
Révision documentaire de publication : `r2`
Version de DLL validée : `0.3.92.0`

## Selected concepts — approved

- autonomous hunt: moving brown symbiote and red target reticle;
- emergency extraction: brown symbiote over a prone host, surgical table/lamp
  and red emergency burst;
- forced implantation: brown symbiote, grey host and short red impact arrow;
- ritual implantation: brown symbiote, grey host, red implantation arrow and
  gold ceremonial seal.

## PNG and wiki preparation — passed

- Four gameplay PNGs exist at exactly `64×64` under `Textures/UI/Commands`.
- Every file uses a real alpha channel with transparent exterior pixels.
- No generated checkerboard remains in the final files.
- Every matching `docs/wiki/images` copy is byte-identical.
- Each icon is displayed on its dedicated functional wiki page.
- `Primta-Formal-Ceremony.md` displays the shared ritual icon.

## Build and static checks — passed

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd -ExpectedVersion 0.3.92-dev

git diff --check
```

Validated visual-audit summary:

```text
Final local texture families: 28
Local PNG files: 605
Local texture families: 72
Direct external texture paths: 6
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

## Focused in-game validation — passed

- `Chasse autonome` renders correctly on a free Goa'uld symbiote.
- The developer-only instant `Extraction d'urgence` command renders correctly
  during recent implantation.
- `Implantation forcée` renders correctly for its adjacent-host test path.
- `Implantation rituelle` renders correctly for the Goa'uld ritual flow.
- The shared ritual family remains suitable for Tok'ra implantation/offer
  controls and the formal Jaffa Prim'ta ceremony.
- All four concepts remain distinct and readable at normal gizmo size.
- No opaque square, checkerboard, clipping, incorrect tint or magenta
  missing-texture fallback was observed.
- No gameplay behavior or command availability rule changed.

## Publication result

Publication-only revision `r2` changes documentation status only. The validated
state is prepared for the final feature-branch commit, fast-forward integration
into `develop`, annotated tag `v0.3.92-dev` and separate-wiki synchronization.
The final commit and tag omit local suffixes `r1` and `r2`.
