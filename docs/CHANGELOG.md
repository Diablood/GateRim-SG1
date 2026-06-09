# Changelog

## 0.1.46-dev — Expand RimWorld-oriented Tok'ra biological healing
- Replaced the narrow hard-coded serious-pathology list with a dynamic RimWorld-oriented treatment filter.
- Reused vanilla `isBad` and `everCurableByItem` as the default signals for visible harmful biological conditions.
- Added explicit exclusions for permanent scars, missing body parts, implants, addictions, withdrawals, dependencies, pregnancy and GateRim SG-1 state Hediffs.
- Added progressive regeneration for non-permanent injuries at `0.05` severity per `60`-tick scan.
- Aggregated repeated labels such as asthma on both lungs in confirmation and healing feedback.
- Reused the same dynamic rule for voluntary therapeutic implantation targets.
- Kept advanced scar and limb regeneration outside this milestone.


## 0.1.45-dev — Add voluntary therapeutic Tok'ra implantation

- Added a dedicated therapeutic implantation command to free Tok'ra symbiotes.
- Restricted therapeutic targets to nearby player-controlled compatible humanoids with a configured serious pathology.
- Added an explicit consent confirmation dialog before the existing implantation transfer begins.
- Reused the recent-implantation conversion and active-host healing flows without changing generic Tok'ra voluntary implantation.
- Added English and French keyed translations plus player-wiki documentation.


## 0.1.44-dev
- Add `GameComponent_TokraTherapeuticHosting`.
- Scan spawned active Tok'ra hosts every `60` ticks.
- Reuse persistent adult-symbiote origin data to distinguish Tok'ra from Goa'uld hosts.
- Cure a deliberately narrow configured serious-pathology list.
- Keep injuries, scars and unlisted illnesses untouched.
- Keep Goa'uld hosts unchanged.
- Add bilingual healing feedback, technical documentation and player-wiki drafts.

## 0.1.43-dev
- Enable low-frequency storyteller-selected Tok'ra peaceful visits.
- Set `baseChance = 0.10`.
- Delay the first natural visit until day `15`.
- Add a `30`-day minimum refire interval.
- Keep developer-triggered incident testing available.
- Keep the hidden Tok'ra faction disconnected from world generation, settlements, traders and diplomacy.
- Remove `(test)` from English and French visitor labels.
- Refresh the English and French hidden-faction descriptions.
- Rename visitor logs to reflect natural incident support.
- Update technical documentation and player-wiki drafts.

## 0.1.42-dev
- Create the hidden Tok'ra runtime faction with explicit `FactionGeneratorParms`.
- Add the developer-triggered `SG1_TokraPeacefulVisitors` incident.
- Add `IncidentWorker_TokraPeacefulVisitors`.
- Create or reuse one persistent hidden Tok'ra faction instance.
- Reuse the valid nested Tok'ra `Peaceful` pawn-group profile.
- Spawn one to three non-player Tok'ra voluntary-host visitors.
- Reuse vanilla peaceful visitor and departure behavior.
- Keep storyteller chance at `0`.
- Keep settlements, traders and random world generation disabled.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.41-dev
- Replace invalid standalone `PawnGroupMakerDef` entries with nested Tok'ra `FactionDef.pawnGroupMakers`.
- Add one Tok'ra `Combat` profile and one Tok'ra `Peaceful` profile.
- Add the required `maxPawnCostPerTotalPointsCurve`.
- Reuse `SG1_TokraVoluntaryHost` as the first Tok'ra pawn option.
- Keep the hidden Tok'ra faction disconnected from automatic world generation.
- Preserve the staged rollout before visitors, traders and world settlements.
- Document the future therapeutic Tok'ra-hosting path.
- Add technical documentation and player-wiki drafts.

## 0.1.40-dev
- Define a neutral `initialResistanceRange` for the humanlike Tok'ra voluntary-host prototype.
- Add the developer-spawnable `SG1_TokraVoluntaryHost` PawnKindDef.
- Reuse the vanilla human body and player-colony control for the first Tok'ra host pawn.
- Add `GameComponent_TokraHostPrototypeInitializer`.
- Initialize one active adult symbiote with persistent Tok'ra origin.
- Persist initialized pawn IDs and prevent artificial replacement after later removal.
- Reuse the shared adult Goa'uld-family host Hediff.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.39-dev
- Remove unsupported Tok'ra `FactionDef` placeholder fields and add the required raid-loot curve.
- Add the hidden non-generated `SG1_Tokra` faction foundation.
- Add the dev-spawnable `SG1_TokraSymbiote` pawn variant.
- Add explicit persistent-origin creation for free adult symbiotes.
- Disable forced implantation, ritual implantation and autonomous hunting for Tok'ra.
- Add nearby player-controlled voluntary-host targeting.
- Preserve Tok'ra origin through save/reload and extraction.
- Return extracted Tok'ra identities as the non-hunting Tok'ra pawn variant.
- Generalize shared adult-host descriptions for Goa'uld and Tok'ra origins.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.38-dev
- Register `SG1_GoauldRitualBasin` with `<tickerType>Normal</tickerType>` so the timed ceremony progresses.
- Add `CompProperties_JaffaPrimtaCeremony`.
- Add `Comp_JaffaPrimtaCeremony`.
- Extend the Goa'uld ritual basin with a formal Jaffa Prim'ta ceremony command.
- Require an eligible nearby Jaffa and one nearby physical larva.
- Add a persistent `600`-tick ceremony with manual and automatic cancellation.
- Consume the larva only after successful completion.
- Reuse existing Prim'ta dependency relief and first-implantation cultural memory.
- Keep the medical implantation operation available as a separate workflow.
- Document a future Goa'uld-queen larva-origin iteration.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.37-dev
- Add `SG1_PrepareTretoninDoses`.
- Reuse the vanilla `DrugLab` for the first player-usable tretonin-production route.
- Require one physical Prim'ta larva and one medicine unit.
- Produce five physical tretonin doses.
- Require Intellectual skill `6`.
- Preserve the dedicated Goa'uld medical-products storage category.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.36-dev
- Add the physical `SG1_TretoninDose` item.
- Add the dedicated `SG1_GoauldMedicalProducts` category.
- Add the `SG1_AdministerTretonin` health-tab operation.
- Add the temporary `SG1_TretoninSubstitution` Hediff.
- Suppress Jaffa puberty dependency for one day after administration.
- Remove existing deficiency immediately when tretonin is administered.
- Keep tretonin outside vanilla generic medicine.
- Add bilingual text, temporary texture, technical documentation and player-wiki drafts.

## 0.1.35-dev
- Add the situational `SG1_AwaitingPrimta` thought.
- Apply a light `-1` mood effect to compatible Jaffa aged `10+` without Prim'ta.
- Add the temporary `SG1_ReceivedPrimta` memory.
- Grant `+3` mood for `5` days after the first successful implantation.
- Add `GameComponent_JaffaPrimtaCulturalThoughts`.
- Persist first-implantation records and prevent repeated bonuses after reimplantation.
- Keep cultural mood separate from medical dependency.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.34-dev
- Add the progressive `SG1_JaffaPrimtaDependency` Hediff.
- Add `GameComponent_JaffaPrimtaDependency`.
- Start biological dependency at `12` biological years for compatible Jaffa without Prim'ta.
- Increase deficiency severity by `0.1` per in-game day.
- Add four progressive immune and recovery stages.
- Remove dependency immediately after successful Prim'ta implantation.
- Add bilingual messages, logs, technical documentation and player-wiki drafts.

## 0.1.33-dev
- Add a `10` biological-year minimum age for Jaffa Prim'ta implantation.
- Centralize age eligibility in `JaffaPrimtaUtility`.
- Hide the implantation operation for younger compatible Jaffa.
- Add a defensive worker check and bilingual rejection message.
- Preserve physical-larva, medicine, lineage and duplicate-implantation requirements.
- Add technical documentation and player-wiki drafts.

## 0.1.32-dev
- Add `CompProperties_PrimtaLarvaTemperature`.
- Add `Comp_PrimtaLarvaTemperature`.
- Preserve vanilla frozen and refrigerated rotting behavior.
- Accelerate larva deterioration to `×2` from `25 °C`.
- Accelerate larva deterioration to `×3` from `40 °C`.
- Display the current larva temperature, condition and effective deterioration rate.
- Keep deep-freezing penalties outside this first prototype.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.31-dev
- Add the custom non-food `SG1_GoauldBiologicalProducts` storage category.
- Nest Goa'uld biological products under vanilla `ResourcesRaw`.
- Move `SG1_PrimtaLarva` out of `Manufactured`.
- Keep the living larva outside vanilla raw-food categories such as `AnimalProductRaw`.
- Add a final object-category audit TODO.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.30-dev
- Make physical `SG1_PrimtaLarva` items perishable.
- Add vanilla `CompProperties_Rottable` to Prim'ta larvae.
- Set larvae to rot after `6` days.
- Destroy fully rotted larvae.
- Preserve the existing incubation-to-implantation loop.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.29-dev
- Require `10` units of raw meat for `SG1_IncubatePrimtaLarva`.
- Accept the vanilla `MeatRaw` category.
- Allow mixed raw-meat stacks.
- Preserve Handling work, Animals skill `4` and the dedicated incubation work giver.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.28-dev
- Add the constructible `SG1_PrimtaIncubationBasin`.
- Add the `SG1_IncubatePrimtaLarva` work bill.
- Register `SG1_DoBillsPrimtaIncubation` so pawns can detect and prioritize the basin.
- Use Handling work with an explicit Animals skill requirement of `4`.
- Produce one physical `SG1_PrimtaLarva` without developer spawning.
- Keep the first incubation bill ingredient-free for isolated gameplay-loop validation.
- Add French text, a temporary basin graphic, technical documentation and player-wiki drafts.

## 0.1.27-dev
- Add the physical `SG1_PrimtaLarva` item resource.
- Make Prim'ta larvae haulable, stackable and storable.
- Require one larva and one medicine for `SG1_ImplantJaffaPrimta`.
- Let the vanilla bill flow haul and consume the larva.
- Add a defensive C# larva-ingredient validation step.
- Add French text, a temporary larva graphic, technical documentation and player-wiki drafts.

## 0.1.26-dev
- Add the `SG1_ImplantJaffaPrimta` medical operation.
- Add `Recipe_ImplantJaffaPrimta`, derived from vanilla `Recipe_Surgery`.
- Restrict the operation to compatible inherited Jaffa genes.
- Reject duplicate Prim'ta implantation.
- Attach lifecycle logging to `SG1_JaffaPrimta`.
- Require Medicine `4`, one medicine ingredient and medical work time.
- Keep the first prototype independent from a physical larva resource.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.25-dev
- Add the constructible `SG1_GoauldRitualBasin`.
- Require the free symbiote and selected target near the same basin during controlled ceremonies.
- Save and display the active ritual-basin reference.
- Cancel rituals when the basin is destroyed, removed or out of range.
- Preserve persistent symbiote identity after cancellation.
- Keep the basin-backed fallback independent from optional DLC integrations.
- Add French text, a temporary basin graphic, technical documentation and player-wiki drafts.

## 0.1.24-dev
- Add a `600`-tick core ritual ceremony after explicit target selection.
- Persist ritual target and remaining duration through save and reload.
- Display ritual progress in the free-symbiote inspection panel.
- Add `Cancel ritual`.
- Cancel automatically when the symbiote or selected target becomes invalid, unreachable or out of range.
- Preserve the centralized persistent identity-transfer flow on completion.
- Keep the fallback independent from optional DLC integrations.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.23-dev
- Replace automatic nearest-target ritual selection with explicit map targeting.
- Add a ritual target validator for compatible reachable humanoids within `12` cells.
- Allow the player to choose a farther valid pawn.
- Interrupt autonomous pursuit while controlled ritual targeting starts.
- Preserve the centralized persistent symbiote identity-transfer flow.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.22-dev
- Add the controlled `Ritual implantation` command.
- Reuse the centralized persistent symbiote identity-transfer flow.
- Implant the nearest compatible reachable humanoid within `12` cells.
- Consume the free symbiote pawn after successful ritual implantation.
- Add bilingual text, a temporary ritual icon, technical documentation and player-wiki drafts.

## 0.1.21-dev
- Add `SG1_GoauldAutonomousImplant`.
- Add `JobDriver_GoauldAutonomousImplant`.
- Add periodic nearest-target scans for free Goa'uld symbiotes.
- Pursue reachable compatible humanoids and implant automatically on contact.
- Preserve the existing persistent identity-transfer flow.
- Add an autonomous-hunt toggle for development tests.
- Add a short autonomous cooldown after manual or surgical extraction.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.20-dev
- Add the `SG1_EmergencyExtractGoauldSymbiote` medical operation.
- Add `Recipe_EmergencyExtractGoauldSymbiote`, derived from vanilla `Recipe_Surgery`.
- Use the normal surgery outcome path through `CheckSurgeryFail(...)`.
- Require Medicine `6`, one medicine ingredient and medical work time.
- Preserve the same persistent symbiote identity after successful surgery.
- Keep recent implantation active after failed surgery.
- Keep the immediate extraction command temporarily for regression testing.
- Add French text, technical documentation and player-wiki drafts.

## 0.1.19-dev
- Add `HediffComp_GoauldEmergencyExtraction`.
- Add a manual `Emergency extraction` command during recent implantation.
- Transfer the same persistent identity back into a newly generated free symbiote pawn.
- Add free-symbiote initialization from transferred data.
- Add transfer rollback support if no nearby spawn cell is available.
- Add bilingual text, a temporary command icon, technical documentation and player-wiki drafts.

## 0.1.18-dev
- Add `HediffComp_GoauldImplantationConversion`.
- Convert recent implantation into `SG1_GoauldHostSymbiote` immediately before expiry.
- Transfer the same persistent `GoauldSymbioteData` identity into the active-host state.
- Prevent the temporary state's removal from detaching transferred identity data.
- Add active-host immunity, healing, damage-resistance, lifespan and pain modifiers.
- Preserve the host's original germline xenotype.
- Add bilingual text, technical documentation and player-wiki drafts.

## 0.1.17-dev
- Add persistent identity data to the free Goa'uld symbiote pawn.
- Add the manual adjacent-target `Forced implantation` command.
- Transfer the same `GoauldSymbioteData` object into `SG1_GoauldRecentImplantation`.
- Consume the free symbiote pawn after a successful implantation.
- Reject children under 13, animals, mechanoids and already-implanted pawns.
- Add bilingual `Keyed` strings, a placeholder command icon and lifecycle logging.
- Update technical documentation and player-wiki drafts.

## 0.1.16-dev
- Add persistent `GoauldSymbioteData`.
- Add `HediffComp_GoauldSymbiote` and its properties class.
- Deep-save a unique adult-symbiote identity and host-history fields.
- Attach persistent data to recent implantation.
- Add the manual-test `SG1_GoauldHostSymbiote` state.
- Add bilingual `Keyed` strings for the health-state description.
- Log attach, load and detach lifecycle events through `GR_Log`.
- Update technical documentation and save/load test instructions.

## 0.1.15-dev
- Add a colored `<color=#D9B44A>[GateRim SG-1]</color>` logging prefix.
- Add `build.cmd` for Windows users blocked by PowerShell execution policies.
- Document the Windows build wrapper and colored-log regression test.

## 0.1.14-dev
- Add the first C# project scaffold.
- Add centralized `GR_Log` diagnostics with `[GateRim SG-1]` prefix.
- Add `Message`, `Warning`, `Error`, `WarningOnce` and `ErrorOnce`.
- Add an assembly-load bootstrap smoke test.
- Add Windows PowerShell and Bash build helpers.
- Add build and logging documentation.

## 0.1.13-dev
- Split inherited Jaffa lineage traits from immature-symbiote Prim'ta effects.
- Add `SG1_JaffaLineage`.
- Add `SG1_JaffaPouchPotential`.
- Add `SG1_JaffaSymbioteCompatibility`.
- Reduce `SG1_Jaffa` to inherited lineage genes and lower its combat-power factor.
- Add the persistent XML-only `SG1_JaffaPrimta` Hediff prototype.
- Move immunity, healing, pain, damage-resistance and lifespan modifiers to the Prim'ta Hediff.
- Keep `SG1_JaffaLongevity` as a legacy development Def for compatibility with earlier test saves.
- Add French translations, technical documentation and player-wiki updates.
- Add the future Goa'uld-faction Jaffa facial-marking feature to the roadmap.

## 0.1.12-dev
- Set `SG1_Jaffa` to `<inheritable>true</inheritable>` so Jaffa use a germline/endogene foundation.
- Keep `SG1_GoauldHost` non-heritable because adult Goa'uld possession is acquired during life.
- Document the future split between inherited Jaffa lineage traits and removable symbiote-dependent effects.
- Add French Jaffa text updates.
- Add the genetics-model documentation and update player-wiki drafts.

## 0.1.11-dev
- Add the XML-only `SG1_GoauldRecentImplantation` Hediff prototype.
- Add a visible one-day countdown through `HediffCompProperties_Disappears`.
- Add a temporary pain offset during the critical implantation phase.
- Add French `DefInjected` translations.
- Add technical documentation and manual test instructions.
- Update player-wiki drafts.

## 0.1.10-dev
- Add `tools/sync-wiki.ps1` for Windows PowerShell.
- Add `tools/sync-wiki.cmd` as the recommended Windows wrapper.
- Preserve `tools/sync-wiki.sh` for Bash environments.
- Document cross-platform wiki synchronization commands.
- Keep published wiki pages at the root of `GateRim-SG1.wiki`.

## 0.1.9-dev
- Remove the invalid `<wildness>` field from the free Goa'uld symbiote `RaceProperties`.
- Keep developer-mode spawning as the isolated test method.
- Preserve the current player wiki because this correction does not change player-facing behavior.

## 0.1.8-dev
- Add the XML-only `SG1_GoauldSymbiote` animal-style pawn prototype.
- Add a weak bite attack and disable natural biome spawning.
- Add a temporary local sprite for the free symbiote.
- Add French `DefInjected` translations.
- Update player-wiki drafts for the free-symbiote prototype.
- Add `tools/sync-wiki.sh` to publish drafts directly at the wiki-repository root.

## 0.1.7-dev
- Add the XML-only `SG1_GoauldHost` xenotype prototype.
- Add the `SG1_NaquadahBlood` marker gene.
- Add the provisional `SG1_GoauldLongevity` gene with `LifespanFactor ×5`.
- Add temporary local icons for the new Goa'uld genes.
- Add French `DefInjected` translations for the Goa'uld host prototype.
- Update player-wiki drafts for the Goa'uld host foundation.

## 0.1.6-dev
- Add versioned player-wiki drafts under `docs/wiki/`.
- Add initial player pages for setup, content status, Jaffa, Goa'uld and symbiotes.
- Add `_Sidebar.md` and `_Footer.md` for the GitHub wiki.
- Document the publication workflow for the separate `GateRim-SG1.wiki` repository.
- Add the GitHub wiki link to the main README.

## 0.1.5-dev
- Remove leading and trailing whitespace from the `SG1_Jaffa` description.
- Normalize French `DefInjected` values to prevent unintended whitespace.
- Replace the unresolved `UI/Icons/Genes/Gene_Robust` path.
- Add a local temporary icon for `SG1_JaffaPhysiology`.
- Document minimal isolated testing and the third-party `Ability` category conflict.

## 0.1.4-dev
- Add French `DefInjected` translations for `SG1_Jaffa`.
- Add French `DefInjected` translations for `SG1_JaffaPhysiology`.
- Add French `DefInjected` translations for `SG1_JaffaLongevity`.
- Document the localization workflow.

## 0.1.3-dev
- Add the `SG1_JaffaLongevity` gene.
- Set Jaffa lifespan expectancy to `150%` with `LifespanFactor ×1.5`.
- Add the longevity gene to the `SG1_Jaffa` xenotype.
- Document the future migration of longevity to the symbiote system if appropriate.

## 0.1.2-dev
- Remove the forced `Body_Hulk` gene from `SG1_Jaffa`.
- Add the custom `SG1_JaffaPhysiology` gene.
- Add a temporary `+15` carrying-capacity effect without imposing a visible body shape.
- Document future weighted body-type generation tuning.

## 0.1.1-dev
- Add the `SG1_Jaffa` prototype xenotype.
- Declare the Biotech dependency.
- Add Jaffa design notes and a manual test checklist.
- Update the project roadmap.

## 0.1.0-dev
- Initialize the GateRim SG-1 repository skeleton.
- Add RimWorld 1.6 directory layout.
- Preserve the custom mod icon.
