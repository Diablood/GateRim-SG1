# Tok'ra therapeutic-hosting prototype

## Milestone

`0.1.45-dev — Add voluntary therapeutic Tok'ra implantation`

## Scope

Active Tok'ra hosts retain the automatic therapeutic healing behavior
introduced in `0.1.44-dev`. Free Tok'ra symbiotes now also expose a dedicated
therapeutic implantation action.

The action targets nearby player-controlled compatible humanoids affected by at
least one configured serious pathology. After map targeting, a confirmation
dialog asks for explicit consent before the existing persistent implantation
flow starts.

The selected pawn still passes through `SG1_GoauldRecentImplantation`, then the
existing active-host conversion. Once active, the Tok'ra therapeutic-hosting
scan removes matching pathologies every `60` ticks.

Configured pathology DefNames remain deliberately narrow:

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
configured list. It does not add XML-configurable pathology rules, automatic
selection, medical surgery, quests, recruitment, traders or diplomacy.

Generic Tok'ra voluntary implantation remains available for regression tests.
The hard-coded pathology list is temporary.

## Manual regression test

1. Start RimWorld with Core, Biotech and GateRim SG-1.
2. Enable developer mode on a test map.
3. Spawn a free Tok'ra symbiote and a compatible player-controlled humanoid.
4. Add `carcinome` / `Carcinoma` to the humanoid through developer health tools.
5. Select the free Tok'ra symbiote and choose the therapeutic implantation command.
6. Target the sick humanoid and confirm the dialog.
7. Confirm the free symbiote disappears and recent implantation begins.
8. Let the conversion complete.
9. Confirm the active Tok'ra host loses the configured pathology after the therapeutic scan.
10. Repeat with a healthy pawn and confirm therapeutic targeting rejects it.
11. Repeat generic voluntary Tok'ra implantation and Goa'uld workflows as regression tests.

Expected diagnostic shapes:

```text
[GateRim SG-1] Therapeutic Tok'ra implantation transferred Goa'uld symbiote ...
[GateRim SG-1] Tok'ra therapeutic hosting removed ... from ... for symbiote ....
```
