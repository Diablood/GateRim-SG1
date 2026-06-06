# GateRim SG-1 — Initial roadmap

## 0.1.0-dev — Foundation
- [x] Create RimWorld 1.6 directory structure
- [x] Add `About/About.xml`
- [x] Preserve the custom `About/ModIcon.png`
- [x] Add project conventions

## 0.1.1-dev — Jaffa xenotype foundation
- [x] Create the prototype `SG1_Jaffa` xenotype
- [x] Declare the Biotech dependency
- [x] Defer symbiote dependency to a dedicated system

## 0.1.2-dev — Jaffa physiology refinement
- [x] Remove the forced `Body_Hulk` appearance
- [x] Add the custom `SG1_JaffaPhysiology` gene
- [x] Preserve visual body-type variation
- [x] Add a temporary `+15` carrying-capacity effect

## 0.1.3-dev — Jaffa longevity
- [x] Add `SG1_JaffaLongevity`
- [x] Set `LifespanFactor` to `1.5`
- [x] Add the gene to `SG1_Jaffa`

## 0.1.4-dev — French Jaffa translations
- [x] Add French `DefInjected` translations for `SG1_Jaffa`
- [x] Add French `DefInjected` translations for `SG1_JaffaPhysiology`
- [x] Add French `DefInjected` translations for `SG1_JaffaLongevity`
- [x] Document the localization workflow

## 0.1.5-dev — First in-game log fixes
- [x] Remove leading and trailing whitespace from the Jaffa xenotype description
- [x] Normalize French `DefInjected` text values
- [x] Replace the missing `Gene_Robust` texture path
- [x] Add a local placeholder icon for `SG1_JaffaPhysiology`
- [x] Document isolated testing with `Core`, `Biotech`, and `GateRim SG-1`
- [ ] Confirm the corrected log no longer reports GateRim whitespace errors
- [ ] Generate the French translation report if translation errors remain
- [ ] Confirm the physiology icon loads correctly

## First playable content
- [ ] Create the Goa'uld faction
- [ ] Add a basic Jaffa warrior
- [ ] Add an elite Jaffa guard
- [ ] Add the Ma'Tok staff weapon
- [ ] Add the Zat'nik'tel
- [ ] Add generic Jaffa armor
- [ ] Test faction generation and raids

## Later milestones

### Symbiotes
- [ ] Prototype Goa'uld host xenotype
- [ ] Prototype Tok'ra host xenotype
- [ ] Implement Jaffa symbiote dependency
- [ ] Move Jaffa longevity to the dedicated symbiote system if appropriate

### Appearance tuning
- [ ] Observe generated Jaffa body-type distribution in game
- [ ] Decide whether weighted body-type generation requires C# logic

### Additional factions
- [ ] Free Jaffa Nation
- [ ] Tok'ra resistance
- [ ] Unas tribes
- [ ] Asgard quest faction
- [ ] Replicator swarm
