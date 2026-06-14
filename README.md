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
### 0.2.17-dev — Add enterable hidden Tok'ra safehouse site

A stored Tok'ra safehouse lead can now reveal one temporary site that a
caravan can visit.

```text
1 stored safehouse lead
    -> consumed by SG1_TokraHiddenSafehouseSiteIncident
    -> one non-hostile vanilla site near the colony
    -> one small generated map
    -> 2 tretonin doses and 4 industrial medicine
    -> automatic expiration after 10 RimWorld days if unvisited
```

The site deliberately creates no trader, recruitment, permanent settlement,
military aid, hostile pawn or raid. A marker and a site cannot coexist.

A forced C# rebuild is required for this milestone.

## Next development focus

- validate site creation, caravan travel, map generation, reward contents,
  save persistence, cleanup and expiration;
- keep Tok'ra contacts, trading and recruitment for later milestones.

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
- [x] Hidden Tok'ra world-presence baseline
- [x] Stargate crafting-research baseline
- [x] Natural Goa'uld queen acquisition baseline

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


#### Hidden Tok'ra cell cache

A rare non-territorial Tok'ra cell contact can now leave a modest medical cache
near a reachable map edge:

```text
1-2 tretonin dose(s)
2-3 industrial medicine unit(s)
```

The event uses the persistent hidden `SG1_Tokra` faction and remains
deliberately limited:

```text
no Tok'ra settlement
no world site yet
no trader
no recruitment
no military aid
no raid
```

The cache is available only when Tok'ra trust is not wary. This gives the
hidden world presence a first tangible footprint without changing Tok'ra into a
territorial faction.


#### Tok'ra safehouse signal

A rare clandestine Tok'ra contact can now transmit an encrypted safehouse
signal. This is a letter-only event that adds a tiny `+1` Tok'ra trust gain
without creating a world site yet.

The event deliberately creates no settlement, caravan, visitor group, trader,
recruitment, loot, military aid or raid. It prepares the design space for a
future hidden Tok'ra safehouse or world-site prototype.


#### Tok'ra safehouse leads

The safehouse signal now stores a persistent Tok'ra safehouse lead:

```text
+1 Tok'ra trust
+1 safehouse lead
maximum: 3 leads
```

Leads do not create a world site yet. They are a saved progression bridge for a
future hidden Tok'ra world-site or safehouse prototype.


#### Tok'ra safehouse lead cache

Stored safehouse leads can now be consumed by a rare follow-up cache incident:

```text
-1 safehouse lead
2 tretonin doses
3 industrial medicine
```

The event still creates no world site, generated map, caravan, pawn, trader,
recruitment, military aid or raid. It proves lead persistence and consumption
before the future hidden safehouse world-site prototype.


#### Tok'ra hidden safehouse world marker

Stored safehouse leads can now become a temporary world-map marker:

```text
-1 safehouse lead
1 temporary non-hostile world marker
duration: 5 RimWorld days
```

The marker cannot be entered yet. It creates no map, loot, trader,
recruitment, military aid or raid. It is the first safe world-map footprint for
future hidden Tok'ra safehouse systems.


#### Enterable Tok'ra hidden safehouse

Since `0.2.17-dev`, one stored lead can create a temporary vanilla site:

```text
-1 safehouse lead
1 enterable non-hostile site
2 tretonin doses
4 industrial medicine
duration before arrival: 10 RimWorld days
```

The generated map is deliberately small and contains no hostile defenders,
trader, recruitable pawn, military aid or permanent settlement.
