# Project state

Current milestone: `0.3.89-dev - Add final gene icons and complete world visual references`

Status: validated and published. Final local revision `r2` records the
publication closure after the validated implementation and visual revision
`r1`.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.88-dev`.
- Final feature branch:
  `feature/final-gene-icons-and-world-visual-references`.
- Final local revision: `r2`; the suffix remains local and is omitted from the
  final commit and tag.
- Published assembly version: `0.3.89.0`.
- Integration branch: `develop`, updated by fast-forward from the validated
  feature commit.
- Published annotated tag: `v0.3.89-dev`.
- Separate wiki: synchronized from `docs/wiki/` and published with the same
  milestone version.

## Published scope

- Replace six stable gameplay-gene textures:
  - `UI/Genes/SG1_JaffaLineage`;
  - `UI/Genes/SG1_JaffaPhysiology`;
  - `UI/Genes/SG1_JaffaPouchPotential`;
  - `UI/Genes/SG1_JaffaSymbioteCompatibility`;
  - `UI/Genes/SG1_GoauldLongevity`;
  - `UI/Genes/SG1_NaquadahBlood`.
- Remove the obsolete development-only `SG1_JaffaLongevity` `GeneDef`, its
  French translation entries and `Textures/UI/Genes/SG1_JaffaLongevity.png`.
  Jaffa longevity remains provided by `SG1_JaffaPrimta`.
- Reclassify the four existing `World faction` texture families as
  `final` / `done`.
- Add byte-identical wiki copies for the six gene icons, four faction icons and
  seven event-site icons already accepted as final.
- Leave the three technical forehead-mark icons unchanged; they are not gameplay
  genes and remain scheduled for a later Jaffa-mark visual lot.
- Preserve all remaining Def names, stable texture paths, gene effects, factions,
  world sites, storyteller behavior and runtime C# logic.
- Preserve the existing French label `compatibilité avec un symbiote immature`;
  its multiline wrapping was observed in the gene interface and the maintainer
  explicitly chose not to rename it in this milestone.

## Published visual baseline

- `608` PNG files under `Textures/`;
- `75` canonical local texture families;
- `21` exact final local families;
- `27` temporary-original families;
- `15` temporary-recolor families;
- `6` temporary-reuse families;
- `6` personal-icon placeholder families;
- priorities: `8` P0, `21` P1, `25` P2 and `21` done.

## Validation result

Local revision `r1` passed the maintainer's build, static and real-interface
validation:

- the forced `0.3.89.0` assembly build completed successfully;
- duration formatting, visual assets, project consistency and
  `git diff --check` passed;
- all six gameplay-gene icons are present, transparent, centered and readable at
  their actual Biotech gene-UI size;
- no icon is missing, magenta, clipped or backed by an opaque rectangle;
- `SG1_JaffaLongevity` and its obsolete texture are absent;
- Jaffa carrying a Prim'ta retain their existing longevity support;
- the four faction icons and seven event-site icons remain unchanged in game;
- the gene, faction and site galleries render correctly in the wiki drafts;
- all protected wiki copies remain byte-identical to their gameplay textures;
- no new relevant XML, texture or C# error was reported in `Player.log`.

Revision `r2` changes only publication-state documentation. It changes no PNG,
Def, translation, C# source, assembly, gameplay behavior or save data.

## Publication result

- Final commit: `0.3.89-dev - finalize gene and world visual references`.
- The validated feature commit is integrated into `develop` by fast-forward.
- `develop` and annotated tag `v0.3.89-dev` point to the same commit.
- The separate `GateRim-SG1.wiki` repository is synchronized and published.
- Local `r1` and `r2` suffixes do not appear in the final commit or tag.

## Next work

No next milestone number or branch is assigned. The next task must be selected
from the remaining Phase 1 backlog in `docs/ROADMAP.md`; Phase 2 remains blocked
until the core-faction completion gate is explicitly satisfied.

Before starting that milestone, update local `develop`, verify that it matches
`v0.3.89-dev`, then create a new dedicated `feature/*` or `fix/*` branch.
