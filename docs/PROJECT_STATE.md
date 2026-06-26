# Current project state

Current milestone: `0.3.48-dev - Add Goa'uld System Lord leader names` — final local revision `r1` validated and published.

## Repository state

- Starting tag: `v0.3.47-dev`.
- Published branch: `feature/goauld-system-lord-leader-names`.
- Final tag: `v0.3.48-dev`.
- Published versions: `0.3.48-dev` and `0.3.48.0`.
- Final local revision: `r1`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

Newly generated Goa'uld System Lord leaders now receive a cultural formal name during native PawnKind generation, before the world-creation interface displays them.

The published implementation:

- assigns `SG1_NamerPawnGoauldSystemLord` to `SG1_GoauldSystemLordHost` through `nameMaker` and `nameMakerFemale`;
- provides `575` original Goa'uld personal names and `24` language-neutral throne-house bynames for `13,800` formal combinations;
- repeats the personal name as the explicit nickname so short labels retain the personal Goa'uld name while diplomatic interfaces display the full two-part identity;
- keeps the generated world-domain and settlement names independent from the generated leader name;
- reconciles the native visible leader name with the persistent `GoauldSymbioteData.symbioteName` once the game starts;
- generates and stores a distinct off-world human host name before the host Hediff attaches, preventing the visible symbiote identity from overwriting the hidden host identity;
- restores that stored host name when a supported release or extraction removes a Goa'uld symbiote from a pawn currently displaying the symbiote name;
- adds no new save field and does not rename leaders already serialized in existing saves.

## Deliberate limits

This milestone does not:

- introduce named canon System Lords;
- force a leader name to match the independently generated domain or settlement names;
- change leader titles, backstories, equipment, stats, faction behavior or diplomacy;
- replace the temporary vanilla faction and settlement icons;
- modify the ordinary Goa'uld host-caste naming path;
- alter Tok'ra world-faction selection or hidden-presence behavior;
- add a player-facing dual-identity panel for AI-controlled System Lords.

## Final validation

- RimWorld reached world generation without a new XML, RulePackDef or PawnKindDef failure related to the leader name maker;
- a newly generated world displayed cultural two-part Goa'uld names for the inspected System Lord leaders before the player selected a starting tile;
- the validated sample no longer exposed vanilla human leader names;
- world-domain and settlement names remained independent from the leader identities;
- final local revision `r1` is the published implementation;
- explicit save/reload, persistent `symbioteName` / `hostName` inspection and supported extraction remain durable regression checks rather than separately reported focused tests for this validation.

## Planned follow-up branches

The following work remains explicitly planned outside `0.3.48-dev`:

- `feature/tokra-world-faction-selection-audit`: audit the hidden required Tok'ra faction, world-creation visibility and the effect of custom faction removal on the Tok'ra questline;
- `feature/faction-world-icon-overhaul`: replace the shared vanilla house silhouette with faction-specific world icons during the global visual pass.

No version number is assigned to these branches yet. Each must start from the latest published tag available when selected.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.48-dev` on a new dedicated branch.
