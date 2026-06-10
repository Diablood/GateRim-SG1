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
