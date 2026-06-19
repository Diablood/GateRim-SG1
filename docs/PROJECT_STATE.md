# Project state

Current milestone: `0.3.0-dev - Refactor organic operation framework`.

## Active development base

- Functional base tag: `v0.2.53-dev`.
- Dedicated branch: `feature/organic-operation-framework-refactor`.
- Planned final tag after local validation: `v0.3.0-dev`.
- Current local test archive revision: `0.3.0-dev-r2`.

## Milestone scope

This is an internal architectural milestone. It adds no new player-visible Tok'ra operation.

The four existing recurring operations are moved behind a reusable framework:

- Goa'uld observation;
- intelligence-module recovery;
- wounded-agent care;
- medical-supply handoff.

The target architecture contains:

- `GameComponent_TokraOrganicOperationManager` for scheduling and shared lifecycle;
- `TokraOrganicOperationInstance` for the single persistent active operation;
- `TokraOrganicOperationFollowUp` for consequences that outlive primary resolution;
- one worker per archetype;
- shared definition, placement, visitor, resource and trust services;
- one common debug and validation surface.

## Intentional save break

- Saves created with any `0.2.x-dev` build are unsupported.
- A new game is required for `0.3.0-dev`.
- Old Scribe fields and load migrations are not retained.
- The unpublished legacy medical-supply container class, Def and translation are removed.
- New saves created from `0.3.0-dev` become the future compatibility baseline.

## Debug requirements

The framework test surface must remain accessible through either:

- RimWorld developer mode; or
- the GateRim SG-1 advanced-debug option.

Required common controls:

- force any of the four offers;
- accept the current offer;
- advance the current testable phase;
- resolve success;
- resolve failure;
- expire the current state;
- inspect detailed persisted state;
- apply a pending post-operation consequence;
- reset the framework.

No technical control may be visible in normal play.

## Files intentionally removed

- `Source/GateRimSG1/Goauld/GameComponent_TokraOrganicOperationTracker.cs`;
- `Source/GateRimSG1/Goauld/Building_TokraOrganicMedicalSupplyContainer.cs`;
- `1.6/Defs/ThingDefs_Buildings/SG1_TokraOrganicMedicalSupplyContainer.xml`;
- `Languages/French/DefInjected/ThingDef/SG1_TokraOrganicMedicalSupplyContainer.xml`.

The tracker is replaced by the manager. The other three files existed only for unpublished `0.2.52-dev-r1/r2` save migration.

## Required local validation

1. Delete the four obsolete files listed above before extracting the milestone ZIP.
2. Build the assembly and confirm version `0.3.0.0`.
3. Start a new game; do not reuse a `0.2.x-dev` save.
4. Validate all four organic operations through their normal player flows.
5. Save and reload during offered, accepted and ready phases.
6. Save and reload while the medical liaison is leaving after success, then validate the separate death consequence.
7. Validate every common developer action and the communicator debug menu.
8. Disable developer mode and the advanced-debug option, then confirm that no operation-debug control remains visible.
9. Review `Player.log` for loading, Scribe, null-reference or duplicate-resolution errors.
10. Run the durable `0.3.0-dev` framework checks in `docs/TESTING.md`.

## Local validation status

The framework is functionally validated after `r2`:

- the assembly builds successfully;
- all four existing organic operations complete their tested player flows;
- save and reload work across the shared lifecycle phases;
- the common developer actions and communicator debug menu are accessible through the intended debug gates;
- no operation-debug control remains visible in normal play;
- no additional functional correction is currently required before publication.

## Deferred gameplay follow-up

These improvements are deliberately postponed until after framework stabilization:

- redesign the Goa'uld observation operation with a more immersive, interactive and rewarding objective;
- redesign the intelligence-module operation with stronger roleplay, meaningful constraints and more engaging player choices;
- slow the wounded Tok'ra agent's recovery so colony medical care remains necessary and mechanically significant.

They must be handled in later content milestones and must not delay publication of `0.3.0-dev`.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.3.0-dev - refactor organic operation framework`;
- branch: `feature/organic-operation-framework-refactor`;
- annotated tag: `v0.3.0-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
