# Low-frequency natural Tok'ra peaceful visitors

## Scope of 0.1.43-dev

This milestone enables rare storyteller-selected peaceful Tok'ra visits while
keeping the hidden faction disconnected from normal world generation.

Developer tools remain available for immediate controlled tests.

## IncidentDef

```text
SG1_TokraPeacefulVisitors
```

French label:

```text
visiteurs Tok'ra pacifiques
```

## Natural selection settings

```xml
<baseChance>0.10</baseChance>
<earliestDay>15</earliestDay>
<minRefireDays>30</minRefireDays>
```

The first natural Tok'ra visit cannot occur before day `15`. After a successful
visit, the same incident cannot fire naturally again for at least `30` days.

## Trigger methods

Natural storyteller selection:

```text
rare storyteller selection after day 15
    ↓
Tok'ra peaceful visitors
```

Controlled developer test:

```text
Do incident
    ↓
Tok'ra peaceful visitors
```

## Visitor flow

```text
storyteller-selected or developer-triggered incident
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
FactionGenerator.NewGeneratedFaction(...)
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

This milestone still does not enable:

```text
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
3. Confirm no XML error or C# exception appears during loading.
4. Open developer tools.
5. Run:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors
   ```
6. Confirm the neutral French letter no longer contains `(test)`.
7. Confirm `1` to `3` Tok'ra hosts enter from the map edge.
8. Wait up to `60` ticks.
9. Confirm each visitor receives an active adult symbiote with origin `Tok'ra`.
10. Confirm the visitors are not player-controlled.
11. Confirm they leave automatically after their peaceful visit.
12. Save and reload after the first incident.
13. Trigger the incident again.
14. Confirm the hidden Tok'ra faction instance is reused.
15. Confirm no settlement or trader appears.
16. Confirm the free Tok'ra and Goa'uld regression workflows remain valid.
17. During extended balancing, observe at least one natural visit after day `15`.
18. During extended balancing, confirm the `30`-day minimum refire delay.

The immediate smoke test does not require waiting for a natural incident.

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
faction instance hidden and prevents settlement creation during this staged
visitor rollout.

## 0.1.42-dev-r2 backstory-filter cleanup

The Tok'ra faction now uses the modern `backstoryFilters` declaration with the
`Offworld` category. This prevents fallback biography warnings when voluntary
Tok'ra hosts are generated.
