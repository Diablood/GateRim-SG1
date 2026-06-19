# Project state

Current milestone: `0.3.5-dev - Add culture-specific pawn name generators`.

## Active development base

- Functional base tag: `v0.3.4-dev`.
- Dedicated branch: `feature/cultural-pawn-name-generators`.
- Planned final tag after local validation: `v0.3.5-dev`.
- Current local test archive revision: `0.3.5-dev-r2`.

## Milestone scope

This milestone adds a reusable internal naming layer without adding a new race, faction, incident or gameplay reward.

The first supported cultural groups are:

1. Goa'uld-aligned Jaffa;
2. Free Jaffa;
3. Goa'uld;
4. Tok'ra;
5. Tau'ri / SGC.

The generated pools use original culture-inspired combinations with enough variation for recurring visitors, raids and long-running games.

## Assignment model

`GameComponent_CulturalPawnNameManager` scans newly generated pawns on maps, faction leaders and world pawns.

The manager resolves culture from GateRim SG-1 PawnKinds and faction identity, then assigns one persistent cultural name. It stores:

- processed pawn ThingIDs, preventing repeated renaming;
- reserved generated name keys, reducing duplicate cultural names within the same save;
- an initialization baseline, protecting pawns that already existed before the feature was enabled.

Starting player pawns are protected during new-game initialization so scenario-editor names remain unchanged. Pawns already present in a pre-`0.3.5-dev` save are registered without being renamed.

Newly generated visitors, raiders, settlement pawns, faction leaders and compatible debug-spawned pawns are eligible once they enter the normal map or world-pawn lifecycle.

## Host and symbiote identity

`GoauldSymbioteData` now stores both a persistent `symbioteName` and the captured `hostName`.

For newly generated Goa'uld and Tok'ra host profiles:

- the original human name is captured as the host identity;
- the persistent symbiote receives a culture-specific Goa'uld or Tok'ra identity;
- the visible generated pawn receives the culture-specific identity once;
- later implantation, extraction, recruitment or faction changes do not repeatedly rename the pawn.

This milestone prepares the distinction without exposing both identities in every normal player interface.

## Debug requirements

One grouped debug action is added:

```text
Cultural names: show samples
```

It displays examples for all current cultural groups in one report. The same report is accessible from the GateRim SG-1 settings page while advanced debug information is enabled.

No additional communicator gizmo is added, and no name-debug control is visible in normal play.

## Save compatibility

- `0.3.0-dev` remains the framework compatibility baseline.
- Existing pawns in older saves are never renamed when the component first initializes.
- The new processed-ID and reserved-name collections are saved normally from `0.3.5-dev` onward.
- Existing persistent symbiote data receives missing internal host/symbiote identity fields without changing the pawn's current visible name.

## Durable roadmap additions

`docs/ROADMAP.md` now records:

- future Asgard support, trade, military aid and quest-giver presence without ordinary world settlements;
- future pacifist Nox trade and diplomacy;
- future hostile Unas populations and their potential as Goa'uld hosts;
- an optional GateRim SG-1-only world preset that removes selectable vanilla factions where safe;
- a dedicated GateRim SG-1 storyteller that never becomes mandatory for the mod's events.

## Immediate developer-spawn and starter integration

Compatible pawns now notify the relevant host initializer and the shared name manager from `PostSpawnSetup`, allowing the vanilla developer `Spawn pawn` tool to initialize and name GateRim pawns before the player inspects them, even while paused.

The stranded SG-team scenario also assigns Tau'ri names when each candidate is generated for the configuration page. The player may still rename any candidate manually; the manager registers the final chosen names at game start and never overwrites them later.

Generic raid generation is not a valid test for every culture: Tok'ra and Free Jaffa factions currently declare `raidsForbidden`, and the SGC expedition is a player faction. Their names must be tested through their supported visitors, faction leaders, scenario pawns or direct PawnKind spawning instead of forcing a vanilla raid.

## Files intentionally removed

None for this milestone revision.

## Required local validation

1. Build the assembly and confirm version `0.3.5.0`.
2. Open the grouped cultural-name sample report from RimWorld developer mode.
3. Enable the GateRim SG-1 advanced debug option and open the same report from mod settings.
4. Generate multiple Goa'uld-aligned Jaffa and confirm culturally coherent single names.
5. Generate multiple Free Jaffa and confirm a related but distinct naming style.
6. Generate Goa'uld and Tok'ra host profiles and confirm their visible names and persistent symbiote names.
7. Generate or encounter SGC expedition pawns and confirm Tau'ri-style first and last names.
8. Confirm starting player pawns and pawns already present in an older save are not renamed.
9. Confirm visitors, raids and faction leaders keep the same names after save/load.
10. Confirm later recruitment, implantation, extraction or faction changes do not rename processed pawns.
11. Generate several dozen samples per group and check diversity and immediate duplicates.
12. Review `Player.log` for Name, Scribe, GameComponent, world-pawn or missing-Def errors.

## Deferred follow-up

The complete durable backlog remains in `docs/ROADMAP.md`. Relevant follow-ups include:

- extend the naming framework when Asgard, Nox, Unas and other cultures are implemented;
- decide where both host and symbiote names should be visible in normal play;
- continue enriching cultural backstories and their stat modifiers;
- use the naming foundation in the future GateRim SG-1-only world preset.

## Publication after validation

Follow the corrected `docs/MILESTONE_PUBLICATION.md` from the repository.

Expected final publication identifiers:

- commit: `0.3.5-dev - add culture-specific pawn name generators`;
- branch: `feature/cultural-pawn-name-generators`;
- annotated tag: `v0.3.5-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
