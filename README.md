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

## Initial development branch

```text
feature/jaffa-goauld-foundation
```

## Current milestone

### 0.1.12-dev — Jaffa germline correction

The Jaffa xenotype foundation is corrected after clarifying the Biotech inheritance model:

- `SG1_Jaffa` is now inheritable
- Jaffa genes are treated as a germline/endogene foundation
- Two Jaffa parents can produce Jaffa children instead of baseliner children
- Goa'uld host traits remain non-heritable because possession is acquired during life
- Symbiote-dependent Jaffa bonuses will be separated into a dedicated Hediff later
- Player documentation now explains the difference between germline genes and acquired xenogenes

The previously published `0.1.11-dev` implantation prototype remains unchanged:

- `SG1_GoauldRecentImplantation`
- Visible one-day countdown
- Temporary pain offset

## First playable milestone

- [x] Jaffa xenotype foundation
- [x] Goa'uld host xenotype prototype
- [x] Free Goa'uld symbiote pawn prototype
- [x] Recent Goa'uld implantation Hediff prototype
- [ ] Forced implantation
- [ ] Ritual implantation
- [ ] Medical interruption
- [ ] Host conversion after the critical phase
- [ ] Host transfer
- [ ] Goa'uld faction
- [ ] Jaffa pawn kinds
- [ ] Ma'Tok staff weapon
- [ ] Zat'nik'tel
- [ ] Generic Jaffa armor

## Development notes

- Keep indentation at 4 spaces.
- Preserve `About/ModIcon.png`.
- Develop incrementally and test after each small content batch.
- Keep English in `Defs`.
- Add French `DefInjected` translations as soon as a content batch is stabilized.
- Use bilingual `Keyed` files for future UI messages and C# strings.
- Keep versioned player-wiki drafts under `docs/wiki/`.
- Publish wiki pages directly at the root of the separate `GateRim-SG1.wiki` repository.
- Use `./tools/sync-wiki.sh` from Bash or `.\tools\sync-wiki.cmd` from Windows PowerShell to synchronize wiki drafts safely.
- Use dedicated branches for functional changes and important fixes.
- Create annotated Git tags for versioned milestones.
