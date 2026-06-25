# Goa'uld extraction surgeries

## Current medical paths

GateRim SG-1 now exposes two separate RimWorld medical operations:

| Recipe | Host state | Purpose |
|---|---|---|
| `SG1_EmergencyExtractGoauldSymbiote` | `SG1_GoauldRecentImplantation` | remove the symbiote during the one-day intervention window |
| `SG1_ExtractActiveGoauldSymbiote` | `SG1_GoauldHostSymbiote` | attempt removal after Goa'uld control is already established |

Both recipes use normal bills, beds, doctors, medicine and RimWorld surgery outcomes. The exact persistent `GoauldSymbioteData` object moves back into the generated free symbiote pawn only after the surgery succeeds and a valid nearby spawn cell has been secured.

## Shared transaction flow

`Recipe_ExtractGoauldSymbioteBase` owns the common extraction sequence:

1. validate the required host Hediff and persistent symbiote component;
2. run `Recipe_Surgery.CheckSurgeryFail(...)`;
3. generate the correct free symbiote PawnKind from the stored origin;
4. initialize it with the same `GoauldSymbioteData`;
5. place the free pawn near the patient;
6. only after placement succeeds, release hostile host control, detach the identity and remove the host Hediff.

If pawn generation or placement fails, `CancelTransferOut()` leaves the original host state authoritative. The operation must not create a second identity or silently remove the parasite.

## Emergency recent-implantation surgery

```text
SG1_EmergencyExtractGoauldSymbiote
```

| Property | Value |
|---|---:|
| Work amount | `1800` |
| Required Medicine | `6` |
| Medicine | `1` unit |
| Surgery factor | `0.85` |
| Death chance on failed surgery | `0.02` |
| Valid state | recent implantation |
| Goa'uld/Tok'ra origin | both remain supported |

This path preserves the established one-day rescue window and its previous balance.

## Active-host extraction in `0.3.41-dev`

```text
SG1_ExtractActiveGoauldSymbiote
```

| Property | Value |
|---|---:|
| Work amount | `4200` |
| Required Medicine | `10` |
| Medicine | `3` units |
| Surgery factor | `0.75` |
| Death chance on failed surgery | `0.05` |
| Valid state | active adult Goa'uld host |
| Patient access | player-controlled or colony prisoner |
| Tok'ra | excluded |

The operation deliberately requires the hostile former colon to be neutralized and captured first. It is not available on an uncontrolled hostile pawn in the field. Once captured, the host is released from the dedicated takeover assault and the periodic behavior check will not assign a colony prisoner back to that Lord.

### Successful hostile-host extraction

```text
captured active Goa'uld host
    ↓ difficult surgery succeeds
same host pawn restored to displaced faction
    +
dedicated takeover assault removed
    +
same hostile Goa'uld symbiote spawned nearby under anesthesia
```

The host retains its ThingID, name, body, age, injuries, equipment, relations, xenotype and backstories. The extracted symbiote retains its ID, name, origin, current/previous host history and recorded Goa'uld faction allegiance.

The symbiote receives vanilla anesthesia after placement. This prevents immediate reimplantation in the patient or surgeon while preserving the danger when it later wakes.

The operation does not kill the Goa'uld automatically and `0.3.41-dev` adds no dedicated containment path. The colony must secure or eliminate the symbiote before recovery if it does not want to face the same hostile threat again. A possible containment design is recorded only as an unplanned question in `docs/IDEAS_TO_REVISIT.md`.

### Failed active-host extraction

Vanilla surgery failure consequences apply first. If the patient survives, the active host Hediff, hostile faction, persistent symbiote data and takeover state remain in place. No free symbiote is generated.

## Immediate extraction command

`HediffComp_GoauldEmergencyExtraction` remains as a deterministic regression tool for the recent-implantation state. From `0.3.41-dev`, its gizmo appears only when RimWorld developer mode itself is enabled. Advanced GateRim SG-1 diagnostics remain read-only. The normal player path is surgery.

## Required regression rules

- never offer active Goa'uld extraction on a Tok'ra host;
- never offer it on recent implantation;
- never offer it on an uncontrolled hostile active host;
- never remove the host Hediff before the free pawn has been placed successfully;
- never generate a replacement host pawn;
- preserve the displaced faction and remove the takeover Lord before returning player control;
- preserve the extracted symbiote's original faction allegiance;
- keep recent emergency surgery behavior and identity transfer stable;
- verify success, failure and save/reload with the patient captured as a prisoner.
