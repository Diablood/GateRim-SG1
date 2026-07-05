# GateRim SG-1 — Agent instructions

## Project

This repository contains the RimWorld 1.6 mod `GateRim SG-1`.

## General rules

- Before starting any task, read `docs/PROJECT_STATE.md` and treat it as the current project handoff document.
- Also read `docs/ROADMAP.md`; it is the durable backlog for future additions, deferred improvements and cross-discussion decisions.
- Read `docs/BRANCHING_WORKFLOW.md` before creating, merging, tagging or deleting branches.
- Work in small, testable milestones on dedicated `feature/*` or `fix/*` branches.
- Do not work directly on `main` or `develop`.
- `develop` is the canonical integration branch and must match the latest validated published development tag before a new milestone starts.
- Start every ordinary milestone branch from an up-to-date local `develop` branch.
- Reserve `main` for the first stable `1.0.0` line and later stable releases or hotfixes.
- Use four spaces for indentation where applicable.
- Preserve `About/ModIcon.png`.
- Do not create temporary `README-*.txt` patch-note files at the repository root.
- Keep English source Defs and French translations aligned.
- Keep README, technical documentation, and `docs/wiki/` drafts aligned when behavior changes.
- Consult `docs/README.md` before adding documentation. Prefer updating an
  existing subsystem document over creating one file for each micro-milestone.
- Keep published history in `docs/CHANGELOG.md` and Git tags, active work in
  `docs/ROADMAP.md`, and speculative work in `docs/IDEAS_TO_REVISIT.md`; do not
  create an archive directory as a substitute for consolidation.
- When `docs/wiki/` changes, synchronize the separate `GateRim-SG1.wiki`
  repository, review its diff, commit it and push it as part of publication
  unless explicitly instructed not to publish.
- Do not commit ZIP archives stored at the repository root.
- Do not publish a milestone unless explicitly requested.
- Once the maintainer asks to publish a validated milestone, that authorization
  includes the final feature-branch commit, fast-forward integration into
  `develop`, push of `develop`, the annotated final `v...-dev` tag and the
  separate wiki when `docs/wiki/` changed, unless the maintainer explicitly
  excludes one of them.
- Publishing the temporary feature branch itself is optional. Tags and
  `develop` are the durable history after integration.

## Context recovery

After a discussion reaches its context limit, after starting a new discussion or whenever project continuity is uncertain, read these files before proposing work:

1. `AGENTS.md`;
2. `docs/PROJECT_STATE.md`;
3. `docs/ROADMAP.md`;
4. `docs/BRANCHING_WORKFLOW.md` before any branch operation;
5. `docs/MILESTONE_PUBLICATION.md` before any commit, merge, tag, push or wiki publication instructions.

Do not rely on conversation memory for deferred work when it can be recorded in `docs/ROADMAP.md`. Add newly validated future work to that file during the current milestone.

## C# build rules

- After applying C# changes, run a forced rebuild because extracted ZIP files may preserve timestamps.
- A technical assembly-version change also requires a rebuild even when gameplay code is unchanged.
- Prefer the existing Windows scripts.
- When invoking dotnet directly, use `-t:Rebuild`.

## Delivery rules

- Delivery ZIPs must contain only files added or modified since the previous
  delivered revision. Use a whole-repository archive only for explicit recovery
  or when the maintainer specifically requests one. A complete-file patch means
  full replacement contents for the included paths, not a copy of the repository.
- Summarize modified files.
- Report the validation and build commands executed.
- Before requesting in-game validation, add the exact manual test checklist to
  `docs/PROJECT_STATE.md` and to the relevant technical document.
- Manual tests must name the exact developer-menu path and visible label for
  every action. Do not require hidden state changes or debug actions that do
  not exist.
- Keep the mandatory happy-path test short. Put refusal, expiration and other
  regression cases in a separate optional section.
- After the user reports the results, record the validation status in
  `docs/PROJECT_STATE.md` and update the corresponding `docs/ROADMAP.md`
  checklist.
- Report any remaining uncertainty.
- Propose a short Git commit message:
  `version - description`
- Treat the annotated `v...-dev` tag on the integrated `develop` commit as a
  mandatory part of publishing every validated milestone, unless the
  maintainer explicitly asks to omit it.
