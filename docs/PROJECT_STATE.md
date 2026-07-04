# Project state

Current milestone: `0.3.67-dev - Adopt develop-based branch workflow`
- closed after final local revision `r4`.

## Repository state

- Starting published tag: `v0.3.66-dev`.
- Canonical integration branch: `develop`, initially created exactly from the
  commit targeted by `v0.3.66-dev`.
- Validated milestone branch: `feature/develop-branch-workflow`.
- Published version and final unique tag: `0.3.67-dev` / `v0.3.67-dev`.
- Technical assembly version: `0.3.67.0`.
- Final validated local revision: `r4`.
- Fast-forward integration into `develop`: completed.
- Main repository publication: completed.
- Separate wiki synchronization and publication: completed because the two
  public version metadata lines changed.
- `main` remains unchanged and reserved for the future stable `1.0.0` line.

## Published workflow

GateRim SG-1 now uses a lightweight integration model:

- `develop` is the canonical integrated state of the latest validated
  development milestone;
- ordinary milestones start from up-to-date `develop` on temporary
  `feature/*` branches;
- corrections against the current development line start from `develop` on
  temporary `fix/*` branches;
- validated branches are integrated with `git merge --ff-only`;
- annotated `v...-dev` tags are created only after integration and point to the
  same commit as local and remote `develop`;
- temporary branches may be retained or deleted only after tag verification;
- `main` remains reserved for `1.0.0` and later stable releases or hotfixes;
- annotated tags are compared through `git rev-list -n 1 <tag>` or the peeled
  `<tag>^{}` form rather than the tag-object identifier.

## Published documentation

- Add `docs/BRANCHING_WORKFLOW.md` as the durable branching reference.
- Update `AGENTS.md`, `README.md`, `docs/README.md` and
  `docs/MILESTONE_PUBLICATION.md` to use `develop` as the ordinary base.
- Record durable branch-topology checks in `docs/TESTING.md`.
- Preserve every existing tag and historical feature branch without rewriting
  history.
- Keep remote publication of temporary branches optional.
- Document divergence handling, future `1.0.0` promotion and stable hotfixes.

## Final validation

The final `r4` validation confirmed:

- `develop` initially matched the commit targeted by `v0.3.66-dev`;
- `feature/develop-branch-workflow` descended from `develop`;
- `main` remained unchanged;
- the project-consistency check completed with every check passing after the
  two wiki metadata versions were aligned;
- assembly `0.3.67.0` rebuilt successfully;
- RimWorld reached the main menu with the minimal mod list and no new load
  error attributable to the milestone;
- no gameplay C#, Def, translation, texture, save data or storyteller behavior
  changed;
- the only wiki changes were the documented-version and content-status revision
  lines.

## Next step

The next selected gameplay milestone is:

`0.3.68-dev - Add open-conflict Goa'uld pressure reduction`

It must start from published `develop` after `v0.3.67-dev`, on:

`feature/goauld-open-conflict-pressure-reduction`.
