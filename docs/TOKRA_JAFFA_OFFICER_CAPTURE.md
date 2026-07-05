# Tok'ra Jaffa officer capture operation

Status: mission flow published in `0.3.37-dev`; the distinctive officer
loadout correction is validated and published in `0.3.74-dev` after final
revision `r2`.

## Player flow

1. A recurrent Tok'ra offer identifies a temporary Goa'uld field position.
2. Acceptance creates one world site and routes one sealed twelve-charge Tok'ra hypodermic rifle to the preferred delivery point.
3. The player identifies the silver-marked officer by his red command armor and retractable helmet, downs him alive and neutralizes the active escort.
4. No prison bed is required on the hostile map. The ordinary caravan-reformation dialog selects the downed officer as a prisoner for transport.
5. A mission-only physical restraint protects the prisoner during carrying and caravan travel after the temporary dart effect expires.
6. Once the caravan has actually removed the officer and no player pawn remains, the temporary map and its world marker are deleted.
7. On a player home map, the restraint is removed and ordinary RimWorld detention resumes.
8. Once the living prisoner is held by the colony, a pawn can use `Call the Tok'ra extraction team` on a powered communicator.
9. After an XML-configured delay of roughly four to twelve in-game hours, two or three Tok'ra agents enter visibly from the colony edge.
10. The mission restraint is reapplied for pickup. One Tok'ra agent uses the ordinary carrying flow to take the prisoner to the edge while the rest withdraw.
11. Success is resolved only after the prisoner and every extraction-team member have left the map.

## Technical structure

- `GateRimMissionCaptureDef` stores the site, target, tool, escort, restraint and home-extraction parameters.
- `WorldObject_TokraJaffaOfficerCaptureSite` owns only the hostile encounter and exports its live target before it is removed.
- `TokraJaffaOfficerCaptureTransferState` is serialized inside `TokraOrganicOperationInstance` and persists the officer, target-loss grace, extraction request, map, delay, team, carrier and departure state.
- `GameComponent_TokraOrganicOperationManager` migrates active development saves from the still-loaded site, keeps the target tracked after site cleanup and exposes the MissionDef `completeActionKey` through the communicator.
- The communicator action is available on any player home map that actually contains the living colony prisoner.
- `PawnsArrivalModeDefOf.EdgeWalkIn` creates the visible Tok'ra team.
- The carrier uses the vanilla `Kidnap` job only after the target is secured and downed by the mission restraint.
- `LordJob_ExitMapBest` withdraws the remaining agents after the carrier crosses the perimeter.
- Legacy development handoff fields remain readable and are migrated or reset for save compatibility.

## Site cleanup rule

The map is eligible for removal once:

- the encounter is initialized;
- no incoming transporter blocks cleanup;
- no player-faction pawn remains on the hostile map;
- the officer is dead, already resolved, present in a player caravan or present on a player home map.

Downed enemies and abandoned hostile pawns do not keep the temporary site alive after the player has successfully extracted the target.

## Deliberate limits

- Calling the Tok'ra never deletes or teleports the pawn.
- The prisoner must remain alive, present and held by the colony when pickup can begin.
- Before the team arrives, RimWorld keeps full control of prisoner beds, waiting and escape behavior.
- The transfer restraint returns only for the visible extraction sequence and is cleared on failure or retry.
- The extraction team may be interrupted; the mission retries rather than silently completing.
- The operation remains subject to its original deadline, recurrence, anti-repetition and adaptive escort profile.

## Validation result

The final `r6` test confirmed that the field map and marker disappear without losing the prisoner or the communicator action. The Tok'ra team then entered the colony, physically carried the prisoner off-map and awarded success only after the complete team had departed.

`0.3.74-dev-r1` added stable dedicated armor and helmet Defs with temporary
red textures. Research, manual rendering, protection and `SocialImpact +0.10`
were validated, but the mission-generated target did not receive the torso or
helmet. Final cumulative revision `r2` keeps both variants unavailable to random
apparel generation and explicitly verifies/equips them on the newly generated
capture target. The complete target loadout, helmet modes, save/reload and
extraction flow are validated. Final artwork may replace the PNG contents later
without changing the paths.
