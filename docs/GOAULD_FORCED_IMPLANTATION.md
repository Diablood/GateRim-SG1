# Forced Goa'uld implantation prototype

## Scope of 0.1.17-dev

This milestone adds the first interactive forced-implantation loop.

It intentionally uses a manual adjacent-target command before autonomous AI is introduced.

## Current flow

```text
Free Goa'uld symbiote pawn
    ↓ select while adjacent to one compatible humanoid
Forced implantation command
    ↓
SG1_GoauldRecentImplantation
    ↓
Free symbiote pawn is consumed
```

The same persistent `symbioteId` moves from the free pawn into the host Hediff.

## Compatibility rules

| Candidate | Current result |
|---|---|
| Adult humanlike pawn | Allowed |
| Adult Jaffa | Allowed |
| Child under 13 biological years | Rejected |
| Animal | Rejected |
| Mechanoid | Rejected |
| Pawn with recent implantation | Rejected |
| Pawn with adult host symbiote state | Rejected |
| Future Unas | Not yet supported by the humanlike-only rule |

## Important limitation

This is not autonomous melee AI yet.

The tester must:

1. spawn a free symbiote;
2. place it adjacent to a target;
3. select the symbiote;
4. click `Forced implantation`.

A later milestone can add target selection, jobs and autonomous hostile behavior after the identity-transfer path is stable.

## Manual test checklist

1. Build with `build.cmd`.
2. Start RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Enable developer mode.
4. Spawn one `Goa'uld symbiote`.
5. Place an adult humanoid pawn in an adjacent cell.
6. Select the free symbiote.
7. Record the free symbiote ID shown in the inspection panel.
8. Click `Forced implantation`.
9. Confirm that the free symbiote disappears.
10. Confirm that the target receives `recent Goa'uld implantation`.
11. Open the target health-state description.
12. Confirm that the transferred ID is unchanged.
13. Save and reload.
14. Confirm that the ID remains unchanged after reload.
15. Inspect `Player.log` for creation, transfer, attachment and load logs.

## Negative test checklist

- click the command without an adjacent humanoid;
- try a child under 13;
- try an animal;
- try a pawn already carrying recent implantation;
- try a pawn already carrying the adult symbiote host state.

All should be rejected without consuming the free symbiote.

## 0.3.41-dev-r2 interface boundary

The adjacent `Forced implantation` command is retained as a deterministic developer regression tool. It is no longer exposed merely because a hostile free symbiote can be selected. RimWorld developer mode must be enabled before the command appears.

Normal hostile symbiotes continue to use autonomous pursuit and contact implantation without granting the player direct control over their target.
