# Project state

Current milestone: `0.3.15-dev - Add unified cultural identity diagnostics` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.14-dev`.
- Dedicated branch: `feature/cultural-identity-diagnostics`.
- Validated local archive revision: `0.3.15-dev-r1`.
- Final published tag: `v0.3.15-dev`.
- Final commit: `0.3.15-dev - add unified cultural identity diagnostics`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

GateRim SG-1 now provides one unified, read-only cultural identity report for the selected pawn.

The same report is available through:

- one grouped RimWorld developer action;
- one button in the existing advanced GateRim SG-1 settings section.

The report exposes the current state supplied by the existing authoritative systems:

- name, `ThingID`, race, xenotype, `PawnKindDef`, faction and active backstories;
- every matching cultural profile, the selected profile and the resolved name group in `NonPlayer` and `PlayerStarter` contexts;
- Jaffa physiology, Prim'ta state and intrinsic forehead mark;
- contextual Free Jaffa, Goa'uld-domain Jaffa, marked-Jaffa, Goa'uld-host, Tok'ra-host, nearby-System-Lord and System-Lord-host flags;
- persistent adult-symbiote data, including the distinct Tok'ra host and symbiote identities when present.

## Functional validation

The complete focused matrix was validated on local revision `r1`:

- forced rebuild and DLL version `0.3.15.0`;
- main-menu loading without new red errors;
- translated rejection when no pawn is selected;
- ordinary human;
- Free Jaffa;
- Goa'uld-aligned Jaffa;
- active Goa'uld host;
- pre-joined Tok'ra before and after personality switching;
- identical report access through developer actions and advanced mod settings;
- correct hiding when both developer mode and the advanced option are disabled;
- repeated inspection, save/load and manual-name preservation;
- no change to names, backstories, factions, marks, Prim'ta, symbiote data or active personality merely from opening the report;
- clean `Player.log` for the tested scope.

No corrective `r2` code revision was required.

## Architecture decision

The diagnostic remains a consumer of existing services rather than a new identity source.

It evaluates the pawn when opened and does not cache, reserve, normalize, repair or migrate cultural data. A discrepancy found later must therefore be reproduced and corrected in the authoritative subsystem that owns the value, not hidden by synchronization inside the report.

This completes the roadmap point requiring a cross-system check between cultural names, backstories, factions, Jaffa marks and social identities.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `Source/GateRimSG1/GateRimSG1Mod.cs`;
- `Source/GateRimSG1/Culture/CulturalIdentityDebugActions.cs`;
- `Source/GateRimSG1/Culture/CulturalIdentityDebugUtility.cs`;
- `Languages/English/Keyed/SG1_CulturalIdentityDiagnostics.xml`;
- `Languages/French/Keyed/SG1_CulturalIdentityDiagnostics.xml`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/CULTURAL_IDENTITY_DIAGNOSTICS.md`.

No `docs/wiki/*.md` page was modified. No separate wiki synchronization is required for this milestone.

## Next development base

The next milestone must start from the published tag `v0.3.15-dev` on a new dedicated `feature/...` branch.

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
