# Active Goa'uld host conversion

## Scope of 0.1.18-dev

This milestone completes the first possession loop:

```text
Free symbiote
    ↓ manual forced implantation
Recent implantation
    ↓ one-day countdown
Active Goa'uld host
```

## Identity transfer

Immediately before `SG1_GoauldRecentImplantation` expires, the custom disappearing component:

```text
HediffComp_GoauldImplantationConversion
```

moves the same `GoauldSymbioteData` object into:

```text
SG1_GoauldHostSymbiote
```

The temporary state then disappears normally.

`HediffComp_GoauldSymbiote` tracks a `transferredOut` flag so removal of the temporary state does not detach or clear the active symbiote identity.

## Active-host effects

| Modifier | Current value |
|---|---:|
| `ImmunityGainSpeed` | `×1.75` |
| `InjuryHealingFactor` | `×1.75` |
| `IncomingDamageFactor` | `×0.8` |
| `LifespanFactor` | `×5` |
| Pain factor | `×0.7` |

## Genetics rule

Adult possession does not replace the host's germline xenotype.

A baseliner remains a baseliner host. A Jaffa remains a Jaffa host. The Goa'uld is represented by a persistent acquired health state.

## Manual test checklist

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Implant an adjacent adult humanlike pawn.
4. Record the symbiote ID in `recent Goa'uld implantation`.
5. Save and reload during the critical phase.
6. Wait until the one-day countdown expires.
7. Confirm that `recent Goa'uld implantation` disappears.
8. Confirm that `adult Goa'uld symbiote` appears.
9. Confirm that the symbiote ID is unchanged.
10. Confirm the active-host stat modifiers.
11. Confirm that the pawn's original germline xenotype remains unchanged.
12. Save and reload again.
13. Confirm that the same active-host ID remains visible.
14. Inspect `Player.log` for the conversion lifecycle.
