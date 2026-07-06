# Project state

Current milestone: `0.3.80-dev - Make Goa'uld relations influence raid doctrines`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.79-dev` at commit
  `e978acf527681c0b0f6456ce08a08a3f41607652`.
- Active branch: `feature/goauld-relations-raid-doctrine-interactions`.
- Final local revision: `r1`, validated and ready for publication.
- Target assembly version: `0.3.80.0`.
- This milestone changes only the weighting of already eligible doctrines for
  ordinary natural Goa'uld Jaffa raids under `Commandement SG-1`.

## Validated design decisions

- Keep the permanent domain profiles authoritative:
  - conquest: `4 / 1 / 1`;
  - enslavement: `2 / 3 / 1`;
  - scorched earth: `2 / 1 / 3`.
- Apply one bounded relation multiplier of `x1.25`:
  - alliance favors direct assault;
  - rivalry favors abduction;
  - open conflict favors destruction;
  - neutrality and truce apply no modifier.
- Resolve only one relation influence with priority
  `open conflict > alliance > rivalry`.
- Never stack multiple alliances, rivalries or conflicting relation effects.
- Keep the relation configuration in XML Defs so later balancing does not
  require an architectural rewrite.
- Apply the modifier only after the existing point and colony-context
  eligibility checks. A zero ineligible weight remains zero.
- Apply the doctrine modifier only under `Commandement SG-1` and only to the
  ordinary natural Goa'uld raid worker.
- Preserve all historical forced debug raid commands as deterministic paths that
  bypass relation doctrine influence.
- Preserve incident frequency, refire delay, vanilla source points, final
  `75% / 110%` pressure factors, alliance outcome probabilities, officer rules,
  pawn budgets and existing raid strategies.

## Implementation

- `GoauldRelationDoctrineModifierDef` declares relation, priority and the three
  doctrine multipliers.
- Five XML Defs document neutrality, rivalry, open conflict, truce and alliance.
  Only rivalry, open conflict and alliance carry a non-neutral multiplier.
- `GoauldRelationDoctrineModifierUtility` reads active relation pairs, ignores
  unrelated or inactive pairs and selects the highest-priority effective Def.
- `IncidentWorker_GoauldJaffaNaturalRaid` now separates:
  1. permanent profile weights;
  2. existing doctrine eligibility;
  3. optional SG-1 Command relation multiplication;
  4. doctrine selection;
  5. the already published point-pressure and alliance-manifestation layers.
- `GoauldDomainDoctrineDebugActions.ShowReport` displays base weights, eligible
  pre-relation weights, selected relation Def, multipliers and final percentages.
- The inter-domain relation report now lists doctrine-weight influence among the
  published strategic effects.

## Expected probability examples

When all three doctrines are eligible:

- conquest under alliance: `5 / 1 / 1`, so direct remains dominant;
- enslavement under alliance: `2.5 / 3 / 1`, so abduction remains dominant;
- scorched earth under alliance: `2.5 / 1 / 3`, so destruction remains dominant;
- conquest in open conflict: `4 / 1 / 1.25`, so direct remains dominant;
- enslavement in rivalry: `2 / 3.75 / 1`, strengthening its existing specialty.

These values influence probability only. They do not guarantee a doctrine.

## Validation result

Final revision `r1` is validated:

- the project builds successfully as assembly `0.3.80.0`;
- the duration audit remains at `104` unique keys;
- project consistency passes across versions, XML, translations, documentation
  and `83` BackstoryDefs;
- neutrality and truce leave eligible doctrine weights unchanged;
- rivalry selects priority `100` and multiplies only eligible abduction by
  `x1.25`;
- alliance selects priority `200` and multiplies direct assault by `x1.25`;
- open conflict selects priority `300`, overrides simultaneous lower-priority
  relations and multiplies only eligible destruction by `x1.25`;
- zero ineligible abduction or destruction weights remain zero;
- non-SG-1 storytellers suspend relation doctrine influence without deleting
  stored relations;
- historical forced direct, abduction and destruction commands remain exact and
  deterministic;
- standard, delayed `75/25` and joint `60/40` alliance regression paths remain
  unchanged;
- no new relevant error appears in the accepted `Player.log`.

Assistant-side static validation also confirms `390` valid XML files, `269` C#
files passing structural checks, `283` coherent Markdown files, complete-file
scope for all `22` revision paths and no change to thresholds, storyteller
frequency, raid-point factors or alliance budget splits.

## Next step

Integrate `feature/goauld-relations-raid-doctrine-interactions` into `develop` by
fast-forward, publish annotated tag `v0.3.80-dev` and synchronize the separate
wiki repository. No later milestone or branch is reserved automatically.
