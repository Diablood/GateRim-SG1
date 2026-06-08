# Jaffa puberty dependency prototype

## Scope of 0.1.34-dev

This milestone adds the first progressive biological consequence for compatible
Jaffa who reach puberty without an implanted Prim'ta.

## Thresholds

```text
under 10 biological years
    ↓
implantation unavailable

10 to 11 biological years
    ↓
implantation available without medical dependency

12 biological years and older without Prim'ta
    ↓
progressive Prim'ta deficiency
```

## Hediff

```text
SG1_JaffaPrimtaDependency
```

## Scanner

```text
GameComponent_JaffaPrimtaDependency
```

The first prototype checks spawned pawns on active maps every `2500` ticks,
approximately once per in-game hour.

## Progression

Severity increases by:

```text
0.1 per in-game day
```

| Severity | Stage | Immunity | Healing |
|---:|---|---:|---:|
| `0` to below `0.25` | Early | `×0.85` | unchanged |
| `0.25` to below `0.5` | Moderate | `×0.65` | `×0.9` |
| `0.5` to below `0.75` | Advanced | `×0.4` | `×0.7` |
| `0.75` to `1` | Critical | `×0.15` | `×0.45` |

The prototype creates a serious vulnerability to disease and slows recovery.
It does not directly kill the pawn yet.

## Relief

Adding:

```text
SG1_JaffaPrimta
```

removes the dependency immediately.

## Scope boundaries

This first prototype does not yet add:

```text
tretonin substitution
caravan and world-pawn progression
direct lethal consequences
cultural mood thoughts
formal Prim'ta ceremony
```

## Test checklist

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `11`.
3. Confirm no dependency Hediff appears.
4. Spawn a compatible Jaffa aged `12` without Prim'ta.
5. Wait up to one in-game hour.
6. Confirm `Prim'ta deficiency` appears.
7. Accelerate time and confirm severity increases.
8. Confirm immunity and healing modifiers worsen at the documented thresholds.
9. Implant a physical Prim'ta larva.
10. Confirm the dependency Hediff disappears immediately.
11. Save and reload during progression and confirm the severity persists.
