# Project state

Current milestone: `0.3.21-dev - Add cultural starter loadout rules` — functionally validated, closed and published.

## Published milestone

- Development base tag: `v0.3.20-dev`.
- Dedicated branch: `feature/cultural-starter-loadouts`.
- Validated functional archive revision: `0.3.21-dev-r4`.
- Final documentary checker correction: `0.3.21-dev-r6`.
- Final published tag: `v0.3.21-dev`.
- Final commit: `0.3.21-dev - add cultural starter loadout rules`.

The local `r4` suffix identifies only the tested archive revision. It does not appear in the published commit or tag.

## Milestone result

The shared cultural starter framework now controls both candidate restrictions and layered starter equipment through Defs:

- minimum biological age and violence capability remain configurable per cultural starter rule;
- legacy ordered apparel lists remain supported for compatibility;
- weighted apparel slots support mandatory or optional selection, weighted options, optional stuff and normal-quality generation;
- shared `variantGroup` and `variantKey` values keep related pieces coherent while preserving independent randomness between pawns;
- configuration validation reports invalid chances, weights, apparel, stuff and variant groups before generation.

The stranded SG-team scenario now uses the generic cultural rule instead of scenario-specific equipment code:

- mandatory vanilla cloth shirt using `Apparel_BasicShirt`;
- mandatory olive, black or desert SG field pants;
- optional matching SG field jacket;
- mandatory tactical boots, gloves and vest;
- optional field helmet, with the same slot ready for a future SG cap or no headgear;
- one assault rifle, one machine pistol, one autopistol and one pump shotgun as the intended vanilla human firearm set;
- no loose batch of four helmets.

The former combined uniform Defs remain available for save compatibility, but the scenario no longer equips them. The tactical vest now uses the `Shell` layer so the shirt, optional jacket and vest can coexist.

`SG1_StrandedSGTeamStartingGear` remains as a hidden compatibility marker using the generic no-op `ScenPart_CulturalMarker`. The obsolete `Source/GateRimSG1/Scenarios/ScenPart_SGTeamStartingGear.cs` is deleted.

## Functional validation

The complete focused protocol was validated on local revision `r4`:

- project consistency checker passed for `0.3.21-dev`, assembly `0.3.21.0` and `83` backstories;
- the final checker accepts both the preparatory `Version de DLL attendue` wording and the closed-state `Version de DLL validée` wording, and no longer reports a duplicate empty-value failure;
- the intentional Markdown-tab probe was detected, removed and followed by a clean positive rerun;
- forced rebuild produced DLL version `0.3.21.0`;
- the invalid `Apparel_Tshirt` reference found in `r3` is corrected to `Apparel_BasicShirt`;
- SG-team candidates remain at least 20 biological years old and capable of violence;
- every starter receives the mandatory shirt, pants, boots, gloves and vest at normal quality;
- olive, black and desert pants were observed, with optional jackets always matching the same pawn's pants variant;
- starters with and without jackets, and with and without helmets, were observed;
- shirt, jacket and vest layers coexist without silent replacement or visible layer regression in the tested orientations and body types;
- the mixed four-weapon set, bedrolls and normal supplies arrive without four loose helmets;
- a vanilla scenario retains its normal candidate rules and clothing;
- save/reload preserves the generated equipment;
- `Player.log` is clean for the validated scope.

No further functional correction is required after `r4`.

## Durable outcome

Future cultures can define starter restrictions and weighted layered apparel mainly in XML. Future SG uniform variants can be added as weighted options, and the planned SG cap can be added to the existing headgear slot without scenario-specific C#.

The scenario deliberately uses balanced vanilla human firearms. GateRim SG-1 does not need a duplicate Tau'ri weapon line solely because the weapons are human; optional compatibility patches may substitute equivalent weapons from other mods later.

Markdown command examples use `/` in repository-relative paths. The consistency checker rejects literal tab characters in `README.md` and `docs/**/*.md`.

## Files published

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- cultural starter framework and generic scenario marker files under `Source/GateRimSG1/`;
- starter-loadout and apparel patches under `1.6/Patches/`;
- modular SG field-uniform Defs, translations and textures;
- stranded SG-team scenario data;
- `README.md`;
- `tools/check-project-consistency.ps1`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/TESTING.md`;
- `docs/CHANGELOG.md`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/CULTURAL_STARTER_LOADOUTS.md`;
- `docs/PROJECT_CONSISTENCY_CHECKS.md`;
- `docs/MILESTONE_PUBLICATION.md`;
- SG-team equipment and scenario pages under `docs/wiki/`.

Because files under `docs/wiki/` changed, the separate wiki repository must be synchronized as part of the final publication.

## Next development base

The next milestone must start from the published tag `v0.3.21-dev` on a new dedicated `feature/...` branch.

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
