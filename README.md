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
### 0.1.77-dev — Add Zat'nik'tel incapacitation prototype

The first playable Zat'nik'tel sidearm is now available as a craftable test
weapon.

Its first-shot prototype deliberately models only non-lethal disruption:

```text
all direct targets     -> 10 Stun
non-organic pawn       -> additional 10 EMP
building or turret     -> additional 8 EMP
```

The primary effect neutralizes biological targets temporarily without adding a
physical injury. The weak EMP follow-up keeps the weapon relevant against
vanilla mechanical targets and compatible structures.

The weapon and projectile reuse the declarative `Goauld` energy-technology
extension introduced for the Ma'Tok. Future Replicator resistance can query
that shared classification.

The lethal second shot, disintegrating third shot, automatic Jaffa loadouts
and complex deployment visuals remain deferred until the basic control role is
validated.

Natural Goa'uld raids, settlements and traders remain disabled.

## Next development focus

- validate biological incapacitation duration and capture utility;
- validate weak disruption against vanilla mechanoids and turrets;
- tune range, cooldown and EMP values only after hands-on testing;
- design the persistent second-shot state as a separate milestone.

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
- [x] Goa'uld System Lord faction foundation
- [x] Goa'uld-aligned Jaffa pawn kinds
- [x] Automatic initial Prim'ta for Goa'uld-aligned Jaffa
- [x] Ma'Tok staff weapon prototype
- [x] Automatic Ma'Tok loadout for Goa'uld Jaffa
- [x] Zat'nik'tel first-shot incapacitation prototype
- [x] Modular Jaffa armor prototypes
- [x] Retractable Jaffa helmet modes
- [x] Automatic Jaffa armor loadouts
- [ ] Generic human SG-team uniform
- [ ] SG tactical boots
- [ ] SG tactical gloves

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Include code, technical documentation and wiki drafts in the first ZIP of each milestone.
- Keep manifests outside ZIP archives.
- Keep English in `Defs`.
- Add French `DefInjected` translations as soon as a content batch is stabilized.
- Use bilingual `Keyed` files for future UI messages and C# strings.
- Keep versioned player-wiki drafts under `docs/wiki/`.
- Publish wiki pages directly at the root of the separate `GateRim-SG1.wiki` repository.
- Use `./tools/sync-wiki.sh` from Bash or `.\tools\sync-wiki.cmd` from Windows PowerShell.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
