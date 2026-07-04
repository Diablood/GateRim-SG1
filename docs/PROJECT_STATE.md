# Project state

Current milestone: `0.3.62-dev - Add Goa'uld healing device prototype` -
validated and published from final local revision `r1`.

## Repository state

- Published branch: `feature/goauld-healing-device`.
- Published base: `v0.3.61-dev`.
- Last published version and tag: `0.3.62-dev` / `v0.3.62-dev`.
- Technical assembly version: `0.3.62.0`.
- Publication state: branch commit, annotated tag and separate wiki published.

## Implemented scope

- Add `SG1_GoauldHealingBracelet` as a portable medical device separate from
  the kara kesh, with its own component, cooldown and research project.
- Require persistent biological naquadah traces for activation.
- Restrict treatment to one adjacent living biological humanlike patient.
- Stabilize every bleeding injury at `80%` tending quality, then heal at most
  `20` total injury severity across the four highest-priority recent injuries.
- Reduce existing blood loss by at most `0.15` severity.
- Apply `SG1_GoauldHealingBraceletFatigue` for `12000` ticks and a persistent
  `30000`-tick device cooldown.
- Exclude permanent scars, missing parts, diseases, infections, cancers,
  chronic conditions, addictions, resurrection and multi-patient treatment.
- Equip generated Goa'uld System Lords once with both their existing kara kesh
  and this separate bracelet. Hostile AI uses the bracelet only on itself when
  seriously injured or bleeding.
- Raise System Lord `combatPower` from `550` to `625` for the extra survival
  tool and rare recoverable item.
- Keep the item absent from normal traders and random equipment. Local crafting
  requires `Goa'uld biotechnology` and `kara kesh`, then Crafting `12` at a
  machining table.
- Use a temporary texture on the stable bracelet path pending the global visual
  pass.

The kara kesh receives no healing action, medical state or healing-energy cost.
Its research is only one prerequisite for reverse-engineering the bracelet's
naquadah interface.

## Vanilla and lore reference

Biotech's local `Coagulate` Def is a touch ability that rapidly tends wounds.
The bracelet therefore uses adjacency and stabilizes all bleeding injuries,
but adds only limited real healing, fatigue and a long cooldown. The Stargate
healing-bracelet reference describes a naquadah-sensitive, concentration-driven
device whose repeated use causes severe fatigue. The sarcophagus remains a
separate future heavy technology and is not implemented here.

## Required in-game test

Load:

```text
Core
Harmony
Biotech
GateRim SG-1
```

1. Place two player colonists adjacent. Open exactly
   `Actions de débogage > GateRim SG-1 > Goa'uld... > Healing bracelet...`.
2. Run `Prepare healing-bracelet wearer` on the healer, then
   `Prepare bleeding patient` on the second colonist.
3. Select the healer. Confirm the visible gizmo
   `Utiliser le bracelet de guérison` / `Use healing bracelet`, then target the
   adjacent patient.
4. In the patient's Health tab, confirm all three cuts are tended, no more than
   `20` total severity was healed and blood loss fell from at least `30%` to
   about `15%`. No disease, scar or missing part must be restored.
5. In the healer's Health tab, confirm
   `épuisement du dispositif de guérison` / `healing-device exhaustion` with
   roughly `12000` ticks remaining. Run `Inspect healing-bracelet state` on the
   healer and confirm a cooldown close to `30000` ticks.
6. Attempt immediate reuse: the gizmo must remain disabled. Save, reload and
   confirm both fatigue and cooldown persist.
7. Open `Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...` and
   run `Spawn hostile System Lord with rank equipment`. Confirm both the kara
   kesh and healing bracelet are worn.
8. Return to `Healing bracelet...`, run `Prepare bleeding patient` on that
   Grand Master and unpause. Within about `60` ticks, it must heal itself once,
   receive fatigue and enter cooldown without repeatedly healing.
9. Check the research tab `GateRim SG-1`: `Dispositifs de guérison Goa'uld` /
   `Goa'uld healing devices` must require both `Biotechnologies Goa'uld` and
   `Kara kesh`.
10. Inspect `Player.log` for new XML, Def, apparel, Hediff, targeting, Scribe or
    C# errors.

## Optional regression checks

- A wearer without persistent naquadah traces can wear the bracelet but cannot
  activate it.
- Animal, mechanoid, dead pawn, healthy pawn, patient beyond adjacency or behind
  a solid wall: targeting refused.
- More than four injuries: all bleeding injuries are stabilized, but only four
  receive the limited healing budget.
- Permanent scar, disease, infection, cancer, addiction and missing part remain
  unchanged.
- Kara kesh shield, kinetic blast, neural attack and paralysis hold remain
  unchanged and consume no bracelet state.

## Validation state

- Forced local build `0.3.62.0`: passed with `0` errors; only the existing
  offline NuGet vulnerability-audit warning was emitted.
- In-game validation: maintainer confirmed the complete mandatory `r1`
  checklist, including treatment, fatigue, cooldown, hostile self-treatment,
  research visibility, save/reload and `Player.log`.

## Next action

Select the next small milestone from `docs/ROADMAP.md`. It must start from
`v0.3.62-dev` on a dedicated branch. The Odyssey and other optional-DLC
compatibility pass remains only in `docs/IDEAS_TO_REVISIT.md` until separately
discussed and approved.
