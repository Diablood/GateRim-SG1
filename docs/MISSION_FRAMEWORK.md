# GateRim SG-1 mission framework

Status: foundation introduced in `0.3.23-dev`; focused validation required.

## Purpose

The mission framework is a reusable toolbox for recurring operations and longer questlines. It is intended to cover roughly 70 to 90 percent of common mission structures while keeping explicit C# extension points for unique mechanics.

It is not intended to replace RimWorld's Quest system or to force every mission into one universal abstraction.

## Architecture

### `GateRimMissionDef`

XML definitions may describe:

- offer, ready and deadline timing;
- recurrence delays, context weights and repeated-mission penalties;
- RimWorld threat-point capture and scaling profiles;
- RP text banks with weighted variants;
- action, status and trust-message keys;
- skill rewards and trust consequences;
- ordered phases, reusable objectives, conditions, transitions and consequences;
- an optional specialized worker class.

### `GateRimMissionRuntimeData`

Each active occurrence may persist:

- its mission Def and current generic phase;
- the captured base and scaled threat points;
- selected text-variant indexes;
- generic counters and scalar values.

Specialized systems may keep strongly typed fields beside this record during gradual migration. Existing saves therefore do not need to be converted into a completely new object graph at once.

### `GateRimMissionWorker`

A specialized worker remains available when a mission requires behavior outside the common vocabulary. Examples include custom map generation, unusual medical logic, faction-specific AI or a unique interaction sequence.

The framework should expand only after several real missions need the same new concept.

## Replayability rules

Recurring missions must normally:

- become eligible again after success, failure or ignored offers;
- use hidden variable delays;
- penalize the most recently offered archetype;
- avoid immediate reuse of the same visible RP text when alternatives exist;
- keep technical weights, indexes and timing logic out of player-facing text.

Text variants are not mandatory when a single carefully written contextual text remains natural after repetition. Quality takes priority over the number of variants.

## Difficulty rules

Mission difficulty profiles use RimWorld's current storyteller threat points as their shared baseline. Those points already reflect the active storyteller context and colony strength.

A mission may store:

- base threat points at the relevant phase;
- a mission-specific scaling factor;
- minimum and maximum bounds;
- the final scaled value used by specialized mechanics.

Fixed enemy counts should be avoided when a threat-point budget can provide equivalent behavior. Non-combat missions may still capture a snapshot without consuming it immediately, as demonstrated by the first observation pilot.

## First pilot

`SG1_TokraOrganic_GoauldObservation` migrates the recurring Tok'ra observation operation to an XML-backed definition while retaining its existing specialized execution code.

The pilot validates:

- Def loading and adapter fallback;
- specialized-worker hooks for eligibility, offer, acceptance, periodic updates and resolution;
- per-tier selection weights;
- per-mission repeat penalty;
- three RP offer variants with local anti-repetition;
- persistent generic runtime data;
- the real observation work duration read from the `MaintainOperator` objective instead of a mission-specific random C# range;
- threat-point snapshot capture;
- old-save initialization;
- coexistence with three still-legacy organic operations.

The XML phases, conditions, transitions and consequences are validated as reusable data in this first foundation. The existing observation code remains authoritative for executing its detailed field sequence; a generic transition evaluator will be introduced only when a second migrated mission demonstrates the common execution rules.

The intelligence recovery, wounded-agent care and medical handoff operations remain intentionally unmigrated in this milestone. A later combat-capable pilot should validate actual threat-budget consumption before the framework is expanded further.

## Debug contract

The common inspection action is available only in developer mode:

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

It reports loaded Defs, phase and text counts, recurrence penalties and the current map's threat snapshot. Per-occurrence technical data remains in the existing Tok'ra operation report.

Future generic phase-forcing tools should remain grouped under one mission debug entry rather than adding many top-level gizmos.
