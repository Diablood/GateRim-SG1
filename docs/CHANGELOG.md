# Changelog

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
