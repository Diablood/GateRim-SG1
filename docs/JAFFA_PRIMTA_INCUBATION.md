# Prim'ta larva acquisition prototype

## Scope of 0.1.28-dev

This milestone adds the first player-usable Prim'ta larva acquisition loop
without developer spawning.

## Worktable

```text
SG1_PrimtaIncubationBasin
```

Player-facing label:

```text
Prim'ta incubation basin
```

French label:

```text
bassin d'incubation du Prim'ta
```

## Bill

```text
SG1_IncubatePrimtaLarva
```

Player-facing label:

```text
incubate Prim'ta larva
```

French label:

```text
incuber une larve de Prim'ta
```

## Current balance

| Property | Value |
|---|---:|
| Basin size | `2 × 1` |
| Basin cost | `80` steel and `8` gold |
| Work to build | `1600` |
| Incubation work amount | `1800` |
| Work type | Handling |
| Work skill | Animals |
| Minimum skill | `4` |
| Product | `1 × SG1_PrimtaLarva` |
| Raw meat input | `10` units |
| Accepted category | `MeatRaw` |

## 0.1.29-dev nutrient requirement

After validating the ingredient-free loop in `0.1.28-dev`, incubation now
requires:

```text
10 units of raw meat
```

The recipe accepts the `MeatRaw` category and allows mixed raw-meat stacks.

This is the first biological-input balance pass. Temperature, preservation,
nutrition-value calculation, maturation time and faction-specific access remain
future milestones.

## Current flow

```text
construct Prim'ta incubation basin
    ↓ add incubate Prim'ta larva bill
provide 10 units of raw meat
    ↓ colon performs Handling work with Animals 4+
physical SG1_PrimtaLarva item
    ↓
Jaffa implantation surgery
```

## Test checklist

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Open the Bills tab.
4. Add `incubate Prim'ta larva`.
5. Provide at least `10` units of raw meat.
6. Let an eligible colonist complete the work.
7. Confirm the meat is consumed.
8. Confirm one physical `Prim'ta larva` appears.
9. Confirm the larva can be hauled and stacked.
10. Implant it into a compatible Jaffa.
11. Confirm the larva is consumed by surgery.
12. Save and reload after production and after implantation.
13. Confirm the item and Hediff persist as expected.


## 0.1.28-dev-r1 work-giver registration fix

The incubation basin now has a dedicated bill scanner:

```text
SG1_DoBillsPrimtaIncubation
```

The `WorkGiverDef` uses:

```text
WorkGiver_DoBill
Handling work type
SG1_PrimtaIncubationBasin as fixed bill giver
```

This registration is required for automatic work selection and direct manual
prioritization.

The recipe also declares:

```text
requiredGiverWorkType = Handling
workSkill = Animals
Animals skill requirement = 4
```

The earlier Intellectual assignment represented a laboratory-style placeholder.
Handling is a better fit for the current living-larva prototype.
