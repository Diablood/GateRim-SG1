# Project state

Current milestone: `0.3.31-dev - Add Tok'ra introduction artifact mission` — validated locally on final revision `r3` and published under the final tag `v0.3.31-dev`.

## Published base

- Starting tag: `v0.3.30-dev`.
- Dedicated branch: `feature/tokra-introduction-artifact-mission`.
- Final local revision: `0.3.31-dev-r3`.
- Final published tag: `v0.3.31-dev`.
- Assembly version: `0.3.31.0`.
- Cultural backstory count remains `83`.

## Final milestone outcome

This milestone adds a standalone Tok'ra introduction arc available before the secure communicator exists. A hidden contact can reveal a temporary Goa'uld-held recovery site containing a physical Tok'ra cipher module. The arc remains retryable after refusal, expiration or failure and becomes permanently closed only when the exact tracked module is recovered by the player.

The introduction arc remains independent from the six recurrent communicator operations. It owns its own persistent state, hidden retry timing, choice letter, world site, adaptive encounter and permanent completion flag without consuming the recurrent-operation slot.

## Validated introduction flow

- Schedule the first hidden opportunity after `4–12` days and retain it through save/reload.
- Present a persistent choice letter with explicit accept and decline actions.
- Return declined, ignored or expired offers after a hidden `10–60` day delay.
- Create the hostile recovery site only after acceptance and preserve normal RimWorld caravan travel and arrival.
- Capture the threat snapshot at offer time and use a deliberately moderate profile: factor `0.35`, `180–650` points and `2–6` defenders.
- Generate one exact tracked Tok'ra cipher module and reject separately spawned copies as completion objectives.
- Preserve vanilla combat, loot selection and caravan reformation so the player may recover survivors, animals, enemy equipment and other allowed map items.
- Complete the arc only after the tracked module actually reaches a player caravan, pawn inventory or home map.
- Warn once approximately one day before the six-day site deadline and persist that warning through save/reload.
- Fail and reschedule accepted attempts after artifact destruction, site timeout or unexpected site loss, using a hidden `7–45` day delay.
- Recalculate and persist a new delay after each failed attempt without permanently locking the arc.
- Close the arc permanently after the first successful recovery and prevent duplicate offers, sites, modules or completion rewards.

## Validation status

- Revision `r1` persistent-state, retry and permanent-completion foundations were validated.
- Revision `r2` compiled after the consolidated API and MissionDef fixes; the natural offer, persistent choice letter, site creation, adaptive combat, physical module recovery, loot and vanilla reformation flow were validated.
- Revision `r3` warning, real site expiration, real tracked-module destruction, intermediate save/reload and final successful recovery were reported valid in game.
- Multiple failure paths preserve retry eligibility and do not close the arc before success.
- The final player-text review found no opaque debug-style wording requiring a gameplay-text rewrite.
- No blocking loading or runtime error was reported during the final validated pass.

## Deferred work

- Add a dedicated final texture for the cipher module during the later global visual pass.
- Continue long-duration balancing of the `2–6` defender profile across a wider range of colony wealth and storyteller settings; the introduction must remain easier than later Tok'ra combat arcs.
- Add artifact study and dedicated Tok'ra research with `Electricity` as a prerequisite.
- Require the completed research for communicator construction.
- Gate recurrent Tok'ra operations behind an available powered communicator without making them storyteller-dependent.

## Next development step

No new milestone or branch is opened by this publication. The next milestone must start explicitly from `v0.3.31-dev` on a dedicated branch after reviewing `docs/ROADMAP.md`. The next documented part of the arc is the cipher-module study and dedicated Tok'ra research; communicator construction and recurrent-operation gating remain separate follow-up work.

## Publication

The main repository branch and final annotated tag use the version without a local `-rN` suffix. The updated `docs/wiki/` pages must be synchronized to the separate `GateRim-SG1.wiki` repository as part of this publication.
