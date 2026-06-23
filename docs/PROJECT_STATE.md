# Project state

Current milestone: `0.3.35-dev - Reorganize GateRim SG-1 debug actions into logical submenus` — validated locally on final revision `r2` and published under `v0.3.35-dev`.

## Working state

- Starting tag: `v0.3.34-dev`.
- Dedicated branch: `feature/debug-action-menu-reorganization`.
- Final local revision: `0.3.35-dev-r2`.
- Assembly version: `0.3.35.0`.
- Mod metadata version: `0.3.35-dev`.
- Cultural backstory count remains `83`.
- Main GitHub repository and separate wiki are current through published tag `v0.3.35-dev`.

## Published scope

- Replace the flat GateRim SG-1 debug-action list with four ordered entries directly inside the existing `GateRim SG-1` category.
- Reuse RimWorld's native `DebugActionNode` hierarchy instead of adding a custom debug window.
- Group actions first by system, then by mission or feature.
- Order each submenu according to the normal test flow: state report, scheduling or offer creation, acceptance or progression, success or failure, then reset or cleanup.
- Add a compact Tok'ra hierarchy covering the communicator, introduction arc, module study, organic operations and the earlier safehouse/intelligence chain.
- Split organic operations into one framework submenu and one submenu for each of the seven recurrent archetypes.
- Group Jaffa forehead-mark tools and cultural diagnostics in category-level submenus, while keeping mission-definition inspection as a direct category action.
- Remove the individual `DebugAction` attributes from the existing action methods while preserving their implementations as callable static methods.
- Keep every entry and child action restricted to RimWorld developer mode and to an active map.
- Update durable test documentation to use the new exact menu paths.

## Deliberate limits

- No gameplay rule, mission state, save data, Def, translation or player-facing text is changed.
- Contextual gizmos and the advanced-debug reports exposed on selected GateRim objects remain unchanged.
- This milestone does not redesign RimWorld's debug interface or add search, favorites or custom windows.
- No source file or texture is removed.

## Validation completed

- Project consistency check and forced `0.3.35.0` rebuild completed successfully.
- The native `GateRim SG-1` category directly exposes `Tok'ra...`, `Jaffa...`, `Culture...` and `Inspect mission definitions` in the documented order.
- No additional `GateRim SG-1...` wrapper or legacy flat action remains.
- The Tok'ra hierarchy and the seven organic-operation submenus follow the documented logical order.
- Representative communicator, introduction, study, organic-framework, organic-mission, safehouse, culture and MissionDef actions retain their previous behavior.
- Jaffa forehead-mark entries still activate the pawn-targeting map tool.
- Repeated menu navigation and save/reload produced no duplicate action or new debug-menu error reported during the focused validation.

## Final result

The native RimWorld category is now compact and extensible without adding a redundant first click. Existing debug implementations and gameplay behavior are preserved, while future systems can be added under stable thematic submenus.

## Next milestone

Start the next dedicated branch explicitly from `v0.3.35-dev` after rereading `AGENTS.md`, `docs/PROJECT_STATE.md`, `docs/ROADMAP.md` and `docs/MILESTONE_PUBLICATION.md`. No later milestone scope is fixed by this handoff.
