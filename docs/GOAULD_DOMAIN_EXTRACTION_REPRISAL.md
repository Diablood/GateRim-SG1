# Goa'uld domain extraction ultimatum and reprisal

Version: `0.3.56-dev`

## Purpose

This milestone adds the first player choice to the cause-driven Goa'uld domain
reaction introduced in `0.3.55-dev`. It does not add a mission, offer
catalogue, trust meter or generic hidden grievance score.

The cause is a successful `SG1_ExtractActiveGoauldSymbiote` surgery on a player
home map. The persistent symbiote data identifies the exact System Lord domain
that considers the extraction an affront.

## Reaction contract

- ultimatum: immediate and open for `60000` ticks, or one day;
- demand: surrender the exact living symbiote pawn produced by extraction;
- compliance: remove that pawn, avert the raid and begin cooldown;
- postponement: close the dialog while retaining the letter and timeout;
- custody: refresh anesthesia while the choice remains unresolved;
- demanded-pawn death: resolve immediately as defiance;
- refusal or expiration: announce and schedule the existing reprisal;
- reprisal delay: `60000` to `180000` ticks, or one to three days;
- strength: vanilla threat points captured when extraction succeeds;
- consequence: existing `SG1_GoauldJaffaNaturalRaid`;
- arrival: existing forced `EdgeWalkIn` contract;
- stacking: one active ultimatum or pending reprisal per domain;
- cooldown: `900000` ticks, or 15 days, after resolution.

The shared raid worker now honors an explicitly supplied Goa'uld faction. Its
old fallback still resolves or creates a faction for legacy controlled paths.

## Persistence and migration

`GameComponent_GoauldDomainReprisalTracker` stores deep reaction states with a
Faction reference, target map ID, ultimatum and raid due ticks, point snapshot,
visible cause and a Scribe reference to the demanded pawn. The custom
`ChoiceLetter_GoauldExtractionUltimatum` stores the same domain reference.

The existing `pending` field remains the reprisal flag. Therefore a
`0.3.55-dev` save containing an announced raid loads without migration or
loss; all ultimatum fields default to inactive.

Generated Goa'uld hosts now record their faction in the symbiote identity when
created. Existing generated hosts are backfilled only while they still belong
to an unambiguous System Lord faction; no guessed domain is assigned after that
information has already been lost.

## Choice boundaries

The surrendered pawn is destroyed with `DestroyMode.Vanish` to represent the
handover. If it dies or is destroyed first, the demand resolves immediately as
defiance. If it is merely unspawned or no longer on the target colony map,
compliance is disabled with a localized reason while defiance remains
available.

The tracker checks active choices every `250` ticks. A living demanded pawn on
the colony map keeps an `Anesthetic` severity of at least `1` until resolution.
If it dies or is destroyed, the tracker removes the choice letter and schedules
the reprisal immediately. All reprisal warnings display the randomized delay
through `ToStringTicksToPeriod()`.

No silver, goodwill or invented currency is exchanged. Domain-specific demand
variations remain deferred until this shared loop is stable.

A failed surgery creates no reaction while the active host survives. If the
failure immediately kills that host, the domain announces a direct reprisal:
there is no surviving extracted pawn and therefore no surrender choice.

## Required validation

Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Reset, create an extraction ultimatum and postpone it.
2. Let at least `3000` ticks pass and verify the demanded pawn remains
   anesthetized.
3. Use `Kill demanded symbiote`; verify immediate defiance, the displayed raid
   delay and one pending reaction.
4. Trigger the reprisal and verify a domain-aligned edge-arrival raid.
5. Reset, create and expire another ultimatum; verify its displayed delay.
6. Inspect `Player.log`.

Optional integration test: complete the real active-host extraction surgery on
a secured Goa'uld prisoner and verify that it schedules the same reaction
without using the debug scheduler.

Validation status for final `0.3.56-dev-r5`: passed. Surrender, refusal,
postponement, expiration, persistence, demanded-pawn death, fatal surgery,
delayed raid timing, former-host faction release and `Player.log` are accepted.
