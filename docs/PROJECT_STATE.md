# Project state

Current milestone: `0.3.14-dev - Audit Tok'ra identity through death and resurrection` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.13-dev`.
- Dedicated branch: `feature/tokra-death-resurrection-audit`.
- Validated local archive revision: `0.3.14-dev-r1`.
- Final published tag: `v0.3.14-dev`.
- Final commit: `0.3.14-dev - audit Tok'ra identity through death and resurrection`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The existing Tok'ra dual-identity architecture remains coherent when a directly player-controlled host dies, becomes a corpse, is buried, is saved and reloaded while dead or buried, and is resurrected.

Both complete test matrices were validated:

- death with the host personality active;
- death with the symbiote personality active;
- corpse save/load;
- vanilla burial and grave save/load;
- corpse recovery and resurrection;
- preservation of both identities, both backstory records, the active personality and shared skill progression;
- absence of a personality gizmo while dead or buried;
- return of exactly one gizmo after resurrection;
- ten repeated post-resurrection switches without skill loss, duplication or stacking;
- post-resurrection save/load;
- Tok'ra extraction and reimplantation after resurrection;
- Goa'uld and ordinary-human death/resurrection regressions;
- clean `Player.log`.

## Implementation decision

No C# behavior correction was required.

The validated architecture already provides the complete lifecycle persistence path:

- `GoauldSymbioteData` remains deep-saved by the symbiote Hediff component;
- the pawn stored inside its corpse retains both names, both backstory records, the active-personality state and shared progression;
- dead pawns fail the direct-player-control eligibility check, so no switch gizmo is exposed;
- post-load initialization reconnects the saved symbiote data correctly;
- ordinary death does not trigger extraction or ordinary-Hediff-removal restoration;
- resurrection restores normal player control without rerolling or rebuilding either identity.

The corpse and grave may follow the vanilla label generated from the personality active at death. No parallel corpse identity, grave identity or resurrection identity system is needed.

## Documentary result

This milestone also corrected stale `0.3.13-dev` validation and publication wording that remained in the previously published tracking files.

`docs/MILESTONE_PUBLICATION.md` now requires:

- a final consistency review of `PROJECT_STATE.md`, `ROADMAP.md`, `TESTING_CURRENT.md`, `TESTING.md` and `CHANGELOG.md` before the release commit;
- matching metadata and assembly versions;
- a search for obsolete active-milestone wording;
- a review of the files actually stored in `HEAD` after tagging.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- `docs/TESTING.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/CHANGELOG.md`;
- `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`.

No C# source, Def, translation or `docs/wiki/*.md` page was modified. No separate wiki synchronization was required for this milestone.

## Next development base

The next milestone must start from the published tag `v0.3.14-dev` on a new dedicated `feature/...` branch.

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
- Update the procedure files in the same milestone whenever a durable workflow improvement is discovered.
