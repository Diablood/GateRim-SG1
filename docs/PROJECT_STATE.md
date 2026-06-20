# Project state

Current milestone: `0.3.8-dev - Rework existing cultural backstories`.

## Active development base

- Functional base tag: `v0.3.7-dev`.
- Dedicated branch: `feature/cultural-backstory-rework`.
- Planned final tag: `v0.3.8-dev`.
- Current local archive revision: `0.3.8-dev-r4`.

## Milestone goal

Improve the existing cultural backstory set without expanding its size.

The milestone must:

- preserve all `52` existing `BackstoryDef` identifiers, slots and spawn categories;
- enrich every English and French description with a clearer cultural and practical identity;
- add modest, coherent `skillGains` to every dedicated childhood and adulthood;
- keep military, civilian, administrative, medical and covert backgrounds distinct;
- avoid passions, forced traits, work incapabilities and direct stat multipliers;
- preserve normal generation filters, existing saves and voluntarily implanted colon histories;
- defer any increase in the number of backstories to a later dedicated discussion;
- record that future Asgard, Nox, Unas and other cultures require names and backstories when introduced or immediately afterward;
- document every current backstory in culture-grouped wiki tables with localized name, description and skill bonuses, and require future entries to update those tables;
- record the `0.3.x` direction toward a shared Def-driven cultural framework reused by names, backstories, starter generation, scenarios and future culture-dependent systems;
- defer culture-aware starter backstory randomization to the next dedicated milestone instead of adding hidden generation behavior to this data-only rework.

## Files changed

- `1.6/Defs/BackstoryDefs/SG1_CulturalBackstories.xml`;
- `Languages/French/DefInjected/BackstoryDef/SG1_CulturalBackstories.xml`;
- `About/About.xml`: version `0.3.8-dev`;
- `Source/GateRimSG1/GateRimSG1.csproj`: assembly metadata `0.3.8.0`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/wiki/Cultural-Backstories.md`;
- `docs/wiki/Home.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`;
- `docs/CHANGELOG.md`.

The separate wiki must be synchronized when the milestone is published.

## Gameplay and save compatibility

- Existing backstory `defName` values, categories, slots and adulthood body types are unchanged.
- No pawn, name, trait, passion, work restriction or saved assignment is rerolled.
- Existing saves retain their assigned histories and require no migration.
- `0.3.0-dev` remains the save-compatibility baseline.

## Framework boundary and next step

`0.3.8-dev` does not change starting-pawn randomization or normal world generation. It prepares stable backstory data for the shared cultural framework.

The next dedicated milestone should introduce configurable cultural profiles consumed by a generic C# resolver. Initial real uses are:

- culture-aware randomization of player starting pawns;
- reuse of the existing cultural name generators;
- scenario-specific Tau'ri / SGC restrictions;
- extensible profiles for Jaffa, Goa'uld hosts, Tok'ra and future Asgard, Nox, Unas or other cultures.

Manual selections made through compatible pawn editors must remain valid, and raids, visitors, settlements, incidents, quests and ordinary world pawn generation must retain their current behavior unless a later milestone explicitly migrates them to the shared framework.

## Validation completed for r4

Static validation:

- both backstory XML files parse successfully;
- exactly `52` `BackstoryDef` entries are present;
- exactly `52` French title, short-title and description sets are present;
- every backstory has a non-empty `skillGains` block;
- all `defName`, slot and spawn-category combinations remain unique;
- no skill bonus is negative or greater than `5`;
- metadata versions are `0.3.8-dev` and `0.3.8.0`;
- the player wiki catalogue contains all `52` backstories, grouped into eight cultural tables with their French names, descriptions and exact skill bonuses;
- the durable documentation records the shared cultural-framework direction and the separate starter-filtering milestone without claiming that either behavior is already implemented;
- `docs/TOKRA_DUAL_IDENTITY_DESIGN.md` preserves the full deferred player-controlled Tok'ra host / symbiote design;
- `docs/TESTING_CURRENT.md` provides a concise current-milestone test status while `docs/TESTING.md` remains the historical regression archive;
- the ZIP contains complete files at repository-relative paths and preserves `About/ModIcon.png` by omission.

Local functional validation:

- build succeeded and the assembly reports version `0.3.8.0`;
- RimWorld loaded without relevant XML or translation errors;
- the SG-team scenario, representative cultural PawnKinds, Goa'uld raids and natural generation were tested;
- descriptions and skill gains were coherent across the tested cultural groups;
- names, histories and skills remained stable after time resumed and after save / full reload;
- voluntary Tok'ra implantation preserved the host name and backstories;
- the missing visible reference to the implanted symbiote identity is documented for a separate future milestone;
- `Player.log` was clean for the tested scope.

The milestone is ready for commit, branch publication, final annotated tag and wiki synchronization.

## Publication identifiers

- commit: `0.3.8-dev - rework cultural backstories`;
- branch: `feature/cultural-backstory-rework`;
- annotated tag: `v0.3.8-dev`.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
