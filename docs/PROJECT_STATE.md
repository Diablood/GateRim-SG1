# Project state

Current milestone: `0.2.30-dev - Add Tok'ra emergency medical cache request` adds a trusted, pawn-operated request that places a small emergency medical cache near the secure communicator when a wounded or sick colonist is present.

0.2.30-dev - Tok'ra emergency medical cache request

Status: patch generated for local validation.

This milestone gives the trusted communicator its first limited material support request without turning the Tok'ra into traders or direct healers.

Current mod metadata after applying `0.2.30-dev`:

- `About/About.xml`: `modVersion = 0.2.30-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.30`

## Implemented in this milestone

```text
selected colonist -> right-click communicator
request emergency medical cache
trusted Tok'ra tier required
powered communicator required
wounded or sick human colonist required
small cache placed near communicator
medicine plus optional tretonin dose
seven-day dedicated cache cooldown
no direct healing, trade, recruitment, quest or military aid
```

## Current limits

```text
cache content is deliberately small
no direct healing
no recurring supply chain
no Tok'ra trade inventory
no recruitment
no questline step yet
```

## Recommended next implementation path

```text
0.2.31-dev - Add communicator safehouse-lead request
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
