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
### 0.2.1-dev-r3 — Polish world-faction presentation

The first hostile Stargate world presence is now enabled:

```text
SG1_GoauldSystemLordPrototype
```

New worlds generate one visible faction representing several Goa'uld System
Lord domains as a practical RimWorld abstraction. Its reduced settlement
weight keeps the presence visible but limited.

The `r1` startup fix removed two invalid `FactionDef` icon fields.

The `r2` fix adds the RimWorld 1.6 configurable-faction fields used by the
Create World page:

```text
maxConfigurableAtWorldCreation = 1
startingCountAtWorldCreation = 1
```

The Goa'uld faction now appears once by default in world creation, cannot be
duplicated through the Add faction menu, and uses the valid
`factionIconPath` / `settlementTexturePath` fields for world presentation.

The same correction also removes unnecessary `pawn` jargon from French
player-facing strings and wiki pages.

The `r3` presentation fix capitalizes the French Goa'uld faction name and
adds explicit vanilla-style `Town` / `DefaultSettlement` icon paths to the
custom `SGC expedition` player faction so its UI icon no longer falls back to
a missing-texture placeholder.

The baseline adds:

```text
visible Goa'uld System Lord domains
permanent hostility
limited world settlements
Combat pawn-group profile
Settlement defense pawn-group profile
rare natural direct-assault Jaffa raids
```

Natural raids use a dedicated low-frequency incident:

```text
SG1_GoauldJaffaNaturalRaid
```

The incident reuses the validated direct-assault path:

```text
ImmediateAttack
canSteal = false
canKidnap = false
```

Generic vanilla faction raid selection remains blocked deliberately. Natural
abduction and destruction doctrines remain disabled until they receive a
separate balancing pass.

The existing developer incidents remain available for controlled regression
tests.

## Next development focus

- validate a newly generated world with visible hostile Goa'uld settlements;
- validate one natural or developer-triggered natural direct-assault raid;
- confirm recoverable Ma'Tok loot after combat;
- add the Free Jaffa world-faction baseline next.

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
- [x] SG-team field uniform prototype
- [x] SG tactical boots prototype
- [x] SG tactical gloves prototype
- [x] SG tactical vest prototype
- [x] Black and desert SG-team uniform variants
- [x] Stranded SG-team starter scenario

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
