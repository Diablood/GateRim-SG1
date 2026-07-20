# Retractable Jaffa helmet control

Introduction version: `0.1.67-dev`
Manual-toggle revision: `0.3.101-dev`

## Scope

The deployed and retracted Jaffa helmet Defs remain distinct. Raw armor ratings
stay identical; the deployed Def covers `FullHead`, while the retracted Def
covers `UpperHead`.

## Manual actions

The worn-apparel gizmo now exposes the action that can be performed immediately:

| Current position | Gizmo label | Result |
|---|---|---|
| Retracted | `Deploy helmet` | Switch to the matching deployed Def |
| Deployed | `Retract helmet` | Switch to the matching retracted Def |

Drafting and undrafting no longer alter the helmet. The chosen position persists
through save and reload.

## Legacy-save migration

The historical `Automatic` enum value remains readable only for migration. When
an older save is loaded, the component inspects the physical helmet Def already
stored in the save and converts the legacy value to the matching manual state.

`GameComponent_RetractableJaffaHelmetUpdater` remains as an empty compatibility
type so old saves can resolve it. It performs no periodic scan and no automatic
deployment.

## Multiple helmet pairs

`CompProperties_RetractableJaffaHelmet` continues to declare optional
`deployedDef` and `retractedDef` fields.

The ordinary pair remains:

```text
SG1_JaffaDeployedHelmet
SG1_JaffaRetractedHelmet
```

The capturable officer pair remains:

```text
SG1_JaffaOfficerDeployedHelmet
SG1_JaffaOfficerRetractedHelmet
```

Each helmet changes only within its own pair.

## Validation checklist

1. Equip the ordinary retracted helmet and confirm `Deploy helmet`.
2. Deploy it and confirm the label changes to `Retract helmet`.
3. Draft and undraft the pawn without changing the position.
4. Save and reload once in each position.
5. Repeat the toggle with the officer pair.
6. Confirm deployed coverage is `FullHead` and retracted coverage is `UpperHead`.
7. Confirm raw armor ratings remain identical.
8. Confirm the command icon is a transparent `64×64` cobra-helmet gizmo.
