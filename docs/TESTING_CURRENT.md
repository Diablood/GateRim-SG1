# Current validation — 0.3.30-dev

Jalon : `0.3.30-dev - Add Tok'ra temporary-base delivery mission`
Version de DLL validée : `0.3.30.0`
Branch: `feature/tokra-temporary-base-delivery`
Base: `v0.3.29-dev`
Final local revision: `r9`
Published tag: `v0.3.30-dev`

## Final result

The milestone is locally validated and published. Revision `r9` is the final local test revision; the suffix is not part of the commit message or tag.

The final correction preserves the gameplay validated in `r8` while observing RimWorld's queued ambush-map creation asynchronously. The final approach remains blocked until the temporary map has actually appeared and disappeared, eliminating the false unidentified-world-object error and preventing premature route-secure messages.

A naturally selected interception was also observed without developer forcing, confirming that the organic complication path is reachable during normal play.

## Build and startup

Validated publication expectations:

```powershell
./tools/check-project-consistency.cmd
./build.cmd "D:/SteamLibrary/steamapps/common/RimWorld/RimWorldWin64_Data/Managed"
```

Validated results:

- assembly version `0.3.30.0`;
- six organic MissionDefs loaded;
- no XML, Def, translation or C# initialization error;
- no incomplete or unknown warning for the delivery MissionDef or its two encounter IncidentDefs;
- final `Player.log` clean for the tested flow.

## Contract generation and normal delivery

Validated coverage:

- a contract is offered only when its product can reasonably be manufactured with the colony's current recipes, research, worktable and capable colonists;
- selected product, quantity, quality and condition remain stable after save/reload;
- the rendezvous appears `6–16` tiles away and remains reachable through normal caravan routing;
- incomplete cargo may travel but cannot be handed over;
- `Hand over the requested goods` / `Remettre la commande` reports the real missing amount;
- exactly the requested conforming quantity is consumed;
- unrelated cargo, excess conforming goods and recovered battlefield loot remain in the caravan;
- an on-time delivery grants `+2` trust exactly once.

## Delay and expiry

Validated coverage:

- the normal deadline opens one `120000`-tick grace window rather than failing immediately;
- the warning appears once and survives save/reload;
- delivery during the grace period grants `+1` trust;
- final expiry applies failure and `-1` trust exactly once;
- no late warning, result or trust change is duplicated after reload.

## Ordinary journey interception

Validated coverage:

- `Debug actions menu → GateRim SG-1 → Tok'ra ops: delivery interception` arms exactly one ordinary Goa'uld ambush;
- the ambush triggers only for a complete conforming shipment actively travelling to the exact rendezvous;
- the encounter consumes the offer-time threat snapshot within configured bounds;
- military victory grants no trust and does not complete the operation;
- cargo lost or damaged in combat no longer counts;
- the surviving shipment must still reach the Tok'ra;
- the trigger, retry state and single-occurrence guarantee persist after save/reload.

## Final-approach ambush

Validated coverage:

- `Debug actions menu → GateRim SG-1 → Tok'ra ops: delivery approach ambush` compromises the last approach without attacking while the caravan remains farther away;
- the temporary map opens on the caravan's adjacent tile when its next path tile is the rendezvous;
- the rendezvous remains visible one tile away and never hosts the combat map;
- caravan pawns, animals and real inventory enter through the vanilla flow;
- the player may recover surviving cargo, enemy weapons, apparel and all other vanilla-selectable map items;
- the complete vanilla reformation dialog creates the caravan on the approach tile;
- the rendezvous can be selected directly as the next destination without an artificial detour;
- the queued encounter is bound after map creation, with no unidentified-world-object error;
- the route-secure message appears exactly once only after the hostile map is gone;
- the final one-tile journey and explicit handoff remain required.

## Persistence and anti-duplication

Save/reload coverage includes the offer, accepted contract, travel, late window, pending encounter, hostile map, post-combat reformation, final tile and pre-handoff state.

Validated results:

- contract, deadlines, threat snapshot and complication do not reroll;
- no duplicate map, letter, secure message, handoff, result or trust change appears;
- an encounter triggers at most once per contract;
- local pre-publication saves without a selected new complication do not receive a retroactive reroll;
- cargo requirements and real inventory checks remain authoritative after reload.

## Recurrence, text variation and regressions

Validated milestone-wide coverage:

- the delivery operation becomes eligible again after success, failure and ignored offer only after its hidden variable delay;
- the previous-archetype penalty and single active-operation slot remain intact;
- weighted offer and result variants retain local anti-repetition when alternatives exist;
- technical weights, delays and internal state remain absent from player-facing texts;
- observation, intelligence recovery, wounded-agent care, medical handoff and distress call continue to resolve normally;
- developer actions and reports remain limited to developer mode or the advanced GateRim option;
- adaptive combat uses captured RimWorld threat points rather than fixed enemy counts, with weak and advanced colony coverage retained in the milestone regression record.

## Durable regression points

Future changes to caravan, world-site or mission orchestration code must preserve:

- physical cargo handoff as the only success condition;
- exact cargo consumption without taking unrelated inventory;
- complete vanilla loot selection and caravan reformation after hostile maps;
- no forced detour when the final approach encounter ends;
- one complication at most per contract and no save-scumming reroll;
- delayed map-parent observation for queued caravan incidents;
- on-time `+2`, late `+1` and final-expiry `-1` trust consequences;
- recurrence, hidden delays, anti-repetition and the global single-operation slot.
