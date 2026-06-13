# Natural Goa'uld queen acquisition

Version: `0.2.10-dev`

## Scope

This milestone connects the existing queen-origin Prim'ta production loop to
normal play through one rare storyteller incident:

```text
SG1_GoauldQueenArrival
    -> one player-controlled Goa'uld queen
    -> one immature Prim'ta symbiote per extraction
    -> one-day persistent cooldown
```

The incident becomes eligible after day `30`, has a base chance of `0.02` and
a minimum refire delay of `60` days. It refuses to fire while a living
player-controlled queen exists on a map or in a caravan.

## Preserved maturation loop

The incident does not bypass production research. One immature symbiote still
requires `10` raw meat at the Prim'ta incubation basin, and the maturation bill
remains gated by `SG1_GoauldBiotechnology`.

## Manual test checklist

1. Start with only Core, Biotech and GateRim SG-1 active.
2. Force `SG1_GoauldQueenArrival` through developer incident tools.
3. Confirm one player-controlled queen enters from a reachable map edge.
4. Disable developer mode and confirm `Extract immature symbiote` remains
   visible on the queen.
5. Extract one resource and confirm a second extraction is disabled for one
   RimWorld day.
6. Save and reload during the cooldown and confirm the remaining delay persists.
7. Force the incident again and confirm no second queen appears.
8. Put the queen in a caravan and confirm duplicate prevention still applies.
9. After `SG1_GoauldBiotechnology`, mature the resource with `10` raw meat.
10. Check `Player.log` for XML, incident and pawn-generation errors.

## Deferred

- specialized reproductive chambers;
- queen integration into Goa'uld settlements, raids or quests;
- automatic production without player action;
- final queen artwork.
