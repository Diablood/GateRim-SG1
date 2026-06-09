# Tok'ra storyteller trust weighting

## Status

Prototype introduced in `0.1.54-dev`.

## Purpose

Make Tok'ra trust influence not only the content of incidents but also how often
trust-sensitive Tok'ra incidents may be selected naturally by the storyteller.

Manual developer-triggered incidents remain available for controlled tests.

## Therapeutic opportunities

The XML incident definition keeps a readable neutral baseline chance of
`0.035`. The runtime worker multiplies it by the current trust tier:

| Tier | Factor | Effective base chance |
|---|---:|---:|
| Wary | ×0.50 | 0.0175 |
| Neutral | ×1.00 | 0.0350 |
| Cooperative | ×1.25 | 0.04375 |
| Trusted | ×1.50 | 0.0525 |

The existing day-`30` earliest date, sick-pawn eligibility checks and `60`-day
minimum refire interval still apply. Since `0.1.55-dev`, wary relations also
apply a persistent short diplomatic cooldown after a refusal or expiration
before the `×0.50` selection weight becomes usable again.

## Independent medical-support deliveries

The XML incident definition keeps a cooperative baseline chance of `0.018`.
Deliveries remain unavailable below cooperative trust:

| Tier | Factor | Effective base chance |
|---|---:|---:|
| Wary | ×0.00 | locked |
| Neutral | ×0.00 | locked |
| Cooperative | ×1.00 | 0.018 |
| Trusted | ×1.50 | 0.027 |

The existing day-`45` earliest date, trust gate and `60`-day minimum refire
interval still apply.

## Technical implementation

`GameComponent_TokraTrustTracker` centralizes the factors. Each trust-sensitive
incident worker overrides `BaseChanceThisGame` and multiplies the XML baseline
provided by RimWorld.

Successful manual tests write the active factor and effective runtime base
chance to `GR_Log`, making each tier easy to verify without waiting for natural
storyteller selection.

## Current limits

Peaceful Tok'ra visitor frequency remains unchanged. Quests, vanilla goodwill
and broader diplomacy remain separate future milestones.
