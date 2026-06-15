# 0.2.37-dev - Tok'ra intercepted threat intelligence

## Goal

Give the Tok'ra tactical assessment channel a clear gameplay purpose: it can now help the player prepare for a threat that is not visible yet.

## Player-facing behavior

A rare Tok'ra interception can occur once the colony has trusted Tok'ra contact.

When it fires:

- a letter warns that a Goa'uld/Jaffa force is approaching;
- a persistent right-side alert appears;
- the alert tooltip gives the estimated approach window and a short preparation warning;
- the alert disappears automatically when the attack starts;
- the attack is delayed by roughly 1 to 3 RimWorld days.

## Tactical assessment integration

The Tok'ra communicator's tactical assessment request can now be used in two contexts:

- an active hostile threat already present on the map;
- a Tok'ra-intercepted threat still approaching.

For an intercepted threat, the assessment reveals:

- probable signature;
- probable intent;
- estimated arrival window;
- broad threat severity;
- a short Tok'ra preparation recommendation.

## Limits

This system does not add:

- Tok'ra reinforcements;
- items;
- healing;
- trade;
- recruitment;
- a reward;
- a world site.

The Tok'ra provide intelligence only. The colony must prepare and handle the attack.

## Testing support

Developer tools include actions to create or clear a Tok'ra-intercepted threat for quick validation.

## 0.2.37-dev-r2

The debug action now sends the same visible warning letter as the natural Tok'ra interception incident, while keeping the short test delay.

