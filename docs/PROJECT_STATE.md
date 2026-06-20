# Project state

Current milestone: `0.3.17-dev - Refresh project and wiki presentation` — validated, closed and published.

## Published milestone

- Development base tag: `v0.3.16-dev`.
- Dedicated branch: `feature/project-presentation-refresh`.
- Validated local archive revision: `0.3.17-dev-r2`.
- Final published tag: `v0.3.17-dev`.
- Final commit: `0.3.17-dev - refresh project and wiki presentation`.

The local `r2` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The public presentation now reflects the actual `0.3.x` project state instead of older milestone snapshots:

- the root README is a durable English overview of the mod and its current playable systems;
- the French wiki home documents the project through `0.3.17-dev`;
- `Content-Status.md` distinguishes playable content from future work and includes the cultural framework, the 70-backstory catalogue and persistent Tok'ra dual identity;
- the wiki sidebar is organized into stable thematic categories rather than one long linear list;
- the existing Tok'ra dual-identity and tactical-assessment pages are now present in the navigation;
- the publication procedure now requires every new or renamed wiki page to be assigned to an appropriate sidebar category and checked for missing, obsolete or duplicate links.

No gameplay code, Def, translation, texture or balance value changed in this milestone.

## Validation

The complete focused matrix was validated on local revision `r2`:

- forced rebuild and DLL version `0.3.17.0`;
- RimWorld loading to the main menu without a new GateRim SG-1 error;
- mod metadata version `0.3.17-dev` and unchanged immersive About description;
- removal of stale active-version claims from the README and wiki presentation pages;
- valid repository and wiki links for the tested scope;
- readable thematic sidebar categories in the narrow wiki layout;
- preservation of all previous internal navigation targets without duplication;
- addition of `Tokra-Dual-Identity` and `Tokra-Tactical-Threat-Assessment` to the sidebar;
- accurate separation between current playable systems and future content;
- confirmation that `About/ModIcon.png`, gameplay C#, Defs, translations and textures are unchanged;
- clean `Player.log` for the main-menu smoke test.

No corrective gameplay revision was required after `r2`.

## Files published

- `README.md`;
- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- `docs/wiki/Home.md`;
- `docs/wiki/Content-Status.md`;
- `docs/wiki/_Sidebar.md`.

Because `docs/wiki/*.md` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.17-dev` on a new dedicated `feature/...` branch.

Before selecting it, reread:

- `AGENTS.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- `docs/TESTING_CURRENT.md`.

## Repository rules reminder

- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only one final tag per milestone, without an `-rN` suffix.
- Update procedure files in the same milestone whenever a durable workflow improvement is discovered.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
