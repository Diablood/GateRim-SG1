# Current project state

Current milestone: `0.3.50-dev - Add GateRim faction world icons` - locally validated, branch publication authorized.

## Repository state

- Starting tag: `v0.3.49-dev`.
- Active branch: `feature/faction-world-icon-overhaul`.
- Last published version: `0.3.49-dev`.
- Last published tag: `v0.3.49-dev`.
- Target version: `0.3.50-dev`.
- Technical assembly version: `0.3.50.0`.
- Local revision: `r2`.
- Publication status: final branch commit and push authorized; `docs/wiki/` changes require wiki synchronization. The final tag `v0.3.50-dev` has not been created in this commit/push step.

## Current scope

This milestone replaces the remaining vanilla world-faction icons used by visible GateRim SG-1 factions with dedicated, tintable silhouettes:

- `SG1_FreeJaffa` now points to `World/WorldObjects/Expanding/SG1_FreeJaffa`;
- `SG1_GoauldSystemLordPrototype` now points to `World/WorldObjects/Expanding/SG1_GoauldSystemLords`;
- `SG1_PlayerSGCExpedition` now points to `World/WorldObjects/Expanding/SG1_SGCExpedition`;
- `SG1_Tokra` keeps the existing unique `World/WorldObjects/Expanding/SG1_Tokra` icon validated in `0.3.49-dev`.

The new PNG files are monochrome white/alpha assets. They deliberately do not bake faction colors into the texture so RimWorld can still apply the vanilla faction-color variation when several copies of the same faction are added during world creation.

## Technical approach

No C# behavior change is required for the icons. RimWorld 1.6 already supports `FactionDef.factionIconPath` for the world-faction UI. This revision changes XML paths and texture assets only, plus the standard version metadata.

The dedicated Tok'ra icon and the Harmony warning patch from `0.3.49-dev` are preserved unchanged.

Revision `r1` validated the faction-color behavior, including duplicate-faction tint variation, but the icons were not readable enough at the final in-game size. Revision `r2` keeps the same XML paths and color behavior, but replaces the three new PNGs with simpler silhouettes, fewer internal details and thick dark outlines closer to RimWorld's UI texture language.

## Files currently expected in the milestone

- `1.6/Defs/FactionDefs/SG1_FreeJaffa.xml`
- `1.6/Defs/FactionDefs/SG1_GoauldSystemLordPrototype.xml`
- `1.6/Defs/FactionDefs/SG1_PlayerSGCExpedition.xml`
- `Textures/World/WorldObjects/Expanding/SG1_FreeJaffa.png`
- `Textures/World/WorldObjects/Expanding/SG1_GoauldSystemLords.png`
- `Textures/World/WorldObjects/Expanding/SG1_SGCExpedition.png`
- `About/About.xml`
- `Source/GateRimSG1/GateRimSG1.csproj`
- `docs/CHANGELOG.md`
- `docs/FACTION_WORLD_ICONS.md`
- `docs/PROJECT_STATE.md`
- `docs/ROADMAP.md`
- `docs/TESTING.md`
- `docs/TESTING_CURRENT.md`
- relevant README and `docs/wiki/` pages

## Mandatory r2 test requested

Load order:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Manual checklist:

1. From the main menu, open `New colony`.
2. Select the scenario `Équipe SG isolée` / `Stranded SG team`.
3. Continue to `Create world`.
4. In the `Factions` section, verify that `Jaffa libres` / `Free Jaffa` uses the new Free Jaffa icon, not the vanilla house.
5. Verify that `Domaines des Grands Maîtres Goa'uld` / `Goa'uld System Lord domains` uses the new Goa'uld pyramid/serpent icon, not the vanilla pirate outpost.
6. Verify that `Tok'ra` still uses its existing dedicated circular Tok'ra icon.
7. Click `Add...` and add at least two extra `Jaffa libres` / `Free Jaffa` entries.
8. Click `Add...` and add at least two extra `Domaines des Grands Maîtres Goa'uld` / `Goa'uld System Lord domains` entries.
9. Verify that repeated copies of the same faction keep the same silhouette but receive visible vanilla color variations, such as lighter or darker faction tints.
10. Generate the world and start the scenario on any valid tile.
11. Once in game, open the bottom `Factions` tab and verify that `expédition du SGC` / `SGC expedition` uses the new SGC shield-and-gate icon where RimWorld displays the player faction icon.
12. Check `Player.log`.

Expected result: the four GateRim SG-1 faction identities are visually distinct at the final UI size, duplicate world-faction entries still vary by faction tint, the Tok'ra warning from `0.3.49-dev` still works if `Tok'ra` is removed, and `Player.log` contains no new GateRim SG-1 texture, XML or Harmony error.

## Validation status

- `git diff --check`: passed, with only the usual CRLF normalization warnings.
- `.\tools\check-project-consistency.cmd`: passed.
- Forced rebuild `0.3.50.0`: passed with `0` warnings and `0` errors.
- In-game r1 feedback: faction-color variation is good, but the new icons are too detailed and too thin at final size.
- In-game r2 icon validation: passed. The simplified thick-outlined icons are accepted; the remaining slight color variation is RimWorld's vanilla faction-tint behavior and is considered correct for this milestone.
- Branch commit/push: authorized by the maintainer.
- Wiki synchronization: required because `docs/wiki/` changed.
