# Project state

Current milestone: `0.3.105-dev - Finalize Goa'uld queen mobile visuals`

Status: gameplay and directional visual presentation validated in local revision
`r2`; the final repository state is complete for fast-forward integration into
`develop` and the unique annotated tag `v0.3.105-dev`.

- Starting point: published `develop` aligned with `v0.3.104-dev`.
- Working branch: `feature/final-goauld-queen-mobile-visuals`.
- Final local revision: `r2`.
- Assembly version: `0.3.105.0`.
- Final annotated tag: `v0.3.105-dev`.

## Implemented scope

- Add one dedicated directional mobile family for the Goa'uld queen.
- Move the queen PawnKind to
  `Things/Pawn/Animal/SG1_GoauldQueen/SG1_GoauldQueen`.
- Replace the former enlarged `Graphic_Single` placeholder with
  `Graphic_Multi`.
- Add dedicated north, south and east textures; west uses the mirrored east
  texture.
- Use `drawSize = 0.95`.
- Remove the obsolete single-image placeholder formerly shared by the queen.
- Add protected wiki copies, player documentation and visual-checker coverage.

## Validated results

- The queen renders correctly in north, south, east and mirrored west facings.
- The maintainer-adjusted north and south files match the perceived thickness
  and scale of the east and west profile.
- The reproductive abdomen, neck, head appendages and dorsal ridge remain
  readable at gameplay scale.
- Movement, selection, inspection, capture and save/reload remain correct.
- Manual immature-symbiote production and its persistent cooldown remain
  functional.
- `drawSize = 0.95` is accepted.
- No relevant missing-texture, XML or `Graphic_Multi` error appears.

## Preserved contracts

- `SG1_GoauldQueen`, its ThingDef, PawnKindDef and save identifiers are
  unchanged.
- `combatPower = 30` and all biological, incident and production balance values
  are unchanged.
- The rare escaped-queen incident still prevents a second living
  player-controlled queen.
- One extraction still provides one immature Prim'ta symbiote followed by a
  persistent `180000`-tick, three-day recovery period.
- The adult Goa'uld/Tok'ra visual family remains unchanged.
- The three final queen PNGs remain transparent `128×128` assets.

## Deferred work

- Produce and validate directional worn variants for visible Jaffa armor and
  under-armor families.
- Integrate the separate under-armor, trousers and belt into complete Jaffa
  world-generation and mission outfits.
- Refactor deployed and retracted helmet states without breaking existing saves.

## Publication state

The final commit is:

```text
0.3.105-dev - Finalize Goa'uld queen mobile visuals
```

The final annotated tag is `v0.3.105-dev`. The player wiki source changes and
three protected images require synchronization with the separate wiki repository.
