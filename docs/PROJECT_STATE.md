# Project state

Current milestone: `0.3.81-dev - Add shared Goa'uld alliance reprisals`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.80-dev` at commit
  `1758c2d9b94d4b9a3884ca99c846402aa3703a40`.
- Active branch: `feature/goauld-alliance-shared-reprisals`.
- Final local revision: `r2`, validated and ready for publication.
- Target assembly version: `0.3.81.0`.
- The broader functional procedure and the focused `r2` letter retest are both
  accepted. `r2` changes only the presentation and targets of the programming
  and arrival letters.

## Validated design decisions

- Observe only ordinary standard natural Goa'uld Jaffa raids under
  `Commandement SG-1`.
- Exclude delayed reinforcements, simultaneous joint raids, extraction
  reprisals, controlled raids, missions and developer-forced historical paths.
- Require at least `5` initial Jaffa and evaluate the defeat only once when no
  more than `25%` remain active.
- Apply a `25%` chance to schedule the response.
- Keep one pending shared reprisal globally and a `30`-day cooldown for the
  exact pair.
- Delay normal reprisals by `2–4` RimWorld days; developer creation uses the
  existing short-delay convention.
- Use `80%` of the vanilla threat points current when the reprisal is scheduled.
- Split that complete budget `60/40` between the offended domain and one exact
  allied domain.
- Force a direct simultaneous joint assault from opposite reachable map edges.
- Preserve faction colors and temporary cooperation without changing permanent
  relation state, goodwill, territory, raid frequency or storyteller refire.
- Suspend pending delay progression outside `Commandement SG-1`.

## Implementation

- `GameComponent_GoauldDomainReprisalTracker` reuses the published domain
  reaction layer and now stores:
  - eligible standard-raid observations;
  - one shared-reprisal state per exact pair for pending response or cooldown;
  - storyteller-suspension state.
- `IncidentWorker_GoauldJaffaNaturalRaid` registers only successful ordinary
  standard raids and exposes one exact-pair forced joint path for the reprisal.
- `GoauldSharedAllianceReprisalState.cs` persists observations, pawn references,
  exact factions, target map, due tick, budget and cooldown.
- The debug menu provides:
  - `Show domain reaction state`;
  - `Create shared alliance reprisal`;
  - `Trigger pending shared reprisal now`;
  - `Reset shared alliance reprisals`.

## Revision r2 letter correction

The first in-game test found two presentation ambiguities:

1. the programming letter exposed `Se rendre sur les lieux` even though no
   troop or world site existed yet;
2. the arrival letter focused one force and did not make the second detachment
   sufficiently explicit.

`r2` changes only those surfaces:

- the programming letter is informational and has no `LookTargets`, so no camera
  jump is offered;
- the arrival text names the offended domain and its ally and explicitly states
  that two detachments advance from opposite sides;
- the arrival letter carries two `LookTargets`, one representative pawn from
  each newly spawned detachment;
- budget, split, timing, relation checks, persistence and combat behavior remain
  unchanged.

## Validation result

Final revision `r2` is validated:

- debug scheduling and immediate triggering are conforming;
- the exact allied pair, simultaneous opposite-edge forces, temporary
  cooperation, `80%` total budget and `60/40` split are conforming;
- save persistence and the requested functional regressions are conforming;
- the programming letter is informational and exposes no
  `Se rendre sur les lieux` action;
- the arrival letter explicitly announces two detachments and stores one target
  pawn from each force;
- the exact developer action name is
  `Trigger pending shared reprisal now`;
- no new relevant error is reported in the accepted `Player.log`.

## Next step

Integrate `feature/goauld-alliance-shared-reprisals` into `develop` by
fast-forward, publish annotated tag `v0.3.81-dev` and synchronize the separate
wiki repository. No later milestone or branch is reserved automatically.
