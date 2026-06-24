# Project state

Current milestone: `0.3.36-dev - Add non-lethal capture tools` — validated in final local revision `r9`, closed and published under `v0.3.36-dev`.

## Repository state

- Starting tag: `v0.3.35-dev`.
- Dedicated branch: `feature/non-lethal-capture-tools`.
- Published versions: `0.3.36-dev` and `0.3.36.0`.
- Final commit: `0.3.36-dev - add non-lethal capture tools`.
- Final annotated tag: `v0.3.36-dev`.
- The separate wiki was synchronized because player-facing pages were updated.

## Published scope

- Craftable single-use bolas.
- Experimental Tok'ra hypodermic rifle with five sealed charges.
- One charge consumed per launch and weapon destruction at zero.
- Separate weapon-accuracy and neutralization-resistance rolls.
- Body-size and armor resistance with XML clamps.
- Light real blunt impact: `3` for bolas and `1` for the dart.
- Temporary Tok'ra neuromuscular inhibition and a distinct bolas restraint, both leaving Consciousness intact and setting Moving to zero.
- Ordinary RimWorld downing and vanilla capture during the temporary window.
- Ordinary recovery after expiration when no other condition prevents movement.
- Charges shown in the ground inspect pane and in hover tooltips while equipped or carried in inventory.
- English/French text, debug tools, documentation and four provisional textures.

## Rejected prototypes

- Consciousness-zero neutralization was rejected after hostile targets died on downing.
- Persistent stun plus custom standing-target restraint was rejected because the standing pawn did not integrate cleanly with ordinary capture.

The final design removes the custom JobDef, float-menu patch, restraint ThingComp, stun HediffComp and custom carrying JobDriver introduced by the rejected prototype. No additional file or texture was removed in `r9`.

## Final validation

Final local revision `r9` confirms:

- bolas use a distinct physical-restraint presentation;
- the Tok'ra hypodermic rifle operates with its intended limited charges;
- successful neutralization sets Moving to zero without reducing Consciousness or introducing random downing deaths;
- the ordinary RimWorld capture order works;
- charges remain visible on the ground, while equipped and while carried in inventory;
- charges and inhibition duration persist through save/reload;
- the rifle disappears after its final charge;
- no additional cleanup of files or textures is required.

## Next milestone

The next planned milestone is `0.3.37-dev - Add Tok'ra Jaffa officer capture operation`. It must start explicitly from `v0.3.36-dev` on a dedicated branch after rereading the repository procedures. The operation should consume the validated non-lethal toolkit without making capture guaranteed or replacing the ordinary prisoner flow.
