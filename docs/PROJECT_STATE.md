# Project state

Current milestone: `0.3.84-dev - Add first bounded Goa'uld territorial takeover`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.83-dev` at commit
  `f4006e3e0007aab99678e166abedc63a53ad1689`.
- Final feature branch: `feature/goauld-bounded-territorial-takeover`.
- Final local revision: `r1`.
- Published assembly version: `0.3.84.0`.
- Final annotated tag: `v0.3.84-dev`.
- Integration target: `develop`, by fast-forward from the validated feature
  branch.

## Decided scope

- Add the first real territorial consequence without creating a world-war
  simulation or bypassing the `0.3.83-dev` safety evaluator.
- Restrict the consequence to one permanent vanilla Goa'uld `Settlement` owned
  by the losing domain of an exact pair in `OpenConflict`.
- Preserve the same world object, ID, name and tile. Only the faction owner may
  change.
- Schedule natural attempts only under `Commandement SG-1`, every `45–90` days.
- Persist at most one pending takeover globally with a `1–2` day resolution
  delay. Suspend its deadline, natural cadence and cooldowns under another
  storyteller rather than accumulating backlog.
- Preserve the published minimum of two territorial domains, sparse-world
  threshold `active domains + 2`, final-settlement protection, expansion weights
  `1.00 / 0.50 / 0.25 / 0.10`, size-delay multipliers `1 / 2 / 4 / 8` and
  a projected automatic-hegemony ceiling of `75%` with exactly two active
  domains or `50%` with three or more.
- Add protection for a loaded settlement map, a player world object on the same
  tile and an active quest referencing the exact settlement.
- Keep global `15`-day, involved-domain base `30`-day and exact-pair `60`-day
  cooldowns. The gaining-domain delay uses its post-transfer settlement count.
- Keep the exact pair in open conflict after the transfer. Do not alter any
  player or outside-faction relation or goodwill.
- Announce a successful transfer through one neutral three-variant RP letter
  targeted at the unchanged colony tile.
- Do not create or destroy a settlement, defeat a faction, change a tile, alter
  raids or doctrines, produce rewards or modify storyteller frequency.

## Implemented local revision r1

- `GameComponent_GoauldTerritorialStrategyTracker` now has schema `2`, a
  persistent natural-attempt deadline, natural-versus-developer source state and
  real `CompletedTransfer` outcomes.
- A natural check occurs once every `45–90` days under `Commandement SG-1`.
  Eligible candidates come only from active `OpenConflict` pairs. The gaining
  domain is weighted down as it grows, while domains owning more transferable
  settlements naturally expose more possible losing candidates.
- Scheduling and resolution both call the shared territorial evaluator. Any
  changed owner, relation, domain activity, density, final-settlement,
  hegemony, loaded-map, player-presence, quest-target or exact-pair
  compatibility condition cancels the pending state before mutation.
- Successful resolution uses RimWorld's settlement faction setter, invalidates
  the cached faction-colored world material, destroys stale trader stock and
  clears cached former inhabitants before refreshing the world renderer.
- The same settlement object, ID, label and tile remain. Counts and faction
  existence are unchanged.
- Three English and French letter variants describe the takeover and name the
  settlement, losing domain and gaining domain, with local anti-repetition.
- Schema-`1` dry-run cooldowns from `0.3.83-dev` are cleared during migration.
  A still-pending dry run is cancelled safely as `CancelledLegacyDryRun`; it is
  never upgraded into an unexpected real takeover during load.
- The developer hierarchy exposes report, reconciliation, deterministic
  reservation, immediate natural attempt, immediate resolution, cancellation
  and reset actions under
  `Goa'uld... > Domain reactions... > Territorial strategy...`.

## Validation result

The maintainer reports the focused `r1` procedure as successful.

Validated behavior includes:

- assembly `0.3.84.0` and successful local build and consistency checks;
- one natural-source takeover reservation for an exact pair in open conflict;
- persistence of the exact pair, settlement ID, source, creation snapshot and
  deadline through save and reload before resolution;
- one `CompletedTransfer` ownership change preserving settlement name, ID, tile
  and total permanent-settlement count;
- the losing domain retaining at least one settlement and the gaining domain
  remaining within the required three-domain `50%` ceiling;
- immediate refresh of the world icon and inspect-string faction;
- one targeted neutral RP letter naming the settlement and both domains;
- persistence of the new owner, completed outcome and cooldowns after a second
  save and reload;
- active global, involved-domain and exact-pair cooldowns using the winner's
  post-transfer size multiplier;
- unchanged exact-pair `OpenConflict` / vanilla `Hostile` relation, player and
  outside-faction goodwill, faction count, raids, doctrines, missions, rewards
  and storyteller frequency;
- no new relevant `Player.log` error.

Optional migration, storyteller-suspension, loaded-map, player-presence,
active-quest and strategic-limit cases from `docs/TESTING_CURRENT.md` are not
claimed unless they were part of the maintainer's focused test.

## Publication state

The final milestone state is published through one feature-branch commit,
fast-forward integration into `develop`, push of `develop`, annotated tag
`v0.3.84-dev` and synchronization of the separate wiki because this milestone
changes `docs/wiki/` sources.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.84-dev` point to
   the same integrated commit;
2. read `docs/ROADMAP.md` and select one distinct decided milestone;
3. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch;
4. update this handoff before implementation.
