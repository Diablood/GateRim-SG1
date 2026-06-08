# Tretonin acquisition prototype

## Scope of 0.1.37-dev

This milestone adds the first player-usable production route for physical
tretonin doses without developer spawning.

## Worktable

The first prototype reuses the vanilla:

```text
DrugLab
```

This avoids introducing an additional placeholder building before the Goa'uld
technology tree is defined.

## Recipe

```text
SG1_PrepareTretoninDoses
```

French label:

```text
préparer des doses de trétonine
```

## Inputs and output

```text
1 physical Prim'ta larva
    +
1 medicine unit
    ↓
vanilla drug lab
    ↓ Intellectual 6+
5 tretonin doses
```

| Property | Value |
|---|---:|
| Work amount | `1800` |
| Skill | Intellectual |
| Minimum skill | `6` |
| Prim'ta larvae consumed | `1` |
| Medicine consumed | `1` |
| Tretonin doses produced | `5` |

## Gameplay choice

One physical larva can now be used in two ways:

```text
implant larva
    ↓
permanent Prim'ta support for one compatible Jaffa

or

process larva at drug lab
    ↓
five one-day tretonin doses
```

This is the first balancing pass. Exact yields and pharmaceutical requirements
can be refined later.

## Scope boundaries

This prototype does not yet add:

```text
specialized Goa'uld laboratory
research project specific to tretonin
automated dose administration
drug-policy integration
industrial extraction upgrades
faction-specific supply chains
Tok'ra assistance
```

## Test checklist

1. Build with `build.cmd`.
2. Build or spawn the vanilla `DrugLab`.
3. Open its Bills tab.
4. Confirm `prepare tretonin doses` is available.
5. Confirm the bill displays Intellectual `6+`.
6. Try without a Prim'ta larva and confirm the bill waits.
7. Try without medicine and confirm the bill waits.
8. Supply one larva and one medicine unit.
9. Complete the bill.
10. Confirm both ingredients are consumed.
11. Confirm exactly five physical tretonin doses appear.
12. Store and stack the produced doses.
13. Use one dose through the existing health-tab operation.
14. Confirm one-day substitution still works.
