# GateRim SG-1 — Agent instructions

## Project

This repository contains the RimWorld 1.6 mod `GateRim SG-1`.

## General rules

- Work in small, testable milestones on dedicated Git branches.
- Do not switch to `main`: it is not a usable working base.
- Start new branches from the latest validated published branch or explicit `v...-dev` tag.
- Use four spaces for indentation where applicable.
- Preserve `About/ModIcon.png`.
- Do not create temporary `README-*.txt` patch-note files at the repository root.
- Keep English source Defs and French translations aligned.
- Keep README, technical documentation, and `docs/wiki/` drafts aligned when behavior changes.
- Do not commit ZIP archives stored at the repository root.
- Do not push commits, branches, or tags unless explicitly requested.

## C# build rules

- After applying C# changes, run a forced rebuild because extracted ZIP files may preserve timestamps.
- Prefer the existing Windows scripts.
- When invoking dotnet directly, use `-t:Rebuild`.

## Delivery rules

- Summarize modified files.
- Report the validation and build commands executed.
- Report any remaining uncertainty.
- Propose a short Git commit message:
  `version - description`
- When relevant, propose an annotated tag prefixed with `v`.