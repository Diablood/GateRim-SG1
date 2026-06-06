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
