# GateRim SG-1 — Debug UI and conditional-gizmo audit

## Purpose

Keep normal gameplay readable while preserving advanced prototype diagnostics
for development and troubleshooting.

## Shared visibility rule

`0.1.75-dev` introduces:

```text
Show advanced GateRim SG-1 debug information
```

Advanced diagnostics are visible when:

```text
GateRim debug option enabled
    OR
RimWorld developer mode enabled
```

Warnings and errors in `Player.log` remain visible regardless of this rule.

## First completed pass

| Information or command | Visibility after 0.1.75-dev | Reason |
|---|---|---|
| Free symbiote persistent ID | GateRim debug | Internal identity-transfer diagnostic |
| Free symbiote origin | GateRim debug | Prototype diagnostic until origin affects normal gameplay decisions |
| Autonomous-hunt enabled state and raw cooldown ticks | GateRim debug | Low-level prototype state |
| Active Tok'ra therapeutic-offer remaining days | Contextual | Player needs it while deciding |
| Tok'ra trust tier during an active offer | Contextual | Tier affects the current proposal |
| Raw Tok'ra trust score | GateRim debug | Numeric implementation detail |
| Active Goa'uld ritual target, basin and remaining ticks | Contextual | Useful while the ceremony is active |
| Goa'uld queen raw extraction cooldown ticks | GateRim debug | Low-level timing detail; normal players see the rounded remaining days on the disabled command |
| Routine `GR_Log.Message(...)` lifecycle traces | GateRim debug, `Player.log` only | Useful during tests; must not enter RimWorld's in-game log queue or open an error-looking popup |
| Expected Tok'ra incident refusal during a forced test | GateRim debug, `Player.log` only | Missing eligible patient, active wary cooldown, insufficient trust tier or unavailable entry cell are normal precondition failures, not warnings |
| `GR_Log.Warning(...)` and `GR_Log.Error(...)` | Always logged | Required for troubleshooting |
| Jaffa forehead-mark assignment actions | RimWorld dev mode | Regression and scenario test utility |
| Queen immature-symbiote extraction command | Player-controlled queen or RimWorld dev mode | Normal acquisition is available after the rare queen-arrival incident; developer mode preserves unrestricted tests |

## Gizmo decisions preserved

The first pass does not change contextual gameplay commands:

| Gizmo or command | Current decision |
|---|---|
| Forced Goa'uld implantation | Preserve prototype command |
| Ritual Goa'uld implantation | Preserve contextual command |
| Cancel active ritual | Preserve contextual command |
| Emergency extraction | Preserve narrow recent-implantation command |
| Autonomous-hunt toggle | Preserve prototype command pending broader Goa'uld faction design |
| Tok'ra voluntary implantation | Preserve prototype command |
| Tok'ra therapeutic implantation | Preserve prototype command |
| Refuse tracked Tok'ra offer | Preserve contextual command |
| Jaffa Prim'ta ceremony | Preserve player-faction basin command |
| Jaffa helmet mode | Preserve player-controlled wearer command |

## Remaining follow-up

- Generate the native French translation report before editing the five
  remaining load warnings.
- Revisit the remaining prototype gameplay commands as later natural
  acquisition routes are introduced.
- Keep raw IDs, counters and scanner state behind the shared debug rule.
