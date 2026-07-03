# Genetics model

## RimWorld distinction

RimWorld Biotech distinguishes:

| Type | Code concept | Inherited at birth | Typical use |
|---|---|---|---|
| Germline genes | Endogenes | Yes | Stable lineage or naturally adapted xenotype |
| Xenogenes | Xenogenes | No | Genes acquired during life through implantation |
| Hediff-driven effects | Health conditions | No | Temporary or removable biological states |

## GateRim SG-1 mapping

| GateRim content | Representation |
|---|---|
| Jaffa lineage | Inheritable germline xenotype |
| Jaffa Prim'ta compatibility | Germline genes |
| Functional pouch and immature symbiote | Persistent `SG1_JaffaPrimta` Hediff prototype |
| Effects granted by the immature symbiote | Stage modifiers on `SG1_JaffaPrimta` |
| Goa'uld adult possession | Non-inheritable prototype, later a persistent host state |
| Tok'ra adult symbiosis | Future voluntary host state |

## 0.1.13-dev split

`SG1_Jaffa` now contains only inherited lineage genes:

```text
SG1_JaffaLineage
SG1_JaffaPouchPotential
SG1_JaffaSymbioteCompatibility
SG1_JaffaPhysiology
```

The following benefits move to the persistent Prim'ta Hediff prototype:

```text
immunity gain
injury healing
pain reduction
damage resistance
lifespan factor
```

## Save compatibility

`SG1_JaffaLongevity` remains defined as a legacy development gene so older test saves do not lose the referenced `GeneDef`. Newly generated Jaffa no longer receive it from the xenotype.

## Reproduction tests still required

Observe:

```text
Jaffa × Jaffa
Jaffa mother × baseliner father
Baseliner mother × Jaffa father
Jaffa × another germline xenotype
```

Do not add forced maternal inheritance until vanilla hybrid behavior has been evaluated in game.

## Acquired naquadah traces

`SG1_NaquadahBlood` is a non-inheritable acquired marker, not a Jaffa lineage
gene and not proof of current possession. Since `0.3.58-dev`, adult Goa'uld and
Tok'ra hosts and Prim'ta carriers receive it automatically, and former hosts
retain it after removal. It is the shared biological eligibility check for the
kara kesh.
