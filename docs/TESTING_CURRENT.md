# Current milestone validation

Jalon: `0.3.70-dev - Add open-conflict Goa'uld world battlefield site`

Branch: `feature/goauld-open-conflict-world-battlefield-site`

Révision locale : `r4`

Status: final revision `r4` validated locally; publication pending.

## Validation result

Final revision `r4` validated:

- build `0.3.70.0`;
- world-site creation, icon and RP inspection;
- normal travel and lazy map generation;
- mutual battle and mixed player/rival targeting;
- morale break and bounded withdrawal;
- vanilla caravan reformation;
- neutral ignored-site expiration;
- shared slot, alternation and persistence;
- existing Goa'uld regressions;
- clean `Player.log`.

## Preconditions

- Start RimWorld with developer mode enabled.
- Select **Commandement SG-1**.
- Use a world containing at least two active Goa'uld System Lord domains.
- Use a player home map with at least one free colonist.
- Ensure the first domain pair is in `Open conflict` through the existing
  relation debug menu.
- Start each major test from a save made before forcing the occurrence.

## Build and consistency

From the repository root:

```powershell

git diff --check
.\build.cmd
.\tools\check-project-consistency.cmd
```

Version de DLL validée : `0.3.70.0`
The complete project-consistency check must now pass without exception.

## Developer actions

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

Relevant actions:

- `Show battlefield report`
- `Make battlefield opportunity due`
- `Force local battlefield now`
- `Force world battlefield site now`
- `Expire unvisited world battlefield site`
- `Order battlefield withdrawal`
- `Reset battlefield scheduler`

## Test A — World-site creation

1. Confirm that no local battlefield or world battlefield is active.
2. Run `Force world battlefield site now`.
3. Confirm one neutral-event letter naming the exact two open-conflict domains.
4. Confirm the letter targets the world site.
5. Confirm the site appears `6–18` tiles from a player home map.
6. Confirm the dedicated battlefield icon is distinct from the generic Tok'ra
   mission icons.
7. Inspect the site and confirm both domain names are shown in RP-facing text.
8. Confirm the remaining duration is displayed in days and hours rather than as
   one large hour count.
9. Open `Show battlefield report` and confirm:
   - active slot: world;
   - active world-object ID;
   - exact pair;
   - map not generated;
   - next shared opportunity not running.

## Test B — Normal caravan travel and arrival

1. Select a player caravan and use the normal right-click destination command.
2. Confirm the travel option and approach report describe the battlefield.
3. Save and reload while the caravan is travelling.
4. Let the caravan reach the tile without using a direct debug teleport.
5. Confirm the encounter map is generated only at arrival.
6. Confirm RimWorld pauses on the hostile map and the caravan enters from an
   edge separated from both rally positions.
7. Confirm the two Goa'uld detachments belong to the two exact stored domains.
8. Confirm the tracker now reports both the world-object ID and generated map ID.

## Test C — Reused battlefield behavior

1. Observe both forces entering from their own map edges.
2. Confirm each force travels to its opposing rally point.
3. Confirm no mutual attack begins before the rally message or bounded fallback.
4. Confirm the assault announcement names both domains.
5. Confirm ranged Jaffa advance until they have range and line of sight.
6. Confirm both groups attack one another and do not begin an organized assault
   against the player caravan.
7. Attack one camp while both domains are still fighting and verify mixed
   targeting:
   - nearby Jaffa respond to the attacking colon;
   - other Jaffa continue fighting the rival detachment;
   - the whole camp does not abandon the original front for one provocateur;
   - after `1800` quiet ticks, responders return fully to the rival;
   - no responder pursues beyond `35` cells from the recorded provocation origin.
8. Confirm a one-sided force at or below `30%` mobile strength breaks contact.
9. Confirm two simultaneously depleted forces continue until another ending
   condition is reached.
10. Confirm the absolute two-day battle limit still orders withdrawal.
11. Confirm withdrawal retaliation cannot exceed `6000` ticks and never moves
    the fixed forced-exit deadline.

## Test D — Loot, prisoners and reformation

1. Down at least one enemy without killing it.
2. Capture or carry the pawn through normal RimWorld behavior.
3. Leave bodies, equipment and at least one ordinary item on the map.
4. Confirm the vanilla **Reform caravan** command is visible once no active
   hostile threat remains.
5. Reform the caravan through the vanilla interface.
6. Confirm surviving player pawns, prisoners and selected loot can be taken.
7. Confirm unselected corpses and equipment do not appear as artificial rewards.
8. Confirm the world map/site is removed only after no player pawn or incoming
   transporter blocks map removal.
9. Confirm the shared recurrence delay begins only after complete removal.

## Test E — Ignored-site expiration

1. Force a new world battlefield site.
2. Do not enter it.
3. Save and reload with the marker still active.
4. Use `Expire unvisited world battlefield site`, or let the eight-day deadline
   elapse.
5. Confirm the marker disappears without:
   - mission failure letter;
   - goodwill change;
   - relation-state change;
   - settlement destruction;
   - reward or penalty.
6. Confirm the shared tracker clears the slot and schedules one normal recurrence
   delay.

## Test F — Shared slot and alternation

1. Force a local battlefield and confirm forcing a world site is rejected while
   it remains active.
2. Resolve the local battle completely.
3. Force a world site and confirm forcing another local battle is rejected.
4. Resolve or expire the site completely.
5. Use `Make battlefield opportunity due` several times with both forms
   available.
6. Confirm the scheduler alternates away from the previous form when possible.
7. Confirm the same pair is avoided when another eligible open-conflict pair
   exists.
8. Confirm only one shared opportunity clock is displayed.

## Test G — Persistence and storyteller boundary

Save and reload in each state:

- unvisited world site;
- caravan travelling to the site;
- newly generated map before the assault;
- active combat;
- player retaliation;
- withdrawal;
- resolved map waiting for player departure.

For every state, confirm exact factions, points, expiry, generated pawns, rally,
combat and shared-slot ownership remain coherent.

Then switch to Cassandra or another storyteller:

- an existing site remains visitable and can expire normally;
- an existing battle continues;
- no new local or world opportunity is created;
- the hidden future-opportunity deadline is shifted by the suspension duration;
- returning to Commandement SG-1 does not immediately replay an accumulated
  overdue event.

## Regression set

Revalidate:

- `0.3.66-dev` relation persistence and transitions;
- `0.3.68-dev` `75%` natural-raid pressure factor and exclusions;
- `0.3.69-dev` local battlefield creation and complete combat behavior;
- natural direct, abduction and destruction doctrines;
- extraction reprisals and forced regression raids;
- Tok'ra world sites and ordinary caravan reformation.

## Log review

After the complete pass, inspect `Player.log` for new:

- XML or Def errors;
- missing texture or translation keys;
- C# exceptions;
- Scribe reference errors;
- map-generation or world-object errors;
- caravan-arrival failures;
- Lord, JobDriver or pathing errors;
- duplicate shared-slot or recurrence messages.
