# Prim'ta preservation basin prototype

## Status

Implemented as a powered dedicated-storage prototype in `0.1.59-dev`.

## Design goal

The Prim'ta preservation basin provides a specialized biological-storage option
without removing RimWorld refrigerator and freezer gameplay.

```text
powered preservation basin
    -> ideal internal stabilization
    -> no additional rot progression

no powered basin
    -> ambient-temperature rules remain active
    -> refrigerator, freezer and heat management still matter
```

## Supported resources

The basin accepts only:

- `SG1_ImmaturePrimtaSymbiote`
- `SG1_PrimtaLarva`

## Important behavior

The basin suspends additional deterioration while powered. It does not repair
rot progress accumulated before storage.

The basin uses a provisional reuse of the Prim'ta incubation-basin texture.

## Next step

Implemented in `0.1.60-dev`: deep-freezing penalties now accumulate outside an
active preservation basin below `-15 °C`. A powered basin protects stored
resources and progressively reduces accumulated exposure.
