# Project state

Current milestone: `0.2.48-dev - Add organic Tok'ra operation opportunities`.

## Active development base

- Authoritative base tag: `v0.2.47-dev`.
- Dedicated branch: `feature/tokra-organic-operation-opportunities`.
- Planned final tag after local validation: `v0.2.48-dev`.
- Local test archive revision: `0.2.48-dev-r1`.

## Current implementation

`0.2.48-dev` introduces a persistent scheduler for Tok'ra-initiated operational opportunities. The scheduler is deliberately separate from manual trusted-channel requests so preliminary work can be offered before the colony reaches the Trusted tier.

The first implemented archetype is a discreet observation request concerning unusual Goa'uld activity:

- it can be offered at Wary, Neutral, Cooperative or Trusted trust tiers;
- it requires a player home map with a powered Tok'ra secure communicator;
- the communicator remains constructible after the vanilla Microelectronics research and is not trust-gated;
- the Tok'ra cell opens the request automatically after a hidden variable delay;
- the offer remains available for roughly two in-game days;
- ignoring the offer has no trust penalty;
- a selected colon capable of Intellectual work accepts it through the communicator;
- after roughly six in-game hours, the observation report becomes ready;
- the accepted report must be transmitted before the secure window closes;
- success grants `+3` Tok'ra trust and `250` Intellectual XP to the operator;
- an accepted but missed report applies `-1` Tok'ra trust;
- offer, accepted operation, deadlines, anti-repetition context and counters persist in saves.

The current trust thresholds remain unchanged:

- Wary: below `0`;
- Neutral: `0` to `9`;
- Cooperative: `10` to `24`;
- Trusted: `25` or more.

A colony beginning at trust `0` can therefore progress through repeated successful preliminary operations instead of depending on an interaction already locked behind Trusted trust.

## Scheduling and selection

- Initial hidden delay: about `3` to `6` in-game days.
- Wary recurrence: about `6` to `12` days.
- Neutral recurrence: about `4` to `8` days.
- Cooperative recurrence: about `3` to `7` days.
- Trusted recurrence: about `6` to `12` days.
- Archetypes expose trust-dependent weights.
- The last offered archetype is persisted and receives a strong repeat-weight reduction when multiple compatible archetypes are available.
- No fixed cadence or exact future date is shown to the player.

## Existing behavior intentionally preserved

- Manual secure-communicator support requests keep their existing Trusted-tier requirements.
- Relay sabotage remains a sensitive Trusted-tier operation.
- The new preliminary observation request does not create a world site, raid, trade, recruitment, medical treatment, item reward or military support.
- Existing relay-operation outcome debrief behavior is unchanged.

## Debug validation actions

Under RimWorld developer actions:

- `Force Tok'ra organic observation opportunity`;
- `Make Tok'ra organic observation report ready`;
- `Reset Tok'ra organic operation tracker`.

The forced opportunity still requires a powered player-controlled Tok'ra communicator on the current map so the test follows the real player interaction path.

## Current mod metadata after applying `0.2.48-dev`

- `About/About.xml`: `modVersion = 0.2.48-dev`.
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.48.0`.

## Required local tests

1. Clean build against RimWorld 1.6 managed assemblies.
2. Load an existing save from `v0.2.47-dev` and confirm no load errors.
3. At trust `0`, force an organic opportunity and verify that it appears despite the Trusted-tier lock on manual requests.
4. Ignore one offer and verify that trust does not change.
5. Accept an offer with an Intellectual-capable colon and verify the report is initially unavailable.
6. Make the report ready, transmit it and verify `+3` trust plus `250` Intellectual XP.
7. Accept another offer, let its deadline expire and verify `-1` trust.
8. Save and reload during the offered and accepted states and verify persistence.
9. Verify the status report and communicator inspect string in English and French.
10. Verify that existing manual communicator actions still require Trusted trust.

## Build note

A forced C# rebuild is required. The expected output remains `1.6/Assemblies/GateRimSG1.dll`.

## Next step after validation

Commit the validated milestone, create the unique annotated tag `v0.2.48-dev`, publish the dedicated branch, update the main repository, and synchronize the separate wiki. A later milestone can add a second preliminary archetype so the anti-repetition weighting becomes player-visible rather than only architectural.

## Repository rules reminder

- Work on the dedicated branch created from `v0.2.47-dev`.
- Do not switch to `main` as the working base.
- Publish only the final milestone tag, without an `-rN` suffix.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml` and the changelog only in `docs/CHANGELOG.md`.
