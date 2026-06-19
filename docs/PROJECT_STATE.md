# Project state

Current milestone: `0.3.2-dev - Rework organic Tok'ra observation operation`.

## Active development base

- Functional base tag: `v0.3.1-dev`.
- Dedicated branch: `feature/tokra-observation-operation-rework`.
- Planned final tag after local validation: `v0.3.2-dev`.
- Current local test archive revision: `0.3.2-dev-r3`.

## Milestone scope

This milestone reworks the existing recurring Goa'uld-observation archetype without adding a fifth organic operation.

The `0.3.0-dev` framework remains the lifecycle and persistence base. The observation worker now uses a physical field-operation flow:

1. acceptance delivers one operation-only Tok'ra observation device;
2. a temporary observation point is selected near the map perimeter;
3. an Intellectual-capable colon retrieves the device, carries it to that point and installs it as a field station;
4. the installed station records for several in-game hours while remaining exposed to normal map danger;
5. once the data is ready, an Intellectual-capable colon starts recovery directly from the field station;
6. that colon packs up the sensor, carries it straight to the powered communicator and transmits the recording in the same continuous job;
7. success is applied only after the final transmission completes.

## Player-facing rules

- The observation device and temporary point are generated only by the operation.
- Neither appears in the Architect menu or can be built by the player.
- Deployment begins by right-clicking the delivered device with an Intellectual-capable colon.
- The deployment job physically carries the device to the marked peripheral point and installs it instead of dropping it as a loose item.
- Interrupted transport, deployment, recovery or transmission can be resumed; the communicator is used only to resume after the sensor has already been packed up.
- Destroying or losing the accepted device produces one accepted failure.
- Exceeding the operation deadline after acceptance produces one accepted failure.
- The recording finishing by itself does not grant success.
- Success requires the field station to be packed up on site, carried directly to a powered Tok'ra communicator and transmitted.
- Successful occurrences use several RP result variants and avoid immediate repetition when possible.

## Communicator information boundary

Normal play shows only:

- the currently offered, accepted or active organic operation;
- the observation phase relevant to the player: awaiting deployment, recording, data ready or interrupted transmission;
- durable completed progress from unique mission chains when it remains relevant.

Normal play must not reveal internal timers, selection weights, saved object identifiers, result-variant indices or framework history. The developer report may expose all technical state.

## Save compatibility

- `0.3.0-dev` remains the compatibility baseline.
- New observation fields load with safe defaults.
- A `0.3.1-dev` observation offer remains acceptable.
- A `0.3.1-dev` observation already accepted under the old abstract timer is converted once into the new delivered-device workflow instead of being lost.
- Resolved operations and the other three archetypes retain their existing persistence.

## Debug requirements

The existing single communicator debug gizmo remains the main debug surface. Its menu and the RimWorld developer actions must allow:

- forcing and accepting the observation offer;
- forcing device deployment;
- completing the recording period;
- forcing success, failure or expiration;
- inspecting the complete persisted observation state;
- resetting the framework.

No observation debug control may be visible in normal play.

## Files intentionally removed

None for this milestone revision.

## Required local validation

1. Build the assembly and confirm version `0.3.2.0`.
2. Start from a new game or a save created with `0.3.0-dev`/`0.3.1-dev`.
3. Confirm the device and temporary point are absent from every Architect category.
4. Accept the offer and validate delivery plus peripheral point selection.
5. Make a colon retrieve, carry and deploy the device.
6. Interrupt and resume deployment, including across save/reload.
7. Confirm the recording finishes without granting success.
8. Start the final action directly from the observation site and validate packing, return and transmission as one continuous job.
9. Interrupt recovery before packing, during the return trip and during transmission, including across save/reload.
10. Validate success only after transmission, device cleanup, XP/trust feedback and RP result variants.
11. Validate destruction and deadline failures without duplicate consequences.
12. Compare the compact normal communicator report with the complete debug report.
13. Run the durable `0.3.2-dev` checks in `docs/TESTING.md` and review `Player.log`.

## Deferred follow-up

- Slow the wounded Tok'ra agent's recovery so colony care remains mechanically important.
- Continue consolidating device-specific debug actions into grouped menus when several exist on the same object.
- Add further distinct operation archetypes only after the existing four remain stable on the shared framework.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.3.2-dev - rework organic Tok'ra observation operation`;
- branch: `feature/tokra-observation-operation-rework`;
- annotated tag: `v0.3.2-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
