# Project state

Current milestone: `0.3.79-dev - Add coordinated allied Goa'uld joint raids`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.78-dev` at commit
  `71ec35acad73162e6adb765df4c1a094ed47c3b4`.
- Active branch: `feature/goauld-joint-raids`.
- Final local revision: `r1`, validated and ready for publication.
- Target assembly version: `0.3.79.0`.
- Commit, integration, tag, push and wiki publication still require explicit
  maintainer instruction after this accepted validation.

## Existing relation trigger

No new diplomatic scheduler is required. Since `0.3.66-dev`, while
`Commandement SG-1` is active, every live Goa'uld domain pair automatically
transitions slowly between neutrality, rivalry, open conflict, truce and
alliance. Every real transition produces one neutral RP letter naming both
domains.

That letter changes which future effects are eligible. It never launches a raid
or battlefield immediately and never guarantees the form of the next attack.
Other storytellers preserve the stored relations while suspending automatic
transitions and SG-1-specific consequences.

## Validated design decisions

- The existing natural raid remains the only storyteller incident and retains
  its day, chance and refire delay.
- The alliance factor remains one non-stacking `110%` final budget.
- Only alliance-context raids at `800+` final points can select an allied form.
- `50%` of eligible outcomes remain ordinary single-domain raids.
- The cooperative half of a direct raid is split evenly between:
  - the published delayed `75/25` reinforcement;
  - a simultaneous `60/40` joint raid.
- Abduction and destruction remain either standard or delayed; joint raids use
  direct assault only.
- A joint raid selects one exact allied domain, keeps both faction colors and
  places the forces on reachable opposite map edges.
- One RP letter names both domains when the joint attack arrives.
- Temporary cooperation changes no persistent faction relation or goodwill.
- If either joint detachment retreats or reaches its bounded break threshold,
  both detachments withdraw.
- Defeat causes only ordinary losses and loot. Alliance rupture, shared
  reprisals and territorial effects remain separate future milestones.
- No transport pods, extra raid points, extra incident roll or special reward
  are introduced.

## Implementation

- `IncidentWorker_GoauldJaffaNaturalRaid` now selects `Standard`,
  `DelayedReinforcement` or `JointRaid` only after doctrine and relation pressure
  are resolved.
- Standard keeps all final points on the primary domain.
- Delayed reinforcement preserves the published `75/25` split and hidden
  `1800` to `3600` tick delay.
- Joint raid applies `60/40`, sets opposite `EdgeWalkIn` spawn centers, gives
  the primary incident one shared letter and starts the allied direct force in
  the same execution.
- `GameComponent_GoauldAlliedReinforcementTracker` persists the manifestation,
  exact pair, pawn references, initial counts and allied spawn center.
- Serialized value `0` remains delayed reinforcement, preserving pending
  `0.3.78-dev` saves.
- The existing Harmony hostility override remains restricted to the exact
  active pair.

## Automated validation status

- the forced final C# rebuild succeeds with `0` errors and produces assembly
  `0.3.79.0`;
- `git diff --check` passes;
- the duration audit passes with `104` explicit keys across `272` C# files;
- project consistency passes across `283` Markdown files, all milestone
  metadata and `83` BackstoryDefs;
- focused in-game validation is complete and accepted by the maintainer.

## In-game validation result

The maintainer completed and accepted the mandatory focused test:

- the alliance relation letter named both domains without launching a raid;
- the standard alliance path produced only the primary domain;
- the joint path produced two differently colored forces simultaneously from
  opposite map edges;
- the single localized joint-assault letter named both domains;
- both forces attacked the colony without targeting one another;
- the cooperation report exposed `mode=JointRaid` and both tracked detachments;
- ordering primary withdrawal caused both surviving forces to leave;
- no new relevant error was reported in `Player.log`.

No officer appeared during the deterministic `1200`-point joint test. This is
expected rather than a regression: the forced joint debug path does not enable
the dedicated officer fallback, and the final `1320`-point budget is divided
into `792/528`. At `792` primary points, the ordinary `145`-point guard is not
normally admissible through the pawn-cost curve, leaving no guard for the
officer replacement layer. The published natural officer path remains present
and unchanged.

## Optional regression checks

- Run `Force allied natural raid (1200 points, short delay)` and confirm the
  published delayed arrival-only letter still works.
- Save and reload during an active joint raid and confirm both colors remain
  mutually cooperative.
- Confirm abduction and destruction never select `JointRaid`.
- Confirm another storyteller produces neither automatic relation changes nor
  allied raid forms.
- With three domains, confirm only one allied partner joins a joint raid.

## Next step

Prepare the final documentation-only publication state, then commit, integrate,
tag and synchronize the wiki only on explicit maintainer instruction. No later
milestone or branch is reserved.
