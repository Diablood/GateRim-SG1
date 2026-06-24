# Current project state

Current milestone: `0.3.38-dev - Audit Tok'ra organic operation pool and long-term variety` — functionally validated in local revision `r2` and published under the final milestone version.

## Repository state

- Starting tag: `v0.3.37-dev`.
- Dedicated branch: `feature/tokra-operation-pool-audit`.
- Published versions: `0.3.38-dev` and `0.3.38.0`.
- Final local validation revision: `r2`.
- Final unique tag: `v0.3.38-dev`.
- The main repository and separate wiki are synchronized for the milestone.

## Published outcome

- Audit the complete recurrent Tok'ra pool after its expansion to eight persisted archetypes.
- Derive required coverage directly from the persisted enum instead of a hardcoded minimum count.
- Detect missing or duplicated archetype definitions and verify that every current operation is MissionDef-backed.
- Validate every trust-tier weight, hidden-delay range and local repeated-archetype factor.
- Compare deterministic penalized selection against an equivalent no-penalty baseline for every trust tier.
- Verify that every recurrent operation provides multiple offer and success narrative variants.
- Add two English/French RP offer variants to intelligence recovery, the only archetype that still repeated one offer text.
- Preserve scheduling weights, delays, rewards, the global active slot and save data.

## Final validation

The final `r2` pass validated the shared orchestration in game:

1. the audit reports eight persisted archetypes and eight resolved definitions;
2. no definition is missing or duplicated and every operation is MissionDef-backed;
3. all four trust tiers complete the deterministic `5000`-draw simulations;
4. every weighted archetype is reached in every tier;
5. the real local repeat penalty produces fewer immediate repetitions than the no-penalty baseline;
6. the final audit result is `PASS`;
7. three successive intelligence-recovery offers do not repeat the immediately previous text;
8. the audit remains valid after save and reload;
9. the consistency check, Windows build and `Player.log` show no new blocking error.

The milestone reported no remaining functional blocker. Natural-draw, result-rescheduling and temporarily unavailable-mission cases remain durable regression coverage for future changes to the scheduler.

## Next milestone

No `0.3.39-dev` scope is imposed by this closure. The next milestone must be chosen after reviewing `docs/ROADMAP.md`, then start explicitly from `v0.3.38-dev` on a new dedicated branch after rereading the repository procedures.
