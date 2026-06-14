# GateRim SG-1 — Agent instructions

## Project

This repository contains the RimWorld 1.6 mod `GateRim SG-1`.

## General rules

- Before starting any task, read `docs/PROJECT_STATE.md` and treat it as the current project handoff document.
- Work in small, testable milestones on dedicated Git branches.
- Do not switch to `main`: it is not a usable working base.
- Start new branches from the latest validated published branch or explicit `v...-dev` tag.
- Use four spaces for indentation where applicable.
- Preserve `About/ModIcon.png`.
- Do not create temporary `README-*.txt` patch-note files at the repository root.
- Keep English source Defs and French translations aligned.
- Keep README, technical documentation, and `docs/wiki/` drafts aligned when behavior changes.
- When `docs/wiki/` changes, synchronize the separate `GateRim-SG1.wiki`
  repository, review its diff, commit it and push it as part of publication
  unless explicitly instructed not to publish.
- Do not commit ZIP archives stored at the repository root.
- Do not push commits, branches, or tags unless explicitly requested.

## C# build rules

- After applying C# changes, run a forced rebuild because extracted ZIP files may preserve timestamps.
- Prefer the existing Windows scripts.
- When invoking dotnet directly, use `-t:Rebuild`.

## Delivery rules

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
- When relevant, propose an annotated tag prefixed with `v`.
