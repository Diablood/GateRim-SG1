# Jaffa Prim'ta prototype

## Definition

```text
SG1_JaffaPrimta
```

## Scope

`0.1.13-dev` separated inherited Jaffa lineage traits from the biological
support supplied by an immature Goa'uld symbiote.

`0.1.26-dev` adds the first planifiable medical implantation prototype.

## Current effects

| Modifier | Current value |
|---|---:|
| `ImmunityGainSpeed` | `×1.5` |
| `InjuryHealingFactor` | `×1.5` |
| `IncomingDamageFactor` | `×0.9` |
| `LifespanFactor` | `×1.5` |
| Pain factor | `×0.85` |

## Current procedure

A compatible Jaffa can receive:

```text
implant Jaffa Prim'ta
```

from the health-tab operation menu.

The procedure requires one medicine, one physical `SG1_PrimtaLarva` and a doctor with Medicine `4+`.

## Compatibility

The operation requires the inherited Jaffa lineage, pouch-potential and
immature-symbiote-compatibility genes. A second Prim'ta cannot be added while one
is already present.

## Future work

- apply age or life-stage checks;
- represent the full ceremony;
- add dependency after removal;
- add tretonin as a substitute;
- differentiate Free Jaffa treatment from Goa'uld-controlled implantation.


## Physical larva resource

Since `0.1.27-dev`, Jaffa implantation consumes one physical:

```text
SG1_PrimtaLarva
```

The first item prototype is haulable and stackable. Natural acquisition and
maturation remain future work.
