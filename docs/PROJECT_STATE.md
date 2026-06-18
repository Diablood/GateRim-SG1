# Project state

Current milestone: `0.2.52-dev - Add organic Tok'ra medical supply handoff`.

## Active development base

- Functional base tag: `v0.2.51-dev`.
- The starting snapshot also includes the later documentation commit adding `docs/MILESTONE_PUBLICATION.md`.
- Dedicated branch: `feature/tokra-organic-medical-supply-handoff`.
- Planned final tag after local validation: `v0.2.52-dev`.
- Current local test archive revision: `0.2.52-dev-r3`.

## Milestone scope

This milestone adds a fourth recurring organic Tok'ra operation:

- discreet Goa'uld activity observation;
- encrypted Tok'ra intelligence-module recovery;
- shelter and medical care for a seriously wounded Tok'ra agent;
- a face-to-face medical-supply handoff with a visiting Tok'ra liaison.

The medical handoff is a social and logistical operation distinct from wounded-agent care. It does not replace or alter the patient-care archetype validated in `0.2.51-dev`.

## Player flow

1. A Tok'ra cell opens a discreet channel and asks for two units of industrial medicine.
2. The offer does not inspect colony reserves and may appear even when the requested medicine is unavailable.
3. Ignoring the offer causes no trust consequence.
4. A selected colon accepts through the powered Tok'ra secure communicator.
5. One Tok'ra liaison arrives from a reachable map edge after a hidden delay of roughly one to two in-game hours.
6. The liaison walks toward, in priority order:
   - the Tok'ra delivery zone;
   - a powered Tok'ra communicator;
   - a reachable point near the colony centre.
7. A player colon capable of Social talks to the liaison.
8. A paused dialogue offers exactly two choices:
   - give two industrial medicines;
   - cancel and close the dialogue.
9. Giving the medicine checks accessible colony stocks at that moment and removes exactly two units. Insufficient stocks produce a rejection message without resolving the operation.
10. Success is applied immediately when the donation is confirmed. The liaison then leaves the map while the communicator returns to its generic RP state.

## Resolution rules

- A successful donation qualitatively improves Tok'ra trust.
- The negotiating colon gains `350` Social XP.
- Exact trust changes remain hidden from player-facing text.
- The liaison waits for six in-game hours after arrival.
- If no donation is completed before that deadline, the liaison leaves and the accepted operation fails once.
- If the liaison dies, disappears or is captured before the donation, the accepted operation fails once.
- If the liaison dies after the donation while leaving, the completed operation remains successful, but a separate relationship penalty and RP letter are applied.
- Ignoring the initial offer causes no penalty.
- Resolution guards prevent duplicate trust changes, XP, letters, cleanup or resource consumption after save/reload.

## Recurrence and anti-repetition

The medical-supply archetype is not permanently consumed after success or failure.

- It uses the shared hidden variable delay.
- The last offered archetype receives the existing strong local weighting reduction.
- It remains available with vanilla and compatible modded storytellers.
- Only one organic Tok'ra operation is active and visible at a time.

## Communicator presentation

The communicator exposes only the actual current state:

- medical-resupply offer awaiting a response;
- accepted request awaiting the liaison's arrival;
- liaison approaching the meeting point;
- liaison waiting for the donation;
- generic RP channel state immediately after success, failure, expiration or reset.

No catalog, history, future archetype, weighting or hidden delay is exposed.

## Save compatibility and legacy cleanup

- Existing archetype values remain unchanged:
  - `None = 0`;
  - `GoauldObservation = 1`;
  - `DeadDropRecovery = 2`;
  - `WoundedAgentCare = 3`.
- `MedicalSupplyHandoff = 4` remains appended without renumbering prior values.
- Existing operation-state values and `tokraOrganic...` save keys remain unchanged.
- The framework save version advances to `5` for liaison arrival, meeting, departure and post-success death tracking.
- Saves from `0.2.48-dev` through `0.2.51-dev` remain migration targets.
- Unpublished `0.2.52-dev-r1/r2` saves migrate from the temporary-container prototype to the liaison flow.
- The legacy container ThingDef and minimal building class remain only so those development saves can deserialize safely; every stale legacy container is removed automatically on load.
- The obsolete container component, float-menu component and hauling job are removed from the project.

## Developer validation actions

Under RimWorld developer actions:

- `Force Tok'ra observation offer`;
- `Force Tok'ra intelligence module offer`;
- `Force Tok'ra wounded agent offer`;
- `Force Tok'ra medical resupply offer`;
- `Advance active Tok'ra operation`;
- `Fail active Tok'ra operation`;
- `Reset Tok'ra operations`.

## Metadata

- `About/About.xml`: `modVersion = 0.2.52-dev`.
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.52.0`.
- Expected build output: `1.6/Assemblies/GateRimSG1.dll`.

## Required local validation

The durable checklist is maintained in `docs/TESTING.md` under **Tok'ra organic operation opportunities**:

1. accept the offer with no industrial medicine in storage;
2. verify liaison arrival after roughly one to two hours and meeting-point priority;
3. verify that a colon incapable of Social cannot start the exchange;
4. verify the two-button dialogue, cancellation and insufficient-stock message;
5. verify consumption of exactly two accessible industrial medicines, immediate success and exactly `350` Social XP;
6. verify that the liaison leaves after success and that a death during departure applies only the separate relationship consequence;
7. verify timeout, pre-handoff death, disappearance and capture as accepted failures;
8. save and reload before arrival, while approaching, while waiting, after cancelling the dialogue and while leaving after success;
9. transition through all four archetypes without stale communicator text or legacy containers;
10. load preserved `0.2.48-dev` through `0.2.51-dev` saves, optionally an `r1/r2` development save, and review `Player.log`.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.2.52-dev - add organic Tok'ra medical supply handoff`;
- branch: `feature/tokra-organic-medical-supply-handoff`;
- annotated tag: `v0.2.52-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are already ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable tests in `docs/TESTING.md`; do not create milestone-specific `TEST_PLAN_*.md` files.
