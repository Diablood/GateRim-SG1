# Project state

Current milestone: `0.3.7-dev - Rewrite mod presentation`.

## Active development base

- Functional base tag: `v0.3.6-dev`.
- Dedicated branch: `feature/about-description-rewrite`.
- Planned final tag: `v0.3.7-dev`.
- Current local archive revision: `0.3.7-dev-r1`.

## Milestone goal

Replace the oversized development inventory in `About/About.xml` with a concise and immersive presentation of the current player experience.

The description must:

- introduce the stranded SG-team premise;
- present the Goa'uld, Jaffa, Free Jaffa and Tok'ra without listing every implementation detail;
- summarize the principal playable systems: alien factions, weapons, symbiote biology, Tok'ra contacts and recurring operations;
- state clearly that the functional Stargate and full off-world progression are not yet included;
- keep the Biotech dependency explicit;
- remain suitable for the RimWorld mod manager and future Workshop reuse.

## Files changed

- `About/About.xml`: version `0.3.7-dev` and rewritten description;
- `Source/GateRimSG1/GateRimSG1.csproj`: assembly metadata `0.3.7.0`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`.

No `docs/wiki/*.md` file is changed, so the separate wiki does not require synchronization for this milestone.

## Gameplay and save compatibility

- No Def, C# gameplay behavior, balance value, save data or player-facing interaction is changed.
- `0.3.0-dev` remains the save compatibility baseline.
- No migration or new-game requirement is introduced.

## Validation scope

- parse `About/About.xml` successfully;
- confirm the mod manager displays the new version and formatted description;
- rebuild `GateRimSG1.dll` as `0.3.7.0`;
- launch the game and verify a clean `Player.log`;
- confirm no old exhaustive feature inventory remains in the metadata.

## Publication identifiers

- commit: `0.3.7-dev - rewrite mod presentation`;
- branch: `feature/about-description-rewrite`;
- annotated tag: `v0.3.7-dev`.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Publish only the final tag without an `-rN` suffix.
