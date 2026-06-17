# 0.2.42-dev - Tok'ra decoded mission world site

## Scope

This milestone turns the decoded Tok'ra operational lead into the first visible world-map footprint for the Tok'ra mission chain.

## Player-facing behavior

- After the coded Tok'ra intelligence packet has been analyzed and the Tok'ra decoded mission lead has been confirmed, a short delayed Tok'ra contact reveals a world-map marker.
- The marker represents an isolated Goa'uld relay tied to a Jaffa transit point.
- The marker is temporary and expires if ignored.
- The Tok'ra secure communicator status report shows the mission-site state.

## Deliberate limitations

- No generated mission map yet.
- No automatic attack.
- No immediate item reward.
- No direct healing.
- No Tok'ra reinforcement.
- No trade, recruitment or full quest resolution.

## Technical notes

- Adds `WorldObject_TokraDecodedMissionSite`.
- Adds `SG1_TokraDecodedMissionWorldSite`.
- Extends `GameComponent_TokraTrustTracker` with persistent world-site reveal state.
- Adds a debug action to reveal the site for test runs.

## 0.2.43-dev reconnaissance step

- A revealed decoded mission site can now be reconnoitered when a player caravan is present on the site tile.
- Reconnaissance confirms Goa'uld/Jaffa activity through an RP letter and persistent Tok'ra mission state.
- The Tok'ra channel report now distinguishes between a revealed site and a reconnoitered site.
- The world site remains non-combat and non-rewarding for this milestone: no generated combat map, no raid, no direct reward, no healing, no reinforcements, no trade and no recruitment.
