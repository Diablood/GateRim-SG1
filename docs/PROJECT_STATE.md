# Project state

Current milestone: `0.3.64-dev - Add persistent Goa'uld domain doctrine profiles`
- final revision `r2` rebuilt, validated and published from `v0.3.63-dev`.

## Repository state

- Published base: `v0.3.63-dev`.
- Published branch: `feature/goauld-domain-doctrine-profiles`.
- Last published version and tag: `0.3.64-dev` / `v0.3.64-dev`.
- Technical assembly version: `0.3.64.0`.
- Final local revision: `r2`.
- Publication state: branch commit, annotated tag and separate wiki published.
- The previous experimental `0.3.63-dev` doctrine package was based on
  `v0.3.62-dev` and must not be committed.
- The separate wiki is synchronized and published from `docs/wiki/`.

## Implemented scope

- Add three data-driven doctrine profiles:
  - conquest: direct `4`, abduction `1`, destruction `1`;
  - enslavement: direct `2`, abduction `3`, destruction `1`;
  - scorched earth: direct `2`, abduction `1`, destruction `3`.
- Persist one profile per Goa'uld System Lord faction instance rather than per
  current leader.
- Reconcile older saves and worlds containing several Goa'uld domains.
- Preserve the existing single natural raid incident, vanilla threat points,
  refire delay and contextual eligibility thresholds.
- Apply the profile only to the relative weights of direct assault, abduction
  and destruction after the existing checks.
- Preserve an explicitly supplied Goa'uld faction and select one active domain
  when a natural raid does not yet have one.
- Display the qualitative profile in the normal faction information without
  exposing weights or hidden thresholds.
- Add per-domain developer diagnostics and targeted profile setters.
- Resolve French profile labels and descriptions through explicit keyed
  translations.
- Add `.gitattributes` so normal text remains LF, Windows command scripts remain
  CRLF and binary assets are never converted.
- Remove the obsolete `Tokra-Interaction-Roadmap` entry from the wiki sidebar;
  the documentation-consolidation milestone intentionally deleted that page.

## r2 consistency correction

The first corrected package still carried one obsolete sidebar link copied
from a pre-consolidation snapshot. The project-consistency checker correctly
reported:

```text
docs\wiki\_Sidebar.md:96 -> Tokra-Interaction-Roadmap
```

`r2` removes only that stale navigation entry. No gameplay code, translation,
Def, saved doctrine state or balance value changes from `r1`.

## Validation result

The maintainer confirms the final `r2` state after the corrected `0.3.64.0`
rebuild:

- doctrine assignment and exact `4/1/1`, `2/3/1` and `2/1/3` weights;
- natural-raid integration without frequency, threat-point or threshold change;
- normal faction information with fully French doctrine text;
- save/reload persistence and unchanged faction assignment;
- existing direct, abduction and destruction raid behavior;
- clean `Player.log`;
- successful project-consistency check after removing the obsolete
  `Tokra-Interaction-Roadmap` sidebar link.

The earlier stash based on `v0.3.62-dev` remains obsolete and must never be
reapplied. It may be dropped after the branch, tag and wiki publication are
confirmed and both repositories are clean.

## Next milestone

No next milestone is selected. Future work must start from `v0.3.64-dev` on a
new dedicated branch after reading `docs/ROADMAP.md`.

