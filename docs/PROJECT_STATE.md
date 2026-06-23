# Project state

Current milestone: `0.3.34-dev - Add Tok'ra diversion assault operation` — completed and published under final tag `v0.3.34-dev` after local revision `r3` validation.

## Published state

- Starting tag: `v0.3.33-dev`.
- Dedicated branch: `feature/tokra-decoy-transmission-defense`.
- Final local revision: `0.3.34-dev-r3`.
- Published tag: `v0.3.34-dev`.
- Assembly version validated: `0.3.34.0`.
- Mod metadata version: `0.3.34-dev`.
- Cultural backstory count remains `83`.
- Main GitHub repository and separate wiki are published from the final milestone state.

## Implemented scope

- Keep `SG1_TokraOrganic_DecoyTransmissionDefense` and the stable `DecoyTransmissionDefense` archetype value to avoid renumbering persisted development data.
- Present the operation as a false Tok'ra signal followed by an unavoidable Goa'uld/Jaffa assault, with no delivered device or map objective.
- Use the mission-only raid strategy `SG1_GoauldJaffaLuredBreachingAssault`.
- Add the mission-only pawn-group kind `SG1_TokraDiversionAssault` and select it explicitly through `IncidentParms.pawnGroupKind`.
- Add `SG1_GoauldJaffaBreacher`, marked as a valid sapper and good breacher, restricted to this mission group and equipped through the inherited Ma'Tok-only weapon tag.
- Keep ordinary Goa'uld `Combat` and `Settlement` groups unchanged.
- Validate at configuration time that the mission group exists and contains a Ma'Tok-equipped good breacher; disable the operation cleanly if that contract is broken.
- Register the exact spawned mission raiders and expose the number of registered breachers in the shared framework-state report.
- Resolve success after neutralization or empty-handed retreat; resolve failure when an attacker exits with a hostage or loot, or when the player map is lost.
- Preserve the offer-time threat snapshot with factor `0.75` and bounds `180–3000`.
- Keep the operation recurrent, communicator-gated and integrated into the shared seven-archetype planner.

## Validation completed

- Project consistency check passed for `0.3.34-dev`, assembly `0.3.34.0` and `83` unique backstories.
- Forced rebuild `0.3.34.0` completed successfully.
- The operation remained enabled with no missing-group or missing-breacher configuration error.
- The dedicated raid generated once at its configured threat budget instead of falling back near `104999` points.
- The framework report showed registered raiders and at least one registered breacher.
- A spawned `Goa'uld Jaffa breacher` / `sapeur Jaffa au service des Goa'uld` carried a Ma'Tok.
- The breach group attacked walls or closed access routes on a sealed colony.
- Save/reload during the assault preserved the registered force without creating a second raid.
- Victory by combat resolved once, granted `+3` Tok'ra trust once and released the organic-operation slot.
- The former `MinimumPoints`, missing required pawn-kind and unusable `PawnGroupMaker` errors no longer appeared in `Player.log`.

The optional hostage, loot, map-loss, empty-handed retreat, weak/advanced-colony scaling and full previous-operation regression paths were not rerun during the final focused r3 pass. They remain explicit durable regression coverage in `docs/TESTING.md` and must be revisited when this operation or the shared raid framework changes.

## Files removed from r1

The five rejected transmitter-prototype files were removed before r2 and remain absent from the published state:

- `1.6/Defs/ThingDefs_Buildings/SG1_TokraDecoyTransmitter.xml`;
- `Languages/French/DefInjected/ThingDef/SG1_TokraDecoyTransmitter.xml`;
- `Source/GateRimSG1/Goauld/TokraDecoyTransmissionDefenseUtility.cs`;
- `docs/TOKRA_DECOY_TRANSMISSION_DEFENSE.md`;
- `docs/wiki/Tokra-Decoy-Transmission-Defense.md`.

No texture file was added or removed for the abandoned transmitter prototype. Revision r3 required no additional file deletion.

## Deliberate limits

- No definitive texture, explosive item or new mission object is added.
- The existing Ma'Tok structural-impact behavior supplies the breacher's wall-damaging weapon.
- The dedicated breacher is not added to normal storyteller Goa'uld raids.
- Accepted r1 transmitter development saves remain intentionally unsupported; r2 assault saves remain compatible with r3.

## Next step

Choose the next milestone after re-reading `docs/ROADMAP.md`, then create a dedicated branch explicitly from `v0.3.34-dev`. Re-read `docs/MILESTONE_PUBLICATION.md` before the next publication sequence.
