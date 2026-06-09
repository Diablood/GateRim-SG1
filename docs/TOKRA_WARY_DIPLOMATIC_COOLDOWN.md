# Tok'ra wary diplomatic cooldown

## Status

Prototype introduced in `0.1.55-dev`.

## Purpose

Give negative Tok'ra trust an immediate but reversible diplomatic consequence
without enabling full vanilla goodwill or permanent lockouts.

## Rules

When a therapeutic-offer outcome leaves Tok'ra trust below `0`, new
therapeutic opportunities are suspended temporarily:

```text
explicit refusal = 3 RimWorld days
expiration without an answer = 5 RimWorld days
```

The longer expiration cooldown preserves the existing design principle that
ignoring an offer is slightly worse than refusing it respectfully.

## Persistence

`GameComponent_TokraTrustTracker` stores the cooldown end tick. Save and reload
preserve the remaining duration.

## Scope

The cooldown blocks only new escorted therapeutic opportunities. Ordinary
peaceful Tok'ra visitors remain available. Independent medical-support
deliveries are already unavailable below cooperative trust.

After the cooldown expires, wary relations still use the existing
`×0.50` storyteller multiplier until trust returns to neutral or above.
