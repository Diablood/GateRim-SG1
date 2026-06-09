# GateRim SG-1

A Stargate SG-1 mod project for RimWorld 1.6.

## Project identity

- Public name: `GateRim SG-1`
- Author: `Diablood`
- Package ID: `diablood.gaterimsg1`
- C# namespace: `GateRimSG1`
- Repository: `https://github.com/Diablood/GateRim-SG1`
- Player wiki: `https://github.com/Diablood/GateRim-SG1/wiki`
- Required DLC for the current development branch: `Biotech`

## Current milestone
### 0.1.56-dev — Add trusted Tok'ra advanced medicine support

Trusted Tok'ra medical-support deliveries now include one physical unit of
vanilla ultratech medicine in addition to their existing tretonin shipment.
Cooperative deliveries remain unchanged with two tretonin doses and one
visitor, while trusted deliveries keep four tretonin doses and two visitors.

The additional medicine is intentionally limited to the trusted tier and to
independent deliveries. It provides a concrete positive diplomatic reward
without introducing traders, quests or a new interface yet.

## Next maintenance focus

Before the next gameplay expansion, the project will perform a consolidation pass:

- fix the remaining French translation-report errors;
- audit player-facing information, debug-only diagnostics and conditional gizmos;
- prepare a mod-specific debug option before the `0.2.x` Stargate chapter;
- preserve the future wiki-image and Workshop-asset plan without claiming final visuals yet.

## First playable milestone

- [x] Inheritable Jaffa xenotype foundation
- [x] Separate inherited Jaffa lineage from Prim'ta effects
- [x] Goa'uld host xenotype prototype
- [x] Free Goa'uld symbiote pawn prototype
- [x] Recent Goa'uld implantation Hediff prototype
- [x] Automatic Prim'ta workflow
- [x] Forced Goa'uld implantation
- [x] Ritual Goa'uld implantation
- [x] Host conversion after the critical phase
- [ ] Goa'uld faction
- [ ] Jaffa pawn kinds
- [ ] Ma'Tok staff weapon
- [ ] Zat'nik'tel
- [ ] Generic Jaffa armor
- [ ] Generic human SG-team uniform
- [ ] SG tactical boots
- [ ] SG tactical gloves

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Keep English in `Defs`.
- Add French `DefInjected` translations as soon as a content batch is stabilized.
- Use bilingual `Keyed` files for future UI messages and C# strings.
- Keep versioned player-wiki drafts under `docs/wiki/`.
- Publish wiki pages directly at the root of the separate `GateRim-SG1.wiki` repository.
- Use `./tools/sync-wiki.sh` from Bash or `.\tools\sync-wiki.cmd` from Windows PowerShell.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
