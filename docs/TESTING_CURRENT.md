# Current milestone test procedure

Jalon : `0.3.86-dev - Add final Goa'uld host and Jaffa xenotype icons`

Final local revision: `r2`

Version de DLL attendue : `0.3.86.0`

Status: **final revision `r2` validated and published**.

## Purpose

Validate the first bounded definitive-art replacement lot. Revision `r1`
changed the two xenotype icons and passed the focused in-game test. Revision
`r2` adds byte-identical copies to the wiki source, displays them on the
dedicated pages and extends the checker against visual drift.

The two source visuals were explicitly approved before packaging:

- Jaffa: maintainer-supplied `64×64` human/Baseliner icon carrying the mark of
  Apophis;
- Goa'uld host: approved simplified white Goa'uld symbiote silhouette, used
  because the symbiote itself defines the acquired host state.

## Preconditions

- start from published `develop` aligned with `v0.3.85-dev`;
- work on `feature/final-xenotype-icons`;
- apply `0.3.86-dev-r1` first, then the incremental `0.3.86-dev-r2` ZIP from
  the repository root;
- keep Biotech and Harmony active;
- do not stage the ZIP itself.

## Automated validation

Run from the repository root:

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd

git diff --check
git status --short
```

Expected visual-check summary:

```text
Final local texture families: 9
Local PNG files: 609
Local texture families: 76
Direct external texture paths: 6
Missing local references: 0
Unregistered local families: 0

Visual asset check passed.
```

The check must also confirm that the exact final-family whitelist contains:

```text
UI/Xenotypes/SG1_GoauldHost
UI/Xenotypes/SG1_Jaffa
```

alongside the seven already validated event-site families.


## Focused wiki-reference validation completed in r2

The final validation used:

```powershell
./tools/sync-wiki.cmd
```

The maintainer confirmed:

- `Jaffa.md` visibly displays `images/SG1_Jaffa.png`;
- `Active-Goauld-Host.md` visibly displays `images/SG1_GoauldHost.png`;
- `Visual-Assets.md` displays both final icons in its xenotype table;
- both wiki PNGs are byte-identical to their files under
  `Textures/UI/Xenotypes/`;
- no broken image placeholder or oversized rendering appears.

## Focused in-game validation already completed in r1

### 1. Main-menu smoke test

1. Start RimWorld with GateRim SG-1, Harmony and Biotech.
2. Reach the main menu.
3. Confirm the displayed mod version is `0.3.86-dev`.
4. Confirm no red XML, texture, translation or C# error appears.

### 2. Jaffa xenotype icon

Open a Biotech xenotype-selection, xenotype-editor or another vanilla surface
that displays xenotype icons.

Confirm:

- `Jaffa` uses the new local icon;
- the icon reads as a simplified white human head with the mark of Apophis;
- the unrelated vanilla Hussar icon no longer appears;
- the icon is not magenta or missing;
- the mark remains recognizable at the actual small UI scale;
- no mouth, mask, equipment or added decorative background was introduced.

### 3. Goa'uld-host xenotype icon

On the same type of interface, confirm:

- `Goa'uld host` uses the new simplified symbiote silhouette;
- the personal red-and-black demon mod icon no longer appears;
- the white S-shaped organism, eye, open jaw and body breaks remain readable at
  the actual small UI scale;
- the icon does not resemble a Jaffa forehead mark or a faction emblem;
- the image is not magenta, clipped or unexpectedly tinted.

### 4. Regression boundary

Confirm that the icon-only lot does not change:

- Jaffa inheritance or gene list;
- Goa'uld-host non-inheritable state or gene list;
- xenotype combat-power factors;
- pawn generation;
- implantation, extraction or symbiote lifecycle;
- existing save loading.

A new game is sufficient for visual validation, but one existing save containing
a Jaffa or Goa'uld host may be loaded to confirm that no state migration occurs.

### 5. Log review

Close the game and inspect `Player.log`.

Reject the revision if a new relevant error mentions:

```text
SG1_Jaffa
SG1_GoauldHost
UI/Xenotypes/SG1_Jaffa
UI/Xenotypes/SG1_GoauldHost
Could not load texture
XML error
```

## Expected repository baseline

- `609` PNG files;
- `76` canonical local texture families;
- `9` final local families;
- `13` personal-icon placeholder families;
- `15` P0 families;
- `6` direct external texture paths;
- no local texture reference missing;
- no unregistered local family.

## Final result

Revision `r2` is validated and published. The successful `r1` gameplay result
remains intact, all automated checks pass, and the exact two final PNGs render
correctly on the dedicated wiki pages and the progressive visual-reference
page.

An unrelated pre-existing starter-generation issue was observed during a custom
new-game test: selecting only the Jaffa or Tok'ra xenotype does not necessarily
create the required Prim'ta, Tok'ra symbiote or dual identity. This does not
invalidate the icon-only milestone and is tracked as a separate future gameplay
milestone in `docs/ROADMAP.md`.
