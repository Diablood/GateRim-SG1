# Project state

Current milestone: `0.2.41-dev - Add Tok'ra decoded mission lead`.

## Current status

`0.2.41-dev` extends the first Tok'ra mission chain after the encoded intelligence packet has been analyzed. The Tok'ra cell can now return with a decoded operational lead around an isolated Goa'uld relay.

The step remains deliberately preparatory: it records a persistent mission-lead state and updates the communicator report, but it does not create a world site, raid, reward, healing, reinforcements, trade or recruitment.

## Current mod metadata after applying `0.2.41-dev`

- `About/About.xml`: `modVersion = 0.2.41-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.41`

## Build note

A forced C# rebuild is required.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
