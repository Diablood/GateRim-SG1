# Mission site world icons

Version: `0.3.51-dev`

## Scope

This milestone replaces generic world-map symbols with six dedicated mission-site types. Revision `r1` retained the existing vanilla `Town` and `ItemStash` icons and is rejected because it did not implement the requested semantic grouping.

## Final icon types

| Icon type | Technical targets | Texture path |
| --- | --- | --- |
| Tok'ra clandestine contact | `SG1_TokraHiddenSafehouseMarker`, `SG1_TokraHiddenSafehouseSitePart` | `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` |
| Goa'uld encrypted objective | `SG1_TokraIntroductionArtifactWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` |
| Goa'uld relay sabotage | `SG1_TokraDecodedMissionWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` |
| Tok'ra distress signal | `SG1_TokraDistressCallWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` |
| Tok'ra logistics rendezvous | `SG1_TokraTemporaryBaseDeliverySite` | `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` |
| Jaffa officer field position | `SG1_TokraJaffaOfficerCaptureSite` | `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` |

The safehouse marker and revealed safehouse use one silhouette because they represent two stages of the same contact. Every hidden distress-call outcome uses one signal icon so its result remains undisclosed until arrival.

## Visual rules

- simple silhouette readable at RimWorld world-map size;
- restrained color families for Tok'ra, Goa'uld and Jaffa identity;
- transparent background and thick near-black outline;
- very limited internal detail;
- no text or faction-tint dependency.

All six assets are `128x128` PNG files. Their silhouettes and mission symbolism were approved before XML integration; colored fills were then explicitly authorized because mission-site icons do not need the faction-copy tint behavior required by `0.3.50-dev`.

## Implementation

The final selection is entirely Def-driven. `WorldObjectDef.expandingIconTexture` and `SitePartDef.expandingIconTexture` use the dedicated colored assets. Every `WorldObjectDef.texture` and the safehouse `SitePartDef.siteTexture` use vanilla `GenericSite`. This prevents the custom UI pictograms from being rotated as close-zoom world geometry. No runtime selector or Harmony patch is used.

The developer submenu creates the real objects for validation:

```text
Actions de débogage > GateRim SG-1 > Tok'ra... > Mission-site icon tests...
```

## Validation

1. Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1`.
2. Use a Tok'ra-enabled colony with a powered Tok'ra secure communicator.
3. Open `Actions de débogage` > `GateRim SG-1` > `Tok'ra...` > `Mission-site icon tests...` > `Independent arcs...`.
4. Use these exact buttons and verify their expanded custom icon plus vanilla close-zoom rendering:
   - `Create SG1_TokraHiddenSafehouseMarker`;
   - `Create SG1_TokraHiddenSafehouseSite`;
   - `Create SG1_TokraIntroductionArtifactWorldSite`;
   - `Create SG1_TokraDecodedMissionWorldSite`.
5. Return to `Mission-site icon tests...` > `Organic sites (one active)...` and use these exact buttons one at a time:
   - `Create SG1_TokraDistressCallWorldSite`;
   - `Create SG1_TokraTemporaryBaseDeliverySite`;
   - `Create SG1_TokraJaffaOfficerCaptureSite`.
6. Verify that safehouse stages replace each other, organic sites replace each other and independent arcs remain.
7. Inspect `Player.log`.

Expected result: dedicated colored symbols appear at expanded zoom, vanilla world geometry appears at close zoom, the documented coexistence rules hold and no new texture, XML or C# error appears.

Final `r3` result: validated by the maintainer, including both zoom levels, safehouse and organic-site replacement behavior, independent-arc coexistence and `Player.log`.
