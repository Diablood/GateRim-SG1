# GateRim SG-1 — Initial roadmap

## Completed milestones

### 0.1.0-dev — Foundation
- [x] Create RimWorld 1.6 directory structure
- [x] Add project conventions and preserve the custom icon

### 0.1.1-dev to 0.1.5-dev — Jaffa foundation
- [x] Add and validate the Jaffa xenotype foundation
- [x] Add French translations
- [x] Correct first in-game log issues

### 0.1.6-dev — Player wiki foundation
- [x] Add versioned wiki drafts
- [x] Publish the initial GitHub wiki

### 0.1.7-dev — Goa'uld host foundation
- [x] Add the XML-only `SG1_GoauldHost` prototype
- [x] Add `SG1_NaquadahBlood`
- [x] Add `SG1_GoauldLongevity`
- [x] Add French translations and temporary local icons

## 0.1.8-dev — Free Goa'uld symbiote prototype
- [x] Add the XML-only `SG1_GoauldSymbiote` animal-style pawn
- [x] Add a weak bite attack
- [x] Exclude the symbiote from natural biome spawning
- [x] Add a temporary local pawn sprite
- [x] Add French `DefInjected` translations
- [x] Update wiki drafts
- [x] Add `tools/sync-wiki.sh`
- [ ] Validate the prototype in RimWorld 1.6
- [ ] Publish the updated player-wiki pages

## 0.1.9-dev — Fix free Goa'uld symbiote XML
- [x] Remove the invalid `<wildness>` field from `RaceProperties`
- [x] Preserve developer-mode spawning for isolated tests
- [x] Keep the wiki unchanged because player-facing behavior is not modified
- [ ] Re-run the minimal RimWorld test
- [ ] Confirm that `Player.log` no longer reports the `wildness` XML error

## Next symbiote milestones
- [ ] Add a recent-implantation Hediff
- [ ] Add forced implantation
- [ ] Add ritual implantation
- [ ] Add transfer between hosts
- [ ] Add extraction
- [ ] Add Tok'ra behavior
- [ ] Implement Jaffa symbiote dependency
- [ ] Add tretonin

## Factions and equipment
- [ ] Create the Goa'uld System Lords faction
- [ ] Add Jaffa pawn kinds
- [ ] Add Ma'Tok staff weapon
- [ ] Add Zat'nik'tel
- [ ] Add generic Jaffa armor
- [ ] Add Free Jaffa Nation
- [ ] Add Tok'ra resistance
- [ ] Add Unas tribes
- [ ] Add Asgard quest faction
- [ ] Add Replicator swarm
