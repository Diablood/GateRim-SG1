# Goa'uld queen biological foundation

## Status

Implemented as a developer-spawnable prototype in `0.1.57-dev`.

Natural acquisition update: `0.2.10-dev`.

## Scope

This milestone introduces `SG1_GoauldQueen` as a distinct animal-style pawn and
`PawnKindDef`.

The queen remains excluded from biome tables. Since `0.2.10-dev`, the rare
`SG1_GoauldQueenArrival` storyteller incident can provide one escaped queen
under player control when no living player queen already exists.

## Intentional limitations

The prototype does not yet:

- produce mature Prim'ta larvae directly;
- create adult symbiotes;
- interact with the incubation basin;
- require a specialized chamber;
- participate directly in Goa'uld settlements or raids;
- use a final dedicated texture.

The existing free-symbiote texture is reused provisionally at a larger draw size.

## Current production loop

A player-controlled queen provides one immature symbiote per extraction with a
persistent one-day cooldown. The existing incubation basin then matures that
resource with `10` raw meat after `SG1_GoauldBiotechnology`.

Specialized queen infrastructure and faction-linked acquisition remain later
milestones.
