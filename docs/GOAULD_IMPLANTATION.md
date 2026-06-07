# Recent Goa'uld implantation prototype

## Definition

```text
SG1_GoauldRecentImplantation
```

## Scope of 0.1.11-dev

This milestone introduces an XML-only temporary `HediffDef` representing the critical period immediately after an adult Goa'uld symbiote enters a humanoid host.

The state can currently be added manually through developer mode. It does not yet originate from the free-symbiote pawn and does not yet convert the victim automatically into `SG1_GoauldHost`.

## Current behavior

| Property | Current value |
|---|---:|
| Duration | `60000` ticks |
| Approximate duration | `1` in-game day |
| Visible countdown | Enabled |
| Pain offset | `+0.15` |
| Automatic host conversion | Not implemented |
| Medical interruption | Not implemented |

## Why this intermediate state exists

The final possession workflow needs a visible transition between:

```text
Free symbiote
    ↓
Recent implantation
    ↓
Active Goa'uld host
```

This transition creates a future intervention window for medical treatment, Tok'ra extraction technology or emergency surgery.

## Future C# responsibilities

Later milestones should:

- apply the Hediff after a successful forced implantation attack;
- apply the Hediff after a ritual implantation;
- store the symbiote identity while it is inside the victim;
- interrupt or remove the Hediff when a treatment succeeds;
- convert the victim into an active Goa'uld host when the timer ends;
- preserve the symbiote when changing hosts.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Start a temporary test map.
3. Enable developer mode.
4. Select a humanoid pawn.
5. Use the debug action for adding a Hediff.
6. Add `recent Goa'uld implantation`.
7. Confirm that the health tab displays the state.
8. Confirm that a remaining-time countdown appears.
9. Confirm that the pawn receives additional pain.
10. Let one in-game day pass and confirm that the state disappears.
11. Switch to French and verify the translated label, description and stage.
12. Inspect `Player.log` for `SG1_GoauldRecentImplantation` errors.


## 0.1.17-dev interactive entry point

The recent-implantation state can now originate from a free symbiote through the manual adjacent-target `Forced implantation` command.

The same persistent identity is transferred before the free symbiote pawn is consumed.


## 0.1.18-dev automatic conversion

When the recent-implantation countdown expires, the same persistent parasite identity automatically moves into:

```text
SG1_GoauldHostSymbiote
```

The victim keeps the original germline xenotype and gains an acquired active-host health state.


## 0.1.19-dev emergency interruption

Before the countdown expires, the selected host exposes:

```text
Emergency extraction
```

The manual prototype removes recent implantation and respawns the same parasite
as a free pawn with the same persistent ID.
