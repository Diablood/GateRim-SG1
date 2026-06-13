# GateRim SG-1 — Current project state

## Latest published milestone

`v0.2.8-dev - Add Stargate crafting-research baseline`

## Current known follow-up

Adjust the `Équipe SG isolée` starting scenario:
- prevent starting candidates aged 13 or 15;
- require operational adult candidates, ideally 18+;
- preserve the ability to regenerate the four candidates.

## Next workflow

1. Create a dedicated branch from the latest validated published state.
2. Inspect the scenario generation Defs and C# hooks.
3. Prepare the smallest testable correction.
4. Rebuild with `-t:Rebuild` when C# changes are involved.
5. Update README, technical documentation, and wiki drafts when relevant.