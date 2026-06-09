# Tok'ra trust foundation

## Status

Prototype introduced in `0.1.50-dev`.

## Purpose

Track lightweight Tok'ra diplomatic consequences before enabling normal world
faction diplomacy, settlements or a complete quest layer.

The lazily generated Tok'ra runtime faction remains hidden. RimWorld hidden
factions do not participate in the normal goodwill workflow, so GateRim SG-1
stores a small persistent trust score owned by the mod.

## Score

```text
minimum = -100
initial = 0
maximum = 100
```

Therapeutic-offer outcomes apply:

```text
acceptation = +5
explicit refusal = -1
expiration without an answer = -2
```

The score is intentionally simple and prepares future thresholds for quests,
visitors, recruitment and eventual broader diplomacy.

## Persistence and visibility

`GameComponent_TokraTrustTracker` stores the score across save and reload.
Tracked therapeutic symbiotes display the current trust score below the offer
remaining duration. Each adjustment also generates a bilingual player message
and a `GR_Log` line.

## Current limits

The score does not yet unlock rewards, block incidents, change storyteller
weights or synchronize with vanilla goodwill. Those integrations remain future
milestones after the hidden-faction prototype is stabilized.
