# Project state

Current milestone: `0.3.103-dev - Add Jaffa knife`

Status: gameplay, loadout and visual presentation validated in local revision
`r4`; the final repository state is complete for fast-forward integration into
`develop` and the unique annotated tag `v0.3.103-dev`.

- Starting point: published `develop` aligned with `v0.3.102-dev`.
- Working branch: `feature/jaffa-knife`.
- Final local revision: `r4`.
- Assembly version: `0.3.103.0`.
- Final annotated tag: `v0.3.103-dev`.

## Implemented scope

- Add `SG1_JaffaKnife` as a quality-bearing melee weapon using
  `BaseMeleeWeapon_Sharp_Quality` and `Graphic_Single`.
- Match the vanilla gladius melee profile:
  - mass `0.85`;
  - handle power `9` blunt;
  - point power `16` stab;
  - edge power `16` cut;
  - `2` seconds cooldown for every attack type;
  - `12000` fabrication work.
- Require `30` steel, `5` plasteel, Crafting `4`, the machining table and the
  existing `SG1_JaffaWeaponry` research for local fabrication.
- Allow Free Jaffa clan-supply convoys to carry `0~2` completed knives.
- Extend Goa'uld-aligned and Free Jaffa weapon selections so warriors, guards,
  officers and traders can use the knife as a primary weapon.
- Preserve the Tok'ra diversion breacher as Ma'Tok-only through an explicit
  inherited-list override.
- Finalize the maintainer-retouched transparent `128×128` knife texture with a
  stronger outline and validated `drawSize = 0.65`.
- Add French text, durable testing, player-wiki coverage, a protected wiki image
  and visual-checker enforcement.

## Validated results

- The knife spawns, stores, equips, attacks and survives save/reload correctly.
- Its displayed combat values match the intended gladius-equivalent profile.
- `drawSize = 0.65` gives an accepted map and equipped scale.
- Goa'uld-aligned Jaffa bases, raids and mission groups generate a mixture of
  ranged weapons and knives through their shared PawnKinds.
- Free Jaffa warriors, guards and caravan traders can also receive the knife.
- The mission-only breacher continues to generate with a Ma'Tok staff.
- The final outlined texture is accepted by the maintainer and keeps genuine
  exterior transparency without a baked checkerboard.
- Existing DefNames, faction groups, combat budgets, armor assignments and save
  identifiers remain stable.

## Deferred work

- Produce and validate directional worn variants for visible Jaffa armor and
  under-armor families.
- Integrate the separate under-armor, trousers and belt into complete Jaffa
  world-generation and mission outfits.
- Finalize the multidirectional mobile Goa'uld, Tok'ra and queen symbiote forms.
- Refactor deployed and retracted helmet states without breaking existing saves.

## Publication state

The final commit is:

```text
0.3.103-dev - Add Jaffa knife
```

The final annotated tag is `v0.3.103-dev`. The player wiki source changes and
new protected image require synchronization with the separate wiki repository.
