# Tok'ra therapeutic-hosting prototype

## Milestone

`0.1.44-dev — Add Tok'ra therapeutic healing prototype`

## Scope

This first isolated prototype adds automatic therapeutic healing to active
Tok'ra hosts without modifying the existing implantation, extraction or
visitor flows.

An active Tok'ra host is identified through the shared
`SG1_GoauldHostSymbiote` Hediff and the persistent adult-symbiote origin stored
in `GoauldSymbioteData`. The same host Hediff is reused intentionally: the
origin value distinguishes Tok'ra symbiosis from Goa'uld possession.

`GameComponent_TokraTherapeuticHosting` scans spawned map pawns every `60`
ticks. For active Tok'ra hosts only, it removes these configured pathology
DefNames when they exist in the loaded game:

```text
Carcinoma
Infection
Plague
Malaria
Flu
SleepingSickness
BloodRot
```

## Deliberate limits

This milestone does not heal injuries, scars or illnesses outside the small
configured list. It does not add therapeutic target eligibility, explicit
medical consent UI, quests, recruitment, traders or diplomacy.

The hard-coded list is temporary. A later iteration should move pathology
rules into configurable Defs and extend compatibility with modded diseases.

## Manual regression test

1. Start RimWorld with Core, Biotech and GateRim SG-1.
2. Enable developer mode on a test map.
3. Spawn `hôte Tok'ra volontaire`.
4. Wait briefly for its active Tok'ra symbiote initialization.
5. Add `carcinome` / `Carcinoma` through the developer health tools.
6. Let the game run for at least `60` ticks.
7. Confirm the pathology disappears and a positive message appears.
8. Add a normal injury and confirm that it remains.
9. Test an active Goa'uld host with the same pathology and confirm that it is not healed.
10. Save, reload and repeat the Tok'ra-host test.

Expected diagnostic shape:

```text
[GateRim SG-1] Tok'ra therapeutic hosting removed ... from ... for symbiote ....
```
