# Jaffa tretonin substitution prototype

## Scope of 0.1.36-dev

This milestone adds the first temporary medical alternative to implanted
Prim'ta support.

## Physical resource

```text
SG1_TretoninDose
```

French label:

```text
dose de trétonine
```

| Property | Value |
|---|---:|
| Stack limit | `25` |
| Mass | `0.02` |
| Market value | `45` |
| Storage category | `SG1_GoauldMedicalProducts` |
| Current acquisition | Developer spawn |

Tretonin is deliberately not placed in vanilla `Medicine`, so colonists do not
consume it as a generic treatment ingredient.

## Medical operation

```text
SG1_AdministerTretonin
```

French label:

```text
administrer de la trétonine
```

| Requirement | Value |
|---|---|
| Compatible Jaffa | Required |
| Biological age | `12+` |
| Implanted Prim'ta | Must be absent |
| Active tretonin substitution | Must be absent |
| Physical tretonin dose | `1` |
| Medicine skill | `2` |

## Temporary Hediff

```text
SG1_TretoninSubstitution
```

The Hediff uses vanilla:

```text
HediffCompProperties_Disappears
```

with:

```text
60000 ticks
```

or one in-game day.

## Dependency behavior

```text
pubertal Jaffa without Prim'ta
    ↓
progressive deficiency

administer one tretonin dose
    ↓
existing deficiency removed immediately
    ↓
dependency suppressed for one day

treatment expires
    ↓
dependency starts again during the next hourly scan
```

## Scope boundaries

This prototype does not yet add:

```text
natural tretonin production
automatic administration
drug-policy integration
tolerance or side effects
caravan progression
Free Jaffa faction supply
```

## Test checklist

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `12+` without Prim'ta.
3. Confirm dependency appears after the normal hourly scan.
4. Spawn one `tretonin dose` through developer tools.
5. Open the health-tab operation menu.
6. Schedule `administer tretonin`.
7. Confirm one physical dose is consumed.
8. Confirm the dependency disappears immediately.
9. Confirm `tretonin substitution` appears with a remaining-time display.
10. Save and reload.
11. Confirm the Hediff and remaining duration persist.
12. Let the day expire.
13. Confirm the substitution disappears.
14. Wait for the next hourly scan.
15. Confirm the dependency appears again.
16. Confirm a Jaffa with Prim'ta cannot receive tretonin.
