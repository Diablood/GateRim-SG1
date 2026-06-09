# Tok'ra peaceful visitor prototype

## Scope of 0.1.42-dev

This milestone adds a developer-triggered peaceful Tok'ra visitor incident
without enabling random world generation.

## IncidentDef

```text
SG1_TokraPeacefulVisitors
```

French label:

```text
visiteurs Tok'ra pacifiques (test)
```

## Trigger method

Use developer tools:

```text
Do incident
    ↓
Tok'ra peaceful visitors (test)
```

The incident declares:

```xml
<baseChance>0</baseChance>
```

so the storyteller cannot select it randomly.

## Visitor flow

```text
developer-triggered incident
    ↓
create or reuse hidden SG1_Tokra faction instance
    ↓
reuse nested Peaceful pawn-group profile
    ↓
spawn 1 to 3 SG1_TokraVoluntaryHost pawns
    ↓
vanilla visit-colony LordJob
```

Each spawned host is initialized by the existing:

```text
GameComponent_TokraHostPrototypeInitializer
```

and receives an active persistent Tok'ra symbiote within `60` ticks.

## Persistent hidden faction instance

The first visit creates one hidden Tok'ra faction instance through:

```text
FactionGenerator.NewGeneratedFaction(SG1_Tokra)
Find.FactionManager.Add(...)
```

Later visits reuse the same saved instance.

The faction remains hidden and does not create a settlement.

## Reused vanilla workflow

The custom worker inherits from:

```text
IncidentWorker_VisitorGroup
```

This preserves the vanilla peaceful visitor behavior:

```text
entry cell
group generation
spawn near map edge
visit-colony LordJob
automatic departure
```

## Current visitor size

```text
1 to 3 pawns
```

The worker chooses:

```text
80
160
or
240 points
```

and the current Tok'ra host costs:

```text
80 points
```

## Scope boundaries

This prototype does not yet enable:

```text
random storyteller visits
visible world faction
settlements
traders
trade stock
diplomacy UI
quests
therapeutic implantation events
queen-origin biology
```

## Test checklist

1. Build with `build.cmd`.
2. Restart RimWorld completely.
3. Open developer tools.
4. Run:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
5. Confirm a neutral letter appears.
6. Confirm `1` to `3` Tok'ra hosts enter from the map edge.
7. Wait up to `60` ticks.
8. Confirm each visitor receives an active adult symbiote with origin `Tok'ra`.
9. Confirm the visitors are not player-controlled.
10. Confirm they leave automatically after their peaceful visit.
11. Save and reload after the first incident.
12. Trigger the incident again.
13. Confirm the hidden Tok'ra faction instance is reused.
14. Confirm no settlement, trader or random storyteller visit appears.
15. Confirm the free Tok'ra and Goa'uld regression workflows remain valid.


## 0.1.42-dev-r1 faction-generator build fix

The first local version called:

```csharp
FactionGenerator.NewGeneratedFaction(GR_DefOf.SG1_Tokra)
```

RimWorld 1.6 expects:

```csharp
FactionGenerator.NewGeneratedFaction(
    new FactionGeneratorParms(
        GR_DefOf.SG1_Tokra,
        default(IdeoGenerationParms),
        hidden: true))
```

The explicit `hidden: true` argument is intentional. It keeps the runtime Tok'ra
faction instance hidden and prevents settlement creation during this controlled
visitor prototype.
