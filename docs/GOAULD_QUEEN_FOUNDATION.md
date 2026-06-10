# Goa'uld queen biological foundation

## Status

Implemented as a developer-spawnable prototype in `0.1.57-dev`.

## Scope

This milestone introduces `SG1_GoauldQueen` as a distinct animal-style pawn and
`PawnKindDef`.

The queen is intentionally excluded from biome tables, storyteller incidents and
normal acquisition routes. Spawn it through developer mode for isolated tests.

## Intentional limitations

The prototype does not yet:

- produce Prim'ta larvae;
- create adult symbiotes;
- interact with the incubation basin;
- require a specialized chamber;
- join Goa'uld factions;
- use a final dedicated texture.

The existing free-symbiote texture is reused provisionally at a larger draw size.

## Next biological step

The next iteration should turn the current Prim'ta incubation basin into assisted
maturation infrastructure. Larvae should no longer originate fully ex nihilo from
raw meat alone once queen-origin sourcing is connected.
