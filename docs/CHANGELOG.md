# Changelog

## 0.3.80-dev - Make Goa'uld relations influence raid doctrines

- Start from published `develop` and annotated tag `v0.3.79-dev` at commit
  `e978acf527681c0b0f6456ce08a08a3f41607652` on
  `feature/goauld-relations-raid-doctrine-interactions`.
- Keep the three persistent domain profiles authoritative at `4/1/1`, `2/3/1`
  and `2/1/3`.
- Add XML-driven relation doctrine modifier Defs for neutrality, rivalry, open
  conflict, truce and alliance.
- Apply the accepted bounded `x1.25` multiplier only to one already eligible
  doctrine weight: direct under alliance, abduction under rivalry and
  destruction under open conflict.
- Resolve one non-stacking relation influence with priority
  `open conflict > alliance > rivalry`; neutrality and truce have no effect.
- Keep existing doctrine thresholds authoritative so an ineligible abduction or
  destruction weight remains zero after relation processing.
- Restrict the new influence to ordinary natural Goa'uld Jaffa raids under
  `Commandement SG-1`.
- Preserve all historical forced raid commands as deterministic paths that
  bypass relation doctrine influence.
- Preserve storyteller frequency, refire delay, vanilla source points, final
  `75% / 110%` relation-pressure factors, alliance outcome probabilities,
  `75/25` and `60/40` budget splits, officer generation and raid strategies.
- Extend the domain-doctrine debug report with base weights, eligible
  pre-relation weights, selected relation Def, priority, multipliers and final
  percentages.
- Validate final local revision `r1`: build `0.3.80.0`, all six focused
  relation/doctrine tests, non-stacking priority, threshold preservation,
  other-storyteller exclusion, deterministic forced-command regressions and a
  clean accepted `Player.log`.
- Add no new doctrine, raid incident, reward or territorial consequence.
- Prepare the validated branch for fast-forward integration, annotated tag
  `v0.3.80-dev` and synchronized wiki publication.

## 0.3.79-dev - Add coordinated allied Goa'uld joint raids

- Start from published `develop` and annotated tag `v0.3.78-dev` on
  `feature/goauld-joint-raids`.
- Preserve the automatic SG-1 Command relation transitions and their neutral RP
  letters: an alliance, war, truce, rivalry or neutrality report changes
  eligibility but never launches an incident immediately.
- Keep `50%` of eligible alliance-context natural raids completely standard so
  the player cannot infer the next attack form from a diplomatic report.
- Split the cooperative half of eligible direct raids evenly between the
  published delayed reinforcement and a new simultaneous joint assault.
- Keep non-direct abduction and destruction doctrines limited to standard or
  delayed-reinforcement outcomes.
- Share the existing non-stacking `110%` joint-raid budget `60/40` between one
  primary and one exact allied domain; add no free threat points.
- Place the two direct-assault forces on reachable opposite map edges, retain
  their exact faction colors and announce both domains in one localized RP
  letter.
- Reuse the published temporary-cooperation tracker and hostility override
  without changing persistent faction goodwill.
- Order both joint detachments to withdraw when either side begins retreating
  or falls to its bounded break threshold.
- Keep the result free of alliance rupture, territorial effects, special
  rewards, pods, extra incident rolls and storyteller-frequency changes.
- Preserve `0.3.78-dev` pending-wave save compatibility by keeping delayed
  reinforcement as serialized manifestation value `0`.
- Add deterministic debug actions for standard, delayed and joint alliance
  outcomes plus primary-withdrawal validation.
- Validate final local revision `r1`: alliance letter without immediate raid,
  standard single-domain outcome, simultaneous opposite-edge joint assault,
  exact faction colors, one shared RP letter, mutual cooperation, bilateral
  withdrawal and a clean accepted `Player.log`.
- Record that the absence of an officer in the forced `1200`-point joint test is
  expected: the forced path does not enable the officer fallback and the `60/40`
  split leaves only `792` primary points, normally excluding the `145`-point
  guard that would be replaced. The natural officer path remains unchanged.
- Prepare the validated branch for fast-forward integration, annotated tag
  `v0.3.79-dev` and synchronized wiki publication.

## 0.3.78-dev - Add delayed allied Goa'uld raid reinforcements

- Start from published `develop` and annotated tag `v0.3.77-dev` on
  `feature/goauld-allied-reinforcements`.
- Extend only ordinary natural Goa'uld raids under Commandement SG-1 when the
  attacking domain has an eligible alliance and no open-conflict precedence.
- Require at least `800` final combined points before an allied wave can be
  scheduled.
- Keep the published non-stacking `1.10` alliance factor as the complete raid
  budget, assigning `75%` to the primary force and `25%` to one exact allied
  domain instead of granting free threat points.
- Delay the allied edge-walk arrival by `1800` to `3600` ticks in normal play.
- Give no advance alert, countdown or letter; reveal the reinforcement only at
  arrival through a short localized RP letter.
- Preserve exact faction identity and color for both domains while suppressing
  their mutual vanilla hostility only during the active cooperative attack.
- Coordinate allied withdrawal when the surviving primary force begins its
  exit and remove the temporary cooperation when either force leaves the map.
- Persist pending arrivals, participating pawns and active cooperation through
  save and reload without changing the underlying faction relation.
- Keep raids on `EdgeWalkIn`; add no pod arrival, incident-frequency roll,
  doctrine, officer, reward or separate storyteller incident.
- Add deterministic debug access at `1200` points with a `600`-tick delay.
- Validate local revision `r1`: silent delay, arrival-only RP letter, distinct
  domain colors, mutual cooperation and a clean accepted `Player.log`.
- Integrate the feature branch by fast-forward, publish annotated tag
  `v0.3.78-dev` and synchronize the changed wiki sources.

## 0.3.77-dev - Establish the core-faction completion gate

- Start from published `develop` and annotated tag `v0.3.76-dev` on
  `feature/core-faction-completion-gate`.
- Establish a blocking development priority: close the existing Tok'ra,
  Goa'uld/Jaffa and Tau'ri/SGC foundation before starting peoples or chapters that
  inherit its systems.
- Keep the closed eight-operation Tok'ra pool stable and treat future Tok'ra
  work before expansion as targeted fixes, final presentation and shared-system
  completion rather than a ninth operation.
- Place final visual work, cross-system audits, SGC equipment, Goa'uld strategic
  consequences and rank equipment, transport rings and Stargate progression
  inside the core-completion phase.
- Block Asgard, Nox, Unas, Replicators, optional Ideology/Royalty integration and
  the all-GateRim world preset until the core gate is explicitly closed.
- Keep weighted Tok'ra host origins as a dependent enrichment milestone after
  several new cultures exist; it is not a reason to leave the current Tok'ra
  foundation open indefinitely.
- Require every later culture or threat to reuse the published cultural,
  naming, faction, mission, threat, persistence, documentation and test
  frameworks instead of creating parallel foundations.
- Define closure as completion, explicit removal or explicit deferral of every
  blocking core milestone; silence or an unchecked roadmap entry cannot count as
  closure.
- Keep the order inside the core phase selectable one milestone at a time while
  forbidding work from the blocked expansion phase.
- Change no gameplay, Def, translation, texture, save data or balance.
- Validate final local revision `r1`: forced build `0.3.77.0`, unchanged duration
  audit with `104` unique keys, project consistency, local Markdown links, exact
  documentation-only scope, main-menu version, existing-save loading and a clean
  accepted `Player.log`.
- Integrate the documentation branch by fast-forward, publish annotated tag
  `v0.3.77-dev` and synchronize the separate wiki sources.

## 0.3.76-dev - Reconcile future roadmap and visual debt

- Start from published `develop` and annotated tag `v0.3.75-dev` on
  `feature/future-roadmap-reconciliation`.
- Remove already published work from the active backlog, including the Tok'ra
  observation-scope rework, debug-menu reorganization, operation-pool audit,
  world icons, threat audit, relation factors and both Jaffa-officer milestones.
- Separate permanent regression contracts from future feature milestones.
- Plan every decided future axis as an individual milestone instead of one broad
  unchecked theme.
- Reserve a complete final-quality art pass for every placeholder and temporary
  texture, including already functional assets such as the Tok'ra observation
  scope and red Jaffa officer equipment.
- Split Goa'uld alliance extensions into independent reinforcement, joint-raid,
  doctrine-interaction, shared-reprisal and alliance-rupture milestones.
- Require strategic safeguards before any territorial expansion or settlement
  destruction layer.
- Add transport rings as two separate future milestones: a bounded player-owned
  platform foundation, then later mission and hostile uses.
- Split Stargate progression into foundations, a first bounded off-world
  expedition and the later functional Stargate.
- Keep Asgard, Nox, Unas, Replicators, optional Ideology/Royalty audits and
  the all-GateRim world preset as separate future milestones.
- Record that unresolved design decisions are asked again when their milestone
  starts instead of being answered prematurely in the backlog.
- Keep speculative queen evolution, Tok'ra cultural reactions, sarcophagus,
  optional-DLC compatibility, adult-symbiote confinement and advanced kara kesh
  functions in `IDEAS_TO_REVISIT.md`.
- Record the failed `r1` delivery method: a context-sensitive patch for three
  long documents did not apply to the maintainer's working tree.
- Supersede it in `r2` with complete replacement files only, and forbid ordinary
  delivery through `.patch` files, applicable diffs or `git apply` instructions.
- Change no gameplay, Def, translation, texture, save data or balance.
- Validate final revision `r2`: forced build `0.3.76.0`, duration audit with
  `104` unique keys, project consistency, local Markdown links, exact
  documentation-only scope, main-menu version, existing-save loading and a clean
  accepted `Player.log`.
- Integrate the documentation branch by fast-forward, publish annotated tag
  `v0.3.76-dev` and synchronize the separate wiki sources.

## 0.3.75-dev - Add Jaffa officers to eligible Goa'uld forces

- Start from published `develop` and annotated tag `v0.3.74-dev` on
  `feature/jaffa-officers-in-goauld-forces`.
- Extend the red-armored Jaffa officer beyond the capture target without adding
  a new incident, scheduler or serialized tracker.
- Require at least five eligible Jaffa in one generated force before an officer
  can appear.
- Replace at most one ordinary guard with one officer, preserving group size.
- Preserve the published capture target `SG1_GoauldJaffaOfficer` at its
  historical `165` combat power.
- Add `SG1_GoauldJaffaFieldOfficer` at `145` combat power, matching the combat
  guard it replaces.
- Add `SG1_GoauldSettlementJaffaOfficer` at `130` combat power, matching the
  settlement guard it replaces.
- Cover ordinary natural Goa'uld raids, settlement defense groups, introduction
  defenders, distress-call hostiles, relay defenders and reinforcements,
  delivery interceptions and diversion assaults.
- Keep groups below five Jaffa unchanged and leave a qualifying group without
  an officer when no budget-equivalent guard exists.
- Keep the capture operation at exactly one target officer and do not add an
  officer to its escort.
- Keep controlled raids, extraction reprisals and inter-domain battlefields
  outside the new replacement layer.
- Guarantee the dedicated red armor, retractable helmet and elite silver mark
  for generated officers.
- Extend Prim'ta initialization and Goa'uld Jaffa cultural matching to the
  new field and settlement officer PawnKinds.
- Add a deterministic `900`-point natural-raid developer action for focused
  officer validation while preserving the historical forced-doctrine tests.
- Add no save field, pawn-count increase, incident-frequency change, doctrine
  change, mission phase or free threat budget.
- Record that cumulative revision `r1` failed its first forced build with two
  `CS0115` errors because the natural-raid and diversion workers attempted to
  override a non-overridable `IncidentWorker_RaidEnemy.GeneratePawns` path.
- In cumulative revision `r2`, remove those invalid overrides and apply the same
  list replacement through the existing `PawnGroupMakerUtility.GeneratePawns`
  Harmony postfix only while an explicit incident-scoped combat-generation
  context is active for the expected pawn-group kind.
- Restore the previous generation context after every attempt so settlement,
  controlled raid, reprisal and unrelated pawn-group generation remain isolated.
- Change local delivery archives to contain only paths added or modified since
  the previous revision instead of duplicating the whole repository.
- Validate final cumulative revision `r2`: build `0.3.75.0`, duration audit with
  `104` unique keys, project consistency, eligible natural raid, below-threshold
  exclusion, settlement and mission replacement, capture isolation, save/reload
  persistence and a clean accepted `Player.log`.
- Integrate the feature branch by fast-forward, publish annotated tag
  `v0.3.75-dev` and synchronize the separate wiki sources.

## 0.3.74-dev - Add distinctive Jaffa capture-officer appearance

- Start from published `develop` and annotated tag `v0.3.73-dev` on
  `feature/distinctive-jaffa-capture-officer-appearance`.
- Add `SG1_JaffaOfficerArmor`, `SG1_JaffaOfficerDeployedHelmet` and the internal
  `SG1_JaffaOfficerRetractedHelmet` state.
- Reserve the new torso armor and retractable helmet for the mission-only
  `SG1_GoauldJaffaOfficer` through an exact `apparelRequired` override.
- Keep ordinary warriors, guards, settlement defenders and the officer's escort
  on their existing brown-and-gold Jaffa equipment.
- Reuse the heavy Jaffa armor and retractable helmet silhouettes for temporary
  red-tinted textures while assigning stable final asset paths dedicated to the
  officer set.
- Generalize the existing retractable-helmet component so each helmet Def pair
  can declare its own deployed and retracted states without changing the
  published standard helmet behavior.
- Preserve heavy-armor protection, movement penalty, helmet coverage, raw armor
  values and automatic/manual retraction modes.
- Add a single `SocialImpact +0.10` offset to the officer torso armor; the helmet
  adds no social bonus, preventing double application across retraction states.
- Gate local crafting of the officer torso armor and deployed helmet behind the
  existing `SG1_JaffaArmor` research. Captured pieces remain immediately usable,
  and the retracted state is never crafted separately.
- Set random-generation commonality to zero so the variants are not assigned as
  ordinary apparel outside their exact mission PawnKind.
- Record the `r1` functional defect: the generated mission target retained his
  weapon, gauntlets and boots but received neither dedicated torso armor nor
  dedicated helmet.
- In cumulative revision `r2`, keep zero commonality and add an explicit
  post-generation verification that creates and equips the two distinctive
  pieces only for the capture target.
- Fail encounter initialization cleanly when either required officer apparel Def
  cannot be equipped instead of silently accepting an indistinct target.
- Record a separate future expansion for officers in eligible raids, Goa'uld
  settlement defenses and suitable missions, limited to zero or one officer in
  groups containing at least five Jaffa and preserving the threat budget.
- Correct the roadmap debt for faction and mission-site world icons already
  published in `0.3.50-dev` and `0.3.51-dev`.
- Validate final cumulative revision `r2`: build `0.3.74.0`, duration audit
  with `104` unique keys, project consistency, exact five-piece target loadout,
  red target/brown-and-gold escort distinction, single `SocialImpact +0.10`
  offset, all three helmet modes, standard-pair isolation, save/reload, capture,
  caravan transport, Tok'ra extraction and a clean accepted `Player.log`.
- Integrate the feature branch by fast-forward, publish annotated tag
  `v0.3.74-dev` and synchronize the changed wiki sources.

## 0.3.73-dev - Add bounded Goa'uld alliance raid-strength bonus

- Start from published `develop` and annotated tag `v0.3.72-dev` on
  `feature/goauld-alliance-raid-strength`.
- Extend the existing relation-derived natural-raid modifier instead of adding
  another incident, frequency roll, scheduler or serialized tracker.
- Give a Goa'uld domain participating in at least one alliance a fixed `1.10`
  natural-raid point factor while Commandement SG-1 is active.
- Preserve the published open-conflict factor at `0.75` and let open conflict
  override alliance when both relations affect the same domain.
- Keep several simultaneous conflicts or alliances non-stacking.
- Keep direct, abduction and destruction doctrine selection based on the
  original vanilla storyteller points before the final factor is applied.
- Keep raid chance, earliest day, shared refire delay, doctrine weights,
  contextual thresholds and every non-natural attack path unchanged.
- Keep Cassandra, Phoebe, Randy and compatible modded storytellers at `1.00`.
- Correct the distinction between an ordinary storyteller incident and an
  externally forced caller before the shared raid worker internally sets
  `parms.forced = true`; ordinary natural raids now receive their relation
  factor while forced regressions remain excluded by default.
- Expand the developer report with alliance state, both configured factors and
  open-conflict precedence.
- Add `Set all pairs: Alliance` for deterministic three-domain non-stacking and
  mixed conflict/alliance validation without save editing.
- Add no save field, joint raid, reinforcement, shared reprisal, doctrine
  interaction, territorial effect or goodwill change.
- Validate final local revision `r1`: build `0.3.73.0`, duration audit with
  `104` unique keys, project consistency, alliance `110%`, open-conflict `75%`,
  mixed-state precedence, non-stacking, storyteller isolation, forced-caller
  exclusion, save/reload and a clean accepted `Player.log`.
- Integrate the feature branch by fast-forward, publish annotated tag
  `v0.3.73-dev` and synchronize the separate wiki sources.

## 0.3.72-dev - Repair 0.3.71 publication documentation

- Start from published `develop` and immutable annotated tag `v0.3.71-dev` on
  `fix/0.3.71-publication-documentation`.
- Record that the validated duration-formatting implementation was published,
  while its final documentation package was omitted from the tagged commit.
- Restore the final project state, roadmap, changelog, current validation result
  and durable regression coverage prepared for `0.3.71-dev-r5`.
- Restore the public content-status, duration-formatting and wiki-home sources.
- Preserve the existing `v0.3.71-dev` tag without deletion, movement or rewrite.
- Advance public and technical metadata to `0.3.72-dev` / `0.3.72.0` so the
  corrective integrated state can receive its own immutable tag.
- Keep the shared formatter, `104`-key compatibility bridge, audit scripts,
  translations, stored ticks, deadlines, cooldowns, recurrence, save data and
  gameplay balance unchanged.
- Require only targeted build, consistency, startup-version, save/reload and
  clean-log validation because the correction adds no gameplay implementation.
- Add a durable publication guardrail requiring supplied finalization ZIPs to
  be extracted before staging and excluding one-shot local `Apply-*` /
  `Publish-*` PowerShell helpers from commits.
- Validate final corrective revision `r1`: build `0.3.72.0`, unchanged
  `104`-key duration audit, project consistency, main-menu metadata, preserved
  save/reload deadline and clean `Player.log`.
- Integrate the corrective branch by fast-forward, publish annotated tag
  `v0.3.72-dev` and synchronize the three changed wiki pages without moving
  `v0.3.71-dev`.

## 0.3.71-dev - Standardize player-facing duration formatting

- Start from published `develop` and annotated tag `v0.3.70-dev` on
  `feature/standardize-duration-formatting`.
- Add `GR_PlayerFacingDurationUtility.Format(int ticks)` as the shared
  player-facing duration entry point over RimWorld's localized vanilla period
  formatter.
- Let RimWorld select readable seconds, hours, days, quadrums or years instead
  of exposing large manually converted hour totals.
- Clamp completed countdowns safely while preserving the exact tick on which
  each deadline, expiry or cooldown completes.
- Redirect legacy duration helpers used by temporary world sites, relay
  reinforcements and the living Jaffa-officer extraction flow.
- Add a narrow Harmony translation bridge for `104` explicitly listed GateRim
  keys whose existing callers still provide rounded hours or decimal days.
- Cover all eight published Tok'ra operation families, their offers and active
  status summaries, secure-communicator cooldowns and dialogs, pending stages of
  the trusted first mission, intercepted-threat estimates and the legacy
  safehouse marker.
- Extend the final audit corrections to framework observation offers, the
  therapeutic-offer inspection, Goa'uld-queen extraction recovery, Tok'ra
  diplomatic cooldown text and historical Goa'uld battlefield fallbacks.
- Correct English and French decoded-relay inspection and caravan-command text
  so a complete localized duration never receives a second fixed unit suffix.
- Add `tools/check-duration-formatting.cmd` and a Windows PowerShell 5.1
  compatible repository-wide audit.
- Reject fixed hour/day suffixes attached to dynamic placeholders, duplicate
  migration keys and unapproved manual tick-to-hour/day conversion near
  player-facing C# code.
- Exempt vanilla period-formatting calls, developer-only raw timing reports and
  mechanical per-day calculations from false positives.
- Keep stored ticks, deadlines, cooldowns, recurrence, expiry, save data and all
  balance values unchanged.
- Correct the first audit script's Windows PowerShell parser failure in
  cumulative revision `r4`.
- Correct seven uncovered translation keys and overly broad C# heuristics in
  final cumulative revision `r5`.
- Validate final local revision `r5`, including build `0.3.71.0`, the complete
  `104`-key audit, project consistency, targeted French and English surfaces,
  save/reload preservation, unchanged timing and a clean accepted startup log.

## 0.3.70-dev - Add open-conflict Goa'uld world battlefield site

- Start from published `develop` and annotated tag `v0.3.69-dev` on
  `feature/goauld-open-conflict-world-battlefield-site`.
- Extend the published open-conflict battlefield scheduler instead of creating
  a parallel world-site orchestration system.
- Share one active slot, recurrence clock, pair anti-repetition state and
  local/world alternation between both battlefield forms.
- Create an optional temporary world site for an exact active Goa'uld domain
  pair whose persistent relation is open conflict.
- Place the site `6–18` world tiles from an eligible player home map and keep it
  available for approximately eight days.
- Add a dedicated world-map battlefield icon and bilingual creation, inspection,
  travel and arrival text.
- Snapshot vanilla storyteller threat points when the site is created and reuse
  the validated `0.35`, `250–1800` points-per-detachment contract.
- Generate the encounter map only when a player caravan reaches the site.
- Reuse the complete `0.3.69-dev` battlefield implementation: map-edge entry,
  rallying, assault announcement, ranged pursuit, morale break, bounded player
  retaliation, two-day battle limit and fixed withdrawal deadline.
- Treat player attackers as additional hostile reinforcements during the mutual
  battle: a proportional nearby subset retaliates while other Jaffa continue
  fighting the rival Goa'uld force.
- Restore RimWorld's vanilla caravan-reformation component after all active
  hostile threats have ended.
- Keep ordinary prisoners, downed pawns, bodies, equipment and selected loot
  available through normal RimWorld caravan behavior.
- Expire an ignored site without mission failure, goodwill change, strategic
  relation change, settlement destruction, territory change or artificial
  reward.
- Keep the shared slot occupied until the encounter map and world object are
  completely removed.
- Use RP-facing inspection text and a local day/hour duration display; reserve a
  complete mod-wide vanilla-duration-formatting audit for `0.3.71-dev`.
- Correct the initial multiline string-literal compilation failure in cumulative
  revision `r2`.
- Restore the exact current-test and DLL consistency markers in cumulative
  revision `r3`.
- Correct inspection text, mixed player/rival targeting and vanilla caravan
  reformation in final cumulative revision `r4`.
- Validate final local revision `r4`, including build `0.3.70.0`, world-site
  creation, travel, lazy map generation, mutual combat, player intervention,
  morale break, withdrawal, caravan reformation, neutral expiration, shared
  orchestration, save/reload, existing Goa'uld regressions and a clean
  `Player.log`.

## 0.3.69-dev - Add open-conflict Goa'uld battlefield incident

- Start from published `develop` and annotated tag `v0.3.68-dev` on
  `feature/goauld-open-conflict-battlefield-incident`.
- Add rare local battlefields reserved for `SG1_GateRimStoryteller`.
- Select an exact pair of active Goa'uld domains whose persistent relation is
  open conflict.
- Preserve one active battlefield slot, hidden recurrence delays, save/reload
  state and pair anti-repetition.
- Generate two threat-scaled Jaffa detachments belonging to the exact selected
  domains.
- Spawn both forces from valid map-edge cells and send them to separate rally
  points before combat.
- Announce the assault after the forces assemble, with a bounded fallback delay
  when terrain prevents complete formation.
- Make ranged Jaffa pursue until they obtain weapon range and line of sight
  instead of remaining stationary against distant targets.
- Keep both forces initially focused on one another rather than launching an
  organized attack against the colony.
- Allow optional player intervention with retaliation limited to `1800` quiet
  ticks and a `35`-cell pursuit boundary.
- Limit retaliation during withdrawal to `6000` ticks without extending the
  fixed forced-exit deadline.
- Allow a force to break contact when it alone falls to `30%` or less of its
  initial mobile strength.
- Preserve the absolute two-day battle limit and leave downed pawns, prisoners,
  corpses and abandoned equipment on the map.
- Add bilingual letters, assault and morale-break messages, diagnostics and
  deterministic developer actions.
- Reserve `0.3.70-dev` for the corresponding temporary battlefield site on the
  world map, reusing the same battle-generation contract and active slot.
- Correct functional-test issues across cumulative revisions `r1` to `r5`,
  including local-map targeting, active combat, map-edge arrival, rallying,
  ranged pursuit, player retaliation and bounded withdrawal.
- Correct the `CS1628` lambda capture build failure without gameplay changes in
  cumulative revision `r6`.
- Validate final local revision `r6`, including build `0.3.69.0`, edge arrival,
  rally and assault flow, two-sided combat, player intervention limits, morale
  break, withdrawal, save/reload, existing Goa'uld regressions and a clean
  `Player.log`.

## 0.3.68-dev - Add open-conflict Goa'uld pressure reduction

- Start from published `develop` and annotated tag `v0.3.67-dev` on
  `feature/goauld-open-conflict-pressure-reduction`.
- Add a bounded `0.75` natural-raid point factor for each Goa'uld domain engaged
  in at least one active open conflict.
- Apply the factor only while `SG1_GateRimStoryteller` is active.
- Never stack the reduction when one domain is involved in several open
  conflicts.
- Preserve the original storyteller points for doctrine eligibility and
  selection, then reduce only the final force budget.
- Keep the single natural raid incident, `baseChance`, earliest day and shared
  refire delay unchanged.
- Exclude controlled raids, extraction reprisals, mission attacks, intercepted
  threats and deterministic forced tests from the reduction.
- Add relation and threat-progression diagnostics showing the original points,
  effective points, active factor and reason.
- Add a dedicated developer action that exercises the real reduced natural-raid
  path without changing the deterministic regression commands.
- Update technical documentation, current validation, durable tests and the
  French player wiki.
- Replace the failed patch-based local deliveries with cumulative complete-file
  revision `r3`.
- Validate final local revision `r3`, including the `0.3.68.0` build, the `75%`
  open-conflict factor, non-stacking behavior, storyteller isolation,
  save/reload, unchanged doctrine selection, excluded reprisal and forced paths,
  existing Goa'uld regressions and a clean `Player.log`.

## 0.3.67-dev - Adopt develop-based branch workflow

- Start the workflow migration from published tag `v0.3.66-dev` without
  rewriting any existing tag or historical feature branch.
- Create `develop` exactly at the commit targeted by `v0.3.66-dev` as the
  canonical integration branch.
- Reserve `main` for the future stable `1.0.0` line and later stable hotfixes.
- Require ordinary `feature/*` and `fix/*` branches to start from an up-to-date
  `develop` branch.
- Integrate validated milestones with `git merge --ff-only` so the validated
  feature commit is exactly the integrated commit.
- Create each annotated `v...-dev` tag only after integration and require it to
  match local and remote `develop`.
- Document that annotated tags must be peeled with `git rev-list -n 1` or
  `<tag>^{}` before their commit IDs are compared.
- Make remote publication of temporary feature branches optional and permit
  their deletion after the integrated tag is verified.
- Add `docs/BRANCHING_WORKFLOW.md` and update agent, publication,
  documentation-map and handoff instructions.
- Correct the first three local delivery revisions by replacing fragile
  marker-based edits with complete replacement files and standard Git patches.
- Change no gameplay C#, Def, translation, texture, save data or storyteller
  behavior; update only the two public wiki version metadata lines.
- Bump the public development version to `0.3.67-dev` and rebuild assembly
  `0.3.67.0` for metadata consistency.
- Validate final local revision `r4`: branch ancestry, annotated-tag comparison,
  full project-consistency pass, forced `0.3.67.0` rebuild, minimal RimWorld
  startup and unchanged gameplay behavior.
- Integrate `feature/develop-branch-workflow` into `develop` with a fast-forward,
  publish annotated tag `v0.3.67-dev`, synchronize the separate wiki and leave
  `main` unchanged.
- Reserve `0.3.68-dev` for the open-conflict Goa'uld pressure-reduction slice.

## 0.3.66-dev - Add persistent Goa'uld inter-domain relations

- Start from published tag `v0.3.65-dev` on
  `feature/goauld-inter-domain-relations`.
- Add one persistent state for every unordered pair of Goa'uld System Lord
  faction instances: neutral, rivalry, open conflict, truce or alliance.
- Keep relations attached to factions rather than current leaders and reconcile
  new games, older saves, multiple domains and newly created domains.
- Advance relations only under `SG1_GateRimStoryteller`; freeze and shift all
  strategic deadlines while another storyteller is active.
- Use bounded transitions, `8â€“16` day initial delays, `12â€“24` day pair delays
  and `5â€“10` day global report spacing.
- Avoid selecting the previous pair again when another eligible pair exists.
- Add three English and French RP report variants per resulting state with
  immediate text anti-repetition.
- Add relation diagnostics, direct state setters, forced transitions, reset and
  an additional-domain test generator.
- Keep raids, threat points, doctrines, reprisals, goodwill, battles, alliances,
  expansion and settlement destruction mechanically unchanged.
- Validate final local revision `r1` after the forced `0.3.66.0` rebuild,
  including all five states, French RP reports, save/reload persistence,
  Cassandra suspension and resumption, pair anti-repetition, existing Goa'uld
  regressions and a clean `Player.log`.
- Publish `feature/goauld-inter-domain-relations`, annotated tag
  `v0.3.66-dev` and the synchronized separate wiki.

## 0.3.65-dev - Add GateRim SG-1 storyteller foundation

- Start from published tag `v0.3.64-dev` on
  `feature/sg1-storyteller-foundation`.
- Add the optional `SG1_GateRimStoryteller` with English/French player text and
  a temporary original portrait.
- Use Cassandra Classic's currently resolved RimWorld 1.6 component list as the
  ordinary incident baseline instead of embedding a version-sensitive copy.
- Append one no-op GateRim orchestration component reserved for future strategic
  systems.
- Add persistent activation, deactivation and save/reload lifecycle state.
- Add a shared activation utility so future systems can remain strictly scoped
  to the SG-1 storyteller.
- Add a developer report for baseline initialization, component counts,
  activation state and explicit confirmation that no future relation system is
  active.
- Preserve every existing incident contract and leave Cassandra, Phoebe, Randy
  and compatible modded storytellers unchanged.
- Record future persistent Goa'uld relation states, open-conflict battlefield
  incidents, alliance pressure and later coordinated-raid possibilities without
  implementing them in this milestone.
- Validate the `r1` storyteller foundation, selection, Cassandra baseline, activation lifecycle, save/reload, diagnostics, existing-incident regressions and a clean `Player.log`.
- Shorten the English and French storyteller descriptions in `r2` to a direct behavioral summary matching the vanilla storyteller style and avoiding a French selection-panel scrollbar.
- Keep all C#, storyteller components, textures, save data and balance unchanged.
- Validate final revision `r2`, including the concise French text without a selection-panel scrollbar.
- Publish `feature/sg1-storyteller-foundation`, annotated tag `v0.3.65-dev` and the synchronized separate wiki.

## 0.3.64-dev - Add persistent Goa'uld domain doctrine profiles

- Start from the published documentation-consolidation tag `v0.3.63-dev` on
  `feature/goauld-domain-doctrine-profiles`.
- Add data-driven conquest `4/1/1`, enslavement `2/3/1` and scorched-earth
  `2/1/3` profiles.
- Persist one profile per Goa'uld faction instance across leader replacement
  and save/reload.
- Reconcile older saves and multiple domain instances.
- Preserve the single natural raid incident, vanilla threat points, refire delay
  and existing eligibility thresholds.
- Modify only relative direct, abduction and destruction doctrine weights.
- Add qualitative faction information, multi-domain diagnostics and targeted
  developer setters.
- Use explicit English/French keyed text for profile names and descriptions.
- Add a repository `.gitattributes` line-ending policy.
- Integrate the milestone into the consolidated documentation structure without
  restoring any document removed by `0.3.63-dev`.
- Remove the stale `Tokra-Interaction-Roadmap` sidebar link left by the first
  corrected package; the page remains intentionally deleted.
- Rebuild and validate final revision `r2`, including French localization,
  save/reload persistence, the three raid regressions and a clean `Player.log`.
- Pass the consolidated project-consistency check after removing the obsolete
  sidebar link.
- Publish `feature/goauld-domain-doctrine-profiles`, annotated tag
  `v0.3.64-dev` and the synchronized separate wiki.

## 0.3.63-dev - Consolidate project documentation

- Start from `v0.3.62-dev` on `feature/documentation-consolidation`.
- Add `docs/README.md` as the authoritative documentation map.
- Reduce the roadmap to active work and durable rules.
- Transfer unresolved ideas before removing obsolete documents.
- Remove the redundant root `docs/Content-Status.md` and obsolete Tok'ra roadmap.
- Extend consistency checks for local Markdown targets.
- Change no gameplay behavior, Def, translation or texture.
- Validate and publish final revision `r1`, annotated tag `v0.3.63-dev` and the
  synchronized wiki.

## 0.3.62-dev - Add Goa'uld healing device prototype

- Add `SG1_GoauldHealingBracelet` as a portable medical device separate from
  the kara kesh.
- Require biological naquadah traces and adjacency to a living humanlike
  biological patient.
- Stabilize all bleeding injuries, heal at most `20` severity across four
  injuries and reduce blood loss by at most `0.15`.
- Apply `12000` ticks of fatigue and a persistent `30000`-tick cooldown.
- Reserve natural equipment to System Lords, add guarded hostile self-use and
  raise their `combatPower` from `550` to `625`.
- Add spacer research after Goa'uld biotechnology and kara kesh research.
- Validate final local revision `r1`, publish branch, annotated tag
  `v0.3.62-dev` and synchronized wiki.

## 0.3.61-dev - Add kara kesh paralysis hold

- Start from published tag `v0.3.60-dev` on
  `feature/kara-kesh-paralysis-hold`.
- Add one maintained single-target paralysis mode to the existing kara kesh
  without adding another item or research project.
- Limit activation to conscious hostile humanlike flesh pawns within `6.9`
  cells and direct line of sight.
- Consume `2.5` points from the shared four-point shield reserve, suspend
  recharge while maintained and start a persistent `1800`-tick cooldown.
- Apply `SG1_KaraKeshParalysisHold` for at most `600` ticks, cap Moving at `0`
  and multiply Manipulation by `0.1`, without direct damage or added pain.
- Serialize the exact source apparel and target, then revalidate the link every
  `15` ticks across equipment changes and save/reload.
- End the hold early when wearer control, biological eligibility, shield state,
  hostility, map, range or line of sight becomes invalid.
- Add a manual release command that preserves spent energy and cooldown.
- Let hostile System Lords prioritize the same hold before neural attack and
  kinetic blast, then raise their `combatPower` from `500` to `550`.
- Add English/French text, deterministic debug actions, technical documentation,
  durable tests and updated player-wiki drafts.
- Record the maintainer's deferred kara kesh concepts only in
  `docs/IDEAS_TO_REVISIT.md`; none is implemented or promised here.
- Validate final local revision `r1` after a forced `0.3.61.0` build,
  including player targeting, capacity suppression, shared energy, maintained
  interruptions, hostile AI priority, save/reload persistence, manual release
  and a clean `Player.log`.
- Publish branch `feature/kara-kesh-paralysis-hold`, annotated tag
  `v0.3.61-dev` and the synchronized wiki.

## 0.3.60-dev - Add kara kesh neural attack

- Start from published tag `v0.3.59-dev` on
  `feature/kara-kesh-neural-attack`.
- Add a targeted neural-attack gizmo for biologically eligible kara kesh
  wearers without introducing a second item or research project.
- Limit targets to conscious hostile humanlike flesh pawns within `8.9` cells
  and direct line of sight.
- Consume `1.75` points from the shared four-point shield reserve, pause recharge
  and enter a persistent `1200`-tick cooldown.
- Add temporary `SG1_KaraKeshNeuralAgony` for `600` ticks with `0.45` pain and
  a `0.8` Consciousness factor.
- Deal no direct damage, knockback, stun or forced Moving-capacity lock so
  prolonged paralysis remains a separate future function.
- Let hostile non-player wearers prioritize the same neural attack on a
  `60`-tick target check, then retain kinetic-blast fallback behavior.
- Increase System Lord `combatPower` from `450` to `500` for the added control
  while preserving the shared-energy tradeoff.
- Persist neural cooldown through save/reload and refresh the temporary Hediff
  duration when another valid wearer reapplies the effect.
- Add English/French text, a stable Hediff Def, deterministic debug actions,
  technical documentation, durable tests and updated player-wiki drafts.
- Keep prolonged paralysis, downed-target torture, mental control, remote
  commands, area effects and a dedicated gizmo icon outside this milestone.
- Validate final local revision `r1` after a forced `0.3.60.0` rebuild,
  including player targeting, shared shield energy, temporary neural agony,
  hostile AI use, save/reload persistence and a clean `Player.log`.
- Publish branch `feature/kara-kesh-neural-attack`, annotated tag
  `v0.3.60-dev` and the synchronized wiki.

## 0.3.59-dev - Add kara kesh kinetic blast

- Start from published tag `v0.3.58-dev` on
  `feature/kara-kesh-kinetic-blast`.
- Add a targeted kinetic-blast gizmo for biologically eligible kara kesh
  wearers without introducing a second item or research project.
- Share the existing four-point shield reserve: each blast costs `1.25` energy,
  pauses recharge and enters a `900`-tick cooldown.
- Limit targets to hostile pawns within `10.9` cells and direct line of sight.
- Apply `12` blunt damage, `0.25` armor penetration, a `120`-tick stun and safe
  knockback of up to two walkable, unoccupied cells.
- Let hostile non-player wearers use the same implementation automatically on a
  `60`-tick target check.
- Persist the cooldown across save/reload and expose ready or recharge status in
  the apparel inspection text.
- Increase System Lord `combatPower` from `400` to `450` for the added offensive
  control while preserving the shared-energy tradeoff.
- Add English/French text, deterministic debug actions, technical documentation,
  durable tests and an updated player-wiki draft.
- Keep neural attack, prolonged paralysis, torture, remote control, area damage
  and a dedicated gizmo icon outside this milestone.
- Record the failed `r1` rebuild: RimWorld 1.6 exposes
  `MapPawns.AllPawnsSpawned` as `IReadOnlyList<Pawn>`, not `List<Pawn>`.
- Correct that compile-time collection declaration in cumulative local revision
  `r2` without changing kinetic-blast behavior, balance or save data.
- Validate final local revision `r2` after a forced `0.3.59.0` rebuild,
  including player targeting, shared shield energy, cooldown refusal, safe
  knockback, hostile AI use, save/reload persistence and a clean `Player.log`.
- Publish branch `feature/kara-kesh-kinetic-blast`, annotated tag
  `v0.3.59-dev` and the synchronized wiki.

## 0.3.58-dev - Add persistent biological naquadah traces

- Start from published tag `v0.3.57-dev` on
  `feature/persistent-naquadah-biological-traces`.
- Promote `SG1_NaquadahBlood` from a visible prototype into the shared acquired
  marker for biological naquadah eligibility.
- Grant persistent traces to active Goa'uld and Tok'ra adult hosts and to Jaffa
  carrying a Prim'ta.
- Preserve the marker after adult symbiote extraction or Prim'ta removal so
  former hosts remain biologically compatible.
- Add immediate lifecycle hooks and a stateless reconciliation component for
  existing saves, map pawns, player caravans, faction leaders and world pawns.
- Centralize all eligibility checks in `NaquadahTraceUtility` instead of using
  faction, hostility or PawnKind approximations.
- Require persistent traces to activate the kara kesh shield while still
  allowing any pawn to haul, store or wear the device.
- Hide the shield field and energy gizmo, allow outgoing fire and bypass damage
  absorption for an ineligible wearer.
- Add bilingual descriptions, an inactive-wearer explanation, deterministic
  debug tools, technical documentation, durable tests and a player-wiki draft.
- Keep nearby-symbiote detection, trace quantity/decay, offensive kara kesh
  modes and Biotech gene-extractor restrictions outside this milestone.
- Validate final local revision `r1` after a forced `0.3.58.0` rebuild,
  including inactive and active kara kesh states, persistent former-host traces,
  save/reload behavior and a clean `Player.log`.
- Publish branch `feature/persistent-naquadah-biological-traces`, annotated tag
  `v0.3.58-dev` and the synchronized wiki.

## 0.3.57-dev - Add System Lord kara kesh shield

- Start from published tag `v0.3.56-dev` on `feature/goauld-system-lord-personal-shield`.
- Add a kara kesh required only by generated Goa'uld System Lords and implement
  its personal-shield mode as the first functional slice.
- Reuse native RimWorld shield behavior: ranged absorption, melee and heat
  bypass, blocked outgoing fire, immediate audiovisual EMP collapse and a full
  reset after `1800` ticks.
- Give the Goa'uld field `4.0` maximum energy, `0.2` recharge and `0.01` energy
  loss per damage, strictly outperforming the vanilla shield belt while keeping
  melee, heat and EMP as decisive counters.
- Increase System Lord `combatPower` from `170` to `400` so pawn-group budgets
  account for the added protection.
- Add `Kara kesh` research directly after mandatory `Jaffa armor` research at a
  cost of `3000`.
- Allow machining-table manufacture at Crafting `12` for six advanced
  components, `100` plasteel and `60` gold; captured shields remain usable
  before research.
- Keep natural recovery limited to a real System Lord and exclude the item from
  traders and random generation.
- Replace the ineffective required-apparel assignment with one-time persistent
  initialization, including migration of existing System Lords without
  replacing a shield later removed by the player.
- Prevent the deterministic debug subject from generating incompatible random
  family relations.
- Pause active recharge for `300` ticks after every absorbed hit so sustained
  fire can drain the field while isolated shots remain ineffective.
- Add French text, deterministic debug tools, technical documentation, durable
  tests, a synchronized player-wiki page and a stable placeholder texture path
  for the future global visual pass.
- Reserve the kara kesh's kinetic, neural, paralysis and remote-control
  functions, together with biological naquadah eligibility, for separate future
  milestones.
- Validate final local revision `r5` in game and publish the dedicated branch,
  annotated tag `v0.3.57-dev` and synchronized wiki.

## 0.3.56-dev - Add Goa'uld extraction ultimatum

- Start from published tag `v0.3.55-dev` on `feature/goauld-extraction-ultimatum`.
- Replace the immediate extraction-reprisal warning with a one-day choice from the offended Goa'uld domain.
- Allow the colony to surrender the exact extracted symbiote and avert the raid.
- Turn refusal or expiration into the delayed, domain-specific reprisal validated in `0.3.55-dev`.
- Preserve the extraction-time vanilla threat-point snapshot, one-reaction limit, 15-day cooldown and existing pending-reprisal saves.
- Add a persistent choice letter, English/French text and deterministic debug actions for surrender, refusal, expiration and attack.
- Stop the generated-host caste scanner from dereferencing a removed symbiote after successful active extraction.
- Add `Decide later` / `Voir plus tard` through RimWorld's native postpone-letter behavior while preserving the one-day timeout.
- Turn an immediately fatal active-extraction failure into a direct domain reprisal without offering an impossible surrender.
- Keep the demanded symbiote anesthetized while the ultimatum remains unresolved.
- Treat destruction of that symbiote as immediate defiance and announce the exact remaining delay before the reprisal raid.
- Detach an extracted generated Goa'uld caste host from the System Lord domain while preserving colony prisoner status, recruitment and release choices.
- Preserve the existing displaced-faction restoration for previously possessed player pawns and leave unrelated pawn origins unchanged.
- Validate final revision `r5`, including former-host faction release, prisoner choices and `Player.log`.
- Publish branch `feature/goauld-extraction-ultimatum`, annotated tag `v0.3.56-dev` and the synchronized wiki.

## 0.3.55-dev - Add Goa'uld extraction reprisals

- Start from published tag `v0.3.54-dev` on `feature/goauld-domain-extraction-reprisal`.
- Treat a successful active Goa'uld extraction on a player home map as the first visible domain affront.
- Preserve or safely backfill the generated symbiote's exact System Lord domain allegiance.
- Send an immediate bilingual warning naming the offended domain, extracted symbiote and former host.
- Persist one delayed reprisal per domain with the extraction-time vanilla threat-point snapshot.
- Reuse `SG1_GoauldJaffaNaturalRaid` while honoring the explicitly offended faction and retaining on-foot edge arrival.
- Prevent stacking with one pending reaction per domain and a 15-day cooldown after resolution.
- Add exact debug controls, durable tests, technical documentation and synchronized wiki drafts.
- Validate final revision `r1`: warning, persistence, domain-aligned raid, edge arrival, cooldown and `Player.log` are accepted.
- Publish branch `feature/goauld-domain-extraction-reprisal`, annotated tag `v0.3.55-dev` and the synchronized wiki.

## 0.3.54-dev - Enable natural Goa'uld assault doctrines

- Start from published tag `v0.3.53-dev` on `feature/goauld-natural-assault-doctrines`.
- Keep one low-frequency natural incident and its shared 18-day refire delay instead of creating independent doctrine incidents.
- Keep direct assault always eligible with weight `2`.
- Enable abduction with weight `1` from `800` vanilla threat points when at least two free colonists are present.
- Enable destruction with weight `1` from `1800` points when building wealth reaches `10 000`.
- Preserve zero generic selection curves, doctrine-specific warning text and retreat behavior, and forced on-foot edge arrival.
- Add a doctrine-weight report and deterministic natural-worker test commands for all three doctrines.
- Record partial `r1` validation and make `r2` debug commands bypass natural eligibility gates as their `Force` labels promise; keep exact thresholds visible in the report.
- Validate final revision `r2`: natural direct, abduction and destruction paths, deterministic debug access, edge arrival and `Player.log` are accepted.
- Align English/French faction descriptions, technical documentation, durable tests and player-wiki drafts.
- Publish branch `feature/goauld-natural-assault-doctrines`, annotated tag `v0.3.54-dev` and the synchronized wiki.

## 0.3.53-dev - Audit Goa'uld threat progression

- Start from published tag `v0.3.52-dev` on `feature/goauld-threat-progression-audit`.
- Adopt RimWorld's vanilla threat points as the common difficulty source for existing Goa'uld raids, hostile sites and reprisals, independent of the active storyteller.
- Replace the intercepted-threat system's fixed `500` points with a persistent snapshot captured when the warning begins.
- Resolve missing natural-raid points from the current vanilla storyteller while keeping explicit developer-test points deterministic.
- Remove early-game combat ceilings from the introduction, distress, Jaffa-officer capture, temporary-delivery and diversion encounters while retaining their encounter factors and minimum forces.
- Store relay-sabotage difficulty on the world site before travel, scale its defenders and reinforcements without the previous eight/three-pawn caps, and align the generated Jaffa ratio more closely with the faction Combat group.
- Select a command bunker, split relay station or walled courtyard from increasing defender-point tiers instead of choosing one fixed layout randomly.
- Preserve the intentional one-to-four free-symbiote cap because implantation creates persistent biological consequences.
- Keep natural abduction/destruction doctrines disabled and defer approved ultimatum and rival-domain systems to dedicated future milestones.
- Add an exact developer submenu, technical documentation, durable tests and synchronized player-wiki drafts.
- Record successful `r1` threat-scaling tests and force every shared Goa'uld/Jaffa raid path to vanilla `EdgeWalkIn`, preventing high-point drop-pod arrivals without changing force size or doctrine.
- Validate final local revision `r2`: advanced direct raid, controlled abduction/destruction doctrines and intercepted raid all arrive from the map edge without pods; scaling, doctrine behavior and `Player.log` are accepted.
- Publish branch `feature/goauld-threat-progression-audit`, annotated tag `v0.3.53-dev` and the synchronized wiki.

## 0.3.52-dev - Add the Goa'uld faction caste summary

- Start from final annotated tag `v0.3.51-dev` on `feature/goauld-caste-world-summary`.
- Complement the Goa'uld faction description with qualitative Jaffa-servant, Goa'uld-host and System Lord host castes.
- Keep vanilla xenotype percentages intact and explain that possession is an acquired parasitic state rather than a germline xenotype.
- Apply the summary only to `SG1_GoauldSystemLordPrototype`; Free Jaffa and other faction descriptions remain unchanged.
- Preserve faction counts, settlement composition, pawn generation, raids, leaders, names and all host initialization behavior.
- Clarify the publication procedure so a validated milestone publication includes its annotated final tag and wiki synchronization unless explicitly excluded.
- Validate final local revision `r1`: Goa'uld caste tooltip, unchanged Free Jaffa tooltip, successful world generation and accepted `Player.log`.
- Publish branch `feature/goauld-caste-world-summary`, annotated tag `v0.3.52-dev` and the synchronized wiki.

## 0.3.51-dev - Add GateRim mission-site world icons

- Start from validated branch commit `adb1eed` on `feature/operation-site-icon-overhaul`.
- Reject the initial `Town` / `ItemStash` proposal because it did not implement the requested semantic mission-site types.
- Add six dedicated colored icon families for clandestine Tok'ra contact, Tok'ra logistics, Tok'ra distress, encrypted Goa'uld objective, Goa'uld relay sabotage and a Jaffa officer field position.
- Keep the preliminary and revealed safehouse under one visual type, and keep hidden distress-call outcomes under one shared signal icon.
- Record visual approval of all six silhouettes and their mission symbolism, with colored fills authorized for these non-faction site icons.
- Point each affected expanding-icon field to the matching transparent `128x128` PNG.
- Record partial `r2` validation: all six expanded icons render well, but custom base textures rotate inappropriately at close world-map zoom.
- Restore the vanilla `GenericSite` base texture for all seven mission objects in `r3` while retaining the dedicated colored expanding icons.
- Group icon-test actions into independent arcs and the single active organic-operation slot, with explicit replacement messages and no gameplay-state rule change.
- Add a dedicated `Mission-site icon tests...` debug submenu with the true world-site `defName` labels, so manual validation does not depend on indirect or truncated debug-menu entries.
- Use C# only for direct test access, never for dynamic hidden-variant icons.
- Validate final local revision `r3`: six colored expanded icons, vanilla `GenericSite` close-zoom rendering, documented replacement and coexistence rules, and a clean accepted `Player.log`.

## 0.3.50-dev - Add GateRim faction world icons

- Start from published tag `v0.3.49-dev` on `feature/faction-world-icon-overhaul`.
- Keep the existing dedicated Tok'ra icon from `0.3.49-dev` unchanged.
- Add tintable white/alpha world-faction icons for the Free Jaffa, Goa'uld System Lord domains and SGC expedition.
- Point `SG1_FreeJaffa`, `SG1_GoauldSystemLordPrototype` and `SG1_PlayerSGCExpedition` to their dedicated `FactionDef.factionIconPath` textures.
- Preserve RimWorld's vanilla color variation for repeated faction copies by avoiding baked colors in the icon PNGs.
- Replace the first local icon pass after tester feedback: keep the working tint behavior but simplify the silhouettes and add thick dark outlines for better readability at the final UI size.
- Validate local revision `r2`: Free Jaffa, Goa'uld, Tok'ra and SGC icons are readable in game, duplicate-faction tint variation remains vanilla and `Player.log` is accepted by the tester.

## 0.3.49-dev - Add optional Tok'ra world-faction selection

- Start from published tag `v0.3.48-dev` on `feature/tokra-world-faction-selection-audit`.
- Correct the initial `r1/r2` interpretation after comparison with vanilla mechanoid and insect world-selection behavior.
- Show `SG1_Tokra` in the configurable world-faction list, selected once by default and limited to a maximum count of one.
- Allow the player to remove the faction and continue using the rest of GateRim SG-1.
- Keep the selected Tok'ra faction hidden in normal diplomacy and settlement-free.
- Remove the mandatory game-start count and all runtime new-game, load and periodic recreation.
- Change historical `GetOrCreate...` helpers to resolver-only compatibility aliases.
- Disable the introduction scheduler, organic-operation scheduler, Tok'ra storyteller incidents and communicator interactions when no Tok'ra faction exists.
- Replace the reconciliation command with a read-only world-selection audit.
- Retain duplicate detection without destructive cleanup.
- Add a dedicated Tok'ra faction icon for the world-faction selection row.
- Add a yellow world-generation warning when the Tok'ra row is removed, explaining that Tok'ra contacts, the introduction questline, incidents and recurrent operations are disabled for that game.
- Declare the Harmony mod dependency for this narrow UI patch while keeping `0Harmony.dll` external to GateRim SG-1.
- Validate `r4` partially: the dedicated icon is visible, but the warning is skipped because the Harmony insertion sits before a vanilla branch target.
- Move the `r5` warning append after vanilla resets `warningHeight`, before the warning text-length check, so the Tok'ra warning follows the immediate mechanoid/insect flow.
- Validate local revision `r5`: dedicated icon visible, Tok'ra removal warning appears immediately, re-adding Tok'ra removes the warning and `Player.log` is accepted by the tester.

## 0.3.48-dev - Add Goa'uld System Lord leader names

- Start from published tag `v0.3.47-dev` on `feature/goauld-system-lord-leader-names`.
- Add `SG1_NamerPawnGoauldSystemLord` and assign it to the fixed System Lord host PawnKind through `nameMaker` and `nameMakerFemale`.
- Provide `575` original Goa'uld personal names and `24` throne-house bynames for `13,800` formal generation-time identities.
- Repeat the personal name as the explicit nickname so short labels remain culturally readable while faction interfaces display the full two-part identity.
- Reconcile the native visible name with the persistent symbiote name when the generated leader receives its adult symbiote state.
- Generate and store a distinct off-world human host name before Hediff attachment, using the existing structured host-name persistence fields.
- Restore the stored host name when a supported release or extraction removes a Goa'uld symbiote from a pawn currently displaying the symbiote identity.
- Keep domain and settlement names independent from the generated leader, with no canon System Lord names.
- Preserve existing saves, leader titles, backstories, equipment, diplomacy, raids and temporary world icons.
- Validate final local revision `r1`, including successful world generation and varied cultural System Lord names visible before tile selection.
- Keep explicit save/reload, persistent host/symbiote inspection and supported extraction as durable regression checks without presenting them as separately executed focused tests.
- Publish branch `feature/goauld-system-lord-leader-names`, final tag `v0.3.48-dev` and the synchronized wiki.

## 0.3.47-dev - Add Free Jaffa faction-leader names

- Start from published tag `v0.3.46-dev` on `feature/free-jaffa-faction-leader-names`.
- Observe in local revisions `r1` and `r2` that post-generation scans still leave the leader name vanilla in the world-creation interface.
- Move generation-time naming to `PawnKindDef.nameMaker` on `SG1_FreeJaffaGuard`, the fixed Free Jaffa leader kind.
- Add `SG1_NamerPawnFreeJaffa` and assign it through the supported `nameMaker` and `nameMakerFemale` fields.
- Teach the cultural name manager to preserve an existing native PawnKind-generated name instead of assigning a second name after the game starts.
- Remove the provisional faction-owner fallback and dedicated leader-processing save registry introduced by `r1` and `r2`; no new persistent data remains in the milestone.
- Remove the invalid `chanceToUseNameMaker` PawnKind field after the first `r3` startup error.
- Diagnose in `r4` that a one-token RulePack result becomes a `NameTriple` with empty first and last fields, making every later candidate confusingly similar and producing `Could not get new name` during world generation.
- Replace the one-token grammar in `r5` with `576` explicit Free Jaffa personal names and `24` language-neutral clan bynames.
- Repeat the personal name as the explicit nickname so the short pawn label remains the personal name while the full diplomatic label shows the clan byname.
- Preserve faction names, settlement names, leader titles, backstories, equipment and diplomacy.
- Keep Goa'uld System Lord names, Tok'ra faction-selection behavior and faction world icons in their separate planned branches.
- Validate final local revision `r5`, including successful multi-faction world generation, varied formal Free Jaffa leader names visible before tile selection and absence of the earlier name-exhaustion failure.
- Keep replacement-leader, save/reload and full Free Jaffa regression cases as durable follow-up checks without presenting them as separately executed focused tests.
- Publish branch `feature/free-jaffa-faction-leader-names`, final tag `v0.3.47-dev` and the synchronized wiki.

## 0.3.46-dev - Add Goa'uld world-name generators

- Remove the shared `fixedName` from `SG1_GoauldSystemLordPrototype` while preserving its generic world-creation label.
- Replace `NamerFactionPirate` and `NamerSettlementPirate` with dedicated GateRim SG-1 RulePackDefs.
- Add `SG1_NamerFactionGoauldDomain`, combining `12` forms of power and `24` imperial or religious themes for `288` possible domain names.
- Add `SG1_NamerSettlementGoauldDomain`, combining `12` place types, `24` themes and `5` ordinal ranks for `1,728` possible settlement names.
- Add indexed French translations with lower-case internal common nouns, selected proper-title capitals and masculine/feminine ordinal agreement.
- Keep the generated domain name independent from the separately generated System Lord leader, avoiding false identity links or unplanned canon names.
- Apply the new names only to newly generated factions and settlements; existing save names remain serialized and unchanged.
- Add no custom settlement layout, icon, texture, faction, PawnKind, mission, raid behavior or persistent save data.
- Validate final local revision `r1`, including varied domain and settlement names, natural French casing, bilingual RulePackDef loading, unchanged serialized-name behavior, Goa'uld faction regressions and a clean `Player.log`.
- Record dedicated follow-up branches for Free Jaffa leader names, Goa'uld System Lord leader names and the global faction world-icon overhaul without extending this milestone.
- Publish branch `feature/goauld-world-names`, final tag `v0.3.46-dev` and the synchronized wiki.

## 0.3.45-dev - Add Free Jaffa world-name generators

- Replace the remaining vanilla outlander faction and settlement name makers on `SG1_FreeJaffa` with dedicated GateRim SG-1 RulePackDefs.
- Add an initial curated pool of `24` settlement names in local revision `r1`, then replace it in `r2` with a bilingual combinatorial grammar producing `600` distinct settlement results.
- Keep unnumbered settlement names dominant while allowing natural ordinal forms instead of technical suffixes such as `2` or `3`.
- Refine French casing in cumulative local revision `r3`: generic words remain lower-case inside a compound name, while the first word and proper cultural titles such as `Maîtres` retain capitals.
- Split initial and post-ordinal settlement symbols so the same grammar produces both `Refuge des affranchis` and `Premier refuge des Maîtres déchus` naturally.
- Remove the shared `fixedName` from the Free Jaffa faction and generate newly created factions from `216` bilingual collective names such as `Alliance des clans libres` or `Conseil de la résistance Jaffa`.
- Preserve the generic `Free Jaffa` / `Jaffa libres` label in world-creation controls while allowing each generated faction instance to have its own world name.
- Apply the new names only to newly generated factions and settlements; existing save names remain serialized and unchanged.
- Record faction-specific world-map settlement icons as part of the deferred global visual pass; the current vanilla house silhouette and subtle faction-color variations remain temporary.
- Add no custom settlement layout, icon, texture, mission, incident, PawnKind, equipment or persistent save data.
- Validate final local revision `r3`, including varied faction and settlement names, natural French casing, bilingual RulePackDef loading, unchanged serialized-name behavior, Free Jaffa regressions and a clean `Player.log`.
- Publish branch `feature/free-jaffa-world-names`, final tag `v0.3.45-dev` and the synchronized wiki.

## 0.3.44-dev - Add Free Jaffa military aid

- Enable the ordinary RimWorld military-aid request for allied Free Jaffa factions.
- Reuse the powered communications-console dialogue, allied-relation requirement, goodwill cost, cooldown, arrival and departure behavior.
- Reuse the existing Free Jaffa `Combat` pawn group made of `SG1_FreeJaffaWarrior` and `SG1_FreeJaffaGuard`.
- Preserve Jaffa xenotype, automatic Prim'ta, Free Jaffa names and backstories, Ma'Tok equipment, modular armor and the absence of imposed Goa'uld forehead marks.
- Keep quest sites, natural raids, sieges and staged attacks disabled.
- Add no custom incident, mission, currency, PawnKind, persistent save data or texture.
- Validate final local revision `r1`, including clean startup, neutral/hostile/allied access rules, goodwill cost, repeated-request restrictions, coherent Free Jaffa reinforcements, combat, departure, save/reload and a clean `Player.log`.
- Publish branch `feature/free-jaffa-military-aid`, final tag `v0.3.44-dev` and the synchronized wiki.

## 0.3.43-dev - Add Free Jaffa trade network

- Enable ordinary Free Jaffa trade through vanilla caravan, visitor, settlement and comms-console workflows.
- Reuse `Caravan_Outlander_BulkGoods`, `Visitor_Outlander_Standard` and `Base_Outlander_Standard` instead of introducing a parallel trade economy.
- Add `SG1_FreeJaffaTrader`, a dedicated culturally generated Jaffa caravan contact with a Prim'ta, Ma'Tok staff, light Jaffa armor and no imposed Goa'uld forehead mark.
- Add a Free Jaffa `Trader` pawn-group profile with dedicated trader, Jaffa guards and vanilla pack animals.
- Allow trader requests while keeping military aid, quest sites, natural raids, sieges and staged attacks disabled.
- Exclude combat-supplier and exotic-goods trader kinds from this first balancing pass.
- Preserve all existing faction, pawn, item and save-data identifiers.
- Detect on first launch that `Caravan_Outlander_General` is not a RimWorld 1.6 `TraderKindDef`, remove the invalid reference and keep general commerce through visitor and settlement trade.
- Prepare cumulative local revision `r2` after removing the invalid caravan trader kind.
- Mark `SG1_FreeJaffaTrader` with the vanilla PawnKind trader flag after the forced caravan test reported that the pawn kind was present in the traders list but was not recognized as a trader.
- Prepare cumulative local revision `r3` for functional validation on branch `feature/free-jaffa-trade-network`.
- Validate local revision `r3`, including settlement trade, forced caravan generation, trader recognition and a functional trade window.
- Replace the generic bulk-goods caravan with `SG1_Caravan_FreeJaffaClanSupplies` in cumulative local revision `r4`.
- Specialize the convoy around field provisions, medicine, strategic materials, human industrial weapons, military armor and limited Jaffa equipment.
- Allow the convoy to buy human military equipment while limiting each visit through an `850` to `1300` silver reserve.
- Keep visitors and settlements on their vanilla profiles and defer missing economic categories to future trade-oriented factions such as the Nox.
- Prepare cumulative local revision `r4` for stock-generation, economy and regression validation.
- Detect on the first `r4` launch that the five public Jaffa armor pieces were still `Sellable` only, which allowed the player to sell them but prevented a trader from generating them in stock.
- Change light armor, heavy armor, gauntlets, reinforced boots and the deployed helmet to `tradeability=All` while keeping the internal retracted helmet non-tradeable.
- Prepare cumulative local revision `r5` for startup, stock-generation and trade validation.
- Validate final local revision `r5`, including clean startup, specialized stock generation, military-equipment purchasing, finite merchant budget, trader identity, settlement trade, save/reload and a clean `Player.log`.
- Publish branch `feature/free-jaffa-trade-network`, final tag `v0.3.43-dev` and the synchronized wiki.

## 0.3.42-dev - Separate advanced diagnostics from developer actions

- Add `GR_Debug.DeveloperActionsEnabled` as the shared developer-only gate for commands that force or bypass gameplay state.
- Keep the existing advanced GateRim SG-1 option for read-only inspection details, reports, settings diagnostics and lifecycle traces.
- Hide the Tok'ra communicator's organic-operation force/progress/reset menu unless RimWorld developer mode is active.
- Add a defensive permission check inside the communicator debug-menu method.
- Restrict the decoded Goa'uld relay's duplicate direct launch gizmo to developer mode while preserving the normal caravan right-click flow.
- Route forced implantation, autonomous-hunt control, instant emergency extraction and unrestricted queen testing through the shared developer-action rule without changing legitimate player conditions.
- Clarify the English/French settings text and update the debug, mission-site and player-wiki documentation.
- Validate local revision `r1`, including the build, consistency checks, three-mode visibility matrix, ordinary player interactions, save/reload and `Player.log`.
- Hide trusted-tier communicator requests from the pawn right-click menu until Tok'ra trust actually reaches the trusted tier, preventing future support options from being spoiled.
- Preserve visible disabled reasons for pawn capability, reachability, reservation, power, cooldown, threat and patient conditions after the trusted tier is unlocked.
- Keep channel status and any currently active organic-operation interaction visible below the trusted tier.
- Validate final local revision `r2`, including progressive discovery below and at the trusted tier, contextual disabled reasons after unlock, trust transitions, save/reload and a clean `Player.log`.
- Publish branch `feature/debug-command-visibility-audit`, final tag `v0.3.42-dev` and the synchronized wiki.

## 0.3.41-dev - Add active Goa'uld host extraction surgery

- Add `SG1_ExtractActiveGoauldSymbiote`, a difficult medical operation for an established Goa'uld host.
- Require the patient to be player-controlled or held as a colony prisoner before the operation is available.
- Release captured active hosts from the takeover assault and prevent periodic reassignment while they remain colony prisoners.
- Exclude active Tok'ra symbiosis from the Goa'uld extraction recipe.
- Require Medicine `10`, three medicine units, `4200` work, a `0.75` surgery factor and a `5%` death chance on failed surgery.
- Add a shared transaction-safe surgery base used by recent and active Goa'uld-family extraction.
- Preserve the same persistent symbiote ID, name, origin, host history and faction allegiance after removal.
- Restore the displaced host faction and remove the dedicated takeover assault after successful extraction.
- Preserve the existing pawn, body, equipment, relationships, xenotype, injuries and backstories instead of generating a replacement host.
- Return the extracted active Goa'uld as a live free pawn under temporary vanilla anesthesia.
- Leave the active host state and persistent symbiote untouched after an ordinary surgery failure.
- Keep the existing emergency surgery for recent implantation unchanged in scope.
- Restrict the deterministic immediate-extraction gizmo strictly to RimWorld developer mode; advanced diagnostics no longer grant an instant gameplay action.
- Hide direct forced implantation and autonomous-hunt controls on free symbiotes outside developer mode.
- Expose ritual or voluntary implantation only when the symbiote is genuinely player-controlled or is part of an explicit Tok'ra offer.
- Display a hostile active host under the symbiote's name while control is active, preserve the original host name in persistent data and restore it after successful extraction or recovery.
- Add English/French operation text, targeted validation and updated technical/player documentation.
- Validate final local revision `r2`, including interface restrictions, persistent host/symbiote names, save/reload, extraction restoration and the hostile symbiote wake-up boundary.
- Publish branch `feature/goauld-active-host-extraction-surgery`, final tag `v0.3.41-dev` and the synchronized wiki.

## 0.3.40-dev - Add hostile Goa'uld host takeover

- Persist the free symbiote's faction allegiance and the host faction displaced by implantation.
- Arm hostile takeover only for Goa'uld-origin symbiotes belonging to a non-player faction hostile to the player.
- Transfer the host into the symbiote faction when recent implantation becomes an active Goa'uld host.
- Preserve the same pawn, body, relationships, xenotype and persistent symbiote identity through the faction transition.
- Send a bilingual RP threat letter and remove the converted pawn from player-control UI immediately.
- Preserve pending and active takeover state through save and reload.
- Restore or preserve the displaced host faction when emergency extraction or supported state removal releases the symbiote.
- Return an extracted free symbiote to its recorded faction allegiance.
- Preserve Tok'ra implantation and player-controlled or factionless Goa'uld symbiote behavior.
- Add compact developer actions for inspection, forced conversion and recovery testing.
- Assign incident-spawned hostile free symbiotes to a no-retreat vanilla assault Lord so their implantation pursuit is no longer interrupted by animal flight behavior.
- Assign converted hostile hosts to a dedicated persisted colony assault instead of allowing the former colonist AI to seek an immediate map exit.
- Replace the no-retreat `r3` Lord with a raid-like takeover assault that keeps vanilla timeout and retreat enabled, allowing the host to leave an exhausted or abandoned map.
- Recheck the active host assault every `30` ticks and automatically migrate legacy `r3` development saves to the retreat-capable Lord.
- Remove either dedicated takeover-assault generation before restoring the displaced host faction during recovery or extraction.
- Validate final local revision `r4`: free-symbiote pursuit, pending-state persistence, hostile faction transfer, real attacks against pawns and property, save/reload stability, `r3` migration, eventual raid withdrawal and protected Tok'ra/player paths.
- Publish branch `feature/goauld-hostile-host-takeover`, final tag `v0.3.40-dev` and the synchronized wiki.

## 0.3.39-dev - Add Goa'uld free-symbiote incursion incident

- Add the first recurrent autonomous Goa'uld biological-hazard incident.
- Spawn the existing free Goa'uld symbiote pawn from a reachable hostile map edge.
- Scale the group from one to four symbiotes using storyteller threat points, with a deliberately low cap.
- Require the visible Goa'uld System Lord faction and at least one compatible player colonist.
- Reuse the established autonomous pursuit, persistent identity transfer and recent-implantation systems without a parallel infection path.
- Add three English/French RP warning variants with persistent immediate-repeat prevention.
- Add a compact Goa'uld developer submenu with current, weak-colony and advanced-colony scaling tests.
- Preserve Tok'ra operations and all existing host, extraction and active-symbiote behavior.
- Validate final local revision `r1`, including weak and advanced scaling, autonomous pursuit, persistent identity transfer, non-repeating warning text after save/reload and regressions of existing Goa'uld/Tok'ra systems.
- Confirm the Windows build of `GateRimSG1.dll` version `0.3.39.0`; the initial missing-worker load error was caused only by the stale `0.3.38.0` assembly.
- Publish branch `feature/goauld-free-symbiote-incursion`, final tag `v0.3.39-dev` and the synchronized wiki.

## 0.3.38-dev - Audit Tok'ra organic operation pool and long-term variety

- Derive required recurrent-operation coverage from the persisted archetype enum instead of a hardcoded minimum.
- Detect missing, duplicated or non-MissionDef-backed operation definitions in the developer audit.
- Validate trust-tier weights, hidden-delay ranges, local repeat factors and narrative-bank coverage for all eight operations.
- Compare deterministic penalized selection against a no-penalty baseline for every trust tier.
- Add two English/French RP offer variants to intelligence recovery, bringing all current recurrent operations to multiple offer narratives.
- Preserve scheduling weights, delays, rewards, active-slot behavior and save data.
- Confirm the Windows build of `GateRimSG1.dll` version `0.3.38.0` succeeds.
- Correct the current-test milestone and DLL metadata labels in local revision `r2` so `check-project-consistency.cmd` can detect them; no C#, XML, gameplay or save-data change is introduced.
- Validate final local revision `r2`: eight persisted archetypes, exact definition coverage, MissionDef ownership, all four trust-tier simulations, measurable anti-repetition, three non-repeating intelligence offers and save/reload stability.
- Publish branch `feature/tokra-operation-pool-audit`, final tag `v0.3.38-dev` and the synchronized wiki.

## 0.3.37-dev - Add Tok'ra Jaffa officer capture operation

- Add an eighth recurrent Tok'ra organic-operation archetype without renumbering existing persisted values.
- Add a temporary hostile world site with one living silver-marked Jaffa officer and a threat-scaled Goa'uld/Jaffa escort.
- Deliver one sealed twelve-charge Tok'ra hypodermic rifle through the preferred Tok'ra delivery point.
- Preserve ordinary RimWorld neutralization, caravan reformation and prisoner transport without requiring a prison bed on the hostile map.
- Apply a mission-only physical restraint during carrying and caravan travel, then return control to vanilla prisoner handling on a player colony map.
- Remove the hostile map and world marker after the officer is confirmed in a player caravan or home map and no player pawn remains on the site.
- Persist the officer and all home-extraction state inside the organic-operation instance rather than the disposable field-site WorldObject.
- Add a powered-communicator action that schedules a visible two-to-three-agent Tok'ra extraction team after a hidden four-to-twelve-hour delay.
- Have one Tok'ra agent physically carry the prisoner off-map while the remaining agents withdraw.
- Resolve success only after the prisoner and every extraction-team member have left the map.
- Preserve save compatibility for active development saves by migrating the earlier site-owned transfer state.
- Add complete English/French text, developer diagnostics, durable tests, technical documentation and player-wiki coverage.
- Validate final local revision `r6`, including capture, transport, detention, site cleanup, communicator call, visible pickup and single success after full departure.
- Publish branch `feature/tokra-jaffa-officer-capture-operation`, final tag `v0.3.37-dev` and the synchronized wiki.

## 0.3.36-dev - Add non-lethal capture tools

- Add craftable single-use bolas with a distinct physical-leg-restraint health effect.
- Add an experimental Tok'ra hypodermic rifle with five sealed charges.
- Consume one charge per launch, including misses and resisted hits, and destroy the rifle after the final shot.
- Show remaining charges in the selected-ground inspect pane and in hover tooltips while equipped or carried in inventory.
- Keep separate weapon-accuracy and neutralization-resistance rolls, with body-size and armor modifiers configured through XML.
- Keep light real blunt damage at `3` for bolas and `1` for the dart.
- Apply temporary effects that leave Consciousness intact and set Moving to zero, allowing ordinary RimWorld downing and vanilla capture.
- Refresh one existing Hediff instead of stacking multiple copies and preserve the remaining duration through save/reload.
- Reject the Consciousness-zero prototype after live hostile deaths.
- Reject the persistent-stun/custom-restraint prototype after live capture-flow failure.
- Remove the obsolete custom restraint JobDef, float-menu patch, ThingComp, stun HediffComp and JobDriver while preserving all four provisional textures.
- Validate final local revision `r9`, including distinct bolas wording, rifle operation, Moving-zero neutralization, vanilla capture, charge visibility, persistence and destruction after the last charge.
- Publish branch `feature/non-lethal-capture-tools`, final tag `v0.3.36-dev` and the synchronized wiki.

## 0.3.35-dev - Reorganize GateRim SG-1 debug actions into logical submenus

- Replace seventy separate GateRim SG-1 debug-action entries with four ordered entries directly inside the existing `GateRim SG-1` category.
- Avoid an additional `GateRim SG-1...` wrapper that would add a redundant click.
- Group Tok'ra tools into communicator, introduction, module-study, organic-operation and safehouse/intelligence branches.
- Give the shared organic-operation framework and each of the seven recurrent archetypes a dedicated submenu.
- Order actions by practical test flow: report, scheduling or offer, progression, outcome, then reset.
- Group Jaffa forehead-mark pawn tools and cultural diagnostics in their own category-level submenus, with MissionDef inspection as a direct category action.
- Preserve all existing debug method implementations while removing their individual menu attributes.
- Keep the complete menu restricted to developer mode on an active map.
- Update durable technical documentation and exact developer-menu paths.
- Raise assembly version to `0.3.35.0` and mod metadata version to `0.3.35-dev`.
- Validate final local revision `r2`, including direct category entries, logical submenu order, representative reports, Jaffa pawn targeting, save/reload and the absence of duplicate legacy actions.
- Publish branch `feature/debug-action-menu-reorganization`, final tag `v0.3.35-dev` and the synchronized wiki.

## 0.3.34-dev - Add Tok'ra diversion assault operation

- Add `SG1_TokraOrganic_DecoyTransmissionDefense` as the seventh recurrent MissionDef-backed Tok'ra operation while retaining its stable internal r1 identifier for save compatibility.
- Replace the rejected physical-transmitter prototype with a false signal emitted through the secure communicator; no mission object is delivered or placed.
- Queue one dedicated Goa'uld/Jaffa assault after a hidden XML-configured delay.
- Use a mission-only immediate breaching strategy so the attackers force paths through fortifications instead of waiting outside a sealed colony.
- Add the mission-only pawn-group kind `SG1_TokraDiversionAssault` and select it explicitly instead of passing the normal Goa'uld `Combat` group to the breaching strategy.
- Add `SG1_GoauldJaffaBreacher`, a Ma'Tok-equipped good breacher and sapper isolated from ordinary Goa'uld raids.
- Validate the custom group and required breacher at configuration time so an invalid future setup disables the operation instead of producing the vanilla `99999`-point fallback.
- Allow kidnapping and stealing, disable ordinary raid-timeout withdrawal and resolve victory when the registered force is dead, downed or retreating empty-handed.
- Resolve failure when a registered attacker reaches the map edge with a player pawn or stolen item, or when the player map is lost.
- Capture threat points when the offer is created, scale them by `0.75` and clamp the assault between `180` and `3000` points.
- Persist the assault due tick, triggered state, exact raider ThingIDs and extraction diagnostics in the generic mission runtime without new game-component fields.
- Add three offer variants, three success variants and complete English/French runtime text.
- Add targeted debug actions and expose assault state, registered raiders, registered breachers, active raiders and extracted cargo in the shared framework-state report.
- Remove the obsolete r1 transmitter ThingDef, its French DefInjected translation, its specialized utility and the superseded technical/wiki pages. No texture file requires deletion.
- Raise the orchestration audit requirement to seven resolved definitions.
- Keep assembly version `0.3.34.0` and mod metadata version `0.3.34-dev`.
- Load local revision `r2`, identify the failed raid-generation contract caused by the absence of an `isGoodBreacher` pawn in the selected group, and preserve the active operation for retry.
- Validate final local revision `r3`, including the dedicated breach group, Ma'Tok-equipped sapper, sealed-colony wall attack, save/reload anti-duplication, combat victory and a clean final `Player.log`.
- Publish branch `feature/tokra-decoy-transmission-defense`, final tag `v0.3.34-dev` and the synchronized wiki.

## 0.3.33-dev - Gate Tok'ra operations behind the communicator

- Require `SG1_TokraSecureCommunications` instead of vanilla `MicroelectronicsBasics` to construct the Tok'ra secure communicator.
- Keep already-built communicators compatible with older saves; research completion gates new construction only.
- Add a shared communicator-availability service for player home maps, ownership, GateRim configuration and active power.
- Add `Tok'ra communicator: show availability` for technical diagnosis.
- Gate every new recurrent Tok'ra offer behind at least one available powered communicator.
- Keep the unique introduction mission independent from the communicator.
- Preserve offered, accepted, ready and active operations when power or the building is lost.
- Persist the blocked planner state through save/reload.
- Replace an overdue offer check with a fresh hidden recurrence delay after the channel returns, preventing an immediate guaranteed offer.
- Route natural selection, specific force-offer actions and map selection through the shared availability service.
- Add `Tok'ra ops: make natural offer due` and extend framework diagnostics with gate and channel state.
- Raise assembly version to `0.3.33.0` and mod metadata version to `0.3.33-dev`.

## 0.3.32-dev - Add Tok'ra cipher-module study and research

- Start the milestone from final tag `v0.3.31-dev` on `feature/tokra-artifact-study-research`.
- Add a three-session analysis interaction to the exact cipher module recovered by the introduction mission.
- Reuse RimWorld Biotech's analyzable-item, research-bench hauling and persistent `AnalysisManager` systems.
- Reject separately spawned copies and require the genuine module to be on a player home map.
- Keep the physical module non-tradeable, preserve it through the first two sessions and dismantle it during the final analysis pass.
- Add `SG1_TokraSecureCommunications` to the GateRim SG-1 research tab.
- Require both completed module analysis and vanilla `Electricity` before formal research can start.
- Add bilingual inspection, progress letters, completion text and developer diagnostics.
- Replace a lost tracked module after a hidden `2–8` day delay without resetting analysis progress.
- Reconcile custom starters, edited saves and developer actions where secure-communications research is already complete, closing the introduction arc without generating an obsolete object.
- Make the introduction recovery debug action complete the arc and provide the genuine module whenever analysis is still required.
- Keep communicator construction and recurrent-operation gating for later milestones.
- Raise assembly version to `0.3.32.0` and mod metadata version to `0.3.32-dev`.
- Validate local revision `r1` for the normal study flow, exact-object identity, persistence and research lock.
- Validate local revision `r2` for final dismantling, non-tradeability, automatic delayed replacement, preserved progress and advanced-state reconciliation.
- Publish branch `feature/tokra-artifact-study-research`, final tag `v0.3.32-dev` and the synchronized wiki.

## 0.3.31-dev - Add Tok'ra introduction artifact mission

- Start the milestone from final tag `v0.3.30-dev` on `feature/tokra-introduction-artifact-mission`.
- Add a standalone pre-communicator introduction arc outside the six recurrent Tok'ra operations.
- Persist waiting, offered, active, retry and permanently completed states.
- Open the first encrypted opportunity after a hidden `4–12` day delay.
- Present a persistent choice letter with explicit accept and decline actions.
- Return declined, ignored or expired offers after a hidden `10–60` day delay.
- Create a hostile world site only after acceptance and preserve RimWorld caravan travel, combat, loot selection and reformation.
- Capture the threat snapshot at offer time and use a deliberately moderate profile: factor `0.35`, `180–650` scaled points and `2–6` defenders.
- Place one exact tracked Tok'ra cipher module and complete the arc only when that object reaches a player caravan, pawn inventory or home map.
- Reject separately spawned copies of the same ThingDef as mission completion objectives.
- Add a configurable one-day final warning before the six-day site deadline and persist its anti-duplication state.
- Fail and reschedule accepted attempts after artifact destruction, site timeout or unexpected site loss, using a hidden `7–45` day retry.
- Recalculate a new hidden delay after each failed attempt and close the arc permanently only after successful recovery.
- Preserve the offer, site, combat map, exact artifact identity, warning, retry timing and result through save/reload.
- Add dedicated developer actions for every state transition and real failure path while keeping technical details outside normal player interfaces.
- Add English and French RP variants, a dedicated player-wiki page and durable validation coverage.
- Keep artifact study, dedicated research, communicator prerequisites and recurrent-operation gating for later milestones.
- Retain assembly version `0.3.31.0` and mod metadata version `0.3.31-dev`.
- Validate final local revision `r3`, including warning persistence, real timeout, real artifact loss, repeated retry eligibility, physical recovery, vanilla loot/reformation and permanent completion.
- Publish branch `feature/tokra-introduction-artifact-mission`, final tag `v0.3.31-dev` and the synchronized wiki.

## 0.3.30-dev - Add Tok'ra temporary-base delivery mission

- Add `SG1_TokraOrganic_TemporaryBaseDelivery` as the sixth recurrent MissionDef-backed Tok'ra organic operation.
- Generate only contracts the colony can reasonably manufacture from available recipes, completed research, required content, worktables and capable colonists.
- Persist the selected product, quantity, minimum quality, minimum condition, deadlines, offer-time threat snapshot and complication state.
- Create a concealed temporary Tok'ra rendezvous `6–16` tiles away and preserve normal RimWorld caravan routing.
- Expose `Hand over the requested goods` / `Remettre la commande` on a stationary caravan at the rendezvous, report the exact missing amount and consume only the requested conforming quantity.
- Preserve unrelated cargo, excess conforming goods and any battlefield loot carried by the caravan.
- Add a configurable two-day late-delivery grace period: on-time delivery grants `+2` trust, late delivery grants `+1`, and final expiry applies `-1`.
- Add one optional Goa'uld interception during the journey for a complete shipment travelling to the exact site.
- Add a mutually exclusive Goa'uld ambush on the last approach tile when the caravan's next path tile is the rendezvous.
- Reuse RimWorld's temporary caravan-ambush map, real inventory transfer and complete vanilla reformation dialog so surviving cargo, enemy equipment and other recoverable map items remain selectable.
- Return the reformed caravan to the adjacent tile and allow the final one-tile journey without an artificial detour.
- Keep military victory separate from mission success: trust changes only after the physical cargo handoff or final expiry.
- Scale both hostile complications from the threat snapshot captured at offer time with XML-configured factors and bounds.
- Track queued ambush-map creation asynchronously, preventing premature route clearance and the false unidentified-world-object error.
- Persist all trigger, retry, encounter, cargo, deadline and anti-duplication state through save/reload without retroactive complication rerolls.
- Add developer actions for forcing the contract, late window, ordinary interception and final-approach ambush while keeping technical state outside normal player interfaces.
- Add and align English/French offer, status, warning, combat and result texts.
- Validate final local revision `r9`, including a naturally selected interception, complete battlefield-loot recovery, vanilla reformation, final-tile handoff, persistence, recurrence, anti-repetition, previous-operation regressions and a clean `Player.log`.
- Publish branch `feature/tokra-temporary-base-delivery`, final tag `v0.3.30-dev` and the synchronized wiki.

## 0.3.29-dev - Add Tok'ra distress call world-site mission

- Add `SG1_TokraOrganic_DistressCall` as the fifth MissionDef-backed recurrent Tok'ra operation.
- Create a temporary world site after acceptance, targetable through RimWorld's normal caravan-arrival flow and persistent through save/load.
- Keep the situation hidden until map entry, with genuine rescue, compromised signal and late-arrival outcomes.
- Allow a genuine rescue to degrade into late arrival when the expedition takes too long, without changing a preselected trap.
- Capture the threat snapshot at offer time and scale Goa'uld/Jaffa defenders from configurable bounds and per-variant factors.
- Generate one coherent encounter scene around a shared anchor, with context-specific camp or caravan remains, defenders, survivors, debris and optional Tok'ra/Jaffa corpses.
- Use vanilla automatic map loading, hostile-map pause, drafted caravan entry and a nearby valid edge instead of a parallel custom arrival flow.
- Require real field treatment of the symbiote shock without forcing a medical bed, heating, full healing or a walking departure.
- Add a visible Tok'ra recovery team using vanilla edge arrival, non-hostile carrying and normal map exits; count evacuation only after survivors physically leave the map.
- Place the late-arrival component reward on a configured vanilla storage shelf when the encounter is generated, with no outcome-time material spawn or save/load duplication.
- Add deadlines, cleanup, trust consequences, Medicine XP, recurrence and local anti-repetition through the shared organic-operation manager.
- Add developer actions for each hidden situation and extend framework diagnostics to five organic definitions.
- Force non-incremental local builds so extracted source timestamps cannot leave an older DLL active.
- Add English and French player texts, durable validation coverage and synchronized player documentation.
- Raise the assembly version to `0.3.29.0` and the mod metadata version to `0.3.29-dev`.
- Validate local revision `r7`, including all three variants, adaptive difficulty, expiration, persistence during recovery, recurrence, previous-operation regressions, the physical shelf reward and a clean `Player.log`.
- Publish branch `feature/tokra-distress-call-world-site`, final tag `v0.3.29-dev` and the synchronized wiki.

## 0.3.28-dev - Audit Tok'ra operation orchestration and long-term recurrence

- Filter positively weighted MissionDefs through their mission worker `CanOffer(map)` before the natural weighted draw.
- Prevent a temporarily unavailable mission from suppressing another eligible operation.
- Retry after the normal internal state-check interval when configured candidates exist but all are temporarily unavailable, instead of consuming a full hidden recurrence delay.
- Preserve the stable no-weight state and all existing MissionDef weights, delays, rewards and trust consequences.
- Add `Tok'ra ops: roll next natural offer` to exercise the real scheduler without forcing an archetype.
- Add `Tok'ra ops: audit long-term orchestration` with global-slot state, outcome counters, current offerability, trust-tier configuration and deterministic `5000`-draw simulations.
- Record the locked sequence for the Tok'ra distress-call world site and temporary-base delivery missions.
- Record the long-term mission-pool strategy: progressive variety, post-`1.0.0` rebalancing where useful, Tok'ra first, then separate Goa'uld and other-faction pools.
- Record a deferred Tok'ra introduction arc: unique combat mission, key artifact, dedicated research requiring Electricity, communicator construction, then access to recurrent operations.
- Raise the assembly version to `0.3.28.0` and the mod metadata version to `0.3.28-dev`.
- Validate local revision `r1`, including deterministic audit, natural rolls, success/failure/ignored recurrence, persistence, all four operation regressions, storyteller compatibility and a clean `Player.log`.
- Publish branch `feature/tokra-operation-orchestration-audit`, final tag `v0.3.28-dev` and the synchronized wiki.

## 0.3.27-dev - Migrate medical handoff to mission framework

- Add `SG1_TokraOrganic_MedicalSupplyHandoff` as the fourth complete MissionDef-backed organic operation.
- Add a bounded reusable `handoff` profile for liaison PawnKind, arrival timing, departure grace and post-handoff death trust change.
- Reuse the generic `DeliverThing` objective for the requested ThingDef, count, dialogue JobDef and Social skill.
- Move offer timing, handoff deadline, recurrence weights and trust-tier delay ranges into XML.
- Move all medical-handoff actions, statuses, dialogue keys, failure texts, trust messages and post-handoff consequence texts into XML.
- Add three offer variants and three success variants with local anti-repetition.
- Replace hardcoded industrial-medicine and liaison references in C# with validated MissionDef data.
- Replace the fixed medical-supply trust constants with configured success, failure and post-handoff consequences.
- Remove the complete legacy C# medical-handoff definition and disable the archetype explicitly when required configuration is incomplete or invalid.
- Keep entry-cell selection, meeting geometry, RimWorld Lord behavior, reservations, dialogue execution, stack consumption and save references in the specialized adapter.
- Raise the assembly version to `0.3.27.0` and the mod metadata version to `0.3.27-dev`.
- Validate local revision `r1`, including delayed arrival, exact resource consumption, interaction restrictions, failures, post-handoff death consequence, persistence, RP variants, recurrence, regressions and a clean `Player.log`.
- Publish branch `feature/medical-handoff-mission-migration`, final tag `v0.3.27-dev` and the synchronized wiki.

## 0.3.26-dev - Migrate wounded-agent care to mission framework

- Restore the intelligence accelerated-analysis `<workTicks>5000</workTicks>` entry in local revision `r2`; its omission from the `r1` archive was correctly rejected by the MissionDef validator at load time.
- Keep all C# and wounded-agent gameplay unchanged from the functionally validated `r1` revision.
- Add `SG1_TokraOrganic_WoundedAgentCare` as the third complete MissionDef-backed organic operation.
- Add a bounded reusable `pawnCare` profile for pawn kind, health Hediffs, stable duration, departure grace and medical thresholds.
- Drive optional illness chance and severity from the scaled threat snapshot captured when the offer is created.
- Move wounded-agent weights, context delays, text variants, runtime messages and trust consequences into XML.
- Add three offer variants and three success variants with local anti-repetition.
- Remove the complete C# fallback definition and reject missing, incomplete or invalid required configuration explicitly.
- Keep pawn spawning, vanilla tending, health evaluation, Lord behavior, departure and save references in the specialized adapter.
- Raise the assembly version to `0.3.26.0` and the mod metadata version to `0.3.26-dev`.

- Validate the complete wounded-agent flow on local revision `r1`, including rescue, real shock tending, medical recovery, stable duration, departure, failures, persistence, recurrence, RP variants and regressions.
- Validate adaptive illness parameters on weak and advanced colonies from the threat snapshot captured at offer time.
- Validate local revision `r2` after a full restart: three MissionDefs load, accelerated intelligence analysis uses `5000` ticks again and the final `Player.log` is clean.
- Publish branch `feature/wounded-agent-mission-migration`, final tag `v0.3.26-dev` and the synchronized wiki.

## 0.3.25-dev - Migrate intelligence recovery to mission framework

- Add `SG1_TokraOrganic_IntelligenceRecovery` as the second complete MissionDef-backed organic operation.
- Add context-specific hidden recurrence delay ranges and make intelligence recovery consume its trust-tier range.
- Add named weighted text banks for cautious, accelerated and interference results with independent immediate anti-repetition.
- Add configurable consequence chance, delay range and retry fields for real mission side effects.
- Move the intelligence module, analysis job, skill, cautious and accelerated work durations, XP rewards, trust changes, actions, statuses and player texts into XML.
- Rebalance the XML analysis durations after `r1` validation from `5000` to `10000` ticks for cautious analysis and from `2000` to `5000` ticks for accelerated analysis, preserving the validated reward and consequence behavior.
- Move the accelerated interference chance, Goa'uld patrol IncidentDef and queue timing into XML.
- Scale the interference patrol from the threat snapshot captured when the offer is created, using the configured `0.35` factor and `180–700` point bounds.
- Preserve existing active-work progress and compatibility fields when loading operations created before this migration.
- Remove the complete legacy C# intelligence-recovery definition and disable the archetype explicitly when required configuration is incomplete or invalid.
- Extend the developer report with named text banks, context delay ranges and consequence parameters.
- Keep physical-object placement, communicator interactions, RimWorld jobs, Toils, persistence and incident queuing in the specialized adapter.
- Raise the assembly version to `0.3.25.0` and the mod metadata version to `0.3.25-dev`.
- Validate local revision `r2`, including final `10000 / 5000` analysis pacing, progress persistence, threat consumption on weak and advanced colonies, captured-budget stability, mission regressions and a clean `Player.log`.

## 0.3.24-dev - Complete observation mission Def migration

- Extend mission objectives with secondary targets, job Defs, configured skills, active XP rates and secondary work durations.
- Move the Tok'ra observation device, site marker, deployment job and transmission job references into `SG1_TokraOrganic_GoauldObservation`.
- Move deployment, observation, recovery and transmission work durations into the phase objectives.
- Move all observation actions, runtime messages, status texts and success-result variants into the MissionDef.
- Use the configured skill and XP-per-tick values while an operator records the site.
- Replace the observation-specific final Intellectual reward path with a generic configured skill XP reward (`Intellectual +250`).
- Use the MissionDef recurrence range after an observation resolves.
- Remove the complete legacy C# observation fallback; disable the archetype with an explicit error when required data is missing, duplicated or references an unknown `ThingDef`, `JobDef` or `SkillDef`.
- Expand the developer definition report with recurrence, text-bank and objective configuration details.
- Preserve the specialized RimWorld hauling, reservations, pathfinding, Toils, persistence and old-save migration adapter.
- Raise the assembly version to `0.3.24.0` and the mod metadata version to `0.3.24-dev`.
- Validate the complete migration on local revision `r1`, including configured Def references, all four work durations, active and final skill XP, success variants, failure paths, save/load compatibility, XML recurrence, legacy-operation regressions and a clean `Player.log`.

## 0.3.23-dev - Introduce reusable mission framework foundations

- Add a Def-driven mission and questline toolbox for common timing, recurrence, difficulty, RP texts, rewards, phases and objectives.
- Add persistent generic runtime data that can coexist with specialized legacy mission fields.
- Add weighted RP offer variants with local anti-repetition across occurrences.
- Add RimWorld threat-point snapshots and configurable per-mission repeat penalties.
- Add a developer report for loaded mission definitions and current threat snapshots.
- Migrate the recurring Tok'ra Goa'uld-observation operation as the first XML-backed pilot while preserving its existing player flow and C# fallback.
- Read the pilot's active observation duration from its XML objective and rebalance it to `10000` ticks, or four in-game hours.
- Keep intelligence recovery, wounded-agent care and medical handoff on their existing implementation during the gradual migration.
- Record the global visual overhaul as a later concept-art-driven pass rather than a sequence of isolated texture milestones.
- Raise the assembly version to `0.3.23.0` and the mod metadata version to `0.3.23-dev`.
- Validate the complete framework pilot on local revision `r2`, including the four-hour observation duration, three RP variants, immediate anti-repetition, save/load persistence, differentiated threat snapshots, legacy-operation regressions and a clean `Player.log`.

## 0.3.22-dev - Add an SG-team field cap

- Add a lightweight black SG-team field cap with dedicated ground and four-direction worn graphics.
- Add English Def text and French translations while correcting the field helmet's obsolete scenario description.
- Add the cap to the existing optional cultural headgear slot beside the field helmet with equal relative weight.
- Preserve the slot's `0.6` selection chance, producing helmet, cap or no-headgear outcomes without scenario-specific C#.
- Keep the cap deliberately lighter and far less protective than the field helmet.
- Update the SG-team scenario, starter-loadout documentation, wiki equipment page and public metadata to `0.3.22-dev`.
- Validate loading, all three headgear outcomes, four-direction rendering, mutual exclusivity, save/reload persistence, vanilla-scenario isolation and a clean `Player.log` on local revision `r1`.

## 0.3.21-dev - Add cultural starter loadout rules

- Fix the final consistency check so it accepts both `Version de DLL attendue` and `Version de DLL validée`, while avoiding duplicate failures for a missing captured value.

- Extend `CulturalStarterRule` with Def-driven candidate restrictions and starter apparel.
- Keep legacy ordered apparel lists and add weighted slots with selection chances, weighted options, optional stuff and reusable shared variant groups.
- Move the stranded SG-team minimum age and violence-capability rules into the Tau'ri / SGC cultural profile.
- Replace the scenario-specific starting-gear implementation with a reusable hidden cultural marker.
- Split the SG field uniform into mandatory pants and an optional jacket, each with olive, black and desert variants, while linking both pieces to the same per-pawn color selection.
- Add a mandatory vanilla cloth T-shirt beneath the modular field uniform and correct its Def reference to `Apparel_BasicShirt` after the `r3` startup error.
- Move the tactical vest to the `Shell` layer so the T-shirt, jacket and vest can coexist.
- Equip the field helmet through an optional weighted headgear slot instead of supplying four loose helmets.
- Prepare the same headgear slot for a future SG-team cap without scenario-specific C#.
- Rebalance the vanilla human weapon set from three assault rifles and one pump shotgun to one assault rifle, one machine pistol, one autopistol and one pump shotgun.
- Preserve existing combined-uniform Defs for save compatibility while removing them from the stranded-team starter loadout.
- Replace backslash-based repository-relative command paths in Markdown with forward slashes and make the consistency checker reject literal tab characters in Markdown files.
- Validate local revision `r4`: consistency and Markdown-tab checks, rebuild `0.3.21.0`, SG-team candidate restrictions, mandatory and optional apparel layers, all three linked uniform variants, optional helmets, the mixed vanilla firearm set, vanilla-scenario isolation, save/reload stability and a clean `Player.log`.
- Publish the reusable starter-loadout framework while keeping future SG caps and uniform variants expressible through XML rather than scenario-specific C#.

## 0.3.20-dev - Add automated project consistency checks

- Add a read-only PowerShell consistency checker and Windows wrapper under `tools/`.
- Fix Windows PowerShell 5.1 parser compatibility by avoiding a colon immediately after an interpolated variable in the read-error message.
- Treat `About/About.xml` as the authoritative development version and compare it with the project, README, wiki and active tracking documents.
- Derive the required four-part assembly version from the `x.y.z-dev` milestone version.
- Count every cultural `BackstoryDef`, report missing or duplicate `defName` values and compare the real total with public and technical summaries.
- Compare the wiki backstory table-row count with the loaded Def count.
- Add optional explicit version and backstory-count expectations so failure handling can be tested without editing repository files.
- Correct the README, wiki home and content-status page from the stale `0.3.17-dev` / `70` state to `0.3.20-dev` / `83`.
- Require the consistency command in the milestone publication procedure before the final commit.
- Raise the assembly version to `0.3.20.0` and the mod metadata version to `0.3.20-dev` without changing gameplay code or Defs.
- Validate the complete checker on local revision `r2`, including the Windows PowerShell 5.1 parser fix, the positive path and an intentional negative expectation.
- Confirm non-zero failure handling, a successful positive rerun, a read-only working tree, the forced rebuild, main-menu loading and a clean `Player.log`.
- Publish the corrected public presentation with `83` backstories and make the checker a mandatory pre-commit publication gate.

## 0.3.19-dev - Audit cultural backstory skill coverage

- Audit the actual childhood-and-adulthood pools of every implemented culture against RimWorld's twelve standard skills.
- Record low redundancy without treating it as an automatic reason to add content.
- Confirm that Goa'uld-aligned and Free Jaffa already cover every skill and require no new entry.
- Add eleven targeted adult backstories for demonstrated gaps in the SGC, off-world-human, Goa'uld-host, System Lord and Tok'ra pools.
- Expand the catalogue from `72` to `83` entries.
- Extend explicit starter and generated-host lists through XML while keeping the generic C# cultural framework unchanged.
- Add a durable technical coverage matrix for current and future cultures.
- Update the French player-facing catalogue with every new title, description and skill bonus.
- Raise the assembly version to `0.3.19.0` and the mod metadata version to `0.3.19-dev`.
- Validate the complete matrix on local revision `r1`: all five modified cultural pools, cultural name groups, both generated-host origins, save/load stability, legacy-history preservation and Tok'ra personality switching.
- Confirm that ordinary-human starters remain vanilla-majority, Jaffa pools remain unchanged and `Player.log` is clean.
- Publish the final catalogue with `83` backstories without requiring any corrective C# or Def revision.

## 0.3.18-dev - Add a Tau'ri origin for generated Tok'ra hosts

- Add a minority `SG1_GeneratedHost_TauriSGCVolunteer` origin to the existing weighted generated-host framework.
- Keep the off-world-human origin dominant with relative weights `1` and `0.2`.
- Reuse the existing Tau'ri name generator and eight validated SGC adult careers.
- Add two dedicated modern-Earth childhoods for science-fair and military-family backgrounds.
- Restrict the new childhoods to their own spawn category so they do not leak into ordinary starter pools.
- Expose both origins to the Tok'ra cultural profile through XML only, without changing the generic C# resolver.
- Preserve stored generated identities, real implantation behavior, extraction and reimplantation boundaries.
- Expand the cultural-backstory catalogue from `70` to `72` entries and update the French wiki table.
- Raise the assembly version to `0.3.18.0` and the mod metadata version to `0.3.18-dev`.
- Validate weighted generation, identity switching, shared progression, save/load stability, legacy-identity preservation, real reimplantation boundaries, starter isolation and a clean `Player.log` on local revision `r1`.
- Record a future culture-by-culture audit of RimWorld skill coverage without expanding the `0.3.18-dev` functional scope.

## 0.3.17-dev - Refresh project and wiki presentation

- Replace the root README's obsolete `0.2.18-dev` milestone log with a durable project overview.
- Summarize the currently playable SGC, Jaffa, Goa'uld and Tok'ra systems without exposing internal milestone history as player documentation.
- Update the French wiki home to the current `0.3.17-dev` documentation baseline.
- Record the 70-backstory catalogue, configurable starter profiles and persistent Tok'ra dual-identity systems in the public presentation.
- Bring `Content-Status.md` forward from its `0.3.5-dev` revision and register the validated cultural and identity milestones through `0.3.16-dev`.
- Remove completed or misleading future entries, including generic backstory enrichment and Tok'ra extraction listed as unavailable.
- Replace the increasingly confusing flat wiki sidebar with stable thematic categories while preserving all existing navigation targets.
- Add the existing Tok'ra dual-identity and tactical-assessment pages that were missing from the former sidebar.
- Extend the milestone publication procedure so new or renamed wiki pages must be assigned to an appropriate sidebar category and checked before synchronization.
- Keep the functional Stargate, Asgard, Nox, Unas, dedicated storyteller and full GateRim SG-1 world preset explicitly separated as future work.
- Raise the assembly version to `0.3.17.0` and the mod metadata version to `0.3.17-dev` without changing gameplay code or Defs.
- Validate the rebuild, main-menu loading, public-page links, categorized sidebar, preserved navigation targets and a clean `Player.log` on local revision `r2`.

## 0.3.16-dev - Expand cultural backstory variety

- Add twelve culturally distinct native RimWorld backstories without changing the shared cultural resolver.
- Add two SGC adult careers for survival preparation and expedition logistics.
- Add two shared Jaffa childhoods rooted in naquadah mining and Chappa'ai settlement life.
- Add two Goa'uld-aligned Jaffa careers for Ha'tak boarding and tribute enforcement.
- Add two Free Jaffa careers for settlement diplomacy and captured-equipment maintenance.
- Add two ordinary Goa'uld-host careers for naquadah oversight and ship-system maintenance.
- Add two Tok'ra careers for covert engineering and safehouse coordination.
- Append the new Defs to the existing starter-profile and cultural-name rules through XML patches only.
- Preserve the additive vanilla-majority behavior of ordinary human starters and the exclusive SGC adulthood pool of the stranded SG-team scenario.
- Expand the player-facing wiki catalogue from `58` to `70` entries.
- Raise the assembly version to `0.3.16.0` and the mod metadata version to `0.3.16-dev`.
- Validate all twelve French entries, moderate skill bonuses, spawn categories and cultural-profile integration.
- Validate Jaffa, Goa'uld-host, ordinary-human and stranded-SG-team starters, normal world generation, save/load, Tok'ra switching and a clean `Player.log`.
- Confirm that the complete expansion requires no post-test C# or Def correction after local revision `r1`.

## 0.3.15-dev - Add unified cultural identity diagnostics

- Add one read-only technical report for the currently selected pawn.
- Display current race, xenotype, `PawnKindDef`, faction and active backstories.
- Display every matching cultural profile, the selected profile and the resolved cultural name group for normal and player-starter generation contexts.
- Display Jaffa physiology, Prim'ta state, intrinsic forehead mark and existing contextual social-identity flags.
- Display persistent Goa'uld or Tok'ra symbiote data when the pawn carries an adult host symbiote.
- Expose the same report through one grouped developer action and the existing advanced GateRim SG-1 settings section.
- Add English and French labels while keeping the report itself technical.
- Keep the diagnostic strictly read-only and hidden outside developer mode or explicit advanced-debug opt-in.
- Raise the assembly version to `0.3.15.0` and the mod metadata version to `0.3.15-dev`.
- Validate ordinary humans, Free Jaffa, Goa'uld-aligned Jaffa, Goa'uld hosts and Tok'ra dual identities, including personality switching, save/load and debug visibility.
- Confirm through repeated inspection that the report does not modify names, backstories, factions, marks, Prim'ta or persistent symbiote data.
- Validate both access paths, hidden-state behavior and a clean `Player.log` without requiring a post-test code correction.

## 0.3.14-dev - Audit Tok'ra identity through death and resurrection

- Validate the complete death, corpse, burial, grave save/load and resurrection lifecycle with either the host or symbiote personality active.
- Confirm that both names, both backstory records, the active-personality state and shared skill progression persist without rerolling or reconstruction.
- Confirm that no personality gizmo is exposed while the pawn is dead or buried and that exactly one returns after resurrection.
- Accept vanilla corpse and grave labels based on the active display name at death without creating a second corpse or grave identity.
- Validate ten repeated post-resurrection switches without skill loss, duplication or stacking.
- Validate post-resurrection save/load, Tok'ra extraction and reimplantation, plus Goa'uld and ordinary-human regressions.
- Keep the existing single-pawn, single-symbiote-data and shared-skill architecture because no reproducible defect required a C# correction.
- Correct stale `0.3.13-dev` validation and publication statuses left in the published tracking documents.
- Strengthen the milestone publication procedure with a documentary consistency gate before the final commit and a published-file review after tagging.
- Raise the assembly version to `0.3.14.0` and the mod metadata version to `0.3.14-dev`.

## 0.3.13-dev - Generate distinct identities for pre-joined Tok'ra

- Add a persistent `TokraHostIdentitySource` marker that distinguishes real implantations from Tok'ra generated already fused.
- Add configurable weighted `GeneratedHostOriginDef` profiles to the shared cultural framework.
- Configure an initial off-world-human host origin for `SG1_TokraVoluntaryHost` pawns without assuming Tau'ri ancestry.
- Generate one stable historical host name, childhood and adulthood from the persistent symbiote identity key.
- Keep the generated host and Tok'ra symbiote names distinct without relying on duplicate-name detection for migration.
- Add six narrowly scoped adult off-world-human backstories for village, caravan, medical, frontier, artisan and record-keeping lives.
- Declare those careers with the RimWorld 1.6 skill-gain map syntax and require their dedicated spawn category so they cannot leak into ordinary human backstory pools.
- Preserve the generated host as the active identity by default while keeping the Tok'ra name and career available through personality switching.
- Coordinate the cultural name manager so it can finalize the symbiote name without replacing the active generated-host name.
- Migrate older pre-joined Tok'ra once from an explicit unknown source marker and keep the result stable through later loads.
- Preserve real implantation behavior and replace the generated-host source with the actual new host after extraction and reimplantation.
- Extend the cultural-name debug samples and the player-facing backstory catalogue for the new off-world-human data.
- Raise the assembly version to `0.3.13.0` and the mod metadata version to `0.3.13-dev`.

## 0.3.12-dev - Audit Tok'ra active identity integration

- Centralize the direct-player-control boundary for Tok'ra dual-identity interfaces.
- Keep the existing map behavior for spawned player colonists.
- Treat permanent colonist owners of a player-controlled caravan as directly controlled while travelling.
- Keep guests, prisoners, slaves, allies, visitors, mental-state pawns and unrecruited quest pawns excluded.
- Add one compact `Tok'ra identities` caravan command that opens a list instead of creating one world-map gizmo per pawn.
- Delegate caravan switching to the same validated Hediff method and shared skill model used on colony maps.
- Attach the integration through a vanilla `WorldObjectComp` patch without adding Harmony.
- Prepare focused audits for Bio, Social, Health, messages, relations, death and resurrection before adding any further compatibility patch.
- Validate map-to-caravan-to-map switching, grouped multi-Tok'ra selection, interface consistency, player/AI boundaries, shared progression, save/load, extraction and a clean `Player.log`.
- Record the distinct generation gap for Tok'ra PawnKinds created already fused: a future dedicated milestone must generate a separate host identity from configurable weighted cultural origins instead of comparing or rerolling duplicate names.
- Raise the assembly version to `0.3.12.0` and the mod metadata version to `0.3.12-dev`.

## 0.3.11-dev - Add player-controlled Tok'ra personality switching

- Add one personality-switch gizmo to directly player-controlled Tok'ra colonists only.
- Switch the active displayed name, adulthood and derived title between the stored host and symbiote identities; preserve the host childhood when no dedicated symbiote childhood exists.
- Preserve exact `NameSingle` and `NameTriple` host-name structures for reversible switching.
- Add the reusable `BackstorySkillOffsetUtility` cultural-framework service.
- Store one shared raw-XP progression per skill and apply only the active backstory offsets.
- Preserve passions, gene aptitudes, randomized skill baselines and XP earned during play.
- Persist the active personality and shared skill state through save/load.
- Restore the host identity automatically before symbiote extraction, transfer or ordinary Hediff removal.
- Keep AI-managed Tok'ra on the classic behavior with no gizmo or manual personality change.
- Extend the dual-identity health summary with the currently active personality.
- Raise the assembly version to `0.3.11.0` and the mod metadata version to `0.3.11-dev`.
- Fix the first-click `NullReferenceException` caused by assigning a missing Tok'ra childhood to the vanilla story tracker.
- Prepare focused tests for repeated switching, shared XP, migration, save/load, extraction, reimplantation and Goa'uld regressions.
- Validate repeated switching without stacking, shared XP progression, save/load under both active identities, extraction, reimplantation, player/AI boundaries, Goa'uld regressions and a clean `Player.log`.

## 0.3.10-dev - Preserve implanted Tok'ra identity

- Extend the shared cultural profile schema with configurable persistent-identity childhood and adulthood pools.
- Configure the existing Tok'ra profile with the six current Tok'ra adult careers without adding culture-specific selection branches.
- Add deterministic identity-backstory selection keyed by the persistent symbiote ID.
- Store the host name, host childhood and host adulthood alongside the existing symbiote name and host history.
- Preserve the same symbiote name and Tok'ra career through recent implantation, active-host conversion, save/load, extraction and reimplantation.
- Display both identities and their recorded backgrounds in the health description of directly player-controlled Tok'ra hosts.
- Keep AI-managed Tok'ra on the previous classic display and expose no personality-switch gizmo in this milestone.
- Preserve the pawn's active name, active backstories, skills, relations, faction, body and equipment.
- Record the later player-only personality-switch prototype as a separate milestone because safe backstory-derived skill offsets still require dedicated validation.
- Raise the assembly version to `0.3.10.0` and the mod metadata version to `0.3.10-dev`.
- Validate implantation, active-host conversion, save migration, save/load, extraction, reimplantation, player/AI boundaries, Goa'uld regressions and clean logs in game.

## 0.3.9-dev - Add configurable starter cultural profiles

- Add reusable `CulturalPawnProfileDef` profiles configured in XML and resolved generically by priority.
- Support race, xenotype, `PawnKindDef`, faction and player-starter matching without culture-specific branches in the resolver.
- Add a low-priority ordinary-human starter profile that preserves vanilla generation while inserting the six Tau'ri / SGC adult careers into the same effective weighted pool as compatible vanilla adult backstories, without a fixed replacement chance.
- Migrate the existing cultural pawn-name manager to the shared resolver while preserving processed-name save data and current world-pawn behavior.
- Add a hidden scenario part to Def-based scenarios without adding Harmony as a dependency.
- Restrict newly randomized Jaffa starters to the current Jaffa childhoods and Goa'uld-aligned or Free Jaffa adult careers.
- Restrict newly randomized Goa'uld-host starters to off-world-human childhoods and Goa'uld-host or Tok'ra adult careers.
- Restrict adults in the stranded SG-team scenario to the six current SGC careers while preserving ordinary childhood generation.
- Select Jaffa, Goa'uld or Tok'ra starter names from the adulthood actually chosen by mixed profiles.
- Preserve each starter's randomized skill baseline, passions and gene aptitudes while applying the exact difference between the replaced and selected backstory bonuses.
- Preserve manual post-generation edits and leave raids, visitors, settlements, quests, incidents, developer spawns and ordinary world pawn generation unchanged.
- Add `docs/CULTURAL_FRAMEWORK.md` and document the starter rules in the player wiki.
- Raise the assembly version to `0.3.9.0` and the mod metadata version to `0.3.9-dev`.
- Validate starter profiles, vanilla-weighted Tau'ri career integration, world-generation isolation, persistence and clean logs in game.

## 0.3.8-dev - Rework existing cultural backstories

- Preserve all `52` existing cultural `BackstoryDef` identifiers, slots, categories and adulthood body-type defaults.
- Enrich every English and French description with a second sentence connecting cultural origin to practical experience.
- Add modest, coherent `skillGains` to every dedicated childhood and adulthood.
- Differentiate military, civilian, medical, technical, administrative, diplomatic and covert careers without forcing traits, passions or work incapabilities.
- Keep Tau'ri / SGC, Jaffa, Free Jaffa, off-world human, Goa'uld host, System Lord and Tok'ra generation filters unchanged.
- Preserve existing save assignments, pawn names and voluntarily implanted colon histories.
- Defer any increase in the number of backstories to a later dedicated discussion focused on a reasonable and maintainable volume.
- Record that future Asgard, Nox, Unas and other cultures require both names and backstories when introduced or immediately afterward.
- Refresh the cultural-backstory technical document and player wiki page, and update the wiki home page from the outdated documented version.
- Add culture-grouped wiki tables covering all `52` backstories with their French names, descriptions and exact skill bonuses, and establish the same documentation rule for future entries.
- Record the `0.3.x` direction toward a shared Def-driven cultural framework reusable by names, backstories, starter generation, scenarios and future culture-dependent systems.
- Defer culture-aware starting-pawn backstory filtering to a separate milestone, while explicitly preserving ordinary world generation and manual editor selections in this rework.
- Raise the assembly version to `0.3.8.0` and the mod metadata version to `0.3.8-dev`.
- Add `docs/TESTING_CURRENT.md` as the concise active-milestone test record while preserving the complete historical regression archive.
- Preserve the complete deferred design for player-controlled Tok'ra host / symbiote dual identity in `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`; no dual-identity gameplay is added in this milestone.

## 0.3.7-dev - Rewrite mod presentation

- Replace the oversized `About/About.xml` development inventory with a concise, immersive presentation.
- Introduce the stranded SG-team premise and the current Goa'uld, Jaffa, Free Jaffa and Tok'ra conflict.
- Summarize the major playable experiences without exposing milestone history, internal prototypes or the complete feature catalogue.
- State clearly that the functional Stargate and full off-world progression are not yet included.
- Keep the RimWorld Biotech dependency explicit.
- Add no gameplay, balance, Def, save-data or wiki change.
- Raise the assembly version to `0.3.7.0` and the mod metadata version to `0.3.7-dev`.

## 0.3.6-dev - Consolidate Goa'uld Jaffa PawnKind variants

- Audit the standard and Settlement Goa'uld-aligned Jaffa PawnKinds instead of treating their similar definitions as accidental duplicates.
- Keep `SG1_GoauldJaffaWarrior` and `SG1_GoauldJaffaGuard` for basic faction membership and Combat groups.
- Keep `SG1_GoauldSettlementJaffaWarrior` and `SG1_GoauldSettlementJaffaGuard` for Settlement groups.
- Preserve the Settlement warrior `maxPerGroup` limit of `7` and the Settlement guard limit of `2`.
- Preserve the standard guard `combatPower` of `145` and the intentionally lower Settlement guard value of `130`.
- Add abstract warrior and guard XML profiles to centralize the xenotype, backstories, weapons, apparel, ages and other shared fields.
- Preserve all four concrete `defName` values, faction references, save compatibility and cultural-name behavior.
- Add no new pawn, faction, incident, raid doctrine, settlement behavior or player-facing option.
- Update the durable project state, roadmap, testing workflow and version metadata.
- Record a future concise, immersive rewrite of the oversized `About/About.xml` description without changing it in this milestone.
- Raise the assembly version to `0.3.6.0` and the mod metadata version to `0.3.6-dev`.

## 0.3.5-dev - Add culture-specific pawn name generators


- Apply cultural names immediately when compatible pawns spawn, including through the vanilla developer `Spawn pawn` tool while the game is paused.
- Initialize generated Goa'uld and Tok'ra hosts before immediate naming so host and symbiote identities remain coherent even in paused developer spawns.
- Generate Tau'ri names for the stranded SG-team candidates before the player configuration page, while preserving later manual renaming.
- Preserve the initial player-pawn baseline while removing the short post-start window that could permanently register newly spawned test pawns under vanilla names.
- Add reusable culture-specific name generation for Goa'uld-aligned Jaffa, Free Jaffa, Goa'uld, Tok'ra and Tau'ri / SGC pawns.
- Apply cultural names once to newly generated map pawns, faction leaders and world pawns while preserving all existing pawns in older saves.
- Protect starting player pawns so scenario-editor and player-selected names remain unchanged.
- Persist processed pawn IDs and reserved cultural names to prevent repeated renaming and reduce duplicates during long games.
- Extend persistent adult-symbiote data with separate host and symbiote names, preparing future dual-identity displays.
- Add one grouped developer report containing samples from all five current cultural generators.
- Expose the same sample report from GateRim SG-1 settings only while advanced debug information is enabled.
- Record the future Asgard, Nox and Unas cultures, the optional GateRim-only world preset and the dedicated non-mandatory storyteller in the durable roadmap.
- Preserve compatibility with saves created from the `0.3.0-dev` framework baseline without renaming existing pawns.
- Raise the assembly version to `0.3.5.0` and the mod metadata version to `0.3.5-dev`.

## 0.3.4-dev - Improve Tok'ra observation site visuals and field flow

- Replace the generic observation-point marker with a dedicated field-scope texture so the deployed site reads clearly as Tok'ra surveillance equipment.
- Remove the former automatic six-hour recording timer.
- Require the assigned colon to remain at the deployed scope for a random observation period of roughly one to two in-game hours.
- Keep deployment, observation, recovery, return to the communicator and transmission in one continuous ordered task.
- Allow an interrupted field watch to resume directly from the installed observation site.
- Remove the construction effect and drill-like sound from deployment and recovery; the observer now remains beside and faces the scope during the field watch.
- Keep failure on device/site loss and on expiration of the accepted operation deadline.
- Convert active older timed observations into persistent remaining operator-controlled work without invalidating `0.3.x-dev` saves.
- Keep the observation site non-buildable and absent from Architect categories.
- Raise the assembly version to `0.3.4.0` and the mod metadata version to `0.3.4-dev`.
- Replace the outdated roadmap with a concise durable backlog and add explicit context-recovery instructions in `AGENTS.md` and `docs/PROJECT_STATE.md`.

## 0.3.3-dev - Rework organic Tok'ra wounded agent care

- Keep the validated wounded-agent arrival, rescue, treatment, departure and living-exit resolution flow unchanged.
- Add `SG1_TokraWoundedAgentPostShockRecovery` after the acute symbiote shock is tended.
- Suspend the direct Tok'ra therapeutic injury regeneration while acute symbiote shock remains active, then resume it at `25%` of its normal rate after emergency treatment.
- Keep ordinary tending, bleeding control and medical rest relevant without requiring forced medicine consumption or an arbitrary fixed waiting timer.
- Preserve the existing travel-fitness checks, five-day care window and death-priority failure rule.
- Apply the same weakened-recovery state through the shared debug phase-advance action.
- Remove the operation-specific shock and recovery conditions when the operation resolves.
- Update RP text so first aid no longer claims that normal Tok'ra regeneration immediately resumes.
- Preserve compatibility with saves created from the `0.3.0-dev` framework baseline.
- Raise the assembly version to `0.3.3.0` and the mod metadata version to `0.3.3-dev`.

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

## 0.3.66-dev - Add persistent Goa'uld inter-domain relations

- Start from published tag `v0.3.65-dev` on
  `feature/goauld-inter-domain-relations`.
- Add one persistent state for every unordered pair of Goa'uld System Lord
  faction instances: neutral, rivalry, open conflict, truce or alliance.
- Keep relations attached to factions rather than current leaders and reconcile
  new games, older saves, multiple domains and newly created domains.
- Advance relations only under `SG1_GateRimStoryteller`; freeze and shift all
  strategic deadlines while another storyteller is active.
- Use bounded transitions, `8â€“16` day initial delays, `12â€“24` day pair delays
  and `5â€“10` day global report spacing.
- Avoid selecting the previous pair again when another eligible pair exists.
- Add three English and French RP report variants per resulting state with
  immediate text anti-repetition.
- Add relation diagnostics, direct state setters, forced transitions, reset and
  an additional-domain test generator.
- Keep raids, threat points, doctrines, reprisals, goodwill, battles, alliances,
  expansion and settlement destruction mechanically unchanged.
- Prepare local revision `r1` for forced rebuild and focused in-game validation.
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
