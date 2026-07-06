# Project state

Current milestone: `0.3.83-dev - Add Goa'uld territorial safeguards and diplomatic coherence`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.82-dev` at commit
  `bb1fc23ef843513b626ec05f4da73839fe5863ff`.
- Final feature branch:
  `feature/goauld-territorial-strategic-safeguards`.
- Final local revision: `r3`.
- Published assembly version: `0.3.83.0`.
- Final annotated tag: `v0.3.83-dev`.
- Integration target: `develop`, by fast-forward from the validated feature
  branch.

## Decided scope

- Propose three Goa'uld System Lord faction instances by default in the vanilla
  world-faction list while retaining one required baseline so the player may
  reduce the count when the vanilla faction limit matters.
- Enable territorial strategy only with at least two active territorial Goa'uld
  domains. A territorial domain must be non-defeated and own at least one
  permanent vanilla settlement.
- Count only permanent Goa'uld `Settlement` world objects. Exclude player and
  non-Goa'uld settlements, mission sites, temporary sites, battlefields,
  caravans and travelling groups.
- Protect the final settlement of every domain.
- Require at least `active domains + 2` permanent Goa'uld settlements before a
  hostile transfer can pass the dry-run evaluator.
- Slow automatic expansion as the gaining domain grows: weights
  `1.00 / 0.50 / 0.25 / 0.10` and cooldown multipliers `1 / 2 / 4 / 8` for
  `1 / 2 / 3 / 4+` settlements.
- Reject an automatic gain that would place one domain above `50%` of all
  permanent Goa'uld settlements.
- Persist at most one global territorial reservation, with global, involved
  domain and exact-pair cooldowns.
- Suspend pending deadlines and cooldowns outside `Commandement SG-1` instead
  of consuming a backlog.
- Cancel an invalid reservation when its exact domains, settlement, ownership,
  relation, world density or compatibility conditions no longer hold.
- Reconcile GateRim pair relations to vanilla faction relations:
  `Neutral/Rivalry/Truce -> Neutral`, `OpenConflict -> Hostile`,
  `Alliance -> Ally`.
- Never alter relations toward the player or any outside faction.
- Apply no real territorial consequence in this milestone.

## Corrective local revision r3

- The first `r2` launch reached the initial player map, but `Player.log` repeated
  RimWorld's error that `Faction.SetRelationDirect` cannot be used for factions
  whose relation kind is controlled by goodwill.
- The previous `permanentEnemy` flag also prevented the correct goodwill API
  from changing relations between two Goa'uld instances.
- `SG1_GoauldSystemLordPrototype` now uses
  `permanentEnemyToEveryoneExcept = SG1_GoauldSystemLordPrototype`: Goa'uld
  domains remain permanently hostile to the player and every outside faction,
  while relations between two instances of the same Goa'uld Def may use
  vanilla goodwill normally.
- Inter-domain reconciliation now targets goodwill `-100 / 0 / 100` for
  `Hostile / Neutral / Ally` through `TryAffectGoodwillWith` with messages and
  hostility letters disabled, verifies the resulting relation kind and emits at
  most one warning per failed pair/state.
- No territorial rule, persistent field, debug action or world-object mutation
  changes from `r2`.

## Corrective local revision r2

- The first `r1` build stopped in
  `GoauldTerritorialSafeguardUtility.EvaluateTransfer` with `CS0165` because a
  null-conditional `TryGetRelation(..., out relation)` call did not definitely
  assign `relation` when the tracker was absent.
- `relation` now starts at `GoauldInterDomainRelation.Neutral`, the tracker is
  stored explicitly, and `TryGetRelation` is called only when that tracker is
  non-null.
- No gameplay rule, persistent field, Def, debug action, localization or manual
  test expectation changes from `r1`.

## Implemented local revision r1

- `SG1_GoauldSystemLordPrototype` now proposes three entries at world creation;
  `requiredCountAtGameStart` remains one and the vanilla list stays editable.
- `GoauldTerritorialSafeguardUtility` centralizes permanent-settlement scope,
  minimum-domain and sparse-world rules, final-settlement protection,
  expansion weights, delay multipliers and the `50%` hegemony ceiling.
- `GameComponent_GoauldTerritorialStrategyTracker` persists one dry-run
  reservation, three cooldown layers, storyteller suspension, reconciliation,
  completion and cancellation outcomes without mutating any world object.
- The existing relation tracker now persists vanilla reconciliation metadata,
  synchronizes every pair on creation, transition, load and periodic
  reconciliation, and exposes actual versus expected vanilla relation kinds.
- A pending alliance rupture for the same exact pair blocks a territorial
  reservation; unrelated pairs are not globally blocked.
- The developer hierarchy contains
  `Goa'uld... > Domain reactions... > Territorial strategy...` with report,
  reconciliation, creation, immediate dry-run resolution, cancellation and
  reset actions. The relation menu adds `Set all pairs: Open conflict`.
- `docs/GOAULD_TERRITORIAL_SAFEGUARDS.md` records the complete reusable
  contract and explicit non-effects.

## Validation result

The maintainer reports the focused `r3` procedure as successful.

Validated behavior includes:

- assembly `0.3.83.0` and successful local build;
- three Goa'uld entries proposed by default while the vanilla faction list
  remains reducible;
- coherent vanilla `Neutral`, `Hostile` and `Ally` relations for all five
  GateRim pair states;
- persistent `Alliance -> Ally` between two Goa'uld instances;
- permanent Goa'uld hostility toward the player and outside factions;
- unchanged player and outside-faction goodwill;
- correct active-domain and permanent-settlement counts, sparse-world threshold,
  final-settlement protection, expansion weights and hegemony ceiling;
- one exact dry-run reservation preserved through save and reload;
- `CompletedDryRun` with global, domain and pair cooldowns;
- no settlement owner, settlement count, tile, faction, raid, reward, doctrine
  or storyteller-frequency mutation;
- no new relevant `Player.log` error, including no repeated
  `SetRelationDirect` rejection or goodwill-reconciliation loop.

Optional boundary cases from `docs/TESTING_CURRENT.md` are not claimed unless
they were part of the maintainer's focused test.

## Publication state

The final milestone state is published through one feature-branch commit,
fast-forward integration into `develop`, push of `develop`, annotated tag
`v0.3.83-dev` and synchronization of the separate wiki because this milestone
changes `docs/wiki/` sources.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.83-dev` point to
   the same integrated commit;
2. read `docs/ROADMAP.md` and select one distinct decided milestone;
3. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch;
4. update this handoff before implementation.
