# 0.2.43-dev - Tok'ra decoded mission site reconnaissance

## Scope

This milestone gives the first revealed Tok'ra mission world site a light, caravan-based use without opening a full combat mission yet.

## Player-facing behavior

- A player caravan can reconnoiter the revealed isolated Goa'uld relay when present on the site tile.
- The normal player-facing action is available from the caravan right-click menu on the world map. The direct site command is kept only for developer mode or the GateRim SG-1 advanced debug option.
- Reconnaissance opens an RP letter confirming Goa'uld/Jaffa activity.
- The Tok'ra secure communicator status report records that the site has been reconnoitered.

## Deliberate limitations

- No generated combat map.
- No automatic raid.
- No item reward.
- No healing.
- No Tok'ra reinforcement.
- No trade, recruitment or full quest resolution.

## Technical notes

- Extends `WorldObject_TokraDecodedMissionSite` with a reconnaissance command and caravan float-menu option gated by caravan presence.
- Extends `GameComponent_TokraTrustTracker` with persistent reconnaissance state.
- Updates the Tok'ra channel report with the reconnoitered-site state.
- Adds a debug action to mark reconnaissance for test runs.


If the selected caravan is not currently on the site tile, the same right-click menu offers travel back to the revealed relay before reconnaissance is available.
