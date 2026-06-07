# Emergency Goa'uld extraction prototype

## Scope of 0.1.19-dev

This milestone adds the first player countermeasure during recent implantation.

It intentionally uses a manual Hediff gizmo before introducing medical bills,
doctor skill requirements and surgery-failure risks.

## Current flow

```text
Recent Goa'uld implantation
    ↓ select the implanted host
Emergency extraction command
    ↓
Recent state removed
    ↓
Free Goa'uld symbiote pawn respawned nearby
```

The same persistent `symbioteId` moves back into the free pawn.

## Technical classes

```text
HediffComp_GoauldEmergencyExtraction
HediffCompProperties_GoauldEmergencyExtraction
```

The recent-implantation state receives the new extraction component between:

```text
HediffComp_GoauldSymbiote
HediffComp_GoauldImplantationConversion
```

## Prototype limitation

This is not yet a medical operation.

The current command is immediate and deterministic so reverse identity transfer
can be tested independently from doctor AI, medicine ingredients and surgery
failure handling.

## Manual test checklist

1. Build with `build.cmd`.
2. Implant an adult humanoid using a free Goa'uld symbiote.
3. Record the persistent ID in `recent Goa'uld implantation`.
4. Select the implanted host before the countdown expires.
5. Click `Emergency extraction`.
6. Confirm that the recent state disappears.
7. Confirm that a free symbiote pawn appears nearby.
8. Confirm that the free pawn carries the same persistent ID.
9. Re-implant the same free pawn into an adjacent host.
10. Confirm that the same ID is preserved again.
11. Save and reload after extraction.
12. Confirm that the free pawn retains the same ID.
13. Inspect `Player.log` for extraction lifecycle logs.

## Future medical milestone

Replace or complement the immediate command with a surgery bill:

```text
Emergency Goa'uld extraction surgery
```

That future version should require medical work, apply failure risks and possibly
injure or kill the host.


## 0.1.20-dev medical operation

A real medical operation is now available from the pawn health tab:

```text
emergency Goa'uld extraction
```

The immediate command remains temporarily available as a regression-testing
tool. The surgery is the intended player-facing path.
