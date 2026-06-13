# AGENTS.md

## Project

This repository contains the RimWorld 1.6 mod `GateRim SG-1`.

## Working rules

- Work in small, testable milestones on dedicated Git branches.
- Do not switch to `main`: the usable base is the latest validated published branch or explicit `v...-dev` tag.
- Use four spaces for indentation where applicable.
- Preserve `About/ModIcon.png`.
- Do not add temporary patch notes or `README-*.txt` files at the repository root.
- Keep English source Defs and French translations aligned.
- Keep README, technical documentation, and `docs/wiki/` drafts aligned when behavior changes.
- Do not commit ZIP patch archives stored at the repository root.
- Do not push commits, tags, or branches unless explicitly requested.
- Do not rewrite or move existing Git tags unless explicitly requested.

## C# changes

- For C# changes, use a forced rebuild after extracting or applying patches because preserved ZIP timestamps can leave an obsolete DLL treated as current.
- Prefer the existing Windows build scripts.
- Use `dotnet build ... -t:Rebuild` when invoking `dotnet` directly.

## Delivery

- Summarize the modified files.
- Report the executed build or validation commands and their outcome.
- Propose a short Git commit message in the format:
  `version - description`
- When relevant, propose an annotated Git tag prefixed with `v`.