# Genetics model

## RimWorld distinction

RimWorld Biotech distinguishes:

| Type | Code concept | Inherited at birth | Typical use |
|---|---|---|---|
| Germline genes | Endogenes | Yes | Stable lineage or naturally adapted xenotype |
| Xenogenes | Xenogenes | No | Genes acquired during life through implantation |
| Hediff-driven effects | Health conditions | No | Temporary or removable biological states |

## GateRim SG-1 mapping

| GateRim content | Current representation | Intended long-term representation |
|---|---|---|
| Jaffa lineage | Inheritable xenotype | Germline/endogene xenotype |
| Jaffa pouch and latent compatibility | Not yet implemented | Germline gene or dedicated mechanic |
| Effects granted by an immature symbiote | Provisional genes inside `SG1_Jaffa` | Dedicated Jaffa symbiote Hediff |
| Goa'uld adult possession | Non-inheritable `SG1_GoauldHost` prototype | Symbiote entity plus host Hediff/C# state |
| Tok'ra adult symbiosis | Not yet implemented | Voluntary host Hediff/C# state |

## Immediate correction

`SG1_Jaffa` now uses:

```xml
<inheritable>true</inheritable>
```

The current gene set is therefore created as a germline foundation when the xenotype is assigned.

`SG1_GoauldHost` intentionally remains:

```xml
<inheritable>false</inheritable>
```

A Goa'uld host is created during life by implantation and must not produce automatically possessed children.

## Planned refinement

The current Jaffa xenotype still contains provisional bonuses associated with the immature symbiote:

- immunity;
- wound healing;
- reduced pain;
- long lifespan;
- clotting;
- digestive resilience.

As the symbiote system matures, review each effect and move symbiote-dependent bonuses into a removable Hediff. Keep only the inherited Jaffa lineage traits in the germline xenotype.
