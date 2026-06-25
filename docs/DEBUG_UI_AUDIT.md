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

## Nested debug-action menu (`0.3.35-dev`)

The flat `GateRim SG-1` action category had reached seventy separate entries and no longer scaled with the recurrent mission pool.

`0.3.35-dev` keeps the existing RimWorld category and exposes four ordered entries directly inside it:

```text
GateRim SG-1
├─ Tok'ra...
│  ├─ Show communicator availability
│  ├─ Introduction arc...
│  ├─ Module study...
│  ├─ Organic operations...
│  │  ├─ Framework...
│  │  ├─ Observation...
│  │  ├─ Intelligence recovery...
│  │  ├─ Wounded agent...
│  │  ├─ Medical handoff...
│  │  ├─ Distress call...
│  │  ├─ Temporary-base delivery...
│  │  └─ Diversion assault...
│  └─ Safehouse and intelligence chain...
├─ Jaffa...
│  └─ Forehead marks...
├─ Culture...
└─ Inspect mission definitions
```

No additional `GateRim SG-1...` action is inserted below the category. This avoids a redundant click while retaining native submenus for the systems that need them.

Within each submenu, reports appear first, followed by setup or offer actions, progression actions, outcome actions and destructive reset or cleanup actions. Short labels rely on the parent menu for context instead of repeating `Tok'ra ops:` or another technical prefix on every row.

The hierarchy uses RimWorld's normal `DebugActionNode` children. It does not introduce a custom window, does not change contextual gizmos and does not expose any developer action in normal play.

## Later top-level extensions

The category remains compact while later validated systems add one branch only
when they represent a distinct family of tests.

`0.3.36-dev` added:

```text
Equipment...
```

for non-lethal capture tools. `0.3.39-dev` adds:

```text
Goa'uld...
└─ Free-symbiote incursion...
   ├─ Show current scaling
   ├─ Force current scaling
   ├─ Force weak-colony scaling
   └─ Force advanced-colony scaling
```

The current intended root order is therefore:

```text
Tok'ra...
Goa'uld...
Jaffa...
Equipment...
Culture...
Inspect mission definitions
```

The Goa'uld branch is developer-only, keeps scaling diagnostics inside its
submenu and does not expose storyteller points or incident eligibility in
normal play.

## Diagnostic versus action boundary (`0.3.42-dev`)

The advanced GateRim SG-1 option is now explicitly read-only. It may expose:

- persistent IDs and raw counters;
- detailed inspection strings and status reports;
- sample or identity reports from the settings page;
- routine lifecycle traces in `Player.log`.

It must not expose commands that force, complete, fail, reset or bypass gameplay state. Those commands use `GR_Debug.DeveloperActionsEnabled`, which is true only while RimWorld developer mode is active.

The published audit closes two remaining leaks:

- the Tok'ra communicator's organic-operation force/progress/reset menu;
- the duplicate direct launch gizmo on the decoded Goa'uld relay world site.

The ordinary caravan right-click route for the relay remains available, and all contextual player commands keep their existing ownership, faction and mission checks.

## Player-facing unlock visibility (`0.3.42-dev`)

Developer diagnostics and player-facing progression are separate concerns. The pawn right-click menu must not reveal trusted-tier Tok'ra support before the colony has earned that tier.

While trust is below `Trusted`, the communicator therefore exposes only:

- the read-only channel status consultation;
- an interaction for an organic operation that is already genuinely proposed or active.

The future trusted requests are omitted entirely rather than displayed with an `insufficient trust` reason. Once the trusted tier is reached, ordinary contextual failures remain visible as disabled options: pawn capability, reachability, reservation, missing power, cooldown, missing threat or missing patient. This keeps useful immediate feedback without turning the menu into a catalogue of future unlocks.
