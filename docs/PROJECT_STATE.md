# Current project state

Current milestone: `0.3.52-dev - Add the Goa'uld faction caste summary` - final revision `r1` validated and published.

## Repository state

- Starting tag: `v0.3.51-dev`.
- Starting commit: `3ec8ac0`.
- Active branch: `feature/goauld-caste-world-summary`.
- Last published version: `0.3.52-dev` on `feature/goauld-caste-world-summary`.
- Last published tag: `v0.3.52-dev`.
- Retrospective publication correction: annotated tags `v0.3.50-dev` and
  `v0.3.51-dev` were added manually by the maintainer and point to their
  validated milestone commits.
- Target version: `0.3.52-dev`.
- Technical assembly version: `0.3.52.0`.
- Local revision: `r1`.
- Publication status: final branch committed and pushed; annotated tag and separate wiki published.

## Current scope

This milestone completes the player-visible description of the Goa'uld world
faction. RimWorld's vanilla summary can only report xenotype percentages and
therefore shows `Jaffa: 100%`, while generated settlements also contain a
minority host caste and the faction leader is a persistent System Lord host.

The implementation appends one localized qualitative caste section to
`FactionDef.Description` only for `SG1_GoauldSystemLordPrototype`. It does not
alter `xenotypeSet`, pawn groups, settlement weights, leaders, raids or host
initialization. Harmony is already a declared mod dependency and no Harmony
DLL is embedded in GateRim SG-1.

## Validated r1 test

1. Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1`.
2. Open `Nouvelle colonie` > `Équipe SG isolée` > `Créer le monde` > `Factions`.
3. Place the pointer over `Domaines des Grands Maîtres Goa'uld`.
4. Verify that the tooltip retains the faction description and vanilla member-xenotype section, then adds `Castes Goa'uld` with dominant Jaffa servants, minority Goa'uld hosts, the System Lord host leader and the acquired-possession explanation.
5. Place the pointer over `Jaffa libres` and verify that no `Castes Goa'uld` section appears.
6. Generate the world and inspect `Player.log`.

Expected result: the Goa'uld tooltip explains both biological xenotypes and
acquired host castes without changing other faction tooltips; world generation
completes and `Player.log` contains no new Harmony, translation or C# error.

## Local validation

- `git diff --check`: passed, with only the usual line-ending warnings.
- English and French keyed XML parsing: passed.
- Translation-key reference audit: passed.
- `./tools/check-project-consistency.cmd`: passed for `0.3.52-dev` / `0.3.52.0`.
- Forced C# rebuild: passed with `0` warnings and `0` errors.
- In-game validation: passed and accepted by the maintainer.
- Goa'uld tooltip, Free Jaffa non-regression, world generation and `Player.log`: passed.

## Previous milestone record

The remainder of this document retains the validated `0.3.51-dev` mission-site
icon record as the previous milestone history.

### Mission-site icon scope

The previous milestone standardized the player-visible world-map icons for GateRim SG-1 mission markers. The uniform vanilla `ItemStash` proposal did not satisfy the requested semantic grouping and is not an acceptable result.

The required six visual types are:

| Player-facing type | Affected defs | Dedicated texture |
| --- | --- | --- |
| Tok'ra clandestine contact | `SG1_TokraHiddenSafehouseMarker`, `SG1_TokraHiddenSafehouseSite` and `SG1_TokraHiddenSafehouseSitePart` | sandstone and teal concealed arch |
| Goa'uld encrypted objective | `SG1_TokraIntroductionArtifactWorldSite` | gold and violet encrypted tablet |
| Goa'uld relay sabotage | `SG1_TokraDecodedMissionWorldSite` | bronze and violet relay with a red-orange strike |
| Tok'ra distress signal | `SG1_TokraDistressCallWorldSite` | teal diamond beacon with amber signal arcs |
| Tok'ra logistics rendezvous | `SG1_TokraTemporaryBaseDeliverySite` | ochre supply crate with teal straps and rendezvous arrows |
| Jaffa officer field position | `SG1_TokraJaffaOfficerCaptureSite` | gold Jaffa helmet above a crimson field post |

The preliminary safehouse marker and the revealed safehouse site belong to the same player-facing type and must keep the same silhouette. Hidden distress-call variants also keep one shared icon so their outcome is not revealed before arrival.

This milestone does not change mission mechanics, spawn logic, deadlines, caravan actions, rewards, trust, operation recurrence, faction generation or the Tok'ra world-selection warning.

The functional addition is a dedicated debug submenu using the real XML `defName` labels. It avoids the old indirect mission menus and the truncated entries in RimWorld's vanilla `Do incident (Map)` list.

## Technical approach

Icon selection remains entirely Def-driven. The custom `128x128` color PNGs are used only through `expandingIconTexture`. Close zoom uses the vanilla `GenericSite` texture for all seven mission objects, allowing RimWorld's normal rotated top-down site rendering to remain meaningful. No runtime icon switching or Harmony patch is needed.

C# is used only for test accessibility:

- `Source/GateRimSG1/Goauld/MissionSiteIconDebugActions.cs` creates the real mission marker or site through existing flows where possible;
- `Source/GateRimSG1/Debug/GateRimDebugActionMenu.cs` exposes those actions under one submenu.

## Files in the milestone

Modified implementation and metadata:

- the six affected `WorldObjectDef` files and `SG1_TokraHiddenSafehouseSitePart`;
- six PNG files under `Textures/World/WorldObjects/Expanding/Sites/`;
- `About/About.xml`
- `Source/GateRimSG1/GateRimSG1.csproj`
- `Source/GateRimSG1/Debug/GateRimDebugActionMenu.cs`
- `Source/GateRimSG1/Goauld/MissionSiteIconDebugActions.cs`

Updated project documentation:

- `README.md`
- `docs/CHANGELOG.md`
- `docs/MISSION_SITE_WORLD_ICONS.md`
- `docs/PROJECT_STATE.md`
- `docs/ROADMAP.md`
- `docs/TESTING.md`
- `docs/TESTING_CURRENT.md`
- version references in the relevant `docs/wiki/` drafts

The six approved assets are present under `Textures/World/WorldObjects/Expanding/Sites/`. Only the expanding texture paths point to them; close-zoom fields use vanilla world-object textures.

## Revision status

- `r1`: rejected. It exposed the real sites through a useful debug submenu but retained the pre-existing `Town` / `ItemStash` icons instead of implementing the six requested semantic types.
- `r2`: partially validated. All six colored expanding icons are present and render well. Close zoom incorrectly reused and randomly rotated the same custom icon instead of a vanilla world-object texture.
- The apparent coexistence difference is expected gameplay: the safehouse marker and revealed site are alternate stages; distress call, logistics rendezvous and Jaffa capture share one organic-operation slot; introduction and decoded-relay arcs are independent.
- `r3`: restores vanilla close-zoom textures and groups the debug menu by lifecycle without changing mission-state rules.
- `r3` validation passed: dedicated expanded icons, vanilla close-zoom rendering, safehouse-stage replacement, the single organic slot, independent-arc coexistence and `Player.log` are accepted.

## Validated r3 test

1. Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1`.
2. Use a colony with Tok'ra enabled and a built, powered Tok'ra secure communicator.
3. Open `Actions de débogage` > `GateRim SG-1` > `Tok'ra...` > `Mission-site icon tests...` > `Independent arcs...`.
4. Select `Create SG1_TokraHiddenSafehouseMarker`, then `Create SG1_TokraHiddenSafehouseSite`: verify the shared custom arch when zoomed out, a vanilla close-zoom texture when zoomed in, and replacement of the marker by the revealed site.
5. Select `Create SG1_TokraIntroductionArtifactWorldSite`, then `Create SG1_TokraDecodedMissionWorldSite`: verify their custom expanded icons, vanilla close-zoom site texture and simultaneous presence with the safehouse.
6. Return to `Mission-site icon tests...` > `Organic sites (one active)...`.
7. Select `Create SG1_TokraDistressCallWorldSite`, `Create SG1_TokraTemporaryBaseDeliverySite`, then `Create SG1_TokraJaffaOfficerCaptureSite`, checking each icon before creating the next.
8. Verify that each organic site replaces the preceding organic site, while the independent safehouse, introduction and decoded-relay sites remain.
9. Inspect `Player.log`.

Expected result: custom colored icons appear only at expanded zoom; close zoom uses vanilla marker/site rendering; safehouse stages replace each other; only one organic-operation site remains; independent arcs coexist; `Player.log` contains no new texture, XML or C# error.

## Validation status

- Initial branch created: `feature/operation-site-icon-overhaul`.
- Relevant `WorldObjectDef`, `SitePartDef`, `MissionDef` and vanilla icon paths audited.
- Initial two-icon vanilla policy rejected after maintainer review.
- Six dedicated player-facing icon types restored as the required scope.
- Dedicated `Mission-site icon tests...` submenu added in C#.
- `git diff --check`: passed, with only the usual CRLF normalization warnings.
- `.\tools\check-project-consistency.cmd`: passed.
- Forced rebuild `0.3.51.0`: passed with `0` warnings and `0` errors.
- In-game `r1` result: failed scope review; the expected dedicated icons were absent.
- `r2` icon-preview approval: passed for all six silhouettes and their mission symbolism; colored fills authorized.
- Six colored transparent PNG assets generated and checked at `128x128`.
- `r2` custom expanding icons validated visually in game.
- `r2` close-zoom result rejected because custom icons were reused and randomly rotated as base textures.
- Vanilla `GenericSite` close-zoom paths restored for all seven mission objects in `r3` while custom expanding paths are retained.
- Debug test menu split into `Independent arcs...` and `Organic sites (one active)...` with explicit replacement messages.
- XML texture-reference audit: `7` vanilla `GenericSite` base paths and `7` dedicated expanding paths resolve correctly.
- PNG dimension and transparent-corner audit: passed for all six assets.
- `git diff --check`: passed, with only the usual CRLF normalization warnings.
- `.\tools\check-project-consistency.cmd`: passed for `0.3.51-dev` / `0.3.51.0`.
- Forced rebuild `0.3.51.0`: passed with `0` warnings and `0` errors after the `r3` debug-menu change.
- In-game `r2` validation: partial; expanded icons passed, close zoom failed, coexistence behavior observed.
- In-game `r3` validation: passed and accepted by the maintainer.
- `Player.log`: passed as part of the validated `r3` checklist.
- Branch publication: completed after explicit maintainer authorization.
- Separate wiki synchronization and publication: completed because `docs/wiki/` changed.
- Final annotated tag `v0.3.51-dev`: published by the maintainer after the
  branch and wiki publication.

## Deferred visual scope

The current pawn, apparel, object and building textures are primarily
placeholders that establish stable texture paths while gameplay systems are
still evolving. Do not treat isolated polish of those assets as the next
milestone. Their final art direction, including a more distinctive Jaffa
officer, belongs to one later complete texture pass. The faction and mission
world-map icons validated in `0.3.50-dev` and `0.3.51-dev` remain accepted UI
assets and do not imply that all in-map textures are final.
