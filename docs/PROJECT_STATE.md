# Project state

Current milestone: `0.2.53-dev - Consolidate French player wiki`.

## Active development base

- Functional base tag: `v0.2.52-dev`.
- Dedicated branch: `feature/french-player-wiki-consolidation`.
- Planned final tag after local validation: `v0.2.53-dev`.
- Current local test archive revision: `0.2.53-dev-r1`.

## Milestone scope

This milestone changes documentation only. It does not add or alter gameplay.

The French player wiki is consolidated to match the current project state:

- rewrite `docs/wiki/Home.md`;
- correct obsolete milestone and roadmap information;
- move development directions out of the useful-links section;
- rewrite the Tok'ra interaction roadmap through `0.2.52-dev`;
- reorganize `docs/wiki/Content-Status.md` so implemented features are no
  longer listed under planned content;
- translate remaining English player-facing prose in `docs/wiki/*.md`;
- keep proper names, RimWorld identifiers, developer-action names and technical
  values in English when translation would be misleading;
- clean raw or inconsistent labels in `_Sidebar.md`;
- correct clearly obsolete player documentation for Prim'ta age, dependency,
  temperature and implantation, Goa'uld forced or ritual implantation, Tok'ra
  trust, support deliveries, safehouse leads and the decoded mission chain;
- update contradictory FAQ answers that still described implemented systems as
  future work;
- validate internal wiki links and French terminology.

English mirror pages are explicitly outside this milestone. They may be added
later after the French documentation and gameplay have reached a stable public
state.

## Main wiki updates

- `Home.md` now reflects the playable `0.2.x` scope and no longer presents the
  wiki as an early `0.1.6-dev` prototype.
- `Prochain développement majeur` now lists current directions without assigning
  unsupported intermediate version numbers.
- `Liens utiles` contains only navigation and repository references.
- Tok'ra organic-operation, delivery-zone, safehouse-contact and decoded-site
  pages are fully harmonized in French.
- Obsolete English validation notes are removed from player-facing pages.
- Earlier Prim'ta, Goa'uld and Tok'ra pages no longer contradict later
  implemented milestones.
- The FAQ reflects autonomous Goa'uld implantation, Prim'ta dependency,
  deep-freezing penalties and current Tok'ra world events.
- The sidebar uses French labels and exposes the decoded-site and
  reconnaissance pages without raw wiki-link syntax.

## Compatibility

- No Def, save key, enum, C# behavior or gameplay data changes.
- Save compatibility is unchanged from `0.2.52-dev`.
- `About/ModIcon.png` remains untouched.
- `About/About.xml` and assembly metadata advance to `0.2.53-dev` /
  `0.2.53.0` only to identify the milestone consistently.

## Required local validation

1. Build the assembly and confirm version `0.2.53.0`.
2. Review `Home.md`, `Content-Status.md`, `Tokra-Interaction-Roadmap.md`,
   `_Sidebar.md` and every modified Tok'ra page.
3. Confirm that player-facing prose is French while proper names, commands and
   technical identifiers remain readable.
4. Check every internal Markdown link in `docs/wiki/*.md`.
5. Run the durable French-wiki checklist in `docs/TESTING.md`.
6. Synchronize the separate wiki only after local validation.
7. Review `Player.log` to confirm that the metadata-only assembly change adds
   no loading error.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.2.53-dev - consolidate French player wiki`;
- branch: `feature/french-player-wiki-consolidation`;
- annotated tag: `v0.2.53-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by
  Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
