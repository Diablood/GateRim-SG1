# Current project state

Current milestone: `0.3.40-dev - Add hostile Goa'uld host takeover` — final local revision `r4` functionally validated and published.

## Repository state

- Starting tag: `v0.3.39-dev`.
- Dedicated branch: `feature/goauld-hostile-host-takeover`.
- Published versions: `0.3.40-dev` and `0.3.40.0`.
- Final local revision: `r4`.
- Final tag: `v0.3.40-dev`.
- Main repository and separate wiki are published from the final milestone state.

## Published scope

- Preserve the existing one-day `recent Goa'uld implantation` intervention window.
- Record the free symbiote's persistent faction allegiance before implantation.
- Arm a hostile takeover only when a Goa'uld-origin symbiote belonging to a non-player faction hostile to the player implants a player colonist.
- At conversion, move the host into the symbiote's faction and remove player control without replacing the pawn, body, relationships, xenotype or persistent symbiote identity.
- Send a visible threat letter identifying the lost host and the controlling Goa'uld faction.
- Preserve pending and active takeover state through save and reload.
- Cancel the takeover and preserve or restore the displaced host faction when the symbiote is successfully extracted or the host state is removed.
- Preserve player control for Tok'ra implantations and for Goa'uld symbiotes controlled by the player or lacking a hostile faction allegiance.
- Keep the system independent from the free-symbiote incident so future hostile implantation paths can reuse it.

## Final implementation

Final local revision `r4` includes:

- persistent `allegianceFaction`, `displacedHostFaction` and `hostControlState` fields in `GoauldSymbioteData`;
- `Pending` and `Active` takeover states transferred with the same symbiote identity;
- hostile faction transfer at the recent-to-active conversion boundary;
- automatic host-faction restoration when extraction or supported state removal releases the symbiote;
- transaction-safe emergency extraction so a failed free-symbiote placement does not clear pending takeover state;
- restoration of the extracted symbiote's original faction allegiance;
- a dedicated developer submenu for inspection, forced conversion and state recovery;
- English/French takeover letter text and updated technical/player documentation;
- incident-spawned hostile free symbiotes attached to a no-retreat vanilla assault Lord so animal flight does not override implantation pursuit;
- converted hostile hosts attached to a dedicated persisted raid-like assault Lord instead of inheriting the immediate map-exit behavior of an isolated hostile former colonist;
- vanilla raid timeout and withdrawal enabled for converted hosts, allowing them to leave after a colony is abandoned or the assault is exhausted;
- automatic migration of the legacy no-retreat Lord used by local revision `r3`;
- removal of either takeover-assault generation before restoring the displaced host faction during recovery or extraction.

## Final validation

The final `r4` validation confirmed:

- consistency checks for `0.3.40-dev`, 83 backstories and DLL version `0.3.40.0`;
- stable free-symbiote pursuit before implantation without alternating into animal flight;
- persistent hostile allegiance, displaced faction and symbiote identity through implantation and save/reload;
- loss of player control, transfer to the recorded Goa'uld faction and a single threat letter at active conversion;
- immediate hostile attacks against player pawns and colony property instead of an immediate map exit;
- automatic migration of existing `r3` development saves to the retreat-capable takeover assault;
- stable assault behavior after save/reload;
- continued destruction of valid targets followed by eventual vanilla raid withdrawal from an abandoned or exhausted map;
- preservation of emergency extraction, Tok'ra implantation and player-controlled or factionless Goa'uld paths;
- no new GateRim SG-1 error reported in `Player.log`.

## Explicit boundaries

- This milestone does not add a new raid incident, world site or quest.
- It does not create a second implantation or identity system.
- Free symbiotes and converted hosts use separate assault Lords: the former preserves implantation pursuit without retreat, while the latter uses vanilla raid withdrawal after takeover.
- Post-conversion surgical extraction remains outside the mandatory player flow; developer recovery validates cleanup of the dedicated assault while the existing emergency intervention window remains the normal rescue path.

## Next milestone

No `0.3.41-dev` scope is imposed by this closure. Select the next item from `docs/ROADMAP.md`, reread the repository procedures, and create a dedicated branch explicitly from `v0.3.40-dev`.
