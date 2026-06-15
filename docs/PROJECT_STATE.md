# Project state

Current milestone: `0.2.32-dev-r2 - Remove redundant power line from Tok'ra status report`.

## Current status

`0.2.32-dev` added a pawn-operated Tok'ra communicator status report. The report
lets a selected colonist consult the secure communicator without sending a real
request or consuming any cooldown.

`0.2.32-dev-r1` kept the feature unchanged and replaced the first
technical/debug-style report text with a more in-universe Tok'ra transmission.

`0.2.32-dev-r2` keeps the same gameplay and removes the visible power-state
line from the report because the communicator can only be operated when its
power state is already meaningful to the player.

## Implemented status report information

```text
selected colonist -> right-click communicator
consult Tok'ra channel status
Tok'ra trust state
main channel state
defensive diversion availability or cooldown
tactical assessment availability or cooldown
medical support availability or cooldown
emergency medical cache availability or cooldown
active hostile count
wounded/sick human colonist count
no request sent
no cooldown consumed
no gameplay effect triggered
```

## Current polish note

A broader end-of-project pass should review all player-facing letters, messages,
popups and dialogue windows for RP tone. The 0.2.32 status report is the first
localized correction in that direction.

## Current mod metadata after applying `0.2.32-dev-r2`

- `About/About.xml`: `modVersion = 0.2.32-dev-r2`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` remain set to `0.2.32`

## Build note

This patch changes localization and metadata only. A forced rebuild is still safe
and recommended in the normal validation workflow.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
