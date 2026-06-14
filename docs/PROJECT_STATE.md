# Project state

0.2.26-dev - Add trusted Tok'ra secure communicator foundation

Status: prepared for local application and testing.

This milestone adds the first trusted-tier Tok'ra communicator prototype. It is
intentionally a foundation: it gives the player a visible, powered object and a
vanilla dialog for the secure channel, but it does not yet grant medical help,
safehouse leads, quests, recruitment, trade or military aid.

Current mod metadata after applying `0.2.26-dev`:

- `About/About.xml`: `modVersion = 0.2.26-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.26`

## Implemented in this milestone

```text
buildable Tok'ra secure communicator
powered building using Microelectronics as a construction prerequisite
trusted-tier command gate
short historical message when a channel is opened
vanilla closeable dialog listing future request families
```

## Current limits

```text
no medical request yet
no safehouse-lead request yet
no defensive aid yet
no trade
no recruitment
no questline step yet
```

## Recommended next implementation path

```text
0.2.27-dev - Add communicator medical-support request
0.2.28-dev - Add communicator safehouse-lead request
0.2.29-dev - Add trusted Tok'ra tactical warning prototype
later      - Add short Tok'ra questline and race/culture branches
```

## Build note

This milestone adds C# gameplay code and a building def. Use a forced rebuild
after applying the ZIP.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
