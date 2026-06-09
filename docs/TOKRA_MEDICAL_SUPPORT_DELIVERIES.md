# Tok'ra medical-support deliveries

## Status

Prototype introduced in `0.1.53-dev`.

## Purpose

Add a first trust-gated Tok'ra support incident that can occur independently
from therapeutic symbiosis offers. This makes cooperative relations useful even
when no colon currently needs a host symbiote.

## Storyteller tuning

```text
XML baseline chance: 0.018
earliest day       : 45
minimum refire     : 60 days
required trust tier: cooperative or trusted
cooperative factor : ×1.00 -> effective base chance 0.018
trusted factor     : ×1.50 -> effective base chance 0.027
```

## Tier behavior

| Tier | Tretonin delivery | Visitor team |
|---|---:|---:|
| Wary | locked | locked |
| Neutral | locked | locked |
| Cooperative | 2 physical doses | exactly 1 voluntary host |
| Trusted | 4 physical doses + 1 ultratech medicine | exactly 2 voluntary hosts |

The supplies appear near the Tok'ra arrival point. Since `0.1.56-dev`, trusted
deliveries also include `1` physical vanilla `MedicineUltratech` unit as an
advanced aid package. The accompanying hosts reuse the vanilla peaceful
colony-visit behavior and leave naturally after a short stay.

## Separation from therapeutic opportunities

A medical-support delivery:

```text
does not require a sick pawn
does not spawn a free symbiote
does not request a permanent symbiosis
does not alter Tok'ra trust directly
```

## Current limits

The Tok'ra faction remains hidden and disconnected from normal world
settlements, traders, vanilla goodwill and quests. Since `0.1.54-dev`, trusted relations also increase the storyteller weight
of future deliveries moderately. The first trusted-tier advanced medicine package was added in `0.1.56-dev`.
Future milestones may add trade access, quest rewards or broader diplomatic
services.
