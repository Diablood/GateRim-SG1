# Tok'ra organic intelligence recovery

## Purpose

This milestone adds a second preliminary organic Tok'ra operation so the scheduler introduced in `0.2.48-dev` can select between genuinely different activities. It also makes the persisted anti-repetition weight visible in normal play without turning opportunities into a fixed alternating sequence.

The player-facing objective is a sealed **Tok'ra intelligence module**, not a material cache or a loot container. Internal `DeadDrop` identifiers remain unchanged only to preserve save compatibility.

## Player flow

1. The organic-operation scheduler waits for a hidden trust-dependent delay.
2. It may open a cautious intelligence-recovery offer on a player home map with a powered Tok'ra secure communicator.
3. Ignoring the offer simply lets the channel close; trust does not change.
4. An Intellectual-capable colon accepts the request by right-clicking the communicator.
5. Only after acceptance, a sealed intelligence module is delivered through the same established routing and placement flow already used by other Tok'ra deliveries and caches:
   - beside or on the dedicated Tok'ra delivery drop zone when one exists;
   - beside a powered Tok'ra secure communicator only when no delivery zone exists;
   - at a reachable and unfogged map edge only when neither preferred delivery target exists.

The shared delivery utility is left unchanged; the operation resolves its preferred anchor first and then uses vanilla near-placement exactly like the validated delivery incidents.
6. An Intellectual-capable colon right-clicks the module and spends a short time checking its integrity, extracting its encrypted data and transmitting it to the Tok'ra cell.
7. The module vanishes and the operation completes immediately.

There is no second communicator action, no recovered inventory object and no material reward.

## Outcomes

### Success

- `+2` Tok'ra trust.
- `200` Intellectual XP for the colon who secures the module.
- The module is removed.
- A new hidden delay is scheduled.

### Ignored offer

- No trust change.
- No module is delivered.
- A new hidden delay is scheduled after the offer closes.

### Accepted failure

An accepted recovery fails when:

- the roughly `36`-hour recovery window expires;
- the module is destroyed;
- the module, its map or its saved reference is lost before completion.

Failure applies `-1` Tok'ra trust exactly once, removes any remaining module and schedules the next hidden opportunity.

## Selection and anti-repetition

Both current archetypes are compatible at all four Tok'ra trust tiers:

| Tier | Observation | Intelligence recovery |
|---|---:|---:|
| Wary | 0.60 | 0.35 |
| Neutral | 1.00 | 0.85 |
| Cooperative | 0.85 | 1.00 |
| Trusted | 0.35 | 0.55 |

When multiple archetypes are available, the last offered archetype has its weight multiplied by `0.25`. Repetition remains possible, but another compatible operation becomes much more likely.

## Physical objective rules

The intelligence module:

- is spawned by the operation and cannot be constructed;
- cannot be minified, hauled or deconstructed;
- has no market value;
- yields no resources when destroyed;
- remains physically destructible;
- uses the existing Tok'ra coded-intelligence packet texture;
- exposes only the operation-specific right-click action while it is active.

If no valid delivery-zone, communicator or reachable edge location can be found, acceptance is rejected without changing the offer state or trust.

## Save compatibility

The tracker persists:

- archetype and state;
- active map;
- offer and operation deadlines;
- direct reference to the active intelligence module;
- last offered and completed archetypes;
- completion, failure and expiry counters.

Existing `0.2.48-dev` observation saves contain no module reference and continue through the original flow. Internal enum, Def and class names containing `DeadDrop` are retained to avoid unnecessary save migration.

## Out of scope

This milestone does not add:

- loot or inventory contents;
- a raid or automatic hostile response;
- a world site;
- trade, recruitment or medical support;
- changes to manual Trusted-tier communicator requests;
- changes to the playable relay sabotage mission.
