# Tok'ra organic-operation pool audit

Status: published in `0.3.38-dev` after final local validation revision `r2`.

## Purpose

The recurrent Tok'ra pool now contains eight persisted archetypes. Before adding another mission, this milestone verifies that the shared scheduler still provides complete coverage, hidden recurrence, local anti-repetition, narrative variation and one global active slot.

The audit is developer-only. It does not expose weights, delays, histories or future operations to the player-facing communicator.

## Persisted coverage

The report derives the required archetype list directly from `TokraOrganicOperationArchetype`, excluding only `None`. A new persisted enum value therefore becomes an explicit audit requirement automatically.

The report fails when:

- an enum archetype has no resolved operation definition;
- an archetype resolves more than once;
- the number of resolved definitions differs from the persisted enum count;
- a current operation is not backed by a `GateRimMissionDef`.

## Definition invariants

Every current definition must provide:

- a repeated-archetype factor greater than zero and lower than one;
- at least two offer-text variants;
- at least two success-narrative variants, either in the direct success bank or in named success banks;
- a positive selection weight for each current Tok'ra trust tier;
- a positive hidden-delay range with a maximum not lower than the minimum.

Intelligence recovery was the only operation with a single offer text. Revision `r1` adds two English/French variants without changing its objective, timing, reward, threat behavior or persistence. Revision `r2` changes documentation metadata only after the successful Windows build.

## Deterministic selection simulation

For each trust tier, the report performs two deterministic `5000`-draw simulations without mutating the save:

1. the real candidate weights with the previous-archetype penalty applied;
2. the same weights with no local repeat penalty.

The audit requires every weighted archetype to appear and the penalized run to produce fewer immediate repetitions than the no-penalty baseline. The report also prints per-archetype draw totals for balance review without treating equal distribution as a goal.

## Runtime selection contract

The real scheduler continues to:

1. collect definitions with a positive weight for the current trust tier;
2. call the mission worker's `CanOffer(map)` check;
3. remove temporarily unavailable operations before the draw;
4. penalize only the most recently offered archetype;
5. draw from the remaining weights;
6. schedule another hidden delay after success, failure or an ignored offer.

No timing, weight, reward or save-field change is introduced by this audit milestone.

## Developer path

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra...
→ Organic operations...
→ Framework...
→ Audit long-term orchestration
```

## Final validation

Final local revision `r2` validated the audit in game:

- eight persisted archetypes and eight resolved definitions;
- no missing or duplicated definition;
- every current operation MissionDef-backed;
- all four trust tiers completing `5000` draws and reaching all weighted archetypes;
- `penalty effective=True` in every tier;
- final result `Audit result: PASS`;
- three successive intelligence-recovery offers without immediate text repetition;
- the same `PASS` result after save and reload;
- no new blocking GateRim SG-1 error in `Player.log`.

The durable regression coverage is maintained in `docs/TESTING.md` and the final milestone result in `docs/TESTING_CURRENT.md`.
