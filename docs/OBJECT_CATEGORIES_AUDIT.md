# Object-category audit

## Purpose

Before final stabilization, inventory every `ThingDef` added by GateRim SG-1 and
review its storage categories.

## Audit checklist

For each object:

1. record the `ThingDef`;
2. record its current `thingCategories`;
3. decide whether a vanilla category remains semantically correct;
4. check for unintended food, recipe, storage or trade interactions;
5. group related mod objects only when a dedicated category improves player readability;
6. harmonize English and French labels;
7. update storage filters and player-wiki pages.

## Families to review

```text
Goa'uld biological products
symbiotes and larvae
ritual objects
Goa'uld technologies
Jaffa weapons and equipment
crafting resources
medicine and tretonin
System Lord faction objects
```

## 0.1.31-dev first correction

`SG1_PrimtaLarva` no longer uses `Manufactured`.

It now uses:

```text
SG1_GoauldBiologicalProducts
```

The custom category is nested under `ResourcesRaw`.

It intentionally does not use `AnimalProductRaw`, because that vanilla category
belongs to the raw-food branch and may be accepted by cooking recipes or broad
modded food filters.

## Future decision

During the final audit, decide whether `SG1_GoauldBiologicalProducts` should
remain a single category or become the root of a deeper tree for larvae,
symbiotes, biological components and related treatments.


## 0.1.36-dev medical-products category

`SG1_TretoninDose` uses:

```text
SG1_GoauldMedicalProducts
```

nested under:

```text
Manufactured
```

The dedicated category prevents tretonin from being treated as generic vanilla
medicine. Re-evaluate this category during the final audit when more Goa'uld
medical resources exist.


## 0.1.37-dev tretonin recipe review

`SG1_PrepareTretoninDoses` intentionally consumes:

```text
SG1_PrimtaLarva
Medicine
```

The larva remains in `SG1_GoauldBiologicalProducts` and the output remains in
`SG1_GoauldMedicalProducts`.

During the final audit, review whether tretonin should keep accepting generic
vanilla medicine or require a dedicated pharmaceutical input.
