# Autonomous free-symbiote hunt

## Scope of 0.1.21-dev

This milestone gives free Goa'uld symbiotes their first autonomous hostile
behavior.

## Flow

```text
free symbiote
    ↓ periodic scan
nearest reachable compatible humanoid
    ↓ dedicated pursuit job
contact
    ↓
recent Goa'uld implantation
```

The same persistent identity-transfer path used by the manual command remains in
use.

## C# classes

```text
Comp_GoauldForcedImplantation
JobDriver_GoauldAutonomousImplant
```

## JobDef

```text
SG1_GoauldAutonomousImplant
```

## Current parameters

| Property | Value |
|---|---:|
| Scan interval | `60` ticks |
| Search radius | `35` cells |
| Minimum target age | `13` biological years |
| Cooldown after extraction | `2500` ticks |
| Locomotion urgency | `Jog` |

## Why a cooldown exists

A symbiote returned to the map after manual extraction or surgery should not
immediately re-enter the patient on the same tick.

The reverse-transfer paths now call:

```text
DetachFromHost(...)
```

before initializing the free pawn. The resulting `lastDetachTick` drives the
short autonomous-hunt cooldown.

## Manual toggle

The selected free symbiote exposes:

```text
Autonomous hunt
```

This toggle is primarily useful during development tests. Manual forced
implantation remains available as a regression tool.

## Current limitations

- target selection is nearest compatible reachable humanoid;
- the symbiote does not yet prioritize downed targets or specific factions;
- `0.3.39-dev` adds a rare threat-scaled colony incident that spawns free symbiotes;
- the prototype uses a dedicated pursuit job but not a full tactical AI tree;
- Unas support remains future work.

## Test checklist

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote at least several cells from an adult humanoid.
3. Confirm autonomous hunt is enabled in the inspection panel.
4. Wait for target acquisition.
5. Confirm the symbiote moves toward the target.
6. Confirm automatic implantation on contact.
7. Confirm the free pawn disappears and recent implantation appears.
8. Confirm the persistent ID is unchanged.
9. Extract the symbiote through surgery.
10. Confirm the free pawn respawns with a non-zero autonomous cooldown.
11. Confirm it does not instantly re-implant the patient.
12. Wait for cooldown expiry and confirm hunting resumes.
13. Toggle autonomous hunt off and confirm pursuit stops.
14. Toggle it on and confirm pursuit resumes.
15. Inspect `Player.log`.

## 0.3.41-dev interface boundary

The `Autonomous hunt` toggle is a developer-only diagnostic control. A hostile free symbiote selected during normal play exposes no command that lets the player suspend or resume its hunt.

The autonomous state itself remains persistent and unchanged; only the visibility of the manual toggle is restricted.
