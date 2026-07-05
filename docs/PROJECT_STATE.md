# Project state

Current milestone: `0.3.68-dev - Add open-conflict Goa'uld pressure reduction`
- closed after final local revision `r3`.

## Repository state

- Starting published integration state: `develop` at annotated tag
  `v0.3.67-dev`.
- Validated milestone branch:
  `feature/goauld-open-conflict-pressure-reduction`.
- Published version and final unique tag: `0.3.68-dev` / `v0.3.68-dev`.
- Technical assembly version: `0.3.68.0`.
- Final validated local revision: `r3`.
- Fast-forward integration into `develop`: completed.
- Main repository publication: completed.
- Separate wiki synchronization and publication: completed.
- `main` remains unchanged and reserved for the future stable `1.0.0` line.

## Published scope

- Apply a fixed natural-raid pressure factor of `0.75` to every active Goa'uld
  domain participating in at least one `open conflict` relation.
- Apply that factor only while `SG1_GateRimStoryteller` /
  `Commandement SG-1` is active.
- Keep the factor non-stacking when one domain is engaged in several open
  conflicts.
- Derive the effect from the existing persistent relation tracker without adding
  a new serialized field or schema migration.
- Choose direct, abduction or destruction doctrine from the original vanilla
  storyteller points, then reduce only the points transmitted to ordinary raid
  generation.
- Preserve the shared natural incident chance, earliest day, refire delay,
  doctrine eligibility and relative doctrine weights.
- Exclude every forced path from the automatic factor: extraction reprisals,
  controlled raids, mission attacks and deterministic doctrine regression
  actions retain their exact points.
- Add one explicit developer command that exercises the ordinary natural-raid
  worker with pressure enabled.
- Add read-only per-domain and threat-progression diagnostics showing vanilla
  points, effective points and the derived factor.
- Add no battlefield incident, alliance effect, goodwill change, expansion,
  joint raid, reinforcement or settlement destruction.

## Mechanical contract

For an ordinary natural Goa'uld Jaffa raid:

```text
effective points = max(1, vanilla points × pressure factor)
```

The factor is `0.75` only when:

- the attacking faction is an active Goa'uld System Lord domain;
- that domain belongs to at least one active pair in `open conflict`;
- `Commandement SG-1` is the active storyteller;
- the execution is the ordinary natural incident path or the dedicated
  pressure-enabled developer test.

Every other path uses factor `1.00`.

## Final validation

The final `r3` validation confirmed:

- forced rebuild of `GateRimSG1.dll` in version `0.3.68.0`;
- full project consistency apart from the intentionally deferred changelog
  header before finalization;
- `75%` pressure for both domains of an open-conflict pair under
  Commandement SG-1;
- no stacking when a domain participates in more than one open conflict;
- immediate return to `100%` under Cassandra and after leaving open conflict;
- doctrine eligibility and weights still calculated from original vanilla
  points;
- the dedicated natural-raid test applied and logged the expected reduction;
- deterministic `300`, `800` and `1800` doctrine tests retained exact points;
- extraction reprisals retained stored points and produced no pressure-reduction
  log;
- save/reload preserved the relation and re-derived the same factor without new
  serialized data;
- existing relation transitions, raids, doctrines and ultimatum/reprisal flows
  remained functional;
- `Player.log` contained no new C#, XML, Scribe, faction, storyteller or raid
  error attributable to the milestone.

## Publication model

The validated feature commit is integrated into `develop` with
`git merge --ff-only`. The annotated tag `v0.3.68-dev`, local `develop` and
`origin/develop` must all resolve to that same commit. The temporary feature
branch may be removed after verification.

## Next step

No next milestone is selected yet. The next branch must start from published
`develop` after `v0.3.68-dev`, following `docs/BRANCHING_WORKFLOW.md`.
