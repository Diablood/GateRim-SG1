# Tok'ra tretonin-support gifts

## Status

Prototype introduced in `0.1.52-dev`.

## Purpose

Add a first lightweight material benefit to the hidden Tok'ra trust system
without introducing a trader, a quest reward or full faction diplomacy.

## Tier behavior

```text
wary        : no support gift
neutral     : no support gift
cooperative : 1 physical SG1_TretoninDose
trusted     : 2 physical SG1_TretoninDose
```

The gift is computed once when an escorted therapeutic opportunity is created.
The stack is placed near the Tok'ra arrival point and generates localized player
feedback plus a `GR_Log` line.

## Outcome behavior

The support stack remains on the map regardless of whether the player accepts,
refuses or ignores the therapeutic offer. This is intentional: Tok'ra trust
represents willingness to provide humanitarian support. Refusal and expiration
still reduce the trust score, naturally preventing repeated low-trust farming.

## Current limits

The support gift currently uses only physical tretonin doses. Future milestones
may add medical envoys, special equipment, trade access or quest rewards.
