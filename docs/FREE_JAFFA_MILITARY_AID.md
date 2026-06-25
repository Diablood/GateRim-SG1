# Free Jaffa military aid

Version: `0.3.44-dev`

## Purpose

This milestone allows an allied Free Jaffa faction to answer RimWorld's ordinary military-aid request. It extends the visible faction through an existing diplomatic service instead of introducing a GateRim-specific mission or support currency.

## Activation

`SG1_FreeJaffa` now uses:

```text
canRequestMilitaryAid: true
```

The request is made by a colon through a powered communications console. RimWorld remains responsible for:

- checking the allied relation;
- presenting the request in faction dialogue;
- charging goodwill;
- enforcing the request cooldown;
- choosing the force size and arrival mode;
- controlling combat, losses and departure.

No Tok'ra trust, hidden GateRim timer or custom incident state is involved.

## Reinforcement composition

The request reuses the faction's existing `Combat` pawn-group profile:

```text
SG1_FreeJaffaWarrior: weight 4
SG1_FreeJaffaGuard:   weight 1
```

These PawnKinds already provide:

- Jaffa xenotype;
- automatic Prim'ta initialization;
- Free Jaffa names and cultural backstories;
- Ma'Tok weapons;
- light or heavy modular Jaffa armor;
- no automatic Goa'uld forehead mark.

The milestone adds no dedicated aid pawn, officer, transport or animal.

## Boundaries retained

The faction still uses:

```text
canGenerateQuestSites: false
raidsForbidden: true
canSiege: false
canStageAttacks: false
```

Military aid therefore does not enable Free Jaffa quest sites, natural raids, sieges or staged attacks. Trade and peaceful visitors remain unchanged.

## Save compatibility

No persistent identifier, component or serialized field is added. Existing saves receive the updated faction permission when the Def is loaded. Any active allied force is persisted by RimWorld's ordinary pawn, faction and map systems.

## Final validation

Final local revision `r1` validated:

- neutral, hostile and allied relation states;
- powered communications-console access;
- goodwill cost and repeated-request restriction;
- coherent Free Jaffa combat-group generation;
- arrival, combat and departure under vanilla control;
- save/reload during the intervention;
- continued absence of Free Jaffa quests, natural raids, sieges and staged attacks;
- regressions of the existing trade network and peaceful visitors;
- a clean `Player.log`.

The feature was published under `v0.3.44-dev` without adding persistent save data or a GateRim-specific diplomatic subsystem.
