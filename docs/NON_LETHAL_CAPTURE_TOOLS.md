# Non-lethal capture tools

## Milestone

`0.3.36-dev - Add non-lethal capture tools`

Branch: `feature/non-lethal-capture-tools`

Starting tag: `v0.3.35-dev`

Published tag: `v0.3.36-dev`

## Purpose

Future objectives that require a living target must not depend only on ordinary
combat downing. This milestone adds two reusable tools before any capture
mission is implemented:

- craftable single-use bolas;
- an experimental Tok'ra hypodermic rifle with five sealed charges;
- XML-driven neutralization profiles shared by both tools;
- distinct temporary effects for Tok'ra neuromuscular inhibition and bolas restraint;
- ordinary RimWorld downing and capture after a successful effect.

Weapon accuracy is resolved normally, followed by a separate neutralization roll
modified by body size and armor.

## Bolas

- Neolithic technology level.
- Craftable at a crafting spot or vanilla smithy.
- Cost: `15` cloth and `8` steel.
- Range: `11.9` cells.
- One integrated throw, consumed even on a miss.
- Successful hits inflict `3` blunt damage.
- Base neutralization chance after a successful hit: `66%`.
- Successful neutralization lasts `600–900` ticks.
- The health effect is shown as a temporary bolas restraint with entangled legs, not as drug-induced numbness.

## Experimental Tok'ra hypodermic rifle

- Spacer technology level.
- Not craftable, buyable or normally reloadable.
- Five sealed charges.
- Every shot consumes one charge, including misses and resisted hits.
- The weapon destroys itself after the fifth shot.
- Range: `24.9` cells.
- Successful hits inflict `1` blunt damage.
- Base neutralization chance after a successful hit: `82%`.
- Successful neutralization lasts `720–1080` ticks.

For this milestone, the rifle is obtainable only through the developer menu.
A later Tok'ra operation may provide it as a rare mission tool.

## Temporary incapacitation effects

A successful Tok'ra dart applies `SG1_NonLethalNeutralization`. A successful bolas impact applies `SG1_BolasRestraint`.

Both Hediffs leave Consciousness intact but set Moving to zero. The Tok'ra dart represents temporary neuromuscular inhibition; the bolas represent weighted cords physically entangling the legs. RimWorld therefore places the target in its ordinary downed state, making the standard capture order available.

The Hediff is added before the projectile resolves its deliberately light blunt
impact. The incapacity is therefore caused by the medical effect rather than by
the external-violence DamageInfo attached to the projectile.

When the Hediff expires, a pawn with no other incapacitating condition uses the
ordinary RimWorld recovery path and stands again. Repeated successful effects
refresh one existing Hediff instead of stacking copies.

The light impact remains real damage and can still endanger a critically injured
target.

## Removed r7 prototype

The standing-target stun and custom `Restrain` job were rejected after live
testing because a stunned standing pawn was not compatible with ordinary capture.

The final design removes:

- the custom restraint JobDef;
- the human ThingDef float-menu patch;
- the custom restraint ThingComp;
- the persistent-stun HediffComp;
- the custom carrying JobDriver.

No texture is removed.

## Save data

- `CompConsumableCharges` stores rifle charges and exposes them in both the inspect pane and item tooltips;
- `HediffComp_Disappears` stores the inhibition duration;
- ordinary RimWorld health state and capture jobs handle the downed target;
- no new GameComponent is introduced.

## Developer menu

```text
Debug actions menu
→ GateRim SG-1
→ Equipment...
```

Actions:

```text
Show target neutralization chances
Give bolas
Give Tok'ra hypodermic rifle
Clear temporary neutralization
```

## Final validation — r9

Final local validation confirms:

- bolas are consumed after one throw and use the distinct `SG1_BolasRestraint` effect;
- the Tok'ra rifle starts with five sealed charges and consumes one per shot;
- charge counts are visible on the ground, while equipped and while carried in inventory;
- charge counts persist through save/reload;
- the rifle destroys itself immediately after its final shot;
- successful effects leave Consciousness intact and set Moving to zero;
- ordinary RimWorld downing exposes the standard Capture order;
- captured targets follow the ordinary prisoner-bed flow;
- uncaptured targets recover normally when the temporary effect expires;
- repeated successes refresh one Hediff instead of stacking copies;
- the final revision adds no further file or texture deletion.

## Deliberate limits

- No capture mission is included.
- Neutralization is not guaranteed.
- No automatic prisoner conversion is added.
- No normal rifle distribution or reloading source exists.
- Four provisional textures remain for the future visual pass.
