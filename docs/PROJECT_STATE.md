# Project state

Current milestone: `0.3.71-dev - Standardize player-facing duration formatting`

- Current cumulative local revision: `r5`.
- `r1` build and functional validation: passed according to the user.
- `r2` build and functional validation: passed according to the user.
- `r3` added the final static audit and corrected five remaining decoded-relay
  translation suffixes, but its PowerShell script failed to parse on Windows
  PowerShell 5.1 before the audit could run.
- `r4` corrected the audit-script parser failure and successfully exposed the
  remaining real omissions and overly broad C# heuristics.
- `r5` migrates seven additional legacy translation keys and narrows the C#
  detector so existing vanilla formatters, mechanical per-day calculations and
  developer-only actions are classified correctly; validation is pending.

## Repository state

- Published starting point: `develop` exactly aligned with annotated tag
  `v0.3.70-dev` at commit
  `a54ab3ff7db947e8b5dfcf6a34338810e4caf28e`.
- Active milestone branch: `feature/standardize-duration-formatting`.
- Current development version: `0.3.71-dev`.
- Technical assembly version: `0.3.71.0`.
- `main` remains reserved for the first stable `1.0.0` line.
- No commit, fast-forward integration, tag or publication is authorized before
  complete local validation.

## Validated r1 scope

The first cumulative revision introduced
`GR_PlayerFacingDurationUtility.Format(int ticks)` and redirected legacy helper
methods used by:

- the decoded Tok'ra relay site;
- the Tok'ra distress-call site;
- the Tok'ra introduction recovery site;
- the temporary-base delivery site;
- the Jaffa-officer capture and extraction flow;
- the Goa'uld open-conflict world battlefield;
- the Tok'ra relay reinforcement countdown.

The user reported the `r1` functional test as successful.

## Revision r2 scope

The second cumulative revision adds an explicit translation-key compatibility
bridge. It post-processes only known GateRim strings that still receive legacy
rounded-hour or decimal-day arguments, replacing their complete English or
French duration phrase with the shared vanilla-formatted period.

Covered surfaces now also include:

- all eight published Tok'ra operation offer families and their active status
  summaries;
- observation, intelligence, wounded-agent, medical-supply, diversion,
  distress-call, delivery and officer-capture timing messages;
- secure-communicator cooldown messages, dialogs, disabled reasons and detailed
  status lines;
- pending stages of the first trusted Tok'ra mission;
- intercepted-threat arrival estimates;
- the legacy hidden-safehouse world marker.

The bridge uses a strict allowlist. Unlisted translation keys and non-numeric
arguments remain untouched. Stored ticks, deadlines, cooldowns, recurrence,
save data and balance remain unchanged. Raw technical durations remain allowed
in developer reports.

## Revision r3 scope

The third cumulative revision adds a repository-wide duration-formatting audit:

- scan every `Languages/*/Keyed/*.xml` file;
- reject a translated placeholder followed by a fixed hour/day suffix unless
  the exact key is listed in the compatibility bridge;
- reject duplicate bridge keys;
- scan all C# sources for an unapproved manual hour/day converter near
  player-facing UI code;
- keep developer-only raw timing reports exempt;
- correct five decoded-relay inspection and caravan-command translations that
  still appended a fixed day suffix to a complete vanilla-formatted duration;
- change no gameplay code, timer or save state from the functionally validated
  `r2` revision.

Required `r5` validation:

1. Run `git diff --check`.
2. Build assembly `0.3.71.0` with `build.cmd`.
3. Run `tools\check-duration-formatting.cmd`.
4. Run `tools\check-project-consistency.cmd`; only the changelog mismatch is
   expected before finalization.
5. Recheck the decoded-relay world-site inspection and its reconnaissance and
   sabotage command descriptions in French and English.
6. Recheck one MissionFramework observation offer, one therapeutic-offer
   inspection, the Goa'uld-queen extraction cooldown and the Tok'ra diplomatic
   cooldown.
7. Start RimWorld once and confirm no new Harmony, XML or translation error. A
   full replay of the other `r2` scenarios is not required.

## Next step

After the `r5` audit and targeted duration retest pass, create one final cumulative ZIP that updates the
changelog, durable regression suite, project state, roadmap and wiki. Only that
final documentation package authorizes commit, fast-forward integration, tag
and publication.
