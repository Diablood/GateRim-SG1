# Project state

Current milestone: `0.2.50-dev - Consolidate organic Tok'ra operation framework`.

## Active development base

- Authoritative base tag: `v0.2.49-dev`.
- Dedicated branch: `feature/tokra-organic-operation-framework-consolidation`.
- Planned final tag after local validation: `v0.2.50-dev`.
- Local test archive revision: `0.2.50-dev-r5`.

## Milestone scope

This milestone consolidates the two existing preliminary Tok'ra operations without adding a new player-visible archetype or changing their balance:

- discreet Goa'uld activity observation;
- encrypted Tok'ra intelligence-module recovery.

Manual communicator requests, the playable relay mission and all Trusted-tier requirements remain separate and unchanged.

## Shared operation framework

`TokraOrganicOperationFramework` now contains the common definition of each archetype:

- trust-tier selection weights and repeat reduction;
- offer duration, preparation delay and operation deadline;
- Intellectual XP and Tok'ra trust consequences;
- communicator action, letter and status translation keys;
- optional physical objective definition;
- shared objective placement, lookup and cleanup rules.

The persistent tracker keeps the established player flow while using one common resolution path for success and failure. That path applies trust, XP, letters, counters, cleanup and rescheduling once only.
Observation readiness is now an explicit persistent state. Tick processing, communicator menu generation, direct interaction and load repair all synchronize the state before deciding whether `Transmit Tok'ra observation report` is available.

## Save compatibility

Compatibility with saves created by `0.2.48-dev` and `0.2.49-dev` is required:

- existing archetype values and the legacy state values `None = 0`, `Offered = 1` and `Accepted = 2` remain unchanged;
- the consolidated observation-ready state is persisted as the new value `Ready = 3`;
- all existing `tokraOrganic...` Scribe keys remain available;
- a framework save-version key and resolution guard are added with safe defaults;
- missing legacy deadlines and preparation ticks are reconstructed from the current archetype definition;
- an accepted intelligence module is recovered from the active map when the saved reference is absent;
- stale intelligence modules not associated with the active operation are removed;
- reloading or calling completion twice must never grant duplicate trust, XP or letters.

## Existing balance preserved

Observation remains:

- success: `+3` Tok'ra trust and `250` Intellectual XP;
- accepted failure: `-1` trust;
- report preparation and secure-window timings unchanged.

Intelligence recovery remains:

- success: `+2` Tok'ra trust and `200` Intellectual XP;
- accepted failure: `-1` trust;
- delivery and recovery timings unchanged;
- no material reward.

Ignored offers remain consequence-free.

## Physical-objective routing

Physical Tok'ra objectives use the shared delivery helper already used elsewhere in the mod:

1. beside or on the Tok'ra delivery drop zone when present;
2. beside a powered Tok'ra secure communicator when no delivery zone is available;
3. at a reachable, unfogged map edge only as the final fallback.

The intelligence-module operation still requires a communicator for acceptance, so the edge-only path remains primarily an internal safety fallback.

## Developer validation actions

Under RimWorld developer actions:

- `Force Tok'ra observation offer`;
- `Force Tok'ra intelligence module offer`;
- `Advance active Tok'ra operation`;
- `Fail active Tok'ra operation`;
- `Reset Tok'ra operations`.

Developer labels remain technical but are deliberately short enough to avoid truncation in RimWorld's developer-action menu. Player-facing communicator labels and messages remain concise and RP-oriented. Player-facing tests verify qualitative trust feedback rather than hidden raw values.

Earlier developer actions now follow the same compact `subject: action` convention for Jaffa marks and the Tok'ra safehouse, trust, cache, mission-site, relay and intercepted-threat tools.

## Metadata

- `About/About.xml`: `modVersion = 0.2.50-dev`.
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.50.0`.
- Expected build output: `1.6/Assemblies/GateRimSG1.dll`.

## Required local validation

The durable checklist is maintained in `docs/TESTING.md` under **Tok'ra organic operation opportunities**, using the structure defined in `docs/TESTING_GUIDELINES.md`. Tests are ordered as reusable sessions rather than milestone shorthand:

1. run the uninterrupted observation and intelligence-recovery success paths consecutively;
2. verify delivery-zone and communicator placement in the same running game;
3. use named current-version checkpoints for offered, accepted, ready and resolved reload tests;
4. run destructive and expiry failures from copies of the common base checkpoint;
5. load preserved `0.2.48-dev` and `0.2.49-dev` saves for migration checks;
6. retest manual communicator requests and review `Player.log` for regressions.

## Publication after validation

1. Commit with `0.2.50-dev - consolidate organic Tok'ra operation framework`.
2. Publish `feature/tokra-organic-operation-framework-consolidation`.
3. Create and publish the unique annotated tag `v0.2.50-dev`.
4. Update the separate wiki repository with `git pull --ff-only`.
5. Run `.\tools\sync-wiki.cmd` from the main repository root.
6. Return to `GateRim-SG1.wiki`, review, commit and run `git push origin HEAD`.

## Repository rules reminder

- Work from `v0.2.49-dev`, not `main`.
- Publish only the final milestone tag without an `-rN` suffix.
- Keep test ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml` and the changelog only in `docs/CHANGELOG.md`.
- Keep durable tests in `docs/TESTING.md`, follow `docs/TESTING_GUIDELINES.md`, and do not create milestone-specific `TEST_PLAN_*.md` files.
