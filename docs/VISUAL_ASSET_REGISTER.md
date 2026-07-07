# Visual asset register

## Milestone

- Version: `0.3.85-dev`
- Branch: `feature/provisional-visual-asset-audit`
- Target assembly: `0.3.85.0`
- Status: final revision `r3` validated and published.

## Purpose

This document is the authoritative inventory of GateRim SG-1 visual assets.
It records every local texture family, every direct vanilla texture dependency
and the current art status without changing gameplay or replacing art in this
milestone. Paths are kept stable unless a later art milestone explicitly proves
that a technical rename is required.

The companion command `./tools/check-visual-assets.cmd` verifies that this
register and the repository remain synchronized. Corrective revision `r2` fixes Windows PowerShell path normalization after
`r1` retained a trailing period while removing `.png`. Corrective revision `r3`
then applies the maintainer-approved final-art boundary: only the preserved
public mod icon and the seven validated world-event site icons are final; every
other local visual family remains temporary.
The maintainer validated the complete `r3` register, priority order, automated
checks and progressive wiki-reference rule.

## Approved final references

- `About/ModIcon.png`: `final` public mod identity. It is intentionally outside
  the `Textures/` family count and must never be reused as gameplay art.
- The only `Textures/` families currently accepted as `final` are the seven
  validated world-event site icons listed below.
- Storyteller portraits, faction icons, equipment, buildings, genes, xenotypes,
  projectiles, pawns and command icons remain temporary even when they are
  functional or visually acceptable for development.
- As soon as a later visual is explicitly validated, update this register and
  the dedicated wiki reference page in the same revision. Do not postpone all
  visual references to one global wiki pass.

Validated world-event site families:

- `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective`
- `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield`
- `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage`
- `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition`
- `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact`
- `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal`
- `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous`

## Classification

| Status | Meaning |
|---|---|
| `final` | Dedicated custom art already accepted; no replacement is scheduled. |
| `temporary-original` | Custom GateRim art that works but remains below the definitive-art target. |
| `temporary-recolor` | A color variant derived from another temporary family. |
| `temporary-reuse` | One asset is intentionally standing in for a different object or concept. |
| `placeholder-personal-icon` | The project demon icon is reused as a visible gameplay placeholder. |
| `accepted-vanilla` | Vanilla art is semantically appropriate and may remain. |
| `placeholder-vanilla` | Vanilla art is functional but does not provide the required GateRim identity. |

| Priority | Meaning |
|---|---|
| `P0` | Misleading or highly visible placeholder; replace in the first definitive-art lot. |
| `P1` | Prominent temporary art; replace before presentation closure. |
| `P2` | Visible development art; replace after P0/P1. |
| `P3` | Low-priority polish or accepted generic dependency. |
| `done` | Accepted final art. |

## Audit summary

- Local PNG files: `608`.
- Local texture families: `75`.
- Accepted final local families: `7` world-event site icons.
- Temporary original families: `33`.
- Temporary recolor families: `15`.
- Temporary reuse families: `6`.
- Project-icon placeholder families: `14`.
- Priorities: `16` P0, `27` P1, `25` P2, `7` done.
- Direct external texture paths: `7`.
- Runtime vanilla icon constants: `2`.
- Missing referenced local texture families: `0`.
- Unreferenced local texture families: `0`.

## Highest-priority findings

1. Four command icons, seven gene icons, the Goa’uld-host xenotype icon and both
   ritual/incubation basins reuse the personal demon mod icon exactly.
2. The kara kesh and healing bracelet reuse the Zat’nik’tel inventory texture.
3. The Jaffa xenotype uses the unrelated vanilla Hussar icon.
4. The Prim’ta larva and free Goa’uld symbiote use the same image.
5. Several distinct Tok’ra mission objects reuse one intelligence-packet icon,
   and the relay sabotage device reuses the secure communicator.
6. Jaffa, officer, Tok’ra and SGC apparel are technically complete but remain
   temporary art families; many body-type variants are exact copies rather than
   tailored silhouettes.

## Local texture inventory

The file count includes directional and body-type variants belonging to the
same canonical family. Representative dimensions refer to the base image.

<!-- LOCAL_ASSET_TABLE_START -->
| Canonical path under `Textures/` | Files | Base size | Surface | Status | Priority | Referenced by | Audit note |
|---|---:|---:|---|---|---|---|---|
| `Genes/Icons/SG1_JaffaForeheadMark_Generic` | 1 | 128×128 | Gene UI | `temporary-original` | `P2` | SG1_JaffaForeheadMark_Generic | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Genes/Icons/SG1_JaffaForeheadMark_GenericGold` | 1 | 128×128 | Gene UI | `temporary-recolor` | `P2` | SG1_JaffaForeheadMark_GenericGold | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Genes/Icons/SG1_JaffaForeheadMark_GenericSilver` | 1 | 128×128 | Gene UI | `temporary-recolor` | `P2` | SG1_JaffaForeheadMark_GenericSilver | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Storytellers/SG1_Command` | 1 | 580×620 | Storyteller UI | `temporary-original` | `P1` | SG1_GateRimStoryteller | Functional custom portrait, but not explicitly accepted as final by the maintainer. |
| `Storytellers/SG1_Command_Tiny` | 1 | 122×130 | Storyteller UI | `temporary-original` | `P1` | SG1_GateRimStoryteller | Functional custom portrait, but not explicitly accepted as final by the maintainer. |
| `Things/Building/SG1_GoauldRitualBasin` | 1 | 64×64 | Map/building | `placeholder-personal-icon` | `P0` | SG1_GoauldRitualBasin | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `Things/Building/SG1_PrimtaIncubationBasin` | 1 | 64×64 | Map/building | `placeholder-personal-icon` | `P0` | SG1_PrimtaIncubationBasin, SG1_PrimtaPreservationBasin | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `Things/Building/SG1_TokraObservationPoint/SG1_TokraObservationPoint` | 1 | 64×64 | Map/building | `temporary-original` | `P1` | SG1_TokraObservationPoint | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Building/SG1_TokraSecureCommunicator` | 1 | 128×128 | Map/building | `temporary-reuse` | `P1` | SG1_TokraRelaySabotageDevice, SG1_TokraSecureCommunicator | Communicator art is also reused by the relay sabotage device. |
| `Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot` | 1 | 64×64 | Map/building | `temporary-original` | `P2` | SG1_TokraDeliveryDropSpot | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Item/Equipment/WeaponRanged/SG1_Bolas` | 1 | 128×128 | Map/inventory weapon | `temporary-original` | `P2` | SG1_Bolas | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Item/Equipment/WeaponRanged/SG1_MatokStaff` | 1 | 128×48 | Map/inventory weapon | `temporary-original` | `P2` | SG1_MatokStaff | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Item/Equipment/WeaponRanged/SG1_TokraHypodermicRifle` | 1 | 128×128 | Map/inventory weapon | `temporary-original` | `P2` | SG1_TokraHypodermicRifle | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Item/Equipment/WeaponRanged/SG1_ZatnikTel` | 1 | 128×128 | Map/inventory weapon | `temporary-original` | `P2` | SG1_ZatnikTel | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Item/SG1_PrimtaLarva` | 1 | 128×128 | Map/inventory item | `temporary-reuse` | `P1` | SG1_ImmaturePrimtaSymbiote, SG1_PrimtaLarva | Exact shared image between biologically distinct larval/symbiote forms. |
| `Things/Item/SG1_TokraMissionIntelPacket` | 1 | 64×64 | Map/inventory item | `temporary-reuse` | `P1` | SG1_TokraIntroductionArtifact, SG1_TokraMissionIntelPacket, SG1_TokraObservationDevice, SG1_TokraOrganicDeadDrop | One packet icon represents several different Tok’ra mission objects. |
| `Things/Item/SG1_TretoninDose` | 1 | 64×64 | Map/inventory item | `temporary-original` | `P2` | SG1_TretoninDose | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Pawn/Animal/SG1_GoauldSymbiote/SG1_GoauldSymbiote` | 1 | 128×128 | Pawn/map | `temporary-reuse` | `P1` | SG1_GoauldQueen, SG1_GoauldSymbiote, SG1_TokraSymbiote | Exact shared image between biologically distinct larval/symbiote forms. |
| `Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet` | 1 | 128×128 | Pawn apparel | `temporary-reuse` | `P0` | SG1_GoauldHealingBracelet | Exact reuse of the Zat texture for a different Goa’uld device. |
| `Things/Pawn/Humanlike/Apparel/JaffaDeployedHelmet/JaffaDeployedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaDeployedHelmet | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/JaffaGauntlets/JaffaGauntlets` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaGauntlets | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/JaffaHeavyArmor/JaffaHeavyArmor` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaHeavyArmor | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/JaffaLightArmor/JaffaLightArmor` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaLightArmor | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerArmor/JaffaOfficerArmor` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerArmor | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerDeployedHelmet/JaffaOfficerDeployedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerDeployedHelmet | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerRetractedHelmet/JaffaOfficerRetractedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerRetractedHelmet | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/JaffaReinforcedBoots/JaffaReinforcedBoots` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaReinforcedBoots | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/JaffaRetractedHelmet/JaffaRetractedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaRetractedHelmet | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh` | 1 | 128×128 | Pawn apparel | `temporary-reuse` | `P0` | SG1_KaraKesh | Exact reuse of the Zat texture for a different Goa’uld device. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalBoots/SGTacticalBoots` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalBoots | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalGloves/SGTacticalGloves` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalGloves | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalVest/SGTacticalVest` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalVest | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldCap/SGTeamFieldCap` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTeamFieldCap | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldHelmet/SGTeamFieldHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTeamFieldHelmet | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketBlack/SGTeamJacketBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamJacket | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketDesert/SGTeamJacketDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamJacket | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketOlive/SGTeamJacketOlive` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_OliveSGTeamJacket | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsBlack/SGTeamPantsBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamPants | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsDesert/SGTeamPantsDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamPants | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsOlive/SGTeamPantsOlive` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_OliveSGTeamPants | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniform/SGTeamUniform` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_GenericSGTeamUniform | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformBlack/SGTeamUniformBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamUniform | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformDesert/SGTeamUniformDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamUniform | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/Apparel/TokraFieldGarb/TokraFieldGarb` | 25 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_TokraFieldGarb | Custom functional art, but still part of the planned definitive equipment/presentation pass. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `temporary-recolor` | `P2` | SG1_JaffaForeheadMark_GenericGoldIntrinsic | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `temporary-original` | `P2` | SG1_JaffaForeheadMark_GenericIntrinsic | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `temporary-recolor` | `P2` | SG1_JaffaForeheadMark_GenericSilverIntrinsic | Color variant derived from another temporary family; keep path stable for final replacement. |
| `Things/Projectile/SG1_BolasProjectile` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_BolasProjectile | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Projectile/SG1_MatokBlast` | 1 | 64×32 | Map projectile | `temporary-original` | `P2` | SG1_MatokStaffProjectile | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Projectile/SG1_TokraHypodermicDart` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_TokraHypodermicDart | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `Things/Projectile/SG1_ZatnikTelBlast` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_ZatnikTelProjectile | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `UI/Commands/SG1_AutonomousHunt` | 1 | 64×64 | Command UI | `placeholder-personal-icon` | `P0` | C# Source/GateRimSG1/Goauld/Comp_GoauldForcedImplantation.cs | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Commands/SG1_EmergencyExtraction` | 1 | 64×64 | Command UI | `placeholder-personal-icon` | `P0` | C# Source/GateRimSG1/Goauld/HediffComp_GoauldEmergencyExtraction.cs | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Commands/SG1_ForcedImplantation` | 1 | 64×64 | Command UI | `placeholder-personal-icon` | `P0` | C# Source/GateRimSG1/Goauld/Comp_GoauldForcedImplantation.cs | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Commands/SG1_JaffaHelmetMode` | 1 | 64×64 | Command UI | `temporary-original` | `P2` | C# Source/GateRimSG1/Jaffa/Comp_RetractableJaffaHelmet.cs | Custom functional art that remains acceptable for development but is not yet accepted as final. |
| `UI/Commands/SG1_RitualImplantation` | 1 | 64×64 | Command UI | `placeholder-personal-icon` | `P0` | C# Source/GateRimSG1/Goauld/Comp_GoauldForcedImplantation.cs, C# Source/GateRimSG1/Jaffa/Comp_JaffaPrimtaCeremony.cs | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_GoauldLongevity` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_GoauldLongevity | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_JaffaLineage` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_JaffaLineage | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_JaffaLongevity` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_JaffaLongevity | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_JaffaPhysiology` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_JaffaPhysiology | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_JaffaPouchPotential` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_JaffaPouchPotential | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_JaffaSymbioteCompatibility` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_JaffaSymbioteCompatibility | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Genes/SG1_NaquadahBlood` | 1 | 64×64 | Gene UI | `placeholder-personal-icon` | `P0` | SG1_NaquadahBlood | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `UI/Xenotypes/SG1_GoauldHost` | 1 | 64×64 | Xenotype UI | `placeholder-personal-icon` | `P0` | SG1_GoauldHost | Exact reuse of the project demon icon; misleading for this gameplay surface. |
| `World/WorldObjects/Expanding/SG1_FreeJaffa` | 1 | 128×128 | World faction | `temporary-original` | `P1` | SG1_FreeJaffa | Functional faction identity, but only event-site icons are currently accepted as final. |
| `World/WorldObjects/Expanding/SG1_GoauldSystemLords` | 1 | 128×128 | World faction | `temporary-original` | `P1` | SG1_GoauldSystemLordPrototype | Functional faction identity, but only event-site icons are currently accepted as final. |
| `World/WorldObjects/Expanding/SG1_SGCExpedition` | 1 | 128×128 | World faction | `temporary-original` | `P1` | SG1_PlayerSGCExpedition | Functional faction identity, but only event-site icons are currently accepted as final. |
| `World/WorldObjects/Expanding/SG1_Tokra` | 1 | 128×128 | World faction | `temporary-original` | `P1` | SG1_Tokra | Functional faction identity, but only event-site icons are currently accepted as final. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraIntroductionArtifactWorldSite | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` | 1 | 64×64 | World site | `final` | `done` | SG1_GoauldOpenConflictBattlefieldSite | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraDecodedMissionWorldSite | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraJaffaOfficerCaptureSite | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraHiddenSafehouseMarker, SG1_TokraHiddenSafehouseSitePart | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraDistressCallWorldSite | Dedicated custom art already validated in its published presentation milestone. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraTemporaryBaseDeliverySite | Dedicated custom art already validated in its published presentation milestone. |
<!-- LOCAL_ASSET_TABLE_END -->

## Direct vanilla texture dependencies

These string paths are referenced directly by XML or C# and intentionally do
not resolve under the mod's `Textures/` directory.

<!-- EXTERNAL_ASSET_TABLE_START -->
| External path | Surface | Status | Priority | Referenced by | Audit note |
|---|---|---|---|---|---|
| `World/WorldObjects/DefaultSettlement` | World settlement | `accepted-vanilla` | `P3` | SG1_FreeJaffa, SG1_GoauldSystemLordPrototype, SG1_PlayerSGCExpedition | Intentional shared vanilla settlement silhouette; faction color and dedicated faction icons carry identity. |
| `World/WorldObjects/Sites/GenericSite` | World site base | `accepted-vanilla` | `P3` | SG1_GoauldOpenConflictBattlefieldSite, SG1_TokraDecodedMissionWorldSite, SG1_TokraDistressCallWorldSite, SG1_TokraHiddenSafehouseMarker, SG1_TokraHiddenSafehouseSitePart, SG1_TokraIntroductionArtifactWorldSite, SG1_TokraJaffaOfficerCaptureSite, SG1_TokraTemporaryBaseDeliverySite | Generic underlying site material retained because every visible expanding icon is dedicated. |
| `Things/Building/Door/DoorSimple_Mover` | Mission map building | `placeholder-vanilla` | `P2` | SG1_TokraRelaySiteDoor | Relay-site door still uses the vanilla simple-door mover graphic. |
| `Things/Building/Door/DoorSimple_MenuIcon` | Build/menu icon | `placeholder-vanilla` | `P2` | SG1_TokraRelaySiteDoor | Relay-site door still uses the vanilla simple-door menu icon. |
| `Things/Building/Linked/Sandbags_Atlas` | Mission map building | `placeholder-vanilla` | `P2` | SG1_TokraRelaySiteBarricade | Relay-site barricade still uses the vanilla sandbag atlas. |
| `UI/Icons/Study` | Command UI | `accepted-vanilla` | `P3` | SG1_TokraIntroductionArtifact | Generic study action icon is semantically correct and not a GateRim identity surface. |
| `UI/Icons/Xenotypes/Hussar` | Xenotype UI | `placeholder-vanilla` | `P0` | SG1_Jaffa | Jaffa currently display the unrelated Hussar xenotype icon and require dedicated final art. |
<!-- EXTERNAL_ASSET_TABLE_END -->

## Runtime vanilla icon constants

| Runtime icon | Usage | Status | Priority | Audit note |
|---|---|---|---|---|
| `TexCommand.Attack` | Kara kesh attack abilities | `accepted-vanilla` | `P3` | Generic attack affordance remains semantically correct. |
| `TexCommand.GatherSpotActive` | Tok’ra identity and symbiote controls | `placeholder-vanilla` | `P2` | Functionally clear but too generic for the final identity presentation. |

## Repository-level public icon

| Path | Status | Rule |
|---|---|---|
| `About/ModIcon.png` | `final` | Preserve Diablood's personal red-and-black demon icon. It is valid as the mod icon but must not be reused as object, gene, xenotype, building or command art. |

## Stable-path contract for later art lots

- Replace PNG contents in place whenever the Def and rendering contract are already stable.
- Do not rename Defs or texture paths merely to improve art.
- Preserve every required directional and body-type filename for apparel.
- Keep English Defs and French public documentation aligned when a visual identity changes.
- Update this register and run the visual checker in the same revision as every added, removed or renamed texture.
- A family may move to `final` only after its inventory icon, map/pawn rendering and relevant world or UI surface have been reviewed.

## Explicit non-effects

`0.3.85-dev` changes no texture content, Def behavior, rendering code, stats,
research, recipe, faction, mission, storyteller, save data or gameplay rule.
It only adds the durable register, automated checks, version metadata and
documentation required to plan later art replacement lots.

## Validation procedure

Run from the repository root:

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd
git diff --check
```

Expected visual-audit result:

```text
Local PNG files: 608
Local texture families: 75
Direct external texture paths: 7
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

Because no image or gameplay file changes, the focused manual review is limited
to checking the register's P0/P1 findings and confirming that RimWorld reaches
the main menu with version `0.3.85-dev` and no new XML, texture or C# error.
