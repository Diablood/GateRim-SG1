# Goa'uld threat progression audit

Version: `0.3.53-dev`

## Goal

Existing Goa'uld combat content must follow RimWorld's normal threat progression
before any new doctrine becomes natural. Colony wealth, pawn strength and the
active storyteller difficulty remain owned by RimWorld. GateRim SG-1 consumes
the resulting threat points and applies only documented encounter factors.

No code path is specialized for a particular storyteller. A future GateRim
SG-1 storyteller and compatible vanilla or modded storytellers must all pass
through the same vanilla point APIs.

## Audit result

| System | Previous behavior | `0.3.53-dev` behavior |
| --- | --- | --- |
| Natural Jaffa raid | Usually received vanilla incident points; missing points fell back to `500` | Preserves supplied points and resolves a missing value from the current vanilla storyteller |
| Controlled raid doctrines | Explicit test points or a `500`-point fallback | Unchanged developer fallback; the new menu can supply current, `300` or `4000` points explicitly |
| Intercepted Tok'ra warning | Always stored `500` points | Stores the vanilla storyteller points when the warning is created and uses the same snapshot when the raid arrives |
| Free-symbiote incursion | Vanilla points mapped to one through four symbiotes | Unchanged intentional biological cap; one symbiote can create a persistent hostile host and is not equivalent to one ordinary raider |
| Combat mission sites | Vanilla point snapshots were reduced by encounter factors and then capped for early play | Encounter factors and minimums remain, but combat ceilings no longer flatten advanced colonies |
| Relay sabotage site | Calculated points from an empty temporary map, capped defenders at eight and reinforcements at three, then selected a random fixed layout | Stores points on the world site before travel; defender and reinforcement budgets keep scaling; bunker, split station and walled courtyard are selected by threat tier |
| Goa'uld settlements | Vanilla `Settlement` pawn-group and map-generation budgets | Unchanged; settlement growth remains owned by RimWorld and is covered by regression testing |

The manual mission groups keep a minimum count so low-point encounters cannot
generate an empty objective. They no longer apply a custom maximum count. Their
guard cadence now matches the faction Combat group weighting more closely:
roughly four warriors for one guard.

All Goa'uld/Jaffa raids routed through the shared worker explicitly use vanilla
`EdgeWalkIn`. High point budgets must never unlock drop-pod arrival modes: pods
are mechanically valid for RimWorld but do not match the current GateRim SG-1
lore or the absence of a functional Stargate transport layer.

## Relay tiers

The relay uses the mission defender budget after the `0.80` encounter factor:

| Defender points | Layout |
| ---: | --- |
| below `600` | command bunker |
| `600` to `1399` | split relay station |
| `1400` and above | walled relay courtyard |

Reinforcements use `25%` of that defender budget with a one-pawn minimum. The
site serializes the original vanilla point snapshot so travel time, temporary
map wealth and save/reload do not silently change its intended difficulty.

## Deliberately deferred

- Natural abduction and destruction doctrines remain disabled.
- Goa'uld demands and ultimatums are approved as a direction but need their own
  design and implementation milestone.
- Rival-domain reports, territorial expansion and settlement destruction are
  approved as a direction but remain deferred until discreet anti-collapse
  safeguards are designed.
- The future full texture pass remains separate.

## Developer access

Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

`Show current progression` reports the current vanilla points, raid snapshot,
symbiote count, relay defender/reinforcement budgets and expected relay layout.
The same menu can force current direct, abduction and destruction doctrines,
plus explicit `300`-point and `4000`-point direct raids.

## Required in-game validation

1. Open `Show current progression` and record the displayed vanilla points.
2. Save the game, force the `300`-point direct raid and note its approximate
   size.
3. Reload that save, force the `4000`-point direct raid and confirm that it is
   substantially larger.
4. Reload between tests and force each current-point doctrine. Confirm that
   direct assault, abduction and destruction keep their distinct behavior and
   use a force consistent with the current report.
5. Create an intercepted threat through the exact Tok'ra debug submenu and
   confirm in `Player.log` that the scheduled and triggered point values match
   the current snapshot rather than always showing `500`.
6. Reveal a decoded relay site, enter it normally with a caravan and compare
   its garrison and layout with the earlier progression report.
7. Save/reload before entering a second time when possible and verify that the
   stored difficulty does not change.
8. Inspect `Player.log` for new C#, XML, Scribe, pawn-generation or Lord errors.

Revision `r1` validated threat scaling and doctrine behavior but exposed a
high-point vanilla drop-pod arrival. Revision `r2` must repeat the advanced and
current-doctrine raids and confirm edge arrival without any transport pod.

Final validation passed on `r2`: the advanced direct raid, controlled
abduction/destruction doctrines and intercepted raid use edge arrival without
pods, while the previously accepted scaling remains unchanged.

Optional settlement regression: compare an attacked Goa'uld settlement from a
low-wealth save with another settlement from an advanced save under the same
storyteller settings. Vanilla `Settlement` generation must produce a clearly
stronger base rather than a repeated fixed garrison.
