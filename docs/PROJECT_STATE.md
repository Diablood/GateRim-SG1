# Project state

Current milestone: `0.3.70-dev - Add open-conflict Goa'uld world battlefield site`

- Final local revision: `r4`.
- Build `0.3.70.0`, functional tests, save/reload coverage and required
  regressions are validated.
- Integration, annotated tag publication and wiki synchronization remain.

## Repository state

- Published starting point: `develop` exactly aligned with annotated tag
  `v0.3.69-dev` at commit
  `ee2ae6e82371e5a8f4e92c5c79cb97309c7d8718`.
- Active milestone branch:
  `feature/goauld-open-conflict-world-battlefield-site`.
- Current development version: `0.3.70-dev`.
- Technical assembly version: `0.3.70.0`.
- `main` remains reserved for the future stable `1.0.0` line.
- Local validation is complete; fast-forward integration, tag and publication are
  authorized.

## Implemented scope

- Extend the published `0.3.69-dev` battlefield scheduler instead of adding a
  parallel world-site manager.
- Keep one shared active slot, one recurrence clock, one last-pair memory and
  one text anti-repetition state for local and world battlefields.
- Alternate local and world forms when both are available; fall back to the
  other form when the preferred one cannot be created.
- Select an exact active Goa'uld domain pair whose stored relation is
  `open conflict`.
- Create one temporary world site `6–18` tiles from an eligible player home map.
- Snapshot vanilla storyteller points at site creation and apply the validated
  `0.35`, `250–1800` points-per-detachment contract.
- Keep the site fully optional and available for eight days.
- Expire an ignored site silently without mission failure, goodwill change,
  strategic-relation change, settlement change or territorial consequence.
- Add a dedicated world-map icon and bilingual creation, inspection, travel,
  arrival and failure text.
- Use the normal RimWorld caravan destination and arrival-action flow.
- Use RP-facing world-site inspection text with natural day/hour formatting.
- During an active two-domain assault, distribute a proportional nearby subset
  toward player provocateurs while the remaining Jaffa keep fighting the rival.
- Use RimWorld's vanilla form-caravan world-object component so the encounter
  can be reformed normally after active threats end.
- Generate the encounter map only when a player caravan enters the site.
- Reuse the exact published local-battlefield generation and map component:
  edge arrival, opposing rally points, announced assault, ranged pursuit,
  player retaliation limits, `30%` morale break, two-day battle limit and fixed
  withdrawal deadline.
- Place the player caravan at a map edge separated from both rally points.
- Keep the world object and shared slot while the encounter map exists.
- Remove the map and world object only after the battlefield resolves and no
  player pawn or incoming transporter blocks ordinary map removal.
- Preserve ordinary RimWorld capture, loot and caravan-reformation behavior.
- Add developer actions for forcing a local battlefield, forcing a world site,
  expiring an unvisited site, ordering withdrawal and resetting the scheduler.

## Persistence model

The version-2 global tracker serializes:

- shared check and opportunity ticks;
- active local-map ID;
- active world-object ID;
- last selected domain-pair IDs;
- last battlefield form (`local` or `world`);
- last RP-letter variant;
- storyteller suspension state.

The world object serializes:

- both exact domain faction references;
- the vanilla threat snapshot and points per detachment;
- site expiration and map size;
- launched, resolved and tracker-notified state.

The existing map component now also serializes an optional parent world-site
reference. All pawn, combat, provocation, rally, morale and withdrawal state
remains owned by the same component used by the local incident.

Older `0.3.69-dev` saves retain their local battlefield state. New version-2
fields default to no active world site and no previous world/local alternation.

## Guardrails

- Natural local and world opportunities remain exclusive to
  `Commandement SG-1`.
- Changing storyteller suspends only future opportunity timing; an existing
  local battle or world site remains valid.
- A local battlefield and world site can never occupy the shared slot together.
- World-site expiration is neutral and produces no forced quest failure.
- The milestone changes no domain relation, faction goodwill, settlement,
  territory, raid doctrine, natural-raid frequency or pressure factor.
- No material reward is spawned by the framework; all recoverable equipment is
  ordinary battlefield equipment.

## Validation result

Final local revision `r4` is validated:

- forced build succeeds with assembly `0.3.70.0`;
- the world site is created for the exact stored open-conflict pair;
- the dedicated icon, RP inspection text and day/hour countdown work;
- normal caravan routing, save/reload and automatic arrival work;
- the encounter map is generated only at arrival;
- both forces enter, rally and conduct their mutual assault correctly;
- player attackers are treated as additional enemies without making the whole
  camp forget the rival detachment;
- morale break, battle deadline and withdrawal limits work;
- vanilla caravan reformation, prisoner and loot selection work;
- ignored-site expiration is neutral;
- the shared local/world slot and alternation remain coherent;
- existing local-battlefield and Goa'uld regressions pass;
- `Player.log` is clean.

## Next step

After publication of `0.3.70-dev`, the selected milestone is:

`0.3.71-dev - Standardize player-facing duration formatting`

Planned branch:

`feature/standardize-duration-formatting`

The milestone must audit all player-facing duration displays, prefer RimWorld's
vanilla formatting helpers, use appropriate minutes, hours, days, quadrums or
years, retain raw ticks only in developer reports and change no actual timing or
balance.