# Non-lethal capture tools

## Milestone

`0.3.36-dev - Add non-lethal capture tools`

Branch: `feature/non-lethal-capture-tools`

Starting tag: `v0.3.35-dev`

## Purpose

Future objectives that require a living target must not depend only on ordinary
combat downing. This milestone adds two reusable tools before any capture
mission is implemented:

- craftable single-use bolas;
- an experimental Tok'ra hypodermic rifle published with five sealed charges;
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

## Final rifle presentation

Since `0.3.97-dev`, the experimental Tok'ra hypodermic rifle uses a dedicated
simplified `128×128` final texture. The source sprite is horizontal, as expected
for RimWorld long guns. Its small variation when dropped is inherited from the
normal weapon ground-rotation behavior and is intentionally preserved.

The final silhouette favors strong outlines and broad cyan charge modules over
fine mechanical detail so the rifle remains readable at gameplay scale and
under stronger Camera+ zoom. This visual update does not change charge count,
accuracy, projectile behavior, neutralization, delivery or self-disposal.

## Experimental Tok'ra hypodermic rifle

- Spacer technology level.
- Not craftable, buyable or normally reloadable.
- Published `v0.3.36-dev`: five sealed charges.
- Published `0.3.37-dev`: twelve sealed charges for the mission-issued weapon.
- Every shot consumes one charge, including misses and resisted hits.
- The weapon destroys itself when the final charge is consumed.
- Range: `24.9` cells.
- Successful hits inflict `1` blunt damage.
- Base neutralization chance after a successful hit: `82%`.
- Successful neutralization lasts `720–1080` ticks.

In `v0.3.36-dev`, the rifle is obtainable only through the developer menu.
The `0.3.37-dev` officer-capture operation now provides one as a limited mission tool.

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

## Mandatory focused test — r9

1. Rebuild `0.3.36.0` and restart RimWorld.
2. Use a healthy hostile humanlike target and an available prisoner bed.
3. Confirm the displayed post-hit chances.
4. Give the shooter the Tok'ra rifle and confirm `5 / 5` both when selected on the ground and when hovered while equipped or carried in inventory.
5. Fire until `Neutralized` appears.
6. Confirm that Consciousness remains normal, Moving is zero, the target is downed and alive, and the ordinary Capture order is available.
7. Capture the pawn through the standard RimWorld command.
8. On another target, let the effect expire and confirm ordinary recovery.
9. Verify charge persistence and rifle destruction after the fifth shot.
10. Test bolas consumption and the same downing/capture flow; the Health tab must show a bolas restraint with entangled legs rather than neuromuscular numbness.
11. Save and reload during the Hediff.
12. Inspect `Player.log`.

## 0.3.37-dev integration note

Published `0.3.37-dev` keeps the twelve-charge reserve, preferred Tok'ra delivery point, vanilla temporary-site reformation and mission-only transfer restraint. The restraint protects the caravan journey, is removed on a player home map and returns only when the visible Tok'ra extraction team arrives to collect the detained officer. The pharmacological neutralization remains temporary and unchanged.

- The original `0.3.36-dev` milestone contains no capture mission; mission integration begins in `0.3.37-dev`.
- No automatic prisoner conversion is added.
- The communicator call does not delete the pawn; a Tok'ra carrier physically removes the target from the colony map.

## Deliberate limits

- The original `0.3.36-dev` milestone contains no capture mission; the mission integration begins in `0.3.37-dev`.
- Neutralization is not guaranteed.
- No automatic prisoner conversion is added.
- No normal rifle distribution or reloading source exists.
- Four provisional textures remain for the future visual pass.
