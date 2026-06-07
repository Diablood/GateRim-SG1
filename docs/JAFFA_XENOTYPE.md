# Jaffa xenotype foundation

## Definitions

```text
SG1_Jaffa
SG1_JaffaLineage
SG1_JaffaPouchPotential
SG1_JaffaSymbioteCompatibility
SG1_JaffaPhysiology
SG1_JaffaPrimta
```

## Germline policy

Jaffa are a modified human lineage that reproduces as Jaffa.

The inherited xenotype contains only lineage traits. A child can therefore be born Jaffa without already carrying an immature symbiote.

## Prim'ta policy

The Prim'ta is represented separately by:

```text
SG1_JaffaPrimta
```

This persistent Hediff currently grants:

| Effect | Current factor |
|---|---:|
| Immunity gain | `×1.5` |
| Injury healing | `×1.5` |
| Incoming damage | `×0.9` |
| Lifespan expectancy | `×1.5` |
| Pain | `×0.85` |

## Prototype limitations

The XML-only version does not yet implement:

- age validation;
- the Prim'ta ceremony;
- automatic implantation;
- symbiote dependency;
- tretonin substitution;
- medical consequences after removal;
- nearby Goa'uld sensitivity.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Generate a new Jaffa pawn.
3. Confirm that only the four lineage genes are germline genes.
4. Confirm that the pawn does not automatically receive `Prim'ta symbiote`.
5. Add `SG1_JaffaPrimta` manually with developer mode.
6. Confirm immunity, healing, pain, damage and lifespan modifiers.
7. Remove the Hediff and confirm that the modifiers disappear.
8. Use newly generated pawns for this test; older development pawns may retain legacy genes.
9. Check `Player.log` for `SG1_Jaffa` or `SG1_JaffaPrimta` errors.
