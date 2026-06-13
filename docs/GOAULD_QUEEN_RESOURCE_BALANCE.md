# Goa'uld queen-origin Prim'ta acquisition balance

Version: `0.2.11-dev`

## Purpose

`0.2.10-dev` connected the Goa'uld queen to normal play. The first values were
intentionally simple:

```text
one rare queen arrival
one immature Prim'ta symbiote per extraction
one-day extraction cooldown
10 raw meat for assisted maturation
```

In practice, a single safe queen could become a very strong steady source of
Prim'ta larvae. This balance pass keeps the loop playable while slowing its
long-term output.

## Updated natural arrival

```text
earliestDay: 45
baseChance: 0.015
minRefireDays: 90
```

The incident still refuses to fire while a living player-controlled queen exists
on a map or in a caravan.

## Updated extraction cadence

```text
harvestCooldownTicks: 180000
```

This equals three RimWorld days.

Each extraction still produces:

```text
1 SG1_ImmaturePrimtaSymbiote
```

## Updated maturation cost

```text
1 SG1_ImmaturePrimtaSymbiote
20 raw meat
workAmount: 2400
research: SG1_GoauldBiotechnology
```

The Animals skill requirement remains unchanged:

```text
Animals 4
```

## Preserved behavior

This pass does not change:

```text
manual player extraction
developer-mode access for tests
persistent cooldown saving
duplicate queen prevention
the need for SG1_GoauldBiotechnology
the need for a Prim'ta incubation basin
```

## Expected gameplay effect

One queen becomes a rare strategic biological asset instead of a daily larva
factory. Without specialized future infrastructure, the sustainable output is
approximately:

```text
1 immature symbiote every 3 RimWorld days
+ 20 raw meat and handling work per mature larva
```

## Deferred

Future infrastructure may deliberately improve the output rate, but should do
so through visible investment such as:

```text
specialized queen chamber
advanced Goa'uld biotechnology research
high nutrient cost
power or maintenance requirements
risk events
```

## Manual test checklist

1. Force `SG1_GoauldQueenArrival`.
2. Confirm one player-controlled queen arrives.
3. Extract one immature symbiote.
4. Confirm the command is disabled for about `3.0` RimWorld days.
5. Save and reload during the cooldown.
6. Confirm the remaining cooldown persists.
7. Complete `SG1_GoauldBiotechnology`.
8. Confirm `SG1_IncubatePrimtaLarva` requires:
   - `1` immature Prim'ta symbiote;
   - `20` raw meat.
9. Complete the bill and confirm it produces `1` Prim'ta larva.
10. Force the queen-arrival incident again while the queen is alive and confirm
    no second queen appears.
