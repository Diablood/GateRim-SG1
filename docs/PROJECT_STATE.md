# Current project state

Current milestone: `0.3.37-dev - Add Tok'ra Jaffa officer capture operation` — functionally validated in local revision `r6` and published under the final milestone version.

## Repository state

- Starting tag: `v0.3.36-dev`.
- Dedicated branch: `feature/tokra-jaffa-officer-capture-operation`.
- Published versions: `0.3.37-dev` and `0.3.37.0`.
- Final local validation revision: `r6`.
- Final unique tag: `v0.3.37-dev`.
- The main repository and separate wiki are synchronized for the milestone.

## Published outcome

- Add an eighth recurrent Tok'ra organic-operation archetype without renumbering existing persisted values.
- Create a temporary hostile world site containing one living silver-marked Jaffa officer and a threat-scaled Goa'uld/Jaffa escort.
- Deliver one sealed twelve-charge Tok'ra hypodermic rifle through the preferred Tok'ra delivery point.
- Preserve vanilla caravan reformation so the downed officer can be selected as a transported prisoner without a local prison bed.
- Keep the officer physically restrained during carrying and caravan travel, then return control to ordinary RimWorld prisoner handling on the player colony map.
- Remove the hostile map and world marker after the officer is confirmed in a player caravan or player home map and no player pawn remains on the site.
- Persist the officer and all home-extraction state independently of the removed WorldObject.
- Add a powered-communicator action that calls a visible Tok'ra extraction team after a hidden four-to-twelve-hour delay.
- Have the team physically collect the detained officer and resolve success only after the prisoner and every extraction-team member have left the map.
- Preserve recurrence, hidden delays, local anti-repetition, adaptive escort strength, save compatibility and developer diagnostics.

## Final validation

The final `r6` pass validated the complete primary flow in game:

1. hostile-site generation and living-target neutralization;
2. vanilla prisoner selection during caravan reformation without a prison bed on the temporary map;
3. caravan travel and detention in a colony cell;
4. removal of the temporary map and world marker after evacuation;
5. continued prisoner tracking after site cleanup;
6. availability of the extraction call on the powered Tok'ra communicator;
7. delayed visible arrival of the Tok'ra team;
8. physical pickup and departure with the prisoner;
9. single mission success after the complete extraction team leaves the map.

The milestone reported no remaining blocking functional issue. Rare interruption and retry paths remain durable regression cases for future mission-framework changes rather than publication blockers.

## Next milestone

No `0.3.38-dev` scope is imposed by this closure. The next milestone must be chosen after reviewing `docs/ROADMAP.md`, then start explicitly from `v0.3.37-dev` on a new dedicated branch after rereading the repository procedures.
