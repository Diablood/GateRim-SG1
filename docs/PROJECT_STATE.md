# Project state

Current milestone: `0.3.9-dev - Add configurable starter cultural profiles`.

## Active development base

- Functional base tag: `v0.3.8-dev`.
- Dedicated branch: `feature/cultural-starter-profiles`.
- Planned final tag: `v0.3.9-dev`.
- Final local archive revision: `0.3.9-dev-r4`.

## Milestone goal

Introduce the first reusable Def-driven cultural profiles and consume them from two real systems: cultural names and starting-pawn backstory randomization.

The milestone:

- centralizes current cultural identification in `CulturalPawnProfileDef` XML profiles;
- resolves profiles generically by priority from race, xenotype, `PawnKindDef`, faction and generation-context criteria;
- keeps the existing cultural name pools while removing the hard-coded culture switch from the world-pawn name manager;
- adds the hidden `ScenPart_CulturalStarterProfiles` to Def-based scenarios without adding Harmony as a dependency;
- affects only newly generated `PlayerStarter` pawns;
- gives Jaffa starters a Jaffa childhood and an adulthood drawn from either Goa'uld-aligned or Free Jaffa careers;
- gives Goa'uld-host starters an off-world-human childhood and an adulthood drawn from either Goa'uld-host or Tok'ra careers;
- allows ordinary human starters in normal scenarios to keep vanilla backstories while adding the six current Tau'ri / SGC adult careers to the same effective weighted pool as compatible vanilla adult backstories;
- restricts adults in the `Équipe SG isolée` scenario to the six current Tau'ri / SGC careers while preserving ordinary childhood generation because no dedicated Tau'ri childhood set exists yet;
- chooses a cultural starter name from the adulthood actually selected for mixed Jaffa and host profiles;
- adjusts starter skills once by the exact difference between the old and new backstory bonuses;
- leaves manual edits made after generation untouched;
- leaves raids, visitors, settlements, incidents, quests, developer-spawned pawns and ordinary world generation unchanged;
- retains `docs/TESTING_CURRENT.md` as the concise active test record and `docs/TESTING.md` as the historical archive.

## Framework implementation

New framework files:

- `Source/GateRimSG1/Culture/CulturalPawnProfileDef.cs`;
- `Source/GateRimSG1/Culture/CulturalProfileResolver.cs`;
- `Source/GateRimSG1/Culture/ScenPart_CulturalStarterProfiles.cs`;
- `1.6/Defs/CulturalProfileDefs/SG1_CulturalProfiles.xml`;
- `1.6/Defs/ScenPartDefs/SG1_CulturalStarterProfiles.xml`;
- `1.6/Patches/SG1_CulturalStarterProfiles.xml`;
- `docs/CULTURAL_FRAMEWORK.md`.

Migrated consumers:

- `GameComponent_CulturalPawnNameManager` now asks the shared resolver for a name group instead of carrying its own culture-specific condition tree;
- `ScenPart_SGTeamStartingGear` remains responsible only for equipment; Tau'ri starter naming moves to the shared cultural starter part.

Initial XML profiles:

- starter Jaffa;
- starter Goa'uld host;
- Goa'uld-aligned Jaffa;
- Free Jaffa;
- Goa'uld;
- Tok'ra;
- ordinary human / optional Tau'ri career;
- Tau'ri / SGC.

## Compatibility boundary

- No save migration is required.
- Existing processed-name state is preserved.
- No existing pawn is renamed or assigned a new backstory.
- Starter filtering runs only during `PawnGenerationContext.PlayerStarter` generation callbacks.
- Compatible pawn editors can still replace names and backstories manually after generation because the framework does not validate or reject the final selection.
- Raids, visitors, settlements, incidents, quests, developer `Spawn pawn` generation and ordinary world pawn generation retain their previous behavior.
- Custom local or external scenarios that do not originate from a patched `ScenarioDef` may not contain the hidden scenario part; this remains an explicit compatibility boundary for later testing.
- `0.3.0-dev` remains the save-compatibility baseline.

## Files changed

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- the new culture framework source and Def files listed above;
- `Source/GateRimSG1/Names/GameComponent_CulturalPawnNameManager.cs`;
- `Source/GateRimSG1/Scenarios/ScenPart_SGTeamStartingGear.cs`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/wiki/Cultural-Backstories.md`;
- `docs/wiki/Home.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/CHANGELOG.md`.

The separate wiki must be synchronized when the milestone is published.

## Validation completed

Static validation:

- all new XML files parse successfully;
- all referenced xenotypes, pawn kinds, factions, scenario parts and backstories resolve;
- eight cultural profiles load without unintended priority ties in the tested cases;
- metadata versions are `0.3.9-dev` and `0.3.9.0`;
- no Harmony reference or dependency was added;
- the archive contains complete files at repository-relative paths and omits `About/ModIcon.png`.

Local functional validation:

- the project rebuild succeeded and `GateRimSG1.dll` reports version `0.3.9.0`;
- the stranded SG-team scenario consistently assigns one of the six configured SGC adult careers and preserves compatible ordinary childhoods;
- Jaffa starters use only configured Jaffa childhoods and Goa'uld-aligned or Free Jaffa adult careers;
- Goa'uld-host starters use only configured off-world-human childhoods and Goa'uld-host or Tok'ra adult careers;
- mixed profiles select names matching the final adulthood and apply only the expected backstory skill differences;
- ordinary human starters keep vanilla childhoods and a clear majority of vanilla adult careers, while Tau'ri / SGC careers appear occasionally through the compatible vanilla-weighted pool rather than a fixed percentage;
- exclusive profiles remain higher priority than the ordinary-human additive profile;
- manual post-generation name and backstory changes remain untouched in the tested flow;
- developer-spawned and naturally generated world pawns retain the `0.3.8-dev` behavior;
- saving, fully quitting and reloading does not reapply naming, backstory selection or skill adjustments;
- `Player.log` is clean for the tested scope.

The milestone is ready for commit, branch publication, final annotated tag and wiki synchronization.

## Publication identifiers

- commit: `0.3.9-dev - add configurable starter cultural profiles`;
- branch: `feature/cultural-starter-profiles`;
- annotated tag: `v0.3.9-dev`.

## Next step

After publication, select the next real consumer of the shared cultural framework before extending its schema. The player-controlled Tok'ra dual-identity design remains a separate future milestone in `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
