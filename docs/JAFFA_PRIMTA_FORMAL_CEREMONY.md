# Formal Jaffa Prim'ta ceremony prototype

## Scope of 0.1.38-dev

This milestone adds a Core + Biotech formal Prim'ta ceremony while keeping the
existing medical operation available as a parallel workflow.

## Ritual basin

The existing:

```text
SG1_GoauldRitualBasin
```

now receives:

```text
GateRimSG1.Jaffa.CompProperties_JaffaPrimtaCeremony
GateRimSG1.Jaffa.Comp_JaffaPrimtaCeremony
```

## Command

Select a player-owned ritual basin and use:

```text
Formal Prim'ta ceremony
```

French label:

```text
Cérémonie formelle du Prim'ta
```

## Requirements

```text
player-owned ritual basin
    ↓
eligible Jaffa within 6 cells
    +
physical Prim'ta larva within 6 cells
    ↓
600-tick ceremony
```

The target must remain:

```text
alive
spawned
not downed
within range
eligible for Prim'ta implantation
```

The reserved larva must remain:

```text
spawned
within range
available in its stack
```

## Completion

On completion:

```text
one larva consumed
    ↓
SG1_JaffaPrimta attached
    ↓
puberty dependency removed automatically
    ↓
first-implantation cultural memory granted automatically when applicable
```

## Cancellation

The ceremony can be cancelled manually. It is also cancelled automatically if:

```text
target moves out of range
target becomes downed or invalid
larva is removed or moved out of range
basin is destroyed or loses player ownership
```

The larva is only consumed on successful completion.

## Persistence

The selected target, reserved larva and remaining ticks are persisted through
save and reload.

## Design boundary

This is a Core + Biotech fallback. Future optional Ideology integration can
reuse the same eligibility and completion logic while presenting a richer ritual
experience.

## Test checklist

1. Build with `build.cmd`.
2. Construct or spawn `Goa'uld ritual basin`.
3. Place one physical Prim'ta larva within `6` cells.
4. Place one eligible Jaffa aged `10+` within `6` cells.
5. Select the basin and start `Formal Prim'ta ceremony`.
6. Select the Jaffa target.
7. Confirm progress appears in the inspection panel.
8. Save and reload during the rite.
9. Confirm progress persists.
10. Let the ceremony complete.
11. Confirm one larva is consumed.
12. Confirm `SG1_JaffaPrimta` appears.
13. Confirm dependency and cultural-thought regressions.
14. Start another rite and move the larva away.
15. Confirm automatic cancellation without larva consumption.
16. Start another rite and use the manual cancel command.
17. Confirm cancellation without larva consumption.


## 0.1.38-dev-r1 ticker registration fix

The ritual basin explicitly declares:

```xml
<tickerType>Normal</tickerType>
```

This registration is required because the timed ceremony advances through:

```text
Comp_JaffaPrimtaCeremony.CompTick()
```

Without a normal ticker, the ceremony can start and display its persisted
references, but the remaining duration stays frozen at `600 / 600`.
