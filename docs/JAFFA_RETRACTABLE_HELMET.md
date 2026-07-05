# Retractable Jaffa helmet modes

Version: `0.1.67-dev`

## Scope

The existing deployed Jaffa helmet now switches between a deployed `FullHead`
state and an internal retracted `UpperHead` state. Raw armor ratings remain
identical.

## Modes

| Mode | Undrafted | Drafted |
|---|---|---|
| Automatic | Retracted | Deployed |
| Always deployed | Deployed | Deployed |
| Always retracted | Retracted | Retracted |

The mode persists through save and reload. A worn-apparel gizmo cycles it.
A lightweight updater synchronizes automatic mode every `15` ticks.

## Manual test checklist

1. Build and launch RimWorld with developer mode enabled.
2. Equip the deployed Jaffa helmet and confirm the gizmo appears.
3. In automatic mode, confirm retracted visuals and `UpperHead` outside draft.
4. Draft the pawn and confirm deployed visuals and `FullHead` coverage.
5. Undraft the pawn and confirm retraction.
6. Cycle through always deployed and always retracted.
7. Save and reload in each mode to confirm persistence.
8. Confirm the armor ratings stay identical in both positions.
9. Confirm all Jaffa apparel remains under `Apparel > Jaffa`.

## Logging policy

Automatic helmet deployment and retraction are intentionally silent. These
state transitions happen routinely during pawn generation, draft changes and
periodic synchronization. Emitting a normal log message for every transition
can create noisy in-game developer popups and verbose Unity stack traces.

The synchronization behavior remains unchanged.

## Multiple helmet pairs (`0.3.74-dev`)

`CompProperties_RetractableJaffaHelmet` can now declare its own `deployedDef`
and `retractedDef`. Empty fields retain the original
`SG1_JaffaDeployedHelmet` / `SG1_JaffaRetractedHelmet` fallback.

The mission officer uses the dedicated pair:

```text
SG1_JaffaOfficerDeployedHelmet
SG1_JaffaOfficerRetractedHelmet
```

This keeps one shared persistent mode implementation while preventing either
helmet from changing into the other set. Raw armor and body coverage remain
identical to the standard pair.

Final cumulative revision `0.3.74-dev-r2` validates and verifies that the
capture target wears
the officer deployed Def immediately after pawn generation. This preserves zero
random commonality while guaranteeing that the retractable component starts from
the correct pair. All three modes, pair isolation and save/reload are validated.
