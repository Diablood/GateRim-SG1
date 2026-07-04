# Project state

Current milestone: `0.3.63-dev - Consolidate project documentation` - final
local revision `r1` validated and published.

## Repository state

- Published branch: `feature/documentation-consolidation`.
- Published base: `v0.3.62-dev`.
- Last published version and tag: `0.3.63-dev` / `v0.3.63-dev`.
- Technical assembly version: `0.3.63.0`.
- Publication state: branch commit, annotated tag and separate wiki published.

## Scope

- Add `docs/README.md` as the documentation map and lifecycle contract.
- Convert `docs/ROADMAP.md` from accumulated release history into an active
  backlog. Published history remains in `docs/CHANGELOG.md` and Git tags.
- Transfer unresolved queen and Tok'ra cultural-reaction ideas into
  `docs/IDEAS_TO_REVISIT.md`.
- Remove obsolete or redundant documents without creating an archive folder.
- Remove the obsolete Tok'ra interaction-roadmap page from the wiki drafts and
  navigation.
- Add durable documentation-placement rules to `AGENTS.md`.
- Keep `docs/TESTING.md` and detailed subsystem specifications unchanged in
  this first safe pass.

## Files intentionally removed

```text
docs/Content-Status.md
docs/FRENCH_TRANSLATION_AUDIT.md
docs/GOAULD_QUEEN_LARVA_ORIGIN_TODO.md
docs/TOKRA_INTERACTION_ROADMAP.md
docs/WIKI_VISUAL_ASSETS_ROADMAP.md
docs/wiki/Tokra-Interaction-Roadmap.md
```

The separate wiki repository deleted `Tokra-Interaction-Roadmap.md` explicitly
because the synchronization script copies drafts but does not remove obsolete
remote pages automatically.

## Validation checklist

1. Open `docs/README.md` and confirm that current state, active backlog, ideas,
   history, current tests, durable tests and wiki drafts each have one clear
   authoritative location.
2. Open `docs/ROADMAP.md`. Confirm that published milestone checklists before
   `0.3.63-dev` are gone and that open visual, cultural, faction, storyteller,
   Stargate, documentation and maintenance work remains present.
3. Open `docs/IDEAS_TO_REVISIT.md`. Confirm the queen evolution and cultural
   Tok'ra reaction ideas survived the deletion of their old roadmap files.
4. Open `docs/wiki/Tokra.md` and `docs/wiki/_Sidebar.md`. Confirm neither links
   to `Tokra-Interaction-Roadmap`.
5. Run `git status --short` and confirm the six intentional deletions above,
   with no gameplay Def, translation or texture deletion.
6. Run `./tools/check-project-consistency.cmd`, the local Markdown-link audit,
   `git diff --check` and forced build `0.3.63.0`.

No in-game validation is required because the milestone changes documentation
and assembly metadata only.

## Validation state

- Maintainer review: final local revision `r1` approved.
- Forced build `0.3.63.0`: passed with `0` errors and only the existing offline
  NuGet vulnerability-audit warning.
- Project consistency: passed, including `275` local Markdown files with no
  missing target.
- Wiki sidebar: `118` internal targets resolved.
- Diff audit: exactly six documentation deletions and no gameplay, Def,
  translation or texture deletion.

## Next action

Select the next small milestone from the active backlog. It must start from
`v0.3.63-dev` on a dedicated branch. The detailed testing and subsystem-file
consolidation remains deferred until a separate evidence-based audit.
