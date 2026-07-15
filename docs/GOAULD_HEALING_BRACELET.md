# Goa'uld healing bracelet

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

## Final visual

`0.3.94-dev` replaces the temporary Zat'nik'tel reuse at the existing
`Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet`
path. The accepted transparent `128×128` item art shows a gold-and-silver
circular medical device around an orange luminous core. It is validated on the
ground, in inventory, in inspection and equipped. No Def, component, assignment,
research, recipe, balance or save state changes.

A byte-identical player-wiki reference is protected by the visual checker at
`docs/wiki/images/GoauldHealingBracelet.png`.

## Validation

Final local functional revision `r1` of `0.3.62-dev` passed the original
medical checklist. The `0.3.94-dev` visual revision also passed focused in-game
presentation, unchanged treatment, hostile AI and save/reload checks. Durable
behavior coverage remains in `docs/TESTING.md`; final art drift is checked by
`tools/check-visual-assets.ps1`.
