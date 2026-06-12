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
### 0.1.75-dev — Consolidate debug tools and player-facing diagnostics

GateRim SG-1 now exposes a dedicated mod setting:

```text
Show advanced GateRim SG-1 debug information
```

The option is disabled by default. RimWorld developer mode still forces
advanced diagnostics on automatically.

Normal gameplay inspection panels no longer display raw persistent symbiote
IDs or raw autonomous-hunt cooldown ticks. The Goa'uld queen extraction
cooldown in raw ticks is also hidden outside advanced diagnostics.

Temporary Tok'ra therapeutic offers remain readable during normal gameplay,
but show only the trust tier. The underlying numeric trust score remains
available through advanced diagnostics.

Routine `GR_Log.Message(...)` lifecycle traces are now written directly to
`Player.log` only while advanced diagnostics are visible. They bypass
RimWorld's in-game log queue so informational traces cannot open an
error-looking popup. Warnings and errors always remain visible through the
normal RimWorld log channel.

Developer-only prototype commands remain tied to RimWorld developer mode.
Gameplay mechanics validated through `0.1.74-dev` are unchanged.

## Next maintenance focus

- generate the native French translation report for the five remaining load
  warnings before changing any translation blindly;
- continue the conditional-gizmo audit only where a player-facing command is
  actually misleading or unnecessarily exposed;
- keep the next gameplay expansion separate from this maintenance pass.

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
