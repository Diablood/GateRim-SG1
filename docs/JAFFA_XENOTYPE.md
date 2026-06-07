# Jaffa xenotype foundation

## Definitions

```text
SG1_Jaffa
SG1_JaffaPhysiology
SG1_JaffaLongevity
```

## Germline policy

Jaffa are a modified human lineage that reproduces as Jaffa.

The xenotype therefore uses:

```xml
<inheritable>true</inheritable>
```

This makes the current xenotype gene set a germline/endogene foundation instead of a set of acquired xenogenes.

Two Jaffa parents should produce Jaffa children rather than baseliner children. A mixed pairing can still produce a hybrid according to RimWorld's normal germline inheritance logic.

## Appearance policy

Jaffa must not all look like visibly muscular RimWorld hulks.

The vanilla `Body_Hulk` gene remains excluded because it restricts the visible body shape. No `Body_*` gene is imposed by the Jaffa xenotype.

## Current custom genes

| Gene | Effect | Current purpose |
|---|---:|---|
| `SG1_JaffaPhysiology` | `CarryingCapacity +15` | Inherited physical baseline without forced Hulk appearance |
| `SG1_JaffaLongevity` | `LifespanFactor ×1.5` | Provisional 150% lifespan expectancy |

## Important prototype limitation

Not every current Jaffa bonus should necessarily remain germline-based forever.

Several effects are likely to depend on the immature symbiote and should later move into a removable Jaffa symbiote Hediff:

- part of the immunity bonus;
- accelerated recovery;
- reduced pain;
- longevity;
- symbiote dependency;
- tretonin substitution;
- nearby Goa'uld sensitivity.

The current inheritable xenotype is a gameplay foundation until that split is implemented.

## Temporary artwork

`SG1_JaffaPhysiology` currently uses:

```text
Textures/UI/Genes/SG1_JaffaPhysiology.png
```

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Open the xenotype editor.
3. Confirm that `Jaffa` is marked as inheritable.
4. Confirm that Jaffa genes display as germline genes.
5. Generate or breed two Jaffa parents when practical.
6. Confirm that their child inherits the Jaffa lineage rather than becoming a baseliner.
7. Confirm that mixed-lineage offspring behave as RimWorld hybrids.
8. Check `Player.log` for `SG1_Jaffa` errors.
