# Changelog

## 0.3.2-dev - Rework organic Tok'ra observation operation

- Replace the former abstract observation timer with a physical field device and a temporary peripheral observation point.
- Deliver the operation-only device through the shared Tok'ra delivery-zone, communicator and fallback placement order.
- Require an Intellectual-capable colon to retrieve the device, carry it to the marked point and spend time deploying it.
- Keep the deployed device exposed to ordinary map dangers while it records Goa'uld activity for several in-game hours.
- Require the final action to begin from a powered Tok'ra communicator, then physically recover the device and return it before transmission starts.
- Resolve success only after the final transmission completes; recording readiness alone no longer grants success.
- Persist the device, marker, target cell, deployment state, recording deadline and interrupted transmission progress.
- Preserve compatibility with `0.3.0-dev` and `0.3.1-dev` saves, including conversion of an accepted legacy observation into the new physical workflow.
- Add several RP success variants with immediate-repeat prevention.
- Keep the normal communicator report limited to the active player-facing phase while reserving exact state and diagnostics for debug.
- Add grouped observation debug actions for deployment and recording completion without adding another communicator gizmo.
- Keep the observation device and marker non-buildable and absent from Architect categories.
- Raise the assembly version to `0.3.2.0` and the mod metadata version to `0.3.2-dev`.

- Observation deployment now installs the field sensor instead of dropping it as a loose item.
- Data recovery starts directly from the observation site and continues to the communicator as one job.
- Observation hauling jobs now explicitly carry one device, removing the `Invalid count: -1` warning.

## 0.3.1-dev - Rework organic Tok'ra intelligence operation

- Move intelligence-module analysis entirely to the powered Tok'ra secure communicator and remove the former direct module interaction.
- Make the selected colon physically retrieve and carry the delivered module to the communicator before analysis begins.
- Replace the normal communicator inspection and status report with a compact player-facing view; retain the full request catalog and framework diagnostics only in debug mode.
- Keep the operation-generated module non-buildable and explicitly remove it from the Architect menu.
- Let an Intellectual-capable colon choose between a longer cautious analysis and shorter accelerated decoding.
- Persist the chosen method and remaining work so interrupted analysis can resume after another job or save reload.
- Grant `350` Intellectual XP for cautious analysis and `500` for accelerated decoding.
- Give accelerated decoding a chance to leak detectable interference and queue a small delayed Goa'uld Jaffa signal patrol scaled from current vanilla threat points.
- Add a dedicated zero-base-chance signal-patrol incident instead of reusing the developer controlled-raid Def.
- Add three contextual success variants for each outcome family: cautious, accelerated without detection, and accelerated with a patrol warning.
- Persist the last intelligence-result variant and prevent immediate repetition when possible.
- Keep the normal communicator report limited to the currently active operation and durable unique-mission progress; expose exact work, interference and queued-patrol state only in debug diagnostics.
- Extend the single communicator debug menu and RimWorld developer actions with cautious, accelerated and forced-interference controls.
- Retain the former direct-module JobDef and a safe redirect JobDriver solely so `0.3.0-dev` saves made during that job can load and resume through the communicator.
- Remove the two obsolete direct-interaction ThingComp classes.
- Update durable tests, framework documentation, project state and the French player wiki.
- Raise the assembly version to `0.3.1.0` and the mod metadata version to `0.3.1-dev`.

## 0.3.0-dev - Refactor organic operation framework

- Replace the monolithic `GameComponent_TokraOrganicOperationTracker` with `GameComponent_TokraOrganicOperationManager`.
- Persist the single offered or active operation through `TokraOrganicOperationInstance` instead of adding archetype-specific fields directly to the game component.
- Persist post-resolution consequences separately through `TokraOrganicOperationFollowUp`.
- Add a worker registry and one worker for each existing organic archetype: observation, intelligence recovery, wounded-agent care and medical-supply handoff.
- Route acceptance, active ticking and communicator completion through the registered worker while retaining the validated player-facing behavior.
- Keep hidden scheduling, trust-tier weighting, anti-repetition, common resolution and cleanup in the shared manager.
- Deliberately end compatibility with unpublished `0.2.x-dev` saves and require a new game for `0.3.0-dev`.
- Remove the old per-field Scribe migration and load-repair framework.
- Remove the unpublished legacy medical-supply container class, ThingDef and French DefInjected text.
- Add common developer controls to force offers, accept, advance, succeed, fail, expire, inspect, apply pending follow-up consequences and reset organic operations.
- Add one compact communicator debug menu when RimWorld developer mode or the GateRim SG-1 advanced-debug option is active.
- Keep all operation debug controls hidden during normal play.
- Add durable new-save, persistence, duplicate-resolution and debug-visibility checks to `docs/TESTING.md`.
- Rewrite the framework architecture document and update project state and French wiki revision markers.
- Raise the assembly version to `0.3.0.0` and the mod metadata version to `0.3.0-dev`.


## 0.2.53-dev - Consolidate French player wiki

- Rewrite the French wiki home page to reflect the actual playable `0.2.x` scope instead of the early `0.1.6-dev` prototype state.
- Replace obsolete version promises in `Prochain développement majeur` with current development directions.
- Keep `Liens utiles` limited to navigation, installation, status, FAQ and repository references.
- Rewrite the Tok'ra interaction roadmap through the four recurring organic-operation archetypes and the playable relay mission.
- Reorganize the content-status page so implemented `0.2.x` systems are no longer listed under planned content.
- Translate remaining English player-facing prose in Tok'ra operation, delivery-zone, safehouse-contact, decoded-site and reconnaissance pages.
- Remove obsolete English validation notes from public wiki pages.
- Harmonize French labels in the sidebar and expose mission pages without raw wiki-link syntax.
- Correct obsolete Prim'ta documentation for age eligibility, puberty dependency, tretonin support, deep-freezing and implantation paths.
- Correct obsolete Goa'uld documentation for autonomous forced implantation, automatic host conversion and the completed ritual flow.
- Update Tok'ra trust, pawn groups, safehouse leads, medical support and decoded-mission pages to their current implemented state.
- Refresh contradictory FAQ answers that still described existing systems as future additions.
- Add a durable French-wiki consistency checklist to `docs/TESTING.md`.
- Keep gameplay, save data and Defs unchanged.
- Raise the assembly version to `0.2.53.0` and the mod metadata version to `0.2.53-dev`.

## 0.2.52-dev - Add organic Tok'ra medical supply handoff

- Add a fourth recurring Tok'ra organic-operation archetype: a face-to-face logistical handoff of two industrial medicines.
- Allow the offer to appear and be accepted without inspecting or requiring the colony's current medicine reserves.
- Schedule one Tok'ra liaison to enter from a reachable map edge roughly one to two in-game hours after acceptance.
- Send the liaison toward the Tok'ra delivery zone, then a powered communicator, then a reachable point near the colony centre.
- Require a player colon capable of Social to talk to the liaison and open a paused two-choice dialogue.
- Let the player give two industrial medicines directly from accessible colony stocks or cancel the dialogue without resolving the operation.
- Reject the donation cleanly when fewer than two reachable, unforbidden industrial medicines are available.
- Consume exactly two medicine units and grant `350` Social XP only when the donation is confirmed.
- Resolve success immediately on donation and order the liaison to leave without keeping the operation visible on the communicator.
- Keep the completed operation successful if the liaison dies while leaving, while applying a separate qualitative Tok'ra relationship penalty.
- Make the liaison leave and resolve one accepted failure when the six-hour meeting window expires without a donation.
- Retain a minimal legacy container definition only for unpublished `r1/r2` save migration, automatically remove stale containers on load, and remove the obsolete container components and hauling job.
- Reuse the consolidated persistence, cleanup, anti-repetition, single-visible-operation and duplicate-resolution protections.
- Keep `MedicalSupplyHandoff = 4` without renumbering existing archetypes and raise the organic-operation framework save version to `5`.
- Add the short developer action `Force Tok'ra medical resupply offer`, English and French texts, durable tests and wiki documentation.
- Raise the assembly version to `0.2.52.0` and the mod metadata version to `0.2.52-dev`.

## 0.2.51-dev - Add organic Tok'ra wounded agent care

- Add a third recurring Tok'ra organic-operation archetype: shelter and treat a seriously wounded or sick Tok'ra agent.
- Allow the offer to appear before Trusted trust without inspecting the colony's medicine reserves.
- Accept the request through the powered Tok'ra communicator, then receive the patient from a reachable map edge.
- Use normal RimWorld rescue, bed, medicine and tending behavior rather than a special supply-container job.
- Make the patient arrive downed under a temporary symbiote-shock condition that blocks movement and suppresses the usual accelerated Tok'ra recovery until the colony places the patient in a medical bed and performs at least one real treatment.
- Make symbiote shock directly tendable so the operation cannot deadlock when all ordinary injuries or illnesses heal before a doctor treats the patient; initial care is confirmed only after the shock itself has been tended in a player medical bed.
- Keep the patient attached to the active operation while a colon is carrying them during a vanilla rescue, instead of treating the temporary despawned carry state as a disappearance.
- Consider the patient fit to travel once conscious, mobile, medically stable and free from critical untreated conditions; complete the operation only after the patient actually leaves the map.
- Fail the accepted operation once if the patient dies, is captured, disappears or remains unfit when the secure care window closes.
- Keep ignored offers consequence-free, preserve hidden variable delays, and apply the existing anti-repetition weighting so the archetype can recur without immediately repeating.
- Persist the patient reference, stability period and departure state while preserving all `0.2.48` to `0.2.50` operation save keys and enum values.
- Keep the communicator limited to the currently active operation and return immediately to its generic RP state after resolution.
- Add short developer action `Force Tok'ra wounded agent offer`, durable tests, technical documentation and wiki draft.
- Raise the assembly version to `0.2.51.0` and the mod metadata version to `0.2.51-dev`.

## 0.2.50-dev - Consolidate organic Tok'ra operation framework

- Consolidate the two existing organic Tok'ra operation archetypes behind one shared definition registry without adding a new player-visible operation.
- Centralize each archetype's trust-tier weights, hidden timings, Intellectual XP, trust consequences, player-action keys, status keys and optional physical objective.
- Centralize offered, accepted, ready and resolved handling while retaining the existing persisted enum values and Scribe keys used by `0.2.48-dev` and `0.2.49-dev` saves.
- Persist the consolidated observation `Ready` state explicitly as enum value `3`, preserving the legacy `None = 0`, `Offered = 1` and `Accepted = 2` values, and make the advance debug action expose the report-transmission menu immediately.
- Synchronize an elapsed accepted observation to the ready state from ticking, communicator menu queries, interaction handling and legacy-save repair so the right-click action cannot disappear between state checks.
- Add a framework save version and a guarded resolution flag so trust, XP, letters, counters and cleanup cannot be applied twice after reload or repeated completion calls.
- Repair incomplete legacy state on load, recover the active intelligence-module reference when possible and remove stale physical objectives that do not belong to the active operation.
- Route physical objectives through the shared validated Tok'ra delivery helper: delivery zone first, powered communicator second and reachable map edge only as a final fallback.
- Generalize Tok'ra trust outcome handling while retaining the previous observation and intelligence-recovery wrapper methods for compatibility.
- Replace operation-specific progression and expiry debug actions with common advance, fail and reset actions while keeping the two force-archetype actions explicit.
- Shorten the five organic-operation developer-action labels so RimWorld displays them clearly without truncation, and align the durable tests with their exact English names.
- Shorten the earlier Jaffa-mark, Tok'ra safehouse, trust, mission-cache, mission-site, relay and intercepted-threat developer actions using consistent compact labels.
- Preserve all existing operation weights, hidden delays, deadlines, trust gains or losses, Intellectual XP and player-facing flows.
- Add `docs/TESTING_GUIDELINES.md` and rewrite the durable organic-operation checks as standalone, session-ordered procedures with explicit setup, actions, expected results and reload requirements; update `docs/PROJECT_STATE.md`, framework documentation and player wiki drafts.
- Verify trust changes in player-facing tests through qualitative RP feedback rather than requiring hidden raw trust values.
- Raise the assembly version to `0.2.50.0` and the mod metadata version to `0.2.50-dev`.

## 0.2.49-dev - Add Tok'ra organic intelligence recovery

- Add a second preliminary Tok'ra organic-operation archetype available before Trusted contact.
- Let a selected Intellectual-capable colon accept the recovery of a sealed Tok'ra intelligence module through the powered Tok'ra communicator.
- Deliver one sealed intelligence module beside the Tok'ra delivery drop zone when present, otherwise beside a powered communicator, and use a reachable map edge only when neither preferred delivery target exists.
- Reuse the same preferred-cell lookup and vanilla `ThingPlaceMode.Near` placement flow already used by validated Tok'ra medical deliveries and cache incidents.
- Restore the shared Tok'ra delivery utility to its published `v0.2.48-dev` behavior instead of maintaining a separate placement variant for this objective.
- Complete the operation directly at the module without requiring another communicator transmission.
- Grant `+2` Tok'ra trust and `200` Intellectual XP on success, with no material contents or item reward.
- Apply `-1` Tok'ra trust exactly once when an accepted module expires, is destroyed or is otherwise lost.
- Keep ignored offers consequence-free and leave the offer available when no valid delivery location can be generated.
- Persist the active module reference, operation deadline, offer state and anti-repetition context across saves.
- Add trust-tier-specific intelligence-recovery weights and make the existing repeat-weight reduction meaningful across two compatible archetypes.
- Add a non-buildable, non-minifiable and non-deconstructable intelligence-module objective that remains physically destructible.
- Add direct module interaction, a dedicated job, developer validation actions, English/French text and French DefInjected text.
- Clarify the secure communicator description so preliminary observation and intelligence-recovery requests remain distinct from Trusted outgoing support.
- Consolidate durable validation in `docs/TESTING.md`, remove milestone-specific test-plan files, update `docs/PROJECT_STATE.md` and prepare the wiki draft.
- Raise the assembly version to `0.2.49.0` and the mod metadata version to `0.2.49-dev`.

## 0.2.48-dev - Add organic Tok'ra operation opportunities

- Add a persistent scheduler for Tok'ra-initiated opportunities with hidden variable delays and save-persistent active state.
- Add the first preliminary archetype: a discreet Goa'uld-activity observation request available before the Trusted trust tier.
- Require a powered player-controlled Tok'ra secure communicator and an operator capable of Intellectual work, without weakening existing Trusted-tier requirements for manual requests.
- Let the player ignore an unsolicited offer without a trust penalty, while an accepted but missed report reduces trust by `1`.
- Grant `3` Tok'ra trust and `250` Intellectual XP when the completed observation report is transmitted.
- Add trust-tier scheduling ranges, trust-dependent archetype weights, and persisted anti-repetition context for future operation variety.
- Add communicator right-click actions, inspect/status-report integration, developer validation actions, English/French keyed text, JobDef and French DefInjected text.
- Clarify the secure communicator description so preliminary incoming requests are distinct from trusted outgoing support requests.
- Consolidate durable validation in `docs/TESTING.md`, remove milestone-specific test-plan files, update `docs/PROJECT_STATE.md` and prepare the wiki draft.
- Raise the assembly version to `0.2.48.0` and the mod metadata version to `0.2.48-dev`.

## 0.2.47-dev - Add Tok'ra relay operation outcome debrief

- Record the relay-operation outcome only when the resolved temporary site is removed after the caravan has left.
- Schedule an automatic Tok'ra debrief between 6 and 18 in-game hours later without adding another player action.
- Strengthen Tok'ra trust by 5 after a successful discreet sabotage, or reduce it by 3 after destructive mission failure.
- Update the secure-communicator channel report while the debrief is pending and after the final outcome is received.
- Persist the recorded outcome, scheduled contact and completed debrief across saves.
- Migrate completed successful `0.2.46` operations whose temporary relay site has already disappeared.
- Preserve the existing salvage, reinforcement, evacuation and delayed Goa'uld-retaliation behavior without adding a new reward, raid or world action.
- Raise the assembly version to `0.2.47.0`.

## 0.2.46-dev-r5 - Restore complete roofing and remove resolved relay site

- Restore the full roof and room-refog generation pass lost when the dedicated Goa'uld outpost structures were introduced.
- Spawn all perimeter walls and doors before applying constructed roofs across the complete room footprint, preventing unsupported partial roofs during generation.
- Remove the temporary mission map and its world marker after a resolved operation once the reformed caravan has left the map.
- Ignore leftover outpost buildings when cleaning up the completed temporary site; unselected contents are handled by the normal temporary-map removal flow.
- Preserve active-pawn and incoming-transporter safeguards before map cleanup.
- Raise the assembly version to `0.2.46.5`.

## 0.2.46-dev-r4 - Restore caravan reforming and protect enemy outpost structures

- Add a mission-gated variant of the vanilla caravan-reformation component to the Tok'ra relay world site.
- Expose `Reform caravan` only after the relay operation has reached either success or destructive failure.
- Keep the vanilla active-threat restriction: announced reinforcements do not block departure before arrival, while Jaffa currently active on the map do.
- Generate relay walls, doors and defensive barricades from dedicated Goa'uld outpost ThingDefs.
- Keep those enemy structures destructible by weapons while preventing deconstruction until each individual building has been claimed by the player; once claimed, normal vanilla deconstruction remains available even if a new hostile later enters the map.
- Preserve the existing sabotage, reward, reinforcement and retaliation behavior.
- Raise the assembly version to `0.2.46.4`.

## 0.2.46-dev-r3 - Fix defend-base constructor for RimWorld 1.6

- Update the initial Goa'uld/Jaffa garrison LordJob creation to the RimWorld 1.6 `LordJob_DefendBase(Faction, IntVec3, int, bool)` constructor signature.
- Use the vanilla ten-hour defensive delay before an uncompromised garrison may independently escalate, while sabotage or relay damage still activates it immediately.
- Keep the infiltration, destructive-failure and retaliation behavior from `0.2.46-dev-r2` unchanged.
- Raise the assembly version to `0.2.46.3`.

## 0.2.46-dev-r2 - Add guarded infiltration and destructive-failure consequence

- Put the initial Goa'uld/Jaffa garrison on a vanilla defend-base LordJob instead of ordering an immediate assault when the caravan enters the map.
- Let the defenders escalate naturally when attacked or when a hostile Goa'uld building is damaged, and explicitly switch them to assault when sabotage begins.
- Assign the relay control node to the Goa'uld faction so it is treated as an enemy installation rather than a player-owned building with a normal deconstruction order.
- Keep the relay physically destructible; destroying it before sabotage now marks the Tok'ra operation as failed instead of leaving the mission unresolved.
- Cancel the local reinforcement countdown after destructive failure, alert the remaining garrison, and allow evacuation once no active hostiles remain.
- Queue a delayed controlled Goa'uld/Jaffa retaliation against a player home map between roughly two and six days later without revealing the exact timing to the player.
- Persist the mission-failure and retaliation-queued states across saves and display the failed operation state on the world object.
- Raise the assembly version to `0.2.46.2`.

## 0.2.46-dev-r1 - Roof relay outposts and pre-position salvage

- Apply constructed roofs across the complete footprint of every generated relay building.
- Refog enclosed room interiors after generation so the outpost is revealed through normal door entry instead of exposing every room immediately.
- Place the Goa'uld weapon and industrial components during site generation on a vanilla shelf inside the storage room.
- Keep the pre-positioned salvage accessible normally from the beginning of the operation, without an artificial forbidden state or post-sabotage materialization.
- Preserve the existing evacuation rule: the equipment cannot be taken off-map until the relay has been sabotaged and no active hostiles remain.
- Migrate already initialized `0.2.46-dev` mission maps by preparing the shelf cache on the next map tick when the old delayed reward has not yet been granted.
- Raise the assembly version to `0.2.46.1`.

## 0.2.46-dev - Enrich playable Tok'ra relay sabotage site

- Replace the nearly empty relay map with one of three Goa'uld outpost layouts: a command bunker with annex, a split relay station, or a walled courtyard compound.
- Build the layouts from base-game walls, doors, concrete floors, roofs and defensive barricades without adding a new DLC dependency.
- Place the relay control node inside the generated compound and position the initial Jaffa defense around the outpost.
- Grant a modest one-time salvage reward when sabotage completes: one Goa'uld energy weapon, weighted toward a Zat'nik'tel, plus one or two industrial components.
- Spawn the reward directly on the mission map so no additional world action or analysis step is required.
- Persist the planned reward location and reward-granted state for save compatibility.
- Keep threat scaling, sabotage duration, reinforcement timing and evacuation rules unchanged.

## 0.2.45-dev-r5 - Force rebuilt sabotage work and direct Jaffa spawning

- Move sabotage progress onto the relay-device component so work persists across interruptions and saves.
- Use 30,000 work units with Intellectual-based speed: about 12 hours at level 0, 8 hours at level 10, and 6 hours at level 20.
- Keep the duration hidden from the player-facing right-click option.
- Remove `PawnGroupMakerUtility` from both initial defenders and delayed reinforcements because the Goa'uld faction currently has no usable low-point combat group maker.
- Generate Goa'uld/Jaffa defenders directly from the existing warrior and guard PawnKindDefs while still deriving group size from vanilla threat points.
- Send the reinforcement letter only after at least one Jaffa is actually spawned and target that pawn directly.
- Retry failed reinforcement placement up to three times instead of marking an empty wave as arrived.
- Raise the assembly version to `0.2.45.5` so the loaded DLL can be verified in `Player.log`; a clean rebuild is required.

## 0.2.45-dev-r4 - Fix relay sabotage interaction and work duration

- Remove the pawn-specific sabotage estimate from the player-facing right-click option.
- Replace the formatted device label with a fixed translated action label to prevent the empty `(~ h)` text and its associated interaction error.
- Replace the standard wait toil with a persistent tick-by-tick sabotage work counter so the operation cannot complete immediately.
- Keep sabotage speed inversely tied to Intellectual level: about 10 hours at level 0, 5 hours at level 10, and 3.3 hours at level 20.
- Add French DefInjected translations for the relay control node and its active job report.

## 0.2.45-dev-r3 - Rebalance relay sabotage and fix reinforcements

- Scale relay sabotage duration inversely with the operating pawn's Intellectual level.
- Use an estimated duration of about 10 hours at Intellectual 0, 5 hours at 10, and 3.3 hours at 20 while keeping the reinforcement warning at 12 hours.
- Prevent pawns incapable of Intellectual work from starting the sabotage.
- Display the pawn-specific estimated sabotage duration in the right-click option.
- Generate the delayed reinforcement wave directly from Goa'uld Jaffa warrior pawn kinds instead of using a low-point pawn group that could resolve to zero pawns.
- Keep the delayed wave small at one to three Jaffa, derived from the existing reinforcement threat budget.
- Send the arrival letter and message only after at least one reinforcement pawn has spawned, and target the first spawned Jaffa instead of the world site.

## 0.2.45-dev-r2 - Fix relay sabotage mission loading

- Remove the invalid XML `AlertDef`; RimWorld discovers concrete `Alert` subclasses directly.
- Point the relay control node to the existing GateRim SG-1 secure-communicator texture instead of a missing vanilla texture path.
- Keep the reinforcement countdown alert class and all mission behavior unchanged.
- Leave the remaining generic French translation warning for a dedicated translation report if it persists after this load fix.

## 0.2.45-dev-r1 - Fix relay sabotage mission build

- Replace mixed `Thing`/world-object null-coalescing targets with explicit target branches for RimWorld letter and message APIs.
- Use the read-only pawn collection type returned by `MapPawns.AllPawnsSpawned`.
- Keep all playable relay sabotage mission behavior unchanged.

## 0.2.45-dev - Add playable Tok'ra relay sabotage site

- Consolidated the decoded Tok'ra relay flow into a single player-facing world action.
- Launching the operation now generates a temporary local mission map.
- The mission map contains a Goa'uld relay control node to sabotage and a hostile Goa'uld/Jaffa defense scaled from vanilla threat points.
- A colonist must right-click the relay control node and work on the sabotage for a short duration.
- Starting the sabotage triggers a visible reinforcement countdown.
- If the player leaves before the countdown expires, the announced reinforcements do not block evacuation.
- If the countdown expires, a smaller Goa'uld/Jaffa reinforcement wave arrives and becomes an active threat.
- After sabotage, the caravan can leave once no active hostile enemies remain on the map.
- Preserved the older reconnaissance and sabotage-preparation states for save compatibility while turning them into background mission context.
- Added relay sabotage completion state, report text, debug action, device ThingDef, JobDef, alert, and EN/FR keyed text.

## 0.2.44-dev - Add Tok'ra relay sabotage objective

- After the isolated Goa'uld relay has been reconnoitered, let a player caravan present on the site prepare a discreet sabotage plan through the world-map right-click menu.
- Record a persistent relay-sabotage objective state and expose it through the Tok'ra communicator status report.
- Add RP letter, inspect-string update, advanced-debug gizmo and dedicated debug action for the planning step.
- Add an English keyed translation file for the decoded mission world-site sequence while preserving the existing French localization.
- Keep the milestone preparatory: the relay remains intact and no combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.
- Align mod metadata and C# assembly version with `0.2.44`.
- Requires a forced C# rebuild.

## 0.2.43-dev-r2 - Restrict site gizmo and restore caravan travel option

- Hide the direct Tok'ra decoded mission site reconnaissance gizmo in normal gameplay; it remains available only through developer mode or the GateRim SG-1 advanced debug option.
- Keep the player-facing reconnaissance flow on the selected caravan world-map right-click menu.
- Add a travel float-menu option when the selected caravan is not yet on the site tile, so changing destination back to the revealed relay remains possible.
- Keep all reconnaissance limits unchanged: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.

## 0.2.43-dev-r1 - Add caravan right-click reconnaissance action

- Add a world-map float-menu option so a selected player caravan can reconnoiter the revealed Tok'ra decoded mission site by right-clicking the site tile.
- Keep the existing site gizmo as a fallback entry point.
- Keep all reconnaissance limits unchanged: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.

## 0.2.43-dev - Add Tok'ra decoded mission site reconnaissance

- Allow the revealed Tok'ra decoded mission world site to be reconnoitered by a player caravan present on the site tile.
- Record a persistent reconnoitered-site state and expose it through the Tok'ra communicator status report.
- Add RP letter, world-site command, inspect-string update and debug action for the reconnaissance step.
- Keep the step lightweight: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.
- Align mod metadata and C# assembly version with `0.2.43`.
- Requires a forced C# rebuild.

## 0.2.42-dev-r2
- Consume the Tok'ra encoded intelligence packet after successful analysis.
- Clarify that the mission state persists through the Tok'ra channel report after the physical packet is removed.
- Keep the decoded mission world-site feature unchanged.

## 0.2.42-dev
- Add a temporary Tok'ra decoded mission world-site marker after the decoded operational lead.
- Add persistent state for the revealed Tok'ra mission site.
- Update the Tok'ra communicator status report with pending and revealed site states.
- Add a debug action to reveal the site for validation.
- Keep the milestone non-rewarding: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment.

## 0.2.40-dev - Add Tok'ra coded intelligence analysis

- Make the Tok'ra encoded intelligence packet analyzable by a colon capable of Intellectual.
- Add a short right-click work interaction on the packet and a light Intellectual XP gain.
- Track the analyzed intelligence state persistently and expose it through the Tok'ra channel status report.
- Keep the step non-material: no world site, raid, healing, reinforcements, trade, recruitment or reward is created.
- Align mod metadata and C# assembly version with `0.2.40`.
- Requires a forced C# rebuild.

## 0.2.39-dev - Add Tok'ra mission briefing follow-up cache

- Add an automatic follow-up cache after the first Tok'ra mission briefing has been received.
- Deliver a low-value encoded Tok'ra intelligence packet through the Tok'ra delivery drop zone, with communicator and map-edge fallbacks.
- Track the delivered mission cache persistently and expose the state in the Tok'ra channel status report.
- Add a debug action to force-deliver the mission cache for testing.
- No world site, raid, trade, recruitment, healing, military aid or major material reward is created.

## 0.2.38-dev-r2

- Removed inherited storage/thing filter category from the Tok'ra delivery drop zone so the non-minifiable marker no longer reports a startup config warning.

## 0.2.38-dev-r1 - Keep Tok'ra delivery drop zone non-minifiable

- Disable minification for the Tok'ra delivery drop zone.
- Keep replacement as the intended way to move the marker: placing a new zone removes the previous one.
- Keep delivery routing behavior unchanged.

## 0.2.38-dev - Add Tok'ra delivery drop zone

- Add a free, immediate Tok'ra delivery drop zone marker for player-controlled cache placement.
- Keep one Tok'ra delivery zone per map by removing the previous marker when a new one is placed.
- Route manual emergency medical caches, hidden-cell caches, safehouse-lead caches and escorted medical-support supplies through the delivery zone when present.
- Fall back near a powered Tok'ra communicator, then to a reachable map edge if no delivery zone exists; escorted Tok'ra visitors still enter from the map edge.
- Keep the marker non-production and non-storage: it adds no item generation, no healing, no recruitment, no trade and no military aid.
- Align mod metadata and C# assembly version with `0.2.38`.
- Requires a forced C# rebuild.

## 0.2.37-dev-r1 - Fix Tok'ra intercepted threat alert build

### 0.2.37-dev-r2

- Fixed the Tok'ra intercepted threat debug action so it also opens the RP letter used by the natural incident.


- Add the missing RimWorld namespace import for the Tok'ra intercepted threat alert.
- Keep the intercepted threat behavior unchanged.

## 0.2.37-dev - Add Tok'ra intercepted threat intelligence

- Add a rare Tok'ra interception incident that warns trusted colonies about an approaching Goa'uld/Jaffa force.
- Create a persistent intercepted-threat state with a right-side alert and hover details until the delayed attack starts.
- Schedule the delayed attack after a 1 to 3 day preparation window.
- Enrich the tactical threat assessment request so it can reveal useful information before the threat is visible.
- Keep the interaction non-material: no Tok'ra reinforcements, items, healing, trade, recruitment or reward is created.
- Add debug actions to create or clear an intercepted threat for testing.
- Align mod metadata and C# assembly version with `0.2.37`.
- Requires a forced C# rebuild.

## 0.2.36-dev - Add Tok'ra initiated mission briefing contact

- Temporarily hide the player-facing Tok'ra operational debrief request until it has a clearer trust or narrative role.
- Keep the first discreet Tok'ra mission request as a pawn-operated communicator action at trusted tier.
- After a mission is prepared, let the Tok'ra cell recontact the colony automatically after a short RP delay.
- Record a persistent "briefing received" state and display it in the communicator status report.
- Keep the step non-material: no world site, raid, reward, trade, recruitment or direct support is created.
- Align mod metadata and C# assembly version with `0.2.36`.
- Requires a forced C# rebuild.

## 0.2.35-dev - Add Tok'ra first trust mission hook

- Add a pawn-operated trusted-channel request for a discreet future Tok'ra mission.
- Require a powered communicator and trusted Tok'ra confidence.
- Persist the prepared mission hook in the save and expose it in the Tok'ra channel status report.
- Keep the hook deliberately lightweight: no complete quest, world site, forced raid or immediate reward is added.

## 0.2.34-dev - Add Tok'ra operational debrief request

- Add a pawn-operated trusted-channel operational debrief request to the Tok'ra secure communicator.
- Require a powered communicator, trusted Tok'ra tier and an operator capable of learning Social or Intellectual.
- Grant a small Social or Intellectual XP gain to the operator through a vanilla RP dialog, then apply a dedicated two-day cooldown.
- Keep the request non-material and non-military: no item, treatment, reinforcement, trade, recruitment or quest is added.
- Extend the Tok'ra channel status report and documentation with the new debrief channel.

## 0.2.33-dev-r1 - Simplify locked Tok'ra communicator option labels

- Keep the trust-progression status report introduced in `0.2.33-dev`.
- Replace verbose locked right-click option explanations with concise player-facing reasons.
- Locked requests now show short labels such as `Confiance Tok'ra insuffisante` instead of repeating the required trust tier on every command.
- Keep detailed context in the status report, where the player can intentionally consult the channel state.

## 0.2.33-dev - Add Tok'ra trust progression status report

- Enrich the pawn-operated Tok'ra communicator status report with an RP reading of the current Tok'ra cell posture.
- Keep the player-facing report non-numeric: it indicates whether the cell is distant, cautious, cooperative, close to a posture change or already trusted.
- Reuse the existing status consultation: no request is sent, no cooldown is consumed and no gameplay effect is triggered.
- Align mod metadata and C# assembly version with `0.2.33`.
- Requires a forced C# rebuild.
- Requires no new XML root `About.xml`; keep metadata under `About/About.xml`.


## 0.2.32-dev-r2 - Remove redundant power line from Tok'ra status report

- Remove the visible "alimentation du relais" line from the Tok'ra channel status report.
- Keep the same internal validation and communicator requirements.
- Preserve the RP-style transmission introduced in `0.2.32-dev-r1`.
- No gameplay change.
- Requires no new XML root `About.xml`; keep metadata under `About/About.xml`.


## 0.2.32-dev-r1 - RP pass on Tok'ra communicator status report

- Reword the Tok'ra channel status report as an in-universe fragmented transmission.
- Keep the same useful information: trust, power, channel availability, cooldown states, active hostiles and medical context.
- Remove the most technical/debug-like player-facing phrasing from the report body.
- No gameplay change.
- Requires no new XML root `About.xml`; keep metadata under `About/About.xml`.


## 0.2.32-dev - Tok'ra communicator status report

- Added a pawn-operated Tok'ra communicator status report.
- The report lists trust, power, implemented channel availability and cooldown states.
- The consultation has no direct gameplay effect and consumes no cooldown.

# Changelog

## 0.2.53-dev - Consolidate French player wiki

- Rewrite the French wiki home page to reflect the actual playable `0.2.x` scope instead of the early `0.1.6-dev` prototype state.
- Replace obsolete version promises in `Prochain développement majeur` with current development directions.
- Keep `Liens utiles` limited to navigation, installation, status, FAQ and repository references.
- Rewrite the Tok'ra interaction roadmap through the four recurring organic-operation archetypes and the playable relay mission.
- Reorganize the content-status page so implemented `0.2.x` systems are no longer listed under planned content.
- Translate remaining English player-facing prose in Tok'ra operation, delivery-zone, safehouse-contact, decoded-site and reconnaissance pages.
- Remove obsolete English validation notes from public wiki pages.
- Harmonize French labels in the sidebar and expose mission pages without raw wiki-link syntax.
- Correct obsolete Prim'ta documentation for age eligibility, puberty dependency, tretonin support, deep-freezing and implantation paths.
- Correct obsolete Goa'uld documentation for autonomous forced implantation, automatic host conversion and the completed ritual flow.
- Update Tok'ra trust, pawn groups, safehouse leads, medical support and decoded-mission pages to their current implemented state.
- Refresh contradictory FAQ answers that still described existing systems as future additions.
- Add a durable French-wiki consistency checklist to `docs/TESTING.md`.
- Keep gameplay, save data and Defs unchanged.
- Raise the assembly version to `0.2.53.0` and the mod metadata version to `0.2.53-dev`.

## 0.2.43-dev-r2 - Restrict site gizmo and restore caravan travel option

- Hide the direct Tok'ra decoded mission site reconnaissance gizmo in normal gameplay; it remains available only through developer mode or the GateRim SG-1 advanced debug option.
- Keep the player-facing reconnaissance flow on the selected caravan world-map right-click menu.
- Add a travel float-menu option when the selected caravan is not yet on the site tile, so changing destination back to the revealed relay remains possible.
- Keep all reconnaissance limits unchanged: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.

## 0.2.43-dev-r1 - Add caravan right-click reconnaissance action

- Add a world-map float-menu option so a selected player caravan can reconnoiter the revealed Tok'ra decoded mission site by right-clicking the site tile.
- Keep the existing site gizmo as a fallback entry point.
- Keep all reconnaissance limits unchanged: no generated combat map, raid, item reward, healing, reinforcements, trade or recruitment is created.

## 0.2.31-dev — Add Tok'ra tactical threat assessment request

- Add a trusted-tier tactical threat assessment request to the pawn-operated Tok'ra secure communicator.
- Require a powered communicator, trusted Tok'ra tier, selected-pawn operation and an active hostile threat on the current map.
- Open a vanilla tactical report summarizing hostile count, broad threat composition and severity label.
- Start a dedicated one-day tactical-channel cooldown.
- Keep the request informational only: no damage, stun, healing, item delivery, map reveal, trade, recruitment, quest or reinforcements.
- Align mod metadata and C# assembly version with `0.2.31`.
- Requires a forced C# rebuild.

## 0.2.30-dev — Add Tok'ra emergency medical cache request

- Add a trusted-tier emergency medical cache request to the pawn-operated Tok'ra secure communicator.
- Require a powered communicator, trusted Tok'ra tier and at least one wounded or sick human colonist on the current map.
- Place a small emergency cache near the communicator with limited medicine and, when available, one tretonin dose.
- Start a dedicated seven-day cache cooldown.
- Keep the request non-commercial and non-repeatable in the short term: no direct healing, recruitment, quest or military aid.
- Align mod metadata and C# assembly version with `0.2.30`.
- Requires a forced C# rebuild.


## 0.2.29-dev — Add Tok'ra communicator medical support request

- Add a trusted-tier medical guidance request to the Tok'ra secure communicator.
- Require a powered communicator, selected-pawn operation and at least one wounded or sick human colonist on the current map.
- Grant `600` Medicine XP to the operator and start a dedicated three-day medical-channel cooldown.
- Keep the support advisory only: no direct treatment, item delivery, trade, recruitment, quest or military aid.
- Align mod metadata and C# assembly version with `0.2.29`.
- Requires a forced C# rebuild.


## 0.2.28-dev-r1 — Hide direct Tok'ra communicator gizmos outside debug

- Hide direct communicator building gizmos during normal play.
- Keep the selected-colonist right-click workflow as the player-facing interaction.
- Keep direct building commands available only when RimWorld developer mode or the GateRim SG-1 advanced debug information option exposes diagnostic UI.
- Preserve the validated pawn-operated channel and defensive-diversion jobs.
- Requires a forced C# rebuild.

## 0.2.28-dev — Add pawn-operated Tok'ra communicator interaction

- Move Tok'ra secure-communicator use to selected-pawn right-click interactions.
- Add two communicator jobs: open the trusted channel and request a defensive diversion.
- Keep the existing trust, power, active-threat and five-day cooldown requirements.
- Leave building gizmos as status/instruction entries: the player must select a colonist and right-click the communicator to operate it.
- Preserve the validated mixed diversion effect: short immediate stun plus delayed vomiting for biological targets.
- Align mod metadata and C# assembly version with `0.2.28`.
- Requires a forced C# rebuild.


## 0.2.27-dev-r2 — Mix Tok'ra defensive diversion effects

- Change successful defensive diversion from a pure stun into a mixed disruption.
- Keep a short immediate stun for fast feedback.
- Add delayed nausea/vomiting for biological targets when the vanilla vomit job is available.
- Preserve existing limits: trusted tier only, powered communicator, active threat required, up to three hostile pawns, five-day cooldown.
- Keep the future direction that communicator actions should later be operated through a pawn interaction.
- Requires a forced C# rebuild.

## 0.2.27-dev-r1 — Fix Tok'ra defensive diversion stun targeting

- Fix the communicator defensive-diversion request so RimWorld 1.6 stun signatures are matched by parameter type instead of assuming a generic `Thing` instigator.
- Prevent valid hostile targets from always falling through to the diversion-failed message.
- Keep the design unchanged: trusted tier only, active threat required, up to three hostile pawns briefly disrupted, five-day cooldown.
- Note future direction: communicator requests should later be operated through a pawn interaction rather than only as direct building gizmos.
- Requires a forced C# rebuild.

## 0.2.27-dev — Add Tok'ra defensive diversion request

- Add the first active trusted-channel request to the Tok'ra secure communicator.
- Allow trusted colonies to request a rare defensive diversion while hostile pawns are active on the current map.
- Disrupt up to three hostile pawns briefly, then place the request on a five-day cooldown.
- Keep the support defensive and clandestine: no Tok'ra squad, trade, recruitment, quest, item reward or permanent military aid is added.
- Align mod metadata and C# assembly version with `0.2.27`.
- Requires a forced C# rebuild.

## 0.2.26-dev — Add trusted Tok'ra secure communicator foundation

- Add the buildable `SG1_TokraSecureCommunicator` as a first secure-channel prototype.
- Gate its active contact command behind the trusted Tok'ra trust tier; lower tiers can see the locked state but cannot open the channel.
- Require power and Microelectronics before use, while keeping the actual Tok'ra access controlled by trust.
- Show a vanilla closeable dialog listing future request families: advanced medical advice, safehouse leads and rare defensive support.
- Do not grant items, treatment, quests, recruitment, trade or military aid in this foundation step.
- Align mod metadata and C# assembly version with `0.2.26`.
- Requires a forced C# rebuild.


## 0.2.25-dev — Define Tok'ra interaction roadmap

- Add a technical roadmap for Tok'ra interactions before adding larger rewards.
- Document the current implemented loop: hidden faction, trust, safehouse leads, safehouse sites, peaceful contact and once-per-contact briefing.
- Define future interaction families: medical support, safehouse network, secure communicator, rare defensive military support, short questline and later race/culture-specific branches.
- Keep the milestone documentation-focused: no new item, treatment, quest, recruitment, trade or military-aid implementation is added.
- Align mod metadata and C# assembly version with `0.2.25`.

## 0.2.24-dev — Add trust-gated Tok'ra safehouse follow-up lead

- Keep the Tok'ra safehouse contact exchange non-trading, non-recruitable, non-hostile and once per generated contact.
- Preserve the existing `+1` Tok'ra trust acknowledgement, trust-scaled Medicine XP and detailed vanilla briefing dialog.
- Add a trust-gated follow-up lead after the safehouse contact exchange: cooperative and trusted tiers can store one additional Tok'ra safehouse lead if the lead registry has room.
- Keep wary and neutral contacts limited to medical briefing information only.
- Preserve the existing safehouse lead cap and avoid direct item rewards, treatment, recruitment, quests, commerce or military aid.
- Align mod metadata and C# assembly version with `0.2.24`.
- Requires a forced C# rebuild.


## 0.2.23-dev-r1 — Move Tok'ra safehouse briefing details to dialog

- Keep the historical message short so the vanilla message history remains readable.
- Show the detailed trust-tier medical briefing in a vanilla closeable dialog after the exchange.
- Preserve the validated mechanics: once per contact, `+1` Tok'ra trust and trust-scaled Medicine XP.

## 0.2.23-dev — Add Tok'ra safehouse advanced medical hint

- Keep the Tok'ra safehouse contact exchange non-trading, non-recruitable, non-hostile and once per generated contact.
- Preserve the existing `+1` Tok'ra trust acknowledgement and trust-scaled Medicine XP.
- Add a small tier-specific medical hint to the contact briefing, foreshadowing future Tok'ra medical support without granting items, quests, recruitment or military aid.
- Align mod metadata and C# assembly version with `0.2.23`.
- Requires a forced C# rebuild.


## 0.2.22-dev-r2 — Preserve Tok'ra trust during safehouse briefing tests

- Keep `Prepare Tok'ra safehouse site test` focused on the safehouse test environment: remove inactive safehouse markers/sites and store one lead.
- Stop resetting Tok'ra trust to neutral during preparation, so cooperative and trusted briefing XP can be tested after using the +5/-5 trust debug actions.
- Update the preparation message to show the preserved trust score and tier.
- Requires a forced C# rebuild.

## 0.2.22-dev — Add trust-scaled Tok'ra safehouse briefing outcome

- Keep the once-per-contact Tok'ra safehouse exchange as a non-trading, non-recruitable contact outcome.
- Read the current Tok'ra trust tier before applying the safehouse contact acknowledgement.
- Scale the Medicine briefing XP by trust tier: `250` wary, `400` neutral, `600` cooperative and `800` trusted.
- Add tier-specific French narrative messages for the safehouse contact briefing.
- Preserve the existing `+1` Tok'ra trust gain, once-per-contact persistence and no-repeatable-reward rule.
- Align mod metadata and C# assembly version with `0.2.22`.
- Add two developer actions for testing trust-scaled outcomes without hard-coded tier setters: increase Tok'ra trust by `+5` and decrease it by `-5`, clamped to the existing trust minimum and maximum.
- Requires a forced C# rebuild.

## 0.2.21-dev — Add Tok'ra safehouse medical briefing outcome

- Extend the once-per-contact Tok'ra safehouse exchange with a modest field medical briefing.
- The selected colonist who performs the right-click interaction receives `400` Medicine XP.
- Keep the existing `+1` Tok'ra trust acknowledgement and narrative message.
- Keep the contact non-trading, non-recruitable, non-hostile and non-repeatable.
- Align mod metadata and C# assembly version with `0.2.21`.
- Requires a forced C# rebuild.


## 0.2.19-dev-r1 — Add Tok'ra field-garb body-type textures

- Add missing worn apparel texture variants for vanilla adult body types: `Male`, `Female`, `Thin`, `Fat` and `Hulk`.
- Fix the runtime missing-texture report for `Things/Pawn/Humanlike/Apparel/TokraFieldGarb/TokraFieldGarb_Female` observed on a generated Tok'ra safehouse contact.
- Keep the existing `SG1_TokraFieldGarb` gameplay definition unchanged.
- Texture/docs-only patch: no C# rebuild required.

## 0.2.19-dev — Add Tok'ra field clothing set

- Add the single dedicated apparel set `SG1_TokraFieldGarb` / `Tok'ra field garb`.
- Keep the Tok'ra clothing direction deliberately narrow: one sober sand-colored outfit, no variants.
- Use a light textile OnSkin apparel profile covering torso, neck, shoulders, arms and legs.
- Make the outfit craftable at manual and electric tailoring benches after `SG1_SGFieldEquipment`.
- Apply the outfit directly to `SG1_TokraVoluntaryHost` through `apparelRequired`.
- This fixes the validated `0.2.18-dev` safehouse contact appearing naked without a temporary vanilla-clothing workaround.
- Add temporary dedicated item and worn graphics plus French translations, technical documentation and wiki drafts.
- Align C# assembly metadata with `0.2.19`; gameplay changes are XML/texture only.

## 0.2.18-dev — Add non-trading Tok'ra safehouse contact

- Add `GenStep_TokraHiddenSafehouseContact` after the validated item-stash
  generation step.
- Generate exactly one `SG1_TokraVoluntaryHost` on the safehouse map.
- Reuse the site's persistent hidden `SG1_Tokra` faction.
- Assign a three-day vanilla peaceful-visit `LordJob` so the contact remains
  on the generated map instead of immediately leaving it.
- Raise the Tok'ra voluntary-host minimum generation age from `18` to `20` so
  generated contacts receive an adulthood backstory.
- Keep the contact peaceful, explicitly non-recruitable and without a trader
  role or trade inventory.
- Add `Verify Tok'ra safehouse contact test` under the `GateRim SG-1`
  developer actions for a deterministic in-game validation report.
- Preserve the existing medical cache, timeout, site cleanup and absence of
  military aid, raids or permanent settlement.
- Add English/French text, technical documentation, manual tests and wiki
  drafts.
- Align C# assembly version with `0.2.18`.
- Requires a forced C# rebuild.

## 0.2.17-dev — Add enterable hidden Tok'ra safehouse site

- Add incident `SG1_TokraHiddenSafehouseSiteIncident`.
- Add `IncidentWorker_TokraHiddenSafehouseSite`.
- Add vanilla-based site part `SG1_TokraHiddenSafehouseSitePart`.
- Add a reduced-size enterable `Site` world object.
- Consume one stored Tok'ra safehouse lead on successful site creation.
- Reuse the persistent hidden `SG1_Tokra` faction.
- Place `2` tretonin doses and `4` industrial medicine on the generated map.
- Expire an unvisited site after `10` RimWorld days.
- Prevent a Tok'ra safehouse marker and site from coexisting.
- Add `Prepare Tok'ra safehouse site test` under the `GateRim SG-1` developer
  actions to reset trust to neutral, store one lead and remove inactive test
  markers or sites.
- Add `Create Tok'ra safehouse test site` so validation does not depend on
  RimWorld's truncated `Do incident (Map)` Def-name list.
- Keep the site non-hostile, with no trader, recruitment, military aid, raid or
  permanent settlement.
- Add French translations, technical documentation, manual tests and wiki
  drafts.
- Align C# assembly version with `0.2.17`.
- Requires a forced C# rebuild.

## 0.2.16-dev — Add hidden Tok'ra safehouse world marker

- Add world object `SG1_TokraHiddenSafehouseMarker`.
- Add `WorldObject_TokraHiddenSafehouseMarker`.
- Add incident `SG1_TokraHiddenSafehouseWorldMarker`.
- Add `IncidentWorker_TokraHiddenSafehouseWorldMarker`.
- Consume one stored Tok'ra safehouse lead to create a temporary non-hostile marker on the world map.
- Keep the marker intentionally non-enterable and temporary.
- Prevent duplicate active safehouse markers.
- Reuse the persistent hidden `SG1_Tokra` faction.
- Keep wary trust excluded.
- Keep no generated map, loot, trader, recruitment, military aid or raid.
- Align C# assembly version with `0.2.16`.
- Update documentation, wiki drafts and `docs/PROJECT_STATE.md`.
- Requires forced C# rebuild after application.

## 0.2.15-dev — Add Tok'ra safehouse lead cache baseline

- Add storyteller incident `SG1_TokraSafehouseLeadCache`.
- Add `IncidentWorker_TokraSafehouseLeadCache`.
- Add `GameComponent_TokraSafehouseLeadTracker.TryConsumeSafehouseLead(...)`.
- Consume one stored Tok'ra safehouse lead when the follow-up cache resolves.
- Reuse the persistent hidden `SG1_Tokra` faction.
- Keep wary trust excluded.
- Spawn a modest medical cache only:
  - `2` tretonin doses;
  - `3` industrial medicine.
- Keep no world site, generated map, pawn, caravan, trader, recruitment, military aid or raid.
- Add French translations and player messages.
- Align C# assembly version with `0.2.15`.
- Update documentation, wiki drafts and `docs/PROJECT_STATE.md`.
- Requires forced C# rebuild after application.

## 0.2.14-dev — Add Tok'ra safehouse lead tracker baseline

- Add `GameComponent_TokraSafehouseLeadTracker`.
- Persist `tokraSafehouseLeadCount` in saves.
- Cap stored safehouse leads at `3`.
- Update `SG1_TokraSafehouseSignal` so each successful signal stores one lead.
- Keep the signal's `+1` Tok'ra trust gain.
- Update signal letters to show safehouse lead progress.
- Add English/French lead messages.
- Keep the system non-territorial: no world site, no settlement, no caravan, no loot, no trader, no recruitment, no military aid and no raid.
- Align C# assembly version with `0.2.14`.
- Update documentation, wiki drafts and `docs/PROJECT_STATE.md`.
- Requires forced C# rebuild after application.

## 0.2.13-dev — Add Tok'ra safehouse signal baseline

- Add storyteller incident `SG1_TokraSafehouseSignal`.
- Add `IncidentWorker_TokraSafehouseSignal`.
- Reuse the persistent hidden `SG1_Tokra` faction.
- Add a rare letter-only encrypted safehouse signal from a clandestine Tok'ra cell.
- Gate the incident to neutral, cooperative or trusted Tok'ra trust tiers.
- Keep wary trust excluded.
- Add a tiny `+1` Tok'ra trust increase when the signal is acknowledged.
- Add `GameComponent_TokraTrustTracker.NotifyHiddenSafehouseSignalAcknowledged()`.
- Add trust message translations.
- Keep the event non-territorial: no world site, no settlement, no caravan, no trader, no recruitment, no loot, no military aid and no raid.
- Align C# assembly version with `0.2.13`.
- Update French translations, technical documentation, wiki drafts and `docs/PROJECT_STATE.md`.
- Requires forced C# rebuild after application.

## 0.2.12-dev-r1 — Normalize Tok'ra cell cache French label

- Rename the French incident label from `cache d'une cellule Tok'ra cachée` to `cache d'une cellule Tok'ra`.
- Adjust related French/wiki wording from `cachée` repetition to `clandestine` where useful.
- Text-only patch: no rebuild required.

## 0.2.12-dev — Add hidden Tok'ra cell cache baseline

- Add storyteller incident `SG1_TokraHiddenCellCache`.
- Add `IncidentWorker_TokraHiddenCellCache`.
- Reuse the persistent hidden `SG1_Tokra` faction through `TokraFactionUtility.GetOrCreatePersistentFaction(...)`.
- Add a rare non-hostile map-edge cache representing a hidden Tok'ra cell.
- Gate the incident to neutral, cooperative or trusted Tok'ra trust tiers.
- Keep wary trust excluded from clandestine support.
- Spawn modest supplies only:
  - `1` tretonin dose and `2` industrial medicine at neutral trust;
  - `2` tretonin doses and `2` industrial medicine at cooperative trust;
  - `2` tretonin doses and `3` industrial medicine at trusted trust.
- Keep the event non-territorial: no settlement, caravan, trader, recruitment, military aid, quest site or raid.
- Update `GR_DefOf`.
- Align C# assembly version with `0.2.12`.
- Update French translations, technical documentation, wiki drafts and `docs/PROJECT_STATE.md`.
- Requires forced C# rebuild after application.

## 0.2.11-dev — Balance queen-origin Prim'ta acquisition

- Reduce natural queen-arrival frequency from `baseChance = 0.02` to `0.015`.
- Move queen-arrival eligibility from day `30` to day `45`.
- Increase queen-arrival `minRefireDays` from `60` to `90`.
- Increase player-controlled queen extraction recovery from `60000` ticks to `180000` ticks.
- Keep each extraction at `1` immature Prim'ta symbiote.
- Increase assisted maturation cost from `10` to `20` raw meat.
- Increase assisted maturation work amount from `1800` to `2400`.
- Keep `SG1_GoauldBiotechnology` as the research gate for maturation and specialized basins.
- Keep captured, existing or already produced biological resources usable.
- Update English/French player text, technical documentation and wiki pages.
- XML-only balance pass: no C# rebuild is required.

## 0.2.10-dev — Add natural Goa'uld queen acquisition baseline

- Add the rare `SG1_GoauldQueenArrival` storyteller incident after day `30`.
- Enforce a minimum `60`-day refire delay.
- Give the player one escaped Goa'uld queen only when no living
  player-controlled queen exists on a map or in a caravan.
- Expose immature Prim'ta extraction to player-controlled queens while
  retaining unrestricted developer-mode testing.
- Preserve the persistent one-day extraction cooldown and one-resource yield.
- Preserve assisted maturation with one immature symbiote and `10` raw meat.
- Keep local maturation gated by `SG1_GoauldBiotechnology`.
- Align the C# assembly version with `0.2.10`.
- Require a forced C# rebuild.
- Add English and French text, technical documentation and player-wiki drafts.

## 0.2.9-dev — Add natural Zat'nik'tel acquisition baseline

- Add `SG1_ZatnikTel` to the weapon-tag pools of Goa'uld Jaffa guard profiles.
- Keep ordinary Goa'uld Jaffa warriors restricted to Ma'Tok staff weapons.
- Allow rare Zat'nik'tel recovery through existing natural Goa'uld raids and
  generated Goa'uld settlements.
- Preserve immediate use of recovered weapons before `SG1_JaffaWeaponry`.
- Keep local Zat'nik'tel manufacturing gated by `SG1_JaffaWeaponry`.
- Keep queen-origin Prim'ta sourcing outside this focused equipment milestone.
- Keep the milestone XML-only: no C# rebuild is required.
- Validate warrior, guard, settlement and natural-raid loadouts in game.
- Validate recovered Zat'nik'tel use before research and crafting locks after
  research.
- Update technical documentation and player-wiki drafts.

## 0.2.8-dev-r1 — Require adult stranded SG-team candidates

- Reject `Stranded SG team` starting candidates below `20` biological years so
  every candidate receives an adulthood backstory.
- Keep the existing violence-capability requirement.
- Preserve regeneration of all four candidate slots through the vanilla selection page.
- Keep the scenario XML unchanged because `allowedDevelopmentalStages` cannot express an exact minimum age.
- Align the C# assembly version with `0.2.8`.
- Require a forced C# rebuild after extraction.
- Validate the `20+` age threshold, adulthood backstories and repeated
  regeneration of all four candidate slots in game.
- Update technical documentation and the player-wiki draft.

## 0.2.8-dev — Add Stargate crafting-research baseline

- Add dedicated research tab `SG1_GateRimResearch`.
- Add `SG1_JaffaWeaponry` after vanilla `Gunsmithing`.
- Gate local Ma'Tok and Zat'nik'tel crafting behind `SG1_JaffaWeaponry`.
- Add `SG1_JaffaArmor` after vanilla `FlakArmor`.
- Gate local Jaffa light armor, heavy armor, gauntlets, reinforced boots and deployed-helmet crafting behind `SG1_JaffaArmor`.
- Add `SG1_SGFieldEquipment` after vanilla `ComplexClothing`.
- Gate local SG-team uniform variants, tactical boots, tactical gloves, tactical vest and open-face field helmet crafting behind `SG1_SGFieldEquipment`.
- Add `SG1_GoauldBiotechnology` after vanilla `DrugProduction`.
- Gate local tretonin preparation, assisted Prim'ta maturation and construction of the incubation, preservation and ritual basins behind `SG1_GoauldBiotechnology`.
- Keep administration of existing tretonin doses, implantation of existing Prim'ta larvae and use of captured or scenario-supplied equipment available before research.
- Keep the milestone XML-only: no C# rebuild is required.
- Add French translations, technical documentation and player-wiki drafts.

## 0.2.7-dev — Add hidden Tok'ra world-presence baseline

- Promote `SG1_Tokra` from lazy event-time creation to one persistent hidden world-faction anchor.
- Set `requiredCountAtGameStart = 1` and `maxCountAtGameStart = 1`.
- Keep the Tok'ra faction hidden and absent from configurable world-creation lists.
- Keep `settlementGenerationWeight = 0`, natural raids forbidden and territorial systems disabled.
- Add `GameComponent_TokraWorldPresenceInitializer`.
- Automatically create the same hidden presence when loading older saves that do not yet contain a Tok'ra faction.
- Add retry initialization every `600` ticks for robust staged startup.
- Replace incident-time calls with `TokraFactionUtility.GetOrCreatePersistentFaction(...)`.
- Retain `GetOrCreateHiddenFaction(...)` as a historical source-compatibility alias.
- Reuse the same saved Tok'ra faction for peaceful visitors, therapeutic opportunities and medical-support deliveries.
- Extend `GameComponent_TokraHostPrototypeInitializer` to initialize the internal hidden-faction leader with a persistent Tok'ra symbiote identity.
- Preserve existing visitor behavior, trust tiers, therapeutic lifecycle and medical-support logic.
- Add French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.7`.
- Require a forced C# rebuild after extraction.

## 0.2.6-dev — Add Free Jaffa peaceful visitors baseline

- Add storyteller incident `SG1_FreeJaffaPeacefulVisitors`.
- Add `GateRimSG1.Jaffa.IncidentWorker_FreeJaffaPeacefulVisitors`.
- Add `GateRimSG1.Jaffa.FreeJaffaFactionUtility`.
- Select one existing visible non-hostile `SG1_FreeJaffa` world faction at random.
- Do not fabricate a hidden Free Jaffa fallback faction for old saves without world presence.
- Reuse RimWorld's vanilla peaceful visitor-group workflow.
- Add a dedicated Free Jaffa `Peaceful` pawn-group profile.
- Reuse validated `SG1_FreeJaffaWarrior` and `SG1_FreeJaffaGuard` profiles.
- Target two to four armed visitors with `110` points per visitor.
- Enable natural selection after day `10`, with `baseChance = 0.14` and `minRefireDays = 20`.
- Keep trade, gifts, quests, military aid, recruitment and custom diplomacy deferred.
- Preserve disabled natural Free Jaffa raids.
- Add French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.6`.
- Require a forced C# rebuild after extraction.

## 0.2.5-dev — Add contextual social baseline

- Add `GateRimSG1.Social.ContextualSocialIdentityUtility`.
- Distinguish inherited Jaffa physiology, current allegiance, Free Jaffa background, adult-symbiote origin, intrinsic forehead marks and real System Lord hosts.
- Add social thought `SG1_FreeJaffaDistrustsGoauldHost` with `-30` opinion.
- Add social thought `SG1_TokraSeesGoauldEnemy` with `-40` opinion.
- Add social thought `SG1_FreeJaffaWaryOfMarkedJaffa` with `-8` opinion.
- Add mood thought `SG1_DomainJaffaUnderSystemLordGaze` with `+2` mood within `12` cells.
- Restrict the System Lord proximity thought to a Jaffa and System Lord belonging to the same Goa'uld-domain faction.
- Preserve contextual interpretation of forehead marks: former service, coercion, pride and infiltration remain possible.
- Avoid forced permanent relationships, automatic attacks and absolute social restrictions.
- Add French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.5`.
- Require a forced C# rebuild after extraction.

## 0.2.4-dev-r1 — Define adulthood backstory body types

- Add `bodyTypeMale = Male` and `bodyTypeFemale = Female` to all `38` dedicated adulthood backstories.
- Keep childhood backstories unchanged.
- Fix generated Jaffa, Goa'uld, Tok'ra and occasional SGC-profile pawns receiving an undefined body type.
- Fix invisible pawn bodies, missing apparel graphics and portrait-rendering null-reference errors caused by the undefined body type.
- Reapply the correct provisional Goa'uld shirt file using vanilla `Apparel_CollarShirt` as a safety overwrite.
- Keep the milestone XML-only: no C# rebuild is required.

## 0.2.4-dev — Add cultural backstory baseline

- Add `52` native `BackstoryDef` entries.
- Add optional Tau'ri SGC adult careers while preserving vanilla Earth-origin histories.
- Add shared Jaffa childhoods, Goa'uld-domain Jaffa careers and Free Jaffa careers.
- Add off-world human childhoods for generated Goa'uld and Tok'ra hosts.
- Add ordinary Goa'uld-host, System Lord and generated Tok'ra-agent careers.
- Mark every dedicated story with `requiresSpawnCategory = true`.
- Add `backstoryFiltersOverride` to off-world PawnKindDefs.
- Keep existing voluntary Tok'ra host histories unchanged.
- Keep the milestone XML-only: no C# rebuild is required.
- Add French translations, technical documentation and player-wiki drafts.

## 0.2.3-dev-r3 — Fix provisional Goa'uld shirt reference

- Replace invalid `Apparel_ButtonDownShirt` references with vanilla `Apparel_CollarShirt`.
- Apply the correction to both `SG1_GoauldHostCaste` and `SG1_GoauldSystemLordHost`.
- Preserve the temporary pants, collared shirt and duster outfit.
- Keep generated-host healing, settlement composition and persistent symbiote behavior unchanged.
- No C# rebuild is required for this XML and documentation correction.

## 0.2.3-dev-r2 — Add provisional Goa'uld attire and heal generated hosts

- Add temporary vanilla apparel requirements to `SG1_GoauldHostCaste`.
- Add the same temporary vanilla apparel requirements to `SG1_GoauldSystemLordHost`.
- Use pants, button-down shirt and duster until dedicated Goa'uld clothing exists.
- Extend `GameComponent_GoauldHostCasteInitializer` with a narrow generated-host chronic-ailment cleanup.
- Heal pre-existing bad back, frailty, cataracts, blindness, hearing loss, dementia, Alzheimer's, asthma, artery blockage, carcinoma, cirrhosis and organ decay after generated-host symbiote initialization.
- Preserve scars, missing body parts and ordinary combat injuries.
- Apply the same cleanup when an existing generated host already carries an adult symbiote state.
- Keep settlement composition, direct raids and persistent identity behavior unchanged.
- Keep dedicated Goa'uld apparel as a later visual milestone.
- Update technical documentation and player-wiki drafts.
- Keep assembly version `0.2.3.0`; a forced rebuild is required because C# changed.

## 0.2.3-dev-r1 — Stabilize Goa'uld settlement castes and Tau'ri backstories

- Add `SG1_GoauldSettlementJaffaWarrior`.
- Add `SG1_GoauldSettlementJaffaGuard`.
- Keep the direct-raid `Combat` profile unchanged.
- Use settlement-only capped profiles for the Goa'uld `Settlement` group.
- Cap each generated settlement group at seven warriors, two guards and one ordinary Goa'uld host; a full city map may resolve more than one group.
- Lower the settlement-only guard combat cost to `130` so it remains eligible throughout the vanilla settlement point range.
- Cap `SG1_GoauldHostCaste` at one pawn per generated group.
- Extend one-time automatic Prim'ta initialization to both settlement-only Jaffa profiles.
- Keep Goa'uld-domain forehead-mark assignment automatic through the existing faction-domain rule.
- Replace the custom SGC expedition faction's narrow temporary `Civil` backstory filter with one broad vanilla-compatible filter.
- Preserve Tau'ri access to generic vanilla Earth-origin histories until dedicated SGC additions are introduced.
- Keep cultural backstories as a separate later milestone.
- Update technical documentation and player-wiki drafts.
- Keep assembly version `0.2.3.0`; a forced rebuild is still required because C# changed.

## 0.2.3-dev — Add Goa'uld host-caste baseline

- Add `SG1_GoauldHostCaste` with the player-facing label `Goa'uld`.
- Add `SG1_GoauldSystemLordHost` with the player-facing label `Goa'uld System Lord`.
- Add `GameComponent_GoauldHostCasteInitializer`.
- Initialize each generated Goa'uld host-caste pawn exactly once with `SG1_GoauldHostSymbiote`.
- Create one persistent `GoauldSymbioteData` identity with `Goauld` origin for each generated host.
- Scan both spawned map pawns and Goa'uld faction leaders.
- Preserve one-time initialization so later removal cannot create an artificial replacement.
- Replace the provisional Jaffa commander leader with a true System Lord host kind.
- Add ordinary Goa'uld hosts as a minority `Settlement` option with weight `0.75`.
- Keep the `Combat` profile and direct raids Jaffa-only.
- Keep the faction-level `Jaffa = 100%` summary as a vanilla-UI limitation: acquired host Hediffs are not xenotypes.
- Do not force the legacy `SG1_GoauldHost` xenotype onto generated hosts.
- Keep backstories, dedicated Goa'uld apparel and active-host extraction as separate later milestones.
- Add French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.3`.

## 0.2.2-dev-r2 — Expand faction limits and generate provisional leaders

- Keep `startingCountAtWorldCreation = 1` for both `SG1_GoauldSystemLordPrototype` and `SG1_FreeJaffa`.
- Raise `maxConfigurableAtWorldCreation` from `1` to `9999` for both factions so players may add extra instances manually like configurable vanilla factions.
- Preserve the default world-generation baseline of one Goa'uld-domain faction and one Free Jaffa faction.
- Add `fixedLeaderKinds` and `leaderForceGenerateNewPawn = true` to both visible humanlike factions.
- Use `SG1_FreeJaffaGuard` as the initial Free Jaffa leader kind.
- Use `SG1_GoauldJaffaGuard` as a provisional Goa'uld-domain leader kind.
- Add `SG1_GoauldJaffaWarrior` as the Goa'uld faction `basicMemberKind` fallback.
- Rename the provisional Goa'uld faction leader title to `domain Jaffa commander` / `commandant Jaffa de domaine`.
- Avoid pretending that the temporary Jaffa commander is already a true Goa'uld System Lord host.
- Reserve real persistent Goa'uld host leaders for a dedicated later milestone.
- No C# rebuild is required for this XML and documentation correction.

## 0.2.2-dev-r1 — Align faction xenotype summaries

- Add a faction-level `xenotypeSet` declaring `SG1_Jaffa = 100%` for `SG1_FreeJaffa`.
- Add a provisional faction-level `xenotypeSet` declaring `SG1_Jaffa = 100%` for `SG1_GoauldSystemLordPrototype`.
- Fix the misleading `baseliner / human = 100%` summaries shown by RimWorld world creation.
- Keep the Goa'uld summary intentionally limited to the current Jaffa-servant baseline.
- Do not add `SG1_GoauldHost` naively at faction level: a xenotype alone would create incomplete hosts without persistent implanted symbiote identity.
- Reserve true minority Goa'uld host profiles for a dedicated later milestone.
- Keep thematic faction icons as a later dedicated visual-production pass.
- No C# rebuild is required for this XML and documentation correction.

## 0.2.2-dev — Add Free Jaffa world-faction baseline

- Add the visible neutral world faction `SG1_FreeJaffa`.
- Generate exactly one Free Jaffa faction by default in RimWorld 1.6 world creation.
- Use a reduced `0.25` settlement-generation weight for a visible but limited presence.
- Add valid vanilla-style faction and settlement icon paths.
- Add `SG1_FreeJaffaWarrior` and `SG1_FreeJaffaGuard`.
- Reuse the validated Jaffa xenotype, Ma'Tok weapon tag and modular armor loadouts.
- Extend one-time Prim'ta initialization to generated Free Jaffa warrior and guard kinds.
- Preserve the historical Prim'ta-initializer component and save-data key for compatibility.
- Add `GoauldSystemLordDomainUtility.HasAssignedDomain(...)`.
- Restrict automatic forehead-mark assignment to Jaffa whose faction carries a Goa'uld domain extension.
- Keep Free Jaffa unmarked by default while preserving manual mark assignment and legacy migration.
- Keep Free Jaffa trade, quests, military aid, visitors and natural raids disabled for this baseline.
- Add French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.2`.

## 0.2.1-dev-r3 — Capitalize Goa'uld name and restore SGC faction icon

- Capitalize the French player-facing faction name to `Domaines des Grands Maîtres Goa'uld`.
- Capitalize the matching fixed faction name.
- Add explicit vanilla-style `factionIconPath = World/WorldObjects/Expanding/Town` to `SG1_PlayerSGCExpedition`.
- Add explicit `settlementTexturePath = World/WorldObjects/DefaultSettlement` to `SG1_PlayerSGCExpedition`.
- Keep the custom SGC expedition identity and existing faction color spectrum.
- No C# rebuild is required for this XML and documentation correction.

## 0.2.1-dev-r2 — Expose Goa'uld faction and normalize French vocabulary

- Add `maxConfigurableAtWorldCreation = 1` and `startingCountAtWorldCreation = 1` to the Goa'uld `FactionDef`.
- Make the Goa'uld faction appear once by default in RimWorld 1.6 world creation.
- Prevent duplicate Goa'uld factions through the Add faction menu.
- Add valid `factionIconPath` and `settlementTexturePath` fields.
- Remove deprecated `canMakeRandomly` and `maxCountAtGameStart` fields.
- Preserve `requiredCountAtGameStart = 1` for generation paths that do not receive an explicit world-creation faction list.
- Replace unnecessary `pawn` jargon in French player-facing strings and wiki prose with contextual terms such as soldier, target, host, colonist, Jaffa or character.
- Keep technical identifiers such as `PawnKindDef` unchanged where they refer to RimWorld code concepts.
- No C# rebuild is required for this XML and documentation correction.

## 0.2.1-dev-r1 — Remove invalid Goa'uld faction icon fields

- Remove invalid `FactionDef` fields `expandingIconTexture` and `homeIconPath`.
- Keep world-map settlements on RimWorld's default rendering.
- Preserve Goa'uld settlement naming, faction color spectrum, world generation and natural-raid behavior.
- No C# rebuild is required for this XML-only correction.

## 0.2.1-dev — Add playable Goa'uld world-faction baseline

- Turn `SG1_GoauldSystemLordPrototype` into a visible hostile world faction.
- Generate exactly one Goa'uld System Lord-domain faction on new worlds.
- Use a reduced `0.35` settlement-generation weight for a visible but limited world presence.
- Add world-map settlement naming and a gold-toned color spectrum.
- Add a `Settlement` pawn-group profile reusing Goa'uld-aligned Jaffa warriors and guards.
- Keep `raidsForbidden = true` so generic vanilla enemy-raid selection cannot enable unintended doctrines.
- Add the low-frequency storyteller incident `SG1_GoauldJaffaNaturalRaid`.
- Reuse the validated direct-assault workflow with explicit `ImmediateAttack`, `canSteal = false` and `canKidnap = false`.
- Keep natural abduction and destruction doctrines disabled.
- Preserve developer-controlled raid incidents for regression testing.
- Rename the shared Goa'uld runtime-faction helper so it no longer implies hidden-only behavior.
- Keep a visible lazy runtime fallback for older saves and isolated controlled tests.
- Update French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.1`.

## 0.2.0-dev-r2 — Add optional SG-team field helmet prototype

- Add `SG1_SGTeamFieldHelmet`.
- Model the SG helmet as an open-face `UpperHead` / `Overhead` field helmet.
- Keep protection moderate and below Jaffa helmet values.
- Add crafting after `Gunsmithing` with `25` steel, `15` cloth and `Crafting 4`.
- Add temporary inventory and four-facing graphics.
- Add four helmets to the stranded SG-team scenario supply crates.
- Keep the helmets optional: starter pawns do not auto-equip them.
- Update technical documentation and player-wiki drafts.

## 0.2.0-dev-r1 — Refine stranded SG-team starter scenario

- Add the dedicated player faction `SG1_PlayerSGCExpedition`.
- Display the player faction as `SGC expedition` / `expédition du SGC` instead of vanilla `New Arrivals`.
- Reduce the pawn-selection page from eight candidates to exactly four candidates for four starting slots.
- Reject generated player starters that are incapable of violence.
- Add `ScenPart_TranslatedGameStartDialog` so the narrative field resolves visibly in the scenario editor and the translated introduction opens reliably at map start.
- Keep the SG-team helmet as a separate follow-up equipment milestone.
- Update documentation and player-wiki drafts.

## 0.2.0-dev — Add stranded SG-team starter scenario

- Start the `0.2.x` playable-slice phase after the `0.1.x` mechanical-foundation phase.
- Add the selectable `SG1_StrandedSGTeam` scenario.
- Start with four adult player pawns and eight candidate pawns in the selection page.
- Add `ScenPart_SGTeamStartingGear` to dress generated starters automatically with the validated olive-drab four-piece SG-team field set.
- Keep temporary Tau'ri firearms as recoverable starting supplies: three vanilla assault rifles and one vanilla pump shotgun.
- Add four cloth bedrolls as believable bivouac equipment.
- Add emergency-supply crates represented by survival meals, industrial medicine, steel, wood, industrial components, cloth and plain leather.
- Do not provide a preconstructed tailoring bench.
- Keep Ma'Tok staffs, Zat'nik'tel sidearms, functional Stargates, world-faction activation and natural Goa'uld raids outside this first playable-slice milestone.
- Add bilingual scenario text, French translations, technical documentation and player-wiki drafts.
- Align the C# assembly version with `0.2.0`.

## 0.1.82-dev — Add black and desert SG-team uniform variants

- Add `SG1_BlackSGTeamUniform`.
- Add `SG1_DesertSGTeamUniform`.
- Keep both variants purely visual with the same protection, cost, coverage and crafting requirements as `SG1_GenericSGTeamUniform`.
- Reuse the dedicated `SG1_SGTeamApparel` category and existing modular tactical boots, gloves and vest.
- Add temporary black and desert inventory graphics, facing-specific graphics and body-type-specific graphics by recoloring the validated olive-drab baseline.
- Keep a forest-camouflage variant intentionally omitted because it would remain too visually close to the olive-drab sprite at RimWorld scale.
- Keep a sand-colored tactical-vest variant optional pending an in-game visual review.
- Add French translations, technical documentation and player-wiki drafts.

## 0.1.81-dev — Add SG tactical vest prototype

- Add `SG1_SGTacticalVest`.
- Keep the vest separate from the SG-team field uniform, tactical boots and tactical gloves.
- Reuse the dedicated `SG1_SGTeamApparel` category.
- Cover `Torso` and `Shoulders` on the `Middle` layer so the vest remains compatible with the `OnSkin` field uniform and limb-specific SG equipment.
- Add modest torso and shoulder protection without turning the load-bearing vest into heavy armor.
- Add crafting recipes for the vanilla hand and electric tailoring benches.
- Require `30` plain leather, `35` cloth and `Crafting 4`.
- Add temporary inventory, facing-specific and body-type-specific graphics.
- Complete the first generic four-piece SG-team field-equipment baseline.
- Add French translation, technical documentation and player-wiki drafts.

## 0.1.80-dev — Add SG tactical gloves prototype

- Add `SG1_SGTacticalGloves`.
- Keep the gloves separate from the SG-team field uniform and tactical boots.
- Reuse the dedicated `SG1_SGTeamApparel` category.
- Cover `Hands` on the `Middle` layer so the gloves remain compatible with the `OnSkin` field uniform.
- Add modest hand and finger protection without approaching Jaffa-gauntlet armor values.
- Add crafting recipes for the vanilla hand and electric tailoring benches.
- Require `20` plain leather and `8` cloth.
- Add temporary inventory, facing-specific and body-type-specific graphics.
- Add French translation, technical documentation and player-wiki drafts.

## 0.1.79-dev — Add SG tactical boots prototype

- Add `SG1_SGTacticalBoots`.
- Keep the footwear separate from the SG-team field uniform.
- Reuse the dedicated `SG1_SGTeamApparel` category.
- Cover only `Feet` on the `Middle` layer so the boots remain compatible with the `OnSkin` field uniform.
- Add modest foot protection without approaching reinforced Jaffa-boot armor values.
- Add crafting recipes for the vanilla hand and electric tailoring benches.
- Require `35` plain leather and `10` cloth.
- Add temporary inventory, facing-specific and body-type-specific graphics.
- Add French translation, technical documentation and player-wiki drafts.

## 0.1.78-dev — Add SG-team field uniform prototype

- Add `SG1_GenericSGTeamUniform`.
- Add a dedicated `SG1_SGTeamApparel` storage category under apparel.
- Model the first SG-team clothing baseline as an olive-drab jacket-and-trousers BDU.
- Keep the uniform on the `OnSkin` layer with torso, shoulder, arm and leg coverage.
- Keep protection deliberately lightweight and separate from future tactical armor.
- Add tailoring recipes for the hand tailoring bench and electric tailoring bench.
- Add temporary graphics for inventory, all four facings and the existing body-type variants.
- Keep SG tactical boots, gloves and vest as separate future apparel items.
- Normalize custom DefInjected translation folders with fully qualified GateRim SG-1 type names.
- Rename the player-facing uniform label to `SG-team field uniform` / `treillis d'équipe SG` and remove unnecessary human-only wording.
- Add French translations, technical documentation and player-wiki drafts.

## 0.1.77-dev — Add Zat'nik'tel incapacitation prototype

- Add the craftable `SG1_ZatnikTel` compact Goa'uld energy sidearm.
- Add a dedicated temporary weapon graphic and pulse-projectile graphic.
- Add `Projectile_ZatnikTelDisruption`.
- Use vanilla `Stun` as the direct first-shot neutralization effect with a base amount of `10`.
- Add a weak targeted `10 EMP` follow-up against non-organic pawns.
- Add a weak targeted `8 EMP` follow-up against buildings and turrets.
- Avoid physical injury and structural damage in the first-shot prototype.
- Reuse the declarative `Goauld` energy-technology extension.
- Keep lethal second-shot behavior, third-shot disintegration, automatic Jaffa loadouts and complex visuals deferred.
- Add French translation, technical documentation and player-wiki drafts.

## 0.1.76-dev — Add Ma'Tok plasma-impact damage prototype

- Keep the Ma'Tok projectile's existing `24` burn damage and `0.28` base armor penetration.
- Add `Projectile_MatokPlasmaImpact` as a specialized ranged projectile.
- Apply an additional reduced `8` blunt damage only to non-organic pawns.
- Apply an additional reduced `12` blunt damage to buildings and turrets.
- Avoid secondary impact damage on organic pawns to prevent an unintended anti-personnel buff.
- Keep area explosions intentionally deferred.
- Add a declarative `Goauld` energy-weapon technology extension on the staff and projectile.
- Reserve that extension as a future shared hook for Replicator resistance.
- Update the technical documentation and player-wiki drafts.

## 0.1.75-dev — Consolidate debug tools and player-facing diagnostics

- Add a persistent GateRim SG-1 mod setting for optional advanced debug information.
- Keep RimWorld developer mode as an automatic override for advanced diagnostics.
- Gate routine `GR_Log.Message(...)` lifecycle traces behind advanced diagnostics.
- Route informational lifecycle traces directly to `Player.log` so they do not enter RimWorld's in-game log queue or auto-open an error-looking popup.
- Reclassify expected Tok'ra incident refusals as informational traces when a forced developer test has no eligible patient, an active wary cooldown, an insufficient trust tier or no valid map-edge entry cell.
- Keep `GR_Log.Warning(...)`, `GR_Log.Error(...)` and their `Once` variants always visible.
- Hide raw free-symbiote IDs and autonomous cooldown ticks from normal inspection panels.
- Hide the Goa'uld queen raw extraction cooldown outside advanced diagnostics.
- Show only the Tok'ra trust tier during normal therapeutic offers while retaining the raw score in advanced diagnostics.
- Keep contextual ritual and temporary Tok'ra-offer information visible during normal gameplay.
- Document the first completed debug-UI consolidation pass and update player-wiki drafts.

## 0.1.74-dev-r1 — Replace technical Jaffa mark genes with intrinsic mark data

- Replace runtime forehead-mark `GeneDef` rendering with dedicated `JaffaForeheadMarkDef` data.
- Persist one optional intrinsic forehead mark per pawn through `GameComponent_JaffaForeheadMarks`.
- Inject intrinsic marks into RimWorld's native render tree through a dedicated dynamic setup.
- Keep the validated black, silver and gold temporary textures and their rank-specific domain slots.
- Remove the xenotype patch that assigned the generic black mark as a hereditary technical gene.
- Retain invisible legacy `GeneDef` placeholders solely to migrate existing saves without missing-Def errors.
- Convert encountered legacy genes into intrinsic pawn data, then remove the obsolete genes from affected pawns.
- Automatically initialize compatible Jaffa with an ordinary domain mark once, while preserving later manual removal.
- Add developer map tools to apply black, silver or gold marks, or remove a mark, on any pawn.
- Update bilingual strings, technical documentation and player-wiki drafts.

## 0.1.61-dev — Add first Goa'uld System Lord faction foundation

- Add the hidden hostile `SG1_GoauldSystemLordPrototype` faction definition.
- Keep permanent hostility enabled for the first Goa'uld System Lord domain.
- Keep automatic world generation disabled: no settlements, raids, traders or quest sites.
- Keep pawn groups intentionally absent until Goa'uld-aligned Jaffa pawn kinds exist.
- Add the required `Spacer` tech level, `Offworld` backstory filter and raid-loot curve.
- Add French `DefInjected` translations, technical documentation and player-wiki drafts.
- Prepare the next faction milestone for Jaffa servants and nested pawn-group profiles.


## 0.1.60-dev — Add Prim'ta deep-freezing penalties

- Add persistent deep-freezing exposure for mature Prim'ta larvae and queen-origin immature symbiotes.
- Keep powered preservation basins fully protective and allow them to reduce accumulated cold exposure.
- Trigger deep-freezing exposure only outside an active basin and at temperatures of `-15 °C` or lower.
- Keep the first `60000` ticks (`1` RimWorld day) temporarily tolerated.
- Add slow biological deterioration at `×0.25` after the grace period.
- Increase deep-freezing deterioration to `×0.50` at `-30 °C` or lower.
- Reduce accumulated exposure at `×2` speed in safer storage or inside an active preservation basin.
- Preserve exposure through save/load, stack merges and stack splits.
- Add English/French status strings, technical documentation and player-wiki drafts.


## 0.1.59-dev — Add Prim'ta preservation basin prototype

- Add the powered `SG1_PrimtaPreservationBasin` dedicated storage building.
- Restrict the basin to immature queen-origin symbiotes and mature Prim'ta larvae.
- Suspend additional rot progression while stored in a powered basin.
- Preserve deterioration accumulated before storage: the basin does not repair biological resources.
- Keep refrigerator, freezer and ambient-temperature gameplay active when no powered basin is available.
- Keep deep-freezing penalties planned for a dedicated follow-up milestone.
- Reuse the incubation-basin texture provisionally.
- Add French translations, technical documentation and player-wiki drafts.


## 0.1.58-dev — Add queen-origin Prim'ta assisted maturation prototype

- Add the physical `SG1_ImmaturePrimtaSymbiote` biological resource.
- Add a developer-only queen extraction gizmo with a persistent one-day cooldown.
- Keep technical queen cooldown information hidden outside RimWorld developer mode.
- Change `SG1_IncubatePrimtaLarva` from ex-nihilo larva production to assisted maturation.
- Require one queen-origin immature symbiote and ten units of raw meat per mature Prim'ta larva.
- Reuse the current larva texture provisionally at a smaller draw size for the immature resource.
- Keep autonomous queen reproduction, natural sourcing and dedicated queen infrastructure planned for later.
- Add French translations, technical documentation and player-wiki drafts.


## 0.1.57-dev — Add Goa'uld queen biological foundation

- Add the XML-only `SG1_GoauldQueen` animal-style pawn prototype.
- Add the developer-spawnable `SG1_GoauldQueen` PawnKindDef.
- Keep the queen excluded from biome tables, storyteller incidents and natural acquisition.
- Keep the first queen prototype passive: no hunting, implantation or larva production.
- Reuse the adult-symbiote texture provisionally at a larger draw size.
- Add French `DefInjected` translations, technical documentation and player-wiki drafts.
- Prepare the next assisted-maturation iteration for Prim'ta incubation.


## 0.1.56-dev docs — Add pre-0.2 cleanup roadmap

- Refresh the first-playable checklist to reflect already validated Jaffa and Goa'uld prototype workflows.
- Add a pre-`0.2.x` audit plan for player-facing information, debug-only diagnostics and conditional gizmos.
- Plan a future GateRim-specific debug option in addition to the RimWorld developer mode.
- Add the missing human SG-team equipment roadmap: generic uniform, tactical boots and tactical gloves.
- Record a wiki-image and Workshop-asset roadmap while keeping all current visuals explicitly provisional.
- Add a dedicated French translation-report follow-up checklist without guessing the five remaining errors.

## 0.1.56-dev — Add trusted Tok'ra advanced medicine support

- Add the first positive trusted-tier diplomatic reward beyond tretonin quantity and storyteller weighting.
- Keep cooperative medical-support deliveries unchanged with `2` tretonin doses and `1` visitor.
- Keep trusted medical-support deliveries at `4` tretonin doses and `2` visitors.
- Add `1` physical vanilla `MedicineUltratech` unit to trusted independent deliveries.
- Place the advanced medicine near the Tok'ra arrival point and keep it on the map after the visit.
- Add bilingual player feedback, `GR_Log` diagnostics, technical documentation and player-wiki drafts.

## 0.1.55-dev — Add wary Tok'ra diplomatic cooldown

- Add a persistent cooldown for new therapeutic opportunities while Tok'ra trust is wary.
- Apply a `3`-day RimWorld cooldown after an explicit refusal that leaves trust below `0`.
- Apply a `5`-day RimWorld cooldown after an unanswered expiration that leaves trust below `0`.
- Preserve the existing `×0.50` wary storyteller multiplier after the cooldown ends.
- Keep ordinary peaceful Tok'ra visitors available during the cooldown.
- Keep independent medical-support deliveries locked below cooperative trust.
- Add bilingual cooldown feedback, technical documentation and player-wiki drafts.

## 0.1.54-dev — Add Tok'ra storyteller trust weighting

- Keep XML incident chances as readable neutral baseline values.
- Multiply therapeutic-opportunity storyteller weight by the current Tok'ra trust tier.
- Reduce wary therapeutic opportunities to `×0.50` of their baseline weight.
- Keep neutral therapeutic opportunities at `×1.00`.
- Raise cooperative therapeutic opportunities to `×1.25`.
- Raise trusted therapeutic opportunities to `×1.50`.
- Keep independent medical-support deliveries locked below cooperative trust.
- Keep cooperative medical-support deliveries at `×1.00` of their baseline weight.
- Raise trusted medical-support deliveries to `×1.50`.
- Add `GR_Log` diagnostics for the active factor and effective runtime base chance.
- Update technical documentation and player-wiki drafts.

## 0.1.53-dev — Add Tok'ra medical-support deliveries
- Add rare storyteller-selected `SG1_TokraMedicalSupportDelivery` incident after day `45`.
- Unlock independent support deliveries only at cooperative and trusted Tok'ra trust tiers.
- Deliver `2` physical `SG1_TretoninDose` items with `1` visitor at cooperative trust.
- Deliver `4` physical `SG1_TretoninDose` items with `2` visitors at trusted trust.
- Reuse the lazily created hidden Tok'ra faction and vanilla peaceful visitor behavior.
- Keep support deliveries separate from therapeutic symbiosis opportunities and trust changes.
- Add English `Defs`, French `DefInjected` text, technical documentation and player-wiki drafts.

## 0.1.52-dev-r1 — Exclude cause-driven Tok'ra healing states
- Exclude `Malnutrition`, `BloodLoss`, `Heatstroke`, `Hypothermia` and `ToxicBuildup` from instant Tok'ra condition removal.
- Prevent repeated remove-and-recreate loops while the underlying hunger, bleeding, temperature or toxic exposure cause remains active.
- Keep RimWorld-oriented healing for curable biological conditions and progressive non-permanent injury regeneration unchanged.

## 0.1.52-dev — Add Tok'ra tretonin support gifts
- Add the first lightweight material benefit unlocked by Tok'ra trust tiers.
- Keep wary and neutral therapeutic opportunities unchanged with no material support.
- Spawn `1` physical `SG1_TretoninDose` item with cooperative therapeutic teams.
- Spawn `2` physical `SG1_TretoninDose` items with trusted therapeutic teams.
- Place the support stack near the Tok'ra arrival point and keep it on the map regardless of acceptance, refusal or expiration.
- Add bilingual player feedback and `GR_Log` diagnostics for support-gift spawning.
- Update incident text so the offer duration is no longer described as a fixed two-day value.
- Update technical documentation and player-wiki drafts.

## 0.1.51-dev — Add Tok'ra trust tiers for therapeutic offers
- Add four persistent trust tiers: wary below `0`, neutral from `0` to `9`, cooperative from `10` to `24`, and trusted from `25` upward.
- Display the current localized trust tier beside the numeric Tok'ra trust score.
- Reduce wary therapeutic offers to `1` day with exactly `1` escort pawn.
- Keep neutral therapeutic offers at `2` days with `1` to `2` escort pawns.
- Extend cooperative therapeutic offers to `3` days with exactly `2` escort pawns.
- Extend trusted therapeutic offers to `4` days with `2` to `3` escort pawns.
- Compute duration and escort size once when each offer is created so later trust changes do not mutate an active offer.
- Keep storyteller weights, material rewards, quests and vanilla-goodwill integration outside this first threshold milestone.
- Update technical documentation and player-wiki drafts.

## 0.1.50-dev — Add lightweight Tok'ra trust foundation
- Add persistent `GameComponent_TokraTrustTracker` storage clamped between `-100` and `100`.
- Increase Tok'ra trust by `+5` when a therapeutic offer is accepted.
- Reduce Tok'ra trust by `-1` after an explicit refusal and by `-2` after an unanswered expiration.
- Display the current trust score below the remaining duration of tracked therapeutic symbiotes.
- Add bilingual keyed messages and logs for each trust adjustment.
- Keep the Tok'ra runtime faction hidden and defer vanilla-goodwill integration until broader diplomacy is introduced.
- Update technical documentation and player-wiki drafts.

## 0.1.49-dev — Add Tok'ra therapeutic offer lifecycle
- Add persistent `GameComponent_TokraTherapeuticOpportunityTracker` records for escorted offers.
- Keep each offer available for two RimWorld days.
- Display the remaining duration in the free symbiote inspection text.
- Add an explicit refusal gizmo with confirmation dialog.
- Remove the free symbiote and ask the escort to leave when an offer expires or is refused.
- Ask the escort to leave after successful implantation consumes the offered symbiote.
- Extend escort visits to the offer duration instead of the vanilla random visitor duration.
- Persist tracked symbiote references, escort references and expiration ticks across save and reload.
- Add bilingual keyed messages and update technical documentation and player-wiki drafts.

## 0.1.48-dev — Add Tok'ra therapeutic escort prototype
- Extend the natural therapeutic-opportunity incident with a small Tok'ra escort.
- Spawn one or two `SG1_TokraVoluntaryHost` pawns near the free symbiote.
- Attach the escort to the vanilla peaceful colony-visit lord behavior.
- Keep the free symbiote selected by the existing voluntary therapeutic workflow.
- Keep implantation manual and protected by the consent confirmation dialog.
- Add `TokraFactionUtility` so peaceful visitors and therapeutic escorts reuse one hidden persistent Tok'ra faction instance.
- Update English and French incident text, technical documentation and player-wiki drafts.

## 0.1.47-dev — Add Tok'ra therapeutic-opportunity incident prototype
- Added rare storyteller-selected `SG1_TokraTherapeuticOpportunity` incident after day `30`.
- Added a `60`-day minimum refire interval and a low `0.035` base chance.
- Search player-controlled compatible humanoids for non-traumatic biological conditions accepted by the shared Tok'ra healing filter.
- Prefer the candidate with the highest aggregated therapeutic-need severity.
- Spawn one free Tok'ra symbiote at a reachable map edge and send a targeted letter.
- Keep the final implantation voluntary through the existing therapeutic command and consent dialog.
- Keep recent injuries healable by active Tok'ra hosts without allowing a minor wound to trigger the narrative incident.
- Add French DefInjected text, technical documentation and player-wiki drafts.


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
