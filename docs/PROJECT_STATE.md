# Project state

Current milestone: `0.3.73-dev - Add bounded Goa'uld alliance raid-strength bonus`

- Final local revision: `r1`.
- Forced build `0.3.73.0`, focused functional validation, save/reload coverage and
  required regressions are validated.
- The milestone branch is integrated by fast-forward into `develop`; annotated
  tag `v0.3.73-dev` and the updated wiki sources are published.

## Repository state

- Published starting point: `develop` exactly aligned with annotated tag
  `v0.3.72-dev` at commit
  `11b2c2f43d5978fb6f000ba415b79b2d52dde1d8`.
- Final milestone branch: `feature/goauld-alliance-raid-strength`.
- Published development version: `0.3.73-dev`.
- Technical assembly version: `0.3.73.0`.
- `develop` and annotated tag `v0.3.73-dev` identify the same final commit.
- `main` remains reserved for the first stable `1.0.0` line.

## Published scope

- Extend the existing relation-derived natural-raid point modifier instead of
  adding another incident, scheduler or serialized tracker.
- While `Commandement SG-1` is active, a Goa'uld domain participating in at
  least one alliance uses a fixed `1.10` factor for its ordinary natural Jaffa
  raid points.
- Keep the existing open-conflict factor at `0.75`.
- Resolve open conflict before alliance when a domain participates in both;
  factors are never multiplied, averaged or stacked.
- Keep doctrine eligibility and weighting based on the original vanilla points,
  then apply the final relation factor only to the generated force.
- Keep incident chance, earliest day, shared refire delay, doctrine weights and
  contextual thresholds unchanged.
- Keep Cassandra, Phoebe, Randy and compatible modded storytellers at `1.00`.
- Keep controlled raids, deterministic doctrine tests, extraction reprisals,
  intercepted threats, missions, sites and settlement defenses outside the
  modifier.
- Correct the distinction between an ordinary storyteller execution and a
  caller that was already forced before entering the shared raid worker.
- Expand developer diagnostics with alliance state, open-conflict precedence,
  both configured factors and deterministic relation setup actions.
- Add no new save field; all effects remain derived from persistent faction-pair
  relations.

## Validation result

Final local revision `r1` is validated:

- forced build succeeds with assembly `0.3.73.0`;
- the duration audit still passes with `104` unique compatibility keys;
- the global project-consistency check passes;
- alliance-only domains report and use `1.10`;
- open-conflict domains report and use `0.75`;
- mixed conflict/alliance domains resolve to `0.75`;
- multiple alliances remain capped at `1.10`;
- other storytellers resolve every domain to `1.00`;
- ordinary natural execution receives the factor despite the shared worker's
  internal forced flag;
- externally forced doctrine tests retain their exact historical points;
- save/reload preserves the relation-derived result;
- `Player.log` contains no new C#, Harmony, XML, Scribe, raid-generation or Lord
  error.

## Next step

No `0.3.74-dev` gameplay milestone or branch is reserved. After publication,
select the next decided item from `docs/ROADMAP.md`, create its dedicated branch
from `develop` aligned with `v0.3.73-dev`, and update this handoff before
implementation.
