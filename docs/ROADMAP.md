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

## 0.1.17-dev — Add forced Goa'uld implantation prototype
- [x] Add persistent identity data to the free symbiote pawn
- [x] Add the manual adjacent-target `Forced implantation` command
- [x] Transfer the same `GoauldSymbioteData` into `SG1_GoauldRecentImplantation`
- [x] Consume the free symbiote pawn after successful transfer
- [x] Reject children under 13, animals, mechanoids and duplicate implantation states
- [x] Add bilingual `Keyed` UI strings
- [x] Add transfer lifecycle logs through `GR_Log`
- [x] Update technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Validate identity transfer in game
- [ ] Validate persistence after save and reload
- [ ] Publish the updated player wiki

## 0.1.18-dev — Convert recent implantation into active Goa'uld host
- [x] Add `HediffComp_GoauldImplantationConversion`
- [x] Derive conversion timing from `HediffComp_Disappears`
- [x] Transfer the same persistent identity into `SG1_GoauldHostSymbiote`
- [x] Prevent temporary-state removal from detaching the transferred symbiote
- [x] Add acquired active-host biological modifiers
- [x] Preserve the original germline xenotype
- [x] Add bilingual UI text, documentation and player-wiki drafts
- [x] Build locally against RimWorld 1.6
- [x] Validate save/reload during the critical phase
- [x] Validate conversion and identity persistence
- [x] Validate save/reload after conversion
- [ ] Publish the updated player wiki

## 0.1.19-dev — Add emergency Goa'uld extraction prototype
- [x] Add `HediffComp_GoauldEmergencyExtraction`
- [x] Add a manual `Emergency extraction` gizmo during recent implantation
- [x] Transfer the same persistent identity back into a free symbiote pawn
- [x] Preserve free-symbiote re-implantation support
- [x] Add bilingual UI strings and a temporary command icon
- [x] Add reverse-transfer lifecycle logging
- [x] Update technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Validate extraction before countdown expiry
- [ ] Validate identical ID after extraction
- [ ] Validate re-implantation of the extracted pawn
- [ ] Validate save/reload after extraction
- [ ] Publish the updated player wiki

## 0.1.20-dev — Add emergency Goa'uld extraction surgery
- [x] Add `SG1_EmergencyExtractGoauldSymbiote`
- [x] Add `Recipe_EmergencyExtractGoauldSymbiote`
- [x] Derive the worker from vanilla `Recipe_Surgery`
- [x] Use vanilla `CheckSurgeryFail(...)`
- [x] Require Medicine `6`, medicine and medical work time
- [x] Preserve the same symbiote identity after successful surgery
- [x] Keep recent implantation active after failed surgery
- [x] Keep the immediate command temporarily for regression tests
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the operation appears in the pawn health tab
- [ ] Validate successful surgery and identity persistence
- [ ] Validate failed surgery behavior
- [ ] Publish the updated player wiki

## 0.1.21-dev — Add autonomous free-symbiote hunt
- [x] Add `SG1_GoauldAutonomousImplant`
- [x] Add `JobDriver_GoauldAutonomousImplant`
- [x] Scan periodically for the nearest compatible reachable humanoid
- [x] Pursue the selected target through a dedicated job
- [x] Implant automatically on contact
- [x] Preserve the existing identity-transfer flow
- [x] Add an autonomous-hunt toggle for development tests
- [x] Add a cooldown after manual or surgical extraction
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Validate pursuit and contact implantation
- [ ] Validate extraction cooldown
- [ ] Validate toggle behavior
- [ ] Publish the updated player wiki

## 0.1.22-dev — Add ritual Goa'uld implantation prototype
- [x] Add `Ritual implantation`
- [x] Reuse the centralized symbiote identity-transfer flow
- [x] Search for the nearest compatible reachable humanoid within `12` cells
- [x] Consume the free symbiote after controlled implantation
- [x] Preserve the same persistent identity
- [x] Add bilingual UI strings and a temporary command icon
- [x] Add technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Validate ritual implantation outside contact range
- [ ] Validate rejection when no compatible target is in range
- [ ] Validate identity persistence after save/reload
- [ ] Publish the updated player wiki

## Next genetics tests
- [ ] Test Jaffa × Jaffa offspring
- [ ] Test Jaffa mother × baseliner father
- [ ] Test baseliner mother × Jaffa father
- [ ] Test Jaffa × another germline xenotype
- [ ] Decide whether vanilla hybrid inheritance is sufficient
- [ ] Avoid forced maternal inheritance unless tests demonstrate a clear need

## Next symbiote milestones
- [ ] Add automatic Prim'ta age checks and ceremony flow
- [x] Apply recent Goa'uld implantation through a manual adjacent forced-implantation prototype
- [x] Add autonomous free-symbiote pursuit and contact implantation
- [ ] Add richer tactical priorities for autonomous symbiotes
- [x] Add controlled ritual Goa'uld implantation prototype
- [ ] Add explicit ritual target selection and ceremony flow
- [x] Add manual emergency extraction prototype
- [x] Complement manual extraction with a medical surgery bill
- [x] Convert a victim into an active Goa'uld host when the timer ends
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
