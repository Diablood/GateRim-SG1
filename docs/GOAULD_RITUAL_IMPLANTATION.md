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


## 0.1.23-dev explicit map target selection

The ritual command now starts a vanilla map-targeting flow:

```text
selected free symbiote
    ↓ Ritual implantation
map cursor
    ↓ click one valid humanoid
recent Goa'uld implantation
```

The target validator keeps the existing restrictions:

```text
compatible humanlike pawn
spawned and alive
minimum biological age
not already implanted or possessed
within 12 cells
reachable by the symbiote
```

The autonomous pursuit job is interrupted when ritual targeting starts, and the
next autonomous scan is delayed briefly so the controlled action remains stable.

The previous automatic nearest-target selection is removed from the player-facing
ritual path.


## 0.1.24-dev timed core ceremony

After explicit target selection, ritual implantation now enters a `600`-tick
ceremony instead of transferring instantly.

The selected target must remain valid, reachable and within `12` cells until
completion. The free symbiote inspection panel displays the remaining duration.

The ceremony can be cancelled manually and is automatically interrupted when
its continuity requirements are no longer met.

The ritual state is saved and resumes after reload.
