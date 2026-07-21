# Project state

Current milestone: `0.3.104-dev - Finalize adult symbiote mobile visuals`

Status: gameplay and directional visual presentation validated in local revision
`r2`; the final repository state is complete for fast-forward integration into
`develop` and the unique annotated tag `v0.3.104-dev`.

- Starting point: published `develop` aligned with `v0.3.103-dev`.
- Working branch: `feature/final-adult-symbiote-mobile-visuals`.
- Final local revision: `r2`.
- Assembly version: `0.3.104.0`.
- Final annotated tag: `v0.3.104-dev`.

## Implemented scope

- Add one shared mobile visual archetype for adult Goa'uld and Tok'ra
  symbiotes.
- Move both PawnKinds to
  `Things/Pawn/Animal/SG1_AdultSymbiote/SG1_AdultSymbiote`.
- Replace `Graphic_Single` with `Graphic_Multi`.
- Add dedicated north, south and east textures; west uses the mirrored east
  texture.
- Use `drawSize = 0.65` for both adult forms.
- Preserve the former single-image family exclusively as the temporary Goa'uld
  queen placeholder.
- Add protected wiki copies, player documentation and visual-checker coverage.

## Validated results

- Goa'uld and Tok'ra render correctly in north, south, east and mirrored west
  facings.
- The maintainer-adjusted north and south files match the perceived thickness
  and scale of the east and west profile.
- The thick black outline remains visible at gameplay scale.
- Movement, selection, inspection, capture and save/reload remain correct.
- Goa'uld hunting and forced implantation remain functional.
- Tok'ra voluntary implantation and persistent identity remain functional.
- `drawSize = 0.65` is accepted.
- The Goa'uld queen retains its previous appearance and behavior.
- No relevant missing-texture, XML or `Graphic_Multi` error appears.

## Preserved contracts

- Goa'uld and Tok'ra keep separate ThingDefs, PawnKindDefs and save identifiers.
- No combat, movement, capture, extraction, incident or identity balance changes.
- The three final PNGs remain transparent `128×128` assets.
- The queen remains outside this milestone.

## Deferred work

- Finalize the Goa'uld queen as a separate directional visual family.
- Produce and validate directional worn variants for visible Jaffa armor and
  under-armor families.
- Integrate the separate under-armor, trousers and belt into complete Jaffa
  world-generation and mission outfits.
- Refactor deployed and retracted helmet states without breaking existing saves.

## Publication state

The final commit is:

```text
0.3.104-dev - Finalize adult symbiote mobile visuals
```

The final annotated tag is `v0.3.104-dev`. The player wiki source changes and
three protected images require synchronization with the separate wiki repository.
