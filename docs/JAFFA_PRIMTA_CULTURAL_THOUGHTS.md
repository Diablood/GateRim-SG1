# Jaffa Prim'ta cultural thoughts

## Scope of 0.1.35-dev

This milestone adds a lightweight mood layer distinct from the medical
dependency introduced in `0.1.34-dev`.

## Situational thought

```text
SG1_AwaitingPrimta
```

| Condition | Value |
|---|---|
| Compatible Jaffa lineage | Required |
| Minimum biological age | `10` |
| Implanted Prim'ta | Absent |
| Mood effect | `-1` |
| Duration | Situational |

The thought disappears automatically after implantation.

## First-implantation memory

```text
SG1_ReceivedPrimta
```

| Property | Value |
|---|---:|
| Mood effect | `+3` |
| Duration | `5` days |
| Stack limit | `1` |
| Repeated after removal and reimplantation | No |

## Persistent registry

```text
GameComponent_JaffaPrimtaCulturalThoughts
```

The component stores pawn `ThingID` values after the first successful Prim'ta
implantation. This prevents repeated mood bonuses after extraction and
reimplantation.

## Design boundary

The first implementation is intentionally generic.

Future milestones can refine these thoughts for:

```text
loyalist Jaffa
traditional unaffiliated Jaffa
Free Jaffa
System Lord factions
optional Ideology integration
```

The cultural layer remains separate from:

```text
SG1_JaffaPrimtaDependency
```

which represents the biological danger beginning at age `12`.

## Test checklist

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `9` without Prim'ta.
3. Confirm `awaiting Prim'ta` is absent.
4. Spawn a compatible Jaffa aged `10` or `11` without Prim'ta.
5. Confirm `awaiting Prim'ta` appears with mood `-1`.
6. Implant one physical larva.
7. Confirm `awaiting Prim'ta` disappears.
8. Confirm `received Prim'ta` appears with mood `+3`.
9. Confirm the memory lasts `5` days.
10. Remove the Prim'ta in developer mode.
11. Confirm `awaiting Prim'ta` returns.
12. Reimplant a larva.
13. Confirm `received Prim'ta` is not granted a second time.
14. Save and reload before reimplantation and confirm the one-time registry persists.
