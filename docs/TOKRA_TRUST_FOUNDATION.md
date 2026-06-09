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

Since `0.1.51-dev`, the score also selects a lightweight gameplay tier for
therapeutic offers. Since `0.1.52-dev`, cooperative and trusted tiers unlock
small tretonin-support gifts. Since `0.1.53-dev`, cooperative and trusted
relations may also receive independent rare tretonin-support deliveries.
Since `0.1.54-dev`, the current tier also modulates the storyteller weights of
therapeutic opportunities and independent deliveries. Since `0.1.55-dev`, a
negative response that leaves trust below `0` also starts a temporary wary
diplomatic cooldown before another therapeutic opportunity may begin. The score
still prepares future thresholds for quests, visitors, recruitment and eventual
broader diplomacy.

## Persistence and visibility

`GameComponent_TokraTrustTracker` stores the score across save and reload.
Tracked therapeutic symbiotes display the current trust score and localized
tier below the offer remaining duration. Each adjustment also generates a
bilingual player message and a `GR_Log` line.

## Current limits

The score now adjusts therapeutic-offer duration, escort size, small physical
tretonin-support gifts and storyteller weights for the two trust-sensitive
Tok'ra incidents. Wary relations now also suspend new therapeutic offers for a
short persistent cooldown after refusal or expiration. It does not yet unlock
quests or synchronize with vanilla goodwill. Those integrations remain future milestones after the hidden-faction
prototype is stabilized.
