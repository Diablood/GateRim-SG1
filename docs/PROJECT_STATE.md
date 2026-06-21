# Project state

Current milestone: `0.3.22-dev - Add an SG-team field cap` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.21-dev`.
- Dedicated branch: `feature/sg-team-field-cap`.
- Validated functional archive revision: `0.3.22-dev-r1`.
- Final published tag: `v0.3.22-dev`.
- Final commit: `0.3.22-dev - add an SG-team field cap`.

The local `r1` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The existing weighted cultural starter loadout now includes a second headgear option without scenario-specific C#:

- lightweight black SG-team field cap;
- dedicated ground graphic and north, south, east and west worn graphics;
- English Def text and French translation;
- discreet SGC insignia;
- crafting after SGC field-equipment research for `20` cloth with Crafting `2`;
- negligible protection compared with the SG field helmet;
- corrected field-helmet description, which no longer refers to four loose helmets supplied by the scenario.

The cultural headgear slot keeps its `0.6` selection chance and equal option weights. Each starter therefore has the following theoretical outcomes:

```text
30% field helmet
30% field cap
40% no headgear
```

The helmet and cap share the same head layer and cannot be worn together. The rest of the validated SG-team loadout remains unchanged.

## Functional validation

The complete focused protocol was validated on local revision `r1`:

- project consistency checker passed for `0.3.22-dev`, assembly `0.3.22.0` and `83` backstories;
- forced rebuild produced DLL version `0.3.22.0`;
- RimWorld reached the main menu without new Def, translation or texture errors;
- starters with a field helmet, field cap and no headgear were all observed;
- the cap rendered correctly from north, south, east and west on the tested body types;
- helmet and cap remained mutually exclusive;
- mandatory shirt, pants, boots, gloves and vest, plus the optional matching jacket, remained unchanged;
- the balanced vanilla firearm set and normal scenario supplies remained unchanged;
- save/reload preserved the selected cap and the rest of the starter equipment;
- a vanilla scenario received no SG-team headgear or starter rules;
- `Player.log` was clean for the validated scope.

No further functional correction is required after `r1`.

## Durable outcome

The SG-team headgear slot now demonstrates the intended XML-only extension path for future cultural starter equipment. Additional compatible headgear options can be added through weighted Def entries without introducing scenario-specific code.

Weighted outcomes remain probabilities rather than guaranteed ratios on a small sample. Future visual additions must still be checked in all four directions, on several body types and through save/reload.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `1.6/Defs/ThingDefs_Apparel/SG1_SGTeamFieldCap.xml`;
- `1.6/Defs/ThingDefs_Apparel/SG1_SGTeamFieldHelmet.xml`;
- `1.6/Patches/SG1_CulturalStarterLoadouts.xml`;
- French ThingDef translations for the cap and helmet;
- five cap textures under `Textures/Things/Pawn/Humanlike/Apparel/SGTeamFieldCap/`;
- `README.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_STARTER_LOADOUTS.md`;
- updated SG equipment and scenario pages under `docs/wiki/`.

Because files under `docs/wiki/` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.22-dev` on a new dedicated `feature/...` branch.

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
- Use `/` in repository-relative PowerShell paths written in Markdown.
- Run the project consistency checker before every final commit.
- Synchronize the separate wiki only when at least one `docs/wiki/*.md` file changed.
