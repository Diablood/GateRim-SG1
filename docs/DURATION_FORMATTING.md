# Player-facing duration formatting

Status: published in `0.3.71-dev` after final local revision `r5` and the
complete repository-wide audit.

## Contract

Player-facing durations should use RimWorld's localized vanilla formatter rather
than manual division by `2500` or `60000` followed by a fixed unit suffix.

`GR_PlayerFacingDurationUtility.Format(int ticks)` delegates to
`ToStringTicksToPeriodVerbose` with hours and quadrums enabled. RimWorld can
therefore choose localized seconds, hours, days, quadrums or years and combine
units when appropriate.

Developer reports may continue to expose raw ticks, absolute game ticks or
technical values. This milestone changes presentation only: it must not change
stored deadlines, cooldowns, recurrence, expiry ticks or balance.

## Migration strategy

### Direct helper redirection

The validated `r1` slice redirects legacy private helpers used by temporary
world sites, Jaffa-officer extraction and relay reinforcements. Their English
and French strings treat the formatted value as a complete duration and do not
append a second unit.

### Translation compatibility bridge

The cumulative `r2` slice adds a narrowly scoped Harmony postfix for the
formatted `Translate` overloads. It activates only for an explicit allowlist of
GateRim translation keys whose existing callers still pass a rounded hour or
decimal-day value. The bridge:

- reads the known duration argument;
- reconstructs the equivalent tick duration;
- formats it with `GR_PlayerFacingDurationUtility`;
- replaces the exact legacy English or French phrase such as `49 hours`,
  `49 h` or `2 jour(s)` in the final translated text.

Unlisted translation keys are untouched. A mapped key whose argument is not a
numeric legacy duration is also left untouched. This avoids rewriting the large
persistent operation managers while preserving their serialized timers and
call structure.

The `r2` allowlist covers:

- Tok'ra observation and intelligence offers and statuses;
- wounded-agent care and medical-supply handoff;
- diversion, distress-call, delivery and officer-capture offers and statuses;
- secure-communicator request messages, dialogs, disabled reasons and cooldown
  summaries;
- first trusted-mission pending stages;
- the intercepted-threat estimate;
- the legacy hidden-safehouse marker.

### Repository-wide static audit

The cumulative `r3` package adds `tools/check-duration-formatting.cmd`. Its
first run against the cumulative files also identified five decoded-relay
inspection and caravan-command strings that still appended `day(s)` /
`jour(s)` after the already formatted duration. `r3` neutralizes those suffixes.

The check scans every keyed translation in every installed language directory and
fails when a placeholder still appends a fixed hour or day suffix without an
explicit entry in the compatibility bridge. It also reviews C# files for
unapproved manual hour/day converters near player-facing UI construction.

The audit deliberately distinguishes four cases:

- migrated legacy translation keys are accepted only through the explicit
  allowlist in `PlayerFacingDurationTranslationPatches.cs`;
- direct helper methods covered by `PlayerFacingDurationHarmonyPatches.cs` are
  accepted as compatibility implementations;
- raw ticks, absolute ticks and decimal-day ranges remain acceptable in
  developer-only diagnostics;
- direct vanilla calls such as `ToStringTicksToPeriod()` and mechanical
  per-day severity calculations are not manual player-facing converters.

This makes later additions fail visibly when they reintroduce a fixed unit into
a dynamic player-facing duration.

## Regression requirements

- English and French must not duplicate units.
- Sub-day, multi-day, quadrum and year-scale examples must remain readable.
- A zero or negative remaining value must be clamped safely.
- Countdown completion must occur on the same game tick as before.
- Save/reload must preserve every underlying timer.
- Unrelated translations must remain byte-for-byte equivalent after formatting.
- Failed Harmony target resolution or a translation exception is blocking.
- `tools/check-duration-formatting.cmd` must pass without uncovered keyed
  duration surfaces or unapproved player-facing C# converters.


## Audit script compatibility

Cumulative revision `r4` keeps the audit compatible with Windows PowerShell 5.1 by avoiding line-leading `+` operators in multiline expressions. This changes only the validation script, not duration formatting or gameplay.


## Audit correction r5

The first successful Windows run identified seven additional migration entries:
two MissionFramework observation offers, the therapeutic-offer inspection, the
Goa’uld-queen extraction cooldown, the wary diplomatic cooldown and the two
legacy hour/day fallback keys of the Goa’uld battlefield site. The bridge also
recognizes `RimWorld day(s)` and `jour(s) RimWorld`.

The C# heuristic now searches only actual tick-to-hour/day divisions. It no
longer treats arbitrary numeric formatting as a duration converter, skips
developer-action files and explicitly classifies the Jaffa Prim’ta dependency
per-day severity calculation as gameplay mathematics rather than UI formatting.

## Final validation

Final local revision `r5` passes the forced `0.3.71.0` build, the complete
project-consistency check and the Windows PowerShell 5.1 duration audit. The
audit loads `104` explicit migration keys without duplicates and reports no
uncovered fixed-unit translation or unapproved player-facing tick conversion.

Targeted French and English tests confirm the decoded-relay inspection and
commands, framework observation offers, therapeutic-offer inspection,
Goa'uld-queen extraction recovery and Tok'ra diplomatic cooldown. Save/reload
preserves the underlying deadlines, and startup introduces no new Harmony, XML,
translation or C# error. The contract is published under `v0.3.71-dev`.
