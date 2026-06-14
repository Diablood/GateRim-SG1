# Project state

Current milestone: `0.2.29-dev - Add Tok'ra communicator medical support request` adds a trusted, pawn-operated medical-guidance request to the secure communicator. It requires a relevant wounded or sick colonist, grants Medicine XP to the operator and does not directly heal or deliver items.

0.2.28-dev - Pawn-operated Tok'ra secure communicator

Status: validated locally after `r1` hides direct communicator gizmos outside debug diagnostics.

This milestone makes the trusted Tok'ra communicator behave like a device operated by a colonist instead of an instant building button.

Current mod metadata after applying `0.2.28-dev`:

- `About/About.xml`: `modVersion = 0.2.28-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.28`

## Implemented in this milestone

```text
selected colonist -> right-click communicator
use secure communicator job
request defensive diversion job
short operation delay at the building
trusted/powered/threat/cooldown checks preserved
validated mixed diversion effects preserved
direct building gizmos hidden unless advanced debug information is visible
```

## Current limits

```text
no Tok'ra reinforcement squad
no direct medical request yet
no safehouse-lead communicator request yet
no trade
no recruitment
no questline step yet
```

## Recommended next implementation path

```text
0.2.29-dev - Add communicator medical-support request
0.2.30-dev - Add communicator safehouse-lead request
later      - Add short Tok'ra questline and race/culture branches
```

## Build note

This milestone updates C# gameplay code. Use a forced rebuild after applying the ZIP.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
