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
### 0.2.2-dev-r2 — Expand faction limits and generate provisional leaders

The second visible Stargate world presence is now enabled:

```text
SG1_FreeJaffa
```

New worlds generate one visible neutral faction:

```text
Free Jaffa
```

The faction represents independent Jaffa communities freed from Goa'uld
domination. It uses a reduced settlement-generation weight so its colonies
remain visible but limited.

The baseline adds:

```text
one configurable Free Jaffa faction by default
neutral initial relation with the SGC expedition
hostility toward Goa'uld domains through the Goa'uld permanent-enemy rule
limited world settlements
Combat pawn-group profile
Settlement defense pawn-group profile
```

Two new pawn kinds support the faction:

```text
SG1_FreeJaffaWarrior
SG1_FreeJaffaGuard
```

They reuse the validated Jaffa lineage, one-time Prim'ta provisioning, Ma'Tok
weapon tag and modular armor loadouts.

A visual-identity correction is included: automatic forehead marks now apply
only to Jaffa whose faction carries a Goa'uld System Lord-domain extension.
Free Jaffa remain unmarked unless a mark is assigned manually or migrated from
an older save.

Trade, quests, aid, visitors and natural Free Jaffa raids remain disabled for
this first world-presence milestone.

The `r1` alignment fix adds faction-level xenotype summaries:

```text
Free Jaffa                    -> Jaffa: 100%
Goa'uld System Lord domains  -> Jaffa: 100% for the current servant baseline
```

The Goa'uld summary is intentionally provisional. True Goa'uld host profiles
remain a separate milestone because a xenotype alone would not create the
persistent implanted symbiote identity required by the existing mechanics.

The `r2` fix keeps one faction of each type by default while allowing players
to add additional Goa'uld-domain or Free Jaffa factions manually from Create
World. It also gives both visible humanlike factions a generated provisional
leader so RimWorld no longer logs a missing faction leader.

Until true persistent Goa'uld hosts exist, each generated Goa'uld-domain
faction is represented by a senior Jaffa commander rather than a fake
incomplete System Lord host.

## Next development focus

- validate one visible neutral Free Jaffa faction in world creation;
- validate limited Free Jaffa settlements and neutral SGC relations;
- visit or attack a test settlement and confirm Free Jaffa defenders;
- confirm Prim'ta, Ma'Tok, modular armor and absence of forced forehead marks;
- add peaceful Free Jaffa encounters in a separate balancing milestone.

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
- [x] Playable Goa'uld world-faction baseline
- [x] Free Jaffa world-faction baseline

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
