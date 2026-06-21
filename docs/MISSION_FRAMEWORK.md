# GateRim SG-1 mission framework

Status: foundation published in `0.3.23-dev`; complete observation data migration validated and published in `0.3.24-dev`.

## Purpose

The framework is a reusable toolbox for recurring missions and longer questlines. It is intended to cover roughly 70 to 90 percent of common mission structure while preserving explicit C# extension points for mechanics that are genuinely unique.

It must not become a universal scripting language for RimWorld. XML owns declarative mission content and balance. Specialized workers and adapters own engine-facing mechanics such as spawning, reservations, pathfinding, Toils, map transitions and unusual objectives.

## Shared definition vocabulary

`GateRimMissionDef` currently supports:

- offer, ready and deadline timing;
- hidden recurrence delay ranges;
- per-context weights and local repeated-mission penalties;
- difficulty snapshots based on RimWorld threat points;
- weighted offer and success text banks;
- named runtime text keys used by specialized adapters;
- common actions and status keys;
- generic per-skill XP rewards and trust consequences;
- phases, objectives, conditions, transitions and consequences;
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

## Recurrence behavior

After a MissionDef-backed operation resolves, the scheduler uses that definition's configured minimum and maximum hidden delay. Legacy operations continue to use their historical trust-tier delay ranges until they are migrated.

The player must never see these internal ranges in normal play. They are visible only in developer reports and internal documentation.

## Difficulty behavior

A MissionDef can capture current RimWorld threat points when offered. The observation operation records this snapshot but does not fabricate a combat encounter merely to consume it.

A future migrated mission with a natural threat should use the captured budget to size enemies, equipment, timing or constraints. Fixed enemy counts should be avoided when a storyteller threat budget can express equivalent behavior.

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
- phase and text-bank counts;
- recurrence factor and hidden delay range;
- difficulty mode and current threat snapshot;
- generic skill XP rewards;
- every objective's target, secondary target, job, primary and secondary work duration, skill and XP rate.

Player-facing interfaces must not expose this technical configuration.

## Validation rule for future migrations

Before treating a migrated mission as a framework reference:

1. remove complete C# duplicates of its declarative data;
2. reject missing, invalid or duplicated required configuration explicitly;
3. preserve specialized mechanics that are not shared yet;
4. validate normal completion, every meaningful failure, interruption and save/reload;
5. validate recurrence and text anti-repetition across several occurrences;
6. verify that no technical details leak into player-facing texts;
7. test all still-legacy operations for regression.

The next mission migration should exercise a genuinely new shared requirement—preferably adaptive threat consumption—rather than extending the framework speculatively.
