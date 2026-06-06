# Changelog

## 0.1.9-dev
- Remove the invalid `<wildness>` field from the free Goa'uld symbiote `RaceProperties`.
- Keep developer-mode spawning as the isolated test method.
- Preserve the current player wiki because this correction does not change player-facing behavior.

## 0.1.8-dev
- Add the XML-only `SG1_GoauldSymbiote` animal-style pawn prototype.
- Add a weak bite attack and disable natural biome spawning.
- Add a temporary local sprite for the free symbiote.
- Add French `DefInjected` translations.
- Update player-wiki drafts for the free-symbiote prototype.
- Add `tools/sync-wiki.sh` to publish drafts directly at the wiki-repository root.

## 0.1.7-dev
- Add the XML-only `SG1_GoauldHost` xenotype prototype.
- Add the `SG1_NaquadahBlood` marker gene.
- Add the provisional `SG1_GoauldLongevity` gene with `LifespanFactor ×5`.
- Add temporary local icons for the new Goa'uld genes.
- Add French `DefInjected` translations for the Goa'uld host prototype.
- Update player-wiki drafts for the Goa'uld host foundation.

## 0.1.6-dev
- Add versioned player-wiki drafts under `docs/wiki/`.
- Add initial player pages for setup, content status, Jaffa, Goa'uld and symbiotes.
- Add `_Sidebar.md` and `_Footer.md` for the GitHub wiki.
- Document the publication workflow for the separate `GateRim-SG1.wiki` repository.
- Add the GitHub wiki link to the main README.

## 0.1.5-dev
- Remove leading and trailing whitespace from the `SG1_Jaffa` description.
- Normalize French `DefInjected` values to prevent unintended whitespace.
- Replace the unresolved `UI/Icons/Genes/Gene_Robust` path.
- Add a local temporary icon for `SG1_JaffaPhysiology`.
- Document minimal isolated testing and the third-party `Ability` category conflict.

## 0.1.4-dev
- Add French `DefInjected` translations for `SG1_Jaffa`.
- Add French `DefInjected` translations for `SG1_JaffaPhysiology`.
- Add French `DefInjected` translations for `SG1_JaffaLongevity`.
- Document the localization workflow.

## 0.1.3-dev
- Add the `SG1_JaffaLongevity` gene.
- Set Jaffa lifespan expectancy to `150%` with `LifespanFactor ×1.5`.
- Add the longevity gene to the `SG1_Jaffa` xenotype.
- Document the future migration of longevity to the symbiote system if appropriate.

## 0.1.2-dev
- Remove the forced `Body_Hulk` gene from `SG1_Jaffa`.
- Add the custom `SG1_JaffaPhysiology` gene.
- Add a temporary `+15` carrying-capacity effect without imposing a visible body shape.
- Document future weighted body-type generation tuning.

## 0.1.1-dev
- Add the `SG1_Jaffa` prototype xenotype.
- Declare the Biotech dependency.
- Add Jaffa design notes and a manual test checklist.
- Update the project roadmap.

## 0.1.0-dev
- Initialize the GateRim SG-1 repository skeleton.
- Add RimWorld 1.6 directory layout.
- Preserve the custom mod icon.
