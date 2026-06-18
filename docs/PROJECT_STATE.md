# Project state

Current milestone: `0.2.49-dev - Add Tok'ra organic intelligence recovery`.

## Active development base

- Authoritative base tag: `v0.2.48-dev`.
- Dedicated branch: `feature/tokra-organic-dead-drop-recovery`.
- Planned final tag after local validation: `v0.2.49-dev`.
- Local test archive revision: `0.2.49-dev-r3`.

## Current implementation

`0.2.49-dev` extends the persistent Tok'ra organic-operation scheduler introduced in `0.2.48-dev` with a second preliminary archetype: recovery of a sealed Tok'ra intelligence module.

The existing discreet Goa'uld observation operation remains unchanged:

- it can be offered before Trusted contact;
- ignoring it has no trust penalty;
- success grants `+3` Tok'ra trust and `250` Intellectual XP;
- an accepted but missed report applies `-1` Tok'ra trust.

The new intelligence-recovery operation follows a different play flow:

- it can be offered at Wary, Neutral, Cooperative or Trusted trust tiers;
- it requires a player home map with a powered Tok'ra secure communicator;
- the initial offer is accepted at the communicator by an Intellectual-capable colon;
- ignoring the offer has no trust penalty;
- acceptance resolves the preferred delivery cell through the established Tok'ra delivery utility, then uses the same vanilla near-placement flow as existing medical deliveries and caches: delivery zone first, powered communicator second, reachable unfogged map edge last;
- the module must be secured directly by an Intellectual-capable colon within roughly `36` in-game hours;
- securing it completes the operation immediately without a second communicator transmission;
- success grants `+2` Tok'ra trust and `200` Intellectual XP to the securing colon;
- expiry, destruction or loss of the accepted module applies `-1` Tok'ra trust exactly once;
- the module represents encrypted data rather than loot, provides no material reward and vanishes after success or failure;
- if no valid delivery location can be found, acceptance is rejected cleanly and the offer remains available;
- the accepted module reference, deadline and operation state persist in saves.

The current trust thresholds remain unchanged:

- Wary: below `0`;
- Neutral: `0` to `9`;
- Cooperative: `10` to `24`;
- Trusted: `25` or more.

Both preliminary archetypes can therefore progress a colony beginning at trust `0` without weakening the Trusted-tier requirements of sensitive manual requests.

## Scheduling and archetype selection

- Initial hidden delay: about `3` to `6` in-game days.
- Wary recurrence: about `6` to `12` days.
- Neutral recurrence: about `4` to `8` days.
- Cooperative recurrence: about `3` to `7` days.
- Trusted recurrence: about `6` to `12` days.
- Observation weights: Wary `0.60`, Neutral `1.00`, Cooperative `0.85`, Trusted `0.35`.
- Intelligence-recovery weights: Wary `0.35`, Neutral `0.85`, Cooperative `1.00`, Trusted `0.55`.
- The last offered archetype is persisted and receives a `0.25` repeat-weight multiplier when both archetypes are compatible.
- The system discourages immediate repetition without enforcing a visible fixed alternation.
- No exact future date or fixed cadence is shown to the player.

## Physical intelligence-module objective

- ThingDef: `SG1_TokraOrganicDeadDrop`.
- JobDef: `SG1_SecureTokraOrganicDeadDrop`.
- The object is not buildable, minifiable, haulable or deconstructable.
- It remains physically destructible so hostile action or player destruction can fail the accepted operation.
- It has zero market value and yields no resources when destroyed.
- It reuses the existing Tok'ra coded-intelligence packet texture; no new binary texture is introduced.
- Internal `DeadDrop` identifiers are retained only for save compatibility and are not shown to players.

## Existing behavior intentionally preserved

- Manual secure-communicator support requests keep their existing Trusted-tier requirements.
- Relay sabotage remains a sensitive Trusted-tier operation.
- The original organic observation operation keeps its timings and rewards.
- The new recovery does not create a raid, world site, trade, recruitment, medical treatment, item reward or military support.
- Existing relay-operation outcome debrief behavior is unchanged.

## Debug validation actions

Under RimWorld developer actions:

- `Force Tok'ra organic observation opportunity`;
- `Force Tok'ra organic intelligence-recovery opportunity`;
- `Make Tok'ra organic observation report ready`;
- `Expire active Tok'ra organic operation`;
- `Reset Tok'ra organic operation tracker`.

Forced opportunities still require a powered player-controlled Tok'ra communicator on the current map so tests follow the real player interaction path.

## Current mod metadata after applying `0.2.49-dev`

- `About/About.xml`: `modVersion = 0.2.49-dev`.
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.49.0`.

## Required local validation

The durable validation checklist is maintained in `docs/TESTING.md` under **Tok'ra organic operation opportunities**.

For this revision, verify the established delivery hierarchy in particular:

1. beside or on the Tok'ra delivery drop zone when one exists;
2. beside a powered communicator only when no delivery zone exists;
3. at a reachable map edge only when neither preferred delivery target exists;
4. the placement log reports the preferred anchor cell and the final module cell.

The milestone-specific `TEST_PLAN_0.2.48-dev.md` and `TEST_PLAN_0.2.49-dev.md` files are obsolete and should be removed; Git and `docs/CHANGELOG.md` preserve milestone history.

## Build note

A forced C# rebuild is required. The expected output remains `1.6/Assemblies/GateRimSG1.dll`.

## Next step after validation

After local validation:

1. commit with `0.2.49-dev - add Tok'ra organic intelligence recovery`;
2. publish `feature/tokra-organic-dead-drop-recovery`;
3. create and publish the unique annotated tag `v0.2.49-dev`;
4. synchronize the separate wiki from the repository root with `./tools/sync-wiki.cmd` on POSIX shells or `.\tools\sync-wiki.cmd` in PowerShell.

A later milestone can add a third operation family, operation-specific world-state conditions, or stronger consequences at higher trust while preserving the current organic scheduler.

## Repository rules reminder

- Work on the dedicated branch created from `v0.2.48-dev`.
- Do not switch to `main` as the working base.
- Publish only the final milestone tag, without an `-rN` suffix.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml` and the changelog only in `docs/CHANGELOG.md`.
- Use `.\tools\sync-wiki.cmd` for normal wiki synchronization instead of manual file copying.
