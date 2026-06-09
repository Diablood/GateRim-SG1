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

| Tier | Offer duration | Offer escort | Offer gift | Independent delivery |
|---|---:|---:|---:|---:|
| Wary | 1 RimWorld day | exactly 1 host | none | locked |
| Neutral | 2 RimWorld days | 1 to 2 hosts | none | locked |
| Cooperative | 3 RimWorld days | exactly 2 hosts | 1 dose | 2 doses with 1 visitor |
| Trusted | 4 RimWorld days | 2 to 3 hosts | 2 doses | 4 doses with 2 visitors |

Since `0.1.53-dev`, cooperative and trusted relations may also receive an
independent rare medical-support delivery without a sick pawn or symbiosis
offer.

## Current limits

Since `0.1.52-dev`, cooperative and trusted teams bring a small physical
trétonin-support gift near their arrival point. The supplies remain available
regardless of the final response. The existing therapeutic-opportunity storyteller weight remains unchanged.
The new independent delivery has its own rare incident definition. Quests,
incident blocking and vanilla-goodwill synchronization remain separate
milestones.
