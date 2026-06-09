# Tok'ra trust tiers

## Status

Prototype introduced in `0.1.51-dev`.

## Purpose

Turn the persistent Tok'ra trust foundation into a lightweight gameplay signal
without enabling full diplomacy or quests yet.

The tier is read once when an escorted therapeutic opportunity is created.
Later trust changes do not retroactively alter the active offer.

## Thresholds

```text
wary        : -100 to -1
neutral     : 0 to 9
cooperative : 10 to 24
trusted     : 25 to 100
```

## Trust-tier effects

| Tier | Offer duration | Offer escort | Offer gift | Offer weight | Independent delivery | Delivery weight |
|---|---:|---:|---:|---:|---:|---:|
| Wary | 1 RimWorld day | exactly 1 host | none | ×0.50 after cooldown | locked | ×0.00 |
| Neutral | 2 RimWorld days | 1 to 2 hosts | none | ×1.00 | locked | ×0.00 |
| Cooperative | 3 RimWorld days | exactly 2 hosts | 1 dose | ×1.25 | 2 doses with 1 visitor | ×1.00 |
| Trusted | 4 RimWorld days | 2 to 3 hosts | 2 doses | ×1.50 | 4 doses with 2 visitors | ×1.50 |

Since `0.1.53-dev`, cooperative and trusted relations may also receive an
independent rare medical-support delivery without a sick pawn or symbiosis
offer. Since `0.1.55-dev`, wary relations temporarily suspend new therapeutic
opportunities after a negative response: `3` days after an explicit refusal and
`5` days after an unanswered expiration.

## Current limits

Since `0.1.52-dev`, cooperative and trusted teams bring a small physical
trétonin-support gift near their arrival point. The supplies remain available
regardless of the final response. Since `0.1.54-dev`, the current tier also multiplies the storyteller weights
of therapeutic opportunities and independent deliveries. The XML values
remain neutral baseline chances. Quests and vanilla-goodwill synchronization
remain separate milestones.
