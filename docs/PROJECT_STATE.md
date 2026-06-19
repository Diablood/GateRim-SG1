# Project state

Current milestone: `0.3.1-dev - Rework organic Tok'ra intelligence operation`.

## Active development base

- Functional base tag: `v0.3.0-dev`.
- Dedicated branch: `feature/tokra-intelligence-operation-rework`.
- Planned final tag after local validation: `v0.3.1-dev`.
- Current local test archive revision: `0.3.1-dev-r1`.

## Milestone scope

This milestone reworks the existing recurring intelligence-module operation without adding a fifth archetype.

The validated `0.3.0-dev` framework remains the lifecycle and persistence base. The intelligence worker now adds a richer operation-specific flow:

1. the Tok'ra cell offers encrypted intelligence through the communicator;
2. acceptance delivers one operation-only module through the delivery-zone/communicator/map-edge placement order;
3. an Intellectual-capable colon uses the powered Tok'ra communicator, never a research bench or the module directly;
4. the player chooses cautious analysis or accelerated decoding;
5. work can be interrupted and resumed without losing progress;
6. success removes the module, grants XP, improves trust and uses a contextual RP result variant;
7. accelerated decoding can leak interference and queue a delayed small Goa'uld signal patrol.

## Player-facing rules

- The intelligence module is generated only by the operation.
- The selected colon physically retrieves the module and carries it to the powered Tok'ra communicator before analysis starts.
- Normal communicator inspection is compact: only the channel state, the active organic operation and completed unique mission state are shown; the full catalog remains debug-only.
- It is not constructible and does not appear in any Architect category.
- Cautious analysis takes longer and grants `350` Intellectual XP.
- Accelerated decoding is shorter and grants `500` Intellectual XP, but can attract a nearby Goa'uld patrol.
- The patrol consequence does not cancel an already completed intelligence success.
- Ignoring the offer remains consequence-free.
- Losing the accepted module or missing the deadline remains an accepted failure.
- Success letters use several context-compatible variants and avoid immediate repetition when possible.

## Communicator information boundary

Normal play shows only:

- the currently offered, accepted or active organic operation;
- durable completed progress from unique mission chains when it remains relevant to the player.

Normal play must not reveal:

- the full operation catalogue;
- future archetypes or hidden scheduling;
- selection weights or anti-repetition internals;
- exact persisted work ticks;
- the interference roll or queued incident internals;
- technical history and diagnostics.

The developer report may expose all of these technical states.

## Save compatibility

- `0.3.0-dev` is the compatibility baseline and remains supported.
- New intelligence fields load with safe defaults.
- An accepted `0.3.0-dev` intelligence module can continue by selecting a method at the communicator.
- The former direct-module JobDef and a compatibility JobDriver are retained only to load a save made while that old job was active; the job stops cleanly and directs the player to the communicator.
- The two obsolete direct-interaction ThingComp classes are removed because the module Def no longer instantiates them.

## Debug requirements

The existing single communicator gizmo remains the main debug surface. Its menu and the RimWorld developer actions must allow:

- forcing the intelligence offer;
- accepting and delivering the module;
- choosing cautious analysis;
- choosing accelerated decoding;
- forcing detectable interference and the delayed patrol;
- advancing/finishing, failing or expiring the active operation;
- inspecting the complete persisted intelligence state;
- resetting the framework.

No intelligence debug control may be visible in normal play.

## Files intentionally removed

- `Source/GateRimSG1/Goauld/CompProperties_TokraOrganicDeadDrop.cs`;
- `Source/GateRimSG1/Goauld/Comp_TokraOrganicDeadDrop.cs`.

They provided the obsolete direct right-click interaction on the module. The physical module class, Def and compatibility JobDriver remain.

## Required local validation

1. Delete the two obsolete ThingComp files before extracting the milestone ZIP.
2. Build the assembly and confirm version `0.3.1.0`.
3. Confirm the intelligence module is absent from every Architect category.
4. Validate delivery and both analysis methods through the powered communicator only.
5. Interrupt and resume both methods, including across save/reload.
6. Validate cautious XP, accelerated XP and module cleanup.
7. Force interference and verify a small delayed Goa'uld patrol is queued and later arrives.
8. Confirm the intelligence success remains acquired when the patrol arrives.
9. Repeat successes to review contextual variants and immediate-repeat prevention.
10. Confirm the normal status report exposes only the current operation while the debug report shows full technical state.
11. Validate the compatibility redirect with the legacy JobDef through debug/save inspection if practical.
12. Run the durable `0.3.1-dev` checks in `docs/TESTING.md` and review `Player.log`.

## Deferred follow-up

- Rework the Goa'uld observation operation into a more immersive activity.
- Slow the wounded Tok'ra agent's recovery so colony care remains mechanically important.
- Continue consolidating other device-specific debug actions into one grouped menu when several exist on the same object.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.3.1-dev - rework organic Tok'ra intelligence operation`;
- branch: `feature/tokra-intelligence-operation-rework`;
- annotated tag: `v0.3.1-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
