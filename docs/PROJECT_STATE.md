# Project state

Current milestone: `0.2.51-dev - Add organic Tok'ra wounded agent care`.

## Active development base

- Authoritative base tag: `v0.2.50-dev`.
- Dedicated branch: `feature/tokra-organic-wounded-agent-care`.
- Planned final tag after local validation: `v0.2.51-dev`.
- Current local test archive revision: `0.2.51-dev-r4`.

## Milestone scope

This milestone adds a third recurring organic Tok'ra operation without changing the two existing archetypes:

- discreet Goa'uld activity observation;
- encrypted Tok'ra intelligence-module recovery;
- shelter and medical care for a seriously wounded Tok'ra agent.

The former prototype based on placing two industrial medicines in a container is not part of this milestone. It may be reconsidered later as a separate logistical operation.

## Player flow

1. A Tok'ra cell opens an urgent channel and asks whether the colony can shelter a wounded agent.
2. The offer does not inspect colony medicine stocks and may be ignored without consequence.
3. A selected colon accepts through the powered Tok'ra secure communicator.
4. The wounded agent reaches the map from a reachable, unfogged edge and arrives downed under acute symbiote shock.
5. The shock prevents movement and suppresses the usual accelerated Tok'ra recovery, so the agent cannot walk to the colony or complete the event without player care.
6. The player must rescue the agent into a player medical bed and tend the symbiote shock itself; being carried during rescue remains a valid active-map state.
7. Once the shock has been tended, it is removed and normal Tok'ra regeneration resumes alongside vanilla medical care. The shock remains treatable even if all ordinary injuries or illnesses have already healed.
8. Once the agent remains medically stable and fit to travel for a short period, the agent is ordered to leave.
9. Success is applied only after the living agent has actually left the map.

The agent does not need to be completely healed. Departure requires consciousness, sufficient movement, controlled bleeding, acceptable overall health, no urgent medical-rest need and no condition near lethal severity.

## Resolution rules

- Successful safe departure improves Tok'ra trust qualitatively.
- The exact trust variation remains hidden from normal player-facing text.
- Normal tending grants vanilla Medicine experience; no artificial skill XP is added by the operation framework.
- Death, capture, disappearance or failure to become fit before the secure deadline causes one accepted-operation failure.
- Ignoring the initial offer causes no penalty.
- Resolution guards prevent duplicate trust effects, letters or cleanup after save/reload.
- A dead patient's corpse and a captured prisoner are not silently removed; other living unresolved patients are removed only by reset or failed-operation extraction cleanup.

## Recurrence and anti-repetition

The wounded-agent archetype is not permanently consumed after success or failure. It returns to the shared hidden scheduler and can recur in long games.

- Hidden variable delays remain active between Tok'ra organic opportunities.
- The last offered archetype receives the existing strong local weight reduction.
- The operation remains compatible with vanilla and modded storytellers.
- Broader coordination between vanilla and GateRim incidents remains planned for the future GateRim SG-1 storyteller.

## Communicator presentation

The communicator exposes only the operation that is actually active:

- current offer awaiting a response;
- accepted wounded-agent care with the patient's name and remaining secure window;
- recovered agent departing;
- generic RP channel status immediately after resolution.

It does not display a catalog, historical list, future archetypes, selection weights or internal delays. The observation and intelligence-recovery archetypes must continue following the same rule to prevent stale or offset status text.

## Save compatibility

- Existing archetype values remain unchanged: `None = 0`, `GoauldObservation = 1`, `DeadDropRecovery = 2`.
- `WoundedAgentCare = 3` is appended without renumbering prior values.
- Existing operation-state values remain unchanged.
- Existing `tokraOrganic...` save keys remain available.
- New persistent fields track the patient, whether initial colony treatment has occurred, the stable period, departure order and departure grace deadline.
- The framework save version advances to `3`.
- Saves from `0.2.48-dev`, `0.2.49-dev` and `0.2.50-dev` remain migration targets.

## Developer validation actions

Under RimWorld developer actions:

- `Force Tok'ra observation offer`;
- `Force Tok'ra intelligence module offer`;
- `Force Tok'ra wounded agent offer`;
- `Advance active Tok'ra operation`;
- `Fail active Tok'ra operation`;
- `Reset Tok'ra operations`.

`Advance active Tok'ra operation` remains intended for observation readiness. Patient recovery should normally be tested through health manipulation or real treatment so the fitness and departure checks are exercised.

## Metadata

- `About/About.xml`: `modVersion = 0.2.51-dev`.
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.51.0`.
- Expected build output: `1.6/Assemblies/GateRimSG1.dll`.

## Required local validation

The durable checklist is maintained in `docs/TESTING.md` under **Tok'ra organic operation opportunities** and follows `docs/TESTING_GUIDELINES.md`:

1. accept the request and verify that the patient arrives downed, cannot walk and cannot become fit to leave before colony treatment;
2. rescue the patient into a player medical bed, verify that carrying them does not fail or remove the operation, tend the symbiote shock itself even when it is the only remaining condition, and verify that the shock is removed before normal Tok'ra recovery resumes;
3. verify departure as soon as the patient is fit, without waiting for complete healing;
4. verify success only after the patient leaves the map;
5. verify death, capture, timeout and reset cleanup as separate destructive tests;
6. save and reload while the offer is pending, during care, while departing and after resolution;
7. transition through observation, intelligence recovery and wounded-agent care without stale communicator text;
8. load preserved `0.2.48-dev` to `0.2.50-dev` saves and review `Player.log`.

## Publication after validation

1. Commit with `0.2.51-dev - add organic Tok'ra wounded agent care`.
2. Publish `feature/tokra-organic-wounded-agent-care`.
3. Create and publish the unique annotated tag `v0.2.51-dev`.
4. Update the separate wiki repository with `git pull --ff-only`.
5. Run `.\tools\sync-wiki.cmd` from the main repository root.
6. Return to `GateRim-SG1.wiki`, review, commit and run `git push origin HEAD`.

## Repository rules reminder

- Work from `v0.2.50-dev`, not `main`.
- Publish only the final milestone tag without an `-rN` suffix.
- Keep test ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml` and the changelog only in `docs/CHANGELOG.md`.
- Keep durable tests in `docs/TESTING.md`; do not create milestone-specific `TEST_PLAN_*.md` files.
