# Free Goa'uld symbiote prototype

## Definitions

```text
SG1_GoauldSymbiote
```

This milestone adds a free adult Goa'uld symbiote as an XML-only animal-style pawn prototype.

## Scope of 0.1.8-dev

The prototype can be spawned through developer mode for visual and balance testing.

It intentionally does not yet simulate:

- forced implantation;
- ritual implantation;
- the recent-implantation state;
- possession of a humanoid target;
- extraction;
- transfer between hosts;
- water-basin behavior;
- faction events.

## Current balance

| Property | Current value |
|---|---:|
| Move speed | `4.8` |
| Body size | `0.15` |
| Health scale | `0.35` |
| Mass | `2` |
| Bite damage | `3` |
| Combat power | `15` |
| Natural biome spawning | Disabled |

The creature should remain threatening mainly because of its future implantation mechanic, not because of direct melee damage.

## Temporary artwork

The prototype uses a local placeholder sprite:

```text
Textures/Things/Pawn/Animal/SG1_GoauldSymbiote/SG1_GoauldSymbiote.png
```

Dedicated artwork can replace it without changing the XML path.

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Enable developer mode.
3. Open the debug actions menu.
4. Use the pawn-spawning action and select `Goa'uld symbiote`.
5. Confirm that the pawn appears with its temporary sprite.
6. Confirm that it can move and perform a weak bite attack.
7. Confirm that it does not appear naturally in a normal biome.
8. Switch to French and verify the translated label and description.
9. Inspect `Player.log` for errors mentioning `SG1_GoauldSymbiote`.


## 0.1.9-dev maintenance note

The initial XML prototype incorrectly declared:

```xml
<wildness>1</wildness>
```

inside the `RaceProperties` block. RimWorld 1.6 does not expose that field on `RaceProperties`, so the line has been removed.

The pawn remains excluded from natural biome spawning because no biome table references it. Developer-mode spawning remains the intended test method.


## Related implantation prototype

Since `0.1.11-dev`, the temporary Hediff below can be added manually through developer mode:

```text
SG1_GoauldRecentImplantation
```

It represents the critical period after entry into a humanoid host. The free symbiote does not apply it automatically yet.


## Interactive forced implantation

Since `0.1.17-dev`, a selected free symbiote can implant the first compatible adjacent humanoid through a manual command.

The free pawn disappears and its persistent identity moves into `SG1_GoauldRecentImplantation`.

Autonomous attack AI remains future work.
