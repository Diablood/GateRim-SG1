# Current project state

Current milestone: `0.3.56-dev - Add Goa'uld extraction ultimatum` - local
revision `r5` validated and published.

## Repository state

- Starting tag: `v0.3.55-dev`.
- Starting commit: `4870031`.
- Active branch: `feature/goauld-extraction-ultimatum`.
- Last published version: `0.3.56-dev` on `feature/goauld-extraction-ultimatum`.
- Last published tag: `v0.3.56-dev`.
- Target version: `0.3.56-dev`.
- Technical assembly version: `0.3.56.0`.
- Local revision: `r5`.
- Publication status: final branch committed and pushed; annotated tag and separate wiki published.

## Current scope

This milestone turns the validated extraction reaction into the first actual
Goa'uld ultimatum. A successful `extract active Goa'uld symbiote` surgery on a
player home map now opens a one-day choice from the exact domain stored in the
symbiote's identity.

The colony may surrender the exact living symbiote created by the surgery. The
pawn is removed as a visible handover, the raid is cancelled and the existing
15-day domain cooldown begins. If the symbiote is no longer spawned on the
target colony map, that choice is disabled with an explicit reason.

The third choice `Decide later` / `Voir plus tard` uses RimWorld's native
postpone option: it closes the dialog, leaves the letter visible and does not
pause its one-day timeout. Expiration remains equivalent to defiance.

While the choice remains unresolved, the extracted hostile symbiote is kept
under monitored anesthesia. It cannot wake and force combat before the player
has answered. Killing it is detected within `250` ticks, closes the ultimatum
and counts as immediate defiance.

Defying the domain or allowing the letter to expire schedules the
`SG1_GoauldJaffaNaturalRaid` validated in `0.3.55-dev`, after one to three
days. The domain, target map and vanilla threat points remain those captured at
extraction time. One active ultimatum or reprisal is allowed per domain. Old
`0.3.55-dev` saves containing a pending reprisal retain that state.

The slice adds no silver tribute, goodwill transaction, new incident, mission
slot or generic Goa'uld quest framework.

An active-extraction surgery that fails but leaves the host alive still creates
no domain reaction. If that failure immediately kills the active host, no
symbiote exists to surrender, so the domain announces a direct delayed
reprisal instead of opening an ultimatum.

After successful extraction, a generated `SG1_GoauldHostCaste` or
`SG1_GoauldSystemLordHost` prisoner no longer belongs to the System Lord
domain. The same pawn remains a factionless colony prisoner and is explicitly
recruitable or releasable. This does not affect a possessed player pawn, which
still returns to its recorded displaced player faction.

## Required r5 test

Reuse a save with a generated Goa'uld caste host captured as a colony prisoner
and ready for `extraire le symbiote Goa'uld actif`.

1. Complete the surgery successfully and confirm the message stating that the
   former host no longer belongs to the named System Lord domain.
2. Select the former host. Confirm the faction is no longer `Domaines des
   Grands Maîtres Goa'uld`, while the pawn remains a colony prisoner.
3. Open the visible `Prisonnier` tab and confirm the normal `Recruter` and
   `Libérer` choices are available.
4. Let at least `120` ticks pass. Confirm no symbiote is recreated and inspect
   `Player.log` for new faction, guest, prisoner, extraction or C# errors.

Expected result: removing the Goa'uld liberates the generated human body from
the domain without gifting the pawn to the player; recruitment or release
remains a deliberate vanilla prisoner decision.

Result: passed. The generated former host left the System Lord domain, remained
a factionless colony prisoner with recruit/release choices, received no
replacement symbiote and produced no new `Player.log` error.

## Validated r4 test

Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Select `Reset extraction reactions`, then `Create extraction ultimatum` and
   `Voir plus tard`. Let at least `3000` ticks pass and confirm the demanded
   symbiote remains anesthetized rather than attacking.
2. Select `Kill demanded symbiote`. Confirm the ultimatum closes immediately
   and the reprisal warning says the demanded symbiote was killed and displays
   the actual delay before arrival.
3. Select `Show extraction reaction state`; confirm one pending reprisal, then
   select `Trigger pending reprisal now` and confirm the domain raid.
4. Select `Reset extraction reactions`, `Create extraction ultimatum`, then
   `Expire current ultimatum`. Confirm the expiration warning also displays its
   actual delay and the report shows a pending reprisal.
5. Inspect `Player.log` for new C#, XML, anesthesia, letter, pawn or reaction
   errors.

Expected result: the demanded pawn stays safely unconscious while the choice
is open; killing it immediately resolves the ultimatum as defiance; every
scheduled attack announces that it remains delayed instead of appearing lost.

Result: passed. Monitored anesthesia, immediate reaction to demanded-symbiote
death, explicit delayed-raid timing, expiration and the resulting domain raid
were accepted.

## r3 finding

Postponement and timeout behavior matched the intended choice flow. Extended
testing killed the demanded pawn and then interpreted the delayed raid as
missing. `Player.log` proves both attempts scheduled correctly, at `120255`
and `68640` ticks. Revision `r4` keeps the pawn sedated, reacts to its death
before timeout and exposes that delay in the warning.

## Validated r2 test

The main debug flow passed in `r1`. The extended real-surgery test found two
distinct facts in `Player.log`:

- the Jaffa raid that arrived during surgery was the previously defied debug
  ultimatum, whose `5000`-tick reprisal resolved normally;
- that raid started the expected 15-day domain cooldown, so the later real
  extraction was correctly prevented from opening a second reaction;
- after extraction, the generated-host caste scanner repeatedly dereferenced
  the now-absent symbiote component. Revision `r2` adds the missing null guard
  without recreating a symbiote.

Reuse a save with a captured generated Goa'uld host ready for the Health-tab
operation `extraire le symbiote Goa'uld actif`.

Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Select `Reset extraction reactions` before starting the surgery. Select
   `Show extraction reaction state` and confirm no reaction or cooldown remains.
2. Complete `extraire le symbiote Goa'uld actif` on the prepared prisoner.
3. Confirm the letter `Ultimatum Goa'uld après extraction` appears immediately
   and names the actual domain, extracted symbiote and former host.
4. Let the game run for at least `120` ticks, then inspect `Player.log`.

Expected result: the real surgery creates one ultimatum because the prior
cooldown was reset; the former host receives no replacement symbiote; no
`GameComponent_GoauldHostCasteInitializer.EnsureGeneratedHostAllegiance`
exception or repeated `NullReferenceException` appears.

Result: passed. The real extraction opened its ultimatum after reset, the
former host received no replacement symbiote and the repeated initializer
`NullReferenceException` disappeared from `Player.log`.

## Validated r1 debug test

Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1` on a player home map.

Open exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Select `Reset extraction reactions`, then `Create extraction ultimatum`.
   Confirm that one anesthetized hostile Goa'uld symbiote appears and the letter
   `Ultimatum Goa'uld après extraction` offers exactly `Remettre le symbiote
   extrait` and `Défier le domaine`.
2. Select `Show extraction reaction state`; confirm an active ultimatum, the
   symbiote identifier, current map and non-zero point snapshot. Save as a
   dedicated test save, reload it and confirm that the same letter, pawn and
   report remain present.
3. In the letter, select `Remettre le symbiote extrait`. Confirm that the exact
   symbiote disappears, a handover message appears and the report shows the
   domain in cooldown with no pending raid.
4. Reload the dedicated save made in step 2. In the restored letter, select
   `Défier le domaine`. Confirm the reprisal letter and use `Show extraction
   reaction state` to verify one pending raid with the preserved points.
5. Select `Trigger pending reprisal now`; confirm a Goa'uld/Jaffa raid arrives
   on foot from a map edge and belongs to the domain named in the ultimatum.
   Confirm the report then shows cooldown and inspect `Player.log` for new C#,
   XML, Scribe, faction, pawn, letter or raid errors.

Expected result: surrender visibly consumes the demanded pawn and prevents the
attack; defiance preserves the existing delayed consequence from the exact
domain; save/reload preserves the unresolved player choice.

Result: the debug surrender, refusal, persistence, raid, cooldown and extended
real-host setup were accepted. Later revisions retain this validated flow while
covering the real-surgery and demanded-pawn lifecycle findings above.

## Optional regressions

- Select `Reset extraction reactions`, create another ultimatum, then select
  `Expire current ultimatum`; confirm the choice letter closes and the expiry
  text schedules the same pending reprisal.
- While an ultimatum or reprisal is pending, select `Create extraction
  ultimatum` again and confirm a refusal instead of a second reaction.
- After surrender or raid resolution, confirm the same refusal during the
  15-day cooldown.
- Make the demanded symbiote unavailable without killing it and confirm that
  surrender is disabled with a visible explanation. Killing it must instead
  close the choice and schedule the reprisal immediately.
- On a prepared prisoner carrying `SG1_GoauldHostSymbiote`, complete the Health
  tab operation `extraire le symbiote Goa'uld actif` and confirm it creates the
  same ultimatum for the actually extracted pawn without debug scheduling.
- With two configured Goa'uld factions, extract a host aligned to the second
  domain and confirm the ultimatum and raiders retain that exact faction.

## Local validation

- Initial forced C# rebuild found one missing `RimWorld` namespace import in the
  new choice letter; corrected locally.
- Forced C# rebuild before metadata update: passed with `0` errors; NuGet
  vulnerability lookup emitted the existing offline `NU1900` warning.
- Final forced rebuild `0.3.56.0`: passed with `0` errors; NuGet vulnerability
  lookup emitted the existing offline `NU1900` warning.
- All `358` XML files parse, EN/FR reaction keys align, `git diff --check` and
  the project consistency check pass.
- In-game `r1`: main debug flow passed; extended real extraction exposed the
  generated-host scanner null dereference corrected in `r2`.
- In-game `r2`: targeted real-extraction retest passed.
- In-game `r3`: choice flow observed; demanded-pawn lifecycle and delayed-raid
  clarity required `r4`.
- In-game `r4`: targeted anesthesia, pawn-death and delay-text test passed.
- In-game `r5`: former-host faction and prisoner-choice validation passed.

## Previous published milestone record

The previous published milestone is `0.3.55-dev - Add Goa'uld extraction
reprisals`, validated as revision `r1` and published from
`feature/goauld-domain-extraction-reprisal`. The remainder of this document retains
the validated `0.3.51-dev` mission-site icon record as earlier history.

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
