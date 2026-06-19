# Project state

Current milestone: `0.3.4-dev - Improve Tok'ra observation site visuals and field flow`.

## Active development base

- Functional base tag: `v0.3.3-dev`.
- Dedicated branch: `feature/tokra-observation-site-visual-rework`.
- Planned final tag after local validation: `v0.3.4-dev`.
- Current local test archive revision: `0.3.4-dev-r4`.

## Milestone scope

This milestone keeps the established Tok'ra observation objective but improves both its visual readability and the coherence of the field actions.

The old automatic six-hour recording phase is removed. Observation is now an operator-controlled task:

1. a Tok'ra observation device is delivered to the map;
2. a colon carries it to the peripheral observation point;
3. the device is installed as a dedicated field scope;
4. the same colon remains at the scope and observes for a random duration of roughly one to two in-game hours;
5. when the observation is complete, the colon immediately packs the scope up;
6. the colon carries it directly to the powered Tok'ra communicator;
7. the final transmission resolves the operation.

If the continuous task is interrupted after deployment, the player can right-click the installed observation site to resume the remaining observation work and continue through recovery and transmission.

## Visual and animation rework

The temporary observation point no longer reuses the generic Tok'ra delivery spot ring. It uses a dedicated field-scope texture with a tripod, directional optic and compact sensor housing.

The previous construction effect and drill-like sound are removed from deployment and recovery. During the actual observation toil, the colon remains beside the scope and faces it, matching the visual language of using an observation instrument rather than constructing a building.

## Failure conditions

The operation fails if:

- the observation site or device is destroyed before transmission;
- the device becomes unavailable;
- the accepted operation is not completed before its existing secure deadline.

The operation no longer succeeds from a background timer. Success is granted only after the observed data are physically returned and transmitted.

## Save compatibility

- `0.3.0-dev` remains the framework compatibility baseline.
- Existing `0.3.2-dev` and `0.3.3-dev` observation saves remain loadable.
- A deployed site using the old automatic timer is converted into remaining operator-controlled observation work.
- An already-ready site remains ready for recovery.
- No active observation should be duplicated during conversion.

## Debug requirements

The grouped communicator debug menu remains the single debug entry point.

- `Observation : déployer le dispositif` creates the installed field state.
- `Observation : terminer l’enregistrement` completes the operator-controlled observation work.
- Full progress values remain visible only in the technical debug report.
- No debug gizmo is visible in normal play.

## Files intentionally removed

None for this milestone revision.

## Required local validation

1. Build the assembly and confirm version `0.3.4.0`.
2. Confirm the observation point remains absent from all Architect categories.
3. Force and accept an observation operation.
4. Confirm the temporary site uses the dedicated field-scope visual.
5. Start deployment and confirm the colon carries the device to the site.
6. Confirm deployment no longer plays the construction drill effect.
7. Confirm the same colon remains at the scope and faces it during roughly one to two hours of observation work.
8. Confirm the colon immediately packs the scope, returns to the communicator and transmits without an automatic waiting phase or a second player order.
9. Interrupt during observation, then resume by right-clicking the installed site.
10. Save and reload during observation, during recovery and during the return trip.
11. Destroy the installed site and confirm a single failure.
12. Let the secure deadline expire and confirm a single failure.
13. Review `Player.log` for XML, texture, job, reservation, Scribe or duplicate-resolution errors.

## Durable handoff files

Project continuity must not depend on the current discussion history. Before resuming work after a context loss or in a new discussion, consult:

- `docs/PROJECT_STATE.md` for the active milestone;
- `docs/ROADMAP.md` for all deferred additions and future actions;
- `docs/MILESTONE_PUBLICATION.md` before publication;
- `AGENTS.md` for repository working rules.

The former outdated roadmap has been replaced by a concise current backlog. Historical milestone details remain in `docs/CHANGELOG.md`.

## Deferred follow-up

The complete durable backlog is maintained in `docs/ROADMAP.md`. The most directly related deferred items are:

- replace the provisional mail-like texture of the portable observation device during the global object-visual pass;
- consider a separate visual state for completed observation data only if it provides clear value;
- continue consolidating device-specific debug actions into grouped menus when several exist on the same object;
- add further distinct operation archetypes only after the existing four remain stable on the shared framework.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.3.4-dev - improve Tok'ra observation field flow`;
- branch: `feature/tokra-observation-site-visual-rework`;
- annotated tag: `v0.3.4-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
