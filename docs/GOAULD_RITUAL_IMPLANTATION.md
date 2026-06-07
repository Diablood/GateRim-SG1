# Ritual Goa'uld implantation prototype

## Scope of 0.1.22-dev

This milestone adds a controlled implantation path distinct from autonomous
hunting and contact-based manual tests.

## Current flow

```text
selected free symbiote
    ↓ Ritual implantation command
nearest compatible reachable humanoid within 12 cells
    ↓
recent Goa'uld implantation
```

The free pawn is consumed and the same persistent `GoauldSymbioteData` object is
transferred into the victim.

## Reused architecture

All three entry paths converge on the same transfer logic:

```text
manual adjacent implantation
autonomous contact implantation
controlled ritual implantation
    ↓
TryImplantHost(...)
    ↓
SG1_GoauldRecentImplantation
```

## Current limitation

The prototype automatically chooses the nearest valid humanoid within range.

It does not yet provide:

- explicit pawn selection;
- a ceremony animation;
- ritual duration;
- faction ownership checks;
- prisoner restrictions;
- room, altar or basin requirements;
- surgery-style scheduling.

These can be added after the controlled transfer path has been validated.

## Parameters

| Property | Value |
|---|---:|
| Ritual radius | `12` cells |
| Target | Nearest compatible reachable humanoid |
| Minimum target age | `13` biological years |
| Symbiote identity | Preserved |
| Free symbiote pawn | Consumed after transfer |

## Manual test checklist

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Disable `Autonomous hunt` for a controlled test.
4. Place one compatible adult humanoid within 12 cells but not adjacent.
5. Select the free symbiote.
6. Click `Ritual implantation`.
7. Confirm that the free pawn disappears.
8. Confirm that the target receives `recent Goa'uld implantation`.
9. Confirm that the same persistent symbiote ID is displayed.
10. Save and reload.
11. Confirm identity persistence.
12. Repeat with no compatible target in range and confirm rejection.
