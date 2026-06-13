# GateRim SG-1 — Current project state

## Latest published milestone

`v0.2.8-dev - Add Stargate crafting-research baseline`

The main GitHub repository and the separate wiki repository are synchronized.

## Current known follow-up

The `Équipe SG isolée` starting scenario can still propose underage candidates.

Observed examples:
- age 13;
- age 15;
- no adult backstory for those candidates.

Expected correction:
- require operational adult candidates, ideally age 18+;
- preserve the ability to regenerate the four candidates;
- keep the correction as small and testable as possible.

## Workflow

1. Create a dedicated branch from `v0.2.8-dev`.
2. Inspect the scenario generation configuration.
3. Prepare the smallest correction.
4. Rebuild with `-t:Rebuild` if C# changes are required.
5. Update documentation and wiki drafts when relevant.
6. Report test cases before publication.