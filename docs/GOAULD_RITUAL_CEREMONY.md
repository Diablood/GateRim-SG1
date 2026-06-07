# Core Goa'uld ritual ceremony

## Scope of 0.1.24-dev

This milestone transforms controlled ritual implantation from an instant action
into a timed core ceremony that remains independent from optional DLCs.

## Flow

```text
selected free symbiote
    ↓ Ritual implantation
explicit map target selection
    ↓
timed core ceremony: 600 ticks
    ↓ target stays valid, reachable and within 12 cells
recent Goa'uld implantation
```

## Cancellation

The ceremony is cancelled when:

```text
the player clicks Cancel ritual
the free symbiote becomes unavailable, dead or downed
the target becomes unavailable, invalid or already implanted
the target leaves the 12-cell ritual range
the target becomes unreachable
```

On cancellation:

```text
the free symbiote remains available
no persistent identity is consumed
autonomous hunting can resume later
```

## Save persistence

The free-symbiote `ThingComp` saves:

```text
ritualTarget
ritualTicksRemaining
ritualTicksTotal
```

A ritual can therefore be saved and resumed after reload.

## Parameters

| Property | Value |
|---|---:|
| Ritual radius | `12` cells |
| Ceremony duration | `600` ticks |
| Target selection | Explicit map targeting |
| Identity transfer | Same `GoauldSymbioteData` on completion |
| DLC dependency | None |

## Optional DLC direction

This core flow remains the fallback without `Ideology`.

A future optional `Ideology` integration can reuse the existing DLC ritual
framework for ceremony quality, roles, participants, rooms and themed objects,
then call the same GateRim SG-1 identity-transfer core on successful completion.

## Test checklist

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Disable autonomous hunt for a controlled test.
4. Click `Ritual implantation`.
5. Select a compatible humanoid within 12 cells.
6. Confirm that the ritual does not implant instantly.
7. Confirm the inspection panel displays the target and remaining duration.
8. Save and reload during the ceremony.
9. Confirm the countdown resumes.
10. Let the ceremony complete.
11. Confirm recent implantation and persistent identity transfer.
12. Start another ritual and click `Cancel ritual`.
13. Confirm no implantation occurs and the free symbiote remains.
14. Start another ritual and move the target out of range.
15. Confirm automatic cancellation.
