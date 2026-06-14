# GateRim SG-1 — Development roadmap

## Immediate maintenance — pre-0.2 cleanup preparation

- [x] Generate the RimWorld French translation report and add the two missing GateRim SG-1 entries. Keep the five remaining load errors documented as vanilla RimWorld French issues.
- [x] Restore the broken `Content-Status.md` planned-content table.
- [ ] Audit player-facing inspection text, contextual information and debug-only diagnostics.
- [ ] Audit gizmos and commands: always visible, contextual, mod-debug only, RimWorld-dev only or removed.
- [ ] Add a GateRim-specific debug option before the `0.2.x` Stargate chapter.
- [ ] Keep final wiki images and Workshop presentation assets as a separate later production pass.

## Pre-0.2 gameplay roadmap

### Biological and faction foundations

- [x] Evaluate deep-freezing penalties and specialized larva containers.
- [x] Add a Goa'uld queen biological-foundation prototype.
- [x] Turn the current Prim'ta incubation basin into assisted maturation infrastructure with a developer-only queen-origin sourcing prototype.
- [x] Add the first Goa'uld System Lord faction prototype.
- [x] Add Goa'uld-aligned Jaffa pawn kinds and keep Free Jaffa visually distinct.
- [ ] Add a first Jaffa facial-marking prototype linked to a Goa'uld faction or future System Lord style.

### Emblematic equipment

- [x] Add a Ma'Tok staff-weapon prototype.
- [x] Add a Zat'nik'tel first-shot incapacitation prototype.
- [x] Add generic Jaffa armor.
- [x] Add an SG-team field uniform prototype.
- [x] Add SG tactical boots prototype.
- [x] Add SG tactical gloves prototype.
- [x] Add a first SG tactical vest or visual equipment layer.
- [x] Add black and desert SG-team field-uniform variants. Keep forest camouflage omitted unless a stronger visual need emerges. Reserve heavy tactical, medical and scientific variants for later.

### Interface and debug cleanup before 0.2.x

- [ ] Inventory every visible information line, gizmo, button and gameplay log.
- [ ] Classify each element as always visible, contextual, mod-debug only, RimWorld-dev only or removed.
- [ ] Hide persistent IDs, raw ticks and internal counters outside debug modes unless they are directly useful to the player.
- [ ] Keep contextual player information readable, such as remaining offer duration expressed in days.
- [ ] Replace development-only commands with contextual gameplay triggers where appropriate.
- [ ] Add a mod settings entry to show advanced GateRim SG-1 diagnostics.

### Wiki visuals and Workshop preparation

- [ ] Create the wiki-asset folder structure only when definitive images start to exist.
- [ ] Capture final or near-final UI buttons, larva, tretonin vial, buildings, clothing, boots, gloves, weapons and representative events.
- [ ] Reuse selected wiki images later for the Steam Workshop page.
- [ ] Keep current temporary textures explicitly provisional.

### New chapter

- [x] Close `0.1.x` as the mechanical-foundation phase.
- [x] Start `0.2.x` as the playable no-gate phase with a stranded SG-team starter scenario.
- [x] Add a playable Goa'uld world-faction baseline.
- [x] Add a Free Jaffa world-faction baseline.
- [ ] Consolidate Tok'ra world presence and non-developer acquisition loops.
- [ ] Start `0.3.x` with the Stargate foundation only after the no-gate slice is playable.

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

## 0.1.32-dev — Add Prim'ta larva temperature tuning
- [x] Add `CompProperties_PrimtaLarvaTemperature`
- [x] Add `Comp_PrimtaLarvaTemperature`
- [x] Preserve vanilla rotting below `25 °C`
- [x] Accelerate deterioration to `×2` from `25 °C`
- [x] Accelerate deterioration to `×3` from `40 °C`
- [x] Display the thermal condition and effective rate
- [x] Keep freezing safe for this first pass
- [x] Add English/French text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm inspection strings
- [ ] Confirm frozen, refrigerated, normal, hot and critical bands
- [ ] Confirm hot deterioration acceleration
- [ ] Confirm incubation, stacking, storage and implantation regressions
- [ ] Publish the updated player wiki

## 0.1.33-dev — Add Prim'ta implantation age eligibility prototype
- [x] Add the `10` biological-year implantation threshold
- [x] Centralize the threshold in `JaffaPrimtaUtility`
- [x] Hide the operation for compatible Jaffa below the threshold
- [x] Keep a defensive age check during surgery execution
- [x] Add a bilingual rejection message
- [x] Preserve physical larva, medicine and compatibility requirements
- [x] Add technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm the operation is hidden below age `10`
- [ ] Confirm the operation appears from age `10`
- [ ] Validate successful surgery, duplicate guard and save persistence
- [ ] Publish the updated player wiki

## 0.1.34-dev — Add Jaffa puberty dependency prototype
- [x] Add `SG1_JaffaPrimtaDependency`
- [x] Add `GameComponent_JaffaPrimtaDependency`
- [x] Start dependency from `12` biological years
- [x] Scan spawned pawns on active maps every in-game hour
- [x] Increase severity by `0.1` per in-game day
- [x] Add four progressive immunity and recovery stages
- [x] Remove dependency immediately when `SG1_JaffaPrimta` is attached
- [x] Add bilingual messages, logs, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm no dependency at age `11`
- [ ] Confirm dependency starts at age `12`
- [ ] Confirm severity progression and stage modifiers
- [ ] Confirm save/reload persistence
- [ ] Confirm implantation removes the dependency immediately
- [ ] Publish the updated player wiki

## 0.1.35-dev — Add Jaffa Prim'ta cultural thoughts
- [x] Add `SG1_AwaitingPrimta`
- [x] Add `ThoughtWorker_AwaitingPrimta`
- [x] Apply a situational `-1` mood effect from age `10` without Prim'ta
- [x] Add the temporary `SG1_ReceivedPrimta` memory
- [x] Grant `+3` mood for `5` days after the first implantation
- [x] Add `GameComponent_JaffaPrimtaCulturalThoughts`
- [x] Persist the first-implantation registry
- [x] Prevent repeated bonuses after removal and reimplantation
- [x] Keep cultural mood separate from medical dependency
- [x] Add French text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm waiting thought at age `10+`
- [ ] Confirm no waiting thought at age `9`
- [ ] Confirm first-implantation memory
- [ ] Confirm removal and reimplantation do not repeat the memory
- [ ] Confirm save/reload persistence of the registry
- [ ] Publish the updated player wiki

## 0.1.36-dev — Add tretonin substitution prototype
- [x] Add physical `SG1_TretoninDose`
- [x] Add dedicated `SG1_GoauldMedicalProducts` storage category
- [x] Keep tretonin outside vanilla generic medicine
- [x] Add `SG1_AdministerTretonin`
- [x] Restrict administration to compatible Jaffa aged `12+` without Prim'ta
- [x] Add temporary `SG1_TretoninSubstitution`
- [x] Use vanilla one-day `HediffCompProperties_Disappears`
- [x] Suppress Prim'ta dependency while substitution is active
- [x] Remove existing dependency immediately after administration
- [x] Add bilingual text, temporary texture, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm dose storage and developer spawning
- [ ] Confirm operation visibility rules
- [ ] Confirm dose consumption and immediate relief
- [ ] Confirm remaining-time display and save persistence
- [ ] Confirm treatment expiry and dependency return
- [ ] Publish the updated player wiki

## 0.1.37-dev — Add tretonin acquisition prototype
- [x] Add `SG1_PrepareTretoninDoses`
- [x] Reuse vanilla `DrugLab` for the first production route
- [x] Require one physical `SG1_PrimtaLarva`
- [x] Require one vanilla medicine unit
- [x] Produce five physical `SG1_TretoninDose` items
- [x] Require Intellectual skill `6`
- [x] Preserve the dedicated Goa'uld medical-products category
- [x] Add French text, technical docs and player-wiki drafts
- [ ] Reload RimWorld and validate XML Def loading
- [ ] Confirm the recipe appears on `DrugLab`
- [ ] Confirm ingredient requirements and consumption
- [ ] Confirm exactly five doses are produced
- [ ] Validate storage, stacking and administration regressions
- [ ] Publish the updated player wiki

## 0.1.38-dev — Add formal Prim'ta ceremony prototype
- [x] Add `CompProperties_JaffaPrimtaCeremony`
- [x] Add `Comp_JaffaPrimtaCeremony`
- [x] Attach the ceremony to `SG1_GoauldRitualBasin`
- [x] Register the basin with `tickerType = Normal`
- [x] Require one eligible nearby Jaffa
- [x] Require one nearby physical `SG1_PrimtaLarva`
- [x] Add a `600`-tick timed rite
- [x] Persist target, reserved larva and remaining duration
- [x] Consume the larva only after successful completion
- [x] Attach `SG1_JaffaPrimta` and reuse dependency/cultural side effects
- [x] Add manual and automatic cancellation
- [x] Keep the existing medical-operation workflow
- [x] Add bilingual text, technical docs and player-wiki drafts
- [x] Document the future Goa'uld-queen larva-origin TODO
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm basin command, targeting and range rules
- [ ] Confirm save/reload persistence
- [ ] Confirm successful completion and larva consumption
- [ ] Confirm cancellation without larva consumption
- [ ] Confirm surgery remains available independently
- [ ] Publish the updated player wiki

## 0.1.39-dev — Add Tok'ra foundation prototype
- [x] Add hidden non-generated `SG1_Tokra` faction foundation
- [x] Remove unsupported Tok'ra FactionDef placeholder fields
- [x] Add the required Tok'ra raid-loot curve for clean Def validation
- [x] Add `SG1_TokraSymbiote`
- [x] Create free symbiotes with explicit persistent origin
- [x] Disable autonomous hunting for Tok'ra
- [x] Hide forced implantation for Tok'ra
- [x] Hide Goa'uld ritual implantation for Tok'ra
- [x] Hide the autonomous-hunt toggle for Tok'ra
- [x] Add explicit voluntary Tok'ra host targeting
- [x] Restrict voluntary targets to nearby player-controlled compatible humanoids
- [x] Preserve Tok'ra origin through save/reload and extraction
- [x] Return extracted Tok'ra identities as the non-hunting pawn variant
- [x] Generalize shared adult-host descriptions for Goa'uld and Tok'ra origins
- [x] Add bilingual text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm Tok'ra dev spawning and inspection data
- [ ] Confirm Tok'ra-only voluntary gizmo
- [ ] Confirm voluntary implantation, conversion and save persistence
- [ ] Confirm extraction returns the Tok'ra variant
- [ ] Confirm normal Goa'uld workflows remain unchanged
- [ ] Publish the updated player wiki

## 0.1.40-dev — Add Tok'ra voluntary host pawn prototype
- [x] Add `SG1_TokraVoluntaryHost`
- [x] Define a neutral humanlike `initialResistanceRange`
- [x] Reuse vanilla `Human` as the first host body
- [x] Spawn the prototype under `PlayerColony`
- [x] Add `GameComponent_TokraHostPrototypeInitializer`
- [x] Scan spawned maps every `60` ticks
- [x] Attach one active adult symbiote with origin `Tokra`
- [x] Persist initialized pawn `ThingID` values
- [x] Prevent artificial replacement after later extraction or removal
- [x] Reuse the shared Goa'uld-family active-host Hediff
- [x] Add bilingual text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm developer spawning and player control
- [ ] Confirm one-time Tok'ra symbiote initialization
- [ ] Confirm persistent ID after save/reload
- [ ] Confirm distinct IDs on multiple prototype hosts
- [ ] Confirm free Tok'ra and Goa'uld workflow regressions
- [ ] Publish the updated player wiki

## 0.1.41-dev — Add Tok'ra pawn-group foundation
- [x] Fix invalid standalone `PawnGroupMakerDef` declarations
- [x] Add a nested Tok'ra `Combat` pawn-group profile
- [x] Add a nested Tok'ra `Peaceful` pawn-group profile
- [x] Reuse `SG1_TokraVoluntaryHost` as the first pawn option
- [x] Add the required `maxPawnCostPerTotalPointsCurve`
- [x] Keep the hidden Tok'ra faction disconnected from automatic world generation
- [x] Preserve the staged rollout before visitors, traders and world settlements
- [x] Document future therapeutic Tok'ra hosting separately
- [x] Add technical docs and player-wiki drafts
- [ ] Restart RimWorld and validate clean Def loading
- [ ] Confirm no group-maker XML errors in `Player.log`
- [ ] Confirm Tok'ra and Goa'uld regression workflows
- [ ] Publish the updated player wiki

## 0.1.42-dev — Add Tok'ra peaceful visitor prototype
- [x] Add `SG1_TokraPeacefulVisitors`
- [x] Add `IncidentWorker_TokraPeacefulVisitors`
- [x] Keep storyteller `baseChance = 0`
- [x] Trigger visits manually through developer tools
- [x] Create or reuse one hidden Tok'ra faction instance
- [x] Build the hidden faction with explicit `FactionGeneratorParms`
- [x] Reuse the nested Tok'ra `Peaceful` pawn-group profile
- [x] Spawn `1` to `3` Tok'ra voluntary-host pawns
- [x] Reuse vanilla peaceful visit and automatic departure behavior
- [x] Preserve hidden faction state and disable settlements, traders and random visits
- [x] Add French text, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm clean `Player.log`
- [ ] Confirm developer incident appears
- [ ] Confirm `1` to `3` peaceful non-player visitors
- [ ] Confirm automatic Tok'ra-host initialization
- [ ] Confirm automatic visitor departure
- [ ] Confirm hidden faction persistence after save/reload
- [ ] Confirm Tok'ra and Goa'uld regression workflows
- [ ] Publish the updated player wiki

## 0.1.43-dev — Enable low-frequency natural Tok'ra visits
- [x] Enable storyteller selection with `baseChance = 0.10`
- [x] Delay natural visits until day `15`
- [x] Add a `30`-day minimum refire interval
- [x] Keep developer-triggered testing available
- [x] Remove `(test)` from English and French labels
- [x] Keep the Tok'ra faction hidden and disconnected from world generation
- [x] Keep settlements, traders, trade stock and diplomacy disabled
- [x] Update logs, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm clean `Player.log`
- [ ] Confirm developer incident still works
- [ ] Observe at least one natural visit after day `15`
- [ ] Confirm the `30`-day refire interval during extended balancing
- [ ] Confirm Tok'ra and Goa'uld regression workflows
- [ ] Publish the updated player wiki

## 0.1.44-dev — Add Tok'ra therapeutic healing prototype
- [x] Add `GameComponent_TokraTherapeuticHosting`
- [x] Scan spawned active Tok'ra hosts every `60` ticks
- [x] Reuse persistent symbiote origin instead of creating a separate host state
- [x] Cure a narrow configured list of serious pathologies
- [x] Keep Goa'uld hosts unchanged
- [x] Keep injuries, scars and unlisted illnesses untouched
- [x] Add bilingual feedback, logs, technical docs and player-wiki drafts
- [ ] Build locally against RimWorld 1.6
- [ ] Confirm clean `Player.log`
- [ ] Confirm an active Tok'ra host removes each configured available pathology
- [ ] Confirm injuries, scars and unlisted illnesses remain
- [ ] Confirm an active Goa'uld host does not receive Tok'ra healing
- [ ] Confirm treatment still works after save/reload
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
- [x] Add first living-symbiote temperature tuning
- [ ] Evaluate deep-freezing penalties and specialized containers
- [ ] Evaluate nutrition-value-based balancing
- [x] Add first Prim'ta implantation age eligibility check
- [x] Add first formal Prim'ta ceremony fallback
- [ ] Add richer ceremony roles, staging and optional Ideology integration
- [x] Add first puberty dependency without Prim'ta
- [x] Add first tretonin substitution prototype
- [x] Add first tretonin acquisition prototype
- [ ] Add specialized Goa'uld pharmaceutical production
- [ ] Evaluate tretonin-specific research and upgraded yields
- [ ] Evaluate automated administration and drug-policy integration
- [ ] Evaluate tolerance, side effects and faction-specific access
- [ ] Extend dependency progression to caravans and world pawns
- [ ] Add direct lethal consequences if needed
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
- [x] Add first Tok'ra therapeutic-healing behavior
- [ ] Add tretonin

## Factions, visuals and equipment
- [ ] Create the Goa'uld System Lords faction
- [ ] Add Jaffa pawn kinds
- [ ] Add Goa'uld-faction facial tattoos or markings for Jaffa
- [ ] Add variants by System Lord or Goa'uld faction where practical
- [ ] Keep Free Jaffa visually distinct from Goa'uld-aligned Jaffa
- [ ] Add Ma'Tok staff weapon
- [x] Add Zat'nik'tel first-shot incapacitation prototype
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


## Future Prim'ta cultural-thought refinements
- [x] Add a light `awaiting Prim'ta` thought from age `10`
- [x] Add a temporary `received Prim'ta` thought after first implantation
- [ ] Differentiate loyalist, traditional and Free Jaffa reactions later
- [ ] Integrate faction and optional Ideology context when available


## Future Goa'uld queen larva origin
- [x] Introduce a Goa'uld queen as the primary biological source of immature symbiotes
- [x] Introduce the first queen representation as a special pawn; evaluate natural acquisition routes later
- [ ] Support hosted and/or hostless queen representations as appropriate
- [x] Turn the current incubation basin into assisted maturation infrastructure
- [ ] Integrate Goa'uld factions, Tok'ra, Free Jaffa, trade, quests and events
- [ ] Revisit this feature after the first Tok'ra iteration


## Future Tok'ra expansion
- [ ] Generate the Tok'ra world faction after pawn-group definitions exist
- [x] Add developer-triggered peaceful Tok'ra visitor prototype
- [x] Enable controlled low-frequency random Tok'ra visits
- [ ] Add Tok'ra settlements, traders and diplomacy
- [ ] Add voluntary-host recruitment events and quests
- [x] Add first developer-spawnable Tok'ra host pawn prototype
- [ ] Add Tok'ra-specific host visuals and cultural content
- [ ] Add named Tok'ra content when the generic foundation is stable
- [ ] Revisit queen-origin biology and Egeria-inspired content afterward


## 0.1.45-dev — Voluntary therapeutic Tok'ra implantation

- [x] Add a dedicated command to free Tok'ra symbiotes
- [x] Filter targets against the configured serious-pathology list
- [x] Restrict the first prototype to compatible nearby player-controlled humanoids
- [x] Ask for explicit confirmation before transfer
- [x] Reuse the existing recent-implantation and active-host conversion flow
- [x] Preserve generic Tok'ra voluntary implantation for regression testing


## Future therapeutic Tok'ra hosting
- [x] Detect compatible humanoids with serious or potentially fatal illnesses
- [x] Add explicit voluntary therapeutic implantation
- [x] Add first automatic cure prototype for configured serious pathologies
- [ ] Move pathology rules into configurable Defs and extend modded-disease support
- [ ] Keep the long-term narrative cost: the pawn becomes a durable Tok'ra host
- [ ] Support vanilla and modded diseases through configurable rules
- [ ] Integrate later with events, quests, visitors, recruitment and diplomacy

- [x] Add a powered dedicated Prim'ta preservation basin prototype without replacing refrigerators.


## 0.1.75-dev — Debug and player-facing diagnostics consolidation

- [x] Add a persistent GateRim SG-1 advanced-debug setting
- [x] Keep RimWorld developer mode as an automatic debug override
- [x] Gate routine informational C# traces while preserving warnings and errors
- [x] Hide raw symbiote IDs and cooldown ticks from normal inspection panels
- [x] Keep contextual Tok'ra-offer and ritual information readable
- [x] Document remaining French translation-report follow-up


## 0.1.76-dev — Ma'Tok plasma-impact damage prototype

- [x] Preserve the existing thermal plasma injury against biological targets
- [x] Add reduced structural impact against non-organic pawns
- [x] Add reduced structural impact against buildings and turrets
- [x] Keep area explosions deferred
- [x] Add reusable Goa'uld energy-technology classification
- [ ] Add future Replicator resistance as a separate milestone


## 0.1.77-dev — Zat'nik'tel incapacitation prototype

- [x] Add craftable Zat'nik'tel sidearm Def
- [x] Add temporary dedicated item and projectile graphics
- [x] Add first-shot vanilla Stun neutralization
- [x] Add weak EMP follow-up against non-organic pawns
- [x] Add weak EMP follow-up against buildings and turrets
- [x] Reuse Goa'uld energy-technology classification
- [ ] Tune stun and EMP values after hands-on testing
- [ ] Add persistent second-shot lethal state in a separate milestone
- [ ] Add third-shot disintegration only after reviewing gameplay value
- [ ] Decide later whether any Jaffa loadout should receive a Zat automatically


## 0.1.78-dev — SG-team field uniform prototype

- [x] Add a dedicated SG-team apparel category
- [x] Add one lightweight olive-drab BDU uniform
- [x] Cover torso, shoulders, arms and legs on the `OnSkin` layer
- [x] Add vanilla tailoring-workstation recipes
- [x] Add temporary inventory and body-type-specific graphics
- [x] Keep tactical boots, gloves and vest separate
- [ ] Validate apparel compatibility and visuals in game
- [ ] Add later environment and role variants only after the baseline is stable


## 0.1.79-dev — SG tactical boots prototype

- [x] Add separate SG tactical-boots apparel Def
- [x] Reuse the SG-team apparel category
- [x] Keep the boots compatible with the OnSkin field uniform
- [x] Limit protection to feet rather than full legs
- [x] Add vanilla hand and electric tailoring-bench recipes
- [x] Add temporary inventory and body-type-specific graphics
- [ ] Validate visuals and compatibility in game
- [ ] Add SG tactical gloves as a separate modular item


## 0.1.80-dev — SG tactical gloves prototype

- [x] Add separate SG tactical-gloves apparel Def
- [x] Reuse the SG-team apparel category
- [x] Keep the gloves compatible with the OnSkin field uniform
- [x] Cover hands with modest protection
- [x] Add vanilla hand and electric tailoring-bench recipes
- [x] Add temporary inventory and body-type-specific graphics
- [ ] Validate visuals and compatibility in game
- [ ] Add SG tactical vest as a separate modular item


## 0.1.81-dev — SG tactical vest prototype

- [x] Add separate SG tactical-vest apparel Def
- [x] Reuse the SG-team apparel category
- [x] Keep the vest compatible with the OnSkin field uniform and limb-specific Middle-layer equipment
- [x] Cover torso and shoulders with moderate protection
- [x] Add vanilla hand and electric tailoring-bench recipes
- [x] Add temporary inventory and body-type-specific graphics
- [x] Complete the first generic four-piece SG-team field-equipment baseline
- [ ] Validate visuals and compatibility in game
- [ ] Review the remaining pre-0.2 roadmap before adding variants


## 0.1.82-dev — Black and desert SG-team uniform variants

- [x] Add a black SG-team field-uniform variant
- [x] Add a desert SG-team field-uniform variant
- [x] Keep both variants mechanically identical to the olive-drab baseline
- [x] Reuse the existing SG-team apparel category and modular accessories
- [x] Recolor the validated temporary body-type and facing-specific graphics
- [x] Omit forest camouflage for now because it would be too close to olive-drab at RimWorld scale
- [ ] Validate black and desert visuals in game
- [ ] Decide after testing whether a desert tactical-vest variant is actually needed


## 0.2.0-dev — Stranded SG-team starter scenario

- [x] Add a selectable stranded SG-team scenario
- [x] Generate four adult player starters
- [x] Dress starter pawns automatically with the olive-drab four-piece SG-team set
- [x] Add temporary vanilla Tau'ri firearms as distributable supplies
- [x] Add four cloth bedrolls
- [x] Add emergency food, medicine and resource crates
- [x] Avoid preconstructed workshops
- [x] Keep the Stargate unusable in the scenario narrative
- [ ] Validate scenario selection and new-game start in RimWorld
- [ ] Continue with the playable Goa'uld world-faction baseline


## 0.2.0-dev-r1 — Refine stranded SG-team starter scenario

- [x] Add a dedicated SGC expedition player faction
- [x] Reduce pawn selection to four candidates for four slots
- [x] Reject starters incapable of violence
- [x] Resolve the translated narrative visibly in the scenario editor
- [x] Open the translated narrative reliably at map start
- [ ] Validate the refined scenario in RimWorld
- [ ] Add an optional SG-team helmet as a separate equipment milestone


## 0.2.0-dev-r2 — Optional SG-team field helmet prototype

- [x] Add an open-face SG-team field helmet
- [x] Cover `UpperHead` on the `Overhead` layer
- [x] Keep protection moderate and below Jaffa helmet values
- [x] Add four optional helmets to the stranded-team supply crates
- [x] Avoid automatic helmet equipment on starter pawns
- [x] Add temporary inventory and facing-specific graphics
- [ ] Validate the helmet and scenario supplies in RimWorld
- [ ] Continue with the playable Goa'uld world-faction baseline


## 0.2.1-dev — Playable Goa'uld world-faction baseline

- [x] Turn the Goa'uld System Lord prototype into a visible hostile world faction
- [x] Generate exactly one Goa'uld faction on new worlds
- [x] Use a reduced settlement-generation weight
- [x] Add a settlement-defense pawn-group profile
- [x] Preserve permanent hostility
- [x] Keep generic vanilla raid selection blocked
- [x] Add one low-frequency natural direct-assault incident
- [x] Reuse explicit `ImmediateAttack`, `canSteal = false` and `canKidnap = false`
- [x] Keep natural abduction and destruction doctrines disabled
- [x] Preserve controlled developer incidents for regression testing
- [ ] Validate world generation, settlements and the first natural raid in RimWorld
- [ ] Continue with the Free Jaffa world-faction baseline


## 0.2.1-dev-r1 — Remove invalid Goa'uld faction icon fields

- [x] Remove invalid `FactionDef` fields `expandingIconTexture` and `homeIconPath`
- [x] Keep default RimWorld world-map settlement rendering
- [x] Preserve settlement naming, gold-toned faction color and world-generation behavior
- [ ] Relaunch RimWorld and confirm that the two XML startup errors are gone


## 0.2.1-dev-r2 — Expose Goa'uld faction and normalize French vocabulary

- [x] Add RimWorld 1.6 configurable-faction fields
- [x] Show one Goa'uld faction by default in world creation
- [x] Prevent duplicate Goa'uld factions through the Add menu
- [x] Use valid `factionIconPath` and `settlementTexturePath` fields
- [x] Remove deprecated world-generation compatibility fields
- [x] Replace unnecessary French `pawn` jargon with contextual player-facing terms
- [ ] Reopen Create World and validate the visible Goa'uld faction row
- [ ] Generate a new planet and validate Goa'uld settlements


## 0.2.1-dev-r3 — Capitalize Goa'uld name and restore SGC faction icon

- [x] Capitalize the French Goa'uld faction name
- [x] Add explicit vanilla-style SGC expedition faction icon path
- [x] Add explicit default SGC settlement texture path
- [ ] Reopen world creation and validate both faction icons


## 0.2.2-dev — Free Jaffa world-faction baseline

- [x] Add one visible neutral Free Jaffa faction by default
- [x] Add limited world settlements
- [x] Add Combat and Settlement pawn-group profiles
- [x] Add Free Jaffa warrior and guard PawnKindDefs
- [x] Reuse Jaffa lineage, Ma'Tok and modular armor loadouts
- [x] Extend one-time Prim'ta provisioning to generated Free Jaffa combatants
- [x] Restrict automatic forehead marks to Goa'uld-domain factions
- [x] Keep Free Jaffa unmarked by default
- [x] Keep trade, aid, quests, visitors and natural raids disabled for the baseline
- [ ] Validate world generation, relations, defenders and visual distinction in RimWorld
- [ ] Add peaceful Free Jaffa encounters as a separate milestone


## 0.2.2-dev-r1 — Align faction xenotype summaries

- [x] Declare `SG1_Jaffa = 100%` for the Free Jaffa faction summary
- [x] Declare provisional `SG1_Jaffa = 100%` for the current Goa'uld servant baseline
- [x] Remove misleading human-baseliner summaries from world creation
- [x] Keep true Goa'uld hosts deferred until persistent symbiote initialization is connected
- [x] Record a later thematic faction-icon visual pass
- [ ] Reopen Create World and validate both faction xenotype summaries
- [ ] Add true minority Goa'uld host profiles in a dedicated later milestone


## 0.2.2-dev-r2 — Expand faction limits and generate provisional leaders

- [x] Keep one Goa'uld-domain faction and one Free Jaffa faction by default
- [x] Allow players to add extra instances manually from Create World
- [x] Add an explicit generated Free Jaffa leader
- [x] Add an explicit provisional Goa'uld-domain Jaffa commander
- [x] Remove `Faction leader ... is null` logs without creating incomplete Goa'uld hosts
- [ ] Validate extra-faction additions in Create World
- [ ] Validate clean new-game logs without missing faction leaders
- [ ] Replace provisional Goa'uld-domain commanders with true persistent host profiles later


## 0.2.3-dev — Goa'uld host-caste baseline

- [x] Add ordinary generated `Goa'uld` host caste
- [x] Add generated `Goa'uld System Lord` host leader kind
- [x] Initialize generated hosts with one persistent adult Goa'uld symbiote identity
- [x] Scan Goa'uld faction leaders and spawned settlement pawns
- [x] Replace provisional Jaffa-domain commanders with true System Lord hosts
- [x] Add ordinary Goa'uld hosts only to Settlement groups
- [x] Keep direct raids Jaffa-only
- [x] Preserve acquired-host Hediff architecture instead of forcing the legacy xenotype
- [ ] Validate faction leader persistent data in RimWorld
- [ ] Validate minority settlement Goa'uld hosts
- [ ] Validate save and reload persistence
- [ ] Add cultural backstories separately
- [ ] Add active-host extraction separately


## 0.2.3-dev-r1 — Stabilize Goa'uld settlement castes and Tau'ri backstories

- [x] Add settlement-only capped Goa'uld Jaffa warrior and guard profiles
- [x] Guarantee readable settlement groups with per-group caps: 7 warriors, 2 guards, 1 host
- [x] Keep direct raids Jaffa-only and unchanged
- [x] Extend one-time Prim'ta initialization to settlement-only Jaffa profiles
- [x] Replace the narrow provisional SGC `Civil` filter with a broad vanilla-compatible Tau'ri filter
- [ ] Validate settlement caste composition in RimWorld
- [ ] Validate clean SGC starter generation logs
- [ ] Continue with dedicated cultural backstories


## 0.2.3-dev-r2 — Provisional Goa'uld attire and generated-host healing

- [x] Prevent ordinary generated Goa'uld hosts from appearing naked
- [x] Prevent generated System Lord hosts from appearing naked
- [x] Use temporary vanilla pants, shirt and duster
- [x] Heal a narrow list of chronic biological ailments after host initialization
- [x] Preserve scars, missing parts and combat injuries
- [x] Keep dedicated Goa'uld apparel for a later visual-production pass
- [ ] Validate clothing on ordinary hosts and System Lords
- [ ] Validate that lumbago and similar chronic conditions are removed
- [ ] Validate persistence after save and reload


## 0.2.3-dev-r3 — Fix provisional Goa'uld shirt reference

- [x] Replace invalid `Apparel_ButtonDownShirt`
- [x] Use vanilla `Apparel_CollarShirt`
- [x] Preserve temporary Goa'uld outfit and host-healing behavior
- [ ] Relaunch RimWorld and confirm that both unresolved ThingDef messages are gone


## 0.2.4-dev — Cultural backstory baseline

- [x] Preserve vanilla Earth histories for Tau'ri humans
- [x] Add optional SGC adulthood careers
- [x] Add shared Jaffa childhood histories
- [x] Add separate Goa'uld-domain and Free Jaffa adulthood histories
- [x] Add off-world human childhood histories
- [x] Add Goa'uld-host, System Lord and generated Tok'ra careers
- [x] Prevent dedicated histories from leaking into unrelated vanilla generation
- [x] Preserve existing voluntary Tok'ra host histories
- [ ] Validate generation samples and clean logs in RimWorld
- [ ] Add contextual social rules separately


## 0.2.4-dev-r1 — Define adulthood backstory body types

- [x] Add standard male and female body types to all `38` dedicated adulthood stories
- [x] Preserve childhood stories unchanged
- [x] Prevent generated off-world pawns from receiving an undefined body type
- [x] Reapply the corrected provisional Goa'uld shirt reference as a safety overwrite
- [ ] Generate a fresh Goa'uld city and validate visible Jaffa, Goa'uld and portraits
- [ ] Generate a fresh Free Jaffa city and validate visible Jaffa and portraits
- [ ] Confirm `Getting apparel graphic with undefined body type` no longer appears
- [ ] Confirm portrait-rendering null-reference errors no longer appear


## 0.2.5-dev — Contextual social baseline

- [x] Add centralized social-identity utility
- [x] Add Free Jaffa distrust toward active Goa'uld hosts
- [x] Add stronger Tok'ra hostility toward active Goa'uld hosts
- [x] Add mild Free Jaffa caution toward Goa'uld-marked Jaffa
- [x] Add local discipline thought for Goa'uld-domain Jaffa near their System Lord
- [x] Keep the effects contextual rather than absolute
- [x] Avoid automatic attacks and permanent forced relationships
- [ ] Validate all opinion thoughts in RimWorld
- [ ] Validate System Lord proximity activation and deactivation
- [ ] Validate save and reload behavior
- [ ] Consider voluntary Tok'ra-host positive memories separately
- [ ] Add optional Ideology integration later


## 0.2.6-dev — Free Jaffa peaceful visitors baseline

- [x] Add low-frequency storyteller incident `SG1_FreeJaffaPeacefulVisitors`
- [x] Select one existing visible non-hostile Free Jaffa world faction
- [x] Support multiple manually configured Free Jaffa communities
- [x] Avoid hidden fallback-faction creation on old saves
- [x] Add dedicated Free Jaffa `Peaceful` pawn-group profile
- [x] Reuse warrior and guard profiles with Prim'ta, Ma'Tok and modular armor
- [x] Target two to four armed visitors
- [x] Keep trade, quests, aid, recruitment and natural raids deferred
- [ ] Validate developer-triggered visitor incident
- [ ] Validate natural storyteller selection after day 10
- [ ] Validate non-hostile-faction selection and hostile-faction exclusion
- [ ] Consider civilian or diplomatic Free Jaffa profiles later


## 0.2.7-dev — Hidden Tok'ra world-presence baseline

- [x] Generate one hidden Tok'ra faction anchor in new worlds
- [x] Keep Tok'ra absent from configurable world-creation lists
- [x] Keep zero Tok'ra settlements
- [x] Keep natural Tok'ra raids forbidden
- [x] Add old-save migration through a GameComponent
- [x] Reuse one saved faction across all existing Tok'ra incidents
- [x] Initialize the internal Tok'ra leader with a persistent symbiote identity
- [x] Preserve visitor, therapeutic and medical-support behavior
- [ ] Validate a fresh game
- [ ] Validate a pre-0.2.7 save migration
- [ ] Validate all existing Tok'ra incidents after save and reload
- [ ] Consider hidden Tok'ra quest sites separately


## 0.2.8-dev — Stargate crafting-research baseline

- [x] Add dedicated GateRim SG-1 research tab
- [x] Add Jaffa weaponry project after vanilla Gunsmithing
- [x] Add Jaffa armor project after vanilla Flak armor
- [x] Add SGC field-equipment project after vanilla Complex clothing
- [x] Add Goa'uld-biotechnology project after vanilla Drug production
- [x] Gate local crafting and specialized-basin construction only
- [x] Preserve immediate use of captured, gifted and scenario-supplied items
- [x] Validate all four projects in RimWorld
- [x] Validate all crafting and construction locks before research
- [x] Validate unlocks after research completion
- [x] Add rare natural Zat'nik'tel acquisition through Goa'uld Jaffa guards
- [x] Add rare natural Goa'uld queen arrival and player-controlled extraction
- [ ] Validate queen arrival, duplicate prevention and cooldown persistence
- [ ] Add specialized queen infrastructure and faction-linked acquisition later


## 0.2.11-dev — Balance queen-origin Prim'ta acquisition

- [x] Make rare queen arrival less early
- [x] Lower natural queen-arrival chance
- [x] Increase queen-arrival refire delay
- [x] Increase queen extraction recovery to three days
- [x] Increase assisted maturation meat cost
- [x] Increase assisted maturation work amount
- [x] Preserve one immature symbiote per extraction
- [x] Preserve `SG1_GoauldBiotechnology` as the research gate
- [ ] Validate fresh queen arrival through developer tools
- [ ] Validate extraction cooldown after save/reload
- [ ] Validate assisted maturation cost and work amount
- [ ] Validate incident refusal while an existing player queen is alive


## 0.2.12-dev — Hidden Tok'ra cell cache baseline

- [x] Add rare hidden Tok'ra cell cache incident
- [x] Reuse persistent hidden Tok'ra faction
- [x] Keep no Tok'ra settlements
- [x] Keep no Tok'ra raids
- [x] Keep no traders, recruitment or military aid
- [x] Spawn only modest medical supplies
- [x] Exclude wary trust
- [x] Update `docs/PROJECT_STATE.md`
- [ ] Validate fresh-game incident
- [ ] Validate old-save migration and incident reuse
- [ ] Validate no duplicate Tok'ra faction
- [ ] Validate save/reload after cache placement


## 0.2.13-dev — Tok'ra safehouse signal baseline

- [x] Add rare Tok'ra safehouse signal incident
- [x] Reuse persistent hidden Tok'ra faction
- [x] Keep no world site yet
- [x] Keep no settlement, trader, recruitment, loot, military aid or raid
- [x] Add tiny trust gain
- [x] Exclude wary trust
- [x] Update `docs/PROJECT_STATE.md`
- [ ] Validate fresh-game incident
- [ ] Validate old-save migration and incident reuse
- [ ] Validate +1 trust persistence after save/reload
- [ ] Validate no duplicate Tok'ra faction


## 0.2.14-dev — Tok'ra safehouse lead tracker baseline

- [x] Add persistent Tok'ra safehouse lead tracker
- [x] Cap provisional leads at 3
- [x] Make safehouse signal store one lead
- [x] Preserve +1 Tok'ra trust gain
- [x] Keep no world site yet
- [x] Update `docs/PROJECT_STATE.md`
- [ ] Validate signal stores a lead
- [ ] Validate lead count persists after save/reload
- [ ] Validate lead cap at 3
- [ ] Validate no world object or site is created


## 0.2.15-dev — Tok'ra safehouse lead cache baseline

- [x] Add lead-consuming Tok'ra follow-up cache
- [x] Consume one stored safehouse lead
- [x] Reuse persistent hidden Tok'ra faction
- [x] Keep no world site yet
- [x] Keep rewards modest
- [x] Keep no trader, recruitment, military aid or raid
- [x] Update `docs/PROJECT_STATE.md`
- [ ] Validate no trigger when leads are 0
- [ ] Validate one lead is consumed
- [ ] Validate cache contents
- [ ] Validate save/reload after consumption


## 0.2.16-dev — Hidden Tok'ra safehouse world marker

- [x] Add temporary Tok'ra safehouse world marker
- [x] Consume one stored safehouse lead
- [x] Reuse persistent hidden Tok'ra faction
- [x] Keep marker non-enterable
- [x] Keep no generated map yet
- [x] Prevent duplicate active markers
- [x] Keep no trader, recruitment, loot, military aid or raid
- [x] Update `docs/PROJECT_STATE.md`
- [x] Validate marker creation
- [x] Validate marker expiration
- [x] Validate save/reload with active marker
- [x] Validate no duplicate marker


## 0.2.17-dev — Enterable hidden Tok'ra safehouse site

- [x] Add a vanilla-based enterable safehouse site
- [x] Consume one stored safehouse lead
- [x] Reuse the persistent hidden Tok'ra faction
- [x] Generate a reduced-size non-hostile map
- [x] Add a modest medical stash
- [x] Expire an unvisited site after 10 days
- [x] Prevent marker and site coexistence
- [x] Add one deterministic developer action for test preparation
- [x] Add one direct developer action for test-site creation
- [x] Keep no trader, recruitment, military aid, hostile pawns or raid
- [x] Add French translations, technical docs and player-wiki drafts
- [x] Validate incident refusal at 0 leads
- [x] Validate site creation and lead consumption
- [x] Validate caravan travel and non-hostile arrival
- [x] Validate generated-map size and medical stash contents
- [x] Validate save/reload before and after map generation
- [x] Validate site cleanup and unvisited expiration
- [x] Validate marker/site duplicate prevention
- [x] Validate no duplicate Tok'ra faction


## 0.2.21-dev — Tok'ra safehouse medical briefing

- Extend the validated non-trading safehouse contact dialogue with a modest medical briefing.
- Award `400` Medicine XP to the selected colonist who performs the once-per-contact exchange.
- Preserve the existing `+1` Tok'ra trust acknowledgement.
- Keep no trader, recruitment, military aid, repeatable reward loop or quest.

## 0.2.18-dev — Non-trading Tok'ra safehouse contact

- [x] Add exactly one peaceful Tok'ra contact to the generated safehouse map
- [x] Reuse `SG1_TokraVoluntaryHost` and the persistent hidden Tok'ra faction
- [x] Keep the contact on site through a three-day peaceful visit duty
- [x] Require generated Tok'ra voluntary hosts to be at least 20 years old
- [x] Keep an adulthood backstory on the generated contact
- [x] Keep the contact explicitly non-recruitable
- [x] Keep no trader role or trade inventory
- [x] Keep no military aid, raid, permanent settlement or default combat
- [x] Add one deterministic developer verification action
- [x] Add English/French text, technical docs and player-wiki drafts
- [ ] Validate exactly one contact on arrival
- [ ] Validate age 20+, adulthood backstory and persistent Tok'ra faction
- [ ] Validate no trader role and disabled recruitment
- [ ] Validate save/reload with the contact present
- [ ] Validate clean caravan departure and site removal
