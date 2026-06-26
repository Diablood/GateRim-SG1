# Current project state

Current milestone: `0.3.47-dev - Add Free Jaffa faction-leader names` — final local revision `r5` validated and published.

## Repository state

- Starting tag: `v0.3.46-dev`.
- Published branch: `feature/free-jaffa-faction-leader-names`.
- Final tag: `v0.3.47-dev`.
- Published versions: `0.3.47-dev` and `0.3.47.0`.
- Final local revision: `r5`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

Newly generated Free Jaffa faction leaders now receive a cultural name during native pawn generation, before the world-creation interface displays them.

The published implementation:

- keeps `SG1_FreeJaffaGuard` as the fixed leader PawnKind;
- assigns `SG1_NamerPawnFreeJaffa` through the supported `nameMaker` and `nameMakerFemale` fields;
- provides `576` explicit Free Jaffa personal names built from the established cultural syllables;
- combines them with `24` language-neutral clan bynames for `13,824` formal combinations;
- repeats the personal name as the explicit nickname so the short label remains cultural while diplomatic interfaces show the full two-part name;
- lets RimWorld validate distinct non-empty first and last fields instead of treating every one-token result as confusingly similar;
- allows the later cultural manager to preserve the native PawnKind-generated name rather than assigning a second identity.

The failed post-generation faction-owner fallback and dedicated leader-processing registry from local revisions `r1` and `r2` are not part of the published implementation. The invalid `chanceToUseNameMaker` field from the first `r3` startup and the one-token grammar that failed under `r4` are also absent. No new save field remains.

## Deliberate limits

This milestone does not:

- rename leaders already serialized in existing saves;
- create a dedicated leader PawnKind or change leader titles, backstories, equipment or faction behavior;
- change faction or settlement names;
- change Goa'uld System Lord names, host identity or symbiote identity;
- alter Tok'ra world-faction selection or hidden-presence behavior;
- add custom faction icons or other visual assets;
- add factions, incidents, missions or diplomacy rules.

Because `SG1_FreeJaffaGuard` is also used outside leadership, ordinary guards generated through that PawnKind receive the same valid formal Free Jaffa naming grammar immediately.

## Final validation

- RimWorld loaded without the invalid PawnKind field error found under `r3`;
- world generation completed without the `Could not get new name` failure found under `r4`;
- several Free Jaffa factions displayed varied two-part cultural leader names before the player selected a starting tile;
- no vanilla human leader name remained in the validated sample;
- faction and settlement names from `0.3.45-dev` remained unchanged;
- the final `r5` correction was XML and documentation only, so the already rebuilt `0.3.47.0` assembly remained valid;
- replacement-leader generation and an explicit save/reload cycle remain durable regression checks rather than separately reported focused tests for this validation.

## Planned follow-up branches

The following work remains explicitly planned outside `0.3.47-dev`:

- `feature/goauld-system-lord-leader-names`: audit and correct the visible Goa'uld leader identity while preserving the distinct host and symbiote names;
- `feature/tokra-world-faction-selection-audit`: audit the hidden required Tok'ra faction, world-creation visibility and the effect of custom faction removal on the Tok'ra questline;
- `feature/faction-world-icon-overhaul`: replace the shared vanilla house silhouette with faction-specific world icons during the global visual pass.

No version number is assigned to these branches yet. Each must start from the latest published tag available when selected.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.47-dev` on a new dedicated branch.
