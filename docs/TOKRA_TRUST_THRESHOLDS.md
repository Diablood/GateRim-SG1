# Tok'ra trust tiers

## Status

Prototype introduced in `0.1.51-dev`.

## Purpose

Turn the persistent Tok'ra trust foundation into a first lightweight gameplay
signal without enabling full diplomacy, quests or material rewards yet.

The tier is read once when an escorted therapeutic opportunity is created.
Later trust changes do not retroactively alter the active offer.

## Thresholds

```text
wary        : -100 to -1
neutral     : 0 to 9
cooperative : 10 to 24
trusted     : 25 to 100
```

## Therapeutic-offer effects

| Tier | Offer duration | Escort size |
|---|---:|---:|
| Wary | 1 RimWorld day | exactly 1 host |
| Neutral | 2 RimWorld days | 1 to 2 hosts |
| Cooperative | 3 RimWorld days | exactly 2 hosts |
| Trusted | 4 RimWorld days | 2 to 3 hosts |

The thresholds intentionally remain easy to test during development. Two
accepted offers move a neutral colony into the cooperative tier, while five
accepted offers reach the trusted tier from the default score of `0`.

## Current limits

The tiers do not yet modify storyteller weights, unlock rewards, start quests,
block incidents or synchronize with vanilla goodwill. Those integrations remain
separate milestones.
