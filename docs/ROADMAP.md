# GateRim SG-1 — Initial roadmap

## Completed milestones

### 0.1.0-dev — Foundation
- [x] Create RimWorld 1.6 directory structure
- [x] Add project conventions and preserve the custom icon

### 0.1.1-dev to 0.1.5-dev — Jaffa foundation
- [x] Add and validate the initial Jaffa xenotype
- [x] Add French translations
- [x] Correct first in-game log issues

### 0.1.6-dev — Player wiki foundation
- [x] Add versioned wiki drafts
- [x] Publish the initial GitHub wiki

### 0.1.7-dev to 0.1.11-dev — Goa'uld prototype chain
- [x] Add the XML-only Goa'uld host prototype
- [x] Add the free-symbiote pawn prototype
- [x] Add the recent-implantation Hediff prototype
- [x] Add Windows-friendly wiki synchronization helpers

### 0.1.12-dev — Jaffa germline correction
- [x] Set `SG1_Jaffa` to an inheritable germline xenotype
- [x] Keep adult Goa'uld possession non-heritable
- [x] Document genetics behavior

## 0.1.13-dev — Split Jaffa lineage and Prim'ta effects
- [x] Reduce `SG1_Jaffa` to inherited lineage genes
- [x] Add `SG1_JaffaLineage`
- [x] Add `SG1_JaffaPouchPotential`
- [x] Add `SG1_JaffaSymbioteCompatibility`
- [x] Preserve the modest inherited `SG1_JaffaPhysiology`
- [x] Keep `SG1_JaffaLongevity` as a legacy development Def
- [x] Add persistent `SG1_JaffaPrimta`
- [x] Move immunity, healing, pain, damage-resistance and lifespan effects to the Prim'ta Hediff
- [x] Add French translations
- [x] Update technical docs and wiki drafts
- [ ] Validate newly generated Jaffa in RimWorld 1.6
- [ ] Validate manual add/remove of `SG1_JaffaPrimta`
- [ ] Publish the updated player wiki

## 0.1.14-dev — Add centralized C# logging scaffold
- [x] Add `GR_Log`
- [x] Add `Message`, `Warning`, `Error`, `WarningOnce` and `ErrorOnce`
- [x] Prefix all diagnostics with `[GateRim SG-1]`
- [x] Add a C# project and solution
- [x] Add Windows and Bash build helpers
- [x] Add an assembly-load smoke-test message
- [x] Document the logging rules
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the bootstrap message in `Player.log`

## 0.1.15-dev — Add colored log prefix and Windows build wrapper
- [x] Add a colored `[GateRim SG-1]` logging prefix
- [x] Use the gold tone `#D9B44A`
- [x] Add `build.cmd`
- [x] Document the Windows build wrapper
- [ ] Build through `build.cmd`
- [ ] Confirm the colored bootstrap message in `Player.log`

## 0.1.16-dev — Add persistent Goa'uld symbiote data
- [x] Add `GoauldSymbioteData`
- [x] Add `HediffComp_GoauldSymbiote`
- [x] Add `HediffCompProperties_GoauldSymbiote`
- [x] Deep-save one unique symbiote ID
- [x] Store origin, age placeholder and host-history fields
- [x] Attach persistence to `SG1_GoauldRecentImplantation`
- [x] Add persistent manual-test carrier `SG1_GoauldHostSymbiote`
- [x] Add bilingual `Keyed` UI strings
- [x] Add lifecycle logging through `GR_Log`
- [ ] Build locally against RimWorld 1.6
- [ ] Validate ID persistence after save and reload
- [ ] Confirm matching `Attached`, `Loaded` and `Detached` logs

## Next genetics tests
- [ ] Test Jaffa × Jaffa offspring
- [ ] Test Jaffa mother × baseliner father
- [ ] Test baseliner mother × Jaffa father
- [ ] Test Jaffa × another germline xenotype
- [ ] Decide whether vanilla hybrid inheritance is sufficient
- [ ] Avoid forced maternal inheritance unless tests demonstrate a clear need

## Next symbiote milestones
- [ ] Add automatic Prim'ta age checks and ceremony flow
- [ ] Apply recent Goa'uld implantation after a successful forced attack
- [ ] Add ritual Goa'uld implantation
- [ ] Add medical interruption
- [ ] Convert a victim into an active Goa'uld host when the timer ends
- [ ] Add transfer between hosts
- [ ] Add extraction
- [ ] Add Tok'ra behavior
- [ ] Add tretonin

## Factions, visuals and equipment
- [ ] Create the Goa'uld System Lords faction
- [ ] Add Jaffa pawn kinds
- [ ] Add Goa'uld-faction facial tattoos or markings for Jaffa
- [ ] Add variants by System Lord or Goa'uld faction where practical
- [ ] Keep Free Jaffa visually distinct from Goa'uld-aligned Jaffa
- [ ] Add Ma'Tok staff weapon
- [ ] Add Zat'nik'tel
- [ ] Add generic Jaffa armor
- [ ] Add Free Jaffa Nation
- [ ] Add Tok'ra resistance
- [ ] Add Unas tribes
- [ ] Add Asgard quest faction
- [ ] Add Replicator swarm
