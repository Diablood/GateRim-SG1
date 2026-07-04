# Goa'uld healing bracelet prototype

## Boundary

`0.3.62-dev` adds a medical bracelet as a device separate from the kara kesh.
It has its own `ThingComp`, cooldown, Hediff, research and debug menu. It never
reads or consumes kara kesh shield energy and adds no kara kesh gizmo.

## Treatment contract

- Operator: living, capable wearer with persistent biological naquadah traces.
- Patient: one living biological humanlike on the same map, within `1.9` cells
  and line of sight; self-treatment is allowed.
- Stabilization: every bleeding, currently tendable injury receives `0.8`
  tending quality.
- Healing: `20` severity maximum across up to four non-permanent injuries,
  ordered by bleed rate and then severity.
- Blood loss: current `BloodLoss` severity decreases by at most `0.15`.
- Cost: `SG1_GoauldHealingBraceletFatigue` for `12000` ticks and a serialized
  `30000`-tick device cooldown.

Permanent injuries, missing parts, added parts, diseases, infections, cancers,
chronic conditions, addictions and death are outside this treatment contract.

## AI and acquisition

Generated System Lords receive the bracelet once through
`GameComponent_GoauldHostCasteInitializer`. Non-player wearers only treat
themselves when bleed rate reaches `0.2` or summary health falls to `55%`, then
the normal cooldown prevents loops.

The item has no normal tradeability or random commonality. Captured devices are
usable immediately by biologically eligible wearers. Local production requires
both `SG1_GoauldBiotechnology` and `SG1_KaraKeshResearch`, Crafting `12`, four
advanced components, `60` plasteel and `40` gold at a machining table.

## Validation

Final local revision `r1` passed the forced `0.3.62.0` build and the complete
mandatory in-game checklist. Treatment, fatigue, cooldown, hostile AI,
save/reload, research visibility and `Player.log` were confirmed by the
maintainer. Exact steps remain in `docs/PROJECT_STATE.md` and
`docs/TESTING_CURRENT.md`; durable coverage is recorded in `docs/TESTING.md`.
The final revision was committed, tagged as `v0.3.62-dev` and published with
the synchronized separate wiki.
