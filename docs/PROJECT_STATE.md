# Project state

Current milestone: `0.3.78-dev - Add delayed allied Goa'uld raid reinforcements`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.77-dev` at commit
  `cff999a715285d45d9380f6d2bab4937f6229e13`.
- Active branch: `feature/goauld-allied-reinforcements`.
- Final revision: `r1`, validated and published on `develop` as annotated tag
  `v0.3.78-dev`.
- Published assembly version: `0.3.78.0`.
- The changed player-facing wiki sources are synchronized with the separate
  wiki repository as part of publication.

## Validated design decisions

- The effect applies only to ordinary natural Goa'uld raids under
  `Commandement SG-1`.
- The primary domain must have an active alliance without open-conflict
  precedence.
- The final combined raid budget must be at least `800` points.
- The existing non-stacking alliance budget remains `110%` of vanilla points.
  It is split `75%` primary force and `25%` allied force; no free points are
  added.
- One exact allied domain is selected. Several alliances never create several
  waves.
- The allied force arrives after `1800` to `3600` ticks through `EdgeWalkIn`.
  Pods remain forbidden.
- No warning, countdown or letter reveals the pending wave. A short localized
  RP letter appears only when the reinforcements enter the map.
- Both domains retain their exact faction identity and visible color.
- Their vanilla mutual hostility is ignored only while the cooperative attack
  is active. The persistent world relation is never rewritten.
- The allied force follows the primary force into withdrawal. Temporary
  cooperation ends when either participating force is no longer present.

## Implementation

- `IncidentWorker_GoauldJaffaNaturalRaid` selects doctrine from the original
  vanilla points, applies the existing relation factor, then prepares the
  bounded budget split before primary pawn generation.
- `GameComponent_GoauldAlliedReinforcementTracker` serializes the silent delay,
  exact domain pair, allied point budget and participating pawn references.
- The delayed wave reuses the validated controlled direct-raid worker with an
  exact faction, exact points, `EdgeWalkIn` and custom localized arrival text.
- A narrow `FactionUtility.HostileTo` Harmony postfix masks hostility only for
  an exact pair recorded as actively cooperating by the tracker.
- Attack-target caches are refreshed when cooperation begins or ends.
- The debug action uses `1200` vanilla points and a `600`-tick delay.

## Automated validation status

- forced C# rebuild succeeds as assembly `0.3.78.0` with zero errors;
- `git diff --check` passes;
- duration audit passes with `104` explicit compatibility keys across `272` C#
  files;
- project consistency passes across `283` Markdown files with no missing local
  link and aligned public/assembly versions;
- the maintainer validated the mandatory in-game checklist.

## Validated in-game result

Use a player-home map with the `Commandement SG-1` storyteller active.

1. Open `Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...`.
2. Run `Create additional test domain` only if the relation report contains
   fewer than two active Goa'uld domains.
3. Run `Set first pair: Alliance`.
4. Run `Force allied natural raid (1200 points, short delay)`.
5. Confirm that the primary Goa'uld raid enters on foot from a map edge and
   that no letter or countdown mentions reinforcements yet.
6. Let the game run for about `600` ticks, approximately ten seconds at normal
   speed.
7. Confirm that a second Jaffa force with a different Goa'uld faction color
   enters from a map edge.
8. Confirm that the arrival itself produces the letter
   `Renforts Goa'uld alliés : <nom du domaine>` with the short RP text.
9. Confirm that both forces attack the colony rather than one another.
10. Open `Show allied reinforcement report` and confirm that it reports one
    active primary/allied pair.
11. Inspect `Player.log` and report any new Harmony, C#, Scribe, raid, Lord,
    translation or pawn-generation error.

The maintainer reported the mandatory test successful:

- the primary raid arrived without advance reinforcement warning;
- the delayed allied wave appeared with a distinct faction color;
- the localized RP letter appeared only at reinforcement arrival;
- both domain forces attacked the colony instead of one another;
- no blocking log error was reported.

## Optional regression checks

These checks remain optional and were not required for local `r1` acceptance.

- Save after the primary raid but before the allied arrival, reload, and confirm
  that the second wave still arrives once with its letter.
- Save after both forces have arrived, reload, and confirm that they remain
  mutually non-hostile and keep their respective colors.
- Let the primary surviving force withdraw and confirm that allied survivors
  also leave instead of continuing an unrelated permanent raid.
- Repeat below `800` final points and confirm that no allied wave is scheduled.
- Switch to another storyteller and confirm that stored alliances do not add
  allied reinforcement waves.

## Next step

Select the next milestone only from the blocking core-completion phase in
`docs/ROADMAP.md`. Repeat its deferred design questions, then create its
dedicated branch from `develop` aligned with `v0.3.78-dev`.
