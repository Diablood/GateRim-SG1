# Emergency Goa'uld extraction surgery

## Scope of 0.1.20-dev

This milestone adds a real RimWorld medical operation for recently implanted
Goa'uld hosts.

The immediate debug-style extraction command from `0.1.19-dev` remains
temporarily available for regression testing. The surgery is now the intended
player-facing path.

## Recipe

```text
SG1_EmergencyExtractGoauldSymbiote
```

## Worker

```text
Recipe_EmergencyExtractGoauldSymbiote
```

The worker derives from vanilla:

```text
Recipe_Surgery
```

and uses:

```text
CheckSurgeryFail(...)
```

before attempting the identity transfer.

## Current balance

| Property | Value |
|---|---:|
| Work amount | `1800` |
| Required skill | Medicine `6` |
| Medicine | `1` unit |
| Surgery success factor | `0.85` |
| Death chance on failed surgery | `0.02` |
| Valid state | `SG1_GoauldRecentImplantation` only |
| Active-host extraction | Not yet supported |

## Success flow

```text
recent implantation
    ↓ scheduled medical operation
normal RimWorld surgery outcome check
    ↓ success
same GoauldSymbioteData transferred
    ↓
free symbiote pawn respawned nearby
```

## Failure flow

The normal RimWorld surgery outcome system remains responsible for the failed
operation and its consequences. The recent-implantation state remains present,
so the countdown toward active-host conversion continues.

## Manual test checklist

1. Build with `build.cmd`.
2. Implant an adult colonist or controllable humanoid.
3. Open the pawn health tab.
4. Add the operation:
   ```text
   emergency Goa'uld extraction
   ```
5. Ensure a doctor with Medicine `6+`, a medical bed and medicine are available.
6. Let the operation complete.
7. On success, confirm that recent implantation disappears.
8. Confirm that the same symbiote ID appears on a nearby free pawn.
9. Re-implant the extracted symbiote and verify the same ID.
10. Test a low-quality setup and confirm failed surgery can occur.
11. Confirm that failure leaves recent implantation in place.
12. Inspect `Player.log` for surgery lifecycle logs.

## 0.1.20-dev-r2 visibility fix

The surgery does not target a specific body part. The recipe must therefore
declare:

```xml
<targetsBodyPart>false</targetsBodyPart>
```

Without this field, RimWorld keeps the `RecipeDef` default value (`true`) and
asks the worker for targetable body parts. This worker intentionally exposes no
body-part list, so the operation menu remains empty for this recipe.
