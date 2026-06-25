# Current project state

Current milestone: `0.3.45-dev - Add Free Jaffa world-name generators` — final local revision `r3` validated and published.

## Repository state

- Starting tag: `v0.3.44-dev`.
- Published branch: `feature/free-jaffa-world-names`.
- Final tag: `v0.3.45-dev`.
- Published versions: `0.3.45-dev` and `0.3.45.0`.
- Final local revision: `r3`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

The visible Free Jaffa faction now uses dedicated bilingual RulePackDefs for both faction and settlement world names instead of vanilla outlander name makers or a shared fixed faction name.

The published generators provide:

- `SG1_NamerFactionFreeJaffa`, combining `12` collective forms and `18` resistance themes for `216` possible faction names;
- `SG1_NamerSettlementFreeJaffa`, combining settlement types, cultural themes and occasional ordinal forms for `600` possible settlement names;
- a generic `Free Jaffa` / `Jaffa libres` label in world-creation controls while each generated faction instance receives its own name;
- French casing that keeps ordinary internal words lower-case while preserving justified titles such as `Maîtres`, `Jaffa` and `Porte`;
- natural ordinal forms such as `Premier refuge...` and `Deuxième cité...` instead of relying on visible technical suffixes.

## Deliberate limits

This milestone does not rename existing saves and does not add:

- custom settlement layouts, icons or textures; the current green vanilla house variants remain temporary and faction-specific silhouettes are deferred to the global visual pass;
- persistent clan identities or a uniqueness registry saved into the world;
- new races, factions, missions, incidents, PawnKinds or equipment;
- changes to trade, military aid, diplomacy or world-faction counts;
- procedural fictional-language syllables.

## Final validation

- `check-project-consistency.cmd` passed and the rebuilt assembly reported version `0.3.45.0`;
- multiple Free Jaffa factions generated distinct collective names instead of sharing the generic faction label;
- Free Jaffa settlements generated varied dedicated names without outlander naming leakage;
- French names used natural capitalization at the beginning of names and after ordinal forms;
- the enlarged settlement grammar avoided systematic visible suffixes such as `2` or `3` during the validation sample;
- English and French indexed RulePackDef content loaded without missing-reference or grammar errors;
- existing serialized faction and settlement names remained outside the migration scope;
- settlement trade, clan-supply convoys, peaceful visitors and allied military aid remained unchanged;
- `Player.log` contained no new GateRim SG-1 error.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.45-dev` on a new dedicated branch.
