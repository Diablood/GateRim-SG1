# Prim'ta larva preservation prototype

## Scope of 0.1.30-dev

This milestone makes physical Prim'ta larvae perishable through RimWorld's
vanilla rottable component.

## ThingDef

```text
SG1_PrimtaLarva
```

## Preservation rule

The item now uses:

```xml
<thingClass>ThingWithComps</thingClass>
<tickerType>Rare</tickerType>
<comps>
    <li Class="CompProperties_Rottable">
        <daysToRotStart>6</daysToRotStart>
        <rotDestroys>true</rotDestroys>
    </li>
</comps>
```

## Gameplay result

```text
Prim'ta larva
    ↓ cold storage
preserved

Prim'ta larva
    ↓ warm storage
rots progressively
    ↓ fully rotted
destroyed
```

## Initial balance

| Property | Value |
|---|---:|
| Days to rot | `6` |
| Rot destroys item | Yes |
| Cold storage | Uses vanilla rottable behavior |
| Dedicated custom C# | None |

## Why vanilla rotting first

The current objective is to add a visible preservation pressure without
introducing a custom temperature system.

This keeps the loop simple:

```text
incubate larva
    ↓ store properly
implant Jaffa before spoilage
```

Future refinements can add dedicated living-symbiote temperature ranges,
freezing penalties, incubator storage bonuses or specialized Goa'uld containers.

## Test checklist

1. Build with `build.cmd` so the version displays `0.1.30.0`.
2. Produce or spawn one `Prim'ta larva`.
3. Select the item and confirm rotting / spoilage information appears.
4. Store one larva at room temperature.
5. Confirm rot progresses.
6. Store another larva in a cold room or freezer.
7. Confirm preservation improves compared with room temperature.
8. Let a warm larva fully rot.
9. Confirm it is destroyed.
10. Produce a fresh larva and implant it into a compatible Jaffa.
11. Confirm the medical bill still consumes the larva normally.
