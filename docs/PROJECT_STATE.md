# Project state

Current milestone: `0.3.88-dev - Add final storyteller SG-1 portrait`

Status: validated and published. Final local revision `r3` records the
publication closure after the validated visual revision `r1` and the
finalization revision `r2`.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.87-dev` at commit
  `d905e8466fd58e3300404140b5fe420e8bca2198`.
- Final feature branch: `feature/final-storyteller-portrait`.
- Final local revision: `r3`; the suffix remains local and is omitted from the
  final commit and tag.
- Published assembly version: `0.3.88.0`.
- Integration branch: `develop`, updated by fast-forward from the validated
  feature commit.
- Published annotated tag: `v0.3.88-dev`.
- Separate wiki: synchronized from `docs/wiki/` and published with the same
  milestone version.

## Published scope

- Replace only the existing large and tiny portraits of
  `SG1_GateRimStoryteller`.
- Keep the stable texture paths:
  - `Storytellers/SG1_Command`;
  - `Storytellers/SG1_Command_Tiny`.
- Use the two exact maintainer-provided PNG files without redrawing or resizing.
- Preserve the storyteller Def, name, descriptions, components, incident cadence,
  Cassandra baseline, strategic orchestration and save behavior.
- Copy the large portrait byte-identically to
  `docs/wiki/images/SG1_Command.png` and display it on
  `docs/wiki/Storyteller-SG1.md`.
- Do not change any other visual family.

## Validated assets

- Large portrait: `560×600`, transparent RGBA PNG.
- Tiny portrait: `122×130`, transparent RGBA PNG.
- The tiny portrait is a dedicated close crop of the same character rather than
  an automatic runtime reduction.
- Visual identity: human SG-1 field officer, olive uniform and tactical vest,
  Earth/SG-1 insignia, clean painterly style close to vanilla storyteller art.
- The wiki copy of the large portrait is byte-identical to the gameplay PNG.

## Validation result

Local revision `r1` passed the maintainer's real-interface test:

- the large portrait is correctly framed and is not stretched or clipped;
- transparency is preserved without an opaque white rectangle;
- the tiny portrait is centered, sharp and recognizable at its actual UI size;
- comparison with Cassandra, Phoebe and Randy revealed no presentation issue;
- storyteller selection, description, cadence and strategic behavior remain
  unchanged;
- the main menu reports `0.3.88-dev` and no new relevant texture, XML or C#
  error was reported.

Revision `r2` passed the final static checks:

- both storyteller texture families are classified as `final` / `done`;
- the exact visual-checker whitelist contains `11` final local families;
- duration formatting, visual assets, project consistency and `git diff --check`
  pass;
- README, wiki-home, content-status, project-state, current-test and changelog
  versions resolve to `0.3.88-dev`;
- the documented and compiled DLL version remains `0.3.88.0`.

Revision `r3` changes only publication-state documentation. It changes no PNG,
C# source, Def, gameplay behavior, assembly or save data.

## Publication result

- Final commit: `0.3.88-dev - finalize storyteller portrait`.
- The validated feature commit is integrated into `develop` by fast-forward.
- `develop` and annotated tag `v0.3.88-dev` point to the same commit.
- The separate `GateRim-SG1.wiki` repository is synchronized and published.
- Local `r1`, `r2` and `r3` suffixes do not appear in the final commit or tag.

## Next work

No next milestone number or branch is assigned. The next task must be selected
from the remaining Phase 1 backlog in `docs/ROADMAP.md`; Phase 2 remains blocked
until the core-faction completion gate is explicitly satisfied.

Before starting that milestone, update local `develop`, verify that it matches
`v0.3.88-dev`, then create a new dedicated `feature/*` or `fix/*` branch.
