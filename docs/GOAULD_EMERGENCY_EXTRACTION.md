# Emergency Goa'uld extraction

## Current role

The recent-implantation state keeps a one-day intervention window before conversion into an active host. The intended player-facing countermeasure is the medical operation:

```text
SG1_EmergencyExtractGoauldSymbiote
```

A successful operation removes the recent host state and returns the same persistent symbiote identity to a free pawn.

## Deterministic developer command

The original immediate `Emergency extraction` gizmo remains useful for identity-transfer regression tests, but it bypasses doctors, medicine and surgery failure. From `0.3.41-dev`, it appears only when RimWorld developer mode itself is enabled. Advanced GateRim SG-1 diagnostics remain read-only and do not expose this action.

It must not appear in normal play.

## Medical operation

The recent-implantation surgery requires:

| Property | Value |
|---|---:|
| Work amount | `1800` |
| Medicine skill | `6` |
| Medicine | `1` unit |
| Surgery factor | `0.85` |
| Death chance on failure | `0.02` |

The normal RimWorld surgery outcome remains responsible for injuries and failure. A failed operation leaves the recent implantation active, so the conversion countdown continues.

## Active-host distinction

The emergency operation applies only during recent implantation. Once the symbiote becomes an active Goa'uld host, the player must first secure the pawn and use the separate, more dangerous operation:

```text
SG1_ExtractActiveGoauldSymbiote
```

See `docs/GOAULD_EXTRACTION_SURGERY.md` for the full balance and transaction rules.
