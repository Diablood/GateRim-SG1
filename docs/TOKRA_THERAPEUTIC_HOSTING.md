# Tok'ra therapeutic hosting

## Status
Implemented prototype — `0.1.44-dev`, expanded in `0.1.46-dev`.

## Purpose
An active Tok'ra symbiote now provides broad biological healing that follows
RimWorld health semantics more closely than a small lore-only allowlist.

The system remains deliberately conservative around permanent damage. It heals
conditions that RimWorld marks as curable by an item and progressively repairs
non-permanent injuries, but it does not erase scars or recreate missing body
parts.

## Eligibility
The periodic treatment component runs only for pawns with:

- the active adult-host Hediff `SG1_GoauldHostSymbiote`;
- persistent symbiote origin `Tokra`.

Goa'uld hosts remain excluded even though they use the same adult-host Hediff.

## Dynamic treatment filter
Every `60` ticks, the component examines visible Hediffs on eligible hosts.

A visible non-injury condition is treatable when RimWorld marks its definition
as harmful and with `everCurableByItem`, unless an explicit exclusion applies.

A non-permanent `Hediff_Injury` is regenerated progressively at `0.05` severity
per scan. It is not erased instantly, so Tok'ra hosts remain vulnerable during
combat.

Repeated labels are aggregated in feedback. For example, asthma affecting both
lungs is displayed as `asthma x2` and both Hediffs are removed.

## Explicit exclusions
The prototype does not remove:

- permanent scars;
- missing or amputated body parts;
- added body parts, implants or prostheses;
- addictions, withdrawals or dependencies;
- pregnancy-related Hediffs;
- GateRim SG-1 state Hediffs whose `defName` starts with `SG1_`;
- invisible internal state Hediffs.

Advanced scar or limb regeneration may be studied later as a separate,
explicitly balanced mechanic if the lore and gameplay justify it.

## Voluntary therapeutic implantation
The free Tok'ra symbiote action added in `0.1.45-dev` reuses the same dynamic
filter. A pawn with asthma, carcinoma or another compatible curable condition
can therefore be selected for therapeutic implantation.

## Recommended tests
- Add asthma to both lungs of a Tok'ra host: both Hediffs must disappear.
- Add a fresh wound: severity must decrease progressively.
- Add a permanent scar: it must remain.
- Remove a body part: it must remain missing.
- Add carcinoma to a Goa'uld host: it must remain.
- Use a free Tok'ra symbiote against a pawn with asthma only: therapeutic target
  selection and explicit confirmation must remain available.
