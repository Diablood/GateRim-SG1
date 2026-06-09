# Trusted Tok'ra advanced medicine support

## Status

Prototype introduced in `0.1.56-dev`.

## Purpose

Add a first positive trusted-tier diplomatic reward beyond increased tretonin
quantity and storyteller weighting. Trusted Tok'ra relations now provide one
small advanced medicine package during independent medical-support deliveries.

## Delivery behavior

```text
cooperative trust
    -> 2 tretonin doses
    -> 1 voluntary-host visitor
    -> no advanced medicine

trusted trust
    -> 4 tretonin doses
    -> 2 voluntary-host visitors
    -> 1 vanilla MedicineUltratech unit
```

The additional medicine is placed near the Tok'ra arrival point, remains on the
map after the short peaceful visit and generates bilingual clickable feedback.

## Current limits

The trusted reward is intentionally small. It does not introduce a trader, a
quest, player-triggered requisition or recurring guaranteed delivery. Trusted
relations continue to rely on the existing rare storyteller-selected medical
support incident.
