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

## 0.1.23-dev — Add explicit ritual target selection
- [x] Replace automatic nearest-target ritual selection
- [x] Start vanilla map targeting from `Ritual implantation`
- [x] Accept only compatible reachable humanoids within `12` cells
- [x] Allow the player to choose a farther valid pawn
- [x] Preserve centralized identity transfer
- [x] Interrupt autonomous pursuit while controlled targeting starts
- [x] Add bilingual UI text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the targeting cursor appears
- [ ] Confirm the clicked pawn is implanted
- [ ] Validate rejection of invalid, unreachable and out-of-range targets
- [ ] Validate identity persistence after save/reload
- [ ] Publish the updated player wiki

## 0.1.24-dev — Add core ritual ceremony duration and cancellation
- [x] Add a `600`-tick core ritual ceremony after explicit targeting
- [x] Persist ritual target and remaining duration through save/reload
- [x] Display ritual progress in the free-symbiote inspection panel
- [x] Add `Cancel ritual`
- [x] Cancel automatically if the symbiote becomes unavailable, dead or downed
- [x] Cancel automatically if the target becomes invalid, unreachable or out of range
- [x] Preserve the same centralized identity-transfer flow on completion
- [x] Keep the fallback independent from optional DLCs
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Validate timed completion
- [ ] Validate save/reload during the ceremony
- [ ] Validate manual and automatic cancellation
- [ ] Publish the updated player wiki

## 0.1.25-dev — Add ritual environmental requirements
- [x] Add `SG1_GoauldRitualBasin`
- [x] Make the basin constructible without optional DLCs
- [x] Require the free symbiote and ritual target near the same basin
- [x] Save the active basin reference during the ceremony
- [x] Display the active basin in the inspection panel
- [x] Cancel if the basin is destroyed, removed or out of range
- [x] Preserve identity after cancellation
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the basin is constructible
- [ ] Validate ritual completion near a basin
- [ ] Validate save/reload during a basin-backed ceremony
- [ ] Validate cancellation after basin destruction and movement
- [ ] Publish the updated player wiki

## 0.1.26-dev — Add Jaffa Prim'ta implantation prototype
- [x] Add `SG1_ImplantJaffaPrimta`
- [x] Add `Recipe_ImplantJaffaPrimta`
- [x] Restrict the operation to compatible Jaffa lineage genes
- [x] Reject duplicate Prim'ta implantation
- [x] Reuse the existing acquired `SG1_JaffaPrimta` effects
- [x] Add lifecycle logging through `HediffComp_JaffaPrimta`
- [x] Require Medicine `4`, one medicine and medical work time
- [x] Keep the prototype independent from a physical larva resource
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the operation appears only for compatible Jaffa
- [ ] Validate successful implantation and acquired effects
- [ ] Validate save/reload persistence
- [ ] Validate duplicate-operation rejection
- [ ] Publish the updated player wiki

## 0.1.27-dev — Add physical Prim'ta larva resource
- [x] Add `SG1_PrimtaLarva`
- [x] Make the larva a haulable and stackable item
- [x] Require one physical larva in `SG1_ImplantJaffaPrimta`
- [x] Keep the existing medicine requirement
- [x] Let the vanilla bill flow haul and consume the larva
- [x] Add a defensive C# ingredient check
- [x] Add French translation, a temporary graphic, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the larva can be spawned, hauled and stored
- [ ] Confirm the implantation bill waits without a larva
- [ ] Validate larva consumption after successful surgery
- [ ] Validate save/reload persistence of the implanted Hediff
- [ ] Publish the updated player wiki

## 0.1.28-dev — Add Prim'ta larva acquisition prototype
- [x] Add `SG1_PrimtaIncubationBasin`
- [x] Make the incubation basin constructible from the Production category
- [x] Add the `SG1_IncubatePrimtaLarva` work bill
- [x] Register `SG1_DoBillsPrimtaIncubation` for automatic and manual work selection
- [x] Use Handling work with Animals skill `4`
- [x] Produce one physical `SG1_PrimtaLarva`
- [x] Keep the first bill ingredient-free for isolated loop validation
- [x] Add French translation, a temporary graphic, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the basin appears in the Production category
- [ ] Confirm the bill is available
- [ ] Validate larva production without developer spawning
- [ ] Validate the full production-to-implantation loop
- [ ] Publish the updated player wiki

## 0.1.29-dev — Add Prim'ta incubation nutrient requirements
- [x] Require `10` units of raw meat for `SG1_IncubatePrimtaLarva`
- [x] Accept the vanilla `MeatRaw` category
- [x] Allow mixed raw-meat stacks
- [x] Preserve the Handling work type and Animals skill `4`
- [x] Preserve the dedicated incubation `WorkGiverDef`
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Reload RimWorld and validate XML Def loading
- [ ] Confirm the bill waits without enough raw meat
- [ ] Confirm `10` raw-meat units are consumed
- [ ] Confirm one physical larva is produced
- [ ] Validate the full incubation-to-implantation loop
- [ ] Publish the updated player wiki

## 0.1.30-dev — Add Prim'ta larva preservation prototype
- [x] Make `SG1_PrimtaLarva` a `ThingWithComps`
- [x] Add vanilla `CompProperties_Rottable`
- [x] Set `daysToRotStart` to `6`
- [x] Destroy fully rotted larvae
- [x] Preserve the existing incubation and implantation loops
- [x] Add French translation, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm rotting information appears on the larva
- [ ] Confirm warm-storage rot progression
- [ ] Confirm cold storage improves preservation
- [ ] Confirm fully rotted larvae are destroyed
- [ ] Confirm implantation still consumes fresh larvae
- [ ] Publish the updated player wiki

## 0.1.31-dev — Add Prim'ta larva biological storage category
- [x] Add `SG1_GoauldBiologicalProducts`
- [x] Nest the custom category under `ResourcesRaw`
- [x] Move `SG1_PrimtaLarva` out of `Manufactured`
- [x] Keep the larva outside vanilla raw-food categories
- [x] Add French translation, technical docs and player-wiki drafts
- [x] Add a final object-category audit TODO
- [ ] Reload RimWorld and validate XML Def loading
- [ ] Confirm the stockpile-filter hierarchy
- [ ] Confirm the larva is absent from manufactured goods
- [ ] Confirm the larva is not treated as raw food
- [ ] Validate stacking, rotting and implantation regressions
- [ ] Publish the updated player wiki

## Next genetics tests
- [ ] Test Jaffa × Jaffa offspring
- [ ] Test Jaffa mother × baseliner father
- [ ] Test baseliner mother × Jaffa father
- [ ] Test Jaffa × another germline xenotype
- [ ] Decide whether vanilla hybrid inheritance is sufficient
- [ ] Avoid forced maternal inheritance unless tests demonstrate a clear need

## Next symbiote milestones
- [x] Add medical Jaffa Prim'ta implantation prototype
- [x] Add a physical Prim'ta larva resource
- [x] Add first Prim'ta larva incubation prototype
- [x] Add first raw-meat nutrient requirement
- [x] Add first vanilla larva preservation prototype
- [ ] Add dedicated living-symbiote temperature tuning
- [ ] Evaluate nutrition-value-based balancing
- [ ] Add automatic Prim'ta age checks and ceremony flow
- [x] Apply recent Goa'uld implantation through a manual adjacent forced-implantation prototype
- [x] Add autonomous free-symbiote pursuit and contact implantation
- [ ] Add richer tactical priorities for autonomous symbiotes
- [x] Add controlled ritual Goa'uld implantation prototype
- [x] Add explicit ritual target selection
- [x] Add core ritual ceremony duration and cancellation
- [x] Add a core ritual basin requirement
- [ ] Add richer environmental requirements and dedicated assets
- [ ] Add optional Ideology ritual integration
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


## Optional DLC integrations
- [ ] Keep `Core + Biotech` as the required foundation
- [ ] Evaluate optional `Ideology` integration for Goa'uld cults, ceremonies, roles and ritual requirements
- [ ] Reuse the existing GateRim SG-1 identity-transfer core from optional Ideology rituals
- [ ] Evaluate optional `Royalty` integration when System Lord factions are introduced
- [ ] Assess titles, favor, permits, quests and limited thematic abilities without making `Royalty` mandatory


## Final object-category audit
- [ ] Inventory every mod-added `ThingDef` and its `thingCategories`
- [ ] Review vanilla-category semantics and side effects
- [ ] Identify biological, technological, military, ritual and faction-specific families
- [ ] Avoid categories that create unintended recipe, storage or trade eligibility
- [ ] Create custom trees only when they improve player usability
- [ ] Harmonize English/French labels, storage filters and wiki documentation
