# Tok'ra safehouse lead tracker baseline

Version: `0.2.14-dev`

## Purpose

This milestone adds a saved progression bridge between the letter-only
safehouse signal and a future hidden Tok'ra world-site prototype.

A signal can now store a persistent safehouse lead without creating a site yet.

## Component

```text
GameComponent_TokraSafehouseLeadTracker
```

Saved value:

```text
tokraSafehouseLeadCount
```

## Limits

```text
MaximumSafehouseLeads = 3
SafehouseSignalLeadGain = 1
```

The cap is provisional. It prevents repeated signals from stockpiling unlimited
future site opportunities before the site system exists.

## Signal integration

A successful `SG1_TokraSafehouseSignal` now does two things:

```text
+1 Tok'ra trust
+1 Tok'ra safehouse lead
```

The event still creates no object, world site, pawn, caravan, loot or raid.

## Future use

Since `0.2.15-dev`, one stored lead can already be consumed by `SG1_TokraSafehouseLeadCache` for a modest medical cache. A later milestone can consume one or more stored leads to create:

```text
temporary hidden safehouse site
limited Tok'ra cache site
clandestine meeting site
```

## Manual test checklist

1. Force `SG1_TokraSafehouseSignal`.
2. Confirm Tok'ra trust increases by `+1`.
3. Confirm safehouse leads increase by `+1`.
4. Save and reload.
5. Force another signal and confirm the previous lead count persists.
6. Repeat until the count reaches `3/3`.
7. Confirm further signals do not exceed `3/3`.
8. Confirm no world site or object is created.


## World-marker follow-up

Since `0.2.16-dev`, one stored lead can also be consumed to create a temporary
non-enterable hidden Tok'ra safehouse marker on the world map.

This is the first world-map footprint for the safehouse system, but it still
does not generate a map or rewards.
