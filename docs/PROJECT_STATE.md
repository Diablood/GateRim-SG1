# Project state

Current milestone: `0.3.77-dev - Establish the core-faction completion gate`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.76-dev` at commit
  `2ddde01f5e7e743d502210fab3cdd9e0d04878e3`.
- Active branch: `feature/core-faction-completion-gate`.
- Final local revision: `r1`, validated and ready for publication.
- Target assembly version: `0.3.77.0`.
- This milestone changes documentation and version metadata only.
- No gameplay, Def, translation, texture, save data or balancing is intentionally
  changed.

## Priority decision

The project remains in a blocking core-completion phase until the existing
Tok'ra, Goa'uld/Jaffa and Tau'ri/SGC foundation is explicitly closed.

This phase includes:

- targeted Tok'ra stabilization and presentation while keeping the published
  pool closed at eight recurrent operations;
- all decided Goa'uld, Jaffa and Free Jaffa strategic, territorial, equipment
  and presentation milestones;
- all decided Tau'ri/SGC equipment and presentation milestones;
- shared visual finalization, economy, debug/RP and documentation/test audits;
- transport-ring foundations and their bounded extensions;
- Stargate technological foundations, the first bounded off-world expedition and
  the functional Stargate milestone.

The following expansion phase remains blocked until that gate is closed:

- Asgard;
- Nox;
- Unas;
- Replicators;
- optional Ideology and Royalty integration audits;
- the all-GateRim world preset.

Weighted Tok'ra host origins remain a dependent enrichment milestone after
several new cultures exist. They do not keep the current Tok'ra core open.

## Inheritance rule

Every later people, faction or threat must reuse the foundations already
published where applicable: cultural profiles, names, backstories, faction and
world presentation, mission definitions, threat scaling, persistence, save
compatibility, diagnostics, documentation and regression tests. A parallel
replacement framework requires an explicit architecture decision.

## Closure rule

The core gate can close only when every blocking milestone is one of:

1. validated and published;
2. explicitly removed from the decided roadmap;
3. explicitly deferred to `IDEAS_TO_REVISIT.md` as non-blocking.

An unanswered design question, silent omission or unchecked roadmap entry does
not count as closure.

## Validation result

Final revision `r1` is validated:

- forced build succeeds as assembly `0.3.77.0`;
- duration audit remains at `104` unique compatibility keys;
- project-consistency check passes;
- all local Markdown links resolve;
- the roadmap visibly separates the blocking core phase from the inherited
  expansion phase;
- Asgard, Nox, Unas, Replicators and the all-GateRim preset are explicitly
  blocked before core closure;
- weighted Tok'ra host origins are documented as a later dependent enrichment;
- no gameplay file outside version metadata is modified;
- RimWorld starts with version `0.3.77-dev` and produces no new log error.

## Next step after publication

Select the next gameplay milestone only from the blocking core-completion phase
of `docs/ROADMAP.md`. Repeat its deferred design questions, record the answers,
then create its dedicated branch from `develop` aligned with `v0.3.77-dev`.
