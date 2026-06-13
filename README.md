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
### 0.2.6-dev — Add Free Jaffa peaceful visitors baseline

Free Jaffa communities can now appear naturally as rare peaceful visitors.

The new incident:

```text
SG1_FreeJaffaPeacefulVisitors
```

reuses RimWorld's vanilla peaceful visitor-group workflow.

It explicitly selects one existing visible Free Jaffa world faction that is
still non-hostile toward the player. It does not create a hidden runtime
fallback faction for old saves.

Natural parameters:

```text
earliest day: 10
base chance: 0.14
minimum refire delay: 20 days
group size target: 2 to 4 armed visitors
```

The faction receives a dedicated `Peaceful` pawn-group profile:

```text
Free Jaffa warrior: weight 4
Free Jaffa guard:   weight 1
```

Visitors therefore reuse the validated Free Jaffa identity:

```text
Jaffa lineage
initial Prim'ta
Ma'Tok
modular armor
retractable helmet
Free Jaffa cultural backstories
no forced Goa'uld forehead mark
```

The baseline remains intentionally narrow. The visitors are not traders and
the milestone does not yet add quests, gifts, military aid, recruitment or
custom diplomatic interactions.

## Next development focus

- validate a developer-triggered peaceful Free Jaffa visit;
- validate storyteller selection after day 10;
- confirm non-hostile world-faction selection with multiple communities;
- confirm hostile Free Jaffa factions are excluded;
- observe the armed visitor presentation before adding civilian or diplomatic
  profiles later.

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
- [x] Persistent Goa'uld host-caste baseline
- [x] Cultural backstory baseline
- [x] Contextual social baseline
- [x] Free Jaffa peaceful visitors baseline

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
