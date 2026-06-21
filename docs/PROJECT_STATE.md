# Project state

Current milestone: `0.3.19-dev - Audit cultural backstory skill coverage` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.18-dev`.
- Dedicated branch: `feature/cultural-backstory-skill-coverage`.
- Validated local archive revision: `0.3.19-dev-r1`.
- Final published tag: `v0.3.19-dev`.
- Final commit: `0.3.19-dev - audit cultural backstory skill coverage`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The seven implemented cultural pools were audited against RimWorld's twelve standard skills by using the actual childhood-and-adulthood combinations available to each profile.

The audit confirmed that Goa'uld-aligned Jaffa and Free Jaffa already covered all twelve skills. Demonstrated zero-coverage gaps remained for Tau'ri / SGC, off-world humans, ordinary Goa'uld hosts, Goa'uld System Lords and Tok'ra adulthood.

Eleven targeted adult careers close those gaps:

- three Tau'ri / SGC careers;
- one off-world-human career;
- two ordinary Goa'uld-host careers;
- two Goa'uld System Lord careers;
- three Tok'ra careers.

The catalogue grows from `72` to `83` entries. Low redundancy remains documented but does not automatically create extra content. No Jaffa backstory was added solely to make catalogue sizes symmetrical.

The implementation remains data-driven:

- the shared cultural C# framework is unchanged;
- new Defs and explicit pool additions are handled through XML;
- generated-host origin weights remain `1` and `0.2`;
- existing pawns and saved identities are not rerolled;
- compatible manual editor selections remain untouched.

## Functional validation

The complete focused matrix was validated on local revision `r1`:

- forced rebuild and DLL version `0.3.19.0`;
- clean loading of the eleven backstories, XML additions and French translations;
- appearance of the three SGC careers in the stranded-team pool without vanilla adulthood leakage;
- correct Goa'uld, System Lord and Tok'ra career selection with matching cultural name groups;
- generated historical hosts using the expanded off-world and Tau'ri adult pools while preserving origin weights;
- Jaffa pools remaining unchanged and free of unrelated adulthood leakage;
- ordinary-human starters retaining their vanilla-majority behavior;
- stable names, backstories and skill gains through save/load;
- ten Tok'ra personality switches without stacking, loss or duplication of skills;
- preservation of existing `0.3.18-dev` histories without reroll;
- clean `Player.log` for the tested scope.

No corrective C# or Def revision was required after `r1`.

## Durable outcome

`docs/CULTURAL_SKILL_COVERAGE.md` is now the technical matrix for present and future cultures. Asgard, Nox, Unas and any later culture must receive the same audit once they have meaningful childhood and adulthood pools.

The catalogue wiki now contains `83` backstories and must remain synchronized whenever an entry, description or skill bonus changes.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `1.6/Defs/BackstoryDefs/SG1_CulturalSkillCoverage.xml`;
- `Languages/French/DefInjected/BackstoryDef/SG1_CulturalSkillCoverage.xml`;
- `1.6/Patches/SG1_CulturalSkillCoverage.xml`;
- `1.6/Defs/GeneratedHostOriginDefs/SG1_GeneratedHostOrigins.xml`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/CULTURAL_SKILL_COVERAGE.md`;
- `docs/wiki/Cultural-Backstories.md`.

Because `docs/wiki/Cultural-Backstories.md` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.19-dev` on a new dedicated `feature/...` branch.

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
