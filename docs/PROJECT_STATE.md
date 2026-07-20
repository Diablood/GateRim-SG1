# Project state

Current milestone: `0.3.102-dev - Finalize remaining non-directional visual families`

Status: validated in local revision `r10`; the final repository state is complete
for fast-forward integration into `develop` and the unique annotated tag
`v0.3.102-dev`.

- Starting point: published `develop` aligned with `v0.3.101-dev`.
- Working branch: `feature/final-nondirectional-visual-families`.
- Final local revision: `r10`.
- Assembly version: `0.3.102.0`.
- Final annotated tag: `v0.3.102-dev`.

## Implemented scope

- Finalize dedicated transparent map and inventory art for:
  - bolas and their projectile;
  - Ma'Tok staff and its energy bolt;
  - Zat'nik'tel and its blast;
  - the Tok'ra hypodermic energy dart.
- Make bolas reusable instead of consuming a one-charge weapon and use the valid
  vanilla `Bow_Small` cast sound.
- Keep the Tok'ra hypodermic rifle at twelve limited charges while replacing its
  firearm report with the validated `Shot_ChargeRifle` energy sound.
- Set the Ma'Tok projectile speed to the validated value `80`.
- Finalize non-directional Jaffa ground and inventory art for:
  - light and heavy standard armor;
  - officer armor;
  - standard and officer deployed helmets;
  - gauntlets and reinforced boots;
  - under-armor clothing, trousers and armor belt.
- Add real ThingDefs and French translations for the Jaffa under-armor clothing,
  trousers and armor belt.
- Keep `drawSize = 0.75` for gauntlets, reinforced boots and armor belt.
- Keep all existing directional worn variants outside this milestone.

- Add protected byte-identical wiki copies for every finalized gameplay PNG in
  this milestone and publish the accepted Jaffa outfit concept art as a
  documentation-only reference.

## Validated results

- Every finalized weapon, projectile and Jaffa item is readable on the map and in
  inventories with genuine exterior transparency.
- No generated checkerboard remains in the validated PNG files.
- Bolas can be thrown repeatedly and no longer disappear after one use.
- Zat'nik'tel, Ma'Tok and Tok'ra hypodermic projectiles render correctly.
- Ma'Tok flight speed `80` is accepted in play.
- The Tok'ra hypodermic rifle uses the intended energy-shot report.
- Standard and officer Jaffa ground icons remain visually distinct.
- The under-armor clothing, trousers and armor belt can coexist with the modular
  armor set.
- Gauntlets, boots and belt use the validated reduced ground scale.
- Save and reload preserve the newly added apparel.
- `JaffaLightArmor` remains a distinct armor item and is not replaced by the
  textile under-armor clothing.
- Directional worn art was not reopened.

## Deferred work

- Produce and validate the directional worn variants for visible Jaffa armor and
  under-armor families.
- Integrate the new clothing and belt into Jaffa world-generation and mission
  loadouts.
- Apply the officer armor and deployed helmet consistently to officer pawns,
  including the living-capture operation.
- Refactor the deployed/retracted helmet implementation without breaking existing
  saves or the standard/officer pair isolation.

## Publication state

The final commit is:

```text
0.3.102-dev - Finalize remaining non-directional visual families
```

It is integrated into `develop` by fast-forward and tagged once as
`v0.3.102-dev`. No separate wiki synchronization is required because this
milestone changes no file under `docs/wiki/`.