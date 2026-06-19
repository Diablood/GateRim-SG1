# Project state

Current milestone: `0.3.6-dev - Consolidate Goa'uld Jaffa PawnKind variants`.

## Active development base

- Functional base tag: `v0.3.5-dev`.
- Dedicated branch: `feature/goauld-jaffa-pawnkind-consolidation`.
- Planned final tag after local validation: `v0.3.6-dev`.
- Current local documentation archive revision: `0.3.6-dev-r2`.

## Audit result

The four apparently duplicated Goa'uld-aligned Jaffa PawnKinds are contextually distinct and must remain available:

- `SG1_GoauldJaffaWarrior` is the standard warrior used by the faction baseline and Combat groups;
- `SG1_GoauldJaffaGuard` is the standard guard used by Combat groups with `combatPower` `145`;
- `SG1_GoauldSettlementJaffaWarrior` is restricted to Settlement groups and keeps `maxPerGroup` `7`;
- `SG1_GoauldSettlementJaffaGuard` is restricted to Settlement groups, keeps `maxPerGroup` `2`, and intentionally uses `combatPower` `130`.

The Settlement variants therefore are not accidental duplicates. Their dedicated `defName` values preserve validated settlement composition without changing direct raids.

## Milestone scope

This milestone consolidates only the duplicated XML structure:

- add one abstract warrior profile and one abstract guard profile;
- inherit the four concrete PawnKinds from those shared profiles;
- preserve all four existing concrete `defName` values;
- preserve faction group references, labels, xenotype, backstories, equipment, ages, combat power and settlement caps;
- keep all cultural-name behavior introduced by `0.3.5-dev` unchanged;
- add no new pawn, faction, incident, raid doctrine, settlement behavior or player-facing option.

## Save compatibility

- `0.3.0-dev` remains the framework compatibility baseline.
- No concrete PawnKind is removed or renamed.
- Existing saves and references continue to resolve the same four concrete `defName` values.
- No Scribe migration or C# compatibility layer is required.

## Documentation scope

Updated in this revision:

- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `About/About.xml` and the assembly metadata for version `0.3.6-dev`.

No player wiki page is changed because the gameplay behavior and visible content remain identical.

## Files intentionally removed

None for this milestone revision.

## Local validation result

The complete targeted validation is successful:

- assembly `0.3.6.0` builds and loads correctly;
- both abstract XML parents load without becoming spawnable PawnKinds;
- all four concrete PawnKinds generate correctly while the game is paused;
- cultural names, xenotypes, Prim'ta, marks, weapons and armor remain correct;
- no second naming or equipment replacement occurs after time resumes;
- direct Goa'uld raids still use the standard Combat profiles;
- Goa'uld settlements still use the contextual Settlement profiles and their limits;
- save/reload preserves all four profiles;
- `Player.log` is clean.

## Deferred follow-up

The complete durable backlog remains in `docs/ROADMAP.md`. This maintenance milestone does not alter the future work on names, backstories, races, the GateRim-only world preset or the dedicated storyteller.

A future documentation-focused milestone must also rewrite `About/About.xml`. Its current description is excessively long and exposes too many implementation details. The replacement should be concise, immersive and focused on the player experience and the mod's major features, following the general presentation rhythm of the Zombieland example supplied by the project author without copying its wording.

## Publication after validation

Follow the corrected `docs/MILESTONE_PUBLICATION.md` from the repository.

Expected final publication identifiers:

- commit: `0.3.6-dev - consolidate Goa'uld Jaffa PawnKind variants`;
- branch: `feature/goauld-jaffa-pawnkind-consolidation`;
- annotated tag: `v0.3.6-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
