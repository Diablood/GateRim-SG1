# Project state

Current milestone: `0.3.29-dev - Add Tok'ra distress call world-site mission` — validated locally on revision `r7` and published under `v0.3.29-dev`.

## Development base

- Starting tag: `v0.3.28-dev`.
- Dedicated branch: `feature/tokra-distress-call-world-site`.
- Final validated local revision: `0.3.29-dev-r7`.
- Published assembly version: `0.3.29.0`.
- Final unique tag: `v0.3.29-dev`.
- Cultural backstory count remains `83`.

## Milestone purpose

This milestone adds a fifth recurrent Tok'ra organic operation. After accepting a fragmentary distress call through the powered communicator, the colony receives a temporary world site. A player caravan must reach it before the signal window closes. The true situation remains hidden until the encounter map is generated.

The implementation reuses the persistent operation slot, MissionDef recurrence, anti-repetition, RP text banks and captured threat snapshot. World-site generation, caravan arrival, encounter layout, combat, field treatment and extraction remain in a specialized adapter because they are concrete RimWorld mechanics.

## Implemented flow

1. the scheduler or a developer action creates a normal offered operation;
2. acceptance captures the scaled threat budget and creates one temporary site between `6` and `18` tiles from the colony;
3. the world-map interaction stores a vanilla `CaravanArrivalAction` on the caravan;
4. reaching the tile automatically generates and loads the map, invokes RimWorld's hostile-map pause and enters the player pawns drafted;
5. a single narrative anchor places the scene, survivors, corpses, a pre-existing salvage cache and defenders in one coherent area rather than using independent edge and centre spawns;
6. success or failure resolves through the global manager and schedules a new hidden recurrence delay;
7. the site and map are removed after resolution once normal RimWorld blockers are gone.

The offer lasts `120000` ticks. The accepted site lasts `300000` ticks. A genuine rescue degrades into late arrival after `120000` ticks. A trap or preselected late-arrival occurrence does not change according to travel speed.

## Hidden variants and scene context

### Genuine rescue

- generates `1` to `3` wounded Tok'ra near the scene anchor, never as unrelated edge entrants;
- generates a Goa'uld/Jaffa group close to the same attacked position;
- uses either a small temporary Tok'ra camp or an ambushed-caravan scene;
- may include a limited number of Tok'ra or Jaffa corpses from the preceding fight;
- requires the hostile presence to be broken and the symbiote shock to be tended;
- accepts normal field tending while the survivor lies on the ground; no player medical bed or improvised heated structure is required;
- starts a configured `600`-tick delay once at least one survivor has been treated and the site is secure;
- brings a visible Tok'ra recovery team through RimWorld's vanilla edge-walk-in arrival mode;
- uses the vanilla non-hostile carry-and-exit job path for downed survivors, while mobile survivors leave through their existing departure behavior;
- succeeds only after at least one survivor has physically left the map alive and all remaining survivors are resolved.

### Compromised signal

- generates no living Tok'ra survivor;
- creates a prepared or damaged defensive position around the false signal;
- may include bodies that make the trap or previous clash credible;
- uses the full configured threat factor;
- succeeds when the active Goa'uld/Jaffa presence is neutralized.

### Late arrival

- generates no living allied survivor;
- creates either an overrun temporary camp or the remains of an ambushed caravan;
- normally includes dead Tok'ra and may include dead Jaffa;
- leaves Goa'uld/Jaffa forces near the scene and a configurable component cache already stored on a vanilla shelf when the map is generated;
- succeeds when the hostile presence is neutralized.

## Vanilla-first implementation rule

Revision `r3` replaces the manual travel/entry sequence with RimWorld's normal `CaravanArrivalAction` and `CaravanEnterMapUtility` flow. The standard hostile-map notification supplies the pause, and the vanilla entry utility drafts the player's colonists. Its existing entry-cell predicate is used only to select the map edge nearest the generated scene.

Custom code is limited to behavior not supplied by the base game for this mission: hidden variant selection, coherent scene placement, wounded Tok'ra generation, treatment recognition and coordination of the visible Tok'ra recovery team.

## Data ownership

`SG1_TokraOrganic_DistressCall` owns:

- offer, accepted-site, late-arrival and recovery-team timings;
- trust-tier weights, context delays and repeat penalty;
- threat scaling and bounds;
- world-object, survivor, recovery-team, salvage-item and salvage-storage Def references;
- distance, map size, actor counts and per-variant threat factors;
- scene-type chances, entry radius and corpse-count ranges;
- offer and success text banks, runtime messages, trust changes and Medicine XP.

The C# adapter owns world-tile selection, the vanilla caravan arrival action, scene geometry, pawn placement, combat observation, field-treatment recognition, recovery-team job assignment and map cleanup.

## Validation history and current status

Revision `r2` was reported globally functional in game. The focused rescue test revealed three design problems:

- arrival did not yet reproduce the expected automatic vanilla hostile-site entry, pause and drafted control;
- survivors and Jaffa could be generated in unrelated areas, including survivors at a map edge;
- the original requirement for a player medical bed, natural recovery and walking departure made cold, hunger and long healing dominate the mission.

Revision `r3` addressed these points and added variant-specific scene context. Revision `r4` added the missing `RimWorld` namespace import required to compile the new caravan arrival action. Revision `r5` kept the vanilla arrival and scene flow, but replaced the abrupt pawn removal with a visible Tok'ra recovery team using vanilla arrival, carrying and map-exit behavior. The `r5` startup log then showed five unknown XML fields because extracted source timestamps allowed the incremental build to retain the older `r4` DLL. Revision `r6` changed `build.cmd` to use `--no-incremental`, ensuring that local overlay tests compile the current source. Revision `r7` kept that validated gameplay unchanged and moved the late-arrival components onto a vanilla storage shelf created with the scene, so the material reward is present before resolution instead of appearing as loose ground loot. The final consistency check, forced rebuild, complete in-game flow, focused shelf test, persistence, recurrence, previous-operation regressions, English/French texts and `Player.log` were reported valid. The durable coverage is recorded in `docs/TESTING.md`.

## Locked next milestone

The next milestone is:

`0.3.30-dev - Add Tok'ra temporary-base delivery mission`

It must start explicitly from `v0.3.29-dev` on a new dedicated branch.

## Publication state

- Published branch: `feature/tokra-distress-call-world-site`.
- Final tag: `v0.3.29-dev`.
- Main repository and separate wiki synchronized.
- Next milestone: `0.3.30-dev - Add Tok'ra temporary-base delivery mission`, starting explicitly from `v0.3.29-dev` on a new dedicated branch.
