# GateRim SG-1 mission framework

Status: foundation published in `0.3.23-dev`; four legacy Tok'ra operations migrated through `0.3.27-dev`; shared orchestration audit validated and published in `0.3.28-dev`.

## Purpose

The framework is a reusable toolbox for recurring missions and longer questlines. It is intended to cover roughly 70 to 90 percent of common mission structure while preserving explicit C# extension points for mechanics that are genuinely unique.

It must not become a universal scripting language for RimWorld. XML owns declarative mission content and balance. Specialized workers and adapters own engine-facing mechanics such as spawning, reservations, pathfinding, Toils, map transitions and unusual objectives.

## Shared definition vocabulary

`GateRimMissionDef` currently supports:

- offer, ready and deadline timing;
- hidden recurrence delay ranges, including optional per-context ranges;
- per-context weights and local repeated-mission penalties;
- difficulty snapshots based on RimWorld threat points;
- weighted offer and success text banks;
- named weighted text banks used by specialized adapters;
- named runtime text keys used by specialized adapters;
- common actions and status keys;
- generic per-skill XP rewards and trust consequences;
- phases, objectives, conditions, transitions and consequences;
- consequence chance, delay range and retry parameters;
- objective target and secondary-target Def names;
- objective JobDef and SkillDef names;
- primary and secondary work durations;
- active XP gained per work tick;
- persistent generic runtime data;
- optional C# mission workers.

The named runtime text collection exists for adapter messages that do not yet justify a universal phase executor. Entries use stable semantic IDs such as `deviceLost` or `transmissionStarted`; the C# adapter knows the semantic event, while the MissionDef selects the player-facing translation key.

## Persistent occurrence data

`GateRimMissionRuntimeData` stores:

- MissionDef identity;
- current common phase identifier;
- base and scaled threat-point snapshots;
- difficulty factor;
- text-bank indexes;
- generic counters and scalar values.

A specialized mission can keep typed save fields beside this generic state while it is migrated. Existing save data must never be rerolled merely because more fields become Def-driven.

## Observation reference implementation

`SG1_TokraOrganic_GoauldObservation` is the first complete data-backed adapter reference.

The MissionDef now owns:

- the observation device and field-marker Def names;
- the deployment and transmission JobDef names;
- `500` deployment ticks;
- `10000` active observation ticks;
- `500` recovery ticks;
- `1000` transmission ticks;
- the `Intellectual` work skill and `0.04` XP per active tick;
- offer duration and accepted-operation deadline;
- hidden recurrence range `240000–480000` ticks;
- trust-tier weights and repeated-archetype factor;
- all observation action keys;
- all observation-specific messages, disabled reasons and status keys;
- three weighted success-letter variants with immediate anti-repetition;
- trust and a generic final `Intellectual +250` skill XP reward.

The previous complete C# fallback definition has been removed. If the required MissionDef is absent, incomplete or references an unknown required `ThingDef`, `JobDef` or `SkillDef`, the observation archetype is omitted and one explicit error is written to the log. Silent recovery to old balance or text values is forbidden because it would conceal a broken configuration.

## Intelligence-recovery reference implementation

`SG1_TokraOrganic_IntelligenceRecovery` is the second complete data-backed adapter and the first one to consume adaptive difficulty.

The MissionDef owns:

- the recovered module, analysis JobDef and Intellectual SkillDef;
- `10000` cautious-analysis ticks and `5000` accelerated-analysis ticks;
- the generic `Intellectual +350` success reward and accelerated `+150` bonus;
- all offer, acceptance, objective, method, status, failure and result text keys;
- three independent named result banks with three weighted variants each;
- trust-tier weights, repeat penalty and trust-tier-specific hidden delay ranges;
- a `ThreatPointsScaled` profile using factor `0.35`, minimum `180` and maximum `700` points;
- the accelerated `35%` interference chance;
- the Goa'uld patrol IncidentDef, `5000–12500` tick queue delay and `2500` tick retry delay.

The complete legacy C# definition has been removed. Required ThingDef, JobDef, SkillDef and IncidentDef references, the adaptive difficulty mode and all four trust-tier delay ranges are validated before the archetype becomes eligible.

The operation captures base and scaled threat points when the offer is created. If accelerated analysis triggers interference, the queued incident receives that stored scaled budget even if colony wealth or current storyteller points have changed since the offer.

Validation on local revision `r2` confirmed the final `10000 / 5000` tick pacing, progress persistence and adaptive patrol budgets on both a weak colony and an advanced colony.

The `0.3.26-dev-r1` archive accidentally omitted the accelerated objective's `5000`-tick XML field. The existing validator disabled intelligence recovery explicitly at load, proving that no hidden C# balance fallback remained. Validated revision `r2` restores the XML field only, without changing C# or intelligence-recovery balance.

## Specialized observation adapter

The observation adapter still owns:

- creation and validation of a suitable peripheral map cell;
- physical delivery and placement;
- reservations and reachability checks;
- hauling and carrying the device;
- ordered deployment, operation, recovery and transmission Toils;
- interruption and resumption behavior;
- powered-communicator validation;
- persistent references to the physical objects;
- migration of older active occurrences.

These are implementation mechanics rather than duplicated mission content. They should only move into shared code when another real mission needs the same behavior.

## Wounded-agent care migration (`0.3.26-dev`)

`SG1_TokraOrganic_WoundedAgentCare` is the third complete data-backed adapter and the first one centered on a persistent pawn objective.

The MissionDef owns:

- the generated pawn kind and operation-specific Hediffs;
- stable-health and departure-grace durations;
- Moving, Consciousness, summary-health, bleeding and critical-Hediff thresholds;
- adaptive optional-illness chance and severity ranges;
- trust-tier weights, repeat penalty and trust-tier-specific hidden delays;
- all offer, arrival, progress, status, failure and success text keys;
- three offer variants and three success variants with local anti-repetition;
- trust consequences and the declarative phase graph.

The adapter still performs the actual RimWorld work: pawn generation, wounds, medical-bed checks, tending detection, live health evaluation, Lord behavior, departure and save/load references. The framework does not pretend that those engine mechanics are already generic.

Optional illness uses the scaled threat snapshot captured at offer time. Chance and severity are interpolated between configured minimum and maximum values, so an advanced colony can receive a more demanding patient without changing the rescue flow or fabricating a combat encounter.

A missing or invalid required PawnKind, Hediff, medical threshold, recurrence context or runtime text disables this archetype with an explicit configuration error. There is no complete C# fallback definition.

Validation used the exact developer actions:

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

and:

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force wounded agent offer
```

The validated happy path accepts through the powered communicator, rescues the downed agent into a colony medical bed, tends the symbiote shock, keeps the pawn above the configured thresholds for `5000` ticks and confirms success only after the agent leaves the map alive. Failure, adaptive difficulty on weak and advanced colonies, save/load, recurrence, text variation and regression coverage were also validated and remain documented in `docs/TESTING_CURRENT.md` and `docs/TESTING.md`.

## Medical-supply handoff migration (`0.3.27-dev`)

`SG1_TokraOrganic_MedicalSupplyHandoff` is the fourth data-backed adapter and the first one centered on a visiting liaison and a configured resource exchange.

The MissionDef owns:

- liaison PawnKind, arrival-delay range and departure-grace duration;
- delivered ThingDef, required count, dialogue JobDef and Social SkillDef;
- offer duration, accepted deadline, trust-tier weights and hidden recurrence ranges;
- generic `Social +350` XP and success/failure trust changes;
- the additional trust loss if a departing liaison dies after a completed handoff;
- all offer, arrival, interaction, dialogue, status, failure, success and post-handoff text keys;
- three offer variants and three success variants with local anti-repetition;
- the declarative offered, accepted, ready, succeeded and failed phases.

The shared `handoff` block is deliberately small. Resource requirements use the existing `DeliverThing` objective instead of duplicating fields. The adapter still owns map entry, meeting-cell selection, Lord behavior, pathfinding, reservations, dialogue execution, actual Thing-stack consumption, pawn references and departure monitoring.

A missing PawnKindDef, ThingDef, JobDef, SkillDef, count, recurrence range, reward or required runtime text disables the archetype explicitly. There is no complete C# fallback definition.

Local revision `r1` validated the complete handoff flow, exact resource consumption, interaction restrictions, failure paths, post-handoff death consequence, save/load persistence, recurrence, text variation and regressions. The detailed coverage remains recorded in `docs/TESTING_CURRENT.md` and `docs/TESTING.md`.

## Distress-call world-site adapter (`0.3.29-dev`)

`SG1_TokraOrganic_DistressCall` is the fifth MissionDef-backed Tok'ra operation and the first recurrent mission whose primary objective exists on a generated world site.

The MissionDef owns:

- offer duration, accepted-site deadline and late-arrival threshold;
- trust-tier weights, repeat penalty and trust-tier-specific hidden delays;
- scaled threat factor and minimum/maximum captured points;
- WorldObjectDef, survivor PawnKindDef, salvage ThingDef and vanilla storage ThingDef references;
- world-site distance, generated-map size and survivor/defender/salvage ranges;
- hidden-variant weights and per-variant threat multipliers;
- emergency-extraction delay and preferred caravan-entry radius;
- scene-selection chances and optional Tok'ra/Jaffa corpse ranges for each variant;
- all offer, target, arrival, status, failure, success and trust text keys;
- three offer variants and three success variants with local anti-repetition;
- Medicine XP and trust consequences.

The adapter deliberately reuses RimWorld's caravan systems when they already provide the desired behavior. The world-object float menu creates a normal `CaravanArrivalAction`; reaching the tile automatically generates the map, calls the vanilla potentially-hostile-map notification to pause the game and enters through `CaravanEnterMapUtility` with player colonists drafted. GateRim adds only a preferred edge-cell filter so the vanilla entry remains reasonably close to the encounter.

Physical mission rewards tied to a generated map should normally exist in that map from the moment the scene is created. They should use contextual vanilla storage or structures where appropriate, rather than appearing beside the player only when the success state is applied. Abstract rewards such as trust or skill experience remain outcome-based.

The mission-specific scene generator owns the behavior that vanilla does not provide: a single narrative anchor coordinates survivors, defenders, salvage, debris, context structures and optional pre-existing corpses. A genuine rescue may show an attacked caravan or a small Tok'ra temporary camp; a compromised signal uses a prepared hostile position; a late arrival may show an overrun camp or the remains of an ambushed caravan. Its component cache is created with the scene on a configured vanilla `Building_Storage`, currently `Shelf`; mission resolution does not spawn a second material reward. All structures, terrain, roofs, items and pawns use vanilla Defs and spawning systems.

The planned variant is selected once at acceptance and stored in generic mission runtime counters. A genuine rescue may degrade into the late-arrival state when the caravan reaches the site after the configured threshold. A compromised signal and a preselected late-arrival state do not change according to travel speed.

The scaled threat snapshot is captured when the offer is created and reused when the site map is initialized. Per-variant factors modify that stored value; the adapter does not recalculate colony wealth or storyteller threat at arrival. Defender counts remain bounded by the MissionDef.

For a genuine rescue, vanilla tending remains the actual player action. The special symbiote-shock Hediff can be treated while the survivor is downed on the ground; a player medical bed is not required. Once the shock has been tended and no active hostile remains, the map component starts a short configured delay and brings in a visible Tok'ra recovery team through the vanilla `EdgeWalkIn` arrival worker. Downed survivors are assigned the vanilla non-hostile `Kidnap` job path, whose report is presented as a rescue and whose base driver physically carries the pawn to an exit cell. Survivors able to walk use the existing Tok'ra departure lord. The mission therefore does not wait for complete natural healing, hunger management or temperature-safe housing, but it resolves only after a survivor actually leaves the map alive.

The local map component persists survivor references, treatment state, recovery-team references, arrival and retry timing, assigned and extracted-survivor IDs, the preferred entry cell and resolution state. The global operation manager remains the sole owner of trust, recurrence, active-slot and outcome accounting. This separation prevents a generated map from becoming a second independent mission scheduler. Existing save-field names are retained where possible so `r2` test saves can load into the revised flow.

A missing WorldObjectDef, PawnKindDef, HediffDef, required text, timing value, threat profile or recurrence range disables the distress-call archetype explicitly. There is no hidden complete C# fallback.

Developer validation uses three separate actions to force genuine rescue, compromised signal and late arrival. The normal player interface never lists these possible variants before entry.

## Recurrence and orchestration behavior

After a MissionDef-backed operation resolves, the scheduler first uses a configured range for the active context, such as a Tok'ra trust tier, and otherwise uses the definition's generic minimum and maximum hidden delay. Success, failure and ignored or expired offers all return to the same persistent scheduler. All five current Tok'ra organic operations consume their configured recurrence data through the framework.

Natural selection follows this order:

1. gather definitions with a positive weight for the current context;
2. call the optional mission worker's `CanOffer(map)` condition;
3. remove temporarily unavailable definitions;
4. apply the local repeat factor to the previously offered archetype;
5. draw from the remaining weights.

Filtering before the draw is important for future world-site and caravan missions. A temporarily impossible site must not consume a selection attempt while another operation is valid. If weighted candidates exist but all are unavailable, the manager retries on its normal internal check interval. It does not pretend that a mission occurred and does not consume a full recurrence delay.

One manager owns one global active slot. No second organic operation may be offered while the current one is offered, accepted, active or ready. The save stores the active occurrence, last offered and completed archetypes, outcome counters, text history and next hidden opportunity.

The player must never see internal ranges, weights, histories or future archetypes in normal play. They are visible only in developer reports and internal documentation.

### Orchestration diagnostics (`0.3.28-dev`)

Two developer-only actions support long-run validation:

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: roll next natural offer
```

This action uses the real natural filter, repeat penalty and weighted draw. It does not force a particular archetype and refuses to replace an active occurrence.

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: audit long-term orchestration
```

The audit report shows the global active slot, hidden scheduling state, outcome counters, current offerability, every trust-tier weight and delay, text-bank counts and a deterministic `5000`-draw simulation per tier. The simulation verifies reachability and reports immediate-repeat frequency without mutating the save.

Local revision `r1` validated the audit as `PASS`, the real natural draw, success/failure/ignored rescheduling, recurrence, local anti-repetition, save/load persistence, storyteller independence, all four previously existing operation regressions and a clean `Player.log`.

## Locked Tok'ra mission expansion

The next mission sequence is:

1. `0.3.28-dev` — audit orchestration and long-term recurrence;
2. `0.3.29-dev` — add a Tok'ra distress-call world-site mission;
3. `0.3.30-dev` — add a Tok'ra temporary-base delivery mission.

The distress call will create a temporary world site with a failure timer and a hidden state revealed on arrival: genuine survivors, a compromised signal or trap, or a late arrival with no allied survivors and enemy forces still searching, guarding or leaving.

The delivery mission will create a temporary Tok'ra destination and configurable cargo. It must handle normal delivery, interception, cargo loss, delay, abandonment and a compromised destination.

The common framework should own declarative phases, timing, cargo or objectives, texts, recurrence, difficulty, rewards and consequences. World-site generation, caravan movement, interception and combat remain specialized adapters until several real missions justify additional shared vocabulary.



## Deferred Tok'ra access progression

A future introduction arc will separate first contact from the recurrent mission pool:

1. a unique combat-bearing Tok'ra encounter or recovery mission awards a persistent key artifact;
2. that artifact enables a dedicated GateRim SG-1 research project with vanilla `Electricity` as prerequisite;
3. the completed research unlocks construction of the Tok'ra communicator;
4. recurrent Tok'ra operations become eligible only while a built, powered communicator is available.

The artifact identity, enemy force, site structure and failure recovery are deliberately unresolved. The introduction must remain unique and narratively meaningful, but a failed first attempt must not permanently lock a campaign. This progression is deferred and remains separate from the recurrent distress-call and delivery archetypes.

## Long-term faction pools

The mission system is technically faction-neutral, not Tok'ra-only. Development completes and enriches the Tok'ra pool first. Later milestones may add independent Goa'uld mission pools and then missions for Jaffa, Asgard, Nox, Unas and other factions.

Each pool must retain its own RP identity, appearance conditions, rewards and consequences. Mission balance is intentionally evolvable: frequencies, rewards, variants and mechanics may be adjusted after prolonged play, including after `1.0.0`, without rewriting the scheduler or persistent runtime model.

## Difficulty behavior

A MissionDef can capture current RimWorld threat points when offered. Observation records the snapshot without fabricating a combat encounter. Intelligence recovery can queue a Goa'uld patrol from the stored scaled snapshot. Wounded-agent care uses the same captured scale to interpolate optional illness chance and severity.

The budget is captured at offer time and remains stable for that occurrence. Consequences must not recalculate a more convenient value later. Fixed enemy counts should be avoided when a storyteller threat budget can express equivalent behavior.

## Text variation

Offer and success banks use weighted entries. When more than one valid entry exists, the immediately previous index is removed from the candidate set before drawing. This prevents obvious back-to-back repetition without imposing a predictable cycle.

A single text remains acceptable when it is deliberately written to survive repetition. Quantity must not replace RP quality.

## Developer inspection

With developer mode enabled:

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

The report lists:

- loaded definitions;
- phase, runtime-text and named-text-bank counts;
- recurrence factor, generic hidden delay and per-context ranges;
- difficulty mode and current threat snapshot;
- generic skill XP rewards;
- every objective's target, secondary target, job, primary and secondary work duration, skill and XP rate;
- every configured consequence's target, value, chance, delay range and retry delay.

Player-facing interfaces must not expose this technical configuration.

## Validation rule for future migrations

Before treating a migrated mission as a framework reference:

1. remove complete C# duplicates of its declarative data;
2. reject missing, invalid or duplicated required configuration explicitly;
3. preserve specialized mechanics that are not shared yet;
4. validate normal completion, every meaningful failure, interruption and save/reload;
5. validate recurrence and text anti-repetition across several occurrences;
6. verify that no technical details leak into player-facing texts;
7. test all other framework-backed operations for regression.

The intelligence-recovery and wounded-agent migrations exercise two different forms of adaptive threat consumption, while medical handoff proves a bounded visitor-and-resource exchange profile. Future missions should add shared vocabulary only when a concrete operation proves the need.
