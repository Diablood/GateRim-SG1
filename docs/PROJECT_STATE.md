# Project state

Current milestone: `0.3.82-dev - Add Goa'uld alliance rupture after major failure`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.81-dev` at commit
  `65164357377f95faf9ff8b64520fe2c57ac2b981`.
- Final feature branch: `feature/goauld-alliance-major-failure-break`.
- Final local revision: `r1`.
- Published assembly version: `0.3.82.0`.
- Final annotated tag: `v0.3.82-dev`.
- Integration target: `develop`, by fast-forward from the validated feature
  branch.

## Validated result

- Only successful **natural** shared alliance reprisals under
  `Commandement SG-1` can enter the automatic failure observation.
- Shared reprisals created or executed through developer commands remain
  excluded from the automatic consequence.
- At least `6` combined Jaffa are required when the two detachments spawn.
- The result is evaluated once when no more than `20%` of the combined force
  remains active on the target home map.
- That decisive defeat deterministically schedules one exact-pair alliance
  rupture after `1–2` RimWorld days, with no second random roll.
- At most one rupture may be pending globally.
- The stored pair transitions from `Alliance` to `Rivalry` through the existing
  persistent relation tracker.
- The pending rupture is cancelled if either domain becomes inactive or the
  exact pair is no longer allied before the deadline.
- The deadline is suspended outside `Commandement SG-1` and shifted forward
  when the storyteller becomes active again.
- Resolution produces one neutral diplomatic letter selected from three RP
  variants with local anti-repetition. It names both domains, explicitly states
  `Alliance -> Rivalry` and has no map or pawn target.
- Raid budgets, doctrines, storyteller frequency, permanent domain profiles,
  vanilla goodwill and all territorial state remain unchanged.

## Validation record

The maintainer validated final local revision `r1` in game.

Confirmed required results:

- assembly `0.3.82.0` and the expected automated consistency checks;
- one pending exact pair with the expected `1/6` debug survivor state;
- persistence of the pair and deadline through save and reload;
- one targetless diplomatic letter naming both domains;
- exact relation transition from `Alliance` to `Rivalry`;
- final rupture outcome `Completed` with no new pending rupture;
- no raid, reward, goodwill or territorial side effect;
- no new relevant error in the accepted `Player.log`.

The optional cancellation and storyteller-suspension procedures remain durable
regression coverage and are not claimed as part of this focused acceptance.

## Publication state

The final milestone state is intended for one commit, fast-forward integration
into `develop`, push of `develop`, annotated tag `v0.3.82-dev` and synchronization
of the separate wiki because this milestone changes `docs/wiki/` sources.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.82-dev` point to
   the same integrated commit;
2. read `docs/ROADMAP.md` and select one distinct decided milestone;
3. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch;
4. update this handoff before implementation.
