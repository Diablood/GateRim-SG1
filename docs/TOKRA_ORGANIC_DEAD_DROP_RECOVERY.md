# Tok'ra organic intelligence analysis

## Purpose

`0.3.1-dev` reworks the recurring intelligence-module archetype on top of the reusable `0.3.0-dev` operation framework.

Internal identifiers containing `DeadDrop` remain stable for save compatibility, but player-facing text describes an intelligence-analysis request.

## Player flow

1. The shared scheduler opens the offer after a hidden trust-dependent delay.
2. Ignoring the offer closes it without a trust penalty.
3. An Intellectual-capable colon accepts through a powered Tok'ra secure communicator.
4. The operation places one sealed module near the Tok'ra delivery zone, near the communicator, or at a reachable map edge.
5. The module itself has no right-click completion action and is never constructible.
6. A colon uses the powered communicator and chooses one method:
   - cautious analysis;
   - accelerated decoding.
7. The colon performs persistent work at the communicator. Interruption or save reload preserves remaining work.
8. Completion removes the module, applies the common success once, sends one contextual result and schedules the next hidden opportunity.

No vanilla research bench can analyze the module.

## Analysis methods

### Cautious analysis

- `5000` work ticks, roughly two in-game hours.
- `350` Intellectual XP.
- No signal-patrol consequence.
- Result text is selected from three cautious variants.

### Accelerated decoding

- `2000` work ticks, under one in-game hour.
- `500` Intellectual XP, including the accelerated bonus.
- A `35%` internal chance of detectable interference after successful decoding.
- Result text is selected from three safe accelerated variants or three interference variants.

The chosen method is persisted and cannot be changed for that operation.

## Detectable interference

When accelerated decoding leaks a usable signal:

- the primary intelligence operation still succeeds immediately;
- a dedicated `SG1_GoauldJaffaSignalPatrol` incident is queued after `5000` to `12500` ticks, roughly two to five hours;
- the patrol uses the validated direct-assault worker and the current player map;
- threat points use `35%` of current default vanilla threat points, clamped between `180` and `700`;
- the incident has zero natural storyteller chance and can only be queued by this consequence or its debug action;
- the success letter warns the player in RP terms without exposing the roll or exact delay.

## Repeated result text

The manager persists the last result-variant index. Each successful occurrence chooses among three compatible variants and avoids the immediately previous index when possible.

Variants are grouped by context:

- cautious decoding;
- accelerated decoding without detected interference;
- accelerated decoding with a queued patrol.

The text can describe logistics, command frequencies, relay schedules or Jaffa patrol windows without pretending every repeated module contains the same intelligence.

## Failure

An accepted operation fails exactly once if:

- the module is destroyed or lost;
- its saved reference or active map becomes invalid;
- the secure deadline expires before analysis finishes.

An unfinished or failed operation cannot queue the signal patrol.

## Architect and objective rules

The module:

- has `<designationCategory IsNull="True" />`;
- has no build designation and no Architect category;
- is generated only by the operation;
- cannot be minified or deconstructed;
- has no market value and yields no resources;
- remains physically destructible;
- contains no direct-interaction ThingComp.

## Persistence

`TokraOrganicOperationInstance` persists:

- chosen method;
- total and remaining work ticks;
- whether the interference roll was resolved;
- whether interference occurred;
- whether the patrol was queued;
- selected result variant.

`lastIntelligenceResultVariant` is persisted by the manager to prevent immediate repeated prose across occurrences.

## `0.3.0-dev` compatibility

The `0.3.0-dev` save baseline remains supported:

- new fields default to no selected method and zero progress;
- an accepted module resumes through the communicator;
- the former `SG1_SecureTokraOrganicDeadDrop` JobDef remains as compatibility-only data;
- its rewritten JobDriver stops an already saved direct-module job and tells the player to resume from the communicator;
- no new direct-module job can be created.

The obsolete direct-interaction ThingComp classes are removed.

## Debug surface

The single communicator debug menu and developer actions can:

- force the offer;
- accept and place the module;
- select either method;
- force the interference patrol;
- advance, succeed, fail or expire the operation;
- inspect exact persisted work and consequence state;
- reset the framework.

Normal communicator text never reveals the operation catalogue, hidden timing, interference roll or queued-incident internals.
