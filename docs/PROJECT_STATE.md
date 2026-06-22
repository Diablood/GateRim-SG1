# Project state

Current milestone: `0.3.30-dev - Add Tok'ra temporary-base delivery mission` — validated locally on final revision `r9` and published under the final tag `v0.3.30-dev`.

## Published base

- Starting tag: `v0.3.29-dev`.
- Dedicated branch: `feature/tokra-temporary-base-delivery`.
- Final local revision: `0.3.30-dev-r9`.
- Final published tag: `v0.3.30-dev`.
- Assembly version: `0.3.30.0`.
- Cultural backstory count remains `83`.

## Final milestone outcome

This milestone adds a sixth recurrent Tok'ra organic operation. The colony receives a production contract limited to ordinary goods it can reasonably manufacture, loads conforming items into a caravan and delivers them to a concealed temporary rendezvous through RimWorld's normal world-travel interactions.

The shared GateRim mission manager remains authoritative for selection, timing, persistence, trust, recurrence and text variation. Specialized adapters handle the temporary WorldObject, cargo inspection, caravan handoff and hostile temporary maps without duplicating the operation as a second vanilla Quest lifecycle.

## Validated delivery flow

- Select only contracts supported by an available recipe, completed research, required DLC or mod content, an existing or usable worktable and at least one capable colon.
- Persist the selected ThingDef, quantity, minimum quality, minimum condition, deadlines, threat snapshot and complication state.
- Create a temporary Tok'ra rendezvous `6–16` tiles away and preserve free caravan routing.
- Leave arrival non-destructive: the stationary caravan exposes `Hand over the requested goods` / `Remettre la commande` and consumes exactly the conforming quantity.
- Keep additional cargo and battlefield loot untouched.
- Grant `+2` trust for an on-time delivery, `+1` during the configurable two-day grace period and `-1` only after final expiry.
- Allow one optional ordinary Goa'uld interception during travel for a complete shipment moving to the exact rendezvous.
- Allow one mutually exclusive Goa'uld ambush on the final approach when the caravan's next tile is the rendezvous.
- Reuse RimWorld's temporary ambush map, real caravan inventory and complete vanilla reformation dialog so the player may recover enemy weapons, apparel and other map items.
- Return the reformed caravan to the adjacent approach tile, then allow the final one-tile journey and normal handoff.
- Never grant trust or complete the operation for military victory alone; the surviving conforming shipment must physically reach the Tok'ra.
- Track queued map creation asynchronously and mark the route secure only after the hostile temporary map has appeared and then disappeared.
- Preserve all active-state and anti-duplication data through save/reload.

## Validation status

- Local revision `r9` compiled and loaded successfully.
- The complete normal, late, ordinary-interception and final-approach flows were validated in game.
- A naturally selected interception occurred without developer forcing, confirming that the configured organic complication path is reachable in normal play.
- Combat, cargo loss, complete battlefield-loot selection, vanilla caravan reformation and the final-tile delivery were validated.
- The queued encounter no longer emits the unidentified-world-object error and no premature route-secure message occurs.
- Save/reload, single-result guarantees, trust consequences, adaptive threat consumption, recurrence, anti-repetition and previous Tok'ra-operation regressions are covered by the final milestone test record.
- Final `Player.log` was reported clean for the validated flow.

## Deferred work

- A visible Tok'ra handoff scene on a dedicated generated map remains optional and should only be added if longer playtests show that the current world-map handoff is too abstract.
- Additional product families remain deferred until the accessibility filter has been exercised across more colony progress levels.
- The unique Tok'ra introduction mission, key artifact, dedicated research and communicator-gated recurrent pool remain a separate future arc.

## Next development step

No new milestone or branch is opened by this publication. The next milestone must start explicitly from `v0.3.30-dev` on a dedicated branch after reviewing `docs/ROADMAP.md`. The Tok'ra introduction arc is a documented candidate, not an automatically selected next task.

## Publication

The main repository branch and final annotated tag use the version without a local `-rN` suffix. The updated `docs/wiki/` pages must be synchronized to the separate `GateRim-SG1.wiki` repository as part of this publication.
