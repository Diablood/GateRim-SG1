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
### 0.1.76-dev — Add Ma'Tok plasma-impact damage prototype

The Ma'Tok staff remains primarily a Goa'uld plasma weapon:

```text
24 burn damage
0.28 base armor penetration
```

Its projectile now adds a deliberately reduced structural follow-up only when
it hits:

```text
non-organic pawn  -> 8 blunt damage
building          -> 12 blunt damage
```

Organic pawns do not receive the secondary impact, preventing an unintended
anti-personnel damage increase. The prototype does not add an area explosion.

The staff weapon and projectile also expose a reusable `Goauld` energy-
technology extension. It has no resistance effect yet, but prepares a shared
hook for future Replicator behavior without hard-coding one exception per
weapon.

Natural Goa'uld raids, settlements and traders remain disabled.

## Next development focus

- validate Ma'Tok damage against biological pawns, mechanoids and structures;
- keep future Replicator resistance separate from this vanilla-target pass;
- continue with the first Zat'nik'tel prototype after validation.

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
- [ ] Zat'nik'tel
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
