# Project state

0.2.27-dev-r2 - Mixed Tok'ra defensive diversion effect

Status: r2 prepared for local application and testing after r1 validated the diversion but the effect felt too deterministic as a pure stun.

This milestone turns the trusted Tok'ra communicator into the first active
defensive-support tool. The help remains rare, discreet and defensive: it does
not spawn a permanent ally squad and does not open trade, recruitment or quests.

Current mod metadata after applying `0.2.27-dev`:

- `About/About.xml`: `modVersion = 0.2.27-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.27`

## Implemented in this milestone

```text
trusted communicator diversion command
requires powered communicator
requires trusted Tok'ra tier
requires active hostile pawns on the map
disrupts up to 3 hostile pawns briefly
r1 fixes RimWorld 1.6 stun invocation by matching StunFor parameters by type
r2 mixes short immediate stun with delayed vomiting for biological targets
five-day cooldown stored on the communicator
vanilla message and closeable dialog
```

## Current limits

```text
no Tok'ra reinforcement squad
no offensive use outside an active threat
no direct item reward
no medical request yet
no safehouse-lead communicator request yet
no pawn-operated communicator job yet
no trade
no recruitment
no questline step yet
```

## Recommended next implementation path

```text
0.2.28-dev - Add pawn-operated communicator interaction
0.2.29-dev - Add communicator medical-support request
0.2.30-dev - Add communicator safehouse-lead request
later      - Add short Tok'ra questline and race/culture branches
```

## Build note

This milestone updates C# gameplay code. Use a forced rebuild after applying the
ZIP.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
