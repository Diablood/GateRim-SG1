# Project state

Current milestone: `0.3.72-dev - Repair 0.3.71 publication documentation`

- Candidate corrective revision: `r1`.
- The gameplay and duration-formatting implementation validated in
  `0.3.71-dev-r5` is unchanged.
- Targeted build, consistency, startup-version and clean-log validation remain
  required before publication.

## Repository state

- Published starting point: `develop` and annotated tag `v0.3.71-dev` both
  identify commit `a52c5ab13cc6943926b7f5d83743922793e3262a`.
- That immutable commit contains the validated `0.3.71-dev` implementation but
  omitted the final documentation package during publication.
- Active corrective branch:
  `fix/0.3.71-publication-documentation`.
- Current development version: `0.3.72-dev`.
- Technical assembly version: `0.3.72.0`.
- `main` remains reserved for the first stable `1.0.0` line.
- The published `v0.3.71-dev` tag must not be moved, deleted or rewritten.

## Corrective scope

- Restore the final `0.3.71-dev` project state, roadmap, changelog, current-test
  result and durable duration-formatting regression coverage.
- Restore the public wiki sources for the content status and duration-formatting
  contract.
- Record the publication omission explicitly instead of pretending the original
  annotated tag already contained those files.
- Advance the repository and assembly metadata to `0.3.72-dev` /
  `0.3.72.0` so the next integrated commit can receive its own immutable tag.
- Preserve the `104`-key compatibility bridge, shared formatter, audit scripts,
  translations, deadlines, cooldowns, recurrence, persistence and balance
  exactly as validated in `0.3.71-dev-r5`.
- Add no gameplay, faction, mission, incident, storyteller or save-data change.

## Required validation

Before publication:

- `git diff --check` must pass;
- forced build must produce assembly `0.3.72.0`;
- `tools/check-duration-formatting.cmd` must still pass with `104` unique keys;
- `tools/check-project-consistency.cmd` must pass with the corrective version;
- the main menu must load with `0.3.72-dev` metadata;
- `Player.log` must contain no new Harmony, XML, translation or C# error;
- the diff against `v0.3.71-dev` must contain only documentation, wiki sources,
  version metadata and the rebuilt assembly output expected by the project
  workflow.

## Next step

Validate corrective revision `r1`. After explicit approval, finalize the
documentation as published, fast-forward the fix branch into `develop`, create
annotated tag `v0.3.72-dev`, and synchronize the two changed wiki pages. Only
then select the next gameplay milestone from `docs/ROADMAP.md`.
