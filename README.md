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
### 0.2.5-dev — Add contextual social baseline

The first lightweight social layer is now in place.

It adds three situational opinion modifiers:

```text
Free Jaffa -> active Goa'uld host
    distrusts a Goa'uld: -30 opinion

active Tok'ra host -> active Goa'uld host
    sees a Goa'uld enemy: -40 opinion

Free Jaffa -> Goa'uld-marked Jaffa
    wary of a Goa'uld-marked Jaffa: -8 opinion
```

It also adds one local mood thought:

```text
Goa'uld-domain Jaffa within 12 cells of the same faction's active System Lord
    under a System Lord's gaze: +2 mood
```

The small positive mood value represents imposed composure and rigid
discipline rather than genuine happiness.

The logic is centralized in:

```text
GateRimSG1.Social.ContextualSocialIdentityUtility
```

The utility separates:

```text
inherited Jaffa physiology
current faction allegiance
persistent Free Jaffa background from PawnKindDef
adult-symbiote origin
intrinsic forehead marks
actual System Lord host profile
```

The baseline deliberately avoids automatic attacks, forced permanent
relationships and absolute social restrictions.

## Next development focus

- validate each opinion modifier with developer-spawned test pawns;
- validate the local System Lord proximity thought;
- confirm save and reload behavior;
- refine social effects incrementally after gameplay observation;
- keep optional Ideology integration as a later layer.

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
