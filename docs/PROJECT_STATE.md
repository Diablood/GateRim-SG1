# Current project state

Current milestone: `0.3.41-dev - Add active Goa'uld host extraction surgery` — completed in final local revision `r2` and published under final tag `v0.3.41-dev`.

## Repository state

- Starting tag: `v0.3.40-dev`.
- Dedicated branch: `feature/goauld-active-host-extraction-surgery`.
- Published versions: `0.3.41-dev` and `0.3.41.0`.
- Final local revision: `r2`.
- Final commit: `0.3.41-dev - add active Goa'uld host extraction surgery`.
- Final annotated tag: `v0.3.41-dev`.
- Main GitHub repository and separate wiki are synchronized.

## Validated result

The final validation covers the complete active-host rescue loop:

- hostile implantation, active takeover, downing and colony capture;
- stable detention without reassignment to the takeover assault;
- availability and completion of `SG1_ExtractActiveGoauldSymbiote`;
- restoration of the existing host pawn, original full name and displaced faction;
- preservation of the symbiote ID, name, origin, host history and Goa'uld allegiance;
- appearance of exactly one live free symbiote under temporary vanilla anesthesia;
- return to hostile behavior after the anesthesia ends;
- ordinary nonlethal surgery failure without identity duplication or state loss;
- persistence of the active name, host identity and extracted symbiote through save/reload;
- strict developer-mode restriction of instant extraction, forced implantation and autonomous-hunt controls;
- preservation of legitimate player-controlled ritual and tracked Tok'ra offer interactions;
- no new GateRim SG-1 error reported in `Player.log`.

## Extracted-symbiote boundary

The active-host operation deliberately returns the Goa'uld alive. It is not killed automatically: the anesthesia only gives the colony time to secure or eliminate it before it wakes and becomes dangerous again.

`0.3.41-dev` adds no improvised prisoner, animal-taming, research or containment path. A possible dedicated containment system and later Tok'ra handoff are recorded only as an unplanned design question in `docs/IDEAS_TO_REVISIT.md`. That entry is not a roadmap commitment and does not create a future milestone.

## Next step

No `0.3.42-dev` feature scope is selected. Any new milestone must be chosen after rereading `docs/ROADMAP.md`, `docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`, then start explicitly from `v0.3.41-dev` on a dedicated branch.
