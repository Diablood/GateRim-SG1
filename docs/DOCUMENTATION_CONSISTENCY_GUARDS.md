# Documentation consistency guards

Version: `0.3.105-dev`
Status: implemented, regression-tested and integrated into the aggregate project
consistency command.

## Purpose

The published `0.3.99-dev` state proved that matching version headers were not
enough. Intermediate changelog entries, durable test sections, visual-register
summaries and final publication wording could drift while the previous aggregate
check still passed.

`tools/check-documentation-consistency.ps1` is a read-only audit dedicated to
those contracts.

## Commands

Run the negative fixtures:

```powershell
.\tools\test-documentation-consistency-guards.cmd
```

Audit an ordinary working revision:

```powershell
.\tools\check-documentation-consistency.cmd
```

Audit the final state:

```powershell
.\tools\check-documentation-consistency.cmd -RequirePublicationReady
```

The aggregate equivalents are:

```powershell
.\tools\check-project-consistency.cmd
.\tools\check-project-consistency.cmd -RequirePublicationReady
```

## Protected contracts

### Published changelog continuity

The checker enumerates real `v*-dev` Git tags from `v0.3.67-dev` onward. Every
published tag through the authoritative current version must have exactly one
level-two heading in `docs/CHANGELOG.md`. The first heading must match
`About/About.xml`, and guarded headings must stay in descending order.

Legacy duplicate headings before `0.3.67-dev` are intentionally outside this
contract.

### Durable testing continuity

`docs/TESTING.md` must contain exactly one level-two section mentioning the
current milestone and every published tag from `v0.3.93-dev` onward. Earlier
published milestones remain valid durable references when present, but the
checker does not retroactively require one section for every older tag.

### Current-version alignment

The authoritative public and assembly versions are compared with:

- `Source/GateRimSG1/GateRimSG1.csproj`;
- `README.md`;
- `docs/PROJECT_STATE.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/CHANGELOG.md`;
- `docs/wiki/Home.md`;
- `docs/wiki/Content-Status.md`;
- `docs/VISUAL_ASSET_REGISTER.md`;
- `docs/PROJECT_CONSISTENCY_CHECKS.md`;
- this document.

### Visual-register self-consistency

The checker parses only the table between `LOCAL_ASSET_TABLE_START` and
`LOCAL_ASSET_TABLE_END`. It derives file, family, status and priority totals and
rejects:

- summary values that disagree with table rows;
- incorrect final-category arithmetic;
- asset rows outside the marked table;
- obsolete milestone status;
- volatile branch metadata;
- known obsolete findings.

The adult symbiote and Goa'uld queen families are final. Each must remain in
the visual whitelist with three byte-identical protected wiki copies, while
the obsolete shared single-image placeholder must remain absent.

### Roadmap role

`docs/ROADMAP.md` must define `Prochain jalon décidé`. Published milestone
history and versioned milestone headings are rejected because completed work
belongs in `docs/CHANGELOG.md`.

### Procedure protection

`docs/MILESTONE_PUBLICATION.md` must require the regression fixtures and the
aggregate `-RequirePublicationReady` gate. It must also forbid temporary
PowerShell scripts at repository root.

### Publication-ready wording

Final mode rejects unfinished current-state wording and requires the validated
DLL label, final annotated tag and an explicit validated or published state.

## Regression fixtures

The fixture suite proves that a valid synthetic repository passes and that the
following mutations fail:

- missing or duplicated guarded changelog milestone;
- missing durable testing section;
- stale or internally inconsistent visual register;
- visual row outside the marked table;
- volatile visual-register branch;
- published history reintroduced into the roadmap;
- missing publication-ready procedure command;
- unfinished final-state wording.
