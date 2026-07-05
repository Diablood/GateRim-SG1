# Project state

Current milestone: `0.3.76-dev - Reconcile future roadmap and visual debt`

- Starting point: published `develop` aligned with annotated tag
  `v0.3.75-dev` at commit
  `010570c1349b52651e634441a8d159160c400b7d`.
- Active branch: `feature/future-roadmap-reconciliation`.
- Final local revision: `r2`, validated and ready for publication.
- Revision `r1` used a context-sensitive `git apply` patch for three long
  documents; it failed against the maintainer's working tree and is superseded.
- Revision `r2` supplies complete replacement files only and permanently bans
  patch-based ordinary deliveries.
- Target assembly version: `0.3.76.0`.
- This milestone changes documentation and version metadata only.
- No gameplay, Def, translation, texture, save data or balancing is intentionally
  changed. The wiki receives only version/status synchronization and a corrected
  summary of future directions.

## Reconciliation scope

- Remove already published work from the active backlog, including the Tok'ra
  observation-scope rework, debug-menu reorganization, operation-pool audit,
  world icons, threat audit, relation factors and both Jaffa-officer milestones.
- Keep temporary-art debt explicit even when the related mechanic is complete.
- Reserve a later complete art pass that replaces every placeholder and
  temporary texture with final-quality work.
- Separate permanent regression contracts from future feature milestones.
- Split alliance extensions, strategic safeguards, territorial consequences,
  equipment, races, Replicators, optional Ideology/Royalty audits, world
  generation, Stargate progression and transport rings into individual future
  milestones.
- Add a first player-platform transport-ring foundation and a separate later
  mission/hostile-use extension.
- Defer unresolved design decisions to the launch of each concerned milestone;
  ask those questions again before implementation rather than assuming answers
  in advance.
- Keep speculative queen evolution, culture-specific Tok'ra reactions,
  sarcophagus, optional-DLC compatibility, adult-symbiote confinement and
  advanced kara kesh functions in `IDEAS_TO_REVISIT.md`.

## Validation result

Final revision `r2` is validated:

- forced build succeeds as assembly `0.3.76.0` despite no C# behavior change;
- duration audit remains at `104` unique compatibility keys;
- project-consistency check passes;
- all local Markdown links resolve;
- the roadmap contains no completed feature as an unchecked future item;
- transport rings and final texture replacement are explicitly planned;
- no gameplay file outside version metadata is modified;
- RimWorld starts with version `0.3.76-dev` and produces no new log error;
- ordinary delivery archives now use complete replacement files only, with no
  `.patch`, applicable diff or `git apply` instruction.

## Next step after publication

No gameplay milestone after `0.3.76-dev` is automatically reserved. Select one
individual entry from `docs/ROADMAP.md`, repeat its deferred design questions,
record the answers in the relevant documents, then create its dedicated branch
from `develop` aligned with `v0.3.76-dev`.
