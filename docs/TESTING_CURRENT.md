# Current milestone testing

Current milestone: `0.3.8-dev - Rework existing cultural backstories`.

Status: local functional validation complete. Publication remains.

## Validated

- RimWorld loads without XML, translation or `BackstoryDef` errors.
- All `52` existing cultural backstories remain available.
- English and French descriptions display correctly.
- Every backstory has coherent, moderate skill gains.
- No passion, trait or work incapability is added.
- SG-team starting candidates retain expected names and histories.
- Representative Jaffa, Free Jaffa, Goa'uld host, System Lord and Tok'ra pawns generate with coherent histories.
- Goa'uld raids and natural pawn generation show no regression.
- Existing colon names and backstories remain unchanged after voluntary Tok'ra implantation.
- Saving, fully quitting and reloading preserve names, histories and skills.
- The wiki catalogue contains `52` entries grouped into eight cultural tables.
- `Player.log` is clean for the tested scope.

## Deferred observations

- Starter-only culture filtering is not implemented in `0.3.8-dev`; it belongs to the next dedicated cultural-profile milestone.
- Tok'ra implantation currently loses the visible reference to the symbiote name after fusion. The preserved design for a future player-controlled dual-identity system is in `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`.

## Publication identifiers

- branch: `feature/cultural-backstory-rework`;
- commit: `0.3.8-dev - rework cultural backstories`;
- final annotated tag: `v0.3.8-dev`.

`docs/TESTING.md` remains the complete historical regression archive. Future active milestones should update this file with only their current targeted checks and results.
