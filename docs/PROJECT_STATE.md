# Current project state

Current milestone: `0.3.39-dev - Add Goa'uld free-symbiote incursion incident` — functionally validated in local revision `r1` and published under the final milestone version.

## Repository state

- Starting tag: `v0.3.38-dev`.
- Dedicated branch: `feature/goauld-free-symbiote-incursion`.
- Published versions: `0.3.39-dev` and `0.3.39.0`.
- Final local validation revision: `r1`.
- Final unique tag: `v0.3.39-dev`.
- The main repository and separate wiki are synchronized for the milestone.

## Published outcome

- Add the first recurrent autonomous Goa'uld biological-hazard incident.
- Spawn the existing free Goa'uld symbiote pawn kind from a reachable hostile map edge.
- Reuse the established autonomous pursuit and persistent implantation path without a parallel infection system.
- Scale each incursion from `1` to `4` symbiotes using storyteller threat points, with a deliberately low cap.
- Require an existing visible Goa'uld System Lord faction and at least one compatible player colonist.
- Add three English/French RP warning variants with persistent immediate-repeat prevention.
- Add a compact Goa'uld developer submenu for current, weak-colony and advanced-colony scaling tests.
- Preserve Tok'ra operations, host identity, extraction, active-host behavior and save data.

## Final validation

The final `r1` pass validated the incident in game after rebuilding and loading `GateRimSG1.dll` version `0.3.39.0`:

1. the consistency check and Windows build complete successfully;
2. the weak-colony profile spawns exactly one hostile free symbiote;
3. the advanced-colony profile spawns exactly four symbiotes in one edge cluster;
4. autonomous target acquisition and pursuit reuse the existing Goa'uld hunt system;
5. successful contact creates one recent Goa'uld implantation, transfers the persistent symbiote identity and removes only the contacting free pawn;
6. warning variants avoid immediate repetition and preserve the previous selection after save and reload;
7. Tok'ra symbiotes, manual and ritual Goa'uld implantation, extraction and Tok'ra organic-operation state remain unchanged;
8. `Player.log` contains no new blocking GateRim SG-1 error after the rebuilt DLL is loaded.

The first load attempt used the previously built `0.3.38.0` DLL and therefore could not resolve the new incident worker. Rebuilding to `0.3.39.0` corrected that deployment mismatch without a code revision.

## Deferred work

- Host faction or player-control changes after Goa'uld implantation remain a separate system-wide milestone.
- Jaffa escorts, world sites and mission rewards are intentionally outside this incident.
- The global world-map icon pass and distinctive Jaffa-officer appearance remain tracked in `docs/ROADMAP.md`.

## Next milestone

No `0.3.40-dev` scope is imposed by this closure. The next milestone must be selected from `docs/ROADMAP.md`, then start explicitly from `v0.3.39-dev` on a new dedicated branch after rereading the repository procedures.
