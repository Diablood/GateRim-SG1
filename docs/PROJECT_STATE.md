# Current project state

Current milestone: `0.3.46-dev - Add Goa'uld world-name generators` — final local revision `r1` validated and published.

## Repository state

- Starting tag: `v0.3.45-dev`.
- Published branch: `feature/goauld-world-names`.
- Final tag: `v0.3.46-dev`.
- Published versions: `0.3.46-dev` and `0.3.46.0`.
- Final local revision: `r1`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

The visible Goa'uld System Lord faction now uses dedicated bilingual RulePackDefs for faction and settlement world names instead of a shared fixed instance name or vanilla pirate name makers.

The published generators provide:

- `SG1_NamerFactionGoauldDomain`, combining `12` forms of power and `24` themes for `288` possible domain names;
- `SG1_NamerSettlementGoauldDomain`, combining settlement types, imperial themes and occasional ordinal forms for `1,728` possible settlement names;
- a generic `Goa'uld System Lord domains` / `Domaines des Grands Maîtres Goa'uld` label in world-creation controls while each generated faction instance receives its own domain name;
- natural French casing and masculine/feminine ordinal agreement;
- names that describe the domain without pretending to identify the separately generated leader.

## Deliberate limits

This milestone does not rename existing saves and does not add:

- named canon System Lords or a leader-to-faction naming link;
- culture-specific faction-leader name generation;
- custom settlement layouts, icons or textures;
- new factions, races, missions, incidents, PawnKinds or equipment;
- changes to domain identity, Jaffa marks, raids, diplomacy or world-faction counts;
- persistent uniqueness registries or new save data.

## Final validation

- `check-project-consistency.cmd` passed and the rebuilt assembly reported version `0.3.46.0`;
- multiple Goa'uld factions generated distinct domain names instead of sharing the generic faction label;
- Goa'uld settlements generated varied dedicated names without pirate-name leakage;
- French names used natural capitalization and correct ordinal agreement;
- English and French indexed RulePackDef content loaded without missing-reference or grammar errors;
- existing serialized faction and settlement names remained outside the migration scope;
- leaders, domain identity, settlements, permanent hostility, raids and free-symbiote incursion remained unchanged;
- `Player.log` contained no new GateRim SG-1 error.

## Planned follow-up branches

The following work is explicitly planned but remains outside `0.3.46-dev`:

- `feature/free-jaffa-faction-leader-names` for culture-specific Free Jaffa faction-leader names;
- `feature/goauld-system-lord-leader-names` for Goa'uld leader naming, with an explicit audit of host and symbiote identity rather than a simple pawn-name replacement;
- `feature/faction-world-icon-overhaul` for distinct world-map icons, beginning with Free Jaffa and Goa'uld settlements and then covering every visible GateRim SG-1 faction.

No version number is assigned to these branches yet. Each must start from the latest published tag available when selected.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.46-dev` on a new dedicated branch.
