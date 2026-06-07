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

### 0.1.11-dev — Recent Goa'uld implantation state

The mod now includes an XML-only medical-state prototype representing the critical period after a Goa'uld symbiote enters a humanoid host:

- `SG1_GoauldRecentImplantation`
- Visible remaining-time countdown
- Temporary duration of one in-game day
- Pain offset during the critical phase
- French `DefInjected` translations
- Updated technical documentation and player-wiki drafts

The Hediff can currently be added through developer mode. Automatic application, interruption and conversion into an active Goa'uld host remain later C# milestones.

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
